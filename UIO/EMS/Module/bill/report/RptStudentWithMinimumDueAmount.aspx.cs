using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects.RO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace EMS.miu.bill.report
{
    public partial class RptStudentWithMinimumDueAmount : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            if(!IsPostBack)
            {
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            ucRegistrationSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void OnRegistrationSessionSelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);
            int registrationAcaCalId = Convert.ToInt32(ucRegistrationSession.selectedValue);
            int batchId = Convert.ToInt32(ucBatch.selectedValue);
            string genderType = Convert.ToString(ddlGender.SelectedItem);

            decimal amount = 0;
            if (txtAmount.Text != string.Empty) 
            {
                amount = Convert.ToDecimal(txtAmount.Text);
            }
            if (genderType == "All")
            {
                genderType = null;
            }
            LoadStudentWithMinimumDue(programId,registrationAcaCalId, acaCalId, batchId, genderType, amount);
        }

        private void LoadStudentWithMinimumDue(int programId, int registrationAcaCalId, int acaCalId, int batchId, string genderType, decimal amount)
        {
            List<rStudentMinimumDue> list = null; //BillHistoryManager.GetStudentWithMinimumDue(programId, registrationAcaCalId, acaCalId, batchId, genderType, amount);

            string program = ucProgram.selectedText;
            string session = ucSession.selectedText;
            string batch = ucBatch.selectedText;
            string Gender = ddlGender.SelectedItem.Text;
            string minAmount = Convert.ToString(txtAmount.Text);

            ReportParameter p1 = new ReportParameter("Program", program);
            ReportParameter p2 = new ReportParameter("Session", session);
            ReportParameter p3 = new ReportParameter("Batch", batch);
            ReportParameter p4 = new ReportParameter("SGender", Gender);
            ReportParameter p5 = new ReportParameter("MinDue", minAmount);


            try
            {
                if (list.Count != 0)
                {
                    StudentWithMinimumDue.LocalReport.ReportPath = Server.MapPath("~/miu/bill/report/RptStudentWithMinimumDueAmount.rdlc");
                    this.StudentWithMinimumDue.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5 });
                    ReportDataSource rds = new ReportDataSource("StudentWithMinimumDue", list);

                    StudentWithMinimumDue.LocalReport.DataSources.Clear();
                    StudentWithMinimumDue.LocalReport.DataSources.Add(rds);
                    lblMessage.Text = "";
                }
                else
                {

                    ReportDataSource rds = new ReportDataSource("StudentWithMinimumDue", list);
                    StudentWithMinimumDue.LocalReport.DataSources.Add(rds);
                    StudentWithMinimumDue.LocalReport.DataSources.Clear();

                    lblMessage.Text = "No Data Found.";
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