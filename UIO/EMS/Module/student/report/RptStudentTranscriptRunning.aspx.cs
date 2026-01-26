using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects.RO;
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
    public partial class RptStudentTranscriptRunning : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //base.CheckPage_Load();

            pnlMessage.Visible = false;

            if (!IsPostBack)
            {
                //lblCount.Text = "0";
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            string studentId = Convert.ToString(txtStudentId.Text);

            if (studentId == string.Empty)
            {
                ShowMessage("Please Give a Student Roll.");
                return;
            }

            else
            {
                LoadStudent(studentId);
            }

        }

        private void LoadStudent(string studentId)
        {
            List<rStudentTranscript> list = StudentManager.GetStudentTrancriptByIdRunning(studentId);

            List<rStudentGradeDetail> gradeDetailList = StudentGradeDetailManager.GetAllGrade(studentId);

            if (list.Count != 0)
            {
                TranscriptRunning.LocalReport.ReportPath = Server.MapPath("~/miu/student/report/RptStudentTranscriptRunning.rdlc");
                ReportDataSource rds = new ReportDataSource("StudentTranscript", list);
                ReportDataSource rds2 = new ReportDataSource("Grade", gradeDetailList);

                TranscriptRunning.LocalReport.DataSources.Clear();
                TranscriptRunning.LocalReport.DataSources.Add(rds);
                TranscriptRunning.LocalReport.DataSources.Add(rds2);
                lblMessage.Text = "";

            }
            else
            {
                ShowMessage("NO Data Found. Enter A Valid Student ID");
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