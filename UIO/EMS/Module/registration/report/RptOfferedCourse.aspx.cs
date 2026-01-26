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

namespace EMS.miu.registration.report
{
    public partial class RptOfferedCourse : BasePage
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
                ShowMessage("Please Select A Program");
                return;
            }
            else if (acaCalId == 0)
            {
                ShowMessage("Please Select A Session");
                return;
            }
            else
            {
                LoadOfferedCourse(programId, acaCalId);
            }
        }

        private void LoadOfferedCourse(int programId, int acaCalId)
        {
            List<rOfferedCourse> List = new List<rOfferedCourse>();
            string program = ucProgram.selectedText;
            string session = ucSession.selectedText;

            ReportParameter p1 = new ReportParameter("Show_Program", program);
            ReportParameter p2 = new ReportParameter("Show_Session", session);

            List<rOfferedCourse> offeredCourseList = CourseManager.GetOfferedCourse(programId, acaCalId);

            if (offeredCourseList.Count != 0)
            {

                foreach (rOfferedCourse sPB in offeredCourseList)
                {
                    List.Add(sPB);
                }

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/miu/registration/report/RptOfferedCourse.rdlc");
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
                ReportDataSource rds = new ReportDataSource("OfferedCourseList", List);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                lblMessage.Text = "";
                //lblCount.Text = offeredCourseList.Count().ToString();
            }
            else
            {
                ShowMessage("NO Data Found. Enter Valid Program And Session");
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