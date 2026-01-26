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

public partial class Admin_ManualGradeChange : BasePage
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
            LoadGrid();

            if (userObj.RoleID == 9)
            {
                User user = UserManager.GetById(userObj.Id);
                Student student = StudentManager.GetBypersonID(user.Person.PersonID);

                txtStudentId.Text = student.Roll;
                txtStudentId.ReadOnly = true;
            }
        }

        lblRegistered.Visible = false;
        // lblWaiver.Visible = false;
        // lblResult.Visible = false;
    }

    private void LoadGrid()
    {
        LoadGradeDropdown();
        LoadStatusDropdown();
    }

    private void LoadStatusDropdown()
    {
        List<CourseStatus> CourseStatusList = CourseStatusManager.GetAll();

        pnddlStatus.Items.Add(new ListItem("Select", "0"));
        pnddlStatus.AppendDataBoundItems = true;

        pnddlStatus.DataTextField = "Description";
        pnddlStatus.DataValueField = "CourseStatusID";

        if (CourseStatusList != null)
        {
            pnddlStatus.DataSource = CourseStatusList;
            pnddlStatus.DataBind();
        }
    }

    private void LoadGradeDropdown()
    {
        List<GradeDetails> GradeDetailsList = GradeDetailsManager.GetAll();

        pnddlGrade.Items.Add(new ListItem("Select", "0"));
        pnddlGrade.AppendDataBoundItems = true;

        pnddlGrade.DataTextField = "GradeWithPoint";
        pnddlGrade.DataValueField = "GradeId";

        if (GradeDetailsList != null)
        {
            pnddlGrade.DataSource = GradeDetailsList;
            pnddlGrade.DataBind();
        }
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


                    if (studentCourseHistoryList.Count > 0 && studentCourseHistoryList != null)
                    {
                        studentCourseHistoryList = studentCourseHistoryList.OrderByDescending(x => x.AcaCalID).ToList();
                        foreach (StudentCourseHistory temp in studentCourseHistoryList)
                            temp.Attribute3 = temp.YearNo.ToString() + "-" + (temp.SemesterNo % 3 == 0 ? 3 : temp.SemesterNo % 3).ToString();
                        lblRegistered.Visible = true;
                        //lblWaiver.Visible = false;
                        //  lblResult.Visible = false;

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

                                int cnt = registeredStudentCourseHistoryList.Where(x => x.CourseID == studentCourseHistory.CourseID).ToList().Count();
                                if (cnt > 1)
                                {
                                    studentCourseHistory.RetakeStatus = (cnt - 1).ToString();
                                }
                            }

                            registeredStudentCourseHistoryList = registeredStudentCourseHistoryList.OrderByDescending(r => r.AcaCalID).ToList();
                            Session["MGCCourseHistoryList"] = null;
                            Session["MGCCourseHistoryList"] = registeredStudentCourseHistoryList;
                            gvRegisteredCourse.DataSource = registeredStudentCourseHistoryList;
                            gvRegisteredCourse.DataBind();
                        }
                        else
                        {
                            gvRegisteredCourse.DataSource = null;
                            gvRegisteredCourse.DataBind();
                        }
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

    protected void gvRegisteredCourse_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            List<StudentCourseHistory> studentCourseHistoryList = (List<StudentCourseHistory>)Session["MGCCourseHistoryList"];

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


    #endregion

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
    }

    protected void lnkBtnUpdate_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id = Convert.ToInt32(linkButton.CommandArgument);
        hdnId.Value = id.ToString();

        Label lblSemester = linkButton.NamingContainer.FindControl("lblSemester") as Label;
        Label lblCourseCode = linkButton.NamingContainer.FindControl("lblCourseCode") as Label;
        Label lblCourseName = linkButton.NamingContainer.FindControl("lblCourseName") as Label;
        Label lblObtainedTotalMarks = linkButton.NamingContainer.FindControl("lblObtainedTotalMarks") as Label;
        Label lblObtainedGrade = linkButton.NamingContainer.FindControl("lblObtainedGrade") as Label;
        HiddenField hdnGradeID = linkButton.NamingContainer.FindControl("hdnGradeId") as HiddenField;
        Label lblObtainedGPA = linkButton.NamingContainer.FindControl("lblObtainedGPA") as Label;
        Label lblCourseStatus = linkButton.NamingContainer.FindControl("lblCourseStatus") as Label;
        HiddenField hdnCourseStatus = linkButton.NamingContainer.FindControl("hdnCourseStatus") as HiddenField;
        Label lblIsConsiderGPA = linkButton.NamingContainer.FindControl("lblIsConsiderGPA") as Label;
        Label lblRemarks = linkButton.NamingContainer.FindControl("lblRemarks") as Label;

        pnchkIsConsiderGPA.Checked = string.IsNullOrEmpty(lblIsConsiderGPA.Text) ? false : Convert.ToBoolean(lblIsConsiderGPA.Text);
        pnddlGrade.SelectedValue = string.IsNullOrEmpty(hdnGradeID.Value.ToString()) ? "0" : hdnGradeID.Value.ToString();
        pnddlStatus.SelectedValue = string.IsNullOrEmpty(hdnCourseStatus.Value.ToString()) ? "0" : hdnCourseStatus.Value.ToString();
        pnlblCourseID.Text = lblCourseCode.Text;
        pnlblCourseName.Text = lblCourseName.Text;
        pnlblSemester.Text = lblSemester.Text;
        pntxtTotalMark.Text = lblObtainedTotalMarks.Text;
        pnTxtRemarks.Text = lblRemarks.Text;

        List<StudentCourseHistoryReplica> previousHistory = StudentCourseHistoryReplicaManager.GetAllByCourseHistoryID(id);
        if (previousHistory != null)
        {
            gvPreviousGrade.DataSource = previousHistory.OrderByDescending(o => o.ID).ToList();
            gvPreviousGrade.DataBind();
        }


        ModalPopupExtender1.Show();
        return;
    }

    protected void btnUpdte_Click(object sender, EventArgs e)
    {
        try
        {
            int courseHistoryId = Convert.ToInt32(hdnId.Value);

            StudentCourseHistory studentCourseHistory = StudentCourseHistoryManager.GetById(courseHistoryId);
            if (studentCourseHistory != null)
            {

                #region History Replica

                StudentCourseHistoryReplica studentCourseHistoryReplica = new StudentCourseHistoryReplica();
                studentCourseHistoryReplica.StudentCourseHistoryID = studentCourseHistory.ID;
                studentCourseHistoryReplica.StudentID = studentCourseHistory.StudentID;
                studentCourseHistoryReplica.CalCourseProgNodeID = studentCourseHistory.CalCourseProgNodeID;
                studentCourseHistoryReplica.AcaCalSectionID = studentCourseHistory.AcaCalSectionID;
                studentCourseHistoryReplica.RetakeNo = studentCourseHistory.RetakeNo;
                studentCourseHistoryReplica.ObtainedTotalMarks = studentCourseHistory.ObtainedTotalMarks;
                studentCourseHistoryReplica.ObtainedGPA = studentCourseHistory.ObtainedGPA;
                studentCourseHistoryReplica.ObtainedGrade = studentCourseHistory.ObtainedGrade;
                studentCourseHistoryReplica.GradeId = studentCourseHistory.GradeId;
                studentCourseHistoryReplica.CourseStatusID = studentCourseHistory.CourseStatusID;
                studentCourseHistoryReplica.CourseStatusDate = studentCourseHistory.CourseStatusDate;
                studentCourseHistoryReplica.AcaCalID = studentCourseHistory.AcaCalID;
                studentCourseHistoryReplica.CourseID = studentCourseHistory.CourseID;
                studentCourseHistoryReplica.VersionID = studentCourseHistory.VersionID;
                studentCourseHistoryReplica.CourseCredit = studentCourseHistory.CourseCredit;
                studentCourseHistoryReplica.CompletedCredit = studentCourseHistory.CompletedCredit;
                studentCourseHistoryReplica.Node_CourseID = studentCourseHistory.Node_CourseID;
                studentCourseHistoryReplica.NodeID = studentCourseHistory.NodeID;
                studentCourseHistoryReplica.IsMultipleACUSpan = studentCourseHistory.IsMultipleACUSpan;
                studentCourseHistoryReplica.IsConsiderGPA = studentCourseHistory.IsConsiderGPA;
                studentCourseHistoryReplica.CourseWavTransfrID = studentCourseHistory.CourseWavTransfrID;
                studentCourseHistoryReplica.CreatedBy = studentCourseHistory.CreatedBy;
                studentCourseHistoryReplica.CreatedDate = studentCourseHistory.CreatedDate;
                studentCourseHistoryReplica.ModifiedBy = studentCourseHistory.ModifiedBy;
                studentCourseHistoryReplica.ModifiedDate = studentCourseHistory.ModifiedDate;
                studentCourseHistoryReplica.Remark = pnTxtRemarks.Text;

                int resultInsert = StudentCourseHistoryReplicaManager.Insert(studentCourseHistoryReplica);
                #endregion

                #region Update

                int gradeId = Convert.ToInt32(pnddlGrade.SelectedValue);
                int statusId = Convert.ToInt32(pnddlStatus.SelectedValue);
                if (gradeId != 0)
                {
                    GradeDetails gradeDetails = GradeDetailsManager.GetById(gradeId);

                    studentCourseHistory.ObtainedGPA = gradeDetails.GradePoint;
                    studentCourseHistory.ObtainedGrade = gradeDetails.Grade;
                    studentCourseHistory.GradeId = gradeDetails.GradeId;
                    if (string.IsNullOrEmpty(pntxtTotalMark.Text))
                        studentCourseHistory.ObtainedTotalMarks = null;
                    else
                        studentCourseHistory.ObtainedTotalMarks = Convert.ToDecimal(pntxtTotalMark.Text.Trim());
                }
                else
                {
                    studentCourseHistory.ObtainedGPA = null;
                    studentCourseHistory.ObtainedGrade = null;
                    studentCourseHistory.GradeId = null;
                    studentCourseHistory.ObtainedTotalMarks = null;
                }
                studentCourseHistory.IsConsiderGPA = pnchkIsConsiderGPA.Checked;
                studentCourseHistory.CourseStatusID = statusId;
                studentCourseHistory.Remark = "";

                studentCourseHistory.ModifiedBy = BaseCurrentUserObj.Id;
                studentCourseHistory.ModifiedDate = DateTime.Now;

                bool resultUpdate = StudentCourseHistoryManager.Update(studentCourseHistory);
                #endregion

                if (resultUpdate)
                {
                    lblMsg.Text = "Successfully grade changed";
                    ClearGrid();
                    ModalPopupExtender1.Hide();

                    #region Log Insert

                    LogGeneralManager.Insert(
                                DateTime.Now,
                                "",
                                "",
                                userObj.LogInID,
                                studentCourseHistory.FormalCode,
                                "",
                                " Grade Change ",
                                " Change Grade # Grade: " +
                                                    studentCourseHistory.ObtainedGrade + ", GPA: " +
                                                    studentCourseHistory.ObtainedGPA + ", Marks: " +
                                                    studentCourseHistory.ObtainedTotalMarks + ", IsConsiderGPA :" +
                                                    studentCourseHistory.IsConsiderGPA,
                                userObj.LogInID + " course registration ",
                                "",
                                "",
                                _pageUrl,
                                txtStudentId.Text.Trim());
                    #endregion

                    btnLoad_Click(null, null);
                    pnddlGrade.SelectedValue = "0";
                    pnddlStatus.SelectedValue = "0";
                }
            }
        }
        catch { }
    }

}