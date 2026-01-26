using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.bill.report
{
    public partial class RptAccountStatement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //base.CheckPage_Load();

            pnlMessage.Visible = false;

            if (!IsPostBack)
            {

            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            int programId = Convert.ToInt32(ucProgram.selectedValue);

        }
        protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
        {
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);
            int batchId = Convert.ToInt32(ucBatch.selectedValue);

            LoadRunningStudentList(programId, acaCalId, batchId);
        }

        private void LoadRunningStudentList(int programId, int acaCalId, int batchId)
        {
            List<RunningStudent> runningStudentList = StudentManager.GetRunningStudentByProgramIdAcaCalId(programId, acaCalId, batchId);

            ddlRunningStudent.Items.Clear();
            ddlRunningStudent.AppendDataBoundItems = true;

            if (runningStudentList != null)
            {
                ddlRunningStudent.Items.Add(new ListItem("All", ""));
                ddlRunningStudent.DataTextField = "Roll";
                ddlRunningStudent.DataValueField = "Roll";


                ddlRunningStudent.DataSource = runningStudentList;
                ddlRunningStudent.DataBind();
            }
        }

        protected void ddlRunningStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void buttonView_Click(object sender, EventArgs e)
        {
            int batchId = Convert.ToInt32(ucBatch.selectedValue);
            string roll = Convert.ToString(ddlRunningStudent.SelectedValue);
            string fromDate = dateTextBox1.Text.Trim();
            string toDate = dateTextBox2.Text.Trim();

            LoadStudentAccountStatement(batchId, roll, fromDate, toDate);
        }

        private void LoadStudentAccountStatement(int batchId, string roll, string fromDate, string toDate)
        {
            List<rAccountStatement> list = null; //BillHistoryManager.GetStudentAccountStatement(batchId, roll, fromDate, toDate);

            string fDate = dateTextBox1.Text.Trim();
            string tDate = dateTextBox2.Text.Trim();

            ReportParameter p1 = new ReportParameter("FromDate", fDate);
            ReportParameter p2 = new ReportParameter("ToDate", tDate);

            try
            {
                if (list.Count != 0)
                {
                    StudentAccountStatement.LocalReport.ReportPath = Server.MapPath("~/miu/bill/report/RptAccountStatement.rdlc");
                    this.StudentAccountStatement.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
                    ReportDataSource rds = new ReportDataSource("StudentAccountStatement", list);

                    StudentAccountStatement.LocalReport.DataSources.Clear();
                    StudentAccountStatement.LocalReport.DataSources.Add(rds);
                    lblMessage.Text = "";
                }
                else
                {
                    ShowMessage("NO Data Found.");
                    return;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        protected void buttonPrint_Click(object sender, EventArgs e)
        {

        }

        private void ShowMessage(string msg)
        {
            pnlMessage.Visible = true;

            lblMessage.Text = msg;
            lblMessage.ForeColor = Color.Red;
        }
    }
}