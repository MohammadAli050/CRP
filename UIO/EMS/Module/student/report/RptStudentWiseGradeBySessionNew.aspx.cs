using BussinessObject;
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
    public partial class RptStudentWiseGradeBySessionNew : BasePage
    {
        UIUMSUser userObj = null;
        string Roll = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtStudentRoll.Enabled = false;
            base.CheckPage_Load();
            
            pnlMessage.Visible = false;

            if (!IsPostBack)
            {
                userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
                Roll = userObj.LogInID;
                txtStudentRoll.Text = Roll.ToString();
                FillAcademicCalenderCombo(Roll);
            }
        }

        private void FillAcademicCalenderCombo(string Roll)
        {
            try
            {
                
                ddlSession.Items.Clear();
                List<rAcaCalSessionListByProgram> list = AcademicCalenderManager.GetAcaCalSessionListCompleted(Roll);

                ddlSession.Items.Add(new ListItem("-Select-", "0"));
                ddlSession.AppendDataBoundItems = true;

                if (list != null)
                {
                    list = list.OrderByDescending(x => x.Year).ToList();
                    int count = list.Count;
                    foreach (rAcaCalSessionListByProgram academicCalender in list)
                    {
                        ddlSession.Items.Add(new ListItem(academicCalender.TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                        count = academicCalender.AcademicCalenderID;
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }
        protected void buttonView_Click(object sender, EventArgs e)
        {
            int acaCalId = Convert.ToInt32(ddlSession.SelectedValue);
            if (acaCalId != 0)
            {
                LoadGradeReport();
            }
            else
            {
                ShowMessage("Please Select a Semester");
            }
        }

        //View Report On Webpage
        private void LoadGradeReport()
        {
            int acaCalId = Convert.ToInt32(ddlSession.SelectedValue);
            string Roll = txtStudentRoll.Text.Trim().ToString();
            string[] typeAndYear = ddlSession.SelectedItem.Text.Split(' ');
            string session = typeAndYear[0] + ", " + typeAndYear[1];
            LogicLayer.BusinessObjects.Student student = StudentManager.GetByRoll(Roll);

            LogicLayer.BusinessObjects.Department dept = DepartmentManager.GetById(student.Program.DeptID);
            string departmentName = dept.Name;
            string programName = student.Program.DetailName;
            string fullName = student.Name;
            string fatherName = student.BasicInfo.FatherName;
            string DOB = (student.BasicInfo.DOB != null) ? student.BasicInfo.DOB.Value.ToString("dd-MMM-yyyy") : null;
            //if (student.Block.IsResultBlock == true)
            //{
            //    lblMessage.Text = "Please CLEAR YOUR DUES to see Result, CONTACT ACCOUNTS DEPARTMENT.";
            //    return;
            //}

            if (student != null)
            {
                if (student.Block != null)
                {
                    if (student.Block.IsResultBlock == true)
                    {
                        ShowMessage("Result is blocked because of DUES. (PAY DUES before deadline to avoid LATE FINE & Result/Registration/AdmitCard BLOCK)");
                        return;
                    }
                }
                string processResult = StudentACUDetailManager.Calculate_GpaCgpa(0, 0, 0, Roll);
                List<rStudentGradePreviousNew> previousList = ExamMarkManager.GetStudentGradeReportPreviousNew(Roll, acaCalId);
                if (previousList.Count == 0)
                {
                    rStudentGradePreviousNew a = new rStudentGradePreviousNew();
                    a.CAttempted = 0;
                    a.CEarned = 0;
                    a.CGPA = 0;
                    a.GPATotal = 0;
                    a.PSecuredTotal = 0;
                    a.TranscriptCGPA = 0;
                    a.TotalCEarned = 0;
                    a.TotalCAttemped = 0;
                    a.TotalGradePoint = 0;
                    a.PreviousTotalGradePoint = 0;
                    previousList.Add(a);
                }
                List<rStudentGradeCurrent> currentList = ExamMarkManager.GetStudentGradeReportCurrentNew(Roll, acaCalId);
                List<rStudentGradeDetail> gradeDetailList = StudentGradeDetailManager.GetAllGrade(Roll);

                if (currentList.Count != 0)
                {
                    ReportParameter p1 = new ReportParameter("Roll", Roll);
                    ReportParameter p2 = new ReportParameter("FullName", fullName);
                    ReportParameter p3 = new ReportParameter("FatherName", fatherName);
                    ReportParameter p4 = new ReportParameter("DepartmentName", departmentName);
                    ReportParameter p5 = new ReportParameter("ProgramName", programName);
                    // ReportParameter p6 = new ReportParameter("DOB", DOB);
                    ReportParameter p6 = new ReportParameter("Session", session);

                    StudentGradeReport.LocalReport.ReportPath = Server.MapPath("~/miu/student/report/RptStudentWiseGradeBySessionNew.rdlc");
                    this.StudentGradeReport.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6 });

                    ReportDataSource rds = new ReportDataSource("StudentCurrentGrade", currentList);
                    ReportDataSource rds2 = new ReportDataSource("StudentPreviousGrade", previousList);
                    ReportDataSource rds3 = new ReportDataSource("Grade", gradeDetailList);

                    StudentGradeReport.LocalReport.DataSources.Clear();
                    StudentGradeReport.LocalReport.DataSources.Add(rds);
                    StudentGradeReport.LocalReport.DataSources.Add(rds2);
                    StudentGradeReport.LocalReport.DataSources.Add(rds3);
                }
                else
                {
                    ShowMessage("Student Have To Complete At Least One Exam For GPA");
                }
            }
        }

        //View Report In PDF For Print
        protected void buttonPrint_Click(object sender, EventArgs e)
        {
            int acaCalId = Convert.ToInt32(ddlSession.SelectedValue);
            if (acaCalId != 0)
            {
                PrintGradeReport();
            }
            else
            {
                ShowMessage("Please Select a Semester");
            }
        }
        private void PrintGradeReport()
        {
            StudentGradeReport.LocalReport.DataSources.Clear();
            StudentGradeReport.LocalReport.Refresh();

            int acaCalId = Convert.ToInt32(ddlSession.SelectedValue);


            LogicLayer.BusinessObjects.Student student = StudentManager.GetByRoll(Roll);
           
            if (student != null)
            {
                if (student.Block != null)
                {
                    if (student.Block.IsResultBlock == true)
                    {
                        ShowMessage("Please CLEAR YOUR DUES to see Result, CONTACT ACCOUNTS DEPARTMENT.");
                        return;
                    }
                }

                List<rStudentGradePrevious> previousList = ExamMarkManager.GetStudentGradeReportPrevious(Roll, acaCalId);
                if (previousList.Count == 0)
                {
                    rStudentGradePrevious a = new rStudentGradePrevious();
                    a.CAttempted = 0;
                    a.CEarned = 0;
                    a.CGPA = 0;
                    a.GPATotal = 0;
                    a.PSecuredTotal = 0;
                    a.TranscriptCGPA = 0;
                    previousList.Add(a);
                }
                List<rStudentGradeCurrent> currentList = ExamMarkManager.GetStudentGradeReportCurrent(Roll, acaCalId);
                List<rStudentGradeDetail> gradeDetailList = StudentGradeDetailManager.GetAllGrade(Roll);

                if (currentList.Count != 0)
                {
                    StudentGradeReport.LocalReport.ReportPath = Server.MapPath("~/miu/student/report/RptStudentGradeBySession.rdlc");
                    ReportDataSource rds = new ReportDataSource("StudentCurrentGrade", currentList);
                    ReportDataSource rds2 = new ReportDataSource("StudentPreviousGrade", previousList);
                    ReportDataSource rds3 = new ReportDataSource("Grade", gradeDetailList);

                    StudentGradeReport.LocalReport.DataSources.Clear();
                    StudentGradeReport.LocalReport.DataSources.Add(rds);
                    StudentGradeReport.LocalReport.DataSources.Add(rds2);
                    StudentGradeReport.LocalReport.DataSources.Add(rds3);

                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;

                    byte[] bytes = StudentGradeReport.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                    using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportPDF/" + "StudentGradeReport" + ".pdf"), FileMode.Create))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }

                    string path = Server.MapPath("~/Upload/ReportPDF/" + "StudentGradeReport" + ".pdf");

                    WebClient client = new WebClient();   // Open PDF File in Web Browser 

                    Byte[] buffer = client.DownloadData(path);
                    if (buffer != null)
                    {
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-length", buffer.Length.ToString());
                        Response.BinaryWrite(buffer);
                    }
                }
                else
                {
                    ShowMessage("Student Have To Complete At Least One Exam For GPA");
                }
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