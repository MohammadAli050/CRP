using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects.RO;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.bill.report
{
    public partial class RptStudentBillPaymentDue : BasePage
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
            lblMsg.Text = string.Empty;
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int sessionId = Convert.ToInt32(ucSession.selectedValue);
            int batchId = Convert.ToInt32(ucBatch.selectedValue);
            if (programId > 0 && batchId> 0) 
            {
                LoadReport(programId, batchId, sessionId);
                
            }
        }

        private void LoadReport(int programId, int batchId, int sessionId)
        {
            string program = ucProgram.selectedText;
            string batch = ucBatch.selectedText;
            string session = ucSession.selectedText;

            ReportParameter p1 = new ReportParameter("Program", program);
            ReportParameter p2 = new ReportParameter("Batch", batch);
            ReportParameter p3 = new ReportParameter("Session", session);

            List<rStudentBillPaymentDue> studentBillPaymentDueList = BillHistoryManager.GetBillPaymentDueByProgramIdBatchIdSessionId(programId, batchId, sessionId);

            try
            {
                if (studentBillPaymentDueList.Count != 0)
                {
                    StudentBillPaymentDue.LocalReport.ReportPath = Server.MapPath("~/miu/bill/report/RptStudentBillPaymentDue.rdlc");
                    this.StudentBillPaymentDue.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3 });
                    ReportDataSource rds = new ReportDataSource("StudentBillPaymentDue", studentBillPaymentDueList);

                    StudentBillPaymentDue.LocalReport.DataSources.Clear();
                    StudentBillPaymentDue.LocalReport.DataSources.Add(rds);
                    lblMsg.Text = "";
                }
                else
                {

                    ReportDataSource rds = new ReportDataSource("StudentBillPaymentDue", studentBillPaymentDueList);
                    StudentBillPaymentDue.LocalReport.DataSources.Add(rds);
                    StudentBillPaymentDue.LocalReport.DataSources.Clear();

                    lblMsg.Text = "No Data Found.";
                    return;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}