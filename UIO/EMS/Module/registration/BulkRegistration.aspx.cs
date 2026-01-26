using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessObjects.DTO;


namespace EMS.miu.registration
{
    public partial class BulkRegistration : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        #region Function

        protected void Page_Load(object sender, EventArgs e)
        {
            //base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            if (!IsPostBack)
            {
                ddlAutoOpen.SelectedValue = "0";
                //ddlPreRegistration.SelectedValue = "0";
                //chkPriority.Checked = true;
                LoadCourse();
            }
            //txtStudentName.Text = string.Empty;
        }

        protected void LoadCourse()
        {
            try
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Select", "0"));

                List<Course> courseList = CourseManager.GetAll();
                if (courseList.Count > 0 && courseList != null)
                {
                    foreach (Course course in courseList)
                    {
                        string valueField = Convert.ToString(course.CourseID); //+"_" + course.VersionID;
                        string textField = "[" + course.FormalCode + "]-" + course.Title;
                        ddlCourse.Items.Add(new ListItem(textField, valueField));
                    }
                }
            }
            catch (Exception ex)
            {
                //lblMsg.Text = ex.Message;
            }
        }

        #endregion

        #region Event

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string[] temp = new string[2];
                string[] courseName = new string[2];
                if (ddlCourse.SelectedValue != "0")
                {
                    temp = ddlCourse.SelectedItem.Text.Split(']');
                    courseName = temp[0].Split('[');

                }


                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);
                string newBatchId;
                if (batchId == 0)
                {
                    newBatchId = "";
                }
                else
                {
                    newBatchId = Convert.ToString(batchId);
                }
                int semesterId = Convert.ToInt32(ucSession.selectedValue);
                int courseId = Convert.ToInt32(ddlCourse.SelectedValue);

                string studentRoll = txtStudentId.Text;
                int autoOpen = ddlAutoOpen.SelectedItem.Text == "X" ? 2 : Convert.ToInt32(ddlAutoOpen.SelectedValue) - 1;
              
                List<ClassForceOperation> forceOperationList = ClassForceOperationManager.GetAllByParameters(programId, newBatchId, semesterId, courseId, studentRoll);

                if (forceOperationList.Count > 0 && forceOperationList != null)
                    forceOperationList = forceOperationList.OrderBy(x => x.StudentID).ToList();

                if (forceOperationList.Count > 0 && forceOperationList != null)
                    if (ddlAutoOpen.SelectedValue != "" && ddlAutoOpen.SelectedValue != "0")
                        forceOperationList = forceOperationList.Where(x => x.IsAutoOpen == Convert.ToBoolean(autoOpen)).ToList();

               
                //if (txtStudentId.Text != null)
                //{
                //    Student student = StudentManager.GetByRoll(txtStudentId.Text);
                //    if (student != null)
                //        if (student.BasicInfo != null)
                //            txtStudentName.Text = student.BasicInfo.FullName;
                //}

                if (forceOperationList.Count > 0 && forceOperationList != null)
                {
                    //if (chkPriority.Checked)
                      //  forceOperationList = forceOperationList.OrderBy(x => x.Priority).ToList();
                    gvWorkSheetGenerate.DataSource = forceOperationList;
                }
                else
                    gvWorkSheetGenerate.DataSource = null;

                gvWorkSheetGenerate.DataBind();
            }
            catch (Exception Ex)
            {
                //lblMsg.Text = Ex.Message;
            }
        }

        protected void btnAutoOpen_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvWorkSheetGenerate.Rows)
                {
                    CheckBox checkBox = (CheckBox)row.FindControl("chkIsAutoOpen");
                    if (checkBox.Checked)
                    {
                        HiddenField hiddenField = (HiddenField)row.FindControl("hdIsAutoOpen");
                        RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(Convert.ToInt32(hiddenField.Value));
                        registrationWorksheet.IsAutoOpen = true;
                        bool resultTrue = RegistrationWorksheetManager.Update(registrationWorksheet);
                    }
                    else
                    {
                        HiddenField hiddenField = (HiddenField)row.FindControl("hdIsAutoOpen");
                        RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(Convert.ToInt32(hiddenField.Value));
                        registrationWorksheet.IsAutoOpen = false;
                        bool resultTrue = RegistrationWorksheetManager.Update(registrationWorksheet);
                    }
                }
            }
            catch { }
        }

        //protected void btnIsAutoAssign_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        foreach (GridViewRow row in gvWorkSheetGenerate.Rows)
        //        {
        //            CheckBox checkBox = (CheckBox)row.FindControl("chkIsAutoAssign");
        //            if (checkBox.Checked)
        //            {
        //                HiddenField hiddenField = (HiddenField)row.FindControl("hdIsAutoAssign");
        //                RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(Convert.ToInt32(hiddenField.Value));
        //                registrationWorksheet.IsAutoAssign = true;
        //                bool resultTrue = RegistrationWorksheetManager.Update(registrationWorksheet);
        //            }
        //            else
        //            {
        //                HiddenField hiddenField = (HiddenField)row.FindControl("hdIsAutoAssign");
        //                RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(Convert.ToInt32(hiddenField.Value));
        //                registrationWorksheet.IsAutoAssign = false;
        //                bool resultTrue = RegistrationWorksheetManager.Update(registrationWorksheet);
        //            }
        //        }
        //    }
        //    catch { }
        //}

        //protected void btnMandatory_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        foreach (GridViewRow row in gvWorkSheetGenerate.Rows)
        //        {
        //            CheckBox checkBox = (CheckBox)row.FindControl("chkIsMandatory");
        //            if (checkBox.Checked)
        //            {
        //                HiddenField hiddenField = (HiddenField)row.FindControl("hdIsMandatory");
        //                RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(Convert.ToInt32(hiddenField.Value));
        //                registrationWorksheet.IsMandatory = true;
        //                bool resultTrue = RegistrationWorksheetManager.Update(registrationWorksheet);
        //            }
        //            else
        //            {
        //                HiddenField hiddenField = (HiddenField)row.FindControl("hdIsMandatory");
        //                RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(Convert.ToInt32(hiddenField.Value));
        //                registrationWorksheet.IsMandatory = false;
        //                bool resultTrue = RegistrationWorksheetManager.Update(registrationWorksheet);
        //            }
        //        }
        //    }
        //    catch { }
        //}

        protected void chkAutoOpenAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)sender;

                if (chk.Checked)
                {
                    chk.Text = "Unselect";
                }
                else
                {
                    chk.Text = "Select";
                }

                foreach (GridViewRow row in gvWorkSheetGenerate.Rows)
                {

                    CheckBox ckBox = (CheckBox)row.FindControl("chkIsAutoOpen");
                    ckBox.Checked = chk.Checked;

                }
            }
            catch (Exception ex) { }
        }

        //protected void chkIsAutoAssignAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        CheckBox chk = (CheckBox)sender;

        //        if (chk.Checked)
        //        {
        //            chk.Text = "Unselect";
        //        }
        //        else
        //        {
        //            chk.Text = "Select";
        //        }

        //        foreach (GridViewRow row in gvWorkSheetGenerate.Rows)
        //        {

        //            CheckBox ckBox = (CheckBox)row.FindControl("chkIsAutoAssign");
        //            ckBox.Checked = chk.Checked;

        //        }
        //    }
        //    catch (Exception ex) { }
        //}

        //protected void chkMandatoryAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        CheckBox chk = (CheckBox)sender;

        //        if (chk.Checked)
        //        {
        //            chk.Text = "Unselect";
        //        }
        //        else
        //        {
        //            chk.Text = "Select";
        //        }

        //        foreach (GridViewRow row in gvWorkSheetGenerate.Rows)
        //        {

        //            CheckBox ckBox = (CheckBox)row.FindControl("chkIsMandatory");
        //            ckBox.Checked = chk.Checked;

        //        }
        //    }
        //    catch (Exception ex) { }
        //}

        #endregion
        protected void btnRegistration_Click(object sender, EventArgs e)
        {
            Student student = new Student();

            try
            {
                foreach (GridViewRow row in gvWorkSheetGenerate.Rows)
                {
                    CheckBox checkBox = (CheckBox)row.FindControl("chkIsAutoOpen");
                    if (checkBox.Checked)
                    {
                        if (student.IsBlock == false)
                        {
                            //ShowLabelMessage("", false);
                            Label stdId = (Label)row.FindControl("lblStudentId");
                            Label courseCode = (Label)row.FindControl("lblCourseCode");
                            student = StudentManager.GetByRoll(stdId.Text);
                            if (student == null)
                            {
                                lblMsg.Text = "No Student Found";
                                return;
                            }

                            AcademicCalender acaCal = AcademicCalenderManager.GetActiveRegistrationCalenderByProgramId((int)student.ProgramID);

                            //SetUpDateForProgram setUpDateForProgram = SetUpDateForProgramManager.GetAll(acaCal.AcademicCalenderID, student.ProgramID, 28).FirstOrDefault();

                            //if (setUpDateForProgram != null && setUpDateForProgram.IsActive)
                            //{
                            //    DateTime _todaysDate = DateTime.Now;
                            //    if (_todaysDate < setUpDateForProgram.StartDate || _todaysDate > setUpDateForProgram.EndDate.AddHours(23).AddMinutes(59))
                            //    {
                            //        lblMsg.Text = "Registration is not allowed right now!";
                            //        return;
                            //    }
                            //}
                            //else if (setUpDateForProgram != null && setUpDateForProgram.IsActive == false)// && currentRoleId == 9)//  Block By MD Sajib Ahmed
                            //{
                            //    lblMsg.Text = "Registration is not allowed right now!";
                            //    return;
                            //}
                            //else if (setUpDateForProgram == null)
                            //{
                            //    lblMsg.Text = "Registration schedule is not set yet!";
                            //    return;
                            //}

                            List<RegistrationWorksheet> rwList = RegistrationWorksheetManager.GetByStudentID(student.StudentID);
                            //rwList = rwList.Where(l => !string.IsNullOrEmpty(l.SectionName)).ToList();
                            RegistrationWorksheet rw = rwList.Where(x => x.FormalCode == courseCode.Text).FirstOrDefault();

                            StudentCourseHistory studentCourseHistory = new StudentCourseHistory();


                            List<StudentCourseHistory> newStudentBillableCourseList = new List<StudentCourseHistory>();

                                studentCourseHistory = null;

                                if (!rw.IsRegistered)
                                {
                                    List<StudentCourseHistory> studentCourseHistoryList = StudentCourseHistoryManager.GetAllByStudentIdAcaCalId(student.StudentID, acaCal.AcademicCalenderID);

                                    studentCourseHistory = studentCourseHistoryList.Find(o => o.CourseStatusID == (int)CommonEnum.CourseStatus.Rn &&
                                                                                              o.CourseID == rw.CourseID &&
                                                                                              o.VersionID == rw.VersionID);
                                    if (studentCourseHistory == null)
                                    {
                                        studentCourseHistory = new StudentCourseHistory();

                                        studentCourseHistory.StudentID = rw.StudentID;
                                        studentCourseHistory.CourseStatusID = (int)CommonEnum.CourseStatus.Rn;
                                        studentCourseHistory.AcaCalID = rw.OriginalCalID;
                                        studentCourseHistory.CourseID = rw.CourseID;
                                        studentCourseHistory.VersionID = rw.VersionID;
                                        studentCourseHistory.CourseCredit = rw.Credits;
                                        studentCourseHistory.AcaCalSectionID = rw.AcaCal_SectionID;
                                        studentCourseHistory.CourseStatusDate = DateTime.Now;
                                        studentCourseHistory.CreatedBy = userObj.Id;
                                        studentCourseHistory.CreatedDate = DateTime.Now;
                                        studentCourseHistory.ModifiedBy = userObj.Id;
                                        studentCourseHistory.ModifiedDate = DateTime.Now;

                                        int i = StudentCourseHistoryManager.Insert(studentCourseHistory);
                                        if (i > 0)
                                        {
                                            #region Log Insert

                                            LogGeneralManager.Insert(
                                                        DateTime.Now,
                                                        "",
                                                        "",
                                                        userObj.LogInID,
                                                        "",
                                                        "",
                                                        " Student course registration ",
                                                        userObj.LogInID + " course history added for " + rw.CourseTitle + ", " + rw.FormalCode + ", " + student.Roll,
                                                        userObj.LogInID + " course registration ",
                                                        ((int)CommonEnum.PageName.Registration).ToString(),
                                                        CommonEnum.PageName.Registration.ToString(),
                                                        _pageUrl,
                                                        student.Roll);
                                            #endregion
                                            studentCourseHistory.ID = i;
                                            newStudentBillableCourseList.Add(studentCourseHistory);

                                            if (i > 0)
                                            {
                                                rw.IsRegistered = true;
                                                bool update = RegistrationWorksheetManager.Update(rw);
                                                if (update)
                                                {
                                                    #region Log Insert

                                                    LogGeneralManager.Insert(
                                                                DateTime.Now,
                                                                "",
                                                                "",
                                                                userObj.LogInID,
                                                                "",
                                                                "",
                                                                " Student course registration ",
                                                                userObj.LogInID + " course worksheet updated for " + rw.CourseTitle + ", " + rw.FormalCode + ", " + student.Roll,
                                                                userObj.LogInID + " course registration ",
                                                                ((int)CommonEnum.PageName.Registration).ToString(),
                                                                CommonEnum.PageName.Registration.ToString(),
                                                                _pageUrl,
                                                                student.Roll);
                                                    #endregion
                                                }
                                                else
                                                {
                                                    //ShowAlertMessage("Registration not completed! Please do it again.");
                                                }
                                            }
                                        }
                                    }
                                }


                                List<StudentBillDetailsDTO> studentBillDeatilList = null; // BillHistoryManager.GetStudentBillingDetails(student.StudentID, Convert.ToInt32(student.ProgramID), student.BatchId, acaCal.AcademicCalenderID); //acaCal.AcademicCalenderID
                            //decimal billAmount= 0;
                            //OpeningDue openingDueObj = OpeningDueManager.GetByStudentId(student.StudentID);
                            decimal totalfees = 0;
                            for (int i = 0; i < studentBillDeatilList.Count; i++)
                            {
                                BillHistory billHistoryObj = new BillHistory();
                                //billHistoryObj.StudentCourseHistoryId = studentBillDeatilList[i].StudentCourseHistoryId;
                                billHistoryObj.StudentId = student.StudentID;
                                billHistoryObj.AcaCalId = acaCal.AcademicCalenderID;
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
                                //    totalfees += billHistoryObj.Fees;
                                //    if (result > 0)
                                //    {

                                //        #region Log Insert

                                //        LogGeneralManager.Insert(
                                //                    DateTime.Now,
                                //                    "",
                                //                    "",
                                //                    userObj.LogInID,
                                //                    "",
                                //                    "",
                                //                    " Student bill generation ",
                                //                    userObj.LogInID + " course bill generation for " + student.Roll + " and fees amount " + billHistoryObj.Fees,
                                //                    userObj.LogInID + " bill generation ",
                                //                    ((int)CommonEnum.PageName.Registration).ToString(),
                                //                    CommonEnum.PageName.Registration.ToString(),
                                //                    _pageUrl,
                                //                    student.Roll);
                                //        #endregion

                                //    }
                                //}
                            }
                            #region Sending SMS
                            if (totalfees > 0)
                            {
                                PersonBlockDTO person = PersonBlockManager.GetByRoll(student.Roll);
                                string msg = "ID-" + student.Roll + ",your registration for " + AcademicCalenderManager.GetById(studentBillDeatilList[0].AcaCalId).FullCode + " is recorded,dues is TK. "
                                    + person.Dues.ToString() + ".Pay fees by due date to avoid registration cancellation.";
                                //SendSMS(student.BasicInfo.SMSContactSelf, student.Roll, msg);
                            }
                            #endregion
                        }

                        else
                        {
                            if (student.IsBlock == true)
                            {
                                //ShowAlertMessage("Currently you are blocked (not able to do registration), please contact with your Department or Accounts Department.");
                                return;
                            }
                        }
                    }
                    
                }
                
            }
            catch (Exception)
            {
                // lblMessage.Text = "Registration not complete.";
                //ShowAlertMessage("Registration not complete.");

            }
            //finally
            //{
            //    if (student != null)
            //        LoadStudentCourse(student.StudentID);
            //}
        }
    }
}