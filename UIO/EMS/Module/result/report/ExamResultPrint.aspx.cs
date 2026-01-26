using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.BusinessObjects.RO;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExamResultPrint : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        pnlMessage.Visible = false;

        if (!IsPostBack)
        {
            SetUserInfoInSession();
            LoadComboBox();
        }
    }

    protected void SetUserInfoInSession()
    {
        try
        {
            int employeeId = 0;

            //HttpCookie aCookie = Request.Cookies[ConstantValue.Cookie_Authentication];
            //string uid = aCookie["UserName"];
            //string pwd = aCookie["UserPassword"];

            string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();

            User user = UserManager.GetByLogInId(loginID);
            if (user != null)
            {
                Role role = RoleManager.GetById(user.RoleID);
                if (role != null)
                {
                    Session["Role"] = role.RoleName;
                }
                if (user.Person != null)
                {
                    if (user.Person.Employee != null)
                        employeeId = user.Person.Employee.EmployeeID;
                }
            }
        }
        catch { }
    }

    protected void LoadComboBox()
    {
        try
        {
            ddlAcademicCalender.Items.Clear();
            ddlAcademicCalender.Items.Add(new ListItem("Select", "0"));
            ddlAcaCalSection.Items.Clear();
            ddlAcaCalSection.Items.Add(new ListItem("Select", "0"));

            LoadCalenderType();
        }
        catch { }
        finally { }
    }

    protected void LoadCalenderType()
    {
        try
        {
            ddlCalenderType.Items.Clear();
            //ddlCalenderType.Items.Add(new ListItem("Select", "0"));
            //ddlCalenderType.AppendDataBoundItems = true;

            List<CalenderUnitMaster> calenderUnitMasterList = CalenderUnitMasterManager.GetAll();

            if (calenderUnitMasterList.Count > 0 && calenderUnitMasterList != null)
            {
                ddlCalenderType.DataValueField = "CalenderUnitMasterID";
                ddlCalenderType.DataTextField = "Name";
                ddlCalenderType.DataSource = calenderUnitMasterList;
                ddlCalenderType.DataBind();
            }
        }
        catch { }
        finally
        {
            int calenderTypeId = Convert.ToInt32(ddlCalenderType.SelectedValue);
            LoadAcademicCalender(calenderTypeId);
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

                academicCalenderList = academicCalenderList.Where(x => x.IsActiveRegistration == true).ToList();
                ddlAcademicCalender.SelectedValue = academicCalenderList[0].AcademicCalenderID.ToString();

                AcademicCalender_Changed(null, null);
            }
        }
        catch { }
    }

    protected void LoadAcaCalSection(int acaCalId)
    {
        try
        {
            int employeeId = 0;

            //HttpCookie aCookie = Request.Cookies[ConstantValue.Cookie_Authentication];
            //string uid = aCookie["UserName"];
            //string pwd = aCookie["UserPassword"];

            string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();

            User user = UserManager.GetByLogInId(loginID);
            if (user != null)
            {
                if (user.Person != null)
                {
                    if (user.Person.Employee != null)
                        employeeId = user.Person.Employee.EmployeeID;
                }
            }

            List<AcademicCalenderSection> acaCalSectionList = AcademicCalenderSectionManager.GetAllByAcaCalId(acaCalId);
            if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
            {
                ddlAcaCalSection.Items.Clear();
                ddlAcaCalSection.Items.Add(new ListItem("Select", "0"));

                //if (acaCalId != 0)
                //    acaCalSectionList = acaCalSectionList.Where(x => x.AcademicCalenderID == acaCalId && x.AcaCal_SectionID == 1171).ToList();

                if (Session["Role"].ToString().Contains("Faculty") || Session["Role"].ToString().Contains("Coordinator"))
                {
                    if (employeeId != 0)
                        acaCalSectionList = acaCalSectionList.Where(x => x.TeacherOneID == employeeId || x.TeacherTwoID == employeeId).ToList();
                    else
                        acaCalSectionList = null;
                }
                else if (!Session["Role"].ToString().Contains("Admin") && !Session["Role"].ToString().Contains("Exam") && !Session["Role"].ToString().Contains("Controller"))
                {
                    acaCalSectionList = null;
                }

                if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
                {
                    List<Course> courseList = CourseManager.GetAll();
                    Hashtable hashCourse = new Hashtable();
                    foreach (Course course in courseList)
                        hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.FormalCode + ":" + course.Title);

                    Dictionary<string, string> dicAcaCalSec = new Dictionary<string, string>();
                    foreach (AcademicCalenderSection acaCalSection in acaCalSectionList)
                    {
                        string courseVersion = acaCalSection.CourseID.ToString() + "_" + acaCalSection.VersionID.ToString();
                        try { dicAcaCalSec.Add(hashCourse[courseVersion] + "(" + acaCalSection.SectionName + ") ", acaCalSection.AcaCal_SectionID.ToString()); }
                        catch { }
                    }
                    //var acaCalSecList = dicAcaCalSec.Where(c => c.Key.ToUpper().Contains(searchKey.ToUpper())).OrderBy(x => x.Key).ToList();
                    var acaCalSecList = dicAcaCalSec.OrderBy(x => x.Key);
                    foreach (var temp in acaCalSecList)
                        ddlAcaCalSection.Items.Add(new ListItem(temp.Key, temp.Value));
                }
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
    }

    protected void AcademicCalender_Changed(Object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            LoadAcaCalSection(acaCalId);
        }
        catch { }
    }

    protected void LoadResult_Click(Object sender, EventArgs e)
    {
        try
        {
            List<ValueSet> valueSetList = ValueSetManager.GetAll().Where(x => x.ValueSetName.Contains("ExamMarkStatus")).ToList();
            List<Value> valueList = ValueManager.GetAll().Where(x => x.ValueSetID == valueSetList[0].ValueSetID && x.ValueName.Contains("Absent")).ToList();
            List<Value> valueList2 = ValueManager.GetAll().Where(x => x.ValueSetID == valueSetList[0].ValueSetID && x.ValueName.Contains("Reported")).ToList();

            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            int acaCalSecId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);

            if (acaCalId == 0 || acaCalSecId == 0)
            {
                ShowMessage("Please Select Session And Course Name");
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
                    //if (examMarkDTOList.Count > 0 && examMarkDTOList != null)
                    //    examMarkDTOList = examMarkDTOList.Where(x => x.IsFinalSubmit != true && x.IsTransfer != true).ToList();


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
                                //===Modify for examMark when ""=====================//
                                if (examMark == "")
                                    examMark = "0";
                                //=========================
                                if (examStatus == valueList[0].ValueID)
                                    rowArray[newRowCounter + 1] = "Absent";
                                else if (examStatus == valueList2[0].ValueID)
                                    rowArray[newRowCounter + 1] = "Reported";
                                else
                                    rowArray[newRowCounter + 1] = examMark;

                                newRowCounter = newRowCounter + 1;
                                studentTotalMark += Convert.ToDecimal(examMark);

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
                            //rowArray[newRowCounter + 1] = Decimal.Ceiling(studentTotalMark);
                            //rowArray[newRowCounter + 2] = temp.Grade;
                            //rowArray[newRowCounter + 3] = temp.GradePoint;
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
                                    this.ExamResultViewPrint.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6,p7,p8 });
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
                                    string section = acaCalSec.SectionName;

                                    ReportParameter p1 = new ReportParameter("Marks1", marks1);
                                    ReportParameter p2 = new ReportParameter("Marks2", marks2);
                                    ReportParameter p3 = new ReportParameter("Marks3", marks3);
                                    ReportParameter p4 = new ReportParameter("Calendar", calendar);
                                    ReportParameter p5 = new ReportParameter("Section", section);

                                    ExamResultViewPrint.LocalReport.ReportPath = Server.MapPath("~/miu/result/report/ExamResultPrintTheory.rdlc");
                                    this.ExamResultViewPrint.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5 });
                                    ReportDataSource rds = new ReportDataSource("ExamResultPrint", examResultPrintTheory);
                                    ReportDataSource rds2 = new ReportDataSource("RptCourseAndTeacherInfo", list);
                                    lblMsg.Text = "";
                                    ExamResultViewPrint.LocalReport.DataSources.Clear();
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds);
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds2);


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
                                    string section = acaCalSec.SectionName;

                                    ReportParameter p1 = new ReportParameter("Marks1", marks1);
                                    ReportParameter p2 = new ReportParameter("Marks2", marks2);
                                    ReportParameter p3 = new ReportParameter("Marks3", marks3);
                                    ReportParameter p4 = new ReportParameter("Marks4", marks4);
                                    ReportParameter p5 = new ReportParameter("Marks5", marks5);
                                    ReportParameter p6 = new ReportParameter("Marks6", marks6);
                                    ReportParameter p7 = new ReportParameter("Marks7", marks7);
                                    ReportParameter p8 = new ReportParameter("Calendar", calendar);
                                    ReportParameter p9 = new ReportParameter("Section", section);

                                    ExamResultViewPrint.LocalReport.ReportPath = Server.MapPath("~/miu/result/report/ExamResultPrintSpecial.rdlc");
                                    this.ExamResultViewPrint.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7, p8, p9 });
                                    ReportDataSource rds = new ReportDataSource("RptExamResultPrintSpecial", examResultPrintSpecial);
                                    ReportDataSource rds2 = new ReportDataSource("RptCourseAndTeacherInfo", list);
                                    lblMsg.Text = "";
                                    ExamResultViewPrint.LocalReport.DataSources.Clear();
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds);
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds2);


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
                                }

                                else if (examTemplate.TemplateName.Contains("Grade Only"))
                                {
                                    List<rGradeOnly> gradeOnly = ExamMarkManager.GetGradeOnly(table, totalColumn);
                                    List<rExamResultCourseAndTeacherInfo> list = ExamMarkManager.GetExamResultCourseAndTeacherInfo(acaCalSecId, acaCalId);

                                    string calendar = ddlCalenderType.SelectedItem.Text;
                                    string section = acaCalSec.SectionName;


                                    ReportParameter p1 = new ReportParameter("Calender", calendar);
                                    ReportParameter p2 = new ReportParameter("Section", section);

                                    ExamResultViewPrint.LocalReport.ReportPath = Server.MapPath("~/miu/result/report/RptExamResultPrintGradeOnly.rdlc");
                                    this.ExamResultViewPrint.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
                                    ReportDataSource rds = new ReportDataSource("RptExamResultPrintGradeOnly", gradeOnly);
                                    ReportDataSource rds2 = new ReportDataSource("RptCourseAndTeacherInfo", list);
                                    lblMsg.Text = "";
                                    ExamResultViewPrint.LocalReport.DataSources.Clear();
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds);
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds2);

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
                                    string section = acaCalSec.SectionName;

                                    ReportParameter p1 = new ReportParameter("Marks1", marks1);
                                    ReportParameter p2 = new ReportParameter("Marks2", marks2);
                                    ReportParameter p3 = new ReportParameter("Marks3", marks3);
                                    ReportParameter p4 = new ReportParameter("Marks4", marks4);
                                    ReportParameter p5 = new ReportParameter("Calendar", calendar);
                                    ReportParameter p6 = new ReportParameter("Section", section);

                                    ExamResultViewPrint.LocalReport.ReportPath = Server.MapPath("~/miu/result/report/RptExamResultPrintLab.rdlc");
                                    this.ExamResultViewPrint.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6 });
                                    ReportDataSource rds = new ReportDataSource("RptExamResultPrintLab", examResultPrintLab);
                                    ReportDataSource rds2 = new ReportDataSource("RptCourseAndTeacherInfo", list);
                                    lblMsg.Text = "";
                                    ExamResultViewPrint.LocalReport.DataSources.Clear();
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds);
                                    ExamResultViewPrint.LocalReport.DataSources.Add(rds2);

                                }

                            }
                        }
                    }
                    else
                    {
                        ExamResultViewPrint.LocalReport.DataSources.Clear();
                        ShowMessage("NO Data Found. Please Select Valid Session And Course Name");
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
                    string section = acaCalSec.SectionName;

                    ReportParameter p1 = new ReportParameter("Calender", calendar);
                    ReportParameter p2 = new ReportParameter("Section", section);

                    ExamResultViewPrint.LocalReport.ReportPath = Server.MapPath("~/miu/result/report/RptExamResultPrintGradeOnly.rdlc");
                    this.ExamResultViewPrint.LocalReport.SetParameters(new ReportParameter[] { p1,p2 });
                    ReportDataSource rds = new ReportDataSource("RptExamResultPrintGradeOnly", gradeOnly);
                    ReportDataSource rds2 = new ReportDataSource("RptCourseAndTeacherInfo", list);
                    lblMsg.Text = "";
                    ExamResultViewPrint.LocalReport.DataSources.Clear();
                    ExamResultViewPrint.LocalReport.DataSources.Add(rds);
                    ExamResultViewPrint.LocalReport.DataSources.Add(rds2);
                }
                else
                {
                    ExamResultViewPrint.LocalReport.DataSources.Clear();
                    ShowMessage("No marks found. Please select another course");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally { }
    }

    private void ShowMessage(string msg)
    {
        pnlMessage.Visible = true;

        lblMsg.Text = msg;
        lblMsg.ForeColor = Color.Red;
    }

    #endregion

}