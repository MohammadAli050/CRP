using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.result
{
    public partial class StudentCourseDrop : BasePage
    {

        #region Function
        BussinessObject.UIUMSUser userObj = null;
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            lblMsg.Text = "";
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            if (!IsPostBack)
            {
                gvGradeChange.Visible = false;
                LoadComboBox();
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

        protected void LoadCourse(int acaCalId, string roll)
        {
            try
            {
                ddlCourse.Items.Clear();

                List<Course> courseList = CourseManager.GetAllByAcaCalIdStudentRoll(acaCalId, roll);

                Student student = StudentManager.GetByRoll(roll);

                List<StudentCourseHistory> tempList = StudentCourseHistoryManager.GetAllByStudentIdAcaCalId(student.StudentID, acaCalId);

                List<StudentCourseHistory> tempList2;

                if (courseList != null && courseList.Count > 0)
                {
                    foreach (Course cr in courseList)
                    {
                        tempList2 = tempList.Where(x => x.CourseID == cr.CourseID && x.VersionID == cr.VersionID).ToList();
                        if (tempList2 != null && tempList2.Count > 0 && tempList2[0].CourseStatusID==9)
                        {
                            ddlCourse.Items.Add(new ListItem(cr.FormalCode + ": " + cr.Title, cr.CourseID + "_" + cr.VersionID));
                        }
                    }

                    if (ddlCourse.Items.Count == 0)
                    {
                        ddlCourse.Items.Clear();
                        ddlCourse.Items.Add(new ListItem("Select", "0_0"));
                        lblMsg.Text = "Course not found";
                    }
                }
                else
                {
                    ddlCourse.Items.Clear();
                    ddlCourse.Items.Add(new ListItem("Select", "0_0"));

                    lblMsg.Text = "Course not found";
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
                ddlStatus.Items.Add(new ListItem("Select", "0"));
                ddlStatus.AppendDataBoundItems = true;

                List<CourseStatus> courseStatusList = CourseStatusManager.GetAll();
                if (courseStatusList.Count > 0 && courseStatusList != null)
                {
                    courseStatusList = courseStatusList.Where(x => x.Code.Equals("Dp")).ToList();
                    ddlStatus.DataSource = courseStatusList;
                    ddlStatus.DataValueField = "CourseStatusID";
                    ddlStatus.DataTextField = "Code";
                    ddlStatus.DataBind();
                }
            }
            catch { }
            finally { }
        }

        protected void LoadGradeCombo(DropDownList ddlGrade, int gradeMasterId)
        {
            try
            {
                ddlGrade.Items.Clear();
                ddlGrade.Items.Add(new ListItem("Select", "0"));
                ddlGrade.AppendDataBoundItems = true;

                List<GradeDetails> gradeDetailsList = GradeDetailsManager.GetAll();
                if (gradeDetailsList.Count > 0 && gradeDetailsList != null)
                {
                    gradeDetailsList = gradeDetailsList.Where(x => x.GradeMasterId == gradeMasterId).ToList();

                    if (gradeDetailsList.Count > 0 && gradeDetailsList != null)
                    {
                        foreach (GradeDetails temp in gradeDetailsList)
                            ddlGrade.Items.Add(new ListItem(temp.Grade + " (" + temp.GradePoint.ToString() + ")", temp.GradeId.ToString()));
                    }
                }
            }
            catch { }
            finally { }
        }

        protected void ClearField()
        {
            try
            {
                gvGradeChange.Visible = false;
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Select", "0"));
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
                ClearField();
            }
            catch { }
            finally
            {
            }
        }

        protected void AcademicCalender_Changed(Object sender, EventArgs e)
        {
            try
            {
                int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
                ClearField();
            }
            catch { }
            finally
            {
            }
        }

        protected void Course_Changed(Object sender, EventArgs e)
        {
            try
            {
                gvGradeChange.Visible = false;
            }
            catch { }
            finally { }
        }

        protected void LoadCourse_Click(Object sender, EventArgs e)
        {
            if (ddlAcademicCalender.SelectedValue == "0" || txtStudentId.Text == "")
            {
                lblMsg.Text = "Please Academic Calender and Student ID";
                return;
            }
            try
            {
                int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
                string studentRoll = txtStudentId.Text;

                gvGradeChange.Visible = false;

                LoadCourse(acaCalId, studentRoll);
            }
            catch { }
            finally { }
        }

        protected void Load_Click(Object sender, EventArgs e)
        {
            if (ddlAcademicCalender.SelectedValue == "0" || txtStudentId.Text == "" || ddlCourse.SelectedValue == "0")
            {
                lblMsg.Text = "Please Academic Calender, Student ID and Course";
                return;
            }
            try
            {
                int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
                string[] courseCode = ddlCourse.SelectedValue.Split('_');
                int courseId = Convert.ToInt32(courseCode[0]);
                int versionId = Convert.ToInt32(courseCode[1]);
                string roll = txtStudentId.Text;

                Student student = StudentManager.GetByRoll(roll);

                List<StudentCourseHistory> tempList = StudentCourseHistoryManager.GetAllByStudentIdAcaCalId(student.StudentID, acaCalId);
                if (tempList.Count > 0 && tempList != null)
                {
                    tempList = tempList.Where(x => x.CourseID == courseId && x.VersionID == versionId).ToList();
                    if (tempList.Count > 0 && tempList != null)
                    {
                        List<CourseStatus> courseStatusList = CourseStatusManager.GetAll();
                        Hashtable hashCourseStatus = new Hashtable();
                        foreach (CourseStatus courseStatus in courseStatusList)
                            hashCourseStatus.Add(courseStatus.CourseStatusID.ToString(), courseStatus.Code);

                        Course course = CourseManager.GetByCourseIdVersionId(courseId, versionId);
                        tempList[0].CourseCode = course.VersionCode;
                        tempList[0].CourseName = course.Title;
                        if (tempList[0].GradeId == 0 && tempList[0].CourseStatusID != 0)
                            tempList[0].ObtainedGrade = hashCourseStatus[tempList[0].CourseStatusID.ToString()].ToString();

                        gvGradeChange.Visible = true;
                        gvGradeChange.DataSource = tempList;
                        gvGradeChange.DataBind();
                    }
                    else
                    {
                        gvGradeChange.Visible = false;
                        gvGradeChange.DataSource = null;
                        gvGradeChange.DataBind();

                        lblMsg.Text = "error a2eda";
                    }
                }
                else
                {
                    gvGradeChange.Visible = false;
                    gvGradeChange.DataSource = null;
                    gvGradeChange.DataBind();

                    lblMsg.Text = "error a2edb";
                }
            }
            catch
            {
                gvGradeChange.Visible = false;
                gvGradeChange.DataSource = null;
                gvGradeChange.DataBind();

                lblMsg.Text = "error a2edc";
            }
            finally { }
        }

        protected void gvGradeChange_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            try
            {
                string roll = txtStudentId.Text;
                Student student = StudentManager.GetByRoll(roll);
              //  DropDownList ddlGrade = (DropDownList)e.Row.FindControl("ddlGrade");
              //  LoadGradeCombo(ddlGrade, Convert.ToInt32(student.GradeMasterId));

                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                LoadStatusCombo(ddlStatus);
            }
            catch { }
            finally { }
        }

      
        protected void Status_Changed(Object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlStatus = (DropDownList)sender;
 
            }
            catch { }
            finally { }
        }

        protected void lbUpdate_Click(Object sender, EventArgs e)
        {
            try
            {
                string changedStatus = "";
                int modified = 99;
                try
                {
                    string loginID = Session[Constants.SESSIONCURRENT_LOGINID].ToString();
                    User user = UserManager.GetByLogInId(loginID);
                    if (user != null)
                        modified = user.User_ID;
                }
                catch { }

                LinkButton linkButton = new LinkButton();
                linkButton = (LinkButton)sender;
                int id = Convert.ToInt32(linkButton.CommandArgument);

                TextBox txtObtainedGrade = linkButton.NamingContainer.FindControl("txtObtainedGrade") as TextBox;
               // DropDownList ddlGrade = linkButton.NamingContainer.FindControl("ddlGrade") as DropDownList;
                DropDownList ddlStatus = linkButton.NamingContainer.FindControl("ddlStatus") as DropDownList;

                if (ddlStatus.SelectedValue == "0")
                {
                    lblMsg.Text = "Plz select status";
                    return;
                }

                StudentCourseHistory courseHistory = StudentCourseHistoryManager.GetById(id);

                if (ddlStatus.SelectedValue != "0")
                {
                    courseHistory.GradeId = 0;
                    courseHistory.ObtainedGrade = "";
                    courseHistory.ObtainedGPA = 0;
                    courseHistory.CourseStatusID = Convert.ToInt32(ddlStatus.SelectedValue);
                    courseHistory.AcaCalSectionID = 0;
                }

                if (courseHistory.Remark == null) courseHistory.Remark = txtObtainedGrade.Text + "(" + modified + ")";
                else if (courseHistory.Remark.Length == 0) courseHistory.Remark = txtObtainedGrade.Text + "(" + modified + ")";
                else courseHistory.Remark += ", " + txtObtainedGrade.Text + "(" + modified + ")";

                courseHistory.ModifiedBy = modified;
                courseHistory.ModifiedDate = DateTime.Now;

                bool resultUpdate = StudentCourseHistoryManager.Update(courseHistory);

                if (resultUpdate)
                {
                    #region Log Insert

                    LogGeneralManager.Insert(
                                                         DateTime.Now,
                                                         "",
                                                         ddlAcademicCalender.SelectedItem.Text,
                                                         userObj.LogInID,
                                                         "",
                                                         "",
                                                         "Student Running Course Drop",
                                                         userObj.LogInID + " Dropped " + ddlCourse.SelectedItem.Text + " Course for semester " + ddlAcademicCalender.SelectedItem.Text,
                                                         userObj.LogInID + " is Dropped Course",
                                                          ((int)CommonEnum.PageName.StudentCourseDrop).ToString(),
                                                         CommonEnum.PageName.StudentCourseDrop.ToString(),
                                                         _pageUrl,
                                                         txtStudentId.Text);
                    #endregion
                    lblMsg.Text = "Course Dropped Successfully";
                    Load_Click(null, null);
                }
                else lblMsg.Text = "Course Drop fail";
            }
            catch { }
            finally { }
        }

        #endregion
    }
}