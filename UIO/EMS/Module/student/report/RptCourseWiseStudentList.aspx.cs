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

namespace EMS.miu.student.report
{
    public partial class RptCourseWiseStudentList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();

            pnlMessage.Visible = false;

            if (!IsPostBack)
            {
                //lblCount.Text = "0";
            }

        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));

        }
        protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);

            if (programId == 0)
            {
                ShowMessage("Please Enter A Program");
                return;
            }
            else if (acaCalId == 0)
            {
                ShowMessage("Please Enter A Session");
                return;
            }
            else
            {
                LoadCourseWiseStudentList(programId, acaCalId);
            }

        }

        private void LoadCourseWiseStudentList(int programId, int acaCalId)
        {          
            List<rCourseWiseStudentList> list = new List<rCourseWiseStudentList>();
            list = CourseManager.GetCourseWiseStudentList(programId, acaCalId);
            lblMessage.Text = "";

            if (list.Count != 0)
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/miu/student/report/RptCourseWiseStudentList.rdlc");
                ReportDataSource rds = new ReportDataSource("CourseWiseStudentList", list); 
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                lblMessage.Text = "";
                //lblCount.Text = list.Count().ToString();
            }

            else
            {
                ReportViewer1.LocalReport.DataSources.Clear();
                ShowMessage("NO Data Found. Please Enter Valid Program And Session");
                return;
            }
            
        }

        private void ShowMessage(string msg)
        {
            pnlMessage.Visible = true;

            lblMessage.Text = msg;
            lblMessage.ForeColor = Color.Red;

        }
    }
}