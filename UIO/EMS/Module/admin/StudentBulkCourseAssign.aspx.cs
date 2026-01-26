using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BussinessObject;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;

namespace EMS.Module.admin
{
    public partial class StudentBulkCourseAssign : BasePage
    {
        UIUMSUser userObj = null;
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
            _scriptMan.AsyncPostBackTimeout = 36000;
            if (!IsPostBack)
            {
                Session["CourseList"] = null;
                ucProgram.LoadDropDownList();

            }
            lblMsg.Text = null;
        }

        protected void btnStudentButton_Click(object sender, EventArgs e)
        {
            try
            {
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);
                int yearId = Convert.ToInt32(ddlSemester.SelectedValue);

                int compCr = string.IsNullOrEmpty(txtCompCredit.Text.Trim()) == true ? 0 :  Convert.ToInt32(txtCompCredit.Text.Trim());

                string CourseNameNew = ddlCourse.SelectedValue;

                int courseId = Convert.ToInt32(CourseNameNew.Split('_').First());
                int versionId = Convert.ToInt32(CourseNameNew.Split('_').Last());

                List<StudentInfoCourseAssign> studentList = StudentManager.GetAllRegisteredStudentByProgramYearBatchCourseCompCredit(programId, batchId, yearId, compCr, courseId, versionId);

                if (studentList != null)
                {
                    gvStudentList.DataSource = studentList;
                    gvStudentList.DataBind();

                }

                gvStudentCourse.Visible = false;
                gvStudentCourse.DataSource = null;
                gvStudentCourse.DataBind();


                btnRegistration.Visible = false;
                btnRegistrationAndBilling.Visible = false;
                btnWorkSheet.Visible = false;
            }
            catch (Exception)
            {

            }


        }
         
        protected void btnLoadAllCourse_Click(object sender, EventArgs e)
        {
            try
            {
                ddlAssignCourse.Items.Clear();
                ddlAssignCourse.Items.Add(new ListItem("Select Course", "0"));

                //List<LogicLayer.BusinessObjects.Course> courseList = CourseManager.GetAllByProgram(Convert.ToInt32(ucProgram.selectedValue)).OrderBy(d => d.FormalCode).ToList();
                List<LogicLayer.BusinessObjects.Course> courseList = CourseManager.GetAll().OrderBy(d => d.FormalCode).ToList();

                if (courseList.Count > 0 && courseList != null)
                {
                    foreach (LogicLayer.BusinessObjects.Course course in courseList)
                    {
                        string valueField = course.CourseID + "_" + course.VersionID;
                        string textField = course.FormalCode + "-" + course.Title;
                        ddlAssignCourse.Items.Add(new ListItem(textField, valueField));
                    }
                }
            }
            catch (Exception ex)
            {
                //lblMsg.Text = ex.Message;
            }
        }

        protected void btnAddCourse_Click(object sender, EventArgs e)
        {
            try
            {
                string CourseNameNew = ddlAssignCourse.SelectedValue;
                if (CourseNameNew != null)
                {
                    int courseId = Convert.ToInt32(CourseNameNew.Split('_').First());
                    int versionId = Convert.ToInt32(CourseNameNew.Split('_').Last());

                    LogicLayer.BusinessObjects.Course courseObj = CourseManager.GetByCourseIdVersionId(courseId, versionId);

                    List<LogicLayer.BusinessObjects.Course> courselist = new List<LogicLayer.BusinessObjects.Course>();
                    var courseSessionList = Session["CourseList"];
                    courselist = courseSessionList as List<LogicLayer.BusinessObjects.Course>;
                    if (courselist != null)
                    {
                        for (int i = 0; i < courselist.Count; i++)
                        {
                            if (CheckCourseExistInGrid(courselist, courseObj.CourseID))
                            {
                                lblMsg.Text = "";
                            }
                            else
                            {
                                courselist.Add(courseObj);
                                lblMsg.Text = "";
                            }
                        }
                    }
                    else
                    {
                        List<LogicLayer.BusinessObjects.Course> newCourselist = new List<LogicLayer.BusinessObjects.Course>();
                        newCourselist.Add(courseObj);
                        courselist = newCourselist;
                    }
                    gvStudentCourse.Visible = true;
                    gvStudentCourse.DataSource = courselist;
                    gvStudentCourse.DataBind();
                    //
                    btnRegistration.Visible = false;
                    //btnRegistrationAndBilling.Visible = true;
                    btnWorkSheet.Visible = true;
                    Session["CourseList"] = courselist;
                }
                else
                {
                    lblMsg.Text = "Please select a course.";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private bool CheckCourseExistInGrid(List<LogicLayer.BusinessObjects.Course> courseList, int courseId)
        {
            //LogicLayer.BusinessObjects.Course courseObj = courseList.Where(d=> d.CourseID == courseId).FirstOrDefault();
            int counter = 0;
            for (int i = 0; i < courseList.Count; i++)
            {
                if (courseList[i].CourseID == courseId)
                {
                    counter = 1;
                }
            }
            if (counter == 1)
            {
                return true;
            }
            else { return false; }
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkHeader = (CheckBox)gvStudentCourse.HeaderRow.FindControl("chkSelectAll");
                if (chkHeader.Checked)
                {
                    for (int i = 0; i < gvStudentCourse.Rows.Count; i++)
                    {
                        GridViewRow row = gvStudentCourse.Rows[i];
                        //Label studentId = (Label)row.FindControl("lblStudentId");
                        CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBox");
                        studentCheckd.Checked = true;
                    }
                }
                if (!chkHeader.Checked)
                {
                    for (int i = 0; i < gvStudentCourse.Rows.Count; i++)
                    {
                        GridViewRow row = gvStudentCourse.Rows[i];
                        //Label studentId = (Label)row.FindControl("lblStudentId");
                        CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBox");
                        studentCheckd.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void chkSelectAllStudent_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkHeader = (CheckBox)gvStudentList.HeaderRow.FindControl("chkSelectAllStudent");
                if (chkHeader.Checked)
                {
                    for (int i = 0; i < gvStudentList.Rows.Count; i++)
                    {
                        GridViewRow row = gvStudentList.Rows[i];
                        //Label studentId = (Label)row.FindControl("lblStudentId");
                        CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBox");
                        studentCheckd.Checked = true;
                    }
                }
                if (!chkHeader.Checked)
                {
                    for (int i = 0; i < gvStudentList.Rows.Count; i++)
                    {
                        GridViewRow row = gvStudentList.Rows[i];
                        //Label studentId = (Label)row.FindControl("lblStudentId");
                        CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBox");
                        studentCheckd.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnRegistration_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucSession.selectedValue == "0")
                {
                    lblMsg.Text = "Please Select Semester. Then try again.";
                    return;
                }

                foreach (GridViewRow row in gvStudentList.Rows)
                {
                    CheckBox ckBox = (CheckBox)row.FindControl("CheckBox");
                    if (ckBox.Checked)
                    {
                        Label lblStudentId = (Label)row.FindControl("lblStudentId");
                        int studentId = Convert.ToInt32(lblStudentId.Text);

                        int acaCalId = Convert.ToInt32(ucSession.selectedValue);
                        List<LogicLayer.BusinessObjects.Course> courselist = new List<LogicLayer.BusinessObjects.Course>();
                        var courseSessionList = Session["CourseList"];
                        courselist = courseSessionList as List<LogicLayer.BusinessObjects.Course>;
                        if (studentId > 0 && acaCalId != 0 && courselist.Count > 0)
                        {
                            DoStudentRegistration(studentId);
                            //ClearAll();
                        }
                        else
                        {
                            lblMsg.Text = "Please provide student roll, registartion session, and course information";
                        }
                    }
                }
                Session["CourseList"] = null;

            }
            catch (Exception ex)
            {
            }
        }

        protected void btnRegistrationAndBilling_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (ucSession.selectedValue == "0")
            //    {
            //        lblMsg.Text = "Please Select Semester. Then try again.";
            //        return;
            //    }

            //    string roll = Convert.ToString(Session["StudentRoll"]);
            //    int acaCalId = Convert.ToInt32(ucSession.selectedValue);
            //    List<LogicLayer.BusinessObjects.Course> courselist = new List<LogicLayer.BusinessObjects.Course>();
            //    var courseSessionList = Session["CourseList"];
            //    courselist = courseSessionList as List<LogicLayer.BusinessObjects.Course>;
            //    if (roll != null && acaCalId != null && courselist.Count > 0)
            //    {
            //        DoStudentRegistration();
            //        GenerateStudentBill();
            //        //ClearAll();
            //        Session["CourseList"] = null;
            //        Session["StudentRoll"] = null;
            //    }
            //    else
            //    {
            //        lblMsg.Text = "Please provide student roll, rgistartion + billing session, and course information";
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
        }

        private void DoStudentRegistration(int studentId)
        {
            try
            {
                LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetById(studentId);
                List<StudentCourseHistory> studentCourseHistoryList = StudentCourseHistoryManager.GetAllByStudentIdAcaCalId(studentObj.StudentID, Convert.ToInt32(ucSession.selectedValue));
                if (studentObj != null)
                {
                    for (int i = 0; i < gvStudentCourse.Rows.Count; i++)
                    {
                        GridViewRow row = gvStudentCourse.Rows[i];
                        Label lblCourseId = (Label)row.FindControl("lblCourseId");
                        Label lblVersionId = (Label)row.FindControl("lblVersionId");
                        Label lblCourseCredit = (Label)row.FindControl("lblCourseCredit");
                        CheckBox courseCheckd = (CheckBox)row.FindControl("CheckBox");
                        if (courseCheckd.Checked == true)
                        {
                            LogicLayer.BusinessObjects.CalenderUnitDistribution calUnitDistributionObj = new LogicLayer.BusinessObjects.CalenderUnitDistribution();
                            calUnitDistributionObj = CalenderUnitDistributionManager.GetByCourseId(Convert.ToInt32(lblCourseId.Text));

                            StudentCourseHistory studentCoursehistrory = new StudentCourseHistory();
                            studentCoursehistrory.StudentID = studentObj.StudentID;
                            studentCoursehistrory.CourseStatusID = 1; //(int)CommonEnum.CourseStatus.Pt;
                            studentCoursehistrory.AcaCalID = Convert.ToInt32(ucSession.selectedValue);
                            studentCoursehistrory.CourseID = Convert.ToInt32(lblCourseId.Text);
                            studentCoursehistrory.VersionID = Convert.ToInt32(lblVersionId.Text);
                            studentCoursehistrory.CourseCredit = Convert.ToDecimal(lblCourseCredit.Text);
                            if (calUnitDistributionObj != null)
                            {
                                if (calUnitDistributionObj.CalenderUnitMasterID == 3)
                                {
                                    //studentCoursehistrory.SemesterNo = 1;
                                    studentCoursehistrory.YearNo = calUnitDistributionObj.Sequence;
                                }
                                else
                                {
                                    int value = 2;
                                    if (calUnitDistributionObj.CalenderUnitMasterID == 1)
                                        value = 2;
                                    else if (calUnitDistributionObj.CalenderUnitMasterID == 2)
                                        value = 3;

                                    //studentCoursehistrory.SemesterNo = calUnitDistributionObj.Sequence % value == 0 ? value : calUnitDistributionObj.Sequence % value;
                                    decimal year = Math.Ceiling(Convert.ToDecimal(calUnitDistributionObj.Sequence) / Convert.ToDecimal(value));
                                    studentCoursehistrory.YearNo = Convert.ToInt32(year);
                                }
                                studentCoursehistrory.SemesterNo = calUnitDistributionObj.Sequence;
                            }
                            //studentCoursehistrory.AcaCalSectionID = item.AcaCal_SectionID;
                            studentCoursehistrory.CourseStatusDate = DateTime.Now;
                            studentCoursehistrory.CreatedBy = userObj.Id;
                            studentCoursehistrory.CreatedDate = DateTime.Now;
                            studentCoursehistrory.ModifiedBy = userObj.Id;
                            studentCoursehistrory.ModifiedDate = DateTime.Now;
                            if (IsCourseHistoryExist(studentCourseHistoryList, studentObj.StudentID, Convert.ToInt32(ucSession.selectedValue), studentCoursehistrory.CourseID, studentCoursehistrory.VersionID))
                            {
                                int result = StudentCourseHistoryManager.Insert(studentCoursehistrory);
                                if (result > 0)
                                {
                                    lblMsg.Text = "Registration done successfully";

                                    #region Log Insert
                                    LogGeneralManager.Insert(
                                                DateTime.Now,
                                                "",
                                                "",
                                                userObj.LogInID,
                                                "",
                                                "",
                                                " Student force registration ",
                                                userObj.LogInID + " course history added for " + studentCoursehistrory.CourseName + ", " + studentCoursehistrory.FormalCode + ", " + studentObj.Roll,
                                                userObj.LogInID + " course force registration ",
                                                ((int)CommonEnum.PageName.ForceRegistration).ToString(),
                                                CommonEnum.PageName.ForceRegistration.ToString(),
                                                _pageUrl,
                                                studentObj.Roll);
                                    #endregion
                                }
                                else
                                {
                                    lblMsg.Text = "Registration could not done successfully";
                                }
                            }
                            else
                            {
                                lblMsg.Text = "Course already registered";
                            }
                        }
                    }

                }
            }
            catch (Exception ex) { lblMsg.Text = ex.Message; }
        }

        private bool IsCourseHistoryExist(List<StudentCourseHistory> studentCourseHistoryList, int studentId, int acaCalId, int courseId, int versionId)
        {
            StudentCourseHistory newStudentCourseHistoryObj = studentCourseHistoryList.Where(d => d.StudentID == studentId && d.AcaCalID == acaCalId && d.CourseID == courseId && d.VersionID == versionId).FirstOrDefault();
            if (newStudentCourseHistoryObj != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void GenerateStudentBill()
        {
            try
            {
                string studentRoll = Convert.ToString(Session["StudentRoll"]);
                LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetByRoll(studentRoll);
                if (studentObj != null)
                {
                    List<StudentBillDetailsDTO> studentBillDeatilList = null;// BillHistoryManager.GetStudentBillingDetails(studentObj.StudentID, Convert.ToInt32(studentObj.ProgramID), studentObj.BatchId, Convert.ToInt32(ucSession.selectedValue));

                    for (int i = 0; i < studentBillDeatilList.Count; i++)
                    {
                        BillHistory billHistoryObj = new BillHistory();
                        //billHistoryObj.StudentCourseHistoryId = studentBillDeatilList[i].StudentCourseHistoryId;
                        billHistoryObj.StudentId = studentObj.StudentID;
                        billHistoryObj.AcaCalId = Convert.ToInt32(ucSession.selectedValue);
                        billHistoryObj.FeeTypeId = studentBillDeatilList[i].TypeDefinitionID;
                        billHistoryObj.Fees = studentBillDeatilList[i].Amount;
                        billHistoryObj.BillingDate = DateTime.Now;
                        billHistoryObj.CreatedBy = userObj.Id;
                        billHistoryObj.CreatedDate = DateTime.Now;
                        billHistoryObj.ModifiedBy = userObj.Id;
                        billHistoryObj.ModifiedDate = DateTime.Now;

                        //if (BillHistoryManager.GetStudentBillHistory(billHistoryObj.StudentCourseHistoryId, billHistoryObj.StudentId, billHistoryObj.AcaCalId, billHistoryObj.TypeDefinationId, billHistoryObj.Fees))
                        //{
                        //    int result = BillHistoryManager.Insert(billHistoryObj);

                        //    if (result > 0)
                        //    {
                        //        lblMsg.Text = "Bill generated successfully";
                        //        #region Log Insert

                        //        LogGeneralManager.Insert(
                        //                    DateTime.Now,
                        //                    "",
                        //                    "",
                        //                    userObj.LogInID,
                        //                    "",
                        //                    "",
                        //                    " Student bill generation for force registration ",
                        //                    userObj.LogInID + " course bill generation for " + studentRoll + " and fees amount " + billHistoryObj.Fees,
                        //                    userObj.LogInID + " bill generation for force registration ",
                        //                    ((int)CommonEnum.PageName.ForceRegistration).ToString(),
                        //                    CommonEnum.PageName.ForceRegistration.ToString(),
                        //                    _pageUrl,
                        //                    studentRoll);
                        //        #endregion
                        //    }
                        //    else
                        //    {
                        //        lblMsg.Text = "Bill could not generated successfully";
                        //    }
                        //}
                    }
                }
                else
                {
                    lblMsg.Text = "Bill could not generated successfully";
                }
            }
            catch (Exception ex) { lblMsg.Text = ex.Message; }
        }

        private void ClearAll()
        {
            ucSession.LoadDropDownList(0);
            ddlCourse.DataSource = null;
            ddlCourse.DataBind();
            gvStudentCourse.DataSource = null;
            gvStudentCourse.DataBind();
            btnRegistration.Visible = false;
            btnRegistrationAndBilling.Visible = false;
        }

        protected void btnWorkSheet_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucSession.selectedValue == "0")
                {
                    lblMsg.Text = "Please Select Semester. Then try again.";
                    return;
                }

                foreach (GridViewRow row in gvStudentList.Rows)
                {
                    CheckBox ckBox = (CheckBox)row.FindControl("CheckBox");
                    if (ckBox.Checked)
                    {
                        Label lblStudentId = (Label)row.FindControl("lblStudentId");
                        int studentId = Convert.ToInt32(lblStudentId.Text);
                        int acaCalId = Convert.ToInt32(ucSession.selectedValue);
                        List<LogicLayer.BusinessObjects.Course> courselist = new List<LogicLayer.BusinessObjects.Course>();
                        var courseSessionList = Session["CourseList"];
                        courselist = courseSessionList as List<LogicLayer.BusinessObjects.Course>;
                        if (studentId > 0 && acaCalId > 0 && courselist.Count > 0)
                        {
                            GenerateWorkSheetEntry(studentId);
                            //ClearAll();
                        }
                        else
                        {
                            lblMsg.Text = "Please provide student roll, rgistartion + billing session, and course information";
                        }
                    }
                }
                Session["CourseList"] = null;


            }
            catch (Exception ex)
            {
            }
        }

        private void GenerateWorkSheetEntry(int studentId)
        {
            try
            {
                LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetById(studentId);
                //List<StudentCourseHistory> studentCourseHistoryList = StudentCourseHistoryManager.GetAllByStudentIdAcaCalId(studentObj.StudentID, Convert.ToInt32(ucSession.selectedValue));
                if (studentObj != null)
                {
                    for (int i = 0; i < gvStudentCourse.Rows.Count; i++)
                    {
                        GridViewRow row = gvStudentCourse.Rows[i];
                        Label lblCourseId = (Label)row.FindControl("lblCourseId");
                        Label lblVersionId = (Label)row.FindControl("lblVersionId");
                        Label lblCourseCredit = (Label)row.FindControl("lblCourseCredit");
                        CheckBox courseCheckd = (CheckBox)row.FindControl("CheckBox");
                        if (courseCheckd.Checked == true)
                        {
                            LogicLayer.BusinessObjects.Course courseObj = CourseManager.GetByCourseIdVersionId(Convert.ToInt32(lblCourseId.Text), Convert.ToInt32(lblVersionId.Text));
                            RegistrationWorksheet regWorkSheet = new RegistrationWorksheet();
                            regWorkSheet.CourseID = courseObj.CourseID;
                            regWorkSheet.VersionID = courseObj.VersionID;
                            regWorkSheet.Credits = courseObj.Credits;
                            regWorkSheet.IsAutoOpen = true;
                            regWorkSheet.BatchID = studentObj.BatchId;
                            //regWorkSheet.Node_CourseID = nodeCourseId;
                            regWorkSheet.StudentID = studentObj.StudentID;
                            regWorkSheet.OriginalCalID = Convert.ToInt32(ucSession.selectedValue);
                            regWorkSheet.CourseTitle = courseObj.Title;
                            regWorkSheet.FormalCode = courseObj.FormalCode;
                            regWorkSheet.VersionCode = courseObj.VersionCode;
                            regWorkSheet.ProgramID = studentObj.ProgramID;
                            regWorkSheet.CreatedBy = userObj.Id;
                            regWorkSheet.CreatedDate = DateTime.Now;
                            regWorkSheet.ModifiedBy = userObj.Id;
                            regWorkSheet.ModifiedDate = DateTime.Now;
                            int result = RegistrationWorksheetManager.Insert(regWorkSheet);
                            if (result > 0)
                            {
                                lblMsg.Text = "Course inserted successfully.";
                            }
                            else
                            {
                                lblMsg.Text = "Course could not inserted successfully.";
                            }

                        }
                    }

                }
            }
            catch (Exception ex) { lblMsg.Text = ex.Message; }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
                ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            }
            catch (Exception)
            {

            }
        }

        protected void ucBatch_BatchSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);
                LoadSemesterDropDown(programId, batchId);
            }
            catch (Exception)
            {

            }
        }
 
        private void LoadSemesterDropDown(int programId, int batchId)
        {
            List<Semester> SemesterList = AcademicCalenderManager.SemesterListByProgramIdBatchId(programId, batchId);

            ddlSemester.Items.Clear();
            ddlSemester.AppendDataBoundItems = true;

            if (SemesterList != null)
            {
                // sessionList = sessionList.Where(b => b.ProgramId == programId).ToList();

                ddlSemester.Items.Add(new ListItem("-Select-", "0"));
                ddlSemester.DataTextField = "SemesterName";
                ddlSemester.DataValueField = "SemesterNo";

                ddlSemester.DataSource = SemesterList;
                ddlSemester.DataBind();
            }
        }

        protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);
                int semesterId = Convert.ToInt32(ddlSemester.SelectedValue);


                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Select Course", "0"));

                List<LogicLayer.BusinessObjects.Course> courseList = CourseManager.GetAllCourseByProgramBatchYearFromCourseHistory(programId, batchId, semesterId).ToList();

                if (courseList.Count > 0 && courseList != null)
                {
                    foreach (LogicLayer.BusinessObjects.Course course in courseList)
                    {
                        string valueField = course.CourseID + "_" + course.VersionID;
                        string textField = course.FormalCode + "-" + course.Title;
                        ddlCourse.Items.Add(new ListItem(textField, valueField));
                    }
                }
 
            }
            catch (Exception)
            { }
             
        }


    }
}