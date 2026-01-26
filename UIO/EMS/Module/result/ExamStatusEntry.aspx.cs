using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;

namespace EMS.miu.result
{
    public partial class ExamStatusEntry : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            lblMsg.Text = "";
            if (!IsPostBack)
            {
                SetUserInfoInSession();
                LoadComboBox();
                pnSubmitStudentMarkTop.Visible = false;
                pnSubmitStudentMarkButtom.Visible = false;
                gvExamResultSubmit.Visible = false;
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
                ddlExam.Items.Clear();
                ddlExam.Items.Add(new ListItem("Select", "-1"));

                LoadProgram();
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
        
        protected void LoadAcaCalSection(int acaCalId)
        {
            try
            {
                ddlAcaCalSection.Items.Clear();
                ddlAcaCalSection.Items.Add(new ListItem("Select", "0"));
                ddlProgram.AppendDataBoundItems = true;

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
                    acaCalSectionList = acaCalSectionList.Where(x => x.ProgramID == Convert.ToInt32(ddlProgram.SelectedValue)).ToList();
                if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
                {
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
                        //var acaCalSecList = dicAcaCalSec.OrderBy(x => x.Key).ToList();
                        var acaCalSecList = dicAcaCalSec.Where(c => c.Key.ToUpper().Contains(txtSearch.Text.ToUpper())).OrderBy(x => x.Key).ToList();
                        foreach (var temp in acaCalSecList)
                            ddlAcaCalSection.Items.Add(new ListItem(temp.Key, temp.Value));
                    }
                }
                AcaCalSection_Changed(null, null);
            }
            catch { }
            finally { }
        }
        
        protected void LoadExam(int examTemplateId)
        {
            try
            {
                ddlExam.Items.Clear();
                ddlExam.Items.Add(new ListItem("Select", "-1"));
                ddlExam.AppendDataBoundItems = true;

                List<Exam> examList = ExamManager.GetAllByExamTemplateId(examTemplateId);
                if (examList.Count == 1)
                {
                    ddlExam.Items.Clear();
                    ddlExam.AppendDataBoundItems = true;

                    ddlExam.DataSource = examList;
                    ddlExam.DataValueField = "ExamId";
                    ddlExam.DataTextField = "ExamName";
                    ddlExam.DataBind();
                }
                else if (examList.Count > 1)
                {
                    ddlExam.Items.Clear();
                    ddlExam.Items.Add(new ListItem("All", "0"));
                    ddlExam.AppendDataBoundItems = true;

                    ddlExam.DataSource = examList;
                    ddlExam.DataValueField = "ExamId";
                    ddlExam.DataTextField = "ExamName";
                    ddlExam.DataBind();
                }
            }
            catch { }
            finally { }
        }
        
        protected void LoadStatusCombo(DropDownList ddlStatus)
        {
            try
            {
                //ddlStatus.Items.Clear();
                ////ddlStatus.Items.Add(new ListItem("Select", "0"));
                ////ddlStatus.AppendDataBoundItems = true;

                
                //if (valueSetList.Count > 0 && valueSetList != null)
                //{
                //    valueSetList = valueSetList.
                //    if (valueSetList.Count > 0 && valueSetList != null)
                //    {
                //        List<Value> valueList = ValueManager.GetByValueSetId(valueSetList[0].ValueSetID);
                //        if (valueList.Count > 0 && valueList != null)
                //        {
                //            if (ddlExam.Items.Count == 1)
                //                valueList = valueList.Where(x => x.ValueName != "Mark").OrderByDescending(x => x.ValueID).ToList();
                //            else
                //                valueList = valueList.Where(x => x.ValueName != "Grade").OrderBy(x => x.ValueID).ToList();

                //            ddlStatus.DataSource = valueList;
                //            ddlStatus.DataValueField = "ValueID";
                //            ddlStatus.DataTextField = "ValueName";
                //            ddlStatus.DataBind();
                //        }
                //    }
                //}
            }
            catch { }
            finally { }
        }

        #region Function
        protected int Insert(int courseHistory, int examId, string mark, int status, bool isFinalSubmit, int userId, string comment)
        {
            try
            {
                ExamMark examMark = new ExamMark();
                examMark.CourseHistoryId = courseHistory;
                examMark.ExamId = examId;
                examMark.Mark = Convert.ToString(0);
                examMark.Status = status;
                examMark.IsFinalSubmit = isFinalSubmit;
                examMark.CreateBy = userId;
                examMark.CreatedDate = DateTime.Now;

                int resultInsert = ExamMarkManager.Insert(examMark);

                if (status == 22)
                {
                    UpdateCourseHistory(courseHistory, userId, examId, comment);
                }

                return resultInsert;
            }
            catch { return 0; }
            finally { }
        }
        
        protected bool Update(int id, int courseHistory, int examId, string mark, int status, bool isFinalSubmit, int userId, string comment)
        {
            try
            {
                ExamMark examMark = ExamMarkManager.GetById(id);

                examMark.CourseHistoryId = courseHistory;
                examMark.ExamId = examId;
                examMark.Mark = Convert.ToString( 0);
                examMark.Status = status;
                examMark.IsFinalSubmit = isFinalSubmit;
                examMark.ModifiedBy = userId;
                examMark.ModifiedDate = DateTime.Now;

                bool resultUpdate = ExamMarkManager.Update(examMark);
                if (status == 22) 
                {
                    UpdateCourseHistory(courseHistory, userId, examId, comment);
                }

                return resultUpdate;
            }
            catch { return false; }
            finally { }
        }

        private void UpdateCourseHistory(int courseHistoryId, int userId, int examId, string comment)
        {
            StudentCourseHistory studentCourseHistoryObj = StudentCourseHistoryManager.GetById(courseHistoryId);
            Student studentObj = StudentManager.GetById(studentCourseHistoryObj.StudentID);
            if (studentObj.GradeMasterId != null)
            {
                GradeDetails gradeDetailsObj = GradeDetailsManager.GetByGradeMasterId(Convert.ToInt32(studentObj.GradeMasterId)).Where(d=> d.Grade.Contains('F')).FirstOrDefault();
                CourseStatus courseStatusObj = CourseStatusManager.GetByCode(Convert.ToString('F'));
                Exam examObj = ExamManager.GetById(examId);
                if (gradeDetailsObj != null) 
                {
                    studentCourseHistoryObj.IsConsiderGPA = true;
                    studentCourseHistoryObj.GradeId = gradeDetailsObj.GradeId;
                    studentCourseHistoryObj.ObtainedGPA = gradeDetailsObj.GradePoint;
                    studentCourseHistoryObj.ObtainedGrade = gradeDetailsObj.Grade;
                    studentCourseHistoryObj.CourseStatusID = courseStatusObj.CourseStatusID;
                    studentCourseHistoryObj.Remark =  "Reported in " + examObj.ExamName +" exam. "+ comment;
                    studentCourseHistoryObj.ModifiedBy = userId;
                    studentCourseHistoryObj.ModifiedDate = DateTime.Now;

                    bool courseHistoryUpdate = StudentCourseHistoryManager.Update(studentCourseHistoryObj);
                }
            }
        }

        protected void GetDataForInsertUpdate(Label hfId, Label hfCourseHistoryId, Label hfExamId, DropDownList ddlStatus, TextBox txtMark, TextBox txtComment)
        {
            try
            {
                int userId = 99;
                string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
                User user = UserManager.GetByLogInId(loginID);
                if (user != null)
                    userId = user.User_ID;

                int id = Convert.ToInt32(hfId.Text);
                int courseHistoryId = Convert.ToInt32(hfCourseHistoryId.Text);
                int examId = Convert.ToInt32(hfExamId.Text) == 0 ? Convert.ToInt32(ddlExam.SelectedValue) : Convert.ToInt32(hfExamId.Text);
                int status = Convert.ToInt32(ddlStatus.SelectedValue);
                string comment = txtComment.Text;
                //string mark = ddlStatus.SelectedItem.Text == "Absent" ? "" : txtMark.Text.ToUpper().Replace(" ", "");
                string mark = null;
                bool isFinalSubmit = false;

                if (id == 0)
                {
                    int resultInsert = Insert(courseHistoryId, examId, mark, status, isFinalSubmit, userId, comment);
                    if (resultInsert > 0)
                    {
                        lblMsg.Text = "Save Successfully";
                    }
                }
                else
                {
                    bool resultUpdate = Update(id, courseHistoryId, examId, mark, status, isFinalSubmit, userId, comment);
                    if (resultUpdate)
                    {
                        lblMsg.Text = "Update Successfully";
                    }
                }
            }
            catch { }
            finally { }
        }
        
        protected bool GradeValidation(string grade)
        {
            try
            {
                if (grade == "A+" || grade == "A" || grade == "A-" || grade == "B+" || grade == "B" || grade == "B-" || grade == "C+" || grade == "C" || grade == "C-" || grade == "D+" || grade == "D" || grade == "D-" || grade == "F")
                    return true;
                else
                    return false;
            }
            catch { return false; }
            finally { }
        }

        protected bool MarkValidation(int examStatus, string examMark) 
        {
            try
            {
                if (examStatus == 17)
                {
                    if (isNumeric(examMark, System.Globalization.NumberStyles.Float))
                    {
                        return true;
                    }
                    else { return false; }
                }
                else
                    return false;
            }
            catch { return false; }
            finally { }
        }

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }
        #endregion Function

        #region Event

        protected void CalenderType_Changed(Object sender, EventArgs e)
        {
            try
            {
                int calenderTypeId = Convert.ToInt32(ddlCalenderType.SelectedValue);
                LoadAcademicCalender(calenderTypeId);

                pnSubmitStudentMarkTop.Visible = false;
                pnSubmitStudentMarkButtom.Visible = false;
                gvExamResultSubmit.Visible = false;

                ddlExam.Items.Clear();
                ddlExam.Items.Add(new ListItem("Select", "-1"));
            }
            catch { }
        }
        
        protected void AcademicCalender_Changed(Object sender, EventArgs e)
        {
            try
            {
                int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
                LoadAcaCalSection(acaCalId);

                pnSubmitStudentMarkTop.Visible = false;
                pnSubmitStudentMarkButtom.Visible = false;
                gvExamResultSubmit.Visible = false;

                ddlExam.Items.Clear();
                ddlExam.Items.Add(new ListItem("Select", "-1"));
            }
            catch { }
        }
        
        protected void Program_Changed(Object sender, EventArgs e)
        {
            try
            {
                int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
                LoadAcaCalSection(acaCalId);

                pnSubmitStudentMarkTop.Visible = false;
                pnSubmitStudentMarkButtom.Visible = false;
                gvExamResultSubmit.Visible = false;

                ddlExam.Items.Clear();
                ddlExam.Items.Add(new ListItem("Select", "-1"));
            }
            catch { }
            finally { }
        }
        
        protected void AcaCalSection_Changed(Object sender, EventArgs e)
        {
            try
            {
                int acaCalSecId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);
                AcademicCalenderSection acaCalSection = AcademicCalenderSectionManager.GetById(acaCalSecId);
                if (acaCalSection != null)
                {
                    LoadExam(acaCalSection.BasicExamTemplateId);
                }
                else
                {
                    ddlExam.Items.Clear();
                    ddlExam.Items.Add(new ListItem("Select", "-1"));
                }

                pnSubmitStudentMarkTop.Visible = false;
                pnSubmitStudentMarkButtom.Visible = false;
                gvExamResultSubmit.Visible = false;

            }
            catch { }
            finally { }
        }
        
        protected void ddlExam_Changed(Object sender, EventArgs e)
        {
            try
            {
                pnSubmitStudentMarkTop.Visible = false;
                pnSubmitStudentMarkButtom.Visible = false;
                gvExamResultSubmit.Visible = false;
            }
            catch { }
            finally { }
        }
        
        protected void GridRebind()
        {
            try
            {
                ValueSet valueSetList = ValueSetManager.GetAll().Where(x => x.ValueSetName.Contains("ExamMarkStatus")).FirstOrDefault();
                List<Value> valueList = ValueManager.GetByValueSetId(valueSetList.ValueSetID).Where(d=> d.Attribute1 == Convert.ToString('1')).ToList();
                
                for (int i = 0; i < gvExamResultSubmit.Rows.Count; i++)
                {
                    GridViewRow row = gvExamResultSubmit.Rows[i];
                    
                    DropDownList dropDown1 = (DropDownList)row.FindControl("ddlStatus1");
                    Label lblStatus1 = (Label)row.FindControl("hfStatus1");
                    dropDown1.DataSource = valueList;
                    dropDown1.DataValueField = "ValueID";
                    dropDown1.DataTextField = "ValueName";
                    dropDown1.DataBind();
                    dropDown1.SelectedValue = Convert.ToString(lblStatus1.Text);

                    DropDownList dropDown2 = (DropDownList)row.FindControl("ddlStatus2");
                    Label lblStatus2 = (Label)row.FindControl("hfStatus2");
                    dropDown2.DataSource = valueList;
                    dropDown2.DataValueField = "ValueID";
                    dropDown2.DataTextField = "ValueName";
                    dropDown2.DataBind();
                    dropDown2.SelectedValue = Convert.ToString(lblStatus2.Text);

                    DropDownList dropDown3 = (DropDownList)row.FindControl("ddlStatus3");
                    Label lblStatus3 = (Label)row.FindControl("hfStatus3");
                    dropDown3.DataSource = valueList;
                    dropDown3.DataValueField = "ValueID";
                    dropDown3.DataTextField = "ValueName";
                    dropDown3.DataBind();
                    dropDown3.SelectedValue = Convert.ToString(lblStatus3.Text);

                    //DropDownList dropDown4 = (DropDownList)row.FindControl("ddlStatus4");
                    //Label lblStatus4 = (Label)row.FindControl("hfStatus4");
                    //dropDown4.DataSource = valueList;
                    //dropDown4.DataValueField = "ValueID";
                    //dropDown4.DataTextField = "ValueName";
                    //dropDown4.DataBind();
                    //dropDown4.SelectedValue = Convert.ToString(lblStatus4.Text);

                    //DropDownList dropDown5 = (DropDownList)row.FindControl("ddlStatus5");
                    //Label lblStatus5 = (Label)row.FindControl("hfStatus5");
                    //dropDown5.DataSource = valueList;
                    //dropDown5.DataValueField = "ValueID";
                    //dropDown5.DataTextField = "ValueName";
                    //dropDown5.DataBind();
                    //dropDown5.SelectedValue = Convert.ToString(lblStatus5.Text);

                    //DropDownList dropDown6 = (DropDownList)row.FindControl("ddlStatus6");
                    //Label lblStatus6 = (Label)row.FindControl("hfStatus6");
                    //dropDown6.DataSource = valueList;
                    //dropDown6.DataValueField = "ValueID";
                    //dropDown6.DataTextField = "ValueName";
                    //dropDown6.DataBind();
                    //dropDown6.SelectedValue = Convert.ToString(lblStatus6.Text);

                    //DropDownList dropDown7 = (DropDownList)row.FindControl("ddlStatus7");
                    //Label lblStatus7 = (Label)row.FindControl("hfStatus7");
                    //dropDown7.DataSource = valueList;
                    //dropDown7.DataValueField = "ValueID";
                    //dropDown7.DataTextField = "ValueName";
                    //dropDown7.DataBind();
                    //dropDown7.SelectedValue = Convert.ToString(lblStatus7.Text);
                }
            }
            catch { }
            finally { }
        }
        
        protected void lbSave_Click(Object sender, EventArgs e)
        {
            try
            {
                LinkButton linkButton = new LinkButton();
                linkButton = (LinkButton)sender;

                if (ddlExam.SelectedValue == "-1")
                {
                    lblMsg.Text = "Grade Sheet Templete is not assign for this COURSE SECTION";
                }
                else if (ddlExam.SelectedValue != "0")
                {
                    //lblMsg.Text = "Grade Only template do not allow to absent or reported any student";
                    #region Single Exam Select

                    Label hfId = linkButton.NamingContainer.FindControl("hfId1") as Label;
                    Label hfCourseHistoryId = linkButton.NamingContainer.FindControl("hfCourseHistoryId1") as Label;
                    Label hfExamId = linkButton.NamingContainer.FindControl("hfExamId1") as Label;
                    DropDownList ddlStatus = linkButton.NamingContainer.FindControl("ddlStatus1") as DropDownList;
                    TextBox txtMark = new TextBox();
                    txtMark.Text = "0";
                    TextBox txtComment = (TextBox)linkButton.NamingContainer.FindControl("txtComment");

                    //if (ddlStatus.SelectedItem.Text.ToLower() == "grade")
                    //{
                    //    bool checkFlag = GradeValidation(txtMark.Text.ToUpper().Replace(" ", ""));
                    //    if (!checkFlag)
                    //    {
                    //        lblMsg.Text = "Please input correct grade.";
                    //        return;
                    //    }
                    //}

                    GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark, txtComment);

                    LoadResult_Click(null, null);

                    #endregion
                }
                else
                {
                    AcademicCalenderSection acaCalSec = AcademicCalenderSectionManager.GetById(Convert.ToInt32(ddlAcaCalSection.SelectedValue));
                    if (acaCalSec != null)
                    {
                        List<Exam> examList = ExamManager.GetAllByExamTemplateId(acaCalSec.BasicExamTemplateId);
                        if (examList.Count > 0 && examList != null)
                        {
                            examList = examList.OrderBy(x => x.Marks).ToList();
                            TextBox txtComment = (TextBox)linkButton.NamingContainer.FindControl("txtComment");
                            if (examList.Count == 3)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    Label hfId = linkButton.NamingContainer.FindControl("hfId" + (i + 1) + "") as Label;
                                    Label hfCourseHistoryId = linkButton.NamingContainer.FindControl("hfCourseHistoryId" + (i + 1) + "") as Label;
                                    Label hfExamId = linkButton.NamingContainer.FindControl("hfExamId" + (i + 1) + "") as Label;
                                    DropDownList ddlStatus = linkButton.NamingContainer.FindControl("ddlStatus" + (i + 1) + "") as DropDownList;
                                    TextBox txtMark = new TextBox();
                                    txtMark.Text = "0";

                                    GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark, txtComment);
                                }
                                LoadResult_Click(null, null);
                            }
                            else if (examList.Count == 4)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    Label hfId = linkButton.NamingContainer.FindControl("hfId" + (i + 1) + "") as Label;
                                    Label hfCourseHistoryId = linkButton.NamingContainer.FindControl("hfCourseHistoryId" + (i + 1) + "") as Label;
                                    Label hfExamId = linkButton.NamingContainer.FindControl("hfExamId" + (i + 1) + "") as Label;
                                    DropDownList ddlStatus = linkButton.NamingContainer.FindControl("ddlStatus" + (i + 1) + "") as DropDownList;
                                    TextBox txtMark = new TextBox();
                                    txtMark.Text = "0";

                                    GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark, txtComment);
                                }
                                LoadResult_Click(null, null);
                            }
                            else if (examList.Count == 5)
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    Label hfId = linkButton.NamingContainer.FindControl("hfId" + (i + 1) + "") as Label;
                                    Label hfCourseHistoryId = linkButton.NamingContainer.FindControl("hfCourseHistoryId" + (i + 1) + "") as Label;
                                    Label hfExamId = linkButton.NamingContainer.FindControl("hfExamId" + (i + 1) + "") as Label;
                                    DropDownList ddlStatus = linkButton.NamingContainer.FindControl("ddlStatus" + (i + 1) + "") as DropDownList;
                                    TextBox txtMark = new TextBox();
                                    txtMark.Text = "0";
                                    GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark, txtComment);
                                }
                                LoadResult_Click(null, null);
                            }
                            else if (examList.Count == 6)
                            {
                                for (int i = 0; i < 6; i++)
                                {
                                    Label hfId = linkButton.NamingContainer.FindControl("hfId" + (i + 1) + "") as Label;
                                    Label hfCourseHistoryId = linkButton.NamingContainer.FindControl("hfCourseHistoryId" + (i + 1) + "") as Label;
                                    Label hfExamId = linkButton.NamingContainer.FindControl("hfExamId" + (i + 1) + "") as Label;
                                    DropDownList ddlStatus = linkButton.NamingContainer.FindControl("ddlStatus" + (i + 1) + "") as DropDownList;
                                    TextBox txtMark = new TextBox();
                                    txtMark.Text = "0";

                                    GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark, txtComment);
                                }
                                LoadResult_Click(null, null);
                            }
                            else if (examList.Count == 7)
                            {
                                for (int i = 0; i < 7; i++)
                                {
                                    Label hfId = linkButton.NamingContainer.FindControl("hfId" + (i + 1) + "") as Label;
                                    Label hfCourseHistoryId = linkButton.NamingContainer.FindControl("hfCourseHistoryId" + (i + 1) + "") as Label;
                                    Label hfExamId = linkButton.NamingContainer.FindControl("hfExamId" + (i + 1) + "") as Label;
                                    DropDownList ddlStatus = linkButton.NamingContainer.FindControl("ddlStatus" + (i + 1) + "") as DropDownList;
                                    TextBox txtMark = new TextBox();
                                    txtMark.Text = "0";

                                    GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark, txtComment);
                                }
                                LoadResult_Click(null, null);
                            }
                        }
                        //Exam List Check IF Condition End
                    }
                    //AcaCalSec Check IF Condition End
                }
            }
            catch { }
            finally { }
        }

        protected void SubmitAllStudentMark_Click(Object sender, EventArgs e)
        {
            try
            {
                if (ddlExam.SelectedValue == "-1")
                {
                    lblMsg.Text = "Grade Sheet Templete is not assign for this COURSE SECTION";
                }
                else
                {
                    foreach (GridViewRow row in gvExamResultSubmit.Rows)
                    {
                        #region Condition Check

                        if (ddlExam.SelectedValue != "0")
                        {
                            Label hfId = (Label)row.FindControl("hfId1");
                            Label hfCourseHistoryId = (Label)row.FindControl("hfCourseHistoryId1");
                            Label hfExamId = (Label)row.FindControl("hfExamId1");
                            DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus1");
                            TextBox txtMark = new TextBox();
                            TextBox txtComment = (TextBox)row.FindControl("txtComment");
                            if (ddlStatus.SelectedItem.Text.ToLower() == "grade")
                            {
                                bool checkFlag = GradeValidation(txtMark.Text.ToUpper().Replace(" ", ""));
                                if (!checkFlag)
                                {
                                    lblMsg.Text = "Please input correct grade.";
                                    return;
                                }
                            }

                            GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark, txtComment);

                            LoadResult_Click(null, null);
                        }
                        else
                        {
                            AcademicCalenderSection acaCalSec = AcademicCalenderSectionManager.GetById(Convert.ToInt32(ddlAcaCalSection.SelectedValue));
                            if (acaCalSec != null)
                            {
                                #region Exam Wise

                                List<Exam> examList = ExamManager.GetAllByExamTemplateId(acaCalSec.BasicExamTemplateId);
                                if (examList.Count > 0 && examList != null)
                                {
                                    examList = examList.OrderBy(x => x.Marks).ToList();
                                    TextBox txtComment = (TextBox)row.FindControl("txtComment");
                                    if (examList.Count == 3)
                                    {
                                        for (int i = 0; i < 3; i++)
                                        {
                                            Label hfId = (Label)row.FindControl("hfId" + (i + 1) + "");
                                            Label hfCourseHistoryId = (Label)row.FindControl("hfCourseHistoryId" + (i + 1) + "");
                                            Label hfExamId = (Label)row.FindControl("hfExamId" + (i + 1) + "");
                                            DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus" + (i + 1) + "");
                                            TextBox txtMark = new TextBox();
                                            txtMark.Text = "0";

                                            GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark, txtComment);
                                        }
                                        LoadResult_Click(null, null);
                                    }
                                    else if (examList.Count == 4)
                                    {
                                        for (int i = 0; i < 4; i++)
                                        {
                                            Label hfId = (Label)row.FindControl("hfId" + (i + 1) + "");
                                            Label hfCourseHistoryId = (Label)row.FindControl("hfCourseHistoryId" + (i + 1) + "");
                                            Label hfExamId = (Label)row.FindControl("hfExamId" + (i + 1) + "");
                                            DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus" + (i + 1) + "");
                                            TextBox txtMark = new TextBox();
                                            txtMark.Text = "0";

                                            GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark, txtComment);
                                        }
                                        LoadResult_Click(null, null);
                                    }
                                    else if (examList.Count == 5)
                                    {
                                        for (int i = 0; i < 5; i++)
                                        {
                                            Label hfId = (Label)row.FindControl("hfId" + (i + 1) + "");
                                            Label hfCourseHistoryId = (Label)row.FindControl("hfCourseHistoryId" + (i + 1) + "");
                                            Label hfExamId = (Label)row.FindControl("hfExamId" + (i + 1) + "");
                                            DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus" + (i + 1) + "");
                                            TextBox txtMark = new TextBox();
                                            txtMark.Text = "0";

                                            GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark, txtComment);
                                        }
                                        LoadResult_Click(null, null);
                                    }
                                    else if (examList.Count == 6)
                                    {
                                        for (int i = 0; i < 6; i++)
                                        {
                                            Label hfId = (Label)row.FindControl("hfId" + (i + 1) + "");
                                            Label hfCourseHistoryId = (Label)row.FindControl("hfCourseHistoryId" + (i + 1) + "");
                                            Label hfExamId = (Label)row.FindControl("hfExamId" + (i + 1) + "");
                                            DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus" + (i + 1) + "");
                                            TextBox txtMark = new TextBox();
                                            txtMark.Text = "0";

                                            GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark, txtComment);
                                        }
                                        LoadResult_Click(null, null);
                                    }
                                    else if (examList.Count == 7)
                                    {
                                        for (int i = 0; i < 7; i++)
                                        {
                                            Label hfId = (Label)row.FindControl("hfId" + (i + 1) + "");
                                            Label hfCourseHistoryId = (Label)row.FindControl("hfCourseHistoryId" + (i + 1) + "");
                                            Label hfExamId = (Label)row.FindControl("hfExamId" + (i + 1) + "");
                                            DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus" + (i + 1) + "");
                                            TextBox txtMark = new TextBox();
                                            txtMark.Text = "0";

                                            GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark, txtComment);

                                        }
                                        LoadResult_Click(null, null);
                                    }
                                }

                                #endregion
                            }

                        }
                        #endregion
                    }

                    #region Log Insert

                    LogGeneralManager.Insert(
                                DateTime.Now,
                                ddlAcademicCalender.SelectedValue,
                                ddlAcademicCalender.SelectedItem.Text,
                                userObj.LogInID,
                                "",
                                "",
                                "Save All Student Exam Status",
                                userObj.LogInID + " Save All Student Exam Status " + ddlProgram.SelectedItem.Text + ", " + ddlAcaCalSection.SelectedItem.Text + ", " + ddlExam.SelectedItem.Text,
                                userObj.LogInID + " Save All Student Exam Status ",
                                ((int)CommonEnum.PageName.ExamStatusEntry).ToString(),
                                CommonEnum.PageName.ExamStatusEntry.ToString(),
                                _pageUrl,
                                "");
                    #endregion

                    LoadResult_Click(null, null);
                }
                //this is end of foreach

                //lblMsg.Text = "Total " + saveCount + " Saved and " + updateCount + " Updated Successfully";
                
            }
            catch { }
            finally { }
        }
        
        protected void LoadResult_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlExam.Items.Count == 1)
                {
                    //btnFinalSubmitTop.Visible = true;
                    //btnFinalSubmitBottom.Visible = true;
                }
                else if (ddlExam.SelectedItem.Text != "All")
                {
                    //btnFinalSubmitTop.Visible = false;
                    //btnFinalSubmitBottom.Visible = false;
                }
                else
                {
                    //btnFinalSubmitTop.Visible = true;
                    //btnFinalSubmitBottom.Visible = true;
                }


                int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
                int acaCalSecId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);
                int examId = Convert.ToInt32(ddlExam.SelectedValue);
                if (acaCalId == 0 || acaCalSecId == 0)
                {
                    lblMsg.Text = "Please select Academic Calender and Course dropdown correctly";
                    return;
                }
                else if (ddlExam.SelectedValue == "-1")
                {
                    lblMsg.Text = "Grade Sheet Templete is not assign for this COURSE SECTION";
                    return;
                }

                if (examId == 0)
                {
                    AcademicCalenderSection acaCalSec = AcademicCalenderSectionManager.GetById(acaCalSecId);
                    if (acaCalSec != null)
                    {
                        List<Exam> examList = ExamManager.GetAllByExamTemplateId(acaCalSec.BasicExamTemplateId);
                        if (examList.Count > 0 && examList != null)
                        {
                            examList = examList.OrderBy(x => x.Marks).ToList();

                            #region Load and Assign Exam Mark
                            List<ExamMarkAllDTO> examMarkAllDTOList = new List<ExamMarkAllDTO>();

                            int finalExamFlag = -1;
                            for (int i = 0; i < examList.Count; i++)
                            {
                                List<ExamMarkDTO> tempExamMarkList = ExamMarkManager.GetAllStudentByAcaCalAcaCalSecExam(acaCalId, acaCalSecId, examList[i].ExamId);
                                if (tempExamMarkList.Count > 0 && tempExamMarkList != null)
                                    tempExamMarkList = tempExamMarkList.Where(x => x.IsFinalSubmit != true && x.IsTransfer != true).ToList();
                                if (tempExamMarkList.Count > 0 && tempExamMarkList != null)
                                {
                                    

                                    tempExamMarkList = tempExamMarkList.OrderBy(x => x.Roll).ToList();
                                    if (i == 0)
                                    {
                                        foreach (ExamMarkDTO temp in tempExamMarkList)
                                        {
                                            ExamMarkAllDTO examMarkAllDTO = new ExamMarkAllDTO();

                                            if (examList[i].ExamName.Contains("Final"))
                                                examMarkAllDTO.forIGrade = examList[i].ExamName.Contains("Final") == true ? (temp.Status == 18 ? 1 : 0) : 0;

                                            examMarkAllDTO.Id1 = temp.Id;
                                            examMarkAllDTO.CourseHistoryId1 = temp.CourseHistoryId;
                                            examMarkAllDTO.ExamId1 = examList[i].ExamId;
                                            examMarkAllDTO.Mark1 = temp.Mark;
                                            examMarkAllDTO.Status1 = temp.Status;
                                            examMarkAllDTO.IsFinalSubmit1 = temp.IsFinalSubmit;
                                            examMarkAllDTO.IsTransfer1 = temp.IsTransfer;

                                            examMarkAllDTO.Roll = temp.Roll;
                                            examMarkAllDTO.StudentId = temp.StudentId;
                                            examMarkAllDTO.FullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(temp.FullName.ToString().ToLower());
                                            examMarkAllDTO.GradeMasterId = temp.GradeMasterId;

                                            examMarkAllDTO.Mark2 = "";
                                            examMarkAllDTO.Mark3 = "";
                                            examMarkAllDTO.Mark4 = "";
                                            examMarkAllDTO.Mark5 = "";
                                            examMarkAllDTO.Mark6 = "";
                                            examMarkAllDTO.Mark7 = "";

                                            examMarkAllDTOList.Add(examMarkAllDTO);
                                        }
                                    }
                                    else if (i == 1)
                                    {
                                        for (int j = 0; j < tempExamMarkList.Count; j++)
                                        {
                                            if (examList[i].ExamName.Contains("Final"))
                                                examMarkAllDTOList[j].forIGrade = examList[i].ExamName.Contains("Final") == true ? (tempExamMarkList[j].Status == 18 ? 1 : 0) : 0;

                                            examMarkAllDTOList[j].Id2 = tempExamMarkList[j].Id;
                                            examMarkAllDTOList[j].CourseHistoryId2 = tempExamMarkList[j].CourseHistoryId;
                                            examMarkAllDTOList[j].ExamId2 = examList[i].ExamId;
                                            examMarkAllDTOList[j].Mark2 = tempExamMarkList[j].Mark;
                                            examMarkAllDTOList[j].Status2 = tempExamMarkList[j].Status;
                                            examMarkAllDTOList[j].IsFinalSubmit2 = tempExamMarkList[j].IsFinalSubmit;
                                            examMarkAllDTOList[j].IsTransfer2 = tempExamMarkList[j].IsTransfer;
                                        }
                                    }
                                    else if (i == 2)
                                    {
                                        for (int j = 0; j < tempExamMarkList.Count; j++)
                                        {
                                            if (examList[i].ExamName.Contains("Final"))
                                                examMarkAllDTOList[j].forIGrade = examList[i].ExamName.Contains("Final") == true ? (tempExamMarkList[j].Status == 18 ? 1 : 0) : 0;

                                            examMarkAllDTOList[j].Id3 = tempExamMarkList[j].Id;
                                            examMarkAllDTOList[j].CourseHistoryId3 = tempExamMarkList[j].CourseHistoryId;
                                            examMarkAllDTOList[j].ExamId3 = examList[i].ExamId;
                                            examMarkAllDTOList[j].Mark3 = tempExamMarkList[j].Mark;
                                            examMarkAllDTOList[j].Status3 = tempExamMarkList[j].Status;
                                            examMarkAllDTOList[j].IsFinalSubmit3 = tempExamMarkList[j].IsFinalSubmit;
                                            examMarkAllDTOList[j].IsTransfer3 = tempExamMarkList[j].IsTransfer;
                                        }
                                    }
                                    else if (i == 3)
                                    {
                                        for (int j = 0; j < tempExamMarkList.Count; j++)
                                        {
                                            if (examList[i].ExamName.Contains("Final"))
                                                examMarkAllDTOList[j].forIGrade = examList[i].ExamName.Contains("Final") == true ? (tempExamMarkList[j].Status == 18 ? 1 : 0) : 0;

                                            examMarkAllDTOList[j].Id4 = tempExamMarkList[j].Id;
                                            examMarkAllDTOList[j].CourseHistoryId4 = tempExamMarkList[j].CourseHistoryId;
                                            examMarkAllDTOList[j].ExamId4 = examList[i].ExamId;
                                            examMarkAllDTOList[j].Mark4 = tempExamMarkList[j].Mark;
                                            examMarkAllDTOList[j].Status4 = tempExamMarkList[j].Status;
                                            examMarkAllDTOList[j].IsFinalSubmit4 = tempExamMarkList[j].IsFinalSubmit;
                                            examMarkAllDTOList[j].IsTransfer4 = tempExamMarkList[j].IsTransfer;
                                        }
                                    }
                                    else if (i == 4)
                                    {
                                        for (int j = 0; j < tempExamMarkList.Count; j++)
                                        {
                                            if (examList[i].ExamName.Contains("Final"))
                                                examMarkAllDTOList[j].forIGrade = examList[i].ExamName.Contains("Final") == true ? (tempExamMarkList[j].Status == 18 ? 1 : 0) : 0;

                                            examMarkAllDTOList[j].Id5 = tempExamMarkList[j].Id;
                                            examMarkAllDTOList[j].CourseHistoryId5 = tempExamMarkList[j].CourseHistoryId;
                                            examMarkAllDTOList[j].ExamId5 = examList[i].ExamId;
                                            examMarkAllDTOList[j].Mark5 = tempExamMarkList[j].Mark;
                                            examMarkAllDTOList[j].Status5 = tempExamMarkList[j].Status;
                                            examMarkAllDTOList[j].IsFinalSubmit5 = tempExamMarkList[j].IsFinalSubmit;
                                            examMarkAllDTOList[j].IsTransfer5 = tempExamMarkList[j].IsTransfer;
                                        }
                                    }
                                    else if (i == 5)
                                    {
                                        for (int j = 0; j < tempExamMarkList.Count; j++)
                                        {
                                            if (examList[i].ExamName.Contains("Final"))
                                                examMarkAllDTOList[j].forIGrade = examList[i].ExamName.Contains("Final") == true ? (tempExamMarkList[j].Status == 18 ? 1 : 0) : 0;

                                            examMarkAllDTOList[j].Id6 = tempExamMarkList[j].Id;
                                            examMarkAllDTOList[j].CourseHistoryId6 = tempExamMarkList[j].CourseHistoryId;
                                            examMarkAllDTOList[j].ExamId6 = examList[i].ExamId;
                                            examMarkAllDTOList[j].Mark6 = tempExamMarkList[j].Mark;
                                            examMarkAllDTOList[j].Status6 = tempExamMarkList[j].Status;
                                            examMarkAllDTOList[j].IsFinalSubmit6 = tempExamMarkList[j].IsFinalSubmit;
                                            examMarkAllDTOList[j].IsTransfer6 = tempExamMarkList[j].IsTransfer;
                                        }
                                    }
                                    else if (i == 6)
                                    {
                                        for (int j = 0; j < tempExamMarkList.Count; j++)
                                        {
                                            if (examList[i].ExamName.Contains("Final"))
                                                examMarkAllDTOList[j].forIGrade = examList[i].ExamName.Contains("Final") == true ? (tempExamMarkList[j].Status == 18 ? 1 : 0) : 0;

                                            examMarkAllDTOList[j].Id7 = tempExamMarkList[j].Id;
                                            examMarkAllDTOList[j].CourseHistoryId7 = tempExamMarkList[j].CourseHistoryId;
                                            examMarkAllDTOList[j].ExamId7 = examList[i].ExamId;
                                            examMarkAllDTOList[j].Mark7 = tempExamMarkList[j].Mark;
                                            examMarkAllDTOList[j].Status7 = tempExamMarkList[j].Status;
                                            examMarkAllDTOList[j].IsFinalSubmit7 = tempExamMarkList[j].IsFinalSubmit;
                                            examMarkAllDTOList[j].IsTransfer7 = tempExamMarkList[j].IsTransfer;
                                        }
                                    }
                                }
                            }
                            //examList check If condition End
                            #endregion

                            

                            for (int i = 0; i < examList.Count; i++)
                            {
                                int maxMarkAll = 0;
                                if (examList[i] != null)
                                    maxMarkAll = examList[i].Marks;

                                if (maxMarkAll != 0)
                                    gvExamResultSubmit.Columns[3 + i ].HeaderText = examList[i].ExamName + "(" + maxMarkAll + ")";
                                else
                                    gvExamResultSubmit.Columns[3 + i ].HeaderText = examList[i].ExamName;
                            }

                            pnSubmitStudentMarkTop.Visible = true;
                            pnSubmitStudentMarkButtom.Visible = true;                            
                            gvExamResultSubmit.Visible = true;

                            if (examMarkAllDTOList.Count == 0)
                            {
                                pnSubmitStudentMarkTop.Visible = false;
                                pnSubmitStudentMarkButtom.Visible = false;
                            }
                            gvExamResultSubmit.DataSource = examMarkAllDTOList;
                            gvExamResultSubmit.DataBind();

                            if (examList.Count == 3)
                            {
                                gvExamResultSubmit.Columns[3].Visible = true;
                                gvExamResultSubmit.Columns[4].Visible = true;
                                gvExamResultSubmit.Columns[5].Visible = true;
                                gvExamResultSubmit.Columns[6].Visible = false;
                                gvExamResultSubmit.Columns[7].Visible = false;
                                gvExamResultSubmit.Columns[8].Visible = false;
                                gvExamResultSubmit.Columns[9].Visible = false;

                            }
                            else if (examList.Count == 4)
                            {
                                gvExamResultSubmit.Columns[3].Visible = true;
                                gvExamResultSubmit.Columns[4].Visible = true;
                                gvExamResultSubmit.Columns[5].Visible = true;
                                gvExamResultSubmit.Columns[6].Visible = true;
                                gvExamResultSubmit.Columns[7].Visible = false;
                                gvExamResultSubmit.Columns[8].Visible = false;
                                gvExamResultSubmit.Columns[9].Visible = false;
 
                            }
                            else if (examList.Count == 5)
                            {
                                gvExamResultSubmit.Columns[3].Visible = true;
                                gvExamResultSubmit.Columns[4].Visible = true;
                                gvExamResultSubmit.Columns[5].Visible = true;
                                gvExamResultSubmit.Columns[6].Visible = true;
                                gvExamResultSubmit.Columns[7].Visible = true;
                                gvExamResultSubmit.Columns[8].Visible = false;
                                gvExamResultSubmit.Columns[9].Visible = false;

                            }
                            else if (examList.Count == 6)
                            {
                                gvExamResultSubmit.Columns[3].Visible = true;
                                gvExamResultSubmit.Columns[4].Visible = true;
                                gvExamResultSubmit.Columns[5].Visible = true;
                                gvExamResultSubmit.Columns[6].Visible = true;
                                gvExamResultSubmit.Columns[7].Visible = true;
                                gvExamResultSubmit.Columns[8].Visible = true;
                                gvExamResultSubmit.Columns[9].Visible = false;

                            }
                            else if (examList.Count == 7)
                            {
                                gvExamResultSubmit.Columns[3].Visible = true;
                                gvExamResultSubmit.Columns[4].Visible = true;
                                gvExamResultSubmit.Columns[5].Visible = true;
                                gvExamResultSubmit.Columns[6].Visible = true;
                                gvExamResultSubmit.Columns[7].Visible = true;
                                gvExamResultSubmit.Columns[8].Visible = true;
                                gvExamResultSubmit.Columns[9].Visible = true;

                            }

                            
                            gvExamResultSubmit.Columns[10].Visible = false;
                            gvExamResultSubmit.Columns[11].Visible = false;
                            gvExamResultSubmit.Columns[12].Visible = false;
                            gvExamResultSubmit.Columns[13].Visible = true;

                        }
                        //main IF condition End
                    }
                    //AcaCalSec Check IF conditon End
                }
                else
                {
                    List<ExamMarkAllDTO> examMarkAllDTOList = new List<ExamMarkAllDTO>();

                    Exam exam = ExamManager.GetById(examId);
                    int maxMark = 0;
                    if (exam != null)
                        maxMark = exam.Marks;

                    List<ExamMarkDTO> examMarkList = ExamMarkManager.GetAllStudentByAcaCalAcaCalSecExam(acaCalId, acaCalSecId, examId);
                    if (examMarkList.Count > 0 && examMarkList != null)
                        examMarkList = examMarkList.Where(x => x.IsFinalSubmit != true && x.IsTransfer != true).ToList();

                    if (examMarkList.Count > 0 && examMarkList != null)
                    {
                        if (maxMark != 0)
                        {
                            ExamTemplate newExamTemplate = ExamTemplateManager.GetById(AcademicCalenderSectionManager.GetById(acaCalSecId).BasicExamTemplateId);
                            if (newExamTemplate.TemplateName == "Grade Only")
                            {
                                gvExamResultSubmit.Columns[3].HeaderText = "Grade";
                            }
                            else { gvExamResultSubmit.Columns[3].HeaderText = exam.ExamName + "(" + maxMark + ")"; }
                        }
                        else
                            gvExamResultSubmit.Columns[3].HeaderText = "Mark";

                        foreach (ExamMarkDTO temp in examMarkList)
                        {
                            ExamMarkAllDTO examMarkAll = new ExamMarkAllDTO();

                            ExamMarkAllDTO examMarkAllDTO = new ExamMarkAllDTO();
                            examMarkAllDTO.Id1 = temp.Id;
                            examMarkAllDTO.CourseHistoryId1 = temp.CourseHistoryId;
                            examMarkAllDTO.ExamId1 = temp.ExamId;
                            examMarkAllDTO.Mark1 = temp.Mark;
                            examMarkAllDTO.Status1 = temp.Status;
                            examMarkAllDTO.IsFinalSubmit1 = temp.IsFinalSubmit;
                            examMarkAllDTO.IsTransfer1 = temp.IsTransfer;

                            examMarkAllDTO.Roll = temp.Roll;
                            examMarkAllDTO.StudentId = temp.StudentId;
                            examMarkAllDTO.FullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(temp.FullName.ToString().ToLower());
                            examMarkAllDTO.GradeMasterId = temp.GradeMasterId;

                            examMarkAllDTOList.Add(examMarkAllDTO);
                        }

                        pnSubmitStudentMarkTop.Visible = true;
                        pnSubmitStudentMarkButtom.Visible = true;
                        gvExamResultSubmit.Visible = true;

                        if (examMarkAllDTOList.Count == 0)
                        {
                            pnSubmitStudentMarkTop.Visible = false;
                            pnSubmitStudentMarkButtom.Visible = false;
                        }
                        gvExamResultSubmit.DataSource = examMarkAllDTOList;
                        gvExamResultSubmit.DataBind();

                        gvExamResultSubmit.Columns[3].Visible = true;
                        gvExamResultSubmit.Columns[4].Visible = false;
                        gvExamResultSubmit.Columns[5].Visible = false;
                        gvExamResultSubmit.Columns[6].Visible = false;
                        gvExamResultSubmit.Columns[7].Visible = false;
                        gvExamResultSubmit.Columns[8].Visible = false;
                        gvExamResultSubmit.Columns[9].Visible = false;
                        gvExamResultSubmit.Columns[10].Visible = false;
                        gvExamResultSubmit.Columns[11].Visible = false;
                        gvExamResultSubmit.Columns[12].Visible = false;
                        gvExamResultSubmit.Columns[13].Visible = true;
                    }
                    else
                    {
                        pnSubmitStudentMarkTop.Visible = false;
                        pnSubmitStudentMarkButtom.Visible = false;

                        gvExamResultSubmit.DataSource = null;
                        gvExamResultSubmit.DataBind();
                    }
                }
                GridRebind();
            }
            catch { pnSubmitStudentMarkTop.Visible = false; pnSubmitStudentMarkButtom.Visible = false; }
            finally { }
        }
        
        protected void Search_Click(Object sender, EventArgs e)
        {
            try
            {
                LoadAcaCalSection(Convert.ToInt32(ddlAcademicCalender.SelectedValue));

                pnSubmitStudentMarkTop.Visible = false;
                pnSubmitStudentMarkButtom.Visible = false;
                gvExamResultSubmit.Visible = false;

                ddlExam.Items.Clear();
                ddlExam.Items.Add(new ListItem("Select", "-1"));
            }
            catch { }
            finally { }
        }

        #endregion

        #region Static methods

        private void ShowMessage(string msg)
        {
            //pnlMessage.Visible = true;

            lblMsg.Text = msg;
            lblMsg.ForeColor = Color.Red;
        }

        private void ShowAlertMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerControlScript", "alert('" + msg + "');", true);
        }
        #endregion Static methods
    }
}