using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExamResultApprove : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        lblMsg.Text = "";
        if (!IsPostBack)
        {
            SetUserInfoInSession();
            LoadComboBox();
            pnSubmitStudentMarkTop.Visible = false;
            pnSubmitStudentMarkButtom.Visible = false;
            gvFinalExamResultApprove.Visible = false;
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

                academicCalenderList = academicCalenderList.Where(x => x.IsCurrent == true).ToList();
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

            List<AcademicCalenderSection> acaCalSectionList = AcademicCalenderSectionManager.GetAllByAcaCalIdState(acaCalId, "IsTransfer");
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

            List<Exam> examList = ExamManager.GetAll();

            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            int acaCalSecId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);

            if (acaCalId == 0 || acaCalSecId == 0)
            {
                lblMsg.Text = "Please select Academic Calender, Course and Test dropdown correctly";
                return;
            }

            DataTable table = new DataTable();
            table.Columns.Add("Student Roll", typeof(string));

            AcademicCalenderSection acaCalSec = AcademicCalenderSectionManager.GetById(acaCalSecId);
            if (acaCalSec != null)
            {
                List<ExamTemplateItem> examTemplateItemList = TemplateGroupManager.GetAllByTemplateId(acaCalSec.BasicExamTemplateId);
                List<ExamMarkDTO> examMarkDTOList = ExamMarkManager.GetAllStudentByAcaCalAcaCalSec(acaCalId, acaCalSecId);
                if (examMarkDTOList.Count > 0 && examMarkDTOList != null)
                    examMarkDTOList = examMarkDTOList.Where(x => x.IsFinalSubmit == true && x.IsTransfer != true).ToList();

                if (examMarkDTOList.Count > 0 && examMarkDTOList != null)
                {
                    List<string> studentList = examMarkDTOList.Select(x => x.Roll).Distinct().ToList();


                    decimal totalMark = 0;
                    int finalExamFlag = 0;
                    for (int i = 0; i < examTemplateItemList.Count; i++)
                    {
                        Exam exam = ExamManager.GetById(examTemplateItemList[i].ExamId);
                        if (exam != null)
                        {
                            if (exam.ExamName.Contains("Final"))
                                finalExamFlag = exam.ExamId;

                            table.Columns.Add(exam.ExamName + "(" + exam.Marks + ")", typeof(string));
                            totalMark += exam.Marks;
                        }
                        else
                            table.Columns.Add("Column" + (i + 1), typeof(string));
                    }
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
                            else
                                rowArray[newRowCounter + 1] = examMark;

                            newRowCounter = newRowCounter + 1;
                            studentTotalMark += Convert.ToDecimal(examMark);

                            if (flag == 0)
                                gradeMasterId = examMarkDTOList.Where(x => x.Roll == studentRoll && x.ExamId == examId).Select(x => x.GradeMasterId).SingleOrDefault();
                            flag = 1;
                        }

                        studentTotalMark = Decimal.Ceiling(studentTotalMark);
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
                            rowArray[newRowCounter + 3] = "-";
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

                    pnSubmitStudentMarkTop.Visible = true;
                    pnSubmitStudentMarkButtom.Visible = true;

                    gvFinalExamResultApprove.Visible = true;
                    gvFinalExamResultApprove.DataSource = table;
                    gvFinalExamResultApprove.DataBind();
                }
                else
                {
                    gvFinalExamResultApprove.Visible = true;
                    gvFinalExamResultApprove.DataSource = null;
                    gvFinalExamResultApprove.DataBind();

                    pnSubmitStudentMarkTop.Visible = false;
                    pnSubmitStudentMarkButtom.Visible = false;
                }
            }
        }
        catch
        {
            gvFinalExamResultApprove.Visible = false;
            gvFinalExamResultApprove.DataSource = null;
            gvFinalExamResultApprove.DataBind();

            pnSubmitStudentMarkTop.Visible = false;
            pnSubmitStudentMarkButtom.Visible = false;
        }
        finally { }
    }

    protected void FinalApprove_Click(Object sender, EventArgs e)
    {
        try
        {
            CourseStatus courseStatus = CourseStatusManager.GetByCode("I");

            int modifiedId = 99;
            string loginId = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
            User user = UserManager.GetByLogInId(loginId);
            if (user != null)
                modifiedId = user.User_ID;

            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            int acaCalSecId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);

            if (acaCalId == 0 || acaCalSecId == 0)
            {
                lblMsg.Text = "Please select Academic Calender, Course and Test dropdown correctly";
                return;
            }


            //int resultUpdateIsTransfer = ExamMarkManager.GetTotalUpdateNumberIsTransfer(acaCalId, acaCalSecId);

            List<ExamMarkDTO> examMarkDTOList = ExamMarkManager.GetAllStudentByAcaCalAcaCalSec(acaCalId, acaCalSecId);

            #region Update 

            int updateCount = 0;
            int columnCount = gvFinalExamResultApprove.Rows[0].Cells.Count;
            for (int i = 0; i < gvFinalExamResultApprove.Rows.Count; i++)
            {
                List<ExamMarkDTO> tempExamMarkDTOList = new List<ExamMarkDTO>();

                string roll = gvFinalExamResultApprove.Rows[i].Cells[1].Text;
                decimal ObtainedTotalMarks = Convert.ToDecimal(gvFinalExamResultApprove.Rows[i].Cells[columnCount - 3].Text);
                string ObtainedGrade = gvFinalExamResultApprove.Rows[i].Cells[columnCount - 2].Text;
                decimal ObtainedGPA = Convert.ToDecimal(gvFinalExamResultApprove.Rows[i].Cells[columnCount - 1].Text == "-" ? "0.0" : gvFinalExamResultApprove.Rows[i].Cells[columnCount - 1].Text);

                tempExamMarkDTOList = examMarkDTOList.Where(x => x.Roll == roll).ToList();
                if (tempExamMarkDTOList.Count > 0 && tempExamMarkDTOList != null)
                {
                    StudentCourseHistory tempStudentCourseHistory = StudentCourseHistoryManager.GetById(tempExamMarkDTOList[0].CourseHistoryId);
                    if (tempStudentCourseHistory != null)
                    {
                        if (ObtainedGrade == "I")
                        {
                            tempStudentCourseHistory.ObtainedTotalMarks = 0;
                            tempStudentCourseHistory.ObtainedGPA = 0;
                            tempStudentCourseHistory.ObtainedGrade = "";
                            tempStudentCourseHistory.CourseStatusID = courseStatus.CourseStatusID;
                        }
                        else
                        {
                            tempStudentCourseHistory.ObtainedTotalMarks = Decimal.Ceiling(ObtainedTotalMarks);
                            tempStudentCourseHistory.ObtainedGPA = ObtainedGPA;
                            tempStudentCourseHistory.ObtainedGrade = ObtainedGrade;
                        }

                        tempStudentCourseHistory.ModifiedBy = modifiedId;
                        tempStudentCourseHistory.ModifiedDate = DateTime.Now;
                        tempStudentCourseHistory.CourseStatusDate = DateTime.Now;

                        bool updateResultHistory = StudentCourseHistoryManager.Update(tempStudentCourseHistory);

                        if (updateResultHistory)
                            updateCount++;
                    }
                }
            }



            if (gvFinalExamResultApprove.Rows.Count == updateCount)
            {
                int resultUpdateIsTransfer = ExamMarkManager.GetTotalUpdateNumberIsTransfer(acaCalId, acaCalSecId);
                if (resultUpdateIsTransfer == updateCount)
                    lblMsg.Text = "Total " + updateCount + " student result approved successfully";
                else
                    lblMsg.Text = "Error No c5005";
            }
            else
                lblMsg.Text = "Error No c5006";
            
            
            #endregion

            

        }
        catch { lblMsg.Text = "Error No c5007"; }
        finally { }
    }

    #endregion
}