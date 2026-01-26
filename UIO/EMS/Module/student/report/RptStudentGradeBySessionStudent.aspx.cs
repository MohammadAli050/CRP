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
    public partial class RptStudentGradeBySessionStudent : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            pnlMessage.Visible = false;

            if (!IsPostBack)
            {
                if (userObj.RoleID == 9)
                {
                    User user = UserManager.GetById(userObj.Id);
                    Student student = StudentManager.GetBypersonID(user.Person.PersonID);

                    txtStudentId.Text = student.Roll;
                    txtStudentId.ReadOnly = true;
                }

                FillAcademicCalenderCombo();
            }
        }

        //protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int programId = Convert.ToInt32(ucProgram.selectedValue);
        //    FillBatchCombo(programId);
        //}

        //private void FillBatchCombo(int programId)
        //{
        //    List<rBatchListByProgram> list = BatchManager.GetBatchListByProgram(programId);

        //    ddlBatch.Items.Clear();
        //    ddlBatch.AppendDataBoundItems = true;

        //    if (list != null)
        //    {
        //        ddlBatch.Items.Add(new ListItem("-Select-", "0"));
        //        ddlBatch.DataTextField = "BatchNO";
        //        ddlBatch.DataValueField = "BatchId";


        //        ddlBatch.DataSource = list;
        //        ddlBatch.DataBind();
        //    }
        //}

        //protected void OnBatchSelected_IndexChanged(object sender, EventArgs e)
        //{
        //    int programId = Convert.ToInt32(ucProgram.selectedValue);
        //    int batchId = Convert.ToInt32(ddlBatch.SelectedValue);
        //    LoadStudentListByProgramAndBatch(programId, batchId);
        //}

        //private void LoadStudentListByProgramAndBatch(int programId, int acaCalId)
        //{
        //    List<RunningStudent> list = StudentManager.GetStudentListByProgramAndBatch(programId, acaCalId);

        //    ddlStudentRoll.Items.Clear();
        //    ddlStudentRoll.AppendDataBoundItems = true;

        //    if (ddlStudentRoll != null)
        //    {
        //        ddlStudentRoll.Items.Add(new ListItem("All", "0"));
        //        ddlStudentRoll.DataTextField = "Roll";
        //        ddlStudentRoll.DataValueField = "Roll";


        //        ddlStudentRoll.DataSource = list;
        //        ddlStudentRoll.DataBind();
        //    }
        //}

        //protected void OnRollSelected_IndexChanged(object sender, EventArgs e)
        //{
        //    string roll = ddlStudentRoll.SelectedItem.Text;
        //    FillAcademicCalenderCombo(roll);
        //}

        private void FillAcademicCalenderCombo()
        {
            string roll = Convert.ToString(txtStudentId.Text);
            try
            {
                ddlSession.Items.Clear();
                List<rAcaCalSessionListByProgram> list = AcademicCalenderManager.GetAcaCalSessionListCompleted(roll);

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

        //View Report On Webpage
        protected void buttonView_Click(object sender, EventArgs e)
        {
            StudentGradeReport.LocalReport.DataSources.Clear();
            StudentGradeReport.LocalReport.Refresh();

            string roll = Convert.ToString(txtStudentId.Text);
            int acaCalId = Convert.ToInt32(ddlSession.SelectedValue);

            if (string.IsNullOrEmpty(txtStudentId.Text))
            {
                lblMessage.Text = "Please select any student's roll.";
                return;
            }

            Student student = StudentManager.GetByRoll(roll);
            if (student.Block.IsResultBlock == true)
            {
                lblMessage.Text = "Please CLEAR YOUR DUES to see Result, CONTACT ACCOUNTS DEPARTMENT.";
                return;
            }

            List<rStudentGradePrevious> previousList = ExamMarkManager.GetStudentGradeReportPrevious(roll, acaCalId);
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
            List<rStudentGradeCurrent> currentList = ExamMarkManager.GetStudentGradeReportCurrent(roll, acaCalId);
            List<rStudentGradeDetail> gradeDetailList = StudentGradeDetailManager.GetAllGrade(roll);

            if (currentList.Count != 0)
            {
                StudentGradeReport.LocalReport.ReportPath = Server.MapPath("~/miu/student/report/RptStudentGradeBySessionStudent.rdlc");
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

        //View Report In PDF For Print
        protected void buttonPrint_Click(object sender, EventArgs e)
        {
            StudentGradeReport.LocalReport.DataSources.Clear();
            StudentGradeReport.LocalReport.Refresh();

            string roll = Convert.ToString(txtStudentId.Text);
            int acaCalId = Convert.ToInt32(ddlSession.SelectedValue);

            if (string.IsNullOrEmpty(txtStudentId.Text))
            {
                lblMessage.Text = "Please select any student's roll.";
                return;
            }

            Student student = StudentManager.GetByRoll(roll);
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
                        lblMessage.Text = "Please CLEAR YOUR DUES to see Result, CONTACT ACCOUNTS DEPARTMENT.";
                        return;
                    }
                }

                List<rStudentGradePrevious> previousList = ExamMarkManager.GetStudentGradeReportPrevious(roll, acaCalId);
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
                List<rStudentGradeCurrent> currentList = ExamMarkManager.GetStudentGradeReportCurrent(roll, acaCalId);
                List<rStudentGradeDetail> gradeDetailList = StudentGradeDetailManager.GetAllGrade(roll);

                if (currentList.Count != 0)
                {
                    StudentGradeReport.LocalReport.ReportPath = Server.MapPath("~/miu/student/report/RptStudentGradeBySessionStudent.rdlc");
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