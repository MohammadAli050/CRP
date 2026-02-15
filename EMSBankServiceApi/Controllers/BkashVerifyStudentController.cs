
using EMS.Module;
using EMSBankServiceApi.Models;
using LogicLayer.BusinessLogic;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

// Note: Ensure you have the necessary using directives for your project

namespace EMSBankServiceApi.Controllers
{
    namespace EMSBankServiceApi.Controllers
    {
        public class BkashVerifyStudentController : ApiController
        {
            // Cache settings to avoid repeated ConfigurationManager lookups
            private readonly string SecretKey = ConfigurationManager.AppSettings["SecretKey"];
            private readonly string UserName = ConfigurationManager.AppSettings["BkashUserName"];
            private readonly string Password = ConfigurationManager.AppSettings["BkashPassword"];

            [Route("api/BkashVerifyStudent")]
            [AcceptVerbs("GET", "POST")]
            public HttpResponseMessage BkashVerifyStudent([FromBody] BkashVerifyStudentRequest request)
            {
                var result = new BkashStudentModel();

                // 1. Initial Logging
                try
                {
                    string logDesc = $"BKash Payment API: ID {request.StudentId}, Type {request.BillingTypeId}, MY {request.MonthYear}";
                    //MisscellaneousCommonMethods.InsertLog(UserName, "Get Student Info By Payment App", logDesc, "", request.StudentId, "0", "BKash Payment API Controller Page", "api/BKashVerifyStudent");
                }
                catch { /* Keep empty as per original logic */ }

                try
                {
                    // 2. Comprehensive Validation
                    if (request == null || request.UserName != UserName || request.Password != Password)
                    {
                        return CreateResponse("Invalid UserName or Password.", "201");
                    }

                    if (string.IsNullOrWhiteSpace(request.StudentId))
                        return CreateResponse("Student ID is required.", "202");

                    if (string.IsNullOrWhiteSpace(request.BillingTypeId))
                        return CreateResponse("Billing Type is required.", "204");

                    string studentRoll = request.StudentId.Trim();
                    int billingTypeIdInt = int.TryParse(request.BillingTypeId.Trim(), out int bId) ? bId : 0;
                    BillDepositMasterResponseObj paymentObj = null;

                    // 3. Logic for Billing Type 1 (UCAM Student)
                    if (billingTypeIdInt == 1)
                    {
                        if (string.IsNullOrWhiteSpace(request.MonthYear))
                            return CreateResponse("Month Year is required.", "203");

                        var studentInfo = StudentManager.GetByRoll(studentRoll);
                        if (studentInfo == null)
                            return CreateResponse("StudentID not found.", "205");

                        result.StudentID = studentInfo.Roll;
                        var personObj = PersonManager.GetById(studentInfo.PersonID);
                        if (personObj != null)
                        {
                            result.StudentName = personObj.FullName ?? "";
                            result.StudentEmail = personObj.Email ?? "";
                            result.StudentMobile = personObj.Phone ?? "";
                        }

                        // Note: Your original code had PaymentObj as null for Type 1 
                        // and then returned "No unpaid bill found". 
                        // Ensure you populate PaymentObj here if Type 1 needs to work.
                    }

                    // 4. Logic for Billing Type 2 (Admission Candidate)
                    else if (billingTypeIdInt == 2)
                    {
                        if (!long.TryParse(studentRoll, out long paymentId))
                        {
                            return CreateResponse("Invalid Student ID format for Admission.", "205");
                        }

                        using (var admissionContext = new AdmissionDAL.Entities())
                        {
                            var candidatePayments = admissionContext.CandidatePayments
                                                    .Where(x => x.PaymentId == paymentId).ToList();

                            if (!candidatePayments.Any())
                                return CreateResponse("Candidate PaymentId not found.", "205");

                            if (candidatePayments.Count > 1)
                                return CreateResponse("Duplicate PaymentId found.", "212");

                            var candidatePayment = candidatePayments.First();
                            var personObj = admissionContext.BasicInfoes.FirstOrDefault(x => x.ID == candidatePayment.CandidateID);

                            result.StudentID = studentRoll;
                            if (personObj != null)
                            {
                                result.StudentName = personObj.FirstName ?? "";
                                result.StudentEmail = personObj.Email ?? "";
                                result.StudentMobile = personObj.SMSPhone ?? "";
                            }

                            paymentObj = new BillDepositMasterResponseObj
                            {
                                BillDepositMasterId = 0,
                                ReferanceNo = candidatePayment.PaymentId.ToString(),
                                IsPaid = Convert.ToBoolean(candidatePayment.IsPaid),
                                PaidAmount = Convert.ToDecimal(candidatePayment.Amount)
                            };
                        }
                    }

                    // 5. Payment Object Validation
                    if (paymentObj == null)
                        return CreateResponse("No unpaid bill found for your selection.", "209");

                    result.Amount = paymentObj.PaidAmount.ToString();
                    result.ReferenceNo = paymentObj.ReferanceNo;

                    if (paymentObj.IsPaid)
                    {
                        result.IsPaid = true;
                        return CreateResponse("Bill already paid for your selection.", "210");
                    }

                    // 6. Token Generation and Success Response
                    result.IsPaid = false;
                    string jwtToken = GenerateJWTToken(studentRoll);

                    if (!string.IsNullOrEmpty(jwtToken))
                    {
                        try
                        {
                            //MisscellaneousCommonMethods.InsertLog(UserName, "Get Student Info Success", $"ID: {request.StudentId}, Ref: {paymentObj.ReferanceNo}", "", request.StudentId, "0", "Controller", "api/BKashVerifyStudent");
                        }
                        catch { }

                        result.Token = jwtToken;
                        result.Message = "Valid Bill Reference Number.";
                        result.StatusCode = "200";
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }

                    return CreateResponse("Token generation failed.", "211");
                }
                catch (Exception ex)
                {
                    return CreateResponse($"An error occurred: {ex.Message}", "404");
                }
            }

            // Helper to keep the main method clean
            private HttpResponseMessage CreateResponse(string message, string statusCode)
            {
                var res = new BkashStudentModel { Message = message, StatusCode = statusCode };
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }

            public string GenerateJWTToken(string studentId)
            {
                try
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var claims = new[]
                    {
                    new Claim(ClaimTypes.Name, studentId),
                    new Claim("role", "ITAdmin")
                };

                    var token = new JwtSecurityToken(
                        issuer: "UCAM",
                        audience: "IT",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: credentials);

                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
                catch
                {
                    return string.Empty;
                }
            }


            public class BkashVerifyStudentRequest
            {
                public string UserName { get; set; }
                public string Password { get; set; }
                public string StudentId { get; set; }
                public string MonthYear { get; set; }
                public string BillingTypeId { get; set; }
            }

            public class BillDepositMasterResponseObj
            {
                public int BillDepositMasterId { get; set; }
                public string ReferanceNo { get; set; }
                public bool IsPaid { get; set; }
                public decimal PaidAmount { get; set; }
            }

        }
    }
}
