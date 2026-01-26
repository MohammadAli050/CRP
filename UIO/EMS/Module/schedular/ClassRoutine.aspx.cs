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

public partial class Admin_ClassRoutine : BasePage
{
    List<ShareBatchInSection> ShareBatchList = new List<ShareBatchInSection>();
    string Session_ShareBatchList = "Session_ShareBatchList";

    List<Batch> batchList = new List<Batch>();
    string Session_BatchList = "Session_BatchList";

    public string flagCourseOffer = "NULL";
    BussinessObject.UIUMSUser userObj = null;
    string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        if (!IsPostBack)
        {
            ucProgram.LoadDropdownWithUserAccess(userObj.Id);
            LoadDropDown();
            //pnPopUp.Visible = false;
            // panelContainer.Enabled = true;
        }
    }

    private void LoadDropDown()
    {
        FillSectionGenderCombo();
    }

    private void FillSectionGenderCombo()
    {
        List<Value> collection = ValueManager.GetByValueSetId((int)CommonEnum.ValueSet.SectionGender);

        ddlSectionGender.Items.Clear();
        ddlSectionGender.Items.Add(new ListItem("-Select-", "0"));
        ddlSectionGender.AppendDataBoundItems = true;
        ddlSectionGender.DataValueField = "ValueID";
        ddlSectionGender.DataTextField = "ValueName";

        ddlSectionGender.DataSource = collection;
        ddlSectionGender.DataBind();

    }

    private void LoadClassSchedule()
    {
        int acaCalId = Convert.ToInt32(ucSession.selectedValue);
        int programId = Convert.ToInt32(ucProgram.selectedValue);

        List<Course> courseList = CourseManager.GetAll();
        Hashtable hashCourse = new Hashtable();
        foreach (Course course in courseList)
            hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.FormalCode + ":" + course.Title);

        List<RoomInformation> roomInfoList = RoomInformationManager.GetAll();
        Hashtable hashRoomInfo = new Hashtable();
        if (roomInfoList != null)
        {
            foreach (RoomInformation roomInfo in roomInfoList)
                hashRoomInfo.Add(roomInfo.RoomInfoID, roomInfo.RoomNumber + ": " + roomInfo.Capacity);
        }

        List<Value> valueList = ValueManager.GetAll();
        valueList = valueList.Where(x => x.ValueSetID == 1).ToList();
        Hashtable hashDay = new Hashtable();
        foreach (Value value in valueList)
            hashDay.Add(value.ValueID, value.ValueName);

        List<TimeSlotPlanNew> timeSlotPlanList = TimeSlotPlanManager.GetAll();
        Hashtable hashTimeSlotPlan = new Hashtable();
        foreach (TimeSlotPlanNew timeSlotPlan in timeSlotPlanList)
            hashTimeSlotPlan.Add(timeSlotPlan.TimeSlotPlanID, timeSlotPlan.StartHour + ":" + timeSlotPlan.StartMin + " " + (timeSlotPlan.StartAMPM == 1 ? "AM" : "PM") + "-" + timeSlotPlan.EndHour + ":" + timeSlotPlan.EndMin + " " + (timeSlotPlan.EndAMPM == 1 ? "AM" : "PM"));

        List<Employee> employeeList = EmployeeManager.GetAll();
        Hashtable hashEmployee = new Hashtable();
        foreach (Employee employee in employeeList)
            hashEmployee.Add(employee.EmployeeID, employee.Code);

        List<Program> programList = ProgramManager.GetAll();
        Hashtable hashProgram = new Hashtable();
        foreach (Program program in programList)
            hashProgram.Add(program.ProgramID, program.ShortName);

        List<GradeSheetTemplate> gradeSheetTempList = GradeSheetTemplateManager.GetAll();
        Hashtable hashGradeSheetTemp = new Hashtable();
        foreach (GradeSheetTemplate gradeSheetTemp in gradeSheetTempList)
            hashGradeSheetTemp.Add(gradeSheetTemp.GradeSheetTemplateID, gradeSheetTemp.Name);

        List<AcademicCalenderSection> acaCalSectionList = AcademicCalenderSectionManager.GetAll();
        if (acaCalSectionList.Count > 0)
        {
            if (acaCalId != 0)
                acaCalSectionList = acaCalSectionList.Where(x => x.AcademicCalenderID == acaCalId).ToList();
            if (programId != 0)
                acaCalSectionList = acaCalSectionList.Where(x => x.ProgramID == programId).ToList();

            for (int i = 0; i < acaCalSectionList.Count; i++)
            {
                string courseIndex = acaCalSectionList[i].CourseID.ToString() + "_" + acaCalSectionList[i].VersionID.ToString();
                acaCalSectionList[i].CourseInfo = hashCourse[courseIndex] == null ? "" : hashCourse[courseIndex].ToString();
                acaCalSectionList[i].RoomInfoOne = hashRoomInfo[acaCalSectionList[i].RoomInfoOneID] == null ? "" : hashRoomInfo[acaCalSectionList[i].RoomInfoOneID].ToString();
                acaCalSectionList[i].RoomInfoTwo = hashRoomInfo[acaCalSectionList[i].RoomInfoTwoID] == null ? "" : hashRoomInfo[acaCalSectionList[i].RoomInfoTwoID].ToString();
                acaCalSectionList[i].DayInfoOne = hashDay[acaCalSectionList[i].DayOne] == null ? "" : hashDay[acaCalSectionList[i].DayOne].ToString();
                acaCalSectionList[i].DayInfoTwo = hashDay[acaCalSectionList[i].DayTwo] == null ? "" : hashDay[acaCalSectionList[i].DayTwo].ToString();
                acaCalSectionList[i].ProgramName = hashProgram[acaCalSectionList[i].ProgramID] == null ? "" : hashProgram[acaCalSectionList[i].ProgramID].ToString();
                acaCalSectionList[i].TimeSlotPlanInfoOne = hashTimeSlotPlan[acaCalSectionList[i].TimeSlotPlanOneID] == null ? "" : hashTimeSlotPlan[acaCalSectionList[i].TimeSlotPlanOneID].ToString();
                acaCalSectionList[i].TimeSlotPlanInfoTwo = hashTimeSlotPlan[acaCalSectionList[i].TimeSlotPlanTwoID] == null ? "" : hashTimeSlotPlan[acaCalSectionList[i].TimeSlotPlanTwoID].ToString();
                acaCalSectionList[i].TeacherInfoOne = hashEmployee[acaCalSectionList[i].TeacherOneID] == null ? "" : hashEmployee[acaCalSectionList[i].TeacherOneID].ToString();
                acaCalSectionList[i].TeacherInfoTwo = hashEmployee[acaCalSectionList[i].TeacherTwoID] == null ? "" : hashEmployee[acaCalSectionList[i].TeacherTwoID].ToString();
                acaCalSectionList[i].GradeSheetTemplate = hashGradeSheetTemp[acaCalSectionList[i].BasicExamTemplateId] == null ? "" : hashGradeSheetTemp[acaCalSectionList[i].BasicExamTemplateId].ToString();
            }
            acaCalSectionList = acaCalSectionList.OrderBy(x => x.CourseInfo).ThenBy(x => x.SectionName).ToList();
        }

        if (acaCalSectionList != null)
        {
            SessionManager.SaveListToSession<AcademicCalenderSection>(acaCalSectionList, "_acaCalSectionList");
            LoadGrid(acaCalSectionList);
        }

        //gvClassSchedule.DataSource = acaCalSectionList;
        //gvClassSchedule.DataBind();
    }

    private void LoadComboBox(int programId)
    {
        try
        {
            FillSectionGenderCombo();

            List<Course> courseList = CourseManager.GetAll();
            if (courseList != null)
            {
                courseList = courseList.OrderBy(c => c.FormalCode).ToList();
            }

            Hashtable hashCourse = new Hashtable();
            foreach (Course course in courseList)
                hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.FormalCode + ":" + course.Title);

            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("-Select-", "0"));
            List<OfferedCourse> offeredCourseList = OfferedCourseManager.GetAll().Where(x => x.AcademicCalenderID == Convert.ToInt32(ucSession.selectedValue) && x.ProgramID == programId).ToList();

            if (offeredCourseList.Count > 0 && offeredCourseList != null)
            {
                offeredCourseList = offeredCourseList.Where(c => c.IsActive == true).ToList();
            }

            if (offeredCourseList.Count > 0 && offeredCourseList != null)
            {
                offeredCourseList = offeredCourseList.OrderBy(c => c.CourseCode).ToList();

                flagCourseOffer = "Offer";

                foreach (OfferedCourse offeredCourse in offeredCourseList)
                {
                    string formalCodeTitle = offeredCourse.CourseID + "_" + offeredCourse.VersionID;
                    ddlCourse.Items.Add(new ListItem(hashCourse[formalCodeTitle].ToString() + " - " + offeredCourse.TreeRoot.Node_Name, offeredCourse.CourseID + "_" + offeredCourse.VersionID));
                }
            }
            else
            {
                flagCourseOffer = "NotOffer";
            }

            ddlRoomInfo1.Items.Clear();
            ddlRoomInfo2.Items.Clear();
            ddlRoomInfo3.Items.Clear();
            ddlRoomInfo1.Items.Add(new ListItem("-Select-", "0"));
            ddlRoomInfo2.Items.Add(new ListItem("-Select-", "0"));
            ddlRoomInfo3.Items.Add(new ListItem("-Select-", "0"));

            List<RoomInformation> roomInfoList = RoomInformationManager.GetAll();
            if (roomInfoList.Count > 0)
                foreach (RoomInformation roomInfo in roomInfoList)
                {
                    ddlRoomInfo1.Items.Add(new ListItem(roomInfo.RoomNumber + ": " + roomInfo.Capacity, roomInfo.RoomInfoID.ToString()));
                    ddlRoomInfo2.Items.Add(new ListItem(roomInfo.RoomNumber + ": " + roomInfo.Capacity, roomInfo.RoomInfoID.ToString()));
                    ddlRoomInfo3.Items.Add(new ListItem(roomInfo.RoomNumber + ": " + roomInfo.Capacity, roomInfo.RoomInfoID.ToString()));
                }

            ddlDay1.Items.Clear();
            ddlDay2.Items.Clear();
            ddlDay3.Items.Clear();
            ddlDay1.Items.Add(new ListItem("-Select-", "0"));
            ddlDay2.Items.Add(new ListItem("-Select-", "0"));
            ddlDay3.Items.Add(new ListItem("-Select-", "0"));

            List<Value> valueList = ValueManager.GetAll().Where(x => x.ValueSetID == 1).ToList();
            if (valueList.Count > 0)
                foreach (Value value in valueList)
                {
                    ddlDay1.Items.Add(new ListItem(value.ValueName, value.ValueID.ToString()));
                    ddlDay2.Items.Add(new ListItem(value.ValueName, value.ValueID.ToString()));
                    ddlDay3.Items.Add(new ListItem(value.ValueName, value.ValueID.ToString()));
                }

            ddlTimeSlot1.Items.Clear();
            ddlTimeSlot2.Items.Clear();
            ddlTimeSlot3.Items.Clear();
            ddlTimeSlot1.Items.Add(new ListItem("-Select-", "0"));
            ddlTimeSlot2.Items.Add(new ListItem("-Select-", "0"));
            ddlTimeSlot3.Items.Add(new ListItem("-Select-", "0"));

            List<TimeSlotPlanNew> timeSlotPlanList = TimeSlotPlanManager.GetAll();
            if (timeSlotPlanList.Count > 0)
                foreach (TimeSlotPlanNew timeSlotPlan in timeSlotPlanList)
                {
                    ddlTimeSlot1.Items.Add(new ListItem(timeSlotPlan.StartHour + ":" + timeSlotPlan.StartMin + " " + (timeSlotPlan.StartAMPM == 1 ? "AM" : "PM") + "-" + timeSlotPlan.EndHour + ":" + timeSlotPlan.EndMin + " " + (timeSlotPlan.EndAMPM == 1 ? "AM" : "PM"), timeSlotPlan.TimeSlotPlanID.ToString()));
                    ddlTimeSlot2.Items.Add(new ListItem(timeSlotPlan.StartHour + ":" + timeSlotPlan.StartMin + " " + (timeSlotPlan.StartAMPM == 1 ? "AM" : "PM") + "-" + timeSlotPlan.EndHour + ":" + timeSlotPlan.EndMin + " " + (timeSlotPlan.EndAMPM == 1 ? "AM" : "PM"), timeSlotPlan.TimeSlotPlanID.ToString()));
                    ddlTimeSlot3.Items.Add(new ListItem(timeSlotPlan.StartHour + ":" + timeSlotPlan.StartMin + " " + (timeSlotPlan.StartAMPM == 1 ? "AM" : "PM") + "-" + timeSlotPlan.EndHour + ":" + timeSlotPlan.EndMin + " " + (timeSlotPlan.EndAMPM == 1 ? "AM" : "PM"), timeSlotPlan.TimeSlotPlanID.ToString()));
                }

            ddlFaculty1.Items.Clear();
            ddlFaculty2.Items.Clear();
            ddlFaculty3.Items.Clear();
            ddlFaculty1.Items.Add(new ListItem("-Select-", "0"));
            ddlFaculty2.Items.Add(new ListItem("-Select-", "0"));
            ddlFaculty3.Items.Add(new ListItem("-Select-", "0"));

            List<Employee> employeeList = EmployeeManager.GetAll();
            if (employeeList.Count > 0)
            {
                employeeList = employeeList.OrderBy(o => o.Code).ToList();

                foreach (Employee employee in employeeList)
                {
                    ddlFaculty1.Items.Add(new ListItem(employee.Code +"   ("+ employee.BasicInfo.FullName+")", employee.EmployeeID.ToString()));
                    ddlFaculty2.Items.Add(new ListItem(employee.Code + "   (" + employee.BasicInfo.FullName + ")", employee.EmployeeID.ToString()));
                    ddlFaculty3.Items.Add(new ListItem(employee.Code + "   (" + employee.BasicInfo.FullName + ")", employee.EmployeeID.ToString()));
                }
            }

            List<Program> programList = ProgramManager.GetAll();
            chkListShareProgram.DataValueField = "ProgramID";
            chkListShareProgram.DataTextField = "ShortName";
            chkListShareProgram.Items.Clear();
            chkListShareProgram.DataSource = programList;
            chkListShareProgram.DataBind();

            //ddlGradeSheetTemplate.Items.Clear();
            //ddlGradeSheetTemplate.Items.Add(new ListItem("-Select-", "0"));
            //List<GradeSheetTemplate> gradeSheetTempList = GradeSheetTemplateManager.GetAll();
            //if (gradeSheetTempList.Count > 0)
            //{
            //    gradeSheetTempList = gradeSheetTempList.Where(x => x.IsActive == true).ToList();
            //    foreach (GradeSheetTemplate gradeSheetTemp in gradeSheetTempList)
            //        ddlGradeSheetTemplate.Items.Add(new ListItem(gradeSheetTemp.Name, gradeSheetTemp.GradeSheetTemplateID.ToString()));
            //}

            List<ExamTemplateMaster> examTemplateList = ExamTemplateMasterManager.GetAll().Where(d=> d.ExamTemplateMasterTypeId== 1 && (d.ProgramId == 0 || d.ProgramId ==programId)).ToList();
            ddlOnlineGeadeSheet.DataTextField = "ExamTemplateMasterName";
            ddlOnlineGeadeSheet.DataValueField = "ExamTemplateMasterId";
            ddlOnlineGeadeSheet.AppendDataBoundItems = true;
            ddlOnlineGeadeSheet.Items.Clear();
            ddlOnlineGeadeSheet.Items.Add(new ListItem("-Select Exam Template-", "0"));
            if (examTemplateList != null)
            {
                ddlOnlineGeadeSheet.DataSource = examTemplateList;
                ddlOnlineGeadeSheet.DataBind();
            }


            List<ExamTemplateMaster> examCalculativeTemplateList = ExamTemplateMasterManager.GetAll().Where(d => d.ExamTemplateMasterTypeId == 2 && (d.ProgramId == 0 || d.ProgramId == programId)).ToList();
            ddlCalculativeTemplate.DataTextField = "ExamTemplateMasterName";
            ddlCalculativeTemplate.DataValueField = "ExamTemplateMasterId";
            ddlCalculativeTemplate.AppendDataBoundItems = true;
            ddlCalculativeTemplate.Items.Clear();
            ddlCalculativeTemplate.Items.Add(new ListItem("-Select Exam Template-", "0"));
            if (examTemplateList != null)
            {
                ddlCalculativeTemplate.DataSource = examCalculativeTemplateList;
                ddlCalculativeTemplate.DataBind();
            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error: 103(Dropdown Load)";
        }
        finally { }
    }

    protected void lbAdd_Click(object sender, EventArgs e)
    {
        if (ucSession.selectedValue == "0" || ucProgram.selectedValue == "0")
        {
            lblMsg.Text = "Please Select Batch and Program.";
            return;
        }

        else if (flagCourseOffer == "NotOffer")
        {
            lblMsg.Text = "Course is not Offered.";
            return;
        }

        lblMsg.Text = "";
        
        btnUpdateInsert.Text = "New Add";

        LoadComboBox(Convert.ToInt32(ucProgram.selectedValue));
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
       
        try
        {            
            btnUpdateInsert.Text = "Update";

            LinkButton linkButton = new LinkButton();
            linkButton = (LinkButton)sender;
            int id = Convert.ToInt32(linkButton.CommandArgument);
            btnUpdateInsert.CommandArgument = id.ToString();

            ucBatch.SelectedIndex0();
            lblShareBatch.Text = string.Empty;

            AcademicCalenderSection acaCalSection = AcademicCalenderSectionManager.GetById(id);
            LoadComboBox(acaCalSection.ProgramID);

            int acaCalId = Convert.ToInt32(ucSession.selectedValue);
            int programId = Convert.ToInt32(ucProgram.selectedValue);

            ddlCourse.SelectedValue = ddlCourse.Items.Count > 0 ? acaCalSection.CourseID + "_" + acaCalSection.VersionID : "0";
            ddlCourse.Enabled = false;

            txtSection.Text = acaCalSection.SectionName;
            txtCapacity.Text = acaCalSection.Capacity.ToString();

            ddlRoomInfo1.SelectedValue = acaCalSection.RoomInfoOneID != 0 ? acaCalSection.RoomInfoOneID.ToString() : "0";
            ddlDay1.SelectedValue = acaCalSection.DayOne != 0 ? acaCalSection.DayOne.ToString() : "0";
            ddlTimeSlot1.SelectedValue = acaCalSection.TimeSlotPlanOneID != 0 ? acaCalSection.TimeSlotPlanOneID.ToString() : "0";
            ddlFaculty1.SelectedValue = acaCalSection.TeacherOneID != 0 ? acaCalSection.TeacherOneID.ToString() : "0";

            ddlRoomInfo2.SelectedValue = acaCalSection.RoomInfoTwoID != 0 ? acaCalSection.RoomInfoTwoID.ToString() : "0";
            ddlDay2.SelectedValue = acaCalSection.DayTwo != 0 ? acaCalSection.DayTwo.ToString() : "0";
            ddlTimeSlot2.SelectedValue = acaCalSection.TimeSlotPlanTwoID != 0 ? acaCalSection.TimeSlotPlanTwoID.ToString() : "0";
            ddlFaculty2.SelectedValue = acaCalSection.TeacherTwoID != 0 ? acaCalSection.TeacherTwoID.ToString() : "0";

            ddlRoomInfo3.SelectedValue = acaCalSection.RoomInfoThreeID != 0 ? acaCalSection.RoomInfoThreeID.ToString() : "0";
            ddlDay3.SelectedValue = acaCalSection.DayThree != 0 ? acaCalSection.DayThree.ToString() : "0";
            ddlTimeSlot3.SelectedValue = acaCalSection.TimeSlotPlanThreeID != 0 ? acaCalSection.TimeSlotPlanThreeID.ToString() : "0";
            ddlFaculty3.SelectedValue = acaCalSection.TeacherThreeID != 0 ? acaCalSection.TeacherThreeID.ToString() : "0";

           // ddlGradeSheetTemplate.SelectedValue = "0";// acaCalSection.GradeSheetTemplateID != 0 ? acaCalSection.GradeSheetTemplateID.ToString() : "0";
            txtSectionDefination.Text = acaCalSection.SectionDefination;
            txtRemark.Text = acaCalSection.Remarks;
            ddlOnlineGeadeSheet.SelectedValue = acaCalSection.BasicExamTemplateId.ToString();
            ddlCalculativeTemplate.SelectedValue = acaCalSection.CalculativeExamTemplateId.ToString();
            ddlSectionGender.SelectedValue = acaCalSection.SectionGenderID.ToString();

            foreach (ListItem item in chkListShareProgram.Items)
            {
                foreach (Program prog in acaCalSection.ShareProgram)
                {
                    if (Convert.ToInt32(item.Value) == prog.ProgramID)
                    {
                        item.Selected = true;
                    }
                }
            }

            if (acaCalSection.ShareBatch != null && acaCalSection.ShareBatch.Count != 0)
            {
                SessionManager.SaveListToSession<Batch>(acaCalSection.ShareBatch, Session_BatchList);
                ShowBatchList(acaCalSection.ShareBatch);
            }
            else
            {
                SessionManager.DeletFromSession(Session_BatchList);
            }

            ModalPopupExtender1.Show();
        }
        catch { lblMsg.Text = "Error: 104 This course isn't offered yet!"; }
        //(Load Select Row)
        finally { }
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id = Convert.ToInt32(linkButton.CommandArgument);
        int occupiedCount = AcademicCalenderSectionManager.GetById(id).Occupied;
        AcademicCalenderSection acacalsec = AcademicCalenderSectionManager.GetById(id);
        Course _course = CourseManager.GetByCourseIdVersionId(acacalsec.CourseID, acacalsec.VersionID);
        
        if (occupiedCount <= 0)
        {
            bool result = AcademicCalenderSectionManager.Delete(id);
            if (result)
            {
                lblMsg.Text = "Section deleted successfully.";
                #region Log Insert
                // Student student = StudentManager.GetById(registrationWorksheet.StudentID);
                LogGeneralManager.Insert(
                                                     DateTime.Now,
                                                     BaseAcaCalCurrent.Code,
                                                     BaseAcaCalCurrent.FullCode,
                                                     BaseCurrentUserObj.LogInID,
                                                     "",
                                                     "",
                                                     "deleted Section",
                                                     BaseCurrentUserObj.LogInID + " deleted Section for : " + _course.FormalCode + " : " + _course.Title + " , section :" + acacalsec.SectionName,
                                                     "normal",
                                                      ((int)CommonEnum.PageName.CourseExplorer).ToString(),
                                                     CommonEnum.PageName.CourseExplorer.ToString(),
                                                     _pageUrl,
                                                     "");
                #endregion
            }
            else
            {
                lblMsg.Text = "Section could not deleted successfully.";
            }
        }
        else 
        {
            lblMsg.Text = "Section could not deleted because it is occupied by " + occupiedCount + " student.";
        }
        LoadClassSchedule();
    }

    private void Update(int id)
    {
        AcademicCalenderSection acaCalSection = AcademicCalenderSectionManager.GetById(id);
        List<int> programIds = new List<int>();

        acaCalSection.SectionName = txtSection.Text;
        acaCalSection.Capacity = txtCapacity.Text.Trim() == "" ? 0 : Convert.ToInt32(txtCapacity.Text);

        acaCalSection.RoomInfoOneID = Convert.ToInt32(ddlRoomInfo1.SelectedValue);
        acaCalSection.DayOne = Convert.ToInt32(ddlDay1.SelectedValue);
        acaCalSection.TimeSlotPlanOneID = Convert.ToInt32(ddlTimeSlot1.SelectedValue);
        acaCalSection.TeacherOneID = Convert.ToInt32(ddlFaculty1.SelectedValue);
       
        acaCalSection.RoomInfoTwoID = Convert.ToInt32(ddlRoomInfo2.SelectedValue);
        acaCalSection.DayTwo = Convert.ToInt32(ddlDay2.SelectedValue);
        acaCalSection.TimeSlotPlanTwoID = Convert.ToInt32(ddlTimeSlot2.SelectedValue);
        acaCalSection.TeacherTwoID = Convert.ToInt32(ddlFaculty2.SelectedValue);

        acaCalSection.RoomInfoThreeID = Convert.ToInt32(ddlRoomInfo3.SelectedValue);
        acaCalSection.DayThree = Convert.ToInt32(ddlDay3.SelectedValue);
        acaCalSection.TimeSlotPlanThreeID = Convert.ToInt32(ddlTimeSlot3.SelectedValue);
        acaCalSection.TeacherThreeID = Convert.ToInt32(ddlFaculty3.SelectedValue);

        acaCalSection.BasicExamTemplateId = Convert.ToInt32(ddlOnlineGeadeSheet.SelectedValue); // Convert.ToInt32(ddlGradeSheetTemplate.SelectedValue);
        acaCalSection.CalculativeExamTemplateId = Convert.ToInt32(ddlCalculativeTemplate.SelectedValue);
        acaCalSection.SectionDefination = txtSectionDefination.Text;
        acaCalSection.SectionGenderID = Convert.ToInt32(ddlSectionGender.SelectedValue);
        acaCalSection.OnlineGradeSheetTemplateID = 0;
        acaCalSection.Remarks = txtRemark.Text.Trim();

        acaCalSection.ModifiedBy = 1;
        acaCalSection.ModifiedDate = DateTime.Now;

        foreach (ListItem item in chkListShareProgram.Items)
        {
            if (item.Selected)
            {
                programIds.Add(Convert.ToInt32(item.Value));
            }
        }

        bool result = AcademicCalenderSectionManager.Update(acaCalSection);
        Course _course = CourseManager.GetByCourseIdVersionId(acaCalSection.CourseID, acaCalSection.VersionID);
        
        if (result)
        {
            ShareProgramInSectionManager.DeleteByAcademicCalenderSectionId(acaCalSection.AcaCal_SectionID);

            foreach (int item in programIds)
            {
                ShareProgramInSection shareProgramInSection = new ShareProgramInSection();

                shareProgramInSection.AcademicCalenderSectionId = acaCalSection.AcaCal_SectionID;
                shareProgramInSection.ProgramId = item;

                ShareProgramInSectionManager.Insert(shareProgramInSection);
            }

            ShareBatchInSectionManager.DeleteByAcademicCalenderSectionId(id);

            batchList = SessionManager.GetListFromSession<Batch>(Session_BatchList);
            if (batchList != null)
            {
                foreach (Batch batch in batchList)
                {
                    ShareBatchInSection shareBatchInSection = new ShareBatchInSection();

                    shareBatchInSection.AcademicCalenderSectionId = id;
                    shareBatchInSection.BatchId = batch.BatchId;

                    ShareBatchInSectionManager.Insert(shareBatchInSection);
                }
            }
            #region Log Insert
            // Student student = StudentManager.GetById(registrationWorksheet.StudentID);
            LogGeneralManager.Insert(
                                                 DateTime.Now,
                                                 BaseAcaCalCurrent.Code,
                                                 BaseAcaCalCurrent.FullCode,
                                                 BaseCurrentUserObj.LogInID,
                                                 "",
                                                 "",
                                                 "update Section",
                                                 BaseCurrentUserObj.LogInID + " update Section for : " + _course.FormalCode + " : " + _course.Title + " , section :" + acaCalSection.SectionName,
                                                 "normal",
                                                  ((int)CommonEnum.PageName.ClassRoutine).ToString(),
                                                 CommonEnum.PageName.ClassRoutine.ToString(),
                                                 _pageUrl,
                                                 "");
            #endregion
        }

        #region Check Conflict
        if (id > 0)
        {
            AcademicCalenderSection academicCalenderSection = AcademicCalenderSectionManager.GetById(id);
            AcademicCalenderSectionManager.CheckConflictByRoom(academicCalenderSection, academicCalenderSection.RoomInfoOneID);
            AcademicCalenderSectionManager.CheckConflictByRoom(academicCalenderSection, academicCalenderSection.RoomInfoTwoID);

            AcademicCalenderSectionManager.CheckConflictByFaculty(academicCalenderSection, academicCalenderSection.TeacherOneID);
            AcademicCalenderSectionManager.CheckConflictByFaculty(academicCalenderSection, academicCalenderSection.TeacherTwoID);
        }
        #endregion
    }

    private void Insert()
    {
        string course = ddlCourse.SelectedValue;
        string[] courseVersion = course.Split('_');
        AcademicCalenderSection acaCalSection = new AcademicCalenderSection();
        List<int> programIds = new List<int>();

        Program program = ProgramManager.GetById(Convert.ToInt32(ucProgram.selectedValue));
        acaCalSection.AcademicCalenderID = Convert.ToInt32(ucSession.selectedValue);
        acaCalSection.ProgramID = program.ProgramID;
        acaCalSection.DeptID = program.DeptID;
        acaCalSection.CourseID = Convert.ToInt32(courseVersion[0]);
        acaCalSection.VersionID = Convert.ToInt32(courseVersion[1]);
        acaCalSection.SectionName = txtSection.Text;
        acaCalSection.Capacity = txtCapacity.Text.Trim() == "" ? 0 : Convert.ToInt32(txtCapacity.Text);

        acaCalSection.RoomInfoOneID = Convert.ToInt32(ddlRoomInfo1.SelectedValue);
        acaCalSection.DayOne = Convert.ToInt32(ddlDay1.SelectedValue);
        acaCalSection.TimeSlotPlanOneID = Convert.ToInt32(ddlTimeSlot1.SelectedValue);
        acaCalSection.TeacherOneID = Convert.ToInt32(ddlFaculty1.SelectedValue);

        acaCalSection.RoomInfoTwoID = Convert.ToInt32(ddlRoomInfo2.SelectedValue);
        acaCalSection.DayTwo = Convert.ToInt32(ddlDay2.SelectedValue);
        acaCalSection.TimeSlotPlanTwoID = Convert.ToInt32(ddlTimeSlot2.SelectedValue);
        acaCalSection.TeacherTwoID = Convert.ToInt32(ddlFaculty2.SelectedValue);

        acaCalSection.RoomInfoThreeID = Convert.ToInt32(ddlRoomInfo3.SelectedValue);
        acaCalSection.DayThree = Convert.ToInt32(ddlDay3.SelectedValue);
        acaCalSection.TimeSlotPlanThreeID = Convert.ToInt32(ddlTimeSlot3.SelectedValue);
        acaCalSection.TeacherThreeID = Convert.ToInt32(ddlFaculty3.SelectedValue);

        acaCalSection.SectionDefination = txtSectionDefination.Text;
        acaCalSection.SectionGenderID = Convert.ToInt32(ddlSectionGender.SelectedValue);
        acaCalSection.OnlineGradeSheetTemplateID = 0;
        acaCalSection.BasicExamTemplateId = Convert.ToInt32(ddlOnlineGeadeSheet.SelectedValue);
        acaCalSection.CalculativeExamTemplateId = Convert.ToInt32(ddlCalculativeTemplate.SelectedValue);
        acaCalSection.Remarks = txtRemark.Text.Trim();

        acaCalSection.CreatedBy = 1;
        acaCalSection.CreatedDate = DateTime.Now;
        acaCalSection.ModifiedBy = 1;
        acaCalSection.ModifiedDate = DateTime.Now;

        foreach (ListItem item in chkListShareProgram.Items)
        {
            if (item.Selected)
            {
                programIds.Add(Convert.ToInt32(item.Value));
            }
        }

        int id = AcademicCalenderSectionManager.Insert(acaCalSection);
        //AcademicCalenderSection academicCalenderSection = AcademicCalenderSectionManager.GetById(id);

        if (id > 0)
        {
            ShareProgramInSectionManager.DeleteByAcademicCalenderSectionId(id);
            foreach (int item in programIds)
            {
                ShareProgramInSection shareProgramInSection = new ShareProgramInSection();

                shareProgramInSection.AcademicCalenderSectionId = id;
                shareProgramInSection.ProgramId = item;

                ShareProgramInSectionManager.Insert(shareProgramInSection);
            }

            ShareBatchInSectionManager.DeleteByAcademicCalenderSectionId(id);

            batchList = SessionManager.GetListFromSession<Batch>(Session_BatchList);
            if (batchList != null)
            {
                foreach (Batch batch in batchList)
                {
                    ShareBatchInSection shareBatchInSection = new ShareBatchInSection();

                    shareBatchInSection.AcademicCalenderSectionId = id;
                    shareBatchInSection.BatchId = batch.BatchId;

                    ShareBatchInSectionManager.Insert(shareBatchInSection);
                }
            }
            #region Log Insert
            // Student student = StudentManager.GetById(registrationWorksheet.StudentID);
            LogGeneralManager.Insert(
                                                 DateTime.Now,
                                                 BaseAcaCalCurrent.Code,
                                                 BaseAcaCalCurrent.FullCode,
                                                 BaseCurrentUserObj.LogInID,
                                                 course,
                                                 "",
                                                 "Add Section",
                                                 BaseCurrentUserObj.LogInID + " add Section for course : " + course + " , " + acaCalSection.SectionName,
                                                 "normal",
                                                  ((int)CommonEnum.PageName.ClassRoutine).ToString(),
                                                 CommonEnum.PageName.ClassRoutine.ToString(),
                                                 _pageUrl,
                                                 "");
            #endregion
        } 

        #region Check Conflict
        if (id > 0)
        {
            AcademicCalenderSection academicCalenderSection = AcademicCalenderSectionManager.GetById(id);
            AcademicCalenderSectionManager.CheckConflictByRoom(academicCalenderSection, academicCalenderSection.RoomInfoOneID);
            AcademicCalenderSectionManager.CheckConflictByRoom(academicCalenderSection, academicCalenderSection.RoomInfoTwoID);

            AcademicCalenderSectionManager.CheckConflictByFaculty(academicCalenderSection, academicCalenderSection.TeacherOneID);
            AcademicCalenderSectionManager.CheckConflictByFaculty(academicCalenderSection, academicCalenderSection.TeacherTwoID);
        }
        #endregion

        LoadClassSchedule();
    }

    protected void btnUpdateInsert_Click(object sender, EventArgs e)
    {
        int id = 0;
        ModalPopupExtender1.Hide();

        if (btnUpdateInsert.CommandArgument != "")
            id = Convert.ToInt32(btnUpdateInsert.CommandArgument);

        if (ddlCourse.SelectedValue == "0")
        {
            lblMsg.Text = "Please select course.";
            lblMsg.Focus();
            return;
        }

        if (string.IsNullOrEmpty(txtSection.Text))
        {
            lblMsg.Text = "Please insert section name.";
            lblMsg.Focus();
            return;
        }

        if (btnUpdateInsert.Text == "Update")
        {
            btnUpdateInsert.CommandArgument = "";
            Update(id);
        }
        else if (btnUpdateInsert.Text == "New Add")
            Insert();

        RefreshPopUpField();
        // pnPopUp.Visible = false;
        // panelContainer.Enabled = true;
        // ddlCourse.Enabled = true;
        // LoadClassSchedule();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlCourse.Enabled = true;
        // pnPopUp.Visible = false;
        //panelContainer.Enabled = true;
        RefreshPopUpField();
    }

    private void RefreshPopUpField()
    {
        ddlCourse.Items.Clear();
        txtSection.Text = "";
        txtSectionDefination.Text = "";
        txtCapacity.Text = "";
        ddlSectionGender.SelectedIndex = 0;

        ddlRoomInfo1.Items.Clear();
        ddlDay1.Items.Clear();
        ddlTimeSlot1.Items.Clear();
        ddlFaculty1.Items.Clear();
       
        ddlRoomInfo2.Items.Clear();
        ddlDay2.Items.Clear();
        ddlTimeSlot2.Items.Clear();
        ddlFaculty2.Items.Clear();

        ddlRoomInfo3.Items.Clear();
        ddlDay3.Items.Clear();
        ddlTimeSlot3.Items.Clear();
        ddlFaculty3.Items.Clear();

        ddlOnlineGeadeSheet.Items.Clear();
        ucBatch.SelectedIndex0();
        lblShareBatch.Text = string.Empty;
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        if (ucSession.selectedValue == "0")
        {
            List<AcademicCalenderSection> acaCalSectionList = new List<AcademicCalenderSection>();
            gvClassSchedule.DataSource = acaCalSectionList;
            gvClassSchedule.DataBind();
            return;
        }

        lblMsg.Text = "";
        LoadClassSchedule();
    }

    protected void ddlSearch_Change(object sender, EventArgs e)
    {
        //string courseIDversionID = ddlSearch.SelectedValue;
        //if (courseIDversionID == "0")
        //    btnLoad_Click(null, null);
        //LoadClassSchedule();
    }

    //protected void btnCopyTo_Click(object sender, EventArgs e)
    //{
    //    int counterFlag = 0;
    //    try
    //    {
    //        if (ucSession.selectedValue == "0" || ucProgram.selectedValue == "0" || ddlAcaCalBatchCopyTo.SelectedValue == "0")
    //            lblMsg.Text = "Please Select Semester, Program and Copy To <b><i>Dropdown</i></b>";
    //        else
    //        {
    //            int copyFromSemesterId = Convert.ToInt32(ucSession.selectedValue);
    //            int copyToSemesterId = Convert.ToInt32(ddlAcaCalBatchCopyTo.SelectedValue);
    //            int programId = Convert.ToInt32(ucProgram.selectedValue);

    //            List<AcademicCalenderSection> academicCalenderSectionList = AcademicCalenderSectionManager.GetAllByAcaCalId(copyToSemesterId);
    //            if (academicCalenderSectionList.Count > 0 && academicCalenderSectionList != null)
    //                academicCalenderSectionList = academicCalenderSectionList.Where(x => x.ProgramID == programId).ToList();

    //            if (academicCalenderSectionList.Count > 0 && academicCalenderSectionList != null)
    //            {
    //                lblMsg.Text = "<b>Already Copy A Routine For This Semester.</b>";
    //            }
    //            else
    //            {
    //                List<AcademicCalenderSection> acaCalSecList = AcademicCalenderSectionManager.GetAllByAcaCalId(copyFromSemesterId);
    //                if (acaCalSecList.Count > 0 && acaCalSecList != null)
    //                {
    //                    acaCalSecList = acaCalSecList.Where(x => x.ProgramID == programId).ToList();
    //                    if (acaCalSecList.Count > 0 && acaCalSecList != null)
    //                    {
    //                        foreach (AcademicCalenderSection acaCalSec in acaCalSecList)
    //                        {
    //                            acaCalSec.AcademicCalenderID = copyToSemesterId;
    //                            int resultInsert = AcademicCalenderSectionManager.Insert(acaCalSec);
    //                            if (resultInsert > 0)
    //                                counterFlag++;
    //                        }
    //                    }
    //                    else
    //                        lblMsg.Text = "This Semester and Program has <b>NO</b> routine.";
    //                }
    //                else
    //                    lblMsg.Text = "This Semester has <b>NO</b> routine.";
    //            }
    //        }
    //    }
    //    catch { }
    //    finally
    //    {
    //        if (counterFlag != 0)
    //            lblMsg.Text = "<b>" + counterFlag + " Section Insert For Semester " + ddlAcaCalBatchCopyTo.SelectedItem.Text + "</b>";
    //    }
    //}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        List<AcademicCalenderSection> list = SessionManager.GetListFromSession<AcademicCalenderSection>("_acaCalSectionList");

        if (list != null)
        {
            LoadGrid(list);
        }
    }

    private void LoadGrid(List<AcademicCalenderSection> list)
    {
        if (!string.IsNullOrEmpty(txtFilterCourse.Text.Trim()))
        {
            list = list.Where(c => c.CourseInfo.ToLower().Contains(txtFilterCourse.Text.ToLower().Trim())).ToList();
        }

        if (!string.IsNullOrEmpty(txtFilterRoom.Text.Trim()))
        {
            list = list.Where(c => c.RoomInfoOne.ToLower().Contains(txtFilterRoom.Text.ToLower().Trim()) ||
                c.RoomInfoTwo.ToLower().Contains(txtFilterRoom.Text.ToLower().Trim())).ToList();
        }

        if (!string.IsNullOrEmpty(txtFilterDay.Text.Trim()))
        {
            list = list.Where(c => c.DayInfoOne.ToLower().Contains(txtFilterDay.Text.ToLower().Trim()) ||
                c.DayInfoTwo.ToLower().Contains(txtFilterDay.Text.ToLower().Trim())).ToList();
        }

        if (!string.IsNullOrEmpty(txtFilteTimeSlotn.Text.Trim()))
        {
            list = list.Where(c => c.TimeSlotPlanInfoOne.ToLower().Contains(txtFilteTimeSlotn.Text.ToLower().Trim()) ||
                c.TimeSlotPlanInfoTwo.ToLower().Contains(txtFilteTimeSlotn.Text.ToLower().Trim())).ToList();
        }

        if (!string.IsNullOrEmpty(txtFilterFaculty.Text.Trim()))
        {
            list = list.Where(c => c.TeacherInfoOne.ToLower().Contains(txtFilterFaculty.Text.ToLower().Trim()) ||
                c.TeacherInfoTwo.ToLower().Contains(txtFilterFaculty.Text.ToLower().Trim())).ToList();
        }

        gvClassSchedule.DataSource = list;
        gvClassSchedule.DataBind();
    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        ucBatch.LoadDropDownListForClassRoutine(Convert.ToInt32(ucProgram.selectedValue));
        //ucCopySession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));

    }

    protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }

    protected void lblR1_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id = Convert.ToInt32(linkButton.CommandArgument);

        #region Check Conflict

        AcademicCalenderSection academicCalenderSection = AcademicCalenderSectionManager.GetById(id);
        if (academicCalenderSection.RoomInfoOneID > 0)
            AcademicCalenderSectionManager.CheckConflictByRoom(academicCalenderSection, academicCalenderSection.RoomInfoOneID);

        #endregion

        LoadClassSchedule();
    }

    protected void lblR2_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id = Convert.ToInt32(linkButton.CommandArgument);

        #region Check Conflict

        AcademicCalenderSection academicCalenderSection = AcademicCalenderSectionManager.GetById(id);
        if (academicCalenderSection.RoomInfoTwoID > 0)
            AcademicCalenderSectionManager.CheckConflictByRoom(academicCalenderSection, academicCalenderSection.RoomInfoTwoID);

        #endregion

        LoadClassSchedule();
    }

    protected void lblT1_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id = Convert.ToInt32(linkButton.CommandArgument);

        #region Check Conflict

        AcademicCalenderSection academicCalenderSection = AcademicCalenderSectionManager.GetById(id);
        if (academicCalenderSection.TeacherOneID > 0)
            AcademicCalenderSectionManager.CheckConflictByFaculty(academicCalenderSection, academicCalenderSection.TeacherOneID);

        #endregion

        LoadClassSchedule();
    }

    protected void lblT2_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id = Convert.ToInt32(linkButton.CommandArgument);

        #region Check Conflict

        AcademicCalenderSection academicCalenderSection = AcademicCalenderSectionManager.GetById(id);
        if (academicCalenderSection.TeacherTwoID > 0)
            AcademicCalenderSectionManager.CheckConflictByFaculty(academicCalenderSection, academicCalenderSection.TeacherTwoID);

        #endregion

        LoadClassSchedule();
    }
    //protected void btnCopyTo_Click(object sender, EventArgs e)
    //{
    //    int counterFlag = 0;
    //    try
    //    {
    //        if (ucProgram.selectedValue == "0" || ucProgram.selectedValue == "0" || ucCopySession.selectedValue == "0")
    //            lblMsg.Text = "Please Select Semester, Program and Copy To <b><i>Dropdown</i></b>";
    //        else
    //        {
    //            int copyFromSemesterId = Convert.ToInt32(ucSession.selectedValue);
    //            int copyToSemesterId = Convert.ToInt32(ucCopySession.selectedValue);
    //            int programId = Convert.ToInt32(ucProgram.selectedValue);

    //            List<AcademicCalenderSection> academicCalenderSectionList = AcademicCalenderSectionManager.GetAllByAcaCalId(copyToSemesterId);
    //            if (academicCalenderSectionList != null && academicCalenderSectionList.Count > 0)
    //                academicCalenderSectionList = academicCalenderSectionList.Where(x => x.ProgramID == programId).ToList();

    //            if (academicCalenderSectionList != null && academicCalenderSectionList.Count > 0)
    //            {
    //                lblMsg.Text = "<b>Already Copy A Routine For This Semester.</b>";
    //            }
    //            else
    //            {
    //                List<AcademicCalenderSection> acaCalSecList = AcademicCalenderSectionManager.GetAllByAcaCalId(copyFromSemesterId);
    //                if (acaCalSecList != null && acaCalSecList.Count > 0)
    //                {
    //                    acaCalSecList = acaCalSecList.Where(x => x.ProgramID == programId).ToList();
    //                    if (acaCalSecList != null && acaCalSecList.Count >0)
    //                    {
    //                        foreach (AcademicCalenderSection acaCalSec in acaCalSecList)
    //                        {
    //                            acaCalSec.AcademicCalenderID = copyToSemesterId;
    //                            acaCalSec.Occupied = 0;
    //                            acaCalSec.CreatedBy = userObj.Id;
    //                            acaCalSec.CreatedDate = DateTime.Now;
    //                            int resultInsert = AcademicCalenderSectionManager.Insert(acaCalSec);
    //                            if (resultInsert > 0)
    //                                counterFlag++;
    //                        }

    //                        LogGeneralManager.Insert(
    //                                                         DateTime.Now,
    //                                                         "",
    //                                                         ucSession.selectedText,
    //                                                         userObj.LogInID,
    //                                                         "",
    //                                                         "",
    //                                                         "Copy Class Routine",
    //                                                         userObj.LogInID + " copied Class Routine for semester " + ucSession.selectedText,
    //                                                         userObj.LogInID + " is Copied Class Routine",
    //                                                          ((int)CommonEnum.PageName.Admin_ClassRoutine).ToString(),
    //                                                         CommonEnum.PageName.Admin_ClassRoutine.ToString(),
    //                                                         _pageUrl,
    //                                                         "");
    //                    }
    //                    else
    //                        lblMsg.Text = "This Semester and Program has <b>NO</b> routine.";
    //                }
    //                else
    //                    lblMsg.Text = "This Semester has <b>NO</b> routine.";
    //            }
    //        }
    //    }
    //    catch { }
    //    finally
    //    {
    //        if (counterFlag != 0)
    //            lblMsg.Text = "<b>" + counterFlag + " Section Insert For Semester " + ucCopySession.selectedText + "</b>";
    //    }
    //}
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (ucSession.selectedValue == "0" || ucProgram.selectedValue == "0")
        {
            lblMsg.Text = "Please Select Batch and Program.";
            return;
        }

        else if (flagCourseOffer == "NotOffer")
        {
            lblMsg.Text = "Course is not Offered.";
            return;
        }

        lblMsg.Text = "";
        btnUpdateInsert.Text = "New Add";
        ucBatch.SelectedIndex0();
        lblShareBatch.Text = string.Empty;
        LoadComboBox(Convert.ToInt32(ucProgram.selectedValue));
        SessionManager.DeletFromSession(Session_BatchList);

        ddlCourse.Enabled = true;
        ModalPopupExtender1.Show();
    }

    protected void btnAddAndNext_Click(object sender, EventArgs e)
    {
        int id = 0;
        if (btnUpdateInsert.CommandArgument != "")
            id = Convert.ToInt32(btnUpdateInsert.CommandArgument);

        if (ddlCourse.SelectedValue == "0")
        {
            lblMsg.Text = "Please select course.";
            lblMsg.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtSection.Text))
        {
            lblMsg.Text = "Please insert section name.";
            lblMsg.Focus();
            return;
        }

        if (btnUpdateInsert.Text == "Update")
        {
            btnUpdateInsert.CommandArgument = "";
            Update(id);
        }
        else if (btnUpdateInsert.Text == "New Add")
            Insert();

        DefaultField();

        ModalPopupExtender1.Show();
    }

    private void DefaultField()
    {
        ddlCourse.SelectedIndex = 0;
        txtSection.Text = "";
        txtSectionDefination.Text = "";
        txtCapacity.Text = "";
        ddlSectionGender.SelectedIndex = 0;

        ddlRoomInfo1.SelectedIndex = 0;
        ddlDay1.SelectedIndex = 0;
        ddlTimeSlot1.SelectedIndex = 0;
        ddlFaculty1.SelectedIndex = 0;
       
        ddlRoomInfo2.SelectedIndex = 0;
        ddlDay2.SelectedIndex = 0;
        ddlTimeSlot2.SelectedIndex = 0;
        ddlFaculty2.SelectedIndex = 0;

        ddlRoomInfo3.SelectedIndex = 0;
        ddlDay3.SelectedIndex = 0;
        ddlTimeSlot3.SelectedIndex = 0;
        ddlFaculty3.SelectedIndex = 0;

        ddlOnlineGeadeSheet.SelectedIndex = 0;
        ucBatch.SelectedIndex0();
        lblShareBatch.Text = string.Empty;
    }

    protected void btnBatchAdd_Click(object sender, EventArgs e)
    {
        batchList = SessionManager.GetListFromSession<Batch>(Session_BatchList);

        if (batchList == null)
            batchList = new List<Batch>();

        if (ucBatch.selectedValue == "-1")
        {
            batchList = null;
            batchList = BatchManager.GetAllByProgram(Convert.ToInt32(ucProgram.selectedValue));

            SessionManager.SaveListToSession<Batch>(batchList, Session_BatchList);
        }
        else
        {
            Batch batch = BatchManager.GetById(Convert.ToInt32(ucBatch.selectedValue));
            if (batch != null)
            {
                if (!batchList.Contains(batch))
                {
                    batchList.Add(batch);
                    SessionManager.SaveListToSession<Batch>(batchList, Session_BatchList);
                }
            }
        }

        ShowBatchList(batchList);
        ModalPopupExtender1.Show();
    }

    private void ShowBatchList(List<Batch> batchList)
    {
        lblShareBatch.Text = string.Empty;
        int f = 0;

        foreach (Batch item in batchList)
        {
            if (f == 0)
            {
                lblShareBatch.Text = item.BatchNO.ToString() + "; ";
                f = 1;
            }
            else
                lblShareBatch.Text += item.BatchNO.ToString() + "; ";
        }
    }

    protected void btnBatchRemove_Click(object sender, EventArgs e)
    {
        batchList = SessionManager.GetListFromSession<Batch>(Session_BatchList);
        if (batchList == null)
            batchList = new List<Batch>();


        if (ucBatch.selectedValue == "-1")
        {
            batchList = new List<Batch>();
            SessionManager.DeletFromSession(Session_BatchList);
        }
        else
        {

            Batch batch = BatchManager.GetById(Convert.ToInt32(ucBatch.selectedValue));
            if (batch != null)
            {
                if (!batchList.Contains(batch))
                {
                    batchList.RemoveAll(b => b.BatchId == batch.BatchId);
                    SessionManager.SaveListToSession<Batch>(batchList, Session_BatchList);
                }
            }
        }
        ShowBatchList(batchList);
        ModalPopupExtender1.Show();
    }
}