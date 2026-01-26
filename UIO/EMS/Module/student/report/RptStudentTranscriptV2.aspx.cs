using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using CommonUtility;
using Microsoft.Reporting.WebForms;
using LogicLayer.BusinessObjects.RO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.IO;

namespace EMS.Module.student.report
{
    public partial class RptStudentTranscriptV2 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            int userId = base.BaseCurrentUserObj.Id;

            if (!IsPostBack)
            {
                txtResultPublishDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtIssuedDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ClearAll();
            }
        }

        private void LoadDegreeName(int programId)
        {
            if (programId == 16)
            {
                lblDegreeName.Visible = true;
                ddlMISSDegreeName.Visible = true;
                ddlMICTDegreeName.Visible = false;
                ddlMICEDegreeName.Visible = false;
                ddlMESDegreeName.Visible = false;
                ddlMESMDegreeName.Visible = false;
            }
            else if (programId == 20)
            {
                lblDegreeName.Visible = true;
                ddlMISSDegreeName.Visible = false;
                ddlMICTDegreeName.Visible = true;
                ddlMICEDegreeName.Visible = false;
                ddlMESDegreeName.Visible = false;
                ddlMESMDegreeName.Visible = false;
            }
            else if (programId == 30) // MICE
            {
                lblDegreeName.Visible = true;
                ddlMICTDegreeName.Visible = false;
                ddlMISSDegreeName.Visible = false;
                ddlMICEDegreeName.Visible = true;
                ddlMESDegreeName.Visible = false;
                ddlMESMDegreeName.Visible = false;
            }
            else if (programId == 27)
            {
                lblDegreeName.Visible = true;
                ddlMICTDegreeName.Visible = false;
                ddlMISSDegreeName.Visible = false;
                ddlMICEDegreeName.Visible = false;
                ddlMESDegreeName.Visible = true;
                ddlMESMDegreeName.Visible = false;

            }
            else if (programId == 39)//MESM
            {
                lblDegreeName.Visible = true;
                ddlMICTDegreeName.Visible = false;
                ddlMISSDegreeName.Visible = false;
                ddlMICEDegreeName.Visible = false;
                ddlMESDegreeName.Visible = false;
                ddlMESMDegreeName.Visible = true;

            }
            else
            {
                lblDegreeName.Visible = false;
                ddlMICTDegreeName.Visible = false;
                ddlMISSDegreeName.Visible = false;
                ddlMICEDegreeName.Visible = false;
                ddlMESDegreeName.Visible = false;
                ddlMESMDegreeName.Visible = false;
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string roll = lblStudentId.Text.Trim();

                StudentGradeReport.LocalReport.DataSources.Clear();

                if (roll != "................." && !string.IsNullOrEmpty(roll) && !string.IsNullOrEmpty(lblName.Text))
                {
                    LoadTranscript(roll);
                }
                else
                {
                    ShowMessage("Please Load Student Info First!");
                }

            }
            catch (Exception)
            { }
        }

        private void LoadTranscript(string roll)
        {
            try
            {

                string ProgramName = "";
                string LoginUserName = "";
                string Designation = "";
                decimal reqCredit = 0;
                string remarks = "";
                string ResultPublishDate = "";
                string PreparedDate = "";
                remarks = txtRemarks.Text.Trim();
                try
                {
                    if (!string.IsNullOrEmpty(txtRequiredCredit.Text))
                        reqCredit = Convert.ToDecimal(txtRequiredCredit.Text.Trim());
                }
                catch (Exception ex)
                { }


                try
                {
                    if (!string.IsNullOrEmpty(txtIssuedDate.Text))
                        PreparedDate = string.IsNullOrEmpty(txtIssuedDate.Text) == true ? "" : DateTime.ParseExact(txtIssuedDate.Text.Replace("/", string.Empty), "ddMMyyyy", null).ToString("dd MMMM, yyyy");
                }
                catch (Exception ex)
                { }


                try
                {
                    int userId = base.BaseCurrentUserObj.Id;
                    UserInPerson uip = UserInPersonManager.GetById((int)userId);

                    LoginUserName = uip.Person.FullName;
                    Designation = uip.Person.Employee != null ? uip.Person.Employee.Designation : "";
                }
                catch (Exception ex)
                { }


                Student student = StudentManager.GetByRoll(roll);

                if (student != null)
                {

                    StudentTranscriptInfo studentTranscriptInfo = StudentTranscriptInfoManager.GetByStudentId(student.StudentID);

                    try
                    {
                        if (studentTranscriptInfo != null && studentTranscriptInfo.PublicationDate != null)
                        {
                            DateTime prepdate = (DateTime)studentTranscriptInfo.PublicationDate;
                            ResultPublishDate = prepdate.ToString("dd MMMM, yyyy");
                        }
                    }
                    catch (Exception ex)
                    { }


                    List<GradeDetails> gradeDetails = GradeDetailsManager.GetAll();

                    List<rStudentGradeCertificateInfo> studentGeneralInfo = StudentManager.GetStudentGradeCertificateInfoByRoll(student.Roll, 0, 0);
                    List<rStudentTranscriptResult> studentTranscriptResult = StudentManager.GetTranscriptResultByRoll(student.Roll);

                    if (studentGeneralInfo != null && studentGeneralInfo.Count > 0)
                    {
                        ProgramName = studentGeneralInfo[0].Program;

                        if (reqCredit > 0)
                        {
                            studentGeneralInfo[0].RequiredCredit = reqCredit;
                        }

                    }


                    if (studentTranscriptResult != null && studentTranscriptResult.Count != 0)
                    {
                        string Duration = "", ExaminationYear = "";
                        Duration = studentGeneralInfo[0].Duration.ToString();
                        if (!string.IsNullOrEmpty(txtDuration.Text))
                            Duration = txtDuration.Text.Trim().ToString();
                        string CalenderMasterType = CalenderUnitMasterManager.GetById(student.Program.CalenderUnitMasterID).Name;
                        string degreeName = studentGeneralInfo[0].DegreeName;//+ " Examination of " + studentGeneralInfo[0].ExamMonthYear;

                        string DegreeAwarded = "Degree Not Awarded";

                        LogicLayer.BusinessObjects.DegreeCompletion dgCom = DegreeCompletionManager.GetByStudentId(student.StudentID);

                        if (dgCom != null && dgCom.IsDegreeComplete != null && dgCom.IsDegreeComplete == true)
                        {
                            DegreeAwarded = "Degree Awarded";
                        }

                        //if (student.ProgramID == 16)
                        //{
                        //    degreeName = ddlMISSDegreeName.SelectedItem.Text;
                        //}
                        //else if (student.ProgramID == 20)
                        //{
                        //    degreeName = ddlMICTDegreeName.SelectedItem.Text;
                        //}
                        //else if (student.ProgramID == 30)
                        //{
                        //    degreeName = ddlMICEDegreeName.SelectedItem.Text;
                        //}
                        //else if (student.ProgramID == 27)
                        //{
                        //    degreeName = ddlMESDegreeName.SelectedItem.Text;
                        //}
                        //else if (student.ProgramID == 39)
                        //{
                        //    degreeName = ddlMESMDegreeName.SelectedItem.Text;
                        //}

                        #region Exam Year
                        var Info = StudentTranscriptInfoManager.GetByStudentId(student.StudentID);
                        if (Info != null)
                            ExaminationYear = Info.ExaminationMonth;
                        #endregion

                        List<ReportParameter> param1 = new List<ReportParameter>();
                        param1.Add(new ReportParameter("ProgramName", student.ProgramID.ToString()));
                        param1.Add(new ReportParameter("ResultPublishDate", ResultPublishDate));
                        param1.Add(new ReportParameter("PreparedDate", PreparedDate));
                        param1.Add(new ReportParameter("LoginUserName", LoginUserName));
                        param1.Add(new ReportParameter("DesignationOfUser", Designation));
                        param1.Add(new ReportParameter("DegreeName", degreeName));
                        param1.Add(new ReportParameter("Name", studentGeneralInfo[0].FullName));
                        param1.Add(new ReportParameter("Roll", studentGeneralInfo[0].Roll));
                        param1.Add(new ReportParameter("RegNo", studentGeneralInfo[0].RegistrationNo));
                        param1.Add(new ReportParameter("SessionName", studentGeneralInfo[0].SessionInfo));
                        param1.Add(new ReportParameter("CustomRemarks", txtRemarks.Text.Trim()));
                        param1.Add(new ReportParameter("Duration", Duration));
                        param1.Add(new ReportParameter("CalenderName", CalenderMasterType));
                        param1.Add(new ReportParameter("ExaminationYear", ExaminationYear));
                        param1.Add(new ReportParameter("DegreeAwarded", DegreeAwarded));




                        StudentGradeReport.LocalReport.ReportPath = Server.MapPath("~/Module/student/report/RptStudentTranscriptV2.rdlc");


                        this.StudentGradeReport.LocalReport.SetParameters(param1);
                        StudentGradeReport.LocalReport.DisplayName = roll;
                        ReportDataSource rds = new ReportDataSource("GeneralInfoDataSet", studentGeneralInfo);
                        ReportDataSource rds2 = new ReportDataSource("ResultDataSet", studentTranscriptResult);

                        StudentGradeReport.LocalReport.DataSources.Clear();
                        StudentGradeReport.LocalReport.DataSources.Add(rds);
                        StudentGradeReport.LocalReport.DataSources.Add(rds2);
                        StudentGradeReport.Visible = true;

                        ShowMessage("");
                    }
                    else
                    {

                        ShowMessage("No Data Found in this Session!");
                        StudentGradeReport.LocalReport.DataSources.Clear();

                    }
                }
                else
                {
                    ShowMessage("No Data Found! Enter Valid StudentID");
                    StudentGradeReport.LocalReport.DataSources.Clear();
                }
                txtDuration.Text = string.Empty;
            }
            catch (Exception)
            { }
        }

        private void ShowMessage(string msg)
        {
            //lblMessage.Text = msg;
            //lblMessage.ForeColor = Color.Red;
        }

        protected void btnLoadStdInfo_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                string roll = txtRoll.Text.Trim();
                if (!string.IsNullOrEmpty(roll))
                {

                    Student stdudent = StudentManager.GetByRoll(roll);
                    if (stdudent != null)
                    {
                        //LoadDegreeName(stdudent.ProgramID);
                        lblStudentId.Text = stdudent.Roll;
                        lblName.Text = stdudent.BasicInfo.FullName;
                        lblProg.Text = stdudent.Program.ShortName;
                    }
                }
                else
                {
                    ShowMessage("Please Enter Student ID!");
                }
            }
            catch (Exception ex)
            { }
        }

        private void ClearAll()
        {
            lblName.Text = ".................";
            lblProg.Text = ".................";
            lblStudentId.Text = ".................";

            StudentGradeReport.LocalReport.DataSources.Clear();
            //LoadDegreeName(0);
        }
    }
}