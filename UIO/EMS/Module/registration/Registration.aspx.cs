using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System.Drawing;
using LogicLayer.BusinessObjects.DTO;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace EMS.miu.registration
{
    public partial class Registration : BasePage
    {
        private string AddRegistrationWorksheetId = "AddRegistrationWorksheet";
        private string SessionStudentId = "Registration_StudentId";
        BussinessObject.UIUMSUser userObj = null;
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            if (!IsPostBack)
            {
                if (currentUserRoleId == (int)CommonEnum.Role.Student) //|| currentUserRoleId == (int)CommonEnum.Role.Coordinator)
                {
                    divTerms.Visible = false;
                    divTerms2.Visible = false;

                    btnPrint.Enabled = true;
                    btnRegistration.Enabled = false;
                    btnRetakeForwardPrint.Enabled = true;
                    btnRetakeRegistration.Enabled = false;

                    btnPrint.Visible = true;
                    btnRegistration.Visible = false;
                    btnRetakeForwardPrint.Visible = true;
                    btnRetakeRegistration.Visible = false;

                    this.gvCourseRegistration.Columns[8].Visible = false;
                    btnSaveExamCenter.Visible = true;

                    if (currentUserRoleId == (int)CommonEnum.Role.Student)
                    {
                        //pnlAddCourse.Visible = false;
                        pnlAddCourse.Visible = true;
                        Person person = PersonManager.GetByUserId(BaseCurrentUserObj.Id);
                        if (person == null)
                        {
                            ShowLabelMessage("User's profile is not uptodate. Make relation with user and person or contact with system admin.", true);
                            txtStudent.ReadOnly = true;
                            return;
                        }

                        Student student = StudentManager.GetBypersonID(person.PersonID);

                        btnLoad.Visible = false;
                        txtStudent.Enabled = false;

                        LogicLayer.BusinessObjects.StudentRegistration sr = StudentRegistrationManager.GetByStudentId(student.StudentID);

                        txtStudent.Text = sr.RegistrationNo;
                        btnLoad_Click(null, null);
                    }
                    else
                    {
                        btnLoad.Visible = true;
                        txtStudent.Enabled = true;
                        this.gvCourseHistory.Columns[9].Visible = false;

                        pnlAddCourse.Visible = true;
                    }

                }
                else
                {

                    divTerms.Visible = true;
                    divTerms2.Visible = true;

                    btnLoad.Visible = true;
                    txtStudent.Enabled = true;
                    pnlAddCourse.Visible = true;
                    btnPrint.Enabled = true;
                    btnRegistration.Enabled = true;
                    btnRetakeForwardPrint.Enabled = true;
                    btnRetakeRegistration.Enabled = true;

                    btnPrint.Visible = true;
                    btnRegistration.Visible = true;
                    btnRetakeForwardPrint.Visible = true;
                    btnRetakeRegistration.Visible = true;
                    this.gvCourseHistory.Columns[9].Visible = true;

                    if (currentUserRoleId == (int)CommonEnum.Role.Admin)
                    {
                        this.gvCourseRegistration.Columns[8].Visible = true;
                    }
                }

            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                ShowLabelMessage("", false);
                ShowLabelMessage("", true);
                CleareGrid();


                if (string.IsNullOrEmpty(txtStudent.Text))
                {
                    ShowLabelMessage("Please enter valid registration no and then click Load button to view details.", true);

                    ShowAlertMessage("Please enter valid registration no and then click Load button to view details.");
                    return;
                }
                LogicLayer.BusinessObjects.StudentRegistration sr = StudentRegistrationManager.GetByRegistrationNo(txtStudent.Text.Trim());

                if (sr != null)
                {
                    #region Exam Center Access Permission

                    StudentInstitution stdInst = StudentInstitutionManager.GetByStudentId(sr.StudentId);
                    if (stdInst != null)
                    {

                        if (currentUserRoleId != (int)CommonEnum.Role.Student && currentUserRoleId != (int)CommonEnum.Role.Admin)
                        {
                            List<UserExamCenter> userExamCenterList = UserExamCenterManager.GetAllByUserId(userObj.Id);

                            if (userExamCenterList != null && userExamCenterList.Count > 0)
                            {

                                if (userExamCenterList.Where(l => l.ExamCenterId == stdInst.ExemCenterId).ToList().Count() == 0)
                                {
                                    string info = "";

                                    if (stdInst.ExemCenterId == 0)
                                    {
                                        info = "Not Assigned.";
                                    }
                                    else
                                    {
                                        info = ExamCenterManager.GetById(stdInst.ExemCenterId).ExamCenterName;
                                    }
                                    ShowLabelMessage("Exam Center Miss-Match. Current Exam Center : " + info, true);
                                    return;
                                }
                            }
                            else
                            {
                                ShowLabelMessage("You Have No Access !", true);
                                return;
                            }


                        }

                        if (stdInst.AffiliatedInstitution != null)
                        {
                            lblInstitute.Text = stdInst.AffiliatedInstitution.Name;
                        }

                        ddlExamCenter.SelectedValue = stdInst.ExemCenterId.ToString();


                    }

                    #endregion

                    Student student = StudentManager.GetByRoll(sr.Roll);
                    if (student.IsActive == false)
                    {

                        ShowAlertMessage(" This Registration No is Inactive. Please contact with system admin.");
                        return;
                    }

                    if (student.Batch != null && student.Batch.BatchNO <= 5)
                    {
                        ShowAlertMessage(" You are not eligible for registration .");
                        return;
                    }


                    SessionManager.SaveObjToSession<int>(student.StudentID, SessionStudentId);
                    if (student != null)
                    {
                        FillStudentInfo(student);
                        SessionManager.SaveObjToSession<Student>(student, ConstantValue.Session_ForRegistrationPage_Student);
                        AcademicCalender acaCal = AcademicCalenderManager.GetActiveRegistrationCalenderByProgramId((int)student.ProgramID);
                        lblRegistrationNo.Text = txtStudent.Text;
                        lblRoll.Text = student.Roll;
                        lblPhone.Text = student.BasicInfo.SMSContactSelf;
                        LoadStudentCourse(student, acaCal);
                        LoadStudentsPreviousCourseHistory(student, acaCal);
                        LoadStudentIrregularCourse(student, acaCal);
                        LoadCourseByStudentIdTypeId();
                        LoadExemCenter();

                        lblRegistrationSession.Text = acaCal.FullCode;

                    }
                }
                else
                { }
            }
            catch (Exception ex)
            {
            }
        }

        #region Registration

        protected void btnRegistration_Click(object sender, EventArgs e)
        {
            Student student = new Student();

            try
            {
                ShowLabelMessage("", false);

                student = SessionManager.GetObjFromSession<Student>(ConstantValue.Session_ForRegistrationPage_Student); //StudentManager.GetById(studentId);
                if (student == null)
                {
                    ShowAlertMessage("No Student Found");
                    return;
                }

                if (student.Batch != null && student.Batch.BatchNO <= 5)
                {
                    ShowAlertMessage(" You are not eligible for registration .");
                    return;
                }

                AcademicCalender acaCal = AcademicCalenderManager.GetActiveRegistrationCalenderByProgramId((int)student.ProgramID);


                List<RegistrationWorksheet> rwList = RegistrationWorksheetManager.GetByStudentID(student.StudentID);
                rwList = rwList.Where(l => l.IsRegistered == false && l.IsAutoOpen == true).ToList();

                StudentCourseHistory studentCourseHistory = new StudentCourseHistory();


                List<StudentCourseHistory> newStudentBillableCourseList = new List<StudentCourseHistory>();
                int RCount = 0;
                int billCount = 0;

                foreach (RegistrationWorksheet item in rwList)
                {
                    studentCourseHistory = null;

                    if (!item.IsRegistered)
                    {
                        List<StudentCourseHistory> studentCourseHistoryList = StudentCourseHistoryManager.GetAllByStudentIdAcaCalId(student.StudentID, acaCal.AcademicCalenderID);

                        studentCourseHistory = studentCourseHistoryList.Find(o => o.CourseStatusID == (int)CommonEnum.CourseStatus.Rn &&
                                                                                  o.CourseID == item.CourseID &&
                                                                                  o.VersionID == item.VersionID);
                        if (studentCourseHistory == null)
                        {
                            studentCourseHistory = new StudentCourseHistory();

                            studentCourseHistory.StudentID = item.StudentID;
                            studentCourseHistory.CourseStatusID = (int)CommonEnum.CourseStatus.Rn;
                            studentCourseHistory.AcaCalID = item.OriginalCalID;
                            studentCourseHistory.CourseID = item.CourseID;
                            studentCourseHistory.VersionID = item.VersionID;
                            studentCourseHistory.CourseCredit = item.Credits;
                            studentCourseHistory.AcaCalSectionID = item.AcaCal_SectionID;
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
                                            item.FormalCode,
                                            "",
                                            " Student course registration ",
                                            userObj.LogInID + " course history added for " + item.CourseTitle + ", " + item.FormalCode + ", " + student.Roll,
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
                                    item.IsRegistered = true;
                                    bool update = RegistrationWorksheetManager.Update(item);
                                    if (update)
                                    {
                                        RCount++;
                                        #region Log Insert

                                        LogGeneralManager.Insert(
                                                    DateTime.Now,
                                                    "",
                                                    "",
                                                    userObj.LogInID,
                                                    item.FormalCode,
                                                    "",
                                                    " Student course registration ",
                                                    userObj.LogInID + " course worksheet updated for " + item.CourseTitle + ", " + item.FormalCode + ", " + student.Roll,
                                                    userObj.LogInID + " course registration ",
                                                    ((int)CommonEnum.PageName.Registration).ToString(),
                                                    CommonEnum.PageName.Registration.ToString(),
                                                    _pageUrl,
                                                    student.Roll);
                                        #endregion


                                        bool isDeleteFC = ForwardCoursesManager.DeleteByStudentIdCourseIdVersionIdAcaCalId(item.StudentID, item.CourseID, item.VersionID, item.OriginalCalID);
                                        string Roll = item.Roll;

                                        if (isDeleteFC)
                                        {
                                            #region Log Insert



                                            LogGeneralManager.Insert(
                                                        DateTime.Now,
                                                        "",
                                                        "",
                                                        userObj.LogInID,
                                                        item.FormalCode,
                                                        "",
                                                        " Remove From Course Forward After Registration",
                                                        userObj.LogInID + " Remove -- " + item.FormalCode + " : " + item.CourseTitle + " -- Course From Course Forward ",
                                                        userObj.LogInID + " Remove From Course Forward After Registration",
                                                        ((int)CommonEnum.PageName.Registration).ToString(),
                                                        CommonEnum.PageName.Registration.ToString(),
                                                        _pageUrl,
                                                        Roll);
                                            #endregion
                                        }

                                    }
                                    else
                                    {
                                        ShowAlertMessage("Registration not completed! Please do it again.");
                                    }
                                }
                            }
                        }
                    }
                }


                #region Sending SMS
                //if (totalfees>0)
                //{
                //    PersonBlockDTO person = PersonBlockManager.GetByRoll(student.Roll);
                //    string msg = "ID-" + student.Roll + ",your registration for " + AcademicCalenderManager.GetById(studentBillDeatilList[0].AcaCalId).FullCode + " is recorded,dues is TK. "
                //        + person.Dues.ToString() + ".Pay fees by due date to avoid registration cancellation.";
                //    SendSMS(student.BasicInfo.SMSContactSelf, student.Roll, msg);
                //}
                #endregion


                if (billCount == 0 && RCount > 0)
                {
                    ShowLabelMessage("Registration Completed !", true);
                }
                else if (billCount > 0 && RCount > 0)
                {
                    ShowLabelMessage("Registration Completed & Bill Generated Successfully !", true);
                }
                else if (billCount > 0 && RCount == 0)
                {
                    ShowLabelMessage("Bill Generated Successfully !", true);
                }
                else
                {
                    ShowLabelMessage("Nothing Happens !", true);
                }



            }
            catch (Exception)
            {
                // lblMessage.Text = "Registration not complete.";
                ShowAlertMessage("Registration not complete.");

            }
            finally
            {
                //if (student != null)
                //LoadStudentCourse(student.StudentID,0);
            }
        }

        protected void lBtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                ShowLabelMessage("", false);
                btnLoad_Click(null, null);
                //LoadStudentCourse(SessionManager.GetObjFromSession<Student>(ConstantValue.Session_ForRegistrationPage_Student).StudentID);
            }
            catch (Exception)
            {
            }
        }

        protected void btnPreRegistrationRetake_Click(object sender, EventArgs e)
        {
            int stageNo = 0;
            try
            {


                LogicLayer.BusinessObjects.StudentRegistration sr = StudentRegistrationManager.GetByRegistrationNo(txtStudent.Text.Trim());
                Student student = StudentManager.GetByRoll(sr.Roll);

                AcademicCalender acaCal = AcademicCalenderManager.GetActiveRegistrationCalenderByProgramId((int)student.ProgramID);

                if (currentUserRoleId == (int)CommonEnum.Role.Student)
                {
                    bool isRegistrationOrForwardDone = RegistrationWorksheetManager.IsForwardOrRegistrationDoneForStudent(student.StudentID, acaCal.AcademicCalenderID, 1);

                    if (isRegistrationOrForwardDone)
                    {
                        btnLoad_Click(null, null);
                        return;
                    }

                }

                LinkButton btn = (LinkButton)sender;
                int id = int.Parse(btn.CommandArgument.ToString());

                StudentCourseHistory studentCourseHistory = StudentCourseHistoryManager.GetById(id);

                stageNo = 1;

                if (studentCourseHistory != null && (studentCourseHistory.GradeId > 5 || (studentCourseHistory.GradeId <= 5 && studentCourseHistory.RegistrationWorksheet.IsAutoOpen == true && (currentUserRoleId == 1 || currentUserRoleId == 3 || currentUserRoleId == 6))))
                {
                    stageNo = 2;

                    if (studentCourseHistory.RegistrationWorksheet != null)
                    {
                        RegistrationWorksheet rw = studentCourseHistory.RegistrationWorksheet;

                        if (rw.IsAutoOpen == true)
                        {
                            rw.IsAutoOpen = false;
                        }
                        else
                        {
                            rw.IsAutoOpen = true;
                        }
                        rw.ModifiedBy = userObj.Id;
                        rw.ModifiedDate = DateTime.Now;

                        bool isUpdate = RegistrationWorksheetManager.Update(rw);
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
                                                            "Auto Assign Successfull",
                                                            userObj.LogInID + "Auto Assign Staus Change " + studentCourseHistory.FormalCode + " " + studentCourseHistory.CourseTitle + " with AutoOpen Stauts " + studentCourseHistory.RegistrationWorksheet.IsAutoOpen.ToString(),
                                                            userObj.LogInID + " ",
                                                            ((int)CommonEnum.PageName.Registration).ToString(),
                                                            CommonEnum.PageName.Registration.ToString(),
                                                            _pageUrl,
                                                            studentCourseHistory.Roll);

                            #endregion

                            if (rw.IsAutoOpen == false)
                            {
                                ForwardCoursesManager.DeleteByStudentIdCourseIdVersionIdAcaCalId(studentCourseHistory.StudentID, studentCourseHistory.CourseID, studentCourseHistory.VersionID, rw.OriginalCalID);
                            }
                        }
                        else
                        {
                            #region Log Insert
                            LogGeneralManager.Insert(
                                                            DateTime.Now,
                                                            BaseAcaCalCurrent.Code,
                                                            BaseAcaCalCurrent.FullCode,
                                                            BaseCurrentUserObj.LogInID,
                                                            "",
                                                            "",
                                                            "Auto Assign Unsuccessfull",
                                                            userObj.LogInID + "Auto Assign Staus Change " + studentCourseHistory.FormalCode + " " + studentCourseHistory.CourseTitle + " with AutoOpen Stauts " + studentCourseHistory.RegistrationWorksheet.IsAutoOpen.ToString(),
                                                            userObj.LogInID + "",
                                                            ((int)CommonEnum.PageName.Registration).ToString(),
                                                            CommonEnum.PageName.Registration.ToString(),
                                                            _pageUrl,
                                                            studentCourseHistory.Roll);

                            #endregion
                        }
                    }
                    else
                    {
                        RegistrationWorksheet regWorkSheet = new RegistrationWorksheet();
                        regWorkSheet.CourseID = studentCourseHistory.CourseID;
                        regWorkSheet.VersionID = studentCourseHistory.VersionID;
                        regWorkSheet.Credits = studentCourseHistory.CourseCredit;
                        regWorkSheet.IsAutoOpen = true;
                        regWorkSheet.BatchID = studentCourseHistory.StudentInfo.BatchId;

                        regWorkSheet.StudentID = studentCourseHistory.StudentID;
                        regWorkSheet.OriginalCalID = acaCal.AcademicCalenderID;
                        regWorkSheet.CourseTitle = studentCourseHistory.CourseTitle;
                        regWorkSheet.FormalCode = studentCourseHistory.FormalCode;
                        regWorkSheet.VersionCode = studentCourseHistory.Course.VersionCode;
                        regWorkSheet.ProgramID = studentCourseHistory.StudentInfo.ProgramID;
                        regWorkSheet.CreatedBy = userObj.Id;
                        regWorkSheet.CreatedDate = DateTime.Now;
                        regWorkSheet.ModifiedBy = userObj.Id;
                        regWorkSheet.ModifiedDate = DateTime.Now;
                        int result = RegistrationWorksheetManager.Insert(regWorkSheet);
                        if (result > 0)
                        {
                            #region Log Insert
                            LogGeneralManager.Insert(
                                                            DateTime.Now,
                                                            BaseAcaCalCurrent.Code,
                                                            BaseAcaCalCurrent.FullCode,
                                                            BaseCurrentUserObj.LogInID,
                                                            "",
                                                            "",
                                                            "Retake & Auto Assign Successfull",
                                                            userObj.LogInID + "Retake & Auto Assign Staus Change " + studentCourseHistory.FormalCode + " " + studentCourseHistory.CourseTitle + " with AutoOpen Stauts " + studentCourseHistory.RegistrationWorksheet.IsAutoOpen.ToString(),
                                                            userObj.LogInID + " ",
                                                            ((int)CommonEnum.PageName.Registration).ToString(),
                                                            CommonEnum.PageName.Registration.ToString(),
                                                            _pageUrl,
                                                            studentCourseHistory.Roll);

                            #endregion
                        }
                        else
                        {
                            #region Log Insert
                            LogGeneralManager.Insert(
                                                            DateTime.Now,
                                                            BaseAcaCalCurrent.Code,
                                                            BaseAcaCalCurrent.FullCode,
                                                            BaseCurrentUserObj.LogInID,
                                                            "",
                                                            "",
                                                            "Retake & Auto Assign Unsuccessfull",
                                                            userObj.LogInID + "Retake & Auto Assign Staus Change " + studentCourseHistory.FormalCode + " " + studentCourseHistory.CourseTitle + " with AutoOpen Stauts " + studentCourseHistory.RegistrationWorksheet.IsAutoOpen.ToString(),
                                                            userObj.LogInID + "",
                                                            ((int)CommonEnum.PageName.Registration).ToString(),
                                                            CommonEnum.PageName.Registration.ToString(),
                                                            _pageUrl,
                                                            studentCourseHistory.Roll);

                            #endregion
                        }
                    }
                }
                else
                {
                    ShowAlertMessage("You are not eligible to retake any course with grade greater than B.");
                }

                LoadStudentsPreviousCourseHistory(studentCourseHistory.StudentInfo, acaCal);

            }
            catch (Exception ex)
            {
                #region Log Insert
                LogGeneralManager.Insert(
                                                DateTime.Now,
                                                BaseAcaCalCurrent.Code,
                                                BaseAcaCalCurrent.FullCode,
                                                BaseCurrentUserObj.LogInID,
                                                "",
                                                "",
                                                "Retake Course Add",
                                                userObj.LogInID + " Trying to retake ",
                                                userObj.LogInID + "",
                                                ((int)CommonEnum.PageName.Registration).ToString(),
                                                CommonEnum.PageName.Registration.ToString(),
                                                _pageUrl,
                                                "");

                #endregion

                if (stageNo == 1)
                {
                    ShowAlertMessage(" Error : Contact with Admin. [ Couse is not Open ]");
                }
            }
        }

        protected void btnFixedCourseTake_Click(object sender, EventArgs e)
        {
            try
            {


                LinkButton btn = (LinkButton)sender;
                int id = int.Parse(btn.CommandArgument.ToString());

                RegistrationWorksheet registrationWorkSheet = RegistrationWorksheetManager.GetById(id);
                AcademicCalender acaCal = AcademicCalenderManager.GetActiveRegistrationCalenderByProgramId(registrationWorkSheet.StudentInfo.ProgramID);

                if (registrationWorkSheet != null)
                {
                    if (registrationWorkSheet.IsAutoOpen == true)
                    {
                        registrationWorkSheet.IsAutoOpen = false;
                    }
                    else
                    {
                        registrationWorkSheet.IsAutoOpen = true;
                    }
                    registrationWorkSheet.ModifiedBy = userObj.Id;
                    registrationWorkSheet.ModifiedDate = DateTime.Now;

                    bool isUpdate = RegistrationWorksheetManager.Update(registrationWorkSheet);
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
                                                        "Auto Assign Successfull",
                                                        userObj.LogInID + "Auto Assign Staus Change " + registrationWorkSheet.FormalCode + " " + registrationWorkSheet.CourseTitle + " with AutoOpen Stauts " + registrationWorkSheet.IsAutoOpen.ToString(),
                                                        userObj.LogInID + " ",
                                                        ((int)CommonEnum.PageName.Registration).ToString(),
                                                        CommonEnum.PageName.Registration.ToString(),
                                                        _pageUrl,
                                                        registrationWorkSheet.Roll);

                        #endregion
                    }
                    else
                    {
                        #region Log Insert
                        LogGeneralManager.Insert(
                                                        DateTime.Now,
                                                        BaseAcaCalCurrent.Code,
                                                        BaseAcaCalCurrent.FullCode,
                                                        BaseCurrentUserObj.LogInID,
                                                        "",
                                                        "",
                                                        "Auto Assign Unsuccessfull",
                                                        userObj.LogInID + "Auto Assign Staus Change " + registrationWorkSheet.FormalCode + " " + registrationWorkSheet.CourseTitle + " with AutoOpen Stauts " + registrationWorkSheet.IsAutoOpen.ToString(),
                                                        userObj.LogInID + "",
                                                        ((int)CommonEnum.PageName.Registration).ToString(),
                                                        CommonEnum.PageName.Registration.ToString(),
                                                        _pageUrl,
                                                        registrationWorkSheet.Roll);

                        #endregion
                    }

                }
                else
                { }
                LoadStudentCourse(registrationWorkSheet.StudentInfo, acaCal);

            }
            catch (Exception ex)
            {
                #region Log Insert
                LogGeneralManager.Insert(
                                                DateTime.Now,
                                                BaseAcaCalCurrent.Code,
                                                BaseAcaCalCurrent.FullCode,
                                                BaseCurrentUserObj.LogInID,
                                                "",
                                                "",
                                                "Course Add",
                                                userObj.LogInID + " Trying to Add New Course ",
                                                userObj.LogInID + "",
                                                ((int)CommonEnum.PageName.Registration).ToString(),
                                                CommonEnum.PageName.Registration.ToString(),
                                                _pageUrl,
                                                "");

                #endregion
            }

        }

        protected void btnIrregularCourseTake_Click(object sender, EventArgs e)
        {
            try
            {
                LogicLayer.BusinessObjects.StudentRegistration sr = StudentRegistrationManager.GetByRegistrationNo(txtStudent.Text.Trim());
                Student student = StudentManager.GetByRoll(sr.Roll);

                AcademicCalender acaCal = AcademicCalenderManager.GetActiveRegistrationCalenderByProgramId((int)student.ProgramID);

                if (currentUserRoleId == (int)CommonEnum.Role.Student)
                {
                    bool isRegistrationOrForwardDone = RegistrationWorksheetManager.IsForwardOrRegistrationDoneForStudent(student.StudentID, acaCal.AcademicCalenderID, 0);

                    if (isRegistrationOrForwardDone)
                    {
                        btnLoad_Click(null, null);
                        return;
                    }
                }

                LinkButton btn = (LinkButton)sender;
                int id = int.Parse(btn.CommandArgument.ToString());

                RegistrationWorksheet registrationWorkSheet = RegistrationWorksheetManager.GetById(id);

                if (registrationWorkSheet != null)
                {
                    if (registrationWorkSheet.IsAutoOpen == true)
                    {
                        registrationWorkSheet.IsAutoOpen = false;
                    }
                    else
                    {
                        registrationWorkSheet.IsAutoOpen = true;
                    }
                    registrationWorkSheet.ModifiedBy = userObj.Id;
                    registrationWorkSheet.ModifiedDate = DateTime.Now;

                    bool isUpdate = RegistrationWorksheetManager.Update(registrationWorkSheet);
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
                                                        "Auto Assign Successfull",
                                                        userObj.LogInID + "Auto Assign Staus Change " + registrationWorkSheet.FormalCode + " " + registrationWorkSheet.CourseTitle + " with AutoOpen Stauts " + registrationWorkSheet.IsAutoOpen.ToString(),
                                                        userObj.LogInID + " ",
                                                        ((int)CommonEnum.PageName.Registration).ToString(),
                                                        CommonEnum.PageName.Registration.ToString(),
                                                        _pageUrl,
                                                        registrationWorkSheet.Roll);

                        #endregion
                    }
                    else
                    {
                        #region Log Insert
                        LogGeneralManager.Insert(
                                                        DateTime.Now,
                                                        BaseAcaCalCurrent.Code,
                                                        BaseAcaCalCurrent.FullCode,
                                                        BaseCurrentUserObj.LogInID,
                                                        "",
                                                        "",
                                                        "Auto Assign Unsuccessfull",
                                                        userObj.LogInID + "Auto Assign Staus Change " + registrationWorkSheet.FormalCode + " " + registrationWorkSheet.CourseTitle + " with AutoOpen Stauts " + registrationWorkSheet.IsAutoOpen.ToString(),
                                                        userObj.LogInID + "",
                                                        ((int)CommonEnum.PageName.Registration).ToString(),
                                                        CommonEnum.PageName.Registration.ToString(),
                                                        _pageUrl,
                                                        registrationWorkSheet.Roll);

                        #endregion
                    }

                }
                else
                { }
                LoadStudentIrregularCourse(registrationWorkSheet.StudentInfo, acaCal);

            }
            catch (Exception ex)
            {
                #region Log Insert
                LogGeneralManager.Insert(
                                                DateTime.Now,
                                                BaseAcaCalCurrent.Code,
                                                BaseAcaCalCurrent.FullCode,
                                                BaseCurrentUserObj.LogInID,
                                                "",
                                                "",
                                                "Course Add",
                                                userObj.LogInID + " Trying to Add New Course ",
                                                userObj.LogInID + "",
                                                ((int)CommonEnum.PageName.Registration).ToString(),
                                                CommonEnum.PageName.Registration.ToString(),
                                                _pageUrl,
                                                "");

                #endregion
            }

        }

        #endregion

        #region Methods

        private void ShowRegistrationOpenMsg(string msg, bool isActivated)
        {
            lblRegistrationOpenMsg.Text = msg;
            if (isActivated)
                lblRegistrationOpenMsg.ForeColor = Color.Green;
            else
                lblRegistrationOpenMsg.ForeColor = Color.Red;
        }

        private void LoadExemCenter()
        {
            try
            {
                List<ExamCenter> list = ExamCenterManager.GetAll();

                ddlExamCenter.Items.Clear();
                ddlExamCenter.AppendDataBoundItems = true;

                if (list != null)
                {
                    ddlExamCenter.Items.Add(new ListItem("-Select-", "0"));
                    ddlExamCenter.DataTextField = "ExamCenterName";
                    ddlExamCenter.DataValueField = "Id";

                    ddlExamCenter.DataSource = list;
                    ddlExamCenter.DataBind();
                }
            }
            catch { }
            finally { }
        }

        private void CleareGrid()
        {
            lblRegistrationNo.Text = "";
            lblName.Text = "";
            lblCreditCount.Text = "";
            lblProgram.Text = "";
            lblRoll.Text = "";
            lblPhone.Text = "";
            ddlExamCenter.SelectedValue = "0";
            gvCourseRegistration.DataSource = null;
            gvCourseRegistration.DataBind();

            lblInstitute.Text = "";
            gvCourseHistory.DataSource = null;
            gvCourseHistory.DataBind();
        }

        private void FillStudentInfo(Student student)
        {
            lblProgram.Text = student.Program.ShortName;
            lblBatch.Text = student.Batch.BatchNO.ToString();
            lblName.Text = student.BasicInfo.FullName;
        }

        private void LoadStudentCourse(Student student, AcademicCalender acaCal)
        {
            List<RegistrationWorksheet> collection = RegistrationWorksheetManager.GetAllCourseByProgramSessionBatchStudentId(student.StudentID, acaCal.AcademicCalenderID);

            bool isRegistrationOrForwardDone = RegistrationWorksheetManager.IsForwardOrRegistrationDoneForStudent(student.StudentID, acaCal.AcademicCalenderID, 0);

            if (collection != null)
            {
                collection = collection.Where(c => c.CourseStatusId != -1 && c.IsOfferedCourse == false).ToList();

                gvCourseRegistration.DataSource = collection.ToList().OrderBy(c => c.FormalCode);
                gvCourseRegistration.DataBind();
            }


        }

        private void LoadStudentIrregularCourse(Student student, AcademicCalender acaCal)
        {
            List<RegistrationWorksheet> collection = RegistrationWorksheetManager.GetAllCourseByProgramSessionBatchStudentId(student.StudentID, acaCal.AcademicCalenderID);

            if (currentUserRoleId == (int)CommonEnum.Role.Student)
            {
                bool isRegistrationOrForwardDone = RegistrationWorksheetManager.IsForwardOrRegistrationDoneForStudent(student.StudentID, acaCal.AcademicCalenderID, 0);
                if (!isRegistrationOrForwardDone)
                {
                    this.gvIrregularCourses.Columns[9].Visible = true;
                }
                else
                {
                    this.gvIrregularCourses.Columns[9].Visible = false;
                }

            }
            else
            {
                this.gvIrregularCourses.Columns[9].Visible = true;
            }

            if (collection != null)
            {
                collection = collection.Where(c => c.CourseStatusId != -1 && c.IsOfferedCourse == true).ToList();

                gvIrregularCourses.DataSource = collection.ToList().OrderBy(c => c.FormalCode);
                gvIrregularCourses.DataBind();
            }


        }

        private void LoadStudentsPreviousCourseHistory(Student student, AcademicCalender acaCal)
        {
            List<StudentCourseHistory> collection = StudentCourseHistoryManager.GetDistinctCourseHistoryByStudentIdAcaCalId(student.StudentID, acaCal.AcademicCalenderID).ToList();

            if (currentUserRoleId == (int)CommonEnum.Role.Student)
            {
                bool isRegistrationOrForwardDone = RegistrationWorksheetManager.IsForwardOrRegistrationDoneForStudent(student.StudentID, acaCal.AcademicCalenderID, 1);
                if (!isRegistrationOrForwardDone)
                {
                    this.gvCourseHistory.Columns[9].Visible = true;
                }
                else
                {
                    this.gvCourseHistory.Columns[9].Visible = false;
                }

            }
            else
            {
                this.gvCourseHistory.Columns[9].Visible = true;
            }

            if (collection != null)
            {
                collection = collection.ToList();

                gvCourseHistory.DataSource = collection.ToList().OrderByDescending(c => c.AcaCalID).ThenBy(c => c.FormalCode);
                gvCourseHistory.DataBind();
            }

        }

        private void SendSMS(string PhoneNo, string roll, string msg)
        {
            SMSBasicSetup smsSetup = SMSBasicSetupManager.Get();
            bool updated = SMSBasicSetupManager.Update(smsSetup);
            if (!string.IsNullOrEmpty(PhoneNo) && PhoneNo.Count() == 14 && PhoneNo.Contains("+") && smsSetup.RemainingSMS > 0 && smsSetup.RegistrationStatus == true)
                SMSManager.Send(PhoneNo, roll, msg, ResultCallBack);
            else
                LogSMSManager.Insert(DateTime.Now, userObj.LogInID.ToString(), roll, "Number format or setup related error", false);
        }

        private void ResultCallBack(string[] data)
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

        private void ShowLabelMessage(string msg, bool isVisible)
        {
            pnlMessage.Visible = isVisible;
            lblMessage.Text = msg;
        }

        private void ShowAlertMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerControlScript", "alert('" + msg + "');", true);
        }

        private bool IsAlreadyTakeThisCourse(int studentId, int courseId, int versionId)
        {
            bool result = false;

            List<RegistrationWorksheet> collection = new List<RegistrationWorksheet>();
            collection = RegistrationWorksheetManager.GetAllOpenCourseByStudentID(studentId);

            int count = collection.Where(c => c.CourseID == courseId && c.VersionID == versionId && !string.IsNullOrEmpty(c.SectionName)).ToList().Count();
            if (count > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        private void LoadCourseByStudentIdTypeId()
        {
            try
            {
                int typeId = Convert.ToInt32(ddlCourseType.SelectedValue);
                LogicLayer.BusinessObjects.StudentRegistration sr = StudentRegistrationManager.GetByRegistrationNo(txtStudent.Text.Trim());

                if (sr != null)
                {

                    List<Course> list = CourseManager.GetAllRetakeOrRegularOpenCourseByStudentIdCourseTypeId(sr.StudentId, typeId);

                    ddlCourse.Items.Clear();
                    ddlCourse.AppendDataBoundItems = true;

                    if (list != null)
                    {
                        ddlCourse.Items.Add(new ListItem("-Select-", "0"));
                        ddlCourse.DataTextField = "CourseFullInfo";
                        ddlCourse.DataValueField = "CoureIdVersionId";

                        ddlCourse.DataSource = list;
                        ddlCourse.DataBind();
                    }
                }
                else
                {
                    ddlCourse.Items.Clear();
                    ddlCourse.AppendDataBoundItems = true;
                    ddlCourse.Items.Add(new ListItem("-Select-", "0"));

                    ddlCourse.DataBind();
                }
            }
            catch { }
            finally { }
        }

        #endregion

        #region Forward To Account & Print

        protected void btnForwardPrintOn_Click(object sender, EventArgs e)
        {
            try
            {

                LogicLayer.BusinessObjects.StudentRegistration sr = StudentRegistrationManager.GetByRegistrationNo(txtStudent.Text.Trim());
                if (sr == null)
                {
                    ShowAlertMessage(" Student not found.");
                    return;
                }

                Student student = StudentManager.GetByRoll(sr.Roll);
                if (student == null)
                {

                    ShowAlertMessage(" Student not found.");
                    return;
                }

                StudentInstitution stdInst = StudentInstitutionManager.GetByStudentId(student.StudentID);
                if (stdInst == null || stdInst.ExemCenterId == 0)
                {
                    ShowAlertMessage("Save Exam Center First.");
                    return;
                }


                if (student.Batch != null && student.Batch.BatchNO <= 5)
                {
                    ShowAlertMessage(" You are not eligible for registration .");
                    return;
                }

                AcademicCalender acaCal = AcademicCalenderManager.GetActiveRegistrationCalenderByProgramId((int)student.ProgramID);
                ReportViewer reportViewer = new ReportViewer();


                if (student.Roll != "" && acaCal.AcademicCalenderID != 0)
                {

                    List<rStudentClassExamSum> list = ReportManager.GetStudentCourseRegSummaryNew(student.StudentID, acaCal.AcademicCalenderID, userObj.Id, 0);
                    rStudentGeneralInfo regInfo = StudentManager.GetStudentGeneralInfoById(student.StudentID);

                    #region Reg.Session

                    string ResSession = "";
                    StudentRegistration RegObj = StudentRegistrationManager.GetByStudentId(student.StudentID);
                    if (RegObj != null)
                    {
                        Batch batch = BatchManager.GetById(RegObj.SessionId);
                        if (batch != null && batch.Session != null)
                            ResSession = batch.Session.Code;

                        ResSession = ResSession.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");

                    }

                    #endregion

                    #region FindSession Name
                    string Session = "";

                    string Batch = student.Batch.Attribute1;

                    string[] BatchInfo = Batch.Split('-');
                    int one = Convert.ToInt32(BatchInfo[0]);
                    int two = one + 1;

                    string FirstYear = one.ToString();
                    string SecondYear = two.ToString();

                    FirstYear = FirstYear.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");
                    SecondYear = SecondYear.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");

                    Session = FirstYear + "-" + SecondYear;

                    #endregion

                    if (list != null && list.Count != 0)
                    {
                        #region Log Insert

                        string courses = "";

                        foreach (var item in list)
                        {
                            courses += item.FormalCode + ", ";
                        }
                        LogGeneralManager.Insert(
                                    DateTime.Now,
                                    "",
                                    "",
                                    userObj.LogInID,
                                    "",
                                    "",
                                    " Course Forword To Account",
                                    userObj.LogInID + " Forward " + courses + " courses for StudentID :  " + student.Roll,
                                    userObj.LogInID + " Forword To Account",
                                    ((int)CommonEnum.PageName.Registration).ToString(),
                                    CommonEnum.PageName.Registration.ToString(),
                                    _pageUrl,
                                    student.Roll);
                        #endregion

                        string yearName = "৩য়";

                        if (regInfo.YearNo == 1)
                        {
                            yearName = "২য়";
                        }
                        else if (regInfo.YearNo == 0)
                        {
                            yearName = "১ম";
                        }

                        ReportViewer.LocalReport.DataSources.Clear();
                        ReportViewer.Reset();

                        ReportViewer.LocalReport.EnableExternalImages = true;
                        string imgUrl = new Uri(Server.MapPath("~/Upload/Avatar/" + regInfo.PhotoPath)).AbsoluteUri;
                        //string imgUrl = AppPath.FullBaseUrl + "/Upload/Avatar" + regInfo.PhotoPath;

                        this.ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Module/registration/report/RptRegistrationPrintOut.rdlc");

                        List<ReportParameter> param1 = new List<ReportParameter>();

                        param1.Add(new ReportParameter("Roll", regInfo.Roll));
                        param1.Add(new ReportParameter("FullName", regInfo.FullName));
                        param1.Add(new ReportParameter("FullNameInBangla", ""));
                        param1.Add(new ReportParameter("FatherName", regInfo.FatherName));
                        param1.Add(new ReportParameter("MotherName", regInfo.MotherName));
                        param1.Add(new ReportParameter("GuardianName", regInfo.GuardianName));
                        param1.Add(new ReportParameter("Phone", regInfo.Phone));
                        param1.Add(new ReportParameter("ProgNameInBan", regInfo.ProgNameInBan));
                        param1.Add(new ReportParameter("Semester", acaCal.FullCode));
                        param1.Add(new ReportParameter("RegistrationNo", regInfo.RegistrationNo));
                        param1.Add(new ReportParameter("ExamCenterName", ddlExamCenter.SelectedItem.Text));
                        param1.Add(new ReportParameter("PhotoPath", imgUrl));
                        param1.Add(new ReportParameter("YearName", yearName));
                        param1.Add(new ReportParameter("Session", Session));
                        param1.Add(new ReportParameter("ResSession", ResSession));




                        ReportDataSource rds = new ReportDataSource("RegisteredCourseDataSet", list);

                        this.ReportViewer.LocalReport.SetParameters(param1);


                        ReportViewer.LocalReport.DataSources.Clear();
                        ReportViewer.LocalReport.DataSources.Add(rds);

                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string filenameExtension;

                        byte[] bytes = this.ReportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                        using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportPDF/" + "ReportRegistration" + student.Roll + ".pdf"), FileMode.Create))
                        {
                            fs.Write(bytes, 0, bytes.Length);
                        }

                        //string url = string.Format("RegistrationPDF.aspx?FN={0}", "ReportRegistration" + student.Roll + ".pdf");
                        //string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
                        //this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);

                        string fileName = "ReportRegistration" + student.Roll + ".pdf";

                        string FilePath = Server.MapPath("~/Upload/ReportPDF/");


                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                        Response.TransmitFile(Server.MapPath("~/Upload/ReportPDF/" + fileName));
                        Response.Flush();
                        Response.End();
                    }

                    else
                    {
                        reportViewer.LocalReport.DataSources.Clear();
                        ShowAlertMessage(" Registered Data not Found.");
                        return;
                    }


                }
                else
                {
                    reportViewer.LocalReport.DataSources.Clear();

                    ShowAlertMessage(" Please Enter Student ID.");
                    return;
                }
            }
            catch (Exception ex)
            {
                PrintUtility pu = new PrintUtility();

                string txtMsg = ex.StackTrace + Environment.NewLine + Environment.NewLine
                    + " ex.Message//" + ex.Message + Environment.NewLine + Environment.NewLine
                    + " ex.Source//" + ex.Source + Environment.NewLine + Environment.NewLine
                    + " ex.TargetSite//" + ex.TargetSite;

                ShowLabelMessage(txtMsg, true);
            }
        }

        protected void btnRetakeForwardPrint_Click(object sender, EventArgs e)
        {
            try
            {
                LogicLayer.BusinessObjects.StudentRegistration sr = StudentRegistrationManager.GetByRegistrationNo(txtStudent.Text.Trim());
                if (sr == null)
                {
                    ShowAlertMessage(" Student not found.");
                    return;
                }
                Student student = StudentManager.GetByRoll(sr.Roll);
                if (student == null)
                {

                    ShowAlertMessage(" Student not found.");
                    return;
                }

                StudentInstitution stdInst = StudentInstitutionManager.GetByStudentId(student.StudentID);
                if (stdInst == null || stdInst.ExemCenterId == 0)
                {
                    ShowAlertMessage("Save Exam Center First.");
                    return;
                }


                if (student.Batch != null && student.Batch.BatchNO <= 5)
                {
                    ShowAlertMessage(" You are not eligible for registration .");
                    return;
                }

                AcademicCalender acaCal = AcademicCalenderManager.GetActiveRegistrationCalenderByProgramId((int)student.ProgramID);
                ReportViewer reportViewer = new ReportViewer();


                if (student.Roll != "" && acaCal.AcademicCalenderID != 0)
                {

                    List<rStudentClassExamSum> list = ReportManager.GetStudentCourseRegSummaryNew(student.StudentID, acaCal.AcademicCalenderID, userObj.Id, 1);
                    rStudentGeneralInfo regInfo = StudentManager.GetStudentGeneralInfoById(student.StudentID);


                    if (list != null && list.Count != 0)
                    {

                        #region Log Insert

                        string courses = "";

                        foreach (var item in list)
                        {
                            courses += item.FormalCode + ", ";
                        }
                        LogGeneralManager.Insert(
                                    DateTime.Now,
                                    "",
                                    "",
                                    userObj.LogInID,
                                    "",
                                    "",
                                    " Course Forword To Account",
                                    userObj.LogInID + " Forward " + courses + " courses for StudentID :  " + student.Roll,
                                    userObj.LogInID + " Forword To Account",
                                    ((int)CommonEnum.PageName.Registration).ToString(),
                                    CommonEnum.PageName.Registration.ToString(),
                                    _pageUrl,
                                    student.Roll);
                        #endregion



                        #region FindSession Name
                        string Session = "";

                        string Batch = student.Batch.Attribute1;

                        string[] BatchInfo = Batch.Split('-');
                        int one = Convert.ToInt32(BatchInfo[0]);
                        int two = one + 1;

                        string FirstYear = one.ToString();
                        string SecondYear = two.ToString();

                        FirstYear = FirstYear.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");
                        SecondYear = SecondYear.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");

                        Session = FirstYear + "-" + SecondYear;

                        #endregion


                        #region Reg.Session

                        string ResSession = "";
                        StudentRegistration RegObj = StudentRegistrationManager.GetByStudentId(student.StudentID);
                        if (RegObj != null)
                        {
                            Batch batch = BatchManager.GetById(RegObj.SessionId);
                            if (batch != null && batch.Session != null)
                                ResSession = batch.Session.Code;

                            ResSession = ResSession.Replace("0", "০").Replace("1", "১").Replace("2", "২").Replace("3", "৩").Replace("4", "৪").Replace("5", "৫").Replace("6", "৬").Replace("7", "৭").Replace("8", "৮").Replace("9", "৯");

                        }

                        #endregion

                        ReportViewer.LocalReport.DataSources.Clear();
                        ReportViewer.Reset();

                        ReportViewer.LocalReport.EnableExternalImages = true;
                        //string imgUrl = AppPath.FullBaseUrl + "/Upload/Avatar" + regInfo.PhotoPath;
                        string imgUrl = new Uri(Server.MapPath("~/Upload/Avatar/" + regInfo.PhotoPath)).AbsoluteUri;

                        if (regInfo.YearNo == 3)
                        {
                            this.ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Module/registration/report/RptRegistrationPrintOutRetakeV3.rdlc");
                        }
                        else if (regInfo.YearNo == 2)
                        {
                            this.ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Module/registration/report/RptRegistrationPrintOutRetakeV2.rdlc");
                        }
                        else
                        {
                            this.ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Module/registration/report/RptRegistrationPrintOutRetake.rdlc");
                        }
                        List<ReportParameter> param1 = new List<ReportParameter>();

                        param1.Add(new ReportParameter("Roll", regInfo.Roll));
                        param1.Add(new ReportParameter("FullName", regInfo.FullName));
                        param1.Add(new ReportParameter("FullNameInBangla", ""));
                        param1.Add(new ReportParameter("FatherName", regInfo.FatherName));
                        param1.Add(new ReportParameter("MotherName", regInfo.MotherName));
                        param1.Add(new ReportParameter("Phone", regInfo.Phone));
                        param1.Add(new ReportParameter("ProgNameInBan", regInfo.ProgNameInBan));
                        param1.Add(new ReportParameter("Semester", acaCal.FullCode));
                        param1.Add(new ReportParameter("RegistrationNo", regInfo.RegistrationNo));
                        param1.Add(new ReportParameter("GuardianName", regInfo.GuardianName));
                        param1.Add(new ReportParameter("ExamCenterName", ddlExamCenter.SelectedItem.Text));
                        param1.Add(new ReportParameter("PhotoPath", imgUrl));
                        param1.Add(new ReportParameter("Session", Session));
                        param1.Add(new ReportParameter("ResSession", ResSession));

                        ReportDataSource rds = new ReportDataSource("RegisteredCourseDataSet", list);

                        this.ReportViewer.LocalReport.SetParameters(param1);


                        ReportViewer.LocalReport.DataSources.Clear();
                        ReportViewer.LocalReport.DataSources.Add(rds);

                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string filenameExtension;

                        byte[] bytes = this.ReportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                        using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportPDF/" + "ReportRegistration" + student.Roll + ".pdf"), FileMode.Create))
                        {
                            fs.Write(bytes, 0, bytes.Length);
                        }

                        //string url = string.Format("RegistrationPDF.aspx?FN={0}", "ReportRegistration" + student.Roll + ".pdf");
                        //string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
                        //this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);

                        string fileName = "ReportRegistration" + student.Roll + ".pdf";

                        string FilePath = Server.MapPath("~/Upload/ReportPDF/");


                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                        Response.TransmitFile(Server.MapPath("~/Upload/ReportPDF/" + fileName));
                        Response.Flush();
                        Response.End();
                    }

                    else
                    {
                        reportViewer.LocalReport.DataSources.Clear();
                        ShowAlertMessage(" Registered Data not Found.");
                        return;
                    }

                }
                else
                {
                    reportViewer.LocalReport.DataSources.Clear();

                    ShowAlertMessage(" Please Enter Student ID.");
                    return;
                }

            }
            catch (Exception ex)
            {
                PrintUtility pu = new PrintUtility();

                string txtMsg = ex.StackTrace + Environment.NewLine + Environment.NewLine
                    + " ex.Message//" + ex.Message + Environment.NewLine + Environment.NewLine
                    + " ex.Source//" + ex.Source + Environment.NewLine + Environment.NewLine
                    + " ex.TargetSite//" + ex.TargetSite;

                ShowLabelMessage(txtMsg, true);
            }
        }

        #endregion

        protected void ddlCourseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCourseByStudentIdTypeId();
        }

        protected void btnAddCourse_Click(object sender, EventArgs e)
        {
            try
            {
                LogicLayer.BusinessObjects.StudentRegistration sr = StudentRegistrationManager.GetByRegistrationNo(txtStudent.Text.Trim());
                Student student = StudentManager.GetByRoll(sr.Roll);

                AcademicCalender acaCal = AcademicCalenderManager.GetActiveRegistrationCalenderByProgramId((int)student.ProgramID);

                string CourseNameNew = ddlCourse.SelectedValue;
                if (CourseNameNew != null)
                {
                    int courseId = Convert.ToInt32(CourseNameNew.Split('_').First());
                    int versionId = Convert.ToInt32(CourseNameNew.Split('_').Last());


                    LogicLayer.BusinessObjects.Course courseObj = CourseManager.GetByCourseIdVersionId(courseId, versionId);
                    RegistrationWorksheet regWorkSheet = new RegistrationWorksheet();
                    regWorkSheet.CourseID = courseObj.CourseID;
                    regWorkSheet.VersionID = courseObj.VersionID;
                    regWorkSheet.Credits = courseObj.Credits;
                    if (Convert.ToInt32(ddlCourseType.SelectedValue) == 2)
                    {
                        regWorkSheet.IsOfferedCourse = true;
                    }
                    regWorkSheet.IsAutoOpen = true;
                    regWorkSheet.BatchID = student.BatchId;
                    //regWorkSheet.Node_CourseID = nodeCourseId;
                    regWorkSheet.StudentID = student.StudentID;
                    regWorkSheet.OriginalCalID = acaCal.AcademicCalenderID;
                    regWorkSheet.CourseTitle = courseObj.Title;
                    regWorkSheet.FormalCode = courseObj.FormalCode;
                    regWorkSheet.VersionCode = courseObj.VersionCode;
                    regWorkSheet.ProgramID = student.ProgramID;
                    regWorkSheet.CreatedBy = userObj.Id;
                    regWorkSheet.CreatedDate = DateTime.Now;
                    regWorkSheet.ModifiedBy = userObj.Id;
                    regWorkSheet.ModifiedDate = DateTime.Now;
                    int result = RegistrationWorksheetManager.Insert(regWorkSheet);
                    if (result > 0)
                    {
                        lblMessage.Text = "Course inserted successfully.";
                    }
                    else
                    {
                        lblMessage.Text = "Course could not inserted successfully.";
                    }
                    LoadStudentCourse(student, acaCal);
                    LoadStudentIrregularCourse(student, acaCal);
                    LoadCourseByStudentIdTypeId();
                }
            }
            catch (Exception)
            { }
        }

        protected void btnSaveExamCenter_Click(object sender, EventArgs e)
        {
            try
            {
                Student student = StudentManager.GetByRoll(lblRoll.Text);
                StudentInstitution stdInstitution = StudentInstitutionManager.GetByStudentId(student.StudentID);

                if (stdInstitution != null && Convert.ToInt32(ddlExamCenter.SelectedValue) != 0)
                {
                    stdInstitution.ExemCenterId = Convert.ToInt32(ddlExamCenter.SelectedValue);
                    bool isUpdate = StudentInstitutionManager.Update(stdInstitution);

                    if (isUpdate)
                    {
                        ShowLabelMessage("Exam Center Update Successfull !", true);
                    }
                    else
                    {
                        ShowLabelMessage("Exam Center Update Error !", true);
                    }
                }
                else
                {
                    ShowLabelMessage("Exam Center can't be empty !", true);
                }
            }
            catch (Exception)
            { }
        }

    }
}