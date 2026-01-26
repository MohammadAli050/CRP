using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExamResultApproveByExamController : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        lblMsg.Text = "";
        ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
        _scriptMan.AsyncPostBackTimeout = 36000;
        if (!IsPostBack)
        {
            LoadComboBox();
            pnSummaryReport.Visible = false;
            gvExamMarkApprove.Visible = false;
        }
    }

    protected void LoadComboBox()
    {
        try
        {
            ddlAcademicCalender.Items.Clear();
            ddlAcademicCalender.Items.Add(new ListItem("Select", "0"));
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Select", "0"));

            LoadProgram();
            LoadCalenderType();
        }
        catch { }
        finally { }
    }

    protected void LoadProgram()
    {
        try
        {
            ddlProgram.Items.Clear();
            ddlProgram.AppendDataBoundItems = true;

            List<Program> programList = ProgramManager.GetAll();

            if (programList != null)
            {
                ddlProgram.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                ddlProgram.DataValueField = "ProgramID";
                ddlProgram.DataTextField = "ShortName";
                ddlProgram.DataBind();
            }
        }
        catch { }
        finally { }
    }

    protected void LoadCalenderType()
    {
        try
        {
            ddlCalenderType.Items.Clear();

            List<CalenderUnitMaster> calenderUnitMasterList = CalenderUnitMasterManager.GetAll();

            if (calenderUnitMasterList.Count > 0 && calenderUnitMasterList != null)
            {
                ddlCalenderType.DataSource = calenderUnitMasterList;
                ddlCalenderType.DataValueField = "CalenderUnitMasterID";
                ddlCalenderType.DataTextField = "Name";
                ddlCalenderType.DataBind();
            }
        }
        catch { }
        finally
        {
            CalenderType_Changed(null, null);
        }
    }

    protected void LoadAcademicCalender(int calenderTypeId)
    {
        try
        {
            ddlAcademicCalender.Items.Clear();
            ddlAcademicCalender.Items.Add(new ListItem("Select", "0"));
            ddlAcademicCalender.AppendDataBoundItems = true;

            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll(calenderTypeId);

            if (academicCalenderList.Count > 0 && academicCalenderList != null)
            {
                foreach (AcademicCalender academicCalender in academicCalenderList)
                    ddlAcademicCalender.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));

                academicCalenderList = academicCalenderList.Where(x => x.IsCurrent == true).ToList();
                ddlAcademicCalender.SelectedValue = academicCalenderList[0].AcademicCalenderID.ToString();
            }
        }
        catch { }
        finally
        {
            AcademicCalender_Changed(null, null);
        }
    }

    protected void LoadCourse(int acaCalId, int programId)
    {
        try
        {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("All", "0_0"));
            ddlCourse.AppendDataBoundItems = true;

            List<Course> courseList = CourseManager.GetAllByAcaCalIdProgramId(acaCalId, programId);
            if (courseList.Count > 0 && courseList != null)
            {
                foreach (Course course in courseList)
                    ddlCourse.Items.Add(new ListItem(course.FormalCode + ": " + course.Title, course.CourseID + "_" + course.VersionID));
            }
        }
        catch { }
        finally { }
    }

    #endregion

    #region Event

    protected void CalenderType_Changed(Object sender, EventArgs e)
    {
        try
        {
            int calenderTypeId = Convert.ToInt32(ddlCalenderType.SelectedValue);
            LoadAcademicCalender(calenderTypeId);
        }
        catch { }
        finally
        {
            gvExamMarkApprove.Visible = false;
            pnSummaryReport.Visible = false;

            gvExamMarkApprove.DataSource = null;
            gvExamMarkApprove.DataBind();
        }
    }

    protected void AcademicCalender_Changed(Object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);
            LoadCourse(acaCalId, programId);
        }
        catch { }
        finally
        {
            gvExamMarkApprove.Visible = false;
            pnSummaryReport.Visible = false;

            gvExamMarkApprove.DataSource = null;
            gvExamMarkApprove.DataBind();
        }
    }

    protected void Program_Changed(Object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);
            LoadCourse(acaCalId, programId);
        }
        catch { }
        finally
        {
            gvExamMarkApprove.Visible = false;
            pnSummaryReport.Visible = false;

            gvExamMarkApprove.DataSource = null;
            gvExamMarkApprove.DataBind();
        }
    }

    protected void Course_Changed(Object sender, EventArgs e)
    {
        try
        {
            gvExamMarkApprove.Visible = false;
            pnSummaryReport.Visible = false;

            gvExamMarkApprove.DataSource = null;
            gvExamMarkApprove.DataBind();
        }
        catch { }
        finally { }
    }

    protected void gvExamMarkApprove_SelectedIndexChanged(Object sender, EventArgs e)
    {
        try
        {
           
        }
        catch { }
        finally { }
    }

    protected void Load_Click(Object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);
            string[] courseCode = ddlCourse.SelectedValue.Split('_');
            int courseId = Convert.ToInt32(courseCode[0]);
            int versionId = Convert.ToInt32(courseCode[1]);

            List<ExamMarkApproveDTO> examMarkApproveDTOList = ExamMarkManager.GetAllAcaCalIdProgramIdCourseIdVersionId(acaCalId, programId, courseId, versionId);
            if (examMarkApproveDTOList.Count > 0 && examMarkApproveDTOList != null)
            {
                gvExamMarkApprove.Visible = true;

                gvExamMarkApprove.DataSource = examMarkApproveDTOList;
                gvExamMarkApprove.DataBind();
            }
            else
            {
                gvExamMarkApprove.Visible = true;

                gvExamMarkApprove.DataSource = null;
                gvExamMarkApprove.DataBind();
            }
        }
        catch { }
        finally
        {
            pnSummaryReport.Visible = true;

            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);

            string[] summary = ExamMarkManager.GetShortSummaryReport(acaCalId, programId).Split('-');

            lblTotalCourse.Text = summary[0];
            lblMarkSubmit.Text = summary[1];
            lblFinalSubmit.Text = summary[2];
            lblApproed.Text = summary[3];            
        }
    }

    protected void lbLoadResult_Click(Object sender, EventArgs e)
    {
        try
        {
            LinkButton linkButton = new LinkButton();
            linkButton = (LinkButton)sender;
            string[] commandArgs = linkButton.CommandArgument.ToString().Split(new char[] { ',' });
            int acaCalSectionId = Convert.ToInt32(commandArgs[0]);
            string sectionName = commandArgs[1].ToString();

            List<ValueSet> valueSetList = ValueSetManager.GetAll().Where(x => x.ValueSetName.Contains("ExamMarkStatus")).ToList();
            List<Value> valueList = ValueManager.GetAll().Where(x => x.ValueSetID == valueSetList[0].ValueSetID && x.ValueName.Contains("Absent")).ToList();
            List<Value> valueList2 = ValueManager.GetAll().Where(x => x.ValueSetID == valueSetList[0].ValueSetID && x.ValueName.Contains("Reported")).ToList();


            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            int acaCalSecId = acaCalSectionId;

            if (acaCalId == 0 || acaCalSecId == 0)
            {
                lblMsg.Text = "Please Select Session And Course Name";
                return;
            }

            DataTable table = new DataTable();
            table.Columns.Add("Student Roll", typeof(string));


            AcademicCalenderSection acaCalSec = AcademicCalenderSectionManager.GetById(acaCalSecId);
            ExamTemplate newExamTemplate = ExamTemplateManager.GetById(acaCalSec.BasicExamTemplateId);
            if (newExamTemplate.TemplateName != "Grade Only")
            {
                if (acaCalSec != null)
                {
                    List<ExamTemplateItem> examTemplateItemList = TemplateGroupManager.GetAllByTemplateId(acaCalSec.BasicExamTemplateId);
                    List<ExamMarkDTO> examMarkDTOList = ExamMarkManager.GetAllStudentByAcaCalAcaCalSec(acaCalId, acaCalSecId);

                    if (examMarkDTOList.Count > 0 && examMarkDTOList != null)
                    {
                        List<string> studentList = examMarkDTOList.Select(x => x.Roll).Distinct().ToList();

                        decimal totalMark = 0;
                        int finalExamFlag = 0;
                        string[] columnName = new string[7];
                        for (int i = 0; i < examTemplateItemList.Count; i++)
                        {
                            Exam exam = ExamManager.GetById(examTemplateItemList[i].ExamId);
                            if (exam != null)
                            {
                                if (exam.ExamName.Contains("Final"))
                                    finalExamFlag = exam.ExamId;

                                table.Columns.Add(exam.ExamName + "(" + exam.Marks + ")", typeof(string));
                                totalMark += exam.Marks;
                                columnName[i] = exam.ExamName + "(" + exam.Marks + ")";
                            }
                            else
                                table.Columns.Add("Column" + (i + 1), typeof(string));
                        }
                        string totalColumn = "Total(" + totalMark + ")";
                        table.Columns.Add("Total(" + totalMark + ")", typeof(string));
                        table.Columns.Add("Grade", typeof(string));
                        table.Columns.Add("Point", typeof(string));

                        for (int i = 0; i < studentList.Count; i++)
                        {
                            string studentRoll = studentList[i];
                            DataRow newRow;
                            object[] rowArray = new object[examTemplateItemList.Count + 4];
                            int newRowCounter = 0;
                            rowArray[0] = studentRoll;
                            decimal studentTotalMark = 0;
                            int flag = 0;
                            int gradeMasterId = 0;
                            int examStatus = 0;
                            for (int j = 0; j < examTemplateItemList.Count; j++)
                            {
                                int examId = examTemplateItemList[j].ExamId;
                                string examMark = examMarkDTOList.Where(x => x.Roll == studentRoll && x.ExamId == examId).Select(x => x.Mark).SingleOrDefault();
                                examStatus = examMarkDTOList.Where(x => x.Roll == studentRoll && x.ExamId == examId).Select(x => x.Status).SingleOrDefault();

                                if (examStatus == valueList[0].ValueID)
                                    rowArray[newRowCounter + 1] = "Absent";
                                else if (examStatus == valueList2[0].ValueID)
                                    rowArray[newRowCounter + 1] = "Reported";
                                else
                                    rowArray[newRowCounter + 1] = examMark;

                                newRowCounter = newRowCounter + 1;
                                if (examMark != string.Empty)
                                {
                                    studentTotalMark += Convert.ToDecimal(examMark);
                                }
                                else { studentTotalMark += 0; }

                                if (flag == 0)
                                    gradeMasterId = examMarkDTOList.Where(x => x.Roll == studentRoll && x.ExamId == examId).Select(x => x.GradeMasterId).SingleOrDefault();
                                flag = 1;
                            }

                            studentTotalMark = decimal.Ceiling(studentTotalMark);
                            GradeDetails temp = new GradeDetails();
                            List<GradeDetails> gradeDetailsList = GradeDetailsManager.GetByGradeMasterId(gradeMasterId);
                            foreach (GradeDetails gradeDetails in gradeDetailsList)
                            {
                                if (gradeDetails.MinMarks <= studentTotalMark && gradeDetails.MaxMarks >= studentTotalMark)
                                {
                                    temp = gradeDetails;
                                    break;
                                }
                            }

                            if (examStatus == valueList[0].ValueID)
                            {
                                rowArray[newRowCounter + 1] = Decimal.Ceiling(studentTotalMark); //Math.Round(studentTotalMark, 2);
                                rowArray[newRowCounter + 2] = "I";
                                rowArray[newRowCounter + 3] = string.Empty;
                            }
                            else
                            {
                                rowArray[newRowCounter + 1] = Decimal.Ceiling(studentTotalMark); //Math.Round(studentTotalMark, 2);
                                rowArray[newRowCounter + 2] = temp.Grade;
                                rowArray[newRowCounter + 3] = temp.GradePoint;
                            }

                            newRow = table.NewRow();
                            newRow.ItemArray = rowArray;
                            table.Rows.Add(newRow);
                        }

                        //Check :: Course is Theory or Lab

                        AcademicCalenderSection acaCalSection = AcademicCalenderSectionManager.GetById(acaCalSecId);
                        int programId = acaCalSection.ProgramID;
                        if (acaCalSection != null)
                        {
                            ExamTemplate examTemplate = ExamTemplateManager.GetById(acaCalSection.BasicExamTemplateId);
                            if (examTemplate != null)
                            {
                                if (examTemplate.TemplateName.ToLower().Contains("special theory"))
                                {
                                    //3
                                    List<rExamResultPrintTheorySpecial> examResultPrintTheorySpecialList = ExamMarkManager.GetExamResultPrintTheorySpecial(table, columnName[0], columnName[1], columnName[2], columnName[3], columnName[4], columnName[5], totalColumn);
                                    List<rExamResultCourseAndTeacherInfo> list = ExamMarkManager.GetExamResultCourseAndTeacherInfo(acaCalSecId, acaCalId);

                                    string marks1 = columnName[0];
                                    string marks2 = columnName[1];
                                    string marks3 = columnName[2];
                                    string marks4 = columnName[3];
                                    string marks5 = columnName[4];
                                    string marks6 = columnName[5];
                                    string calendar = ddlCalenderType.SelectedItem.Text;
                                    string section = acaCalSec.SectionName;

                                    ReportParameter p1 = new ReportParameter("Marks1", marks1);
                                    ReportParameter p2 = new ReportParameter("Marks2", marks2);
                                    ReportParameter p3 = new ReportParameter("Marks3", marks3);
                                    ReportParameter p4 = new ReportParameter("Marks4", marks4);
                                    ReportParameter p5 = new ReportParameter("Marks5", marks5);
                                    ReportParameter p6 = new ReportParameter("Marks6", marks6);
                                    ReportParameter p7 = new ReportParameter("Calender", calendar);
                                    ReportParameter p8 = new ReportParameter("Section", section);

                                    ExamResultViewPrint.LocalReport.ReportPath = Server.MapPath("~/miu/result/report/ExamResultPrintTheorySpecial.rdlc");
                                    this.ExamResultViewPrint.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7, p8 });
                                    ReportDataSource rds = new ReportDataSource("RptExamResultPrintTheorySpecialDS", examResultPrintTheorySpecialList);
                                    ReportDataSource rds2 = new ReportDataSource("RptExamResultPrintTheorySpecialCourseTeacherInfoDS", list);
                                    lblMsg.Text = "";
                                    ExamResultViewPrint.LocalReport.DataSources.Clear();
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds);
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds2);


                                }
                                else if (examTemplate.TemplateName.Contains("Theory"))
                                {
                                    //3
                                    List<rExamResultPrintTheory> examResultPrintTheory = ExamMarkManager.GetExamResultPrintTheory(table, columnName[0], columnName[1], columnName[2], totalColumn);
                                    List<rExamResultCourseAndTeacherInfo> list = ExamMarkManager.GetExamResultCourseAndTeacherInfo(acaCalSecId, acaCalId);

                                    string marks1 = columnName[0];
                                    string marks2 = columnName[1];
                                    string marks3 = columnName[2];
                                    string calendar = ddlCalenderType.SelectedItem.Text;
                                    string section = "";

                                    ReportParameter p1 = new ReportParameter("Marks1", marks1);
                                    ReportParameter p2 = new ReportParameter("Marks2", marks2);
                                    ReportParameter p3 = new ReportParameter("Marks3", marks3);
                                    ReportParameter p4 = new ReportParameter("Calendar", calendar);
                                    ReportParameter p5 = new ReportParameter("Section", sectionName);

                                    ExamResultViewPrint.LocalReport.ReportPath = Server.MapPath("~/miu/result/report/ExamResultPrintTheory.rdlc");
                                    this.ExamResultViewPrint.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5 });
                                    ReportDataSource rds = new ReportDataSource("ExamResultPrint", examResultPrintTheory);
                                    ReportDataSource rds2 = new ReportDataSource("RptCourseAndTeacherInfo", list);
                                    lblMsg.Text = "";
                                    ExamResultViewPrint.LocalReport.DataSources.Clear();
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds);
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds2);

                                    Warning[] warnings;
                                    string[] streamids;
                                    string mimeType;
                                    string encoding;
                                    string filenameExtension;

                                    byte[] bytes = ExamResultViewPrint.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                                    using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportPDF/" + "ExamResultViewPrint" + ".pdf"), FileMode.Create))
                                    {
                                        fs.Write(bytes, 0, bytes.Length);
                                    }

                                    string path = Server.MapPath("~/Upload/ReportPDF/" + "ExamResultViewPrint" + ".pdf");

                                    WebClient client = new WebClient();   // Open PDF File in Web Browser 

                                    Byte[] buffer = client.DownloadData(path);
                                    if (buffer != null)
                                    {
                                        Response.ContentType = "application/pdf";
                                        Response.AddHeader("content-length", buffer.Length.ToString());
                                        Response.BinaryWrite(buffer);
                                    }
                                }
                                else if (examTemplate.TemplateName.Contains("ENB special"))
                                {
                                    //Special
                                    List<rExamResultPrintSpecial> examResultPrintSpecial = ExamMarkManager.GetExamResultPrintLabSpecial(table, columnName[0], columnName[1], columnName[2], columnName[3], columnName[4], columnName[5], columnName[6], totalColumn);
                                    List<rExamResultCourseAndTeacherInfo> list = ExamMarkManager.GetExamResultCourseAndTeacherInfo(acaCalSecId, acaCalId);

                                    string marks1 = columnName[0];
                                    string marks2 = columnName[1];
                                    string marks3 = columnName[2];
                                    string marks4 = columnName[3];
                                    string marks5 = columnName[4];
                                    string marks6 = columnName[5];
                                    string marks7 = columnName[6];
                                    string calendar = ddlCalenderType.SelectedItem.Text;

                                    ReportParameter p1 = new ReportParameter("Marks1", marks1);
                                    ReportParameter p2 = new ReportParameter("Marks2", marks2);
                                    ReportParameter p3 = new ReportParameter("Marks3", marks3);
                                    ReportParameter p4 = new ReportParameter("Marks4", marks4);
                                    ReportParameter p5 = new ReportParameter("Marks5", marks5);
                                    ReportParameter p6 = new ReportParameter("Marks6", marks6);
                                    ReportParameter p7 = new ReportParameter("Marks7", marks7);
                                    ReportParameter p8 = new ReportParameter("Calendar", calendar);
                                    ReportParameter p9 = new ReportParameter("Section", sectionName);

                                    ExamResultViewPrint.LocalReport.ReportPath = Server.MapPath("~/miu/result/report/ExamResultPrintSpecial.rdlc");
                                    this.ExamResultViewPrint.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7, p8,p9 });
                                    ReportDataSource rds = new ReportDataSource("RptExamResultPrintSpecial", examResultPrintSpecial);
                                    ReportDataSource rds2 = new ReportDataSource("RptCourseAndTeacherInfo", list);
                                    lblMsg.Text = "";
                                    ExamResultViewPrint.LocalReport.DataSources.Clear();
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds);
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds2);

                                    Warning[] warnings;
                                    string[] streamids;
                                    string mimeType;
                                    string encoding;
                                    string filenameExtension;

                                    byte[] bytes = ExamResultViewPrint.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                                    using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportPDF/" + "ExamResultViewPrint" + ".pdf"), FileMode.Create))
                                    {
                                        fs.Write(bytes, 0, bytes.Length);
                                    }

                                    string path = Server.MapPath("~/Upload/ReportPDF/" + "ExamResultViewPrint" + ".pdf");

                                    WebClient client = new WebClient();   // Open PDF File in Web Browser 

                                    Byte[] buffer = client.DownloadData(path);
                                    if (buffer != null)
                                    {
                                        Response.ContentType = "application/pdf";
                                        Response.AddHeader("content-length", buffer.Length.ToString());
                                        Response.BinaryWrite(buffer);
                                    }
                                }
                                else if (examTemplate.TemplateName == "Total Marks")
                                {
                                    //DataTable dt = table;
                                    //List<rGradeOnly> studentTotalMarkList = new List<rGradeOnly>();
                                    //for(int i=0; i<dt.Rows.Count; i++)
                                    //{
                                    //    rGradeOnly rGradeOnlyObj = new rGradeOnly();
                                    //    rGradeOnlyObj.Name = "";
                                    //    rGradeOnlyObj.Roll = table.Rows[i]["Student Roll"].ToString();
                                    //    rGradeOnlyObj.Total = table.Rows[i]["Total Marks(100)"].ToString();
                                    //    rGradeOnlyObj.Total = table.Rows[i]["Total(100)"].ToString();
                                    //    rGradeOnlyObj.Grade = table.Rows[i]["Grade"].ToString();
                                    //    rGradeOnlyObj.Point = table.Rows[i]["Point"].ToString();

                                    //    studentTotalMarkList.Add(rGradeOnlyObj);
                                    //}

                                    List<rGradeOnly> studentTotalMarkList = ExamMarkManager.GetTotalMarksStudentResult(table);
                                    List<rExamResultCourseAndTeacherInfo> list = ExamMarkManager.GetExamResultCourseAndTeacherInfo(acaCalSecId, acaCalId);


                                    string calendar = ddlCalenderType.SelectedItem.Text;
                                    string section = acaCalSec.SectionName;

                                    ReportParameter p1 = new ReportParameter("Calender", calendar);
                                    ReportParameter p2 = new ReportParameter("Section", section);

                                    ExamResultViewPrint.LocalReport.ReportPath = Server.MapPath("~/miu/result/report/RptTotalMarkResult.rdlc");
                                    this.ExamResultViewPrint.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
                                    ReportDataSource rds = new ReportDataSource("RptTotalMarks", studentTotalMarkList);
                                    ReportDataSource rds2 = new ReportDataSource("RptCourseAndTeacherInfo", list);
                                    lblMsg.Text = "";
                                    ExamResultViewPrint.LocalReport.DataSources.Clear();
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds);
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds2);

                                    Warning[] warnings;
                                    string[] streamids;
                                    string mimeType;
                                    string encoding;
                                    string filenameExtension;

                                    byte[] bytes = ExamResultViewPrint.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                                    using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportPDF/" + "ExamResultViewPrint" + ".pdf"), FileMode.Create))
                                    {
                                        fs.Write(bytes, 0, bytes.Length);
                                    }

                                    string path = Server.MapPath("~/Upload/ReportPDF/" + "ExamResultViewPrint" + ".pdf");

                                    WebClient client = new WebClient();   // Open PDF File in Web Browser 

                                    Byte[] buffer = client.DownloadData(path);
                                    if (buffer != null)
                                    {
                                        Response.ContentType = "application/pdf";
                                        Response.AddHeader("content-length", buffer.Length.ToString());
                                        Response.BinaryWrite(buffer);
                                    }
                                }

                                else if (examTemplate.TemplateName.Contains("Grade Only"))
                                {
                                    //Grade Only
                                    List<rGradeOnly> gradeOnly = ExamMarkManager.GetGradeOnly(table, totalColumn);
                                    List<rExamResultCourseAndTeacherInfo> list = ExamMarkManager.GetExamResultCourseAndTeacherInfo(acaCalSecId, acaCalId);

                                    string calendar = ddlCalenderType.SelectedItem.Text;

                                    ReportParameter p1 = new ReportParameter("Calender", calendar);
                                    ReportParameter p2 = new ReportParameter("Section", sectionName);

                                    ExamResultViewPrint.LocalReport.ReportPath = Server.MapPath("~/miu/result/report/RptExamResultPrintGradeOnly.rdlc");
                                    this.ExamResultViewPrint.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
                                    ReportDataSource rds = new ReportDataSource("RptExamResultPrintGradeOnly", gradeOnly);
                                    ReportDataSource rds2 = new ReportDataSource("RptCourseAndTeacherInfo", list);
                                    lblMsg.Text = "";
                                    ExamResultViewPrint.LocalReport.DataSources.Clear();
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds);
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds2);

                                    Warning[] warnings;
                                    string[] streamids;
                                    string mimeType;
                                    string encoding;
                                    string filenameExtension;

                                    byte[] bytes = ExamResultViewPrint.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                                    using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportPDF/" + "ExamResultViewPrint" + ".pdf"), FileMode.Create))
                                    {
                                        fs.Write(bytes, 0, bytes.Length);
                                    }

                                    string path = Server.MapPath("~/Upload/ReportPDF/" + "ExamResultViewPrint" + ".pdf");

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
                                    //4
                                    List<rExamResultPrintLab> examResultPrintLab = ExamMarkManager.GetExamResultPrintLab(table, columnName[0], columnName[1], columnName[2], columnName[3], totalColumn);
                                    List<rExamResultCourseAndTeacherInfo> list = ExamMarkManager.GetExamResultCourseAndTeacherInfo(acaCalSecId, acaCalId);

                                    string marks1 = columnName[0];
                                    string marks2 = columnName[1];
                                    string marks3 = columnName[2];
                                    string marks4 = columnName[3];
                                    string calendar = ddlCalenderType.SelectedItem.Text;

                                    ReportParameter p1 = new ReportParameter("Marks1", marks1);
                                    ReportParameter p2 = new ReportParameter("Marks2", marks2);
                                    ReportParameter p3 = new ReportParameter("Marks3", marks3);
                                    ReportParameter p4 = new ReportParameter("Marks4", marks4);
                                    ReportParameter p5 = new ReportParameter("Calendar", calendar);
                                    ReportParameter p6 = new ReportParameter("Section", sectionName);

                                    ExamResultViewPrint.LocalReport.ReportPath = Server.MapPath("~/miu/result/report/RptExamResultPrintLab.rdlc");
                                    this.ExamResultViewPrint.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6 });
                                    ReportDataSource rds = new ReportDataSource("RptExamResultPrintLab", examResultPrintLab);
                                    ReportDataSource rds2 = new ReportDataSource("RptCourseAndTeacherInfo", list);
                                    lblMsg.Text = "";
                                    ExamResultViewPrint.LocalReport.DataSources.Clear();
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds);
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds2);

                                    Warning[] warnings;
                                    string[] streamids;
                                    string mimeType;
                                    string encoding;
                                    string filenameExtension;

                                    byte[] bytes = ExamResultViewPrint.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                                    using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportPDF/" + "ExamResultViewPrint" + ".pdf"), FileMode.Create))
                                    {
                                        fs.Write(bytes, 0, bytes.Length);
                                    }

                                    string path = Server.MapPath("~/Upload/ReportPDF/" + "ExamResultViewPrint" + ".pdf");

                                    WebClient client = new WebClient();   // Open PDF File in Web Browser 

                                    Byte[] buffer = client.DownloadData(path);
                                    if (buffer != null)
                                    {
                                        Response.ContentType = "application/pdf";
                                        Response.AddHeader("content-length", buffer.Length.ToString());
                                        Response.BinaryWrite(buffer);
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        ExamResultViewPrint.LocalReport.DataSources.Clear();
                        lblMsg.Text = "NO Data Found. Please Select Valid Session And Course Name";
                        return;
                    }
                }
            }
            else
            {
                if (newExamTemplate.TemplateName == "Grade Only")
                {

                    List<rGradeOnly> gradeOnly = ExamMarkManager.GetGradeOnlyStudentResult(acaCalSecId);
                    List<rExamResultCourseAndTeacherInfo> list = ExamMarkManager.GetExamResultCourseAndTeacherInfo(acaCalSecId, acaCalId);

                    string calendar = ddlCalenderType.SelectedItem.Text;

                    ReportParameter p1 = new ReportParameter("Calender", calendar);
                    ReportParameter p2 = new ReportParameter("Section", sectionName);

                    ExamResultViewPrint.LocalReport.ReportPath = Server.MapPath("~/miu/result/report/RptExamResultPrintGradeOnly.rdlc");
                    this.ExamResultViewPrint.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
                    ReportDataSource rds = new ReportDataSource("RptExamResultPrintGradeOnly", gradeOnly);
                    ReportDataSource rds2 = new ReportDataSource("RptCourseAndTeacherInfo", list);
                    lblMsg.Text = "";
                    ExamResultViewPrint.LocalReport.DataSources.Clear();
                    ExamResultViewPrint.LocalReport.DataSources.Add(rds);
                    ExamResultViewPrint.LocalReport.DataSources.Add(rds2);

                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;

                    byte[] bytes = ExamResultViewPrint.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                    using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportPDF/" + "ExamResultViewPrint" + ".pdf"), FileMode.Create))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }

                    string path = Server.MapPath("~/Upload/ReportPDF/" + "ExamResultViewPrint" + ".pdf");

                    WebClient client = new WebClient();   // Open PDF File in Web Browser 

                    Byte[] buffer = client.DownloadData(path);
                    if (buffer != null)
                    {
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-length", buffer.Length.ToString());
                        Response.BinaryWrite(buffer);
                    }
                }
            }

        }
        catch { }
        finally { }
    }

    protected void Approve_Click(Object sender, EventArgs e)
    {
        try
        {
            LinkButton linkButton = new LinkButton();
            linkButton = (LinkButton)sender;
            int acaCalSectionId = Convert.ToInt32(linkButton.CommandArgument);


            //string result = ExamMarkManager.GetApprovedNumberByExamController(acaCalSectionId);
            //string[] totalStudent = result.Split('-');
            //lblMsg.Text = "Grade Transfer: " + totalStudent[0] + " and I Grade: " + totalStudent[1];

            int result = ExamMarkManager.GetResubmitApprovedByExamController("approve", acaCalSectionId);

            lblMsg.Text = "Approved " + result + " student result";

            Load_Click(null, null);
        }
        catch { }
        finally { }
    }

    protected void Resubmit_Click(Object sender, EventArgs e)
    {
        try
        {
            LinkButton linkButton = new LinkButton();
            linkButton = (LinkButton)sender;
            int acaCalSectionId = Convert.ToInt32(linkButton.CommandArgument);


            int result = ExamMarkManager.GetResubmitApprovedByExamController("resubmit", acaCalSectionId);

            lblMsg.Text = "Done";

            Load_Click(null, null);
        }
        catch { }
        finally { }
    }

    protected void Publish_Click(Object sender, EventArgs e)
    {
        try
        {
            if (ddlAcademicCalender.SelectedValue == "0" || ddlProgram.SelectedValue=="0")
            {
                lblMsg.Text = "Please select Academic Calender and Program for publishing program wise all result!";
            }
            else
            {
                int result = ExamMarkManager.GetPublishNumberByExamController(Convert.ToInt32(ddlAcademicCalender.SelectedValue),Convert.ToInt32(ddlProgram.SelectedValue));
                lblMsg.Text = "Published " + result + " Sections Result";

                gvExamMarkApprove.Visible = false;
                pnSummaryReport.Visible = false;

                gvExamMarkApprove.DataSource = null;
                gvExamMarkApprove.DataBind();
            }
        }
        catch { }
        finally { }
    }

    protected void SinglePublish_Click(Object sender, EventArgs e)
    {
        try
        {
            if (ddlCourse.SelectedValue == "0_0")
            {
                lblMsg.Text = "Please select a course";
                return;
            }
            else
            {
                int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
                string[] CouseVersion = ddlCourse.SelectedValue.Split('_');
                Course course = CourseManager.GetByCourseIdVersionId(Convert.ToInt32(CouseVersion[0]), Convert.ToInt32(CouseVersion[1]));

                if (course != null)
                {
                    List<AcademicCalenderSection> acaCalSecList = AcademicCalenderSectionManager.GetByAcaCalCourseVersion(acaCalId, course.CourseID, course.VersionID);
                    if (acaCalSecList.Count > 0 && acaCalSecList != null)
                    {
                        int countPublishCourse = 0;
                        foreach (AcademicCalenderSection acaCalSec in acaCalSecList)
                        {
                            string result = ExamMarkManager.GetApprovedNumberByExamController(acaCalSec.AcaCal_SectionID);
                            if (result != "0-0")
                                countPublishCourse++;
                        }
                        lblMsg.Text = "Published " + countPublishCourse + " Sections Result";
                    }
                }
                else
                {
                    lblMsg.Text = "error 5F2CF";
                }
            }
        }
        catch { }
        finally { }
    }

    #endregion
}