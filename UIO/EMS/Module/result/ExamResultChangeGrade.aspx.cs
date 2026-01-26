using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.result
{
    public partial class ExamResultChangeGrade : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

        #region Function

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
                ddlExam.Enabled = false;
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
                ddlStatus.Items.Clear();
                //ddlStatus.Items.Add(new ListItem("Select", "0"));
                //ddlStatus.AppendDataBoundItems = true;

                List<ValueSet> valueSetList = ValueSetManager.GetAll();
                if (valueSetList.Count > 0 && valueSetList != null)
                {
                    valueSetList = valueSetList.Where(x => x.ValueSetName.Contains("ExamMarkStatus")).ToList();
                    if (valueSetList.Count > 0 && valueSetList != null)
                    {
                        List<Value> valueList = ValueManager.GetByValueSetId(valueSetList[0].ValueSetID);
                        if (valueList.Count > 0 && valueList != null)
                        {
                            if (ddlExam.Items.Count == 1)
                                valueList = valueList.Where(x => x.ValueName != "Mark").OrderByDescending(x => x.ValueID).ToList();
                            else
                                valueList = valueList.Where(x => x.ValueName != "Grade" && x.ValueName != "Reported").OrderBy(x => x.ValueID).ToList();

                            ddlStatus.DataSource = valueList;
                            ddlStatus.DataValueField = "ValueID";
                            ddlStatus.DataTextField = "ValueName";
                            ddlStatus.DataBind();
                        }
                    }
                }
            }
            catch { }
            finally { }
        }

        protected int Insert(int courseHistory, int examId, string mark, int status, bool isFinalSubmit, int userId)
        {
            try
            {
                ExamMark examMark = new ExamMark();
                examMark.CourseHistoryId = courseHistory;
                examMark.ExamId = examId;
                examMark.Mark = mark;
                examMark.Status = status;
                examMark.IsFinalSubmit = isFinalSubmit;
                examMark.CreateBy = userId;
                examMark.CreatedDate = DateTime.Now;

                int resultInsert = ExamMarkManager.Insert(examMark);

                return resultInsert;
            }
            catch { return 0; }
            finally { }
        }

        protected bool Update(int id, int courseHistory, int examId, string mark, int status, bool isFinalSubmit, int userId)
        {
            try
            {
                ExamMark examMark = ExamMarkManager.GetById(id);

                examMark.CourseHistoryId = courseHistory;
                examMark.ExamId = examId;
                examMark.Mark = mark;
                examMark.Status = status;
                examMark.IsFinalSubmit = isFinalSubmit;
                examMark.ModifiedBy = userId;
                examMark.ModifiedDate = DateTime.Now;

                bool resultUpdate = ExamMarkManager.Update(examMark);

                return resultUpdate;
            }
            catch { return false; }
            finally { }
        }

        protected void GetDataForInsertUpdate(HiddenField hfId, HiddenField hfCourseHistoryId, HiddenField hfExamId, DropDownList ddlStatus, TextBox txtMark)
        {
            try
            {
                int userId = 99;
                string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
                User user = UserManager.GetByLogInId(loginID);
                if (user != null)
                    userId = user.User_ID;

                int id = Convert.ToInt32(hfId.Value);
                int courseHistoryId = Convert.ToInt32(hfCourseHistoryId.Value);
                int examId = Convert.ToInt32(hfExamId.Value) == 0 ? Convert.ToInt32(ddlExam.SelectedValue) : Convert.ToInt32(hfExamId.Value);
                int status = Convert.ToInt32(ddlStatus.SelectedValue);
                string mark = ddlStatus.SelectedItem.Text == "Absent" ? "" : txtMark.Text;
                bool isFinalSubmit = true;

                if (id == 0)
                {
                    int resultInsert = Insert(courseHistoryId, examId, mark, status, isFinalSubmit, userId);
                    if (resultInsert > 0)
                    {
                        lblMsg.Text = "Save Successfully";
                    }

                }
                else
                {
                    bool resultUpdate = Update(id, courseHistoryId, examId, mark, status, isFinalSubmit, userId);
                    if (resultUpdate)
                    {
                        lblMsg.Text = "Update Successfully";
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

        protected void Status1_Changed(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlStatus = (DropDownList)sender;
                TextBox txtMark = ddlStatus.NamingContainer.FindControl("txtMark1") as TextBox;

                if (ddlStatus.SelectedItem.Text == "Mark")
                {
                    txtMark.Enabled = true;
                    txtMark.Text = "0.00";
                }
                else if (ddlStatus.SelectedItem.Text == "Grade")
                {
                    txtMark.Enabled = true;
                    txtMark.Text = "";
                }
                else
                {
                    txtMark.Enabled = false;
                    txtMark.Text = "ab";
                }
            }
            catch { }
            finally { }
        }

        protected void Status2_Changed(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlStatus = (DropDownList)sender;
                TextBox txtMark = ddlStatus.NamingContainer.FindControl("txtMark2") as TextBox;

                if (ddlStatus.SelectedItem.Text == "Mark")
                {
                    txtMark.Enabled = true;
                    txtMark.Text = "0.00";
                }
                else
                {
                    txtMark.Enabled = false;
                    txtMark.Text = "ab";
                }
            }
            catch { }
            finally { }
        }

        protected void Status3_Changed(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlStatus = (DropDownList)sender;
                TextBox txtMark = ddlStatus.NamingContainer.FindControl("txtMark3") as TextBox;

                if (ddlStatus.SelectedItem.Text == "Mark")
                {
                    txtMark.Enabled = true;
                    txtMark.Text = "0.00";
                }
                else
                {
                    txtMark.Enabled = false;
                    txtMark.Text = "ab";
                }
            }
            catch { }
            finally { }
        }

        protected void Status4_Changed(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlStatus = (DropDownList)sender;
                TextBox txtMark = ddlStatus.NamingContainer.FindControl("txtMark4") as TextBox;

                if (ddlStatus.SelectedItem.Text == "Mark")
                {
                    txtMark.Enabled = true;
                    txtMark.Text = "0.00";
                }
                else
                {
                    txtMark.Enabled = false;
                    txtMark.Text = "ab";
                }
            }
            catch { }
            finally { }
        }

        protected void Status5_Changed(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlStatus = (DropDownList)sender;
                TextBox txtMark = ddlStatus.NamingContainer.FindControl("txtMark5") as TextBox;

                if (ddlStatus.SelectedItem.Text == "Mark")
                {
                    txtMark.Enabled = true;
                    txtMark.Text = "0.00";
                }
                else
                {
                    txtMark.Enabled = false;
                    txtMark.Text = "ab";
                }
            }
            catch { }
            finally { }
        }

        protected void Status6_Changed(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlStatus = (DropDownList)sender;
                TextBox txtMark = ddlStatus.NamingContainer.FindControl("txtMark6") as TextBox;

                if (ddlStatus.SelectedItem.Text == "Mark")
                {
                    txtMark.Enabled = true;
                    txtMark.Text = "0.00";
                }
                else
                {
                    txtMark.Enabled = false;
                    txtMark.Text = "ab";
                }
            }
            catch { }
            finally { }
        }

        protected void Status7_Changed(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlStatus = (DropDownList)sender;
                TextBox txtMark = ddlStatus.NamingContainer.FindControl("txtMark7") as TextBox;

                if (ddlStatus.SelectedItem.Text == "Mark")
                {
                    txtMark.Enabled = true;
                    txtMark.Text = "0.00";
                }
                else
                {
                    txtMark.Enabled = false;
                    txtMark.Text = "ab";
                }
            }
            catch { }
            finally { }
        }

        protected void gvExamResultSubmit_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            try
            {
                DropDownList ddlStatus1 = (DropDownList)e.Row.FindControl("ddlStatus1");
                LoadStatusCombo(ddlStatus1);
                HiddenField hdStatus1 = (HiddenField)e.Row.FindControl("hfStatus1");
                TextBox txtMark1 = (TextBox)e.Row.FindControl("txtMark1");
                int statusId1 = Convert.ToInt32(hdStatus1.Value);
                if (statusId1 == 0)
                {
                    if (ddlExam.Items.Count != 1)
                        ddlStatus1.Items.FindByText("Mark").Selected = true;
                    else
                        ddlStatus1.Items.FindByText("Grade").Selected = true;
                }
                else
                    ddlStatus1.SelectedValue = statusId1.ToString();

                if (ddlStatus1.SelectedItem.Text == "Absent")
                {
                    txtMark1.Text = "ab";
                    txtMark1.Enabled = false;
                }

                DropDownList ddlStatus2 = (DropDownList)e.Row.FindControl("ddlStatus2");
                LoadStatusCombo(ddlStatus2);
                HiddenField hdStatus2 = (HiddenField)e.Row.FindControl("hfStatus2");
                TextBox txtMark2 = (TextBox)e.Row.FindControl("txtMark2");
                int statusId2 = Convert.ToInt32(hdStatus2.Value);
                if (statusId2 == 0)
                    ddlStatus2.Items.FindByText("Mark").Selected = true;
                else
                    ddlStatus2.SelectedValue = statusId2.ToString();

                if (ddlStatus2.SelectedItem.Text != "Mark")
                {
                    txtMark2.Text = "ab";
                    txtMark2.Enabled = false;
                }

                DropDownList ddlStatus3 = (DropDownList)e.Row.FindControl("ddlStatus3");
                LoadStatusCombo(ddlStatus3);
                HiddenField hdStatus3 = (HiddenField)e.Row.FindControl("hfStatus3");
                TextBox txtMark3 = (TextBox)e.Row.FindControl("txtMark3");
                int statusId3 = Convert.ToInt32(hdStatus3.Value);
                if (statusId3 == 0)
                    ddlStatus3.Items.FindByText("Mark").Selected = true;
                else
                    ddlStatus3.SelectedValue = statusId3.ToString();

                if (ddlStatus3.SelectedItem.Text != "Mark")
                {
                    txtMark3.Text = "ab";
                    txtMark3.Enabled = false;
                }

                DropDownList ddlStatus4 = (DropDownList)e.Row.FindControl("ddlStatus4");
                LoadStatusCombo(ddlStatus4);
                HiddenField hdStatus4 = (HiddenField)e.Row.FindControl("hfStatus4");
                TextBox txtMark4 = (TextBox)e.Row.FindControl("txtMark4");
                int statusId4 = Convert.ToInt32(hdStatus4.Value);
                if (statusId4 == 0)
                    ddlStatus4.Items.FindByText("Mark").Selected = true;
                else
                    ddlStatus4.SelectedValue = statusId4.ToString();

                if (ddlStatus4.SelectedItem.Text != "Mark")
                {
                    txtMark4.Text = "ab";
                    txtMark4.Enabled = false;
                }

                DropDownList ddlStatus5 = (DropDownList)e.Row.FindControl("ddlStatus5");
                LoadStatusCombo(ddlStatus5);
                HiddenField hdStatus5 = (HiddenField)e.Row.FindControl("hfStatus5");
                TextBox txtMark5 = (TextBox)e.Row.FindControl("txtMark5");
                int statusId5 = Convert.ToInt32(hdStatus5.Value);
                if (statusId5 == 0)
                    ddlStatus5.Items.FindByText("Mark").Selected = true;
                else
                    ddlStatus5.SelectedValue = statusId5.ToString();

                if (ddlStatus5.SelectedItem.Text != "Mark")
                {
                    txtMark5.Text = "ab";
                    txtMark5.Enabled = false;
                }

                DropDownList ddlStatus6 = (DropDownList)e.Row.FindControl("ddlStatus6");
                LoadStatusCombo(ddlStatus6);
                HiddenField hdStatus6 = (HiddenField)e.Row.FindControl("hfStatus6");
                TextBox txtMark6 = (TextBox)e.Row.FindControl("txtMark6");
                int statusId6 = Convert.ToInt32(hdStatus6.Value);
                if (statusId6 == 0)
                    ddlStatus6.Items.FindByText("Mark").Selected = true;
                else
                    ddlStatus6.SelectedValue = statusId6.ToString();

                if (ddlStatus6.SelectedItem.Text != "Mark")
                {
                    txtMark6.Text = "ab";
                    txtMark6.Enabled = false;
                }

                DropDownList ddlStatus7 = (DropDownList)e.Row.FindControl("ddlStatus7");
                LoadStatusCombo(ddlStatus7);
                HiddenField hdStatus7 = (HiddenField)e.Row.FindControl("hfStatus7");
                TextBox txtMark7 = (TextBox)e.Row.FindControl("txtMark7");
                int statusId7 = Convert.ToInt32(hdStatus7.Value);
                if (statusId7 == 0)
                    ddlStatus7.Items.FindByText("Mark").Selected = true;
                else
                    ddlStatus7.SelectedValue = statusId7.ToString();

                if (ddlStatus7.SelectedItem.Text != "Mark")
                {
                    txtMark7.Text = "ab";
                    txtMark7.Enabled = false;
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
                    #region Single Exam Select

                    HiddenField hfId = linkButton.NamingContainer.FindControl("hfId1") as HiddenField;
                    HiddenField hfCourseHistoryId = linkButton.NamingContainer.FindControl("hfCourseHistoryId1") as HiddenField;
                    HiddenField hfExamId = linkButton.NamingContainer.FindControl("hfExamId1") as HiddenField;
                    DropDownList ddlStatus = linkButton.NamingContainer.FindControl("ddlStatus1") as DropDownList;
                    TextBox txtMark = linkButton.NamingContainer.FindControl("txtMark1") as TextBox;

                    GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark);

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
                            if (examList.Count == 3)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    HiddenField hfId = linkButton.NamingContainer.FindControl("hfId" + (i + 1) + "") as HiddenField;
                                    HiddenField hfCourseHistoryId = linkButton.NamingContainer.FindControl("hfCourseHistoryId" + (i + 1) + "") as HiddenField;
                                    HiddenField hfExamId = linkButton.NamingContainer.FindControl("hfExamId" + (i + 1) + "") as HiddenField;
                                    DropDownList ddlStatus = linkButton.NamingContainer.FindControl("ddlStatus" + (i + 1) + "") as DropDownList;
                                    TextBox txtMark = linkButton.NamingContainer.FindControl("txtMark" + (i + 1) + "") as TextBox;

                                    GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark);
                                }
                                LoadResult_Click(null, null);
                            }
                            else if (examList.Count == 4)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    HiddenField hfId = linkButton.NamingContainer.FindControl("hfId" + (i + 1) + "") as HiddenField;
                                    HiddenField hfCourseHistoryId = linkButton.NamingContainer.FindControl("hfCourseHistoryId" + (i + 1) + "") as HiddenField;
                                    HiddenField hfExamId = linkButton.NamingContainer.FindControl("hfExamId" + (i + 1) + "") as HiddenField;
                                    DropDownList ddlStatus = linkButton.NamingContainer.FindControl("ddlStatus" + (i + 1) + "") as DropDownList;
                                    TextBox txtMark = linkButton.NamingContainer.FindControl("txtMark" + (i + 1) + "") as TextBox;

                                    GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark);
                                }
                                LoadResult_Click(null, null);
                            }
                            else if (examList.Count == 5)
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    HiddenField hfId = linkButton.NamingContainer.FindControl("hfId" + (i + 1) + "") as HiddenField;
                                    HiddenField hfCourseHistoryId = linkButton.NamingContainer.FindControl("hfCourseHistoryId" + (i + 1) + "") as HiddenField;
                                    HiddenField hfExamId = linkButton.NamingContainer.FindControl("hfExamId" + (i + 1) + "") as HiddenField;
                                    DropDownList ddlStatus = linkButton.NamingContainer.FindControl("ddlStatus" + (i + 1) + "") as DropDownList;
                                    TextBox txtMark = linkButton.NamingContainer.FindControl("txtMark" + (i + 1) + "") as TextBox;

                                    GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark);
                                }
                                LoadResult_Click(null, null);
                            }
                            else if (examList.Count == 6)
                            {
                                for (int i = 0; i < 6; i++)
                                {
                                    HiddenField hfId = linkButton.NamingContainer.FindControl("hfId" + (i + 1) + "") as HiddenField;
                                    HiddenField hfCourseHistoryId = linkButton.NamingContainer.FindControl("hfCourseHistoryId" + (i + 1) + "") as HiddenField;
                                    HiddenField hfExamId = linkButton.NamingContainer.FindControl("hfExamId" + (i + 1) + "") as HiddenField;
                                    DropDownList ddlStatus = linkButton.NamingContainer.FindControl("ddlStatus" + (i + 1) + "") as DropDownList;
                                    TextBox txtMark = linkButton.NamingContainer.FindControl("txtMark" + (i + 1) + "") as TextBox;

                                    GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark);
                                }
                                LoadResult_Click(null, null);
                            }
                            else if (examList.Count == 7)
                            {
                                for (int i = 0; i < 7; i++)
                                {
                                    HiddenField hfId = linkButton.NamingContainer.FindControl("hfId" + (i + 1) + "") as HiddenField;
                                    HiddenField hfCourseHistoryId = linkButton.NamingContainer.FindControl("hfCourseHistoryId" + (i + 1) + "") as HiddenField;
                                    HiddenField hfExamId = linkButton.NamingContainer.FindControl("hfExamId" + (i + 1) + "") as HiddenField;
                                    DropDownList ddlStatus = linkButton.NamingContainer.FindControl("ddlStatus" + (i + 1) + "") as DropDownList;
                                    TextBox txtMark = linkButton.NamingContainer.FindControl("txtMark" + (i + 1) + "") as TextBox;

                                    GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark);
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

        protected void LoadResult_Click(object sender, EventArgs e)
        {
            Session["PreviousTotalMark"] = null;
            Session["PreviousGrade"] = null;
            try
            {
                if (ddlExam.Items.Count == 1)
                {
                    btnGradeChangeTop.Visible = true;
                    btnGradeChangeBottom.Visible = true;
                }
                else if (ddlExam.SelectedValue != "0")
                {
                    btnGradeChangeTop.Visible = false;
                    btnGradeChangeBottom.Visible = false;
                }
                else
                {
                    btnGradeChangeTop.Visible = true;
                    btnGradeChangeBottom.Visible = true;
                }


                int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
                int acaCalSecId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);
                int examId = Convert.ToInt32(ddlExam.SelectedValue);
                if (txtStudentId.Text != "" && txtStudentId.Text.Length != 12)
                {
                    lblMsg.Text = "Wrong Student ID";
                    return;
                }
                else if (acaCalId == 0 || acaCalSecId == 0)
                {
                    lblMsg.Text = "Please select Academic Calender and Course dropdown correctly";
                    return;
                }
                else if (ddlExam.SelectedValue == "-1")
                {
                    lblMsg.Text = "Grade Sheet Templete is not assign for this COURSE SECTION";
                    return;
                }

                #region If Exam Id is Zero Means All Number

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
                                    tempExamMarkList = tempExamMarkList.Where(x => x.IsFinalSubmit == true && x.IsTransfer == true && x.Roll == txtStudentId.Text.ToString()).ToList();

                                if (tempExamMarkList.Count > 0 && tempExamMarkList != null)
                                {
                                    if (examList[i].ExamName.Contains("Final"))
                                        finalExamFlag = tempExamMarkList[0].Status == 18 ? 1 : 0;

                                    gvExamResultSubmit.Visible = true;

                                    tempExamMarkList = tempExamMarkList.OrderBy(x => x.Roll).ToList();
                                    if (i == 0)
                                    {
                                        foreach (ExamMarkDTO temp in tempExamMarkList)
                                        {
                                            ExamMarkAllDTO examMarkAllDTO = new ExamMarkAllDTO();
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

                                            examMarkAllDTOList.Add(examMarkAllDTO);
                                        }
                                    }
                                    else if (i == 1)
                                    {
                                        for (int j = 0; j < tempExamMarkList.Count; j++)
                                        {
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
                                else
                                {
                                    gvExamResultSubmit.Visible = false;
                                    gvExamResultSubmit.DataSource = null;
                                    gvExamResultSubmit.DataBind();

                                    lblMsg.Text = "Student result not found";
                                    return;
                                }
                            }
                            //examList check If condition End
                            #endregion

                            foreach (ExamMarkAllDTO tempExamMarkAllDTO in examMarkAllDTOList)
                            {
                                if (examList.Count == 3)
                                {
                                    tempExamMarkAllDTO.TotalMark = Decimal.Ceiling((tempExamMarkAllDTO.Mark1 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark1)) + (tempExamMarkAllDTO.Mark2 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark2)) + (tempExamMarkAllDTO.Mark3 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark3)));
                                }
                                else if (examList.Count == 4)
                                {
                                    tempExamMarkAllDTO.TotalMark = Decimal.Ceiling((tempExamMarkAllDTO.Mark1 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark1)) + (tempExamMarkAllDTO.Mark2 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark2)) + (tempExamMarkAllDTO.Mark3 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark3)) + (tempExamMarkAllDTO.Mark4 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark4)));
                                }
                                else if (examList.Count == 5)
                                {
                                    tempExamMarkAllDTO.TotalMark = Decimal.Ceiling((tempExamMarkAllDTO.Mark1 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark1)) + (tempExamMarkAllDTO.Mark2 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark2)) + (tempExamMarkAllDTO.Mark3 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark3)) + (tempExamMarkAllDTO.Mark4 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark4)) + (tempExamMarkAllDTO.Mark5 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark5)));
                                }
                                else if (examList.Count == 6)
                                {
                                    tempExamMarkAllDTO.TotalMark = Decimal.Ceiling((tempExamMarkAllDTO.Mark1 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark1)) + (tempExamMarkAllDTO.Mark2 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark2)) + (tempExamMarkAllDTO.Mark3 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark3)) + (tempExamMarkAllDTO.Mark4 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark4)) + (tempExamMarkAllDTO.Mark5 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark5)) + (tempExamMarkAllDTO.Mark6 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark6)));
                                }
                                else if (examList.Count == 7)
                                {
                                    tempExamMarkAllDTO.TotalMark = Decimal.Ceiling(tempExamMarkAllDTO.Mark1 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark1) + tempExamMarkAllDTO.Mark2 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark2) + tempExamMarkAllDTO.Mark3 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark3) + tempExamMarkAllDTO.Mark4 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark4) + tempExamMarkAllDTO.Mark5 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark5) + tempExamMarkAllDTO.Mark6 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark6) + tempExamMarkAllDTO.Mark7 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark7));
                                }

                                GradeDetails temp = new GradeDetails();
                                List<GradeDetails> gradeDetailsList = GradeDetailsManager.GetByGradeMasterId(tempExamMarkAllDTO.GradeMasterId);
                                foreach (GradeDetails gradeDetails in gradeDetailsList)
                                {
                                    if (gradeDetails.MinMarks <= tempExamMarkAllDTO.TotalMark && gradeDetails.MaxMarks >= tempExamMarkAllDTO.TotalMark)
                                    {
                                        if (finalExamFlag == 1)
                                        {
                                            temp = gradeDetails;
                                            tempExamMarkAllDTO.Grade = "I";
                                            tempExamMarkAllDTO.GradePoint = Convert.ToDecimal("0.00");
                                        }
                                        else
                                        {
                                            temp = gradeDetails;
                                            tempExamMarkAllDTO.Grade = temp.Grade;
                                            tempExamMarkAllDTO.GradePoint = temp.GradePoint;
                                        }
                                        break;
                                    }
                                }
                            }

                            for (int i = 0; i < examList.Count; i++)
                            {
                                int maxMarkAll = 0;
                                if (examList[i] != null)
                                    maxMarkAll = examList[i].Marks;

                                if (maxMarkAll != 0)
                                    gvExamResultSubmit.Columns[3 + i + i].HeaderText = examList[i].ExamName + "(" + maxMarkAll + ")";
                                else
                                    gvExamResultSubmit.Columns[3 + i + i].HeaderText = examList[i].ExamName;
                            }

                            pnSubmitStudentMarkTop.Visible = true;
                            pnSubmitStudentMarkButtom.Visible = true;
                            gvExamResultSubmit.Visible = true;

                            if (examMarkAllDTOList.Count == 0)
                            {
                                pnSubmitStudentMarkTop.Visible = false;
                                pnSubmitStudentMarkButtom.Visible = false;
                            }

                            foreach (ExamMarkAllDTO temp in examMarkAllDTOList)
                            {
                                temp.Mark1 = temp.Mark1 == null ? "" : temp.Mark1;
                                temp.Mark2 = temp.Mark2 == null ? "" : temp.Mark2;
                                temp.Mark3 = temp.Mark3 == null ? "" : temp.Mark3;
                                temp.Mark4 = temp.Mark4 == null ? "" : temp.Mark4;
                                temp.Mark5 = temp.Mark5 == null ? "" : temp.Mark5;
                                temp.Mark6 = temp.Mark6 == null ? "" : temp.Mark6;
                                temp.Mark7 = temp.Mark7 == null ? "" : temp.Mark7;
                            }
                            gvExamResultSubmit.DataSource = examMarkAllDTOList;
                            gvExamResultSubmit.DataBind();
                            if (examMarkAllDTOList != null && examMarkAllDTOList.Count > 0)
                            {
                                Session["PreviousTotalMark"] = examMarkAllDTOList[0].TotalMark;
                                Session["PreviousGrade"] = examMarkAllDTOList[0].Grade;
                            }
                            if (examList.Count == 3)
                            {
                                gvExamResultSubmit.Columns[3].Visible = true;
                                gvExamResultSubmit.Columns[4].Visible = true;
                                gvExamResultSubmit.Columns[5].Visible = true;
                                gvExamResultSubmit.Columns[6].Visible = true;
                                gvExamResultSubmit.Columns[7].Visible = true;
                                gvExamResultSubmit.Columns[8].Visible = true;

                                gvExamResultSubmit.Columns[9].Visible = false;
                                gvExamResultSubmit.Columns[10].Visible = false;
                                gvExamResultSubmit.Columns[11].Visible = false;
                                gvExamResultSubmit.Columns[12].Visible = false;
                                gvExamResultSubmit.Columns[13].Visible = false;
                                gvExamResultSubmit.Columns[14].Visible = false;
                                gvExamResultSubmit.Columns[15].Visible = false;
                                gvExamResultSubmit.Columns[16].Visible = false;
                            }
                            else if (examList.Count == 4)
                            {
                                gvExamResultSubmit.Columns[3].Visible = true;
                                gvExamResultSubmit.Columns[4].Visible = true;
                                gvExamResultSubmit.Columns[5].Visible = true;
                                gvExamResultSubmit.Columns[6].Visible = true;
                                gvExamResultSubmit.Columns[7].Visible = true;
                                gvExamResultSubmit.Columns[8].Visible = true;
                                gvExamResultSubmit.Columns[9].Visible = true;
                                gvExamResultSubmit.Columns[10].Visible = true;

                                gvExamResultSubmit.Columns[11].Visible = false;
                                gvExamResultSubmit.Columns[12].Visible = false;
                                gvExamResultSubmit.Columns[13].Visible = false;
                                gvExamResultSubmit.Columns[14].Visible = false;
                                gvExamResultSubmit.Columns[15].Visible = false;
                                gvExamResultSubmit.Columns[16].Visible = false;
                            }
                            else if (examList.Count == 5)
                            {
                                gvExamResultSubmit.Columns[3].Visible = true;
                                gvExamResultSubmit.Columns[4].Visible = true;
                                gvExamResultSubmit.Columns[5].Visible = true;
                                gvExamResultSubmit.Columns[6].Visible = true;
                                gvExamResultSubmit.Columns[7].Visible = true;
                                gvExamResultSubmit.Columns[8].Visible = true;
                                gvExamResultSubmit.Columns[9].Visible = true;
                                gvExamResultSubmit.Columns[10].Visible = true;
                                gvExamResultSubmit.Columns[11].Visible = true;
                                gvExamResultSubmit.Columns[12].Visible = true;

                                gvExamResultSubmit.Columns[13].Visible = false;
                                gvExamResultSubmit.Columns[14].Visible = false;
                                gvExamResultSubmit.Columns[15].Visible = false;
                                gvExamResultSubmit.Columns[16].Visible = false;
                            }
                            else if (examList.Count == 6)
                            {
                                gvExamResultSubmit.Columns[3].Visible = true;
                                gvExamResultSubmit.Columns[4].Visible = true;
                                gvExamResultSubmit.Columns[5].Visible = true;
                                gvExamResultSubmit.Columns[6].Visible = true;
                                gvExamResultSubmit.Columns[7].Visible = true;
                                gvExamResultSubmit.Columns[8].Visible = true;
                                gvExamResultSubmit.Columns[9].Visible = true;
                                gvExamResultSubmit.Columns[10].Visible = true;
                                gvExamResultSubmit.Columns[11].Visible = true;
                                gvExamResultSubmit.Columns[12].Visible = true;
                                gvExamResultSubmit.Columns[13].Visible = true;
                                gvExamResultSubmit.Columns[14].Visible = true;

                                gvExamResultSubmit.Columns[15].Visible = false;
                                gvExamResultSubmit.Columns[16].Visible = false;
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
                                gvExamResultSubmit.Columns[10].Visible = true;
                                gvExamResultSubmit.Columns[11].Visible = true;
                                gvExamResultSubmit.Columns[12].Visible = true;
                                gvExamResultSubmit.Columns[13].Visible = true;
                                gvExamResultSubmit.Columns[14].Visible = true;
                                gvExamResultSubmit.Columns[15].Visible = true;
                                gvExamResultSubmit.Columns[16].Visible = true;
                            }

                            gvExamResultSubmit.Columns[17].Visible = true;
                            gvExamResultSubmit.Columns[18].Visible = true;
                            gvExamResultSubmit.Columns[19].Visible = true;
                        }
                        //main IF condition End
                    }
                    //AcaCalSec Check IF conditon End
                }
                #endregion
                else
                {
                    List<ExamMarkAllDTO> examMarkAllDTOList = new List<ExamMarkAllDTO>();

                    Exam exam = ExamManager.GetById(examId);
                    int maxMark = 0;
                    if (exam != null)
                        maxMark = exam.Marks;

                    List<ExamMarkDTO> examMarkList = ExamMarkManager.GetAllStudentByAcaCalAcaCalSecExam(acaCalId, acaCalSecId, examId);
                    if (examMarkList.Count > 0 && examMarkList != null)
                        examMarkList = examMarkList.Where(x => x.IsFinalSubmit == true && x.IsTransfer == true && x.Roll == txtStudentId.Text.ToString()).ToList();

                    if (examMarkList.Count > 0 && examMarkList != null)
                    {
                        if (maxMark != 0)
                            gvExamResultSubmit.Columns[3].HeaderText = "Mark(" + maxMark + ")";
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

                            examMarkAllDTO.Mark2 = "";
                            examMarkAllDTO.Mark3 = "";
                            examMarkAllDTO.Mark4 = "";
                            examMarkAllDTO.Mark5 = "";
                            examMarkAllDTO.Mark6 = "";
                            examMarkAllDTO.Mark7 = "";

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

                        if (examMarkAllDTOList != null && examMarkAllDTOList.Count > 0)
                        {
                            Session["PreviousTotalMark"] = examMarkAllDTOList[0].TotalMark;
                            Session["PreviousGrade"] = examMarkAllDTOList[0].Grade;
                        }
                        

                        gvExamResultSubmit.DataSource = examMarkAllDTOList;
                        gvExamResultSubmit.DataBind();

                        gvExamResultSubmit.Columns[5].Visible = false;
                        gvExamResultSubmit.Columns[6].Visible = false;
                        gvExamResultSubmit.Columns[7].Visible = false;
                        gvExamResultSubmit.Columns[8].Visible = false;
                        gvExamResultSubmit.Columns[9].Visible = false;
                        gvExamResultSubmit.Columns[10].Visible = false;
                        gvExamResultSubmit.Columns[11].Visible = false;
                        gvExamResultSubmit.Columns[12].Visible = false;
                        gvExamResultSubmit.Columns[13].Visible = false;
                        gvExamResultSubmit.Columns[14].Visible = false;
                        gvExamResultSubmit.Columns[15].Visible = false;
                        gvExamResultSubmit.Columns[16].Visible = false;
                        gvExamResultSubmit.Columns[17].Visible = false;
                        gvExamResultSubmit.Columns[18].Visible = false;
                        gvExamResultSubmit.Columns[19].Visible = false;
                    }
                    else
                    {
                        pnSubmitStudentMarkTop.Visible = false;
                        pnSubmitStudentMarkButtom.Visible = false;

                        gvExamResultSubmit.DataSource = null;
                        gvExamResultSubmit.DataBind();
                    }
                }
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

        protected void GradeChange_Click(Object sender, EventArgs e)
        {
            try
            {
                int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
                int acaCalSecId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);
                string roll = txtStudentId.Text;
                int resultTransferForReplica = ExamMarkReplicaManager.InsertByAcaCalAcaCalSecRoll(acaCalId, acaCalSecId, roll);

                AcademicCalenderSection acaCalSec = AcademicCalenderSectionManager.GetById(acaCalSecId);
                List<Exam> examList = ExamManager.GetAllByExamTemplateId(acaCalSec.BasicExamTemplateId);

                if (resultTransferForReplica == examList.Count)
                {
                    SubmitAllStudentMark();
                    string resultTransfer = ExamMarkManager.GetApprovedNumberByExamControllerByAcaCalSecRoll(acaCalSecId, roll);
                    string[] actionHistory = resultTransfer.Split('-');
                    if (actionHistory[1] == "1")
                        lblMsg.Text = lblMsg.Text + " and Transfer I Grade";

                }
                else
                {
                    lblMsg.Text = "Error: Grade Change not possible";
                }
            }
            catch { }
            finally { }
        }

        protected void SubmitAllStudentMark()
        {
            try
            {
                if (ddlExam.SelectedValue == "-1")
                {
                    lblMsg.Text = "Grade Sheet Templete is not assign for this COURSE SECTION";
                }
                else
                {

                    string Grade = "";
                    string TotalMark = "";
                    string PreviousTotalMark = Session["PreviousTotalMark"].ToString();
                    string PreviousGrade = Session["PreviousGrade"].ToString();
                    string okstatus = "";
                    foreach (GridViewRow row in gvExamResultSubmit.Rows)
                    {
                        #region Condition Check

                        if (ddlExam.SelectedValue != "0")
                        {
                            HiddenField hfId = (HiddenField)row.FindControl("hfId1");
                            HiddenField hfCourseHistoryId = (HiddenField)row.FindControl("hfCourseHistoryId1");
                            HiddenField hfExamId = (HiddenField)row.FindControl("hfExamId1");
                            DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus1");
                            TextBox txtMark = (TextBox)row.FindControl("txtMark1");

                            GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark);

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
                                    if (examList.Count == 3)
                                    {
                                        for (int i = 0; i < 3; i++)
                                        {
                                            HiddenField hfId = (HiddenField)row.FindControl("hfId" + (i + 1) + "");
                                            HiddenField hfCourseHistoryId = (HiddenField)row.FindControl("hfCourseHistoryId" + (i + 1) + "");
                                            HiddenField hfExamId = (HiddenField)row.FindControl("hfExamId" + (i + 1) + "");
                                            DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus" + (i + 1) + "");
                                            TextBox txtMark = (TextBox)row.FindControl("txtMark" + (i + 1) + "");

                                            GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark);
                                        }
                                        LoadResult_Click(null, null);
                                    }
                                    else if (examList.Count == 4)
                                    {
                                        for (int i = 0; i < 4; i++)
                                        {
                                            HiddenField hfId = (HiddenField)row.FindControl("hfId" + (i + 1) + "");
                                            HiddenField hfCourseHistoryId = (HiddenField)row.FindControl("hfCourseHistoryId" + (i + 1) + "");
                                            HiddenField hfExamId = (HiddenField)row.FindControl("hfExamId" + (i + 1) + "");
                                            DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus" + (i + 1) + "");
                                            TextBox txtMark = (TextBox)row.FindControl("txtMark" + (i + 1) + "");

                                            GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark);
                                        }
                                        LoadResult_Click(null, null);
                                    }
                                    else if (examList.Count == 5)
                                    {
                                        for (int i = 0; i < 5; i++)
                                        {
                                            HiddenField hfId = (HiddenField)row.FindControl("hfId" + (i + 1) + "");
                                            HiddenField hfCourseHistoryId = (HiddenField)row.FindControl("hfCourseHistoryId" + (i + 1) + "");
                                            HiddenField hfExamId = (HiddenField)row.FindControl("hfExamId" + (i + 1) + "");
                                            DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus" + (i + 1) + "");
                                            TextBox txtMark = (TextBox)row.FindControl("txtMark" + (i + 1) + "");

                                            GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark);
                                        }
                                        LoadResult_Click(null, null);
                                    }
                                    else if (examList.Count == 6)
                                    {
                                        for (int i = 0; i < 6; i++)
                                        {
                                            HiddenField hfId = (HiddenField)row.FindControl("hfId" + (i + 1) + "");
                                            HiddenField hfCourseHistoryId = (HiddenField)row.FindControl("hfCourseHistoryId" + (i + 1) + "");
                                            HiddenField hfExamId = (HiddenField)row.FindControl("hfExamId" + (i + 1) + "");
                                            DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus" + (i + 1) + "");
                                            TextBox txtMark = (TextBox)row.FindControl("txtMark" + (i + 1) + "");

                                            GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark);
                                        }
                                        LoadResult_Click(null, null);
                                    }
                                    else if (examList.Count == 7)
                                    {
                                        for (int i = 0; i < 7; i++)
                                        {
                                            HiddenField hfId = (HiddenField)row.FindControl("hfId" + (i + 1) + "");
                                            HiddenField hfCourseHistoryId = (HiddenField)row.FindControl("hfCourseHistoryId" + (i + 1) + "");
                                            HiddenField hfExamId = (HiddenField)row.FindControl("hfExamId" + (i + 1) + "");
                                            DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus" + (i + 1) + "");
                                            TextBox txtMark = (TextBox)row.FindControl("txtMark" + (i + 1) + "");

                                            GetDataForInsertUpdate(hfId, hfCourseHistoryId, hfExamId, ddlStatus, txtMark);
                                        }
                                        LoadResult_Click(null, null);
                                    }
                                }

                                #endregion
                            }

                        }
                        #endregion                      
                    }

                    LoadResult_Click(null, null);

                    foreach (GridViewRow row in gvExamResultSubmit.Rows)
                    {
                        Label txtTotalMark = (Label)row.FindControl("lblTotal");
                        TotalMark = txtTotalMark.Text.Trim();
                        Label txtGrade = (Label)row.FindControl("lblGrade");
                        Grade = txtGrade.Text.Trim();
                    }

                    Student student = StudentManager.GetByRoll(txtStudentId.Text);
                    if (student != null && (TotalMark != PreviousTotalMark || Grade != PreviousGrade))
                    {
                        #region SMS Sending
                        string sms = "ID-" + student.Roll + ", your grade is changed from " + PreviousGrade + " to " + Grade + ". And total mark is changed from " + PreviousTotalMark + " to " + TotalMark + " on " + DateTime.Now.Date.ToString("dd/M/yyyy") + "";
                        SendSMS(student.BasicInfo.SMSContactSelf, student.Roll, sms);
                        #endregion
                        okstatus = sms;
                    }

                    List<User> vclist = UserManager.GetAll().Where(u => u.RoleID == 12 || u.RoleID == 17 || u.RoleID == 22 || u.RoleID == 30).ToList();

                    if (vclist != null)
                    {
                        foreach (User vc in vclist)
                        {
                            #region SMS Sending
                            string sms = "ID-" + student.Roll + ". Grade for " + ddlAcaCalSection.SelectedItem.Text + " in " + ddlAcademicCalender.SelectedItem.Text + " is changed to " + Grade;
                            SendSMS(vc.Person.SMSContactSelf, student.Roll, sms);
                            #endregion
                        }
                    }  
                    //lblMsg.Text = okstatus;
                }
                //this is end of foreach

                //lblMsg.Text = "Total " + saveCount + " Saved and " + updateCount + " Updated Successfully";

                

            }
            catch { }
            finally { }
        }

        private void SendSMS(string PhoneNo, string roll, string msg)
        {
            SMSBasicSetup smsSetup = SMSBasicSetupManager.Get();
            bool updated = SMSBasicSetupManager.Update(smsSetup);
            if (!string.IsNullOrEmpty(PhoneNo) && PhoneNo.Count() == 14 && PhoneNo.Contains("+") && smsSetup.RemainingSMS > 0 && smsSetup.ResultCorrectionStatus == true)
                SMSManager.Send(PhoneNo, roll, msg, ResultCallBack);
            else
                LogSMSManager.Insert(DateTime.Now, userObj.LogInID.ToString(), roll, "Number format or setup related error", false);
        }

        void ResultCallBack(string[] data)
        {
            if (data[2].Contains("<status>0</status>"))
            {
                LogSMSManager.Insert(DateTime.Now, userObj.LogInID.ToString(), data[0], data[1], true);
            }
            else
            {
                LogSMSManager.Insert(DateTime.Now, userObj.LogInID.ToString(), data[0], data[1], false);
            }
            SMSBasicSetup smsSetup = SMSBasicSetupManager.Get();
            smsSetup.RemainingSMS = smsSetup.RemainingSMS - 1;
            bool updated = SMSBasicSetupManager.Update(smsSetup);
        }

        #endregion
    }
}