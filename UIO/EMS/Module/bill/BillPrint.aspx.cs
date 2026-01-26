using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.bill
{
    public partial class BillPrint : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            if (!IsPostBack)
            {
                ucProgram.LoadDropdownWithUserAccess(userObj.Id);
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
                ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            }
            catch (Exception ex)
            {
                lblMsg.Text = "On Program Selected Index Changed Error.";
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            LoadBillForDelete();
        }

        private void LoadBillForDelete()
        {
            List<BillDeleteDTO> billPrintList = new List<BillDeleteDTO>();
            if (string.IsNullOrEmpty(txtStudentRoll.Text))
            {
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int sessionId = Convert.ToInt32(ucSession.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);
                int studentId = 0;
                DateTime? date = null;

                billPrintList = BillHistoryManager.GetBillForDelete(programId, batchId, sessionId, studentId, date);
            }
            else if (!string.IsNullOrEmpty(txtStudentRoll.Text))
            {
                int programId = 0;
                int sessionId = 0;
                int batchId = 0;
                int studentId = 0;
                DateTime? date = null;

                string studentRoll = Convert.ToString(txtStudentRoll.Text);
                Student studentObj = StudentManager.GetByRoll(studentRoll);
                if (studentObj != null)
                {
                    studentId = studentObj.StudentID;
                    billPrintList = BillHistoryManager.GetBillForDelete(programId, batchId, sessionId, studentId, date);
                }
            }
            else
            {
                int programId = 0;
                int sessionId = 0;
                int batchId = 0;
                int studentId = 0;
                DateTime? date = null;

                billPrintList = BillHistoryManager.GetBillForDelete(programId, batchId, sessionId, studentId, date);
            }
            GvBillPrint.DataSource = billPrintList;
            GvBillPrint.DataBind();
        }

        protected void GvBillPrint_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int billHistoryMasterId = Convert.ToInt32(e.CommandArgument);
            if (billHistoryMasterId > 0)
            {
                BillHistoryMaster billHistoryMasterObj = BillHistoryMasterManager.GetById(billHistoryMasterId);
                if (billHistoryMasterObj != null)
                {
                    LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetById(billHistoryMasterObj.StudentId);
                    List<BillHistory> billHistoryList = BillHistoryManager.GetForBillPrintByBillHistoryMasterId(billHistoryMasterId);
                    if (billHistoryList != null && billHistoryList.Count > 0)
                    {
                        List<string> accountNoList = new List<string>();
                        accountNoList = billHistoryList.Select(d => d.FundAccountNo).Distinct().ToList();
                        if (accountNoList != null)
                        {
                            for (int i = 0; i < accountNoList.Count; i++)
                            {
                                List<BillHistory> fundBillHistory = billHistoryList.Where(d => d.FundAccountNo == accountNoList[i]).ToList();
                                if (fundBillHistory != null && fundBillHistory.Count > 0)
                                {
                                    PrintBillHistory(fundBillHistory, studentObj, accountNoList[i]);
                                }
                            }
                        }
                        else
                        {
                            lblMsg.Text = "No account number found.";
                        }
                    }
                    else
                    {
                        lblMsg.Text = "No bill found.";
                    }
                }
            }
        }

        //BussinessObject.UIUMSUser userObj = null;

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    base.CheckPage_Load();
        //    userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

        //    if (!IsPostBack)
        //    {
        //    }
        //}

        //protected void btnLoad_Click(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtStudentRoll.Text))
        //    {
        //        string studentRoll = Convert.ToString(txtStudentRoll.Text);
        //        LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetByRoll(studentRoll);
        //        if (studentObj != null)
        //        {
        //            AcademicCalender academicCalenderObj = AcademicCalenderManager.GetIsCurrentRegistrationByProgramId(studentObj.ProgramID);
        //            if (academicCalenderObj != null)
        //            {
         //               List<BillHistory> billHistoryList = BillHistoryManager.GetForBillPrintByStudentIdSessionId(studentObj.StudentID, academicCalenderObj.AcademicCalenderID);
        //                if (billHistoryList != null && billHistoryList.Count > 0)
        //                {
        //                    List<string> accountNoList = new List<string>();
        //                    accountNoList = billHistoryList.Select(d => d.FundAccountNo).Distinct().ToList();
        //                    if (accountNoList != null)
        //                    {
        //                        for (int i = 0; i < accountNoList.Count; i++)
        //                        {
        //                            List<BillHistory> fundBillHistory = billHistoryList.Where(d => d.FundAccountNo == accountNoList[i]).ToList();
        //                            if (fundBillHistory != null && fundBillHistory.Count > 0)
        //                            {
        //                                PrintBillHistory(fundBillHistory, studentObj, accountNoList[i]);
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        lblMsg.Text = "No account number found.";
        //                    }
        //                }
        //                else 
        //                {
        //                    lblMsg.Text = "No bill found.";
        //                }
        //            }
        //            else
        //            {
        //                lblMsg.Text = "Current registration session not found.";
        //            }
        //        }
        //        else
        //        {
        //            lblMsg.Text = "Student not found.";
        //        }
        //    }
        //    else 
        //    {
        //        lblMsg.Text = "Please provide a Student Id.";
        //    }
        //    //GetForBillPrintByStudentIdSessionId()
        //}

        private void PrintBillHistory(List<BillHistory> fundBillHistory, Student studentObj, string fundAccountNo)
        {
            try
            {
                lblMsg.Text = string.Empty;
                FundType fundTypeObj = new FundType();
                StudentRegistration studentRegistrationObj = StudentRegistrationManager.GetByStudentId(studentObj.StudentID);

                string fundName = null;
                string sessionName = null;
                if (fundAccountNo != null)
                {
                    fundTypeObj = FundTypeManager.GetAll().Where(d => d.AccountNo == fundAccountNo).FirstOrDefault();
                    fundName = fundTypeObj.FundName;
                }


                if (studentRegistrationObj != null)
                {
                    StudentSession studentSessionObj = StudentSessionManager.GetById(studentRegistrationObj.SessionId);
                    if (studentSessionObj != null)
                    {
                        sessionName = studentSessionObj.Session;
                    }
                }


                ReportViewer MoneyReceiptReport = new ReportViewer();

                ReportParameter p1 = new ReportParameter("StudentName", studentObj.Name);
                ReportParameter p2 = new ReportParameter("StudentRoll", studentObj.Roll);
                ReportParameter p3 = new ReportParameter("Program", studentObj.Program.ShortName);
                ReportParameter p4 = new ReportParameter("FundName", fundName);
                ReportParameter p5 = new ReportParameter("FundAccountNo", fundAccountNo);
                ReportParameter p6 = new ReportParameter("Session", sessionName);
                //ReportParameter p12 = new ReportParameter("TotalPaid", Convert.ToString(std.StudentBillPayment.TotalPaid));
                //ReportParameter p13 = new ReportParameter("LatestPaymentDate", latestPaymentDate);
                if (fundBillHistory != null && fundBillHistory.Count > 0)
                {
                    MoneyReceiptReport.LocalReport.ReportPath = Server.MapPath("~/miu/bill/report/RptBillPrint.rdlc");
                    MoneyReceiptReport.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6 });
                    ReportDataSource rds = new ReportDataSource("MoneyReceipt", fundBillHistory);

                    MoneyReceiptReport.LocalReport.DataSources.Clear();
                    MoneyReceiptReport.LocalReport.DataSources.Add(rds);

                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;

                    byte[] bytes = MoneyReceiptReport.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                    using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportPDF/" + "StudentMoneyReceipt" + ".pdf"), FileMode.Create))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }

                    string url = string.Format("~/miu/bill/report/MoneyReceipt.aspx?FN={0}", "StudentMoneyReceipt" + ".pdf");

                    Response.Redirect(url);


                }
                else
                {
                    ReportDataSource rds = null;

                    MoneyReceiptReport.LocalReport.DataSources.Clear();
                    MoneyReceiptReport.LocalReport.DataSources.Add(rds);
                    lblMsg.Text = "No bill found.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
    }
}