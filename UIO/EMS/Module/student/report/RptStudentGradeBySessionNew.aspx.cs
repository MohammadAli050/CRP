using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.student.report
{
    public partial class RptStudentGradeBySessionNew : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();

            pnlMessage.Visible = false;

            if (!IsPostBack)
            {
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            string roll = txtStudentID.Text.Trim();

            if (string.IsNullOrEmpty(roll))
            {
                lblMessage.Text = "Please select any student's roll.";
                return;
            }
            Student student = StudentManager.GetByRoll(roll);

            if (student != null)
            {

                List<StudentGrade> StudentGradeList = ExamMarkManager.GetGradeReportByRollSesmester(roll);
                List<rCreditGPA> creditGPAList = ExamMarkManager.GetGradeReportCreditGPAByRoll(student.ProgramID,student.BatchId, roll);

                if (StudentGradeList.Count != 0)
                {
                    string CalenderMasterType = CalenderUnitMasterManager.GetById(student.Program.CalenderUnitMasterID).Name;
                    ReportParameter p1 = new ReportParameter("Name", student.BasicInfo.FullName);
                    ReportParameter p2 = new ReportParameter("Roll", student.Roll);
                    ReportParameter p3 = new ReportParameter("Faculty", student.Program.DetailName);
                    ReportParameter p4 = new ReportParameter("CalenderMasterType", CalenderMasterType);

                    StudentGradeReport.LocalReport.ReportPath = Server.MapPath("~/miu/student/report/RptStudentGradeBySessionNew.rdlc");
                    this.StudentGradeReport.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4 });

                    ReportDataSource rds = new ReportDataSource("GradeReportDataSet", StudentGradeList);
                    ReportDataSource rds2 = new ReportDataSource("CGPACreditDataSet", creditGPAList);

                    StudentGradeReport.LocalReport.DataSources.Clear();
                    StudentGradeReport.LocalReport.DataSources.Add(rds);
                    StudentGradeReport.LocalReport.DataSources.Add(rds2);

                }
                else
                {
                    ShowMessage("Student Have To Complete At Least One Exam For GPA");
                }
            }
            else
            {
                ShowMessage("No Data Found! Enter Valid StudentID");
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