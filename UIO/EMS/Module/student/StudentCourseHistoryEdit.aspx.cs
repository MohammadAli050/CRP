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

public partial class StudentCourseHistoryEdit : BasePage
{
    BussinessObject.UIUMSUser userObj = null;
    string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        if (!IsPostBack)
        {
            if (userObj.RoleID == 9)
            {
                User user = UserManager.GetById(userObj.Id);
                Student student = StudentManager.GetBypersonID(user.Person.PersonID);

                txtStudentId.Text = student.Roll;
                txtStudentId.ReadOnly = true;
            }
        }

        lblRegistered.Visible = false;
        lblWaiver.Visible = false;
        lblResult.Visible = false;
    }

    private void Sort(List<StudentCourseHistory> list, String sortBy, String sortDirection)
    {
        if (sortDirection == "ASC")
        {
            list.Sort(new GenericComparer<StudentCourseHistory>(sortBy, (int)SortDirection.Ascending));
        }
        else
        {
            list.Sort(new GenericComparer<StudentCourseHistory>(sortBy, (int)SortDirection.Descending));
        }
        gvRegisteredCourse.DataSource = list;
        gvRegisteredCourse.DataBind();
    }

    private void CleareTxtField()
    {
        txtStudentId.Text = "";
        lblStudentName.Text = "";
        lblStudentBatch.Text = "";
        lblStudentProgram.Text = "";
    }

    private void ClearGrid()
    {
        //gvResult.DataSource = null;
        //gvResult.DataBind();
        gvRegisteredCourse.DataSource = null;
        gvRegisteredCourse.DataBind();
        //gvWaiVeredCourse.DataSource = null;
        //gvWaiVeredCourse.DataBind();
    }
    
    #endregion

    #region Event

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            string studentId = txtStudentId.Text;
                Student student = StudentManager.GetByRoll(studentId);
                if (student != null)
                {
                    if (ProgramAccessAuthentication(userObj, student.ProgramID))
                    {
                        if (student.Block != null)
                        {
                            if (student.Block.IsResultBlock == true)
                            {
                                lblMsg.Text = "ID: " + studentId.ToString() + "'s result is blocked because of DUES. (pay dues before deadline to avoid LATE FINE)";
                                return;
                            }
                        }

                        lblMsg.Text = "";
                        string batchFullName = "";
                        string majorNodeName = "not assign";

                        if (student.Major1NodeID != null && student.Major1NodeID != 0)
                        {
                            Node node = NodeManager.GetById(student.Major1NodeID);
                            if (node != null)
                                majorNodeName = node.Name;
                        }

                        Person person = PersonManager.GetById(student.PersonID);
                        if (person != null)
                        {
                            int acaCalId = student.Batch.AcaCalId;
                            AcademicCalender acaCal = AcademicCalenderManager.GetById(acaCalId);
                            if (acaCal != null)
                            {
                                CalenderUnitType calUnitType = CalenderUnitTypeManager.GetById(acaCal.CalenderUnitTypeID);
                                if (calUnitType != null)
                                    batchFullName = calUnitType.TypeName + " " + acaCal.Year;
                            }
                            lblStudentName.Text = person.FullName;
                            lblStudentBatch.Text = "[" + student.Batch.BatchNO.ToString().PadLeft(3, '0') + "] " + batchFullName;
                            lblStudentProgram.Text = student.Program.ShortName;
                            lblStudentMajor.Text = majorNodeName;
                        }
                        else
                        {
                            lblStudentName.Text = "-------";
                        }

                        List<StudentCourseHistory> studentCourseHistoryList = StudentCourseHistoryManager.GetAllByStudentId(student.StudentID);
                        List<StudentCourseHistory> registeredStudentCourseHistoryList = null;
                        //List<StudentCourseHistory> waiverStudentCourseHistoryList = null;

                        if (studentCourseHistoryList.Count > 0 && studentCourseHistoryList != null)
                        {
                            studentCourseHistoryList = studentCourseHistoryList.OrderByDescending(x => x.AcaCalID).ToList();
                            foreach (StudentCourseHistory temp in studentCourseHistoryList)
                                temp.Attribute3 = temp.YearNo.ToString() + "-" + (temp.SemesterNo % 3 == 0 ? 3 : temp.SemesterNo % 3).ToString();
                            lblRegistered.Visible = true;
                            lblWaiver.Visible = false;
                            lblResult.Visible = false;

                            #region Hashing
                            List<Course> courseList = CourseManager.GetAll();
                            Hashtable courseHashCourseCode = new Hashtable();
                            Hashtable courseHashCourseName = new Hashtable();
                            foreach (Course course in courseList)
                            {
                                courseHashCourseCode.Add(course.CourseID + "_" + course.VersionID, course.VersionCode);
                                courseHashCourseName.Add(course.CourseID + "_" + course.VersionID, course.Title);
                            }

                            List<CalenderUnitType> calUnitTypeList = CalenderUnitTypeManager.GetAll();
                            Hashtable calUnitTypeHash = new Hashtable();
                            foreach (CalenderUnitType calUnitType in calUnitTypeList)
                                calUnitTypeHash.Add(calUnitType.CalenderUnitTypeID, calUnitType.TypeName);

                            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
                            Hashtable acaCalHash = new Hashtable();
                            foreach (AcademicCalender acaCal in academicCalenderList)
                                acaCalHash.Add(acaCal.AcademicCalenderID, (calUnitTypeHash[acaCal.CalenderUnitTypeID] == null ? "" : calUnitTypeHash[acaCal.CalenderUnitTypeID]) + " " + acaCal.Year.ToString());

                            List<CourseStatus> courseStatusList = CourseStatusManager.GetAll();
                            Hashtable courseStatusHash = new Hashtable();
                            foreach (CourseStatus courseStatus in courseStatusList)
                                courseStatusHash.Add(courseStatus.CourseStatusID, courseStatus.Code);
                            #endregion

                            #region Registered Course
                            //registeredStudentCourseHistoryList = studentCourseHistoryList.Where(x => x.CourseWavTransfrID == null || x.CourseWavTransfrID == 0).ToList();
                            registeredStudentCourseHistoryList = studentCourseHistoryList.Where(x => x.CourseStatusID != 6).ToList();
                            if (registeredStudentCourseHistoryList.Count > 0 && registeredStudentCourseHistoryList != null)
                            {
                                registeredStudentCourseHistoryList = registeredStudentCourseHistoryList.OrderBy(x => x.AcaCalID).ToList();
                                foreach (StudentCourseHistory studentCourseHistory in registeredStudentCourseHistoryList)
                                {
                                    if (acaCalHash.ContainsKey(studentCourseHistory.AcaCalID))
                                        studentCourseHistory.Semester = acaCalHash[studentCourseHistory.AcaCalID].ToString();
                                    if (courseHashCourseCode.ContainsKey(studentCourseHistory.CourseID + "_" + studentCourseHistory.VersionID))
                                        studentCourseHistory.CourseCode = courseHashCourseCode[studentCourseHistory.CourseID + "_" + studentCourseHistory.VersionID].ToString();
                                    if (courseHashCourseName.ContainsKey(studentCourseHistory.CourseID + "_" + studentCourseHistory.VersionID))
                                        studentCourseHistory.CourseName = courseHashCourseName[studentCourseHistory.CourseID + "_" + studentCourseHistory.VersionID].ToString();
                                    //if (studentCourseHistory.ObtainedGrade == null && courseStatusHash.ContainsKey(studentCourseHistory.CourseStatusID))
                                    //    studentCourseHistory.CourseStatus = courseStatusHash[studentCourseHistory.CourseStatusID].ToString();
                                    int cnt = registeredStudentCourseHistoryList.Where(x => x.CourseID == studentCourseHistory.CourseID).ToList().Count();
                                    if (cnt > 1)
                                    {
                                        studentCourseHistory.RetakeStatus = (cnt - 1).ToString();
                                    }
                                }

                                registeredStudentCourseHistoryList = registeredStudentCourseHistoryList.OrderByDescending(r => r.AcaCalID).ToList();
                                Session["CourseHistoryList"] = null;
                                Session["CourseHistoryList"] = registeredStudentCourseHistoryList;
                                gvRegisteredCourse.DataSource = registeredStudentCourseHistoryList;
                                gvRegisteredCourse.DataBind();
                            }
                            else
                            {
                                gvRegisteredCourse.DataSource = null;
                                gvRegisteredCourse.DataBind();
                            }
                            #endregion

                            #region Waiverred Course
                            //waiverStudentCourseHistoryList = studentCourseHistoryList.Where(x => x.CourseWavTransfrID != null && x.CourseWavTransfrID != 0).ToList();
                            //if (waiverStudentCourseHistoryList.Count > 0 && waiverStudentCourseHistoryList != null)
                            //{
                            //    foreach (StudentCourseHistory studentCourseHistory in waiverStudentCourseHistoryList)
                            //    {
                            //        if (courseHashCourseCode.ContainsKey(studentCourseHistory.CourseID + "_" + studentCourseHistory.VersionID))
                            //            studentCourseHistory.CourseCode = courseHashCourseCode[studentCourseHistory.CourseID + "_" + studentCourseHistory.VersionID].ToString();
                            //        if (courseHashCourseName.ContainsKey(studentCourseHistory.CourseID + "_" + studentCourseHistory.VersionID))
                            //            studentCourseHistory.CourseName = courseHashCourseName[studentCourseHistory.CourseID + "_" + studentCourseHistory.VersionID].ToString();
                            //    }

                            //    gvWaiVeredCourse.DataSource = waiverStudentCourseHistoryList;
                            //    gvWaiVeredCourse.DataBind();
                            //}
                            //else
                            //{
                            //    gvWaiVeredCourse.DataSource = null;
                            //    gvWaiVeredCourse.DataBind();
                            //}
                            #endregion

                            #region Result
                            //List<StudentACUDetail> studentACUDetailList = StudentACUDetailManager.GetAllByStudentId(student.StudentID);
                            //if (studentACUDetailList.Count > 0 && studentACUDetailList != null)
                            //{
                            //    studentACUDetailList = studentACUDetailList.OrderBy(x => x.StdAcademicCalenderID).ToList();
                            //    foreach (StudentACUDetail studentACUDetail in studentACUDetailList)
                            //        if (acaCalHash.ContainsKey(studentACUDetail.StdAcademicCalenderID))
                            //            studentACUDetail.Semester = acaCalHash[studentACUDetail.StdAcademicCalenderID].ToString();
                            //    gvResult.DataSource = studentACUDetailList;
                            //    gvResult.DataBind();
                            //}
                            //else
                            //{
                            //    gvResult.DataSource = null;
                            //    gvResult.DataBind();
                            //}
                            #endregion
                        }
                        else
                        {
                            lblMsg.Text = "No Course History Found";
                        }
                    }
                    else
                    {
                        ClearGrid();
                        CleareTxtField();
                        lblMsg.Text = "Access Permission Denied.";
                        lblMsg.Focus();
                    }
                }
                else
                {
                    lblMsg.Text = "Student ID Not Found.";
                    txtStudentId.Text = "";
                    lblStudentName.Text = "";
                    lblStudentBatch.Text = "";
                    lblStudentProgram.Text = "";
                    lblStudentMajor.Text = "";
                    ClearGrid();
                }
        }
        catch { ClearGrid(); }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrow = (GridViewRow)(((Button)sender)).NamingContainer;

            DropDownList ddlYearNo = (DropDownList)gvrow.FindControl("ddlYearNo");
            DropDownList ddlSemesterNo = (DropDownList)gvrow.FindControl("ddlSemesterNo");
            CheckBox ChkIsConsiderGPA = (CheckBox)gvrow.FindControl("ChkIsConsiderGPA");
            HiddenField hdnId = (HiddenField)gvrow.FindControl("hdnId");

            StudentCourseHistory sch = StudentCourseHistoryManager.GetById(Convert.ToInt32(hdnId.Value));

            if (sch != null)
            {
                sch.YearNo = Convert.ToInt32(ddlYearNo.SelectedValue);
                sch.SemesterNo = Convert.ToInt32(ddlSemesterNo.SelectedValue);
                sch.IsConsiderGPA = ChkIsConsiderGPA.Checked == true ? true : false;

                sch.ModifiedBy = BaseCurrentUserObj.Id;
                sch.ModifiedDate = DateTime.Now;

                bool isUpdate = StudentCourseHistoryManager.Update(sch);
                if (isUpdate)
                {
                    #region Log Insert
                    LogGeneralManager.Insert(
                        DateTime.Now,
                        BaseAcaCalCurrent.Code,
                        BaseAcaCalCurrent.FullCode,
                        BaseCurrentUserObj.LogInID,
                        "",
                        "",
                        "Student Course History Edit",
                        BaseCurrentUserObj.LogInID + " Is Edited YearNo : " + sch.YearNo + " SemesterNo : " + sch.SemesterNo + " SemesterNo : " + sch.SemesterNo + " IsConsiderGPA : " + ChkIsConsiderGPA.Checked,
                        "normal",
                        ((int)CommonEnum.PageName.StudentCourseHistoryEdit).ToString(),
                        CommonEnum.PageName.StudentCourseHistoryEdit.ToString(),
                        _pageUrl,
                        sch.StudentInfo.Roll);
                    #endregion
                    lblMsg.Text = "Successfylly Updated";
                }
            }


        }
        catch (Exception)
        {
        }
    }

    protected void btnSaveAll_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            foreach (GridViewRow gvrow in gvRegisteredCourse.Rows)
            {
                DropDownList ddlYearNo = (DropDownList)gvrow.FindControl("ddlYearNo");
                DropDownList ddlSemesterNo = (DropDownList)gvrow.FindControl("ddlSemesterNo");
                CheckBox ChkIsConsiderGPA = (CheckBox)gvrow.FindControl("ChkIsConsiderGPA");
                HiddenField hdnId = (HiddenField)gvrow.FindControl("hdnId");

                StudentCourseHistory sch = StudentCourseHistoryManager.GetById(Convert.ToInt32(hdnId.Value));

                if (sch != null)
                {
                    sch.YearNo = Convert.ToInt32(ddlYearNo.SelectedValue);
                    sch.SemesterNo = Convert.ToInt32(ddlSemesterNo.SelectedValue);
                    sch.IsConsiderGPA = ChkIsConsiderGPA.Checked == true ? true : false;

                    sch.ModifiedBy = BaseCurrentUserObj.Id;
                    sch.ModifiedDate = DateTime.Now;

                    bool isUpdate = StudentCourseHistoryManager.Update(sch);
                    if (isUpdate)
                    {
                        #region Log Insert
                        LogGeneralManager.Insert(
                            DateTime.Now,
                            "",
                            "",
                            BaseCurrentUserObj.LogInID,
                            "",
                            "",
                            "Student Course History Edit",
                            BaseCurrentUserObj.LogInID + " Is Edited YearNo : " + sch.YearNo + " SemesterNo : " + sch.SemesterNo + " SemesterNo : " + sch.SemesterNo + " IsConsiderGPA : " + ChkIsConsiderGPA.Checked,
                            "normal",
                            ((int)CommonEnum.PageName.StudentCourseHistoryEdit).ToString(),
                            CommonEnum.PageName.StudentCourseHistoryEdit.ToString(),
                            _pageUrl,
                            sch.StudentInfo.Roll);
                        #endregion
                        count++;
                    }
                }

            }

            lblMsg.Text = "Successfylly Updated : " + count + " Rows ";
        }
        catch (Exception)
        {
        }
    }

    protected void gvRegisteredCourse_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            List<StudentCourseHistory> studentCourseHistoryList = (List<StudentCourseHistory>)Session["CourseHistoryList"];

            if (studentCourseHistoryList != null && studentCourseHistoryList.Count > 0)
            {
                string sortdirection = string.Empty;
                if (Session["direction"] != null)
                {
                    if (Session["direction"].ToString() == "ASC")
                    {
                        sortdirection = "DESC";
                    }
                    else
                    {
                        sortdirection = "ASC";
                    }
                }
                else
                {
                    sortdirection = "DESC";
                }
                Session["direction"] = sortdirection;
                Sort(studentCourseHistoryList, e.SortExpression.ToString(), sortdirection);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void chkSelectAllIsConsiderGPA_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in gvRegisteredCourse.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("ChkIsConsiderGPA");
                ckBox.Checked = chk.Checked;
            } 
        }
        catch (Exception ex)
        {
        }
    }

    #endregion

    
    
}