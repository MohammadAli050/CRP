using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;
using Microsoft.Reporting.WebForms;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.Module.student.report
{
    public partial class RptStudentGradeCertificateSessionWise : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            int userId = base.BaseCurrentUserObj.Id;

            if (!IsPostBack)
            {
                LoadAffiliatedInstitution();
                txtResultPublishDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtIssuedDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ucProgram.LoadDropdownWithUserAccess(userId);
                StudentGradeReport.Visible = false;
                LoadDegreeName(0);
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int sessionId = Convert.ToInt32(ucSession.selectedValue);
                int isChecked = 0;

                if (ddlStudentList.SelectedItem.Text == "All")
                {
                    foreach (ListItem item in ddlStudentList.Items)
                    {
                        item.Selected = true;
                    }
                    ddlStudentList.Items[0].Selected = true;
                }

                foreach (ListItem item in ddlStudentList.Items)
                {
                    if (item.Selected == true)
                    {
                        isChecked = isChecked + 1;
                    }
                }


                if (sessionId != 0 && isChecked != 0)
                {


                    int count = 0;
                    foreach (ListItem item in ddlStudentList.Items)
                    {
                        if (item.Selected)
                        {
                            count++;
                            LoadGradeCertificate(item.Value, sessionId, isChecked, count);
                        }
                    }

                }
                else
                {

                }

            }
            catch (Exception)
            { }
        }

        private void LoadAffiliatedInstitution()
        {
            try
            {
                List<AffiliatedInstitution> list = AffiliatedInstitutionManager.GetAll();

                ddlInstitution.Items.Clear();
                ddlInstitution.AppendDataBoundItems = true;

                if (list != null)
                {
                    ddlInstitution.Items.Add(new ListItem("-Select-", "0"));
                    ddlInstitution.DataTextField = "Name";
                    ddlInstitution.DataValueField = "Id";

                    ddlInstitution.DataSource = list;
                    ddlInstitution.DataBind();
                }
            }
            catch { }
            finally { }
        }

        private void LoadGradeCertificate(string roll, int sessionId, int isChecked, int count)
        {
            try
            {
                int semesterId = 0;

                string FullName = "";
                string Program = "";
                string Faculty = "";
                string BatchInfo = "";
                string RegistrationNo = "";
                string SessionInfo = "";
                string SemesterInfo = "";
                string TranscriptCGPA = "";
                string AttemptedCredit = "";
                string EarnedCredit = "";
                string Remarks = "";
                string LoginUserName = "";
                string Designation = "";


                string Credit1 = "";
                string Credit2 = "";
                string Credit3 = "";
                string Credit4 = "";
                string Credit5 = "";
                string Credit6 = "";
                string Credit7 = "";
                string Credit8 = "";

                string GPA1 = "";
                string GPA2 = "";
                string GPA3 = "";
                string GPA4 = "";
                string GPA5 = "";
                string GPA6 = "";
                string GPA7 = "";
                string GPA8 = "";

                DateTime ResultPublishDate = DateTime.ParseExact(txtResultPublishDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);
                DateTime ResultIssuedDate = DateTime.ParseExact(txtIssuedDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);

                try
                {
                    int userId = base.BaseCurrentUserObj.Id;
                    UserInPerson uip = UserInPersonManager.GetById((int)userId);

                    LoginUserName = uip.Person.FullName;
                    Designation = uip.Person.Employee.Designation;
                    //if (userId == 35495) // Hard Coded for Lieutenant Colonel Md Zahid Hasan
                    //{
                    //    LoginUserName = "Md Zahid Hasan" + "_" + "Lieutenant Colonel";
                    //}

                    if (userId == 42928)// Hard Coded for Lieutenant Colonel Mohammad Sharif Hossain (Rifa_20_11_2022(Email Reference))
                    {
                        LoginUserName = "Mohammad Sharif Hossain" + "_" + "Lieutenant Colonel";
                    }
                }
                catch (Exception ex)
                { }



                if (string.IsNullOrEmpty(roll))
                {
                    lblMessage.Text = "Please select any student's roll.";
                    return;
                }
                Student student = StudentManager.GetByRoll(roll);

                if (student != null)
                {

                    List<GradeDetails> gradeDetails = GradeDetailsManager.GetAll();
                    List<StudentGrade> StudentGradeList = ExamMarkManager.GetGradeReportByRollSession(roll, sessionId);
                    List<rCreditGPA> CreditGPAList = ExamMarkManager.GetGradeReportCreditGPAByRoll(student.ProgramID, student.BatchId, student.Roll).ToList();

                    List<rStudentGradeCertificateInfo> studentGeneralInfo = StudentManager.GetStudentGradeCertificateInfoByRoll(student.Roll, sessionId, 0);

                    if (studentGeneralInfo != null && studentGeneralInfo.Count > 0)
                    {
                        FullName = studentGeneralInfo[0].FullName;
                        Program = studentGeneralInfo[0].Program;
                        Faculty = studentGeneralInfo[0].Faculty;
                        //BatchInfo = studentGeneralInfo[0].BatchInfo;
                        BatchInfo = student.Batch == null ? "" : student.Batch.BatchNO.ToString();
                        if (BatchInfo.Length == 1)
                            BatchInfo = "0" + BatchInfo;
                        RegistrationNo = studentGeneralInfo[0].RegistrationNo;
                        SessionInfo = studentGeneralInfo[0].SessionInfo;
                        TranscriptCGPA = studentGeneralInfo[0].TranscriptCGPA.ToString();
                        AttemptedCredit = studentGeneralInfo[0].AttemptedCredit.ToString();
                        EarnedCredit = studentGeneralInfo[0].EarnedCredit.ToString();
                        SemesterInfo = "Academic Session : " + ucSession.selectedText + " " + studentGeneralInfo[0].SemesterInfo;
                        Remarks = studentGeneralInfo[0].Remarks;

                        semesterId = 8;//studentGeneralInfo[0].SemesterId;

                    }

                    if (student.ProgramID == 16)
                    {
                        Program = ddlMISSDegreeName.SelectedItem.Text;
                    }
                    else if (student.ProgramID == 20)
                    {
                        Program = ddlMICTDegreeName.SelectedItem.Text;
                    }

                    if (CreditGPAList != null && CreditGPAList.Count > 0)
                    {
                        List<rCreditGPA> list;
                        list = CreditGPAList.Where(x => x.Sl == 1).ToList();
                        if (list[0].Sl > semesterId || (list[0].RValue.ToString() == "0" && list[1].RValue.ToString() == "0"))
                        {
                            Credit1 = "-";
                            GPA1 = "-";
                        }
                        else
                        {
                            Credit1 = list[0].RValue.ToString();
                            GPA1 = list[1].RValue.ToString();
                        }

                        list = CreditGPAList.Where(x => x.Sl == 2).ToList();
                        if (list[0].Sl > semesterId || (list[0].RValue.ToString() == "0" && list[1].RValue.ToString() == "0"))
                        {
                            Credit2 = "-";
                            GPA2 = "-";
                        }
                        else
                        {
                            Credit2 = list[0].RValue.ToString();
                            GPA2 = list[1].RValue.ToString();
                        }


                        list = CreditGPAList.Where(x => x.Sl == 3).ToList();
                        if (list[0].Sl > semesterId || (list[0].RValue.ToString() == "0" && list[1].RValue.ToString() == "0"))
                        {
                            Credit3 = "-";
                            GPA3 = "-";
                        }
                        else
                        {
                            Credit3 = list[0].RValue.ToString();
                            GPA3 = list[1].RValue.ToString();
                        }

                        list = CreditGPAList.Where(x => x.Sl == 4).ToList();
                        if (list[0].Sl > semesterId || (list[0].RValue.ToString() == "0" && list[1].RValue.ToString() == "0"))
                        {
                            Credit4 = "-";
                            GPA4 = "-";
                        }
                        else
                        {
                            Credit4 = list[0].RValue.ToString();
                            GPA4 = list[1].RValue.ToString();
                        }

                        list = CreditGPAList.Where(x => x.Sl == 5).ToList();
                        if (list[0].Sl > semesterId || (list[0].RValue.ToString() == "0" && list[1].RValue.ToString() == "0"))
                        {
                            Credit5 = "-";
                            GPA5 = "-";
                        }
                        else
                        {
                            Credit5 = list[0].RValue.ToString();
                            GPA5 = list[1].RValue.ToString();
                        }

                        list = CreditGPAList.Where(x => x.Sl == 6).ToList();
                        if (list[0].Sl > semesterId || (list[0].RValue.ToString() == "0" && list[1].RValue.ToString() == "0"))
                        {
                            Credit6 = "-";
                            GPA6 = "-";
                        }
                        else
                        {
                            Credit6 = list[0].RValue.ToString();
                            GPA6 = list[1].RValue.ToString();
                        }


                        list = CreditGPAList.Where(x => x.Sl == 7).ToList();
                        if (list[0].Sl > semesterId || (list[0].RValue.ToString() == "0" && list[1].RValue.ToString() == "0"))
                        {
                            Credit7 = "-";
                            GPA7 = "-";
                        }
                        else
                        {
                            Credit7 = list[0].RValue.ToString();
                            GPA7 = list[1].RValue.ToString();
                        }


                        list = CreditGPAList.Where(x => x.Sl == 8).ToList();
                        if (list[0].Sl > semesterId || (list[0].RValue.ToString() == "0" && list[1].RValue.ToString() == "0"))
                        {
                            Credit8 = "-";
                            GPA8 = "-";
                        }
                        else
                        {
                            Credit8 = list[0].RValue.ToString();
                            GPA8 = list[1].RValue.ToString();
                        }


                    }

                    if (StudentGradeList.Count != 0)
                    {
                        string CalenderMasterType = CalenderUnitMasterManager.GetById(student.Program.CalenderUnitMasterID).Name;

                        List<ReportParameter> param1 = new List<ReportParameter>();
                        param1.Add(new ReportParameter("ProgramId", student.ProgramID.ToString()));
                        param1.Add(new ReportParameter("ResultPublishDate", ResultPublishDate.ToString("dd-MMM-yyyy")));
                        param1.Add(new ReportParameter("ResultIssuedDate", ResultIssuedDate.ToString("dd-MMM-yyyy")));
                        param1.Add(new ReportParameter("Name", FullName));
                        param1.Add(new ReportParameter("Roll", student.Roll));
                        param1.Add(new ReportParameter("Faculty", Faculty));
                        param1.Add(new ReportParameter("CalenderMasterType", CalenderMasterType));
                        param1.Add(new ReportParameter("Program", Program));
                        param1.Add(new ReportParameter("BatchInfo", BatchInfo));
                        param1.Add(new ReportParameter("SemesterInfo", SemesterInfo));
                        param1.Add(new ReportParameter("RegNo", RegistrationNo));
                        param1.Add(new ReportParameter("SessionNo", SessionInfo));
                        param1.Add(new ReportParameter("CGPA", TranscriptCGPA));
                        param1.Add(new ReportParameter("Remarks", Remarks));
                        param1.Add(new ReportParameter("LoginUserName", LoginUserName));
                        param1.Add(new ReportParameter("DesignationOfUser", Designation));
                        param1.Add(new ReportParameter("SemesterNo", "0"));

                        param1.Add(new ReportParameter("Credit1", Credit1));
                        param1.Add(new ReportParameter("Credit2", Credit2));
                        param1.Add(new ReportParameter("Credit3", Credit3));
                        param1.Add(new ReportParameter("Credit4", Credit4));


                        param1.Add(new ReportParameter("GPA1", GPA1));
                        param1.Add(new ReportParameter("GPA2", GPA2));
                        param1.Add(new ReportParameter("GPA3", GPA3));
                        param1.Add(new ReportParameter("GPA4", GPA4));


                        if (student.ProgramID != 1 && student.ProgramID != 2)
                        {
                            param1.Add(new ReportParameter("Credit5", Credit5));
                            param1.Add(new ReportParameter("Credit6", Credit6));
                            param1.Add(new ReportParameter("GPA5", GPA5));
                            param1.Add(new ReportParameter("GPA6", GPA6));

                            if (student.ProgramID != 7)
                            {
                                param1.Add(new ReportParameter("Credit7", Credit7));
                                param1.Add(new ReportParameter("Credit8", Credit8));
                                param1.Add(new ReportParameter("GPA7", GPA7));
                                param1.Add(new ReportParameter("GPA8", GPA8));
                            }
                        }

                        if (student.ProgramID == 1 || student.ProgramID == 2)
                        {
                            StudentGradeReport.LocalReport.ReportPath = Server.MapPath("~/Module/student/report/RptStudentGradeCertificate3rd.rdlc");
                        }
                        else if (student.ProgramID == 7)
                        {
                            StudentGradeReport.LocalReport.ReportPath = Server.MapPath("~/Module/student/report/RptStudentGradeCertificate6th.rdlc");
                        }
                        else if (student.ProgramID == 1 || student.ProgramID == 20 || student.ProgramID == 21)
                        {
                            StudentGradeReport.LocalReport.ReportPath = Server.MapPath("~/Module/student/report/RptStudentGradeCertificate4th.rdlc");
                        }
                        else
                        {
                            StudentGradeReport.LocalReport.ReportPath = Server.MapPath("~/Module/student/report/RptStudentGradeCertificate8th.rdlc");
                        }


                        this.StudentGradeReport.LocalReport.SetParameters(param1);
                        StudentGradeReport.LocalReport.DisplayName = roll;
                        ReportDataSource rds = new ReportDataSource("GradeReportDataSet", StudentGradeList);
                        ReportDataSource rds2 = new ReportDataSource("GeneralInfoDataSet", studentGeneralInfo);
                        ReportDataSource rds3 = new ReportDataSource("GradeDetailsDataSet", gradeDetails);

                        StudentGradeReport.LocalReport.DataSources.Clear();
                        StudentGradeReport.LocalReport.DataSources.Add(rds);
                        StudentGradeReport.LocalReport.DataSources.Add(rds2);
                        StudentGradeReport.LocalReport.DataSources.Add(rds3);
                        StudentGradeReport.Visible = true;
                        if (isChecked > 1)
                        {
                            AllInOne(roll, isChecked, count);
                        }
                        ShowMessage("");
                    }
                    else
                    {
                        ShowMessage("No Data Found in this Session!");
                        StudentGradeReport.Visible = false;
                    }
                }
                else
                {
                    ShowMessage("No Data Found! Enter Valid StudentID");
                    StudentGradeReport.Visible = false;
                }
            }
            catch (Exception)
            { }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            ucBatch.LoadDropDownList2(Convert.ToInt32(ucProgram.selectedValue));
            ucSession.LoadDropDownListByProgramBatch(programId, 0);
            LoadStudentCheckBoxList(null);
            StudentGradeReport.Visible = false;
            LoadDegreeName(programId);
        }

        private void LoadDegreeName(int programId)
        {
            if (programId == 16)
            {
                lblDegreeName.Visible = true;
                ddlMISSDegreeName.Visible = true;
                ddlMICTDegreeName.Visible = false;
            }
            else if (programId == 20)
            {
                lblDegreeName.Visible = true;
                ddlMICTDegreeName.Visible = true;
                ddlMISSDegreeName.Visible = false;
            }
            else
            {
                lblDegreeName.Visible = false;
                ddlMICTDegreeName.Visible = false;
                ddlMISSDegreeName.Visible = false;
            }
        }

        private void ShowMessage(string msg)
        {
            lblMessage.Text = msg;
            lblMessage.ForeColor = Color.Red;
        }

        protected void ucBatch_BatchSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);
                ucSession.LoadDropDownListByProgramBatch(programId, batchId);
                LoadStudentCheckBoxList(null);
                StudentGradeReport.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void LoadStudentCheckBoxList(List<StudentRollOnly> list)
        {
            try
            {
                ddlStudentList.Items.Clear();
                ddlStudentList.AppendDataBoundItems = true;
                if (list != null)
                {
                    ddlStudentList.Items.Add(new ListItem("All", ""));
                    ddlStudentList.DataTextField = "Roll";
                    ddlStudentList.DataValueField = "Roll";
                    ddlStudentList.DataSource = list;
                    ddlStudentList.DataBind();
                }
                else
                {
                    ddlStudentList.DataSource = null;
                    ddlStudentList.DataBind();
                }
            }
            catch (Exception)
            { }
        }

        private void AllInOne(string roll, int ischecked, int count)
        {
            try
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;

                byte[] bytes = StudentGradeReport.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                StudentGradeReport.LocalReport.Refresh();

                using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportPDF/" + "GradeCertificate" + count + ".pdf"), FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }

                if (count > 1)
                {
                    MargePDF(roll, count);
                }

                if (ischecked == count)
                {
                    System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;

                    Response.ClearHeaders();
                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + "GradeCertificate" + count + ".pdf");
                    Response.TransmitFile(Server.MapPath("~/Upload/ReportPDF/" + "GradeCertificate" + count + ".pdf"));
                    Response.Flush();
                    Response.Close();
                    //Response.End();
                    File.Delete(Server.MapPath("~/Upload/ReportPDF/" + "GradeCertificate" + count + ".pdf"));
                }
                //ShowMessage("Save And Downloaded Successfully", Color.Green);


            }
            catch (Exception)
            { }
        }

        public void MargePDF(string roll, int count)
        {

            using (PdfDocument one = PdfReader.Open(Server.MapPath("~/Upload/ReportPDF/" + "GradeCertificate" + count + ".pdf"), PdfDocumentOpenMode.Import))
            using (PdfDocument two = PdfReader.Open(Server.MapPath("~/Upload/ReportPDF/" + "GradeCertificate" + (count - 1) + ".pdf"), PdfDocumentOpenMode.Import))
            using (PdfDocument outPdf = new PdfDocument())
            {
                CopyPages(one, outPdf);
                CopyPages(two, outPdf);

                File.Delete(Server.MapPath("~/Upload/ReportPDF/" + "GradeCertificate" + count + ".pdf"));
                File.Delete(Server.MapPath("~/Upload/ReportPDF/" + "GradeCertificate" + (count - 1) + ".pdf"));

                outPdf.Save(Server.MapPath("~/Upload/ReportPDF/" + "GradeCertificate" + count + ".pdf"));
            }
        }

        private void CopyPages(PdfDocument from, PdfDocument to)
        {
            for (int i = 0; i < from.PageCount; i++)
            {
                to.AddPage(from.Pages[i]);
            }
        }

        protected void ddlStudentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlStudentList.SelectedItem.Text == "All")
                {
                    foreach (ListItem item in ddlStudentList.Items)
                    {
                        item.Selected = true;
                    }
                    ddlStudentList.Items[0].Selected = true;
                }
                StudentGradeReport.Visible = false;
            }
            catch (Exception)
            { }


        }

        protected void ucSession_SessionSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);
                int sessionId = Convert.ToInt32(ucSession.selectedValue);
                int institutionId = Convert.ToInt32(ddlInstitution.SelectedValue);

                List<StudentRollOnly> stdList = StudentManager.GetStudentListRollByProgramBatchSessionInstitution(sessionId, programId, batchId, institutionId);
                LoadStudentCheckBoxList(stdList);
                StudentGradeReport.Visible = false;

            }
            catch (Exception)
            { }
        }

        protected void ddlInstitution_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);
                int sessionId = Convert.ToInt32(ucSession.selectedValue);
                int institutionId = Convert.ToInt32(ddlInstitution.SelectedValue);

                List<StudentRollOnly> stdList = StudentManager.GetStudentListRollByProgramBatchSessionInstitution(sessionId, programId, batchId, institutionId);
                LoadStudentCheckBoxList(stdList);
                StudentGradeReport.Visible = false;

            }
            catch (Exception)
            { }
        }


    }
}