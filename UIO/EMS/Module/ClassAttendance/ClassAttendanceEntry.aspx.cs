using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ClassAttendanceEntry : BasePage
{
    string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
    string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.ClassAttendanceEntry);
    string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.ClassAttendanceEntry));

    int userId=0;

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
        User user = UserManager.GetByLogInId(loginID);
        if (user != null)
            userId = user.User_ID;
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            txtAttendanceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            SetUserInfoInSession();
            ucProgram.LoadDropdownWithUserAccess(userId);
            //LoadComboBox();
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

    //protected void AcademicCalender_Changed(Object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
    //        LoadAcaCalSection(acaCalId);
    //    }
    //    catch { }
    //}

    //protected void CalenderType_Changed(Object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int calenderTypeId = Convert.ToInt32(ddlCalenderType.SelectedValue);
    //        LoadAcademicCalender(calenderTypeId);
    //    }
    //    catch { }
    //}

    //protected void Program_Changed(Object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
    //        LoadAcaCalSection(acaCalId);
    //    }
    //    catch { }
    //    finally { }
    //}

    //protected void LoadComboBox()
    //{
    //    try
    //    {
    //        ddlAcademicCalender.Items.Clear();
    //        ddlAcademicCalender.Items.Add(new ListItem("Select", "0"));
    //        ddlAcaCalSection.Items.Clear();
    //        ddlAcaCalSection.Items.Add(new ListItem("Select", "0"));
    //        LoadProgram();
    //        LoadCalenderType();
    //        LoadAcaCalSection(Convert.ToInt32(ddlAcademicCalender.SelectedValue));
    //    }
    //    catch { }
    //    finally { }
    //}

    //protected void LoadCalenderType()
    //{
    //    try
    //    {
    //        ddlCalenderType.Items.Clear();
    //        //ddlCalenderType.Items.Add(new ListItem("Select", "0"));
    //        //ddlCalenderType.AppendDataBoundItems = true;

    //        List<CalenderUnitMaster> calenderUnitMasterList = CalenderUnitMasterManager.GetAll();

    //        if (calenderUnitMasterList.Count > 0 && calenderUnitMasterList != null)
    //        {
    //            ddlCalenderType.DataValueField = "CalenderUnitMasterID";
    //            ddlCalenderType.DataTextField = "Name";
    //            ddlCalenderType.DataSource = calenderUnitMasterList;
    //            ddlCalenderType.DataBind();
    //        }
    //    }
    //    catch { }
    //    finally
    //    {
    //        int calenderTypeId = Convert.ToInt32(ddlCalenderType.SelectedValue);
    //        LoadAcademicCalender(calenderTypeId);
    //    }
    //}

    //protected void LoadAcademicCalender(int calenderTypeId)
    //{
    //    try
    //    {
    //        ddlAcademicCalender.Items.Clear();
    //        ddlAcademicCalender.Items.Add(new ListItem("Select", "0"));
    //        ddlAcademicCalender.AppendDataBoundItems = true;

    //        List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll(calenderTypeId);

    //        if (academicCalenderList.Count > 0 && academicCalenderList != null)
    //        {
    //            foreach (AcademicCalender academicCalender in academicCalenderList)
    //                ddlAcademicCalender.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));

    //            academicCalenderList = academicCalenderList.Where(x => x.IsCurrent == true).ToList();
    //            ddlAcademicCalender.SelectedValue = academicCalenderList[0].AcademicCalenderID.ToString();
    //        }
    //    }
    //    catch { }
    //}

    //protected void LoadProgram()
    //{
    //    try
    //    {
    //        ddlProgram.Items.Clear();
    //        ddlProgram.AppendDataBoundItems = true;

    //        List<Program> programList = ProgramManager.GetAll();

    //        if (programList != null)
    //        {
    //            ddlProgram.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
    //            ddlProgram.DataValueField = "ProgramID";
    //            ddlProgram.DataTextField = "ShortName";
    //            ddlProgram.DataBind();
    //        }
    //    }
    //    catch { }
    //    finally { }
    //}

    //protected void LoadAcaCalSection(int acaCalId)
    //{
    //    try
    //    {
    //        ddlAcaCalSection.Items.Clear();
    //        ddlAcaCalSection.Items.Add(new ListItem("Select", "0"));
    //        ddlProgram.AppendDataBoundItems = true;

    //        int employeeId = 0;
    //        //HttpCookie aCookie = Request.Cookies[ConstantValue.Cookie_Authentication];
    //        //string uid = aCookie["UserName"];
    //        //string pwd = aCookie["UserPassword"];

    //        string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();

    //        User user = UserManager.GetByLogInId(loginID);
    //        if (user != null)
    //        {
    //            if (user.Person != null)
    //            {
    //                if (user.Person.Employee != null)
    //                    employeeId = user.Person.Employee.EmployeeID;
    //            }
    //        }

    //        List<AcademicCalenderSection> acaCalSectionList = AcademicCalenderSectionManager.GetAllByAcaCalId(acaCalId);
    //        if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
    //            acaCalSectionList = acaCalSectionList.Where(x => x.ProgramID == Convert.ToInt32(ddlProgram.SelectedValue)).ToList();
    //        if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
    //        {
    //            //if (acaCalId != 0)
    //            //    acaCalSectionList = acaCalSectionList.Where(x => x.AcademicCalenderID == acaCalId && x.AcaCal_SectionID == 1171).ToList();

    //            if (Session["Role"].ToString().Contains("Faculty") || Session["Role"].ToString().Contains("Coordinator"))
    //            {
    //                if (employeeId != 0)
    //                    acaCalSectionList = acaCalSectionList.Where(x => x.TeacherOneID == employeeId || x.TeacherTwoID == employeeId).ToList();
    //                else
    //                    acaCalSectionList = null;
    //            }
    //            else if (!Session["Role"].ToString().Contains("Admin") && !Session["Role"].ToString().Contains("Exam") && !Session["Role"].ToString().Contains("Controller"))
    //            {
    //                acaCalSectionList = null;
    //            }

    //            if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
    //            {
    //                List<Course> courseList = CourseManager.GetAll();
    //                Hashtable hashCourse = new Hashtable();
    //                foreach (Course course in courseList)
    //                    hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.FormalCode + ":" + course.Title);

    //                Dictionary<string, string> dicAcaCalSec = new Dictionary<string, string>();
    //                foreach (AcademicCalenderSection acaCalSection in acaCalSectionList)
    //                {
    //                    string courseVersion = acaCalSection.CourseID.ToString() + "_" + acaCalSection.VersionID.ToString();
    //                    try { dicAcaCalSec.Add(hashCourse[courseVersion] + "(" + acaCalSection.SectionName + ") ", acaCalSection.AcaCal_SectionID.ToString()); }
    //                    catch { }
    //                }
    //                //var acaCalSecList = dicAcaCalSec.Where(c => c.Key.ToUpper().Contains(searchKey.ToUpper())).OrderBy(x => x.Key).ToList();
    //                //var acaCalSecList = dicAcaCalSec.OrderBy(x => x.Key).ToList();
    //                var acaCalSecList = dicAcaCalSec.Where(c => c.Key.ToUpper().Contains(txtSearch.Text.ToUpper())).OrderBy(x => x.Key).ToList();
    //                foreach (var temp in acaCalSecList)
    //                    ddlAcaCalSection.Items.Add(new ListItem(temp.Key, temp.Value));
    //            }
    //        }

    //    }
    //    catch { }
    //    finally { }
    //}

    protected void btnLoad_Click(object sender, EventArgs e)
    {

        if (ddlAcaCalSection.SelectedValue == "0")
            return;
        int acaCalSectionId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);
  
        DateTime attendanceDate = DateTime.ParseExact(txtAttendanceDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);

        LoadData(acaCalSectionId, attendanceDate);

    }
    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }
    protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(ucProgram.selectedValue);
        int acaCalId = Convert.ToInt32(ucSession.selectedValue);

        FillAcaCalSectionCombo(acaCalId, programId, "");
    }

    void FillAcaCalSectionCombo(int acaCalId, int programId, string searchKey)
    {
        try
        {
            BussinessObject.UIUMSUser userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            List<AcademicCalenderSection> acaCalSectionList = AcademicCalenderSectionManager.GetAll();
            User user = UserManager.GetByLogInId(userObj.LogInID);
            if (user.Person != null)
            {
                Employee empObj = EmployeeManager.GetByPersonId(user.Person.PersonID);

                if (empObj != null && empObj.EmployeeID != 2)
                {
                    acaCalSectionList = acaCalSectionList.Where(x => x.TeacherOneID == empObj.EmployeeID || x.TeacherThreeID == empObj.EmployeeID || x.TeacherTwoID == empObj.EmployeeID).ToList();
                }
            }
            if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
            {
                ddlAcaCalSection.Items.Clear();
                ddlAcaCalSection.Items.Add(new ListItem("Select", "0"));

                if (acaCalId != 0 && programId != 0)
                    acaCalSectionList = acaCalSectionList.Where(x => x.AcademicCalenderID == acaCalId && (x.ProgramID == programId)).ToList();
                else if (acaCalId == 0)
                    acaCalSectionList = acaCalSectionList.Where(x => x.ProgramID == programId).ToList();
                else if (programId == 0)
                    acaCalSectionList = acaCalSectionList.Where(x => x.AcademicCalenderID == acaCalId).ToList();

                if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
                {
                    List<Course> courseList = CourseManager.GetAll();
                    Hashtable hashCourse = new Hashtable();
                    foreach (Course course in courseList)
                        hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.Title + ":" + course.FormalCode);

                    //acaCalSectionList = acaCalSectionList.OrderBy(x => x.CourseID).ThenBy(x => x.VersionID).ToList();
                    Dictionary<string, string> dicAcaCalSec = new Dictionary<string, string>();
                    foreach (AcademicCalenderSection acaCalSection in acaCalSectionList)
                    {
                        string courseVersion = acaCalSection.CourseID.ToString() + "_" + acaCalSection.VersionID.ToString();
                        //ddlAcaCalSection.Items.Add(new ListItem(hashCourse[courseVersion] + "(" + acaCalSection.SectionName + ") ", acaCalSection.AcaCal_SectionID.ToString()));
                        try
                        {
                            dicAcaCalSec.Add(hashCourse[courseVersion] + "(" + acaCalSection.SectionName + ") ", acaCalSection.AcaCal_SectionID.ToString());
                        }
                        catch { }
                    }
                    var acaCalSecList = dicAcaCalSec.Where(c => c.Key.ToUpper().Contains(searchKey.ToUpper())).OrderBy(x => x.Key).ToList();
                    foreach (var temp in acaCalSecList)
                        ddlAcaCalSection.Items.Add(new ListItem(temp.Key, temp.Value));
                }
            }
        }
        catch { }
    }
    protected void rdoHeaderIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvheader = gvStudentlists.HeaderRow;
        RadioButtonList rdoStatusAll = (RadioButtonList)gvheader.FindControl("rdoStatusAll");
        if (rdoStatusAll.SelectedValue == "1")
        {
            foreach (GridViewRow gvrow in gvStudentlists.Rows)
            {
                RadioButtonList rdoStatus = (RadioButtonList)gvrow.FindControl("rdoStatus");
                rdoStatus.SelectedValue = "1";
            }
        }
        else
        {
            foreach (GridViewRow gvrow in gvStudentlists.Rows)
            {
                RadioButtonList rdoStatus = (RadioButtonList)gvrow.FindControl("rdoStatus");
                rdoStatus.SelectedValue = "2";
            }
        }
    }

    private void LoadData(int acaCalSectionId,DateTime attendanceDate)
    {
        btnSaveAll.Visible = false;
        Button1.Visible = false;
        try
        {
            if (acaCalSectionId == 0)
                return;

            List<ClassAttendance> list = ClassAttendanceManager.GetAllByAcaCalSectionDate(acaCalSectionId, attendanceDate);

            if (list != null && list.Count > 0)
            {
                list = list.OrderBy(x => x.Roll).ToList();
                btnSaveAll.Visible = true;
                Button1.Visible = true;
                gvStudentlists.DataSource = list;
                gvStudentlists.DataBind();

                ShowMessage("", Color.Red);
            }
            else
            {
                ShowMessage("No Data Found", Color.Red);
                btnSaveAll.Visible = false;
                Button1.Visible = false;
            }


        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, Color.Red);
        }

    }

    private void ShowMessage(String Message, Color color)
    {
        lblMsg.Text = Message;
        lblMsg.ForeColor = color;

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int insertC = 0;
            int updateC = 0;
            int acaCalSectionId = Convert.ToInt16(ddlAcaCalSection.SelectedValue);
     
            DateTime attendanceDate = DateTime.ParseExact(txtAttendanceDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);

            foreach (GridViewRow gvrow in gvStudentlists.Rows)
            {
                Label hdnStdId = (Label)gvrow.FindControl("lblStudentId");
                Label hdnSCAId = (Label)gvrow.FindControl("lblAcaCalSecId");
                Label hdnCreatedBy = (Label)gvrow.FindControl("lblCreatedBy");
                RadioButtonList rdoStatus = (RadioButtonList)gvrow.FindControl("rdoStatus");
                //TextBox Comment = (TextBox)gvrow.FindControl("txtComment");

                int studentId = Convert.ToInt32(hdnStdId.Text);
                int stdAcaCalSecId = Convert.ToInt32(hdnSCAId.Text);
                int status = Convert.ToInt32(rdoStatus.SelectedItem.Value);
                int createdBy = Convert.ToInt32(hdnCreatedBy.Text);


                ClassAttendance _Attendance = null;
                if (createdBy == 0)
                {
                    _Attendance = new ClassAttendance();
                }
                else
                {
                    _Attendance = ClassAttendanceManager.GetByDateStudentIDAcaCalSecIDate(studentId, stdAcaCalSecId, attendanceDate);
                }

                if (createdBy == 0)
                {

                    _Attendance.AcaCalSectionID = stdAcaCalSecId;
                    _Attendance.AttendanceDate = attendanceDate;
                    _Attendance.StudentId = studentId;
                    _Attendance.StatusID = status;
                    //_Attendance.Comment = Comment.Text;
                    _Attendance.CreatedBy = userId;
                    _Attendance.CreatedDate = DateTime.Now;

                    int id = ClassAttendanceManager.Insert(_Attendance);

                    if (id != 0)
                    {
                        insertC++;
                        try
                        {
                            Student studentObj = StudentManager.GetById(studentId);
                            #region Log Insert
                            LogGeneralManager.Insert(
                                                                 DateTime.Now,
                                                                 BaseAcaCalCurrent.Code,
                                                                 BaseAcaCalCurrent.FullCode,
                                                                 BaseCurrentUserObj.LogInID,
                                                                 "",
                                                                 "",
                                                                 "Attendance Insert",
                                                                 BaseCurrentUserObj.LogInID + " inserted attendance for Roll : " + studentObj.Roll + ", Status: " + rdoStatus.SelectedItem.Text + ", Attendance Date: "+_Attendance.AttendanceDate,
                                                                 "normal",
                                                                  ((int)CommonEnum.PageName.ClassAttendanceEntry).ToString(),
                                                                 CommonEnum.PageName.ClassAttendanceEntry.ToString(),
                                                                 _pageUrl,
                                                                 studentObj.Roll);
                            #endregion
                        }
                        catch { }
                    }
                }
                else
                {
                    _Attendance.StatusID = status;
                    //_Attendance.Comment = Comment.Text;
                    _Attendance.ModifiedBy = userId;
                    _Attendance.ModifiedDate = DateTime.Now;

                    bool isUpdate = ClassAttendanceManager.Update(_Attendance);

                    if (isUpdate)
                    {
                        updateC++;
                        try
                        {
                            Student studentObj = StudentManager.GetById(studentId);
                            #region Log Insert
                            LogGeneralManager.Insert(
                                                                 DateTime.Now,
                                                                 BaseAcaCalCurrent.Code,
                                                                 BaseAcaCalCurrent.FullCode,
                                                                 BaseCurrentUserObj.LogInID,
                                                                 "",
                                                                 "",
                                                                 "Attendance Update",
                                                                 BaseCurrentUserObj.LogInID + " updated attendance for Roll : " + studentObj.Roll + ", Status: " + rdoStatus.SelectedItem.Text + ", Attendance Date: " + _Attendance.AttendanceDate,
                                                                 "normal",
                                                                  ((int)CommonEnum.PageName.ClassAttendanceEntry).ToString(),
                                                                 CommonEnum.PageName.ClassAttendanceEntry.ToString(),
                                                                 _pageUrl,
                                                                 studentObj.Roll);
                            #endregion
                        }
                        catch { }
                    }
                }

            }


            LoadData(acaCalSectionId, attendanceDate);
            ShowMessage(insertC + " row Inserted. " + updateC + " row Updated.", Color.Green);

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, Color.Red);
        }
    }

    //protected void Search_Click(Object sender, EventArgs e)
    //{
    //    try
    //    {
    //        LoadAcaCalSection(Convert.ToInt32(ddlAcademicCalender.SelectedValue));
    //    }
    //    catch { }
    //    finally { }
    //}

   
    protected void txtAttendanceDate_TextChanged(object sender, EventArgs e)
    {
        int acaCalSectionId = Convert.ToInt16(ddlAcaCalSection.SelectedValue);
     
        DateTime attendanceDate = DateTime.ParseExact(txtAttendanceDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);

        LoadData(acaCalSectionId, attendanceDate);

    }


}
