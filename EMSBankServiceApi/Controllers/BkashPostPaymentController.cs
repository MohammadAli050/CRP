using EMS.Module;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using EntityState = System.Data.Entity.EntityState;


namespace EMSBankServiceApi.Controllers
{
    public class BkashPostPaymentController : ApiController
    {
        private readonly string SecretKey = ConfigurationManager.AppSettings["SecretKey"];
        private readonly string UserName = ConfigurationManager.AppSettings["BkashUserName"];
        private readonly string Password = ConfigurationManager.AppSettings["BkashPassword"];

        [Route("api/BkashPostPayment")]
        [AcceptVerbs("GET", "POST")]
        public HttpResponseMessage BkashPostPayment([FromBody] BkashPaymentRequestModel request)
        {
            var result = new BkashPaymentResponseModel();

            // 1. Initial Logging
            try
            {
                //MisscellaneousCommonMethods.InsertLog(UserName, "Post Payment Request By Bkash App",
                //    $"Bkash API: ID {request.StudentId}, Ref {request.ReferenceNo}, Amt {request.Amount}, Trx {request.TransactionCode}",
                //    "", request.StudentId, "0", "Bkash Payment API Controller Page", "api/BkashPostPayment");
            }
            catch { }

            // 2. Security & Header Validation
            var authHeader = Request.Headers.Authorization;
            if (authHeader == null || authHeader.Scheme != "Bearer" || string.IsNullOrEmpty(authHeader.Parameter))
            {
                return CreateErrorResponse(result, "1000", "Authorization header is missing or invalid.");
            }

            if (request.UserName != UserName || request.Password != Password)
            {
                return CreateErrorResponse(result, "201", "Invalid UserName or Password.");
            }

            try
            {
                // 3. Basic Input Validation
                if (string.IsNullOrWhiteSpace(request.StudentId)) return CreateErrorResponse(result, "202", "Invalid Student ID.");
                if (string.IsNullOrWhiteSpace(request.ReferenceNo)) return CreateErrorResponse(result, "203", "Invalid Reference No.");
                if (string.IsNullOrWhiteSpace(request.BillingTypeId)) return CreateErrorResponse(result, "204", "Billing Type is required.");
                if (string.IsNullOrWhiteSpace(request.TransactionCode)) return CreateErrorResponse(result, "211", "Invalid Transaction Code.");

                string studentId = request.StudentId.Trim();
                string referenceNo = request.ReferenceNo.Trim();
                string transactionCode = request.TransactionCode.Trim();
                int billingTypeId = Convert.ToInt32(request.BillingTypeId);

                if (!decimal.TryParse(request.Amount, out decimal amount) || amount <= 0)
                {
                    return CreateErrorResponse(result, amount <= 0 ? "210" : "209", amount <= 0 ? "Amount should be positive." : "Invalid Amount.");
                }

                result.StudentId = studentId;
                result.ReferenceNo = referenceNo;

                // 4. JWT Token Validation
                try
                {
                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "UCAM",
                        ValidAudience = "IT",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey))
                    };

                    var principal = new JwtSecurityTokenHandler().ValidateToken(authHeader.Parameter, validationParameters, out _);
                    if (principal.FindFirst(ClaimTypes.Name)?.Value != studentId)
                    {
                        return CreateErrorResponse(result, "405", "Token does not match StudentId.");
                    }
                }
                catch { return CreateErrorResponse(result, "406", "Invalid or expired token."); }

                // 5. Logic for Billing Type 1: UCAM
                if (billingTypeId == 1)
                {
                    var studentInfo = StudentManager.GetByRoll(studentId);
                    if (studentInfo == null) return CreateErrorResponse(result, "205", "StudentID not found.");

                    PopulatePersonDetails(result, studentInfo.PersonID);

                    using (var ucamcontext = new UCAMDAL.UCAMEntities())
                    {
                        var paymentInfo = ucamcontext.BillHistoryMasters.FirstOrDefault(x => x.StudentId == studentInfo.StudentID && x.ReferenceNo == referenceNo && (x.IsDeleted == null || x.IsDeleted == false));

                        if (paymentInfo == null) return CreateErrorResponse(result, "216", "Bill Reference not found.");
                        if (paymentInfo.IsDue == false) { result.IsPaid = true; return CreateErrorResponse(result, "207", "Bill already paid."); }
                        if (amount != paymentInfo.Amount) return CreateErrorResponse(result, "212", "Amount does not match bill.");

                        if (ucamcontext.CollectionHistories.Any(x => x.TransactionCode == transactionCode && (x.IsDeleted == null || x.IsDeleted == false)))
                            return CreateErrorResponse(result, "214", "Payment already exists with this transaction code.");

                        if (ucamcontext.CollectionHistories.Any(x => x.ReferenceNo == referenceNo && (x.IsDeleted == null || x.IsDeleted == false)))
                            return CreateErrorResponse(result, "213", "Payment already exists with this reference no.");

                        // Process Payment
                        var collectionHistoryObj = new UCAMDAL.CollectionHistory
                        {
                            StudentId = studentInfo.StudentID,
                            Amount = paymentInfo.Amount ?? 0,
                            AcaCalId = paymentInfo.AcaCalId ?? 0,
                            ReferenceNo = referenceNo,
                            TransactionCode = transactionCode,
                            PaymentType = "2", // bKash
                            TypeDefinitionId = -2,
                            CollectionDate = DateTime.Now,
                            CreatedBy = 3, // BkashUserId
                            CreatedDate = DateTime.Now,
                            IsDeleted = false
                        };

                        ucamcontext.CollectionHistories.Add(collectionHistoryObj);
                        paymentInfo.IsDue = false;
                        paymentInfo.ModifiedBy = 3; // BkashUserId
                        paymentInfo.ModifiedDate = DateTime.Now;
                        ucamcontext.Entry(paymentInfo).State = EntityState.Modified;

                        ucamcontext.SaveChanges();
                        return CreateSuccessResponse(result, amount);
                    }
                }

                // 6. Logic for Billing Type 2: Admission
                else if (billingTypeId == 2)
                {
                    if (!long.TryParse(studentId, out long pIdLong)) return CreateErrorResponse(result, "205", "Invalid ID.");

                    using (var admissionContext = new AdmissionDAL.Entities())
                    {
                        var candidatePayment = admissionContext.CandidatePayments.FirstOrDefault(x => x.PaymentId == pIdLong);
                        if (candidatePayment == null) return CreateErrorResponse(result, "205", "Candidate PaymentId not found.");
                        if (candidatePayment.IsPaid == true) { result.IsPaid = true; return CreateErrorResponse(result, "207", "Already paid."); }
                        if (amount != candidatePayment.Amount) return CreateErrorResponse(result, "212", "Amount mismatch.");

                        if (admissionContext.OnlineCollectionAttempts.Any(x => x.TransactionId == transactionCode))
                            return CreateErrorResponse(result, "214", "Transaction code already used.");

                        candidatePayment.IsPaid = true;
                        candidatePayment.Attribute1 = transactionCode;
                        candidatePayment.ModifiedBy = 215; // Admission User ID
                        candidatePayment.DateModified = DateTime.Now;

                        var attempt = new AdmissionDAL.OnlineCollectionAttempt
                        {
                            CandidateId = candidatePayment.CandidateID,
                            PaymentId = candidatePayment.ID,
                            TransactionId = transactionCode,
                            PaymentAmount = candidatePayment.Amount,
                            IsPaid = true,
                            Remarks = "Success via Bkash App.",
                            CreatedBy = 215,
                            CreatedDate = DateTime.Now
                        };

                        admissionContext.OnlineCollectionAttempts.Add(attempt);
                        admissionContext.Entry(candidatePayment).State = EntityState.Modified;
                        admissionContext.SaveChanges();

                        return CreateSuccessResponse(result, amount);
                    }
                }

                return CreateErrorResponse(result, "217", "Invalid Billing Type.");
            }
            catch (Exception ex)
            {
                result.StatusCode = "404";
                result.Message = "Error: " + (ex.InnerException?.Message ?? ex.Message);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        private void PopulatePersonDetails(BkashPaymentResponseModel res, int personId)
        {
            var p = PersonManager.GetById(personId);
            if (p != null)
            {
                res.StudentName = p.FullName ?? "";
                res.StudentEmail = p.Email ?? "";
                res.StudentMobile = p.Phone ?? "";
            }
        }

        private HttpResponseMessage CreateErrorResponse(BkashPaymentResponseModel res, string code, string msg)
        {
            res.StatusCode = code; res.Message = msg;
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        private HttpResponseMessage CreateSuccessResponse(BkashPaymentResponseModel res, decimal amt)
        {
            res.IsPaid = true; res.Amount = amt.ToString();
            res.DateTime = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");
            res.StatusCode = "200"; res.Message = "Payment updated successfully.";
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }


        #region Payment Verify By TransactionId API

        [Route("api/VerifyPaymentByTransactionId")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage VerifyPaymentByTransactionId([FromBody] BkashPaymentVerifyRequestModel request)
        {
            var result = new BkashPaymentResponseModel();

            #region Log Insert
            try
            {
                //MisscellaneousCommonMethods.InsertLog(UserName, "Verify Payment Request By Bkash App", "Bkash Payment API Verify Payment Request with  TransactionCode : " + request.TransactionCode, "", "", "0", "Bkash Payment API Controller Page", "api/VerifyPaymentByTransactionId");
            }
            catch (Exception ex)
            {
            }
            #endregion

            if (request.UserName != UserName || request.Password != Password)
            {
                result.Message = "Invalid UserName or Password.";
                result.StatusCode = "201";
                return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
            }
            try
            {
                #region Check Transaction Code is Valid
                string TransactionCode = request.TransactionCode.Trim();

                if (string.IsNullOrWhiteSpace(TransactionCode))
                {
                    result.Message = "Invalid Transaction Code.";
                    result.StatusCode = "211";
                    return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
                }

                #endregion

                #region Check Billing Type No is Empty
                // Check if Billing Type is valid
                if (string.IsNullOrWhiteSpace(request.BillingTypeId))
                {
                    result.Message = "Billing Type is required.";
                    result.StatusCode = "204";
                    return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
                }

                #endregion

                int BillingTypeId = Convert.ToInt32(request.BillingTypeId);

                #region UCAM Billing Type

                if (BillingTypeId == 1)
                {
                    using (var ucamcontext = new UCAMDAL.UCAMEntities())
                    {
                        var CollectionExistsWithThisTransaction = ucamcontext.CollectionHistories.Where(x => x.TransactionCode == TransactionCode && (x.IsDeleted == null || x.IsDeleted == false)).FirstOrDefault();
                        if (CollectionExistsWithThisTransaction != null)
                        {
                            var studentInfo = StudentManager.GetById(CollectionExistsWithThisTransaction.StudentId);
                            if (studentInfo != null)
                            {
                                result.StudentId = studentInfo.Roll;
                                var PersonObj = PersonManager.GetById(studentInfo.PersonID);
                                if (PersonObj != null)
                                {
                                    result.StudentName = PersonObj.FullName != null ? PersonObj.FullName : "";
                                    result.StudentEmail = PersonObj.Email != null ? PersonObj.Email : "";
                                    result.StudentMobile = PersonObj.Phone != null ? PersonObj.Phone : "";
                                }
                            }
                            result.ReferenceNo = CollectionExistsWithThisTransaction.ReferenceNo;
                            result.Amount = CollectionExistsWithThisTransaction.Amount.ToString();
                            result.TransactionCode = CollectionExistsWithThisTransaction.Attribute1;
                            result.IsPaid = true;
                            if (CollectionExistsWithThisTransaction.CollectionDate != null)
                                result.DateTime = Convert.ToDateTime(CollectionExistsWithThisTransaction.CollectionDate).ToString("dd-MM-yyyy hh:mm tt");

                            result.StatusCode = "200";
                            result.Message = "Payment found successfully.";
                            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
                        }
                        else
                        {
                            result.IsPaid = false;
                            result.StatusCode = "208";
                            result.Message = "Payment not found with this transaction code.";
                            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
                        }
                    }
                }
                #endregion

                #region Admission Billing Type
                else if (BillingTypeId == 2)
                {
                    using (var admissionContext = new AdmissionDAL.Entities())
                    {
                        var CollectionExistsWithThisTransaction = admissionContext.OnlineCollectionAttempts.Where(x => x.TransactionId == TransactionCode).FirstOrDefault();
                        if (CollectionExistsWithThisTransaction != null)
                        {
                            var candidatepaymentObj = admissionContext.CandidatePayments.Where(x => x.PaymentId == CollectionExistsWithThisTransaction.PaymentId).FirstOrDefault();

                            if (candidatepaymentObj != null)
                            {
                                result.StudentId = candidatepaymentObj.PaymentId.ToString();
                                var candiateBasicObj = admissionContext.BasicInfoes.Where(x => x.ID == candidatepaymentObj.CandidateID).FirstOrDefault();
                                if (candiateBasicObj != null)
                                {
                                    result.StudentName = candiateBasicObj.FirstName != null ? candiateBasicObj.FirstName : "";
                                    result.StudentEmail = candiateBasicObj.Email != null ? candiateBasicObj.Email : "";
                                    result.StudentMobile = candiateBasicObj.SMSPhone != null ? candiateBasicObj.SMSPhone : "";
                                }
                            }
                            result.ReferenceNo = CollectionExistsWithThisTransaction.PaymentId.ToString();
                            result.Amount = CollectionExistsWithThisTransaction.PaymentAmount.ToString();
                            result.TransactionCode = CollectionExistsWithThisTransaction.TransactionId;
                            result.IsPaid = true;
                            if (CollectionExistsWithThisTransaction.CreatedDate != null)
                                result.DateTime = Convert.ToDateTime(CollectionExistsWithThisTransaction.CreatedDate).ToString("dd-MM-yyyy hh:mm tt");

                            result.StatusCode = "200";
                            result.Message = "Payment found successfully.";
                            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
                        }
                        else
                        {
                            result.IsPaid = false;
                            result.StatusCode = "208";
                            result.Message = "Payment not found with this transaction code.";
                            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
                        }
                    }
                }
                #endregion

                #region Invalid Billing Type
                else
                {
                    result.Message = "Invalid Billing Type.";
                    result.StatusCode = "217";
                    return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
                }
                #endregion

            }
            catch (Exception ex)
            {
                result.StatusCode = "404";
                result.Message = "An error occurred during validation transactionId." + ex.InnerException.Message;
                return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
            }
        }

        #endregion



        #region Temporary Model

        public class BkashPaymentRequestModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string StudentId { get; set; }
            public string ReferenceNo { get; set; }
            public string Amount { get; set; }
            public string BillingTypeId { get; set; }
            public string TransactionCode { get; set; }
        }
        public class BkashPaymentVerifyRequestModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string TransactionCode { get; set; }
            public string BillingTypeId { get; set; }
        }
        public class BkashPaymentResponseModel
        {
            public string StudentId { get; set; }
            public string ReferenceNo { get; set; }
            public string Amount { get; set; }
            public string TransactionCode { get; set; }
            public string StatusCode { get; set; }
            public string Message { get; set; }
            public bool IsPaid { get; set; }
            public string StudentName { get; set; }
            public string StudentEmail { get; set; }
            public string StudentMobile { get; set; }
            public string DateTime { get; set; }
        }

        #endregion
    }
}

