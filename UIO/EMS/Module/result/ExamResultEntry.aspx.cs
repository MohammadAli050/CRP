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
    public partial class ExamResultEntry : BasePage
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
                gvExamResultSubmit.Visible = false;
            }
            //ExamResultViewPrint.LocalReport.DataSources.Clear();
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
                            if (ddlExam.SelectedItem.Text == "Grade Only")
                            {
                                valueList = valueList.Where(x => x.ValueName != "Mark").OrderByDescending(x => x.ValueID).ToList();
                            }
                            else if (ddlExam.SelectedItem.Text == "Total Marks")
                            {
                                valueList = valueList.Where(x => x.ValueName != "Grade").OrderBy(x => x.ValueID).ToList();
                            }
                            else
                                valueList = valueList.Where(x => x.ValueName != "Grade").OrderBy(x => x.ValueID).ToList();

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
                examMark.Mark = mark.ToUpper().Replace(" ", "");
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
                examMark.Mark = mark.ToUpper().Replace(" ", "");
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
                string mark = ddlStatus.SelectedItem.Text == "Absent" ? "" : txtMark.Text.ToUpper().Replace(" ", "");
                bool isFinalSubmit = false;

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

        protected bool MarkValidationWithOutStatus(string examMark)
        {
            try
            {
                if (isNumeric(examMark, System.Globalization.NumberStyles.Float))
                {
                    return true;
                }
                else { return false; }
                
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
                ExamResultViewPrint.LocalReport.DataSources.Clear();
                ExamResultViewPrint.LocalReport.DataSources.Add(null);
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
                    if (ddlExam.SelectedItem.Text == "Grade Only")
                    {
                        ddlStatus1.Items.FindByText("Grade").Selected = true; 
                    }
                    else if (ddlExam.SelectedItem.Text == "Total Marks")
                    {
                        ddlStatus1.Items.FindByText("Mark").Selected = true;
                    }
                    else 
                    {
                        ddlStatus1.Items.FindByText("Mark").Selected = true;
                    }
                }
                else
                {
                    ddlStatus1.SelectedValue = statusId1.ToString();
                }
               
                if (statusId1 == 18)
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
                {
                    if (ddlExam.SelectedItem.Text == "Grade Only")
                    {
                        ddlStatus2.Items.FindByText("Grade").Selected = true;
                    }
                    else if (ddlExam.SelectedItem.Text == "Total Marks")
                    {
                        ddlStatus2.Items.FindByText("Mark").Selected = true;
                    }
                    else
                    {
                        ddlStatus2.Items.FindByText("Mark").Selected = true;
                    }
                }
                else
                {
                    ddlStatus2.SelectedValue = statusId2.ToString();
                }
                
                if (statusId2 == 18)
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
                {
                    if (ddlExam.SelectedItem.Text == "Grade Only")
                    {
                        ddlStatus3.Items.FindByText("Grade").Selected = true;
                    }
                    else if (ddlExam.SelectedItem.Text == "Total Marks")
                    {
                        ddlStatus3.Items.FindByText("Mark").Selected = true;
                    }
                    else
                    {
                        ddlStatus3.Items.FindByText("Mark").Selected = true;
                    }
                }
                else
                    ddlStatus3.SelectedValue = statusId3.ToString();

                if (statusId3 == 18)
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
                {
                    if (ddlExam.SelectedItem.Text == "Grade Only")
                    {
                        ddlStatus4.Items.FindByText("Grade").Selected = true;
                    }
                    else if (ddlExam.SelectedItem.Text == "Total Marks")
                    {
                        ddlStatus4.Items.FindByText("Mark").Selected = true;
                    }
                    else
                    {
                        ddlStatus4.Items.FindByText("Mark").Selected = true;
                    }
                }
                else
                    ddlStatus4.SelectedValue = statusId4.ToString();

                if (statusId4 == 18)
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
                {
                    if (ddlExam.SelectedItem.Text == "Grade Only")
                    {
                        ddlStatus5.Items.FindByText("Grade").Selected = true;
                    }
                    else if (ddlExam.SelectedItem.Text == "Total Marks")
                    {
                        ddlStatus5.Items.FindByText("Mark").Selected = true;
                    }
                    else
                    {
                        ddlStatus5.Items.FindByText("Mark").Selected = true;
                    }
                }
                else
                    ddlStatus5.SelectedValue = statusId5.ToString();

                if (statusId5 == 18)
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
                {
                    if (ddlExam.SelectedItem.Text == "Grade Only")
                    {
                        ddlStatus6.Items.FindByText("Grade").Selected = true;
                    }
                    else if (ddlExam.SelectedItem.Text == "Total Marks")
                    {
                        ddlStatus6.Items.FindByText("Mark").Selected = true;
                    }
                    else
                    {
                        ddlStatus6.Items.FindByText("Mark").Selected = true;
                    }
                }
                    else
                        ddlStatus6.SelectedValue = statusId6.ToString();

                if (statusId6 == 18)
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
                {
                    if (ddlExam.SelectedItem.Text == "Grade Only")
                    {
                        ddlStatus7.Items.FindByText("Grade").Selected = true;
                    }
                    else if (ddlExam.SelectedItem.Text == "Total Marks")
                    {
                        ddlStatus7.Items.FindByText("Mark").Selected = true;
                    }
                    else
                    {
                        ddlStatus7.Items.FindByText("Mark").Selected = true;
                    }
                }
                    else
                        ddlStatus7.SelectedValue = statusId7.ToString();

                if (statusId7 == 18)
                {
                    txtMark7.Text = "ab";
                    txtMark7.Enabled = false;
                }
                ddlStatus7.Enabled = false;
                ddlStatus6.Enabled = false;
                ddlStatus5.Enabled = false;
                ddlStatus4.Enabled = false;
                ddlStatus3.Enabled = false;
                ddlStatus2.Enabled = false;
                ddlStatus1.Enabled = false;

                if (statusId1 == 22 || statusId2 == 22 || statusId3 == 22 || statusId4 == 22 || statusId5 == 22 || statusId6 == 22 || statusId7 == 22) 
                {
                    txtMark7.Enabled = false;
                    txtMark6.Enabled = false;
                    txtMark5.Enabled = false;
                    txtMark4.Enabled = false;
                    txtMark3.Enabled = false;
                    txtMark2.Enabled = false;
                    //if (statusId1 == 22)
                    //{

                        txtMark1.Enabled = false;
                    //}
                    //else 
                    //{
                    //    txtMark1.Enabled = true;
                    //}
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

                    if (ddlStatus.SelectedItem.Text.ToLower() == "grade")
                    {
                        bool checkFlag = GradeValidation(txtMark.Text.ToUpper().Replace(" ", ""));
                        if (!checkFlag)
                        {
                            ShowAlertMessage("Please input correct grade.");
                            txtMark.Text = " ";
                            return;
                        }
                    }

                    else if (ddlStatus.SelectedItem.Text.ToLower() == "mark")
                    {
                        bool checkFlag = MarkValidationWithOutStatus(txtMark.Text);
                        if (!checkFlag)
                        {
                            ShowAlertMessage("Please input correct mark.");
                            txtMark.Text = "0";
                            return;
                        }
                    }

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

                                    if (Convert.ToInt32(ddlStatus.SelectedValue) == 17)
                                    {
                                        bool checkFlag = MarkValidation(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToString(txtMark.Text));
                                        if (!checkFlag)
                                        {
                                            ShowAlertMessage("Please enter number in mark field");
                                            txtMark.Text = "0";
                                            return;
                                        }
                                    }

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

                                    if (Convert.ToInt32(ddlStatus.SelectedValue) == 17)
                                    {
                                        bool checkFlag = MarkValidation(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToString(txtMark.Text));
                                        if (!checkFlag)
                                        {
                                            ShowAlertMessage("Please enter number in mark field");
                                            txtMark.Text = "0";
                                            return;
                                        }
                                    }

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

                                    if (Convert.ToInt32(ddlStatus.SelectedValue) == 17)
                                    {
                                        bool checkFlag = MarkValidation(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToString(txtMark.Text));
                                        if (!checkFlag)
                                        {
                                            ShowAlertMessage("Please enter number in mark field");
                                            txtMark.Text = "0";
                                            return;
                                        }
                                    }

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

                                    if (Convert.ToInt32(ddlStatus.SelectedValue) == 17)
                                    {
                                        bool checkFlag = MarkValidation(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToString(txtMark.Text));
                                        if (!checkFlag)
                                        {
                                            ShowAlertMessage("Please enter number in mark field");
                                            txtMark.Text = "0";
                                            return;
                                        }
                                    }

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

                                    if (Convert.ToInt32(ddlStatus.SelectedValue) == 17)
                                    {
                                        bool checkFlag = MarkValidation(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToString(txtMark.Text));
                                        if (!checkFlag)
                                        {
                                            ShowAlertMessage("Please enter number in mark field");
                                            txtMark.Text = "0";
                                            return;
                                        }
                                    }

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
                            HiddenField hfId = (HiddenField)row.FindControl("hfId1");
                            HiddenField hfCourseHistoryId = (HiddenField)row.FindControl("hfCourseHistoryId1");
                            HiddenField hfExamId = (HiddenField)row.FindControl("hfExamId1");
                            DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus1");
                            TextBox txtMark = (TextBox)row.FindControl("txtMark1");

                            if (ddlStatus.SelectedItem.Text.ToLower() == "grade")
                            {
                                bool checkFlag = GradeValidation(txtMark.Text.ToUpper().Replace(" ", ""));
                                if (!checkFlag)
                                {
                                    ShowAlertMessage( "Please input correct grade.");
                                    txtMark.Text = " ";
                                    return;
                                }
                            }
                            else if (ddlStatus.SelectedItem.Text.ToLower() == "mark")
                            {
                                bool checkFlag = MarkValidationWithOutStatus(txtMark.Text);
                                if (!checkFlag)
                                {
                                    ShowAlertMessage( "Please input correct mark.");
                                    txtMark.Text = "0";
                                    return;
                                }
                            }

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

                                            if (Convert.ToInt32(ddlStatus.SelectedValue) == 17)
                                            {
                                                bool checkFlag = MarkValidation(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToString(txtMark.Text));
                                                if (!checkFlag)
                                                {
                                                    ShowAlertMessage("Please enter number in mark field");
                                                    txtMark.Text = "0";
                                                    return;
                                                }
                                            }

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

                                            if (Convert.ToInt32(ddlStatus.SelectedValue) == 17)
                                            {
                                                bool checkFlag = MarkValidation(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToString(txtMark.Text));
                                                if (!checkFlag)
                                                {
                                                    ShowAlertMessage("Please enter number in mark field");
                                                    txtMark.Text = "0";
                                                    return;
                                                }
                                            }

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

                                            if (Convert.ToInt32(ddlStatus.SelectedValue) == 17)
                                            {
                                                bool checkFlag = MarkValidation(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToString(txtMark.Text));
                                                if (!checkFlag)
                                                {
                                                    ShowAlertMessage("Please enter number in mark field");
                                                    txtMark.Text = "0";
                                                    return;
                                                }
                                            }

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

                                            if (Convert.ToInt32(ddlStatus.SelectedValue) == 17)
                                            {
                                                bool checkFlag = MarkValidation(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToString(txtMark.Text));
                                                if (!checkFlag)
                                                {
                                                    ShowAlertMessage("Please enter number in mark field");
                                                    txtMark.Text = "0";
                                                    return;
                                                }
                                            }

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

                                            if (Convert.ToInt32(ddlStatus.SelectedValue) == 17)
                                            {
                                                bool checkFlag = MarkValidation(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToString(txtMark.Text));
                                                if (!checkFlag)
                                                {
                                                    ShowAlertMessage("Please enter number in mark field");
                                                    txtMark.Text = "0";
                                                    return;
                                                }
                                            }
  
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
                ExamResultViewPrint.LocalReport.DataSources.Clear();
                if (ddlExam.Items.Count == 1)
                {
                    btnFinalSubmitTop.Visible = true;
                    btnFinalSubmitBottom.Visible = true;
                }
                else if (ddlExam.SelectedItem.Text != "All")
                {
                    btnFinalSubmitTop.Visible = false;
                    btnFinalSubmitBottom.Visible = false;
                }
                else
                {
                    btnFinalSubmitTop.Visible = true;
                    btnFinalSubmitBottom.Visible = true;
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
                                    tempExamMarkAllDTO.TotalMark = Decimal.Ceiling((tempExamMarkAllDTO.Mark1 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark1)) + (tempExamMarkAllDTO.Mark2 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark2)) + (tempExamMarkAllDTO.Mark3 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark3)) + (tempExamMarkAllDTO.Mark4 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark4)) + (tempExamMarkAllDTO.Mark5 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark5)) + (tempExamMarkAllDTO.Mark6 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark6)) + (tempExamMarkAllDTO.Mark7 == "" ? 0 : Convert.ToDecimal(tempExamMarkAllDTO.Mark7)));
                                }

                                GradeDetails temp = new GradeDetails();
                                List<GradeDetails> gradeDetailsList = GradeDetailsManager.GetByGradeMasterId(tempExamMarkAllDTO.GradeMasterId);
                                foreach (GradeDetails gradeDetails in gradeDetailsList)
                                {
                                    if (gradeDetails.MinMarks <= tempExamMarkAllDTO.TotalMark && gradeDetails.MaxMarks >= tempExamMarkAllDTO.TotalMark)
                                    {

                                        if (tempExamMarkAllDTO.forIGrade == 1)
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
                            gvExamResultSubmit.DataSource = examMarkAllDTOList;
                            gvExamResultSubmit.DataBind();

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
                            else { gvExamResultSubmit.Columns[3].HeaderText = "Mark(" + maxMark + ")"; }
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

                        if (ddlExam.SelectedItem.Text == "Total Marks")
                        {
                            foreach (ExamMarkAllDTO tempExamMarkAllDTO in examMarkAllDTOList)
                            {
                                GradeDetails temp = new GradeDetails();
                                List<GradeDetails> gradeDetailsList = GradeDetailsManager.GetByGradeMasterId(tempExamMarkAllDTO.GradeMasterId);
                                foreach (GradeDetails gradeDetails in gradeDetailsList)
                                {
                                    if (MarkValidationWithOutStatus(tempExamMarkAllDTO.Mark1))
                                    {
                                        if (gradeDetails.MinMarks <= Convert.ToDecimal(tempExamMarkAllDTO.Mark1) && gradeDetails.MaxMarks >= Convert.ToDecimal(tempExamMarkAllDTO.Mark1))
                                        {

                                            if (tempExamMarkAllDTO.forIGrade == 1)
                                            {
                                                temp = gradeDetails;
                                                tempExamMarkAllDTO.TotalMark = Convert.ToDecimal(tempExamMarkAllDTO.Mark1);
                                                tempExamMarkAllDTO.Grade = "I";
                                                tempExamMarkAllDTO.GradePoint = Convert.ToDecimal("0.00");
                                            }
                                            else
                                            {
                                                temp = gradeDetails;
                                                tempExamMarkAllDTO.TotalMark = Convert.ToDecimal(tempExamMarkAllDTO.Mark1);
                                                tempExamMarkAllDTO.Grade = temp.Grade;
                                                tempExamMarkAllDTO.GradePoint = temp.GradePoint;
                                            }
                                            break;
                                        }
                                    }
                                    else 
                                    {
                                        temp = gradeDetails;
                                        //tempExamMarkAllDTO.TotalMark = Convert.ToDecimal(tempExamMarkAllDTO.Mark1);
                                        tempExamMarkAllDTO.Grade = "No Grade";
                                        //tempExamMarkAllDTO.GradePoint = "No Grade";
                                    }
                                }
                            }
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
                        if (ddlExam.SelectedItem.Text == "Total Marks")
                        {
                            gvExamResultSubmit.Columns[17].Visible = true;
                            gvExamResultSubmit.Columns[18].Visible = true;
                            gvExamResultSubmit.Columns[19].Visible = true;
                        }
                        else 
                        {
                            gvExamResultSubmit.Columns[17].Visible = false;
                            gvExamResultSubmit.Columns[18].Visible = false;
                            gvExamResultSubmit.Columns[19].Visible = false;
                        }
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

        protected void FinalSubmit_Click(Object sender, EventArgs e)
        {
            try
            {
                SubmitAllStudentMark_Click(null, null);


                int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
                int acaCalSecId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);

                if (acaCalId == 0 || acaCalSecId == 0)
                {
                    lblMsg.Text = "Please select Academic Calender, Course and Test dropdown correctly";
                    return;
                }

                int resultUpdate = ExamMarkManager.GetTotalUpdateNumberIsFinalSubmit(acaCalId, acaCalSecId);

                if (resultUpdate > 0)
                {
                    lblMsg.Text = "Total " + resultUpdate + " student mark are submit for approve";

                    gvExamResultSubmit.Visible = false;
                    pnSubmitStudentMarkTop.Visible = false;
                    pnSubmitStudentMarkButtom.Visible = false;

                    PrintStudentMark();
                }
                
            }
            catch { }
            finally { }
        }

        #endregion

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //rptPanel.Visible = true;
            PrintStudentMark();

        }

        private void PrintStudentMark()
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
                                        string section = acaCalSec.SectionName;

                                        ReportParameter p1 = new ReportParameter("Marks1", marks1);
                                        ReportParameter p2 = new ReportParameter("Marks2", marks2);
                                        ReportParameter p3 = new ReportParameter("Marks3", marks3);
                                        ReportParameter p4 = new ReportParameter("Calendar", calendar);
                                        ReportParameter p5 = new ReportParameter("Section", section);

                                        ExamResultViewPrint.LocalReport.ReportPath = Server.MapPath("~/miu/result/report/ExamResultPrintTheory.rdlc");
                                        this.ExamResultViewPrint.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4,p5 });
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
                                        this.ExamResultViewPrint.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7, p8,p9 });
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
                                        this.ExamResultViewPrint.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5,p6 });
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
                            ShowMessage("No marks found. Please select another course");
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
            //pnlMessage.Visible = true;

            lblMsg.Text = msg;
            lblMsg.ForeColor = Color.Red;
        }

        private void ShowAlertMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerControlScript", "alert('" + msg + "');", true);
        }
    }
}