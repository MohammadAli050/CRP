using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.schedular
{
    public partial class ExamScheduleSeatPlanManagementMixed : BasePage
    {

        #region Function

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                pnMaleFemaleCount.Visible = false;
                pnGenerateButton.Visible = false;
                pnSectionAssign.Visible = false;
                gvExamScheduleSeatPlan.Visible = false;
                LoadComboBox();
            }
        }

        protected void LoadComboBox()
        {
            ddlDay.Items.Clear();
            ddlDay.Items.Add(new ListItem("Select", "0"));
            ddlTimeSlot.Items.Clear();
            ddlTimeSlot.Items.Add(new ListItem("Select", "0"));

            LoadCalenderType();
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

                    academicCalenderList = academicCalenderList.Where(x => x.IsActiveRegistration == true).ToList();
                    ddlAcademicCalender.SelectedValue = academicCalenderList[0].AcademicCalenderID.ToString();

                    AcademicCalender_Changed(null, null);
                }
            }
            catch { }
        }

        protected void LoadExamScheduleSet(int acaCalId)
        {
            try
            {
                ddlExamScheduleSet.Items.Clear();
                ddlExamScheduleSet.Items.Add(new ListItem("Select", "0"));
                ddlExamScheduleSet.AppendDataBoundItems = true;

                List<ExamScheduleSet> examScheduleSetList = ExamScheduleSetManager.GetAllByAcaCalId(acaCalId);

                ddlExamScheduleSet.DataSource = examScheduleSetList;
                ddlExamScheduleSet.DataValueField = "Id";
                ddlExamScheduleSet.DataTextField = "SetName";
                ddlExamScheduleSet.DataBind();

                ExamScheduleSet_Changed(null, null);
            }
            catch { }
        }

        protected void LoadExamScheduleDay(int examScheduleSetId)
        {
            try
            {
                ddlDay.Items.Clear();
                ddlDay.Items.Add(new ListItem("Select", "0"));
                ddlDay.AppendDataBoundItems = true;

                List<ExamScheduleDay> examScheduleDayList = ExamScheduleDayManager.GetAllByExamSet(examScheduleSetId);

                ExamScheduleSet examScheduleSet = ExamScheduleSetManager.GetById(examScheduleSetId);
                if (examScheduleSet != null)
                {
                    for (int i = 1; i <= examScheduleSet.TotalDay; i++)
                    {
                        List<ExamScheduleDay> tempExamScheduleDayList = examScheduleDayList.Where(x => x.DayNo == i).ToList();
                        if (tempExamScheduleDayList.Count > 0)
                            ddlDay.Items.Add(new ListItem("Day" + tempExamScheduleDayList[0].DayNo + " [" + tempExamScheduleDayList[0].DayDate.ToString("dd-MMM-yyyy") + "]", tempExamScheduleDayList[0].Id.ToString()));
                        else
                            ddlDay.Items.Add(new ListItem("Day" + i.ToString(), "0"));
                    }
                }
            }
            catch { }
        }

        protected void LoadExamScheduleTimeSlot(int examScheduleSetId)
        {
            try
            {
                ddlTimeSlot.Items.Clear();
                ddlTimeSlot.Items.Add(new ListItem("Select", "0"));

                List<ExamScheduleTimeSlot> examScheduleTimeSlotList = ExamScheduleTimeSlotManager.GetAllByExamSet(examScheduleSetId);

                ExamScheduleSet examScheduleSet = ExamScheduleSetManager.GetById(examScheduleSetId);
                if (examScheduleSet != null)
                {
                    for (int i = 1; i <= examScheduleSet.TotalTimeSlot; i++)
                    {
                        List<ExamScheduleTimeSlot> tempExamScheduleTimeSlotList = examScheduleTimeSlotList.Where(x => x.TimeSlotNo == i).ToList();
                        if (tempExamScheduleTimeSlotList.Count > 0)
                            foreach (ExamScheduleTimeSlot e in tempExamScheduleTimeSlotList)
                                ddlTimeSlot.Items.Add(new ListItem("Slot" + e.TimeSlotNo + " [" + e.StartTime + "-" + e.EndTime, e.Id.ToString()));
                        else
                            ddlTimeSlot.Items.Add(new ListItem("Slot" + i, "0"));
                    }
                }
            }
            catch { }
        }

        protected void LoadExamScheduleData(int acaCalId, int examSetId, int dayId, int timeSlotId)
        {
            try
            {
                string maleFemaleNumber = ExamScheduleManager.GetTotalMaleFemale(acaCalId, dayId, timeSlotId);
                if (maleFemaleNumber.Length > 1)
                {
                    string[] maleFemaletotalNumber = maleFemaleNumber.Split('-');
                    txtStdCount.Text = (Convert.ToInt32(maleFemaletotalNumber[0]) + Convert.ToInt32(maleFemaletotalNumber[1])).ToString();
                    //txtFemaleCount.Text = maleFemaletotalNumber[1];
                }

                List<ExamSchedule> examScheduleList = ExamScheduleManager.GetAllByAcaCalExamSetDayTimeSlot(acaCalId, examSetId, dayId, timeSlotId);
                if (examScheduleList.Count > 0 && examScheduleList != null)
                {

                    #region Hash Table Zone

                    List<Course> courseList = CourseManager.GetAll();
                    Hashtable hashCourse = new Hashtable();
                    foreach (Course course in courseList)
                        hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.FormalCode + ":" + course.Title);

                    List<Program> programList = ProgramManager.GetAll();
                    Hashtable hashProgram = new Hashtable();
                    if (programList != null)
                        foreach (Program program in programList)
                            hashProgram.Add(program.ProgramID, program.ShortName);

                    List<ExamScheduleDay> examScheduleDayList = ExamScheduleDayManager.GetAllByExamSet(examSetId);
                    Hashtable hashExamScheduleDay = new Hashtable();
                    if (examScheduleDayList != null)
                        foreach (ExamScheduleDay examScheduleDay in examScheduleDayList)
                            hashExamScheduleDay.Add(examScheduleDay.Id, "Day" + examScheduleDay.DayNo + " [" + examScheduleDay.DayDate.ToString("dd-MMM-yyyy") + "]");

                    List<ExamScheduleTimeSlot> examScheduleTimeSlotList = ExamScheduleTimeSlotManager.GetAllByExamSet(examSetId);
                    Hashtable hashExamScheduleTimeSlot = new Hashtable();
                    if (examScheduleTimeSlotList != null)
                        foreach (ExamScheduleTimeSlot examScheduleTimeSlot in examScheduleTimeSlotList)
                            hashExamScheduleTimeSlot.Add(examScheduleTimeSlot.Id, "Slot" + examScheduleTimeSlot.TimeSlotNo + " [" + examScheduleTimeSlot.StartTime + "-" + examScheduleTimeSlot.EndTime + "]");

                    #endregion

                    foreach (ExamSchedule examSchedule in examScheduleList)
                    {
                        examSchedule.SectionList = "";
                        int flag = 0;
                        List<ExamScheduleSection> sectionList = ExamScheduleSectionManager.GetAllByExamSchedule(examSchedule.Id);
                        if (sectionList.Count > 0 && sectionList != null)
                        {
                            foreach (ExamScheduleSection examScheduleSection in sectionList)
                            {
                                if (flag == 1) examSchedule.SectionList += "-";
                                flag = 1;
                                examSchedule.SectionList += examScheduleSection.Section;
                            }
                        }
                        try
                        {
                            string totalMaleFemale = ExamScheduleManager.GetTotalStudentMaleFemale(examSchedule.Id);
                            if (totalMaleFemale.Length > 1)
                                examSchedule.StudentNo = totalMaleFemale;
                            else
                                examSchedule.StudentNo = "0-0";
                        }
                        catch
                        {
                            examSchedule.StudentNo = "0-0";
                        }
                        string courseIndex = examSchedule.CourseId.ToString() + "_" + examSchedule.VersionId.ToString();
                        examSchedule.CourseInfo = hashCourse[courseIndex] == null ? "" : hashCourse[courseIndex].ToString();
                        examSchedule.ProgramName = hashProgram[examSchedule.ProgramId] == null ? "" : hashProgram[examSchedule.ProgramId].ToString();
                        examSchedule.Day = hashExamScheduleDay[examSchedule.DayId] == null ? "" : hashExamScheduleDay[examSchedule.DayId].ToString();
                        examSchedule.TimeSlot = hashExamScheduleTimeSlot[examSchedule.TimeSlotId] == null ? "" : hashExamScheduleTimeSlot[examSchedule.TimeSlotId].ToString();
                    }

                    gvExamScheduleSeatPlan.DataSource = examScheduleList;
                    gvExamScheduleSeatPlan.DataBind();
                }
                else
                {
                    gvExamScheduleSeatPlan.DataSource = null;
                    gvExamScheduleSeatPlan.DataBind();
                }
            }
            catch { lblMsg.Text = "Error 2201"; }
            finally
            {
                gvExamScheduleSeatPlan.Visible = true;
            }
        }

        protected void LoadCampusComboBox()
        {
            try
            {
                List<Campus> campusList = CampusManager.GetAll();
                if (campusList.Count > 0 && campusList != null)
                {
                    ddlCampus.DataSource = campusList;
                    ddlCampus.DataValueField = "CampusId";
                    ddlCampus.DataTextField = "CampusName";
                    ddlCampus.DataBind();

                    Campus_Changed(null, null);
                }
            }
            catch { }
            finally { }
        }

        protected void LoadBuildingComboBox(int campusId)
        {
            try
            {
                List<Building> buildingList = BuildingManager.GetAll();
                if (buildingList.Count > 0 && buildingList != null)
                {
                    buildingList = buildingList.Where(x => x.CampusId == campusId).ToList();
                    if (buildingList.Count > 0 && buildingList != null)
                    {
                        ddlBuilding.DataSource = buildingList;
                        ddlBuilding.DataValueField = "BuildingId";
                        ddlBuilding.DataTextField = "BuildingName";
                        ddlBuilding.DataBind();

                        Building_Changed(null, null);
                    }
                }
            }
            catch { }
            finally { }
        }

        protected void LoadRoomComboBox(int buildingId, int acaCalId, int examScheduleSetId, int dayId, int timeSlotId)
        {
            try
            {
                List<RoomInformation> roomList = RoomInformationManager.GetAllByBuildingIdCustom(buildingId, acaCalId, examScheduleSetId, dayId, timeSlotId);
                if (roomList.Count > 0 && roomList != null)
                {
                    ddlRoom.Items.Clear();
                    //ddlRoom.Items.Add(new ListItem("Select", "0"));
                    //ddlRoom.AppendDataBoundItems = true;
                    foreach (RoomInformation roomInfo in roomList)
                        ddlRoom.Items.Add(new ListItem(roomInfo.RoomName + " (" + roomInfo.ExamCapacity + " Seats[R:" + roomInfo.Rows + ", C:" + roomInfo.Columns + "])", roomInfo.RoomInfoID.ToString()));

                    //ddlRoom.DataSource = roomList;
                    //ddlRoom.DataValueField = "RoomInfoID";
                    //ddlRoom.DataTextField = "RoomName";
                    //ddlRoom.DataBind();
                }
                else
                {
                    ddlRoom.Items.Clear();
                    ddlRoom.Items.Add(new ListItem("Select", "0"));
                    ddlRoom.AppendDataBoundItems = true;
                }
            }
            catch { }
            finally { }
        }

        protected void LoadSectionAssign()
        {
            try
            {
                pnSectionAssign.Visible = true;
                LoadCampusComboBox();
            }
            catch { }
            finally { }
        }

        protected void LoadRoomListBoxForMaleFemale(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId)
        {
            try
            {
                List<RoomInformation> roomList = RoomInformationManager.GetAll();
                if (roomList.Count > 0 && roomList != null)
                {
                    Hashtable hashRoomList = new Hashtable();
                    foreach (RoomInformation roomInformation in roomList)
                        hashRoomList.Add(roomInformation.RoomInfoID, roomInformation.RoomName + " (" + roomInformation.ExamCapacity + "Seats[R:" + roomInformation.Rows + ", C:" + roomInformation.Columns + "])");

                    List<ExamScheduleRoomInfo> examScheduleRoomList = ExamScheduleRoomInfoManager.GetAllByAcaCalExamSetDayTimeSlot(acaCalId, examScheduleSetId, dayId, timeSlotId);

                    if (examScheduleRoomList.Count > 0 && examScheduleRoomList != null)
                    {
                        lbMaleRoomList.Items.Clear();
                        List<ExamScheduleRoomInfo> examScheduleRoomListMale = examScheduleRoomList.Where(x => x.GenderType == "Mixed").ToList();
                        if (examScheduleRoomListMale.Count > 0 && examScheduleRoomListMale != null)
                        {
                            lbMaleRoomList.Items.Clear();
                            foreach (ExamScheduleRoomInfo temp in examScheduleRoomListMale)
                                lbMaleRoomList.Items.Add(new ListItem(hashRoomList[temp.RoomInfoId] == null ? "" : hashRoomList[temp.RoomInfoId].ToString(), temp.Id.ToString()));
                        }

                        /*List<ExamScheduleRoomInfo> examScheduleRoomListFemale = examScheduleRoomList.Where(x => x.GenderType == "Female").ToList();
                        if (examScheduleRoomListFemale.Count > 0 && examScheduleRoomListFemale != null)
                        {
                            lbFemaleRoomList.Items.Clear();
                            foreach (ExamScheduleRoomInfo temp in examScheduleRoomListFemale)
                                lbFemaleRoomList.Items.Add(new ListItem(hashRoomList[temp.RoomInfoId] == null ? "" : hashRoomList[temp.RoomInfoId].ToString(), temp.Id.ToString()));
                        }*/
                    }
                  
                }
            }
            catch { }
            finally { }
        }

        protected void GenerateSeatPlan(int acaCalId, int examSetId, int dayId, int timeSlotId)
        {
            try
            {
                bool resultDelete = ExamScheduleSeatPlanManager.DeleteByExamScheduleId(acaCalId, examSetId, dayId, timeSlotId);

                //All Course List Hashing for faster Retrieve FormalCode of Course
                List<Course> courseList = CourseManager.GetAll();
                Hashtable hashCourse = new Hashtable();
                foreach (Course course in courseList)
                    hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.FormalCode);
                //=================================================================


                //Get All Room List for All Room information in one Query
                List<RoomInformation> roomList = RoomInformationManager.GetAll();


                //Get All ExamScheduleRoom List which is assign for Exam acaCal-ExamSet-Day-TimeSlot
                List<ExamScheduleRoomInfo> examScheduleRoomList = ExamScheduleRoomInfoManager.GetAllByAcaCalExamSetDayTimeSlot(acaCalId, examSetId, dayId, timeSlotId);


                //If Room is Assign Then
                if (examScheduleRoomList.Count > 0 && examScheduleRoomList != null)
                {
                    //Separate Male and Female Room Number and Store defferent List
                    List<ExamScheduleRoomInfo> examScheduleRoomListMixed = examScheduleRoomList.Where(x => x.GenderType == "Mixed").ToList();
                    //List<ExamScheduleRoomInfo> examScheduleRoomListFemale = examScheduleRoomList.Where(x => x.GenderType == "Female").ToList();

                    //Total male and female number under this examSchedule and this value collected from front end
                    int totalStudent = Convert.ToInt32(txtStdCount.Text);
                   // int totalFemale = Convert.ToInt32(txtFemaleCount.Text);

                    //Get All ExamSchedule parameter is acaCal-ExamSet-Day-TimeSlot
                    List<ExamSchedule> examScheduleList = ExamScheduleManager.GetAllByAcaCalExamSetDayTimeSlot(acaCalId, examSetId, dayId, timeSlotId);

                    //Set total count male and female number
                    foreach (ExamSchedule examSchedule in examScheduleList)
                    {
                        string totalMaleFemale = ExamScheduleManager.GetTotalStudentMaleFemale(examSchedule.Id);
                        if (totalMaleFemale.Length > 1)
                        {
                            string[] maleFemaletotalNumber = totalMaleFemale.Split('-');
                            examSchedule.totalMale = Convert.ToInt32(maleFemaletotalNumber[0]);
                            examSchedule.totalFemale = Convert.ToInt32(maleFemaletotalNumber[1]);
                        }
                        else
                        {
                            examSchedule.totalMale = 0;
                            examSchedule.totalFemale = 0;
                        }
                    }

                    //For Male Student Seat Plan
                    if (examScheduleRoomListMixed.Count > 0)
                    {
                        List<ExamScheduleSeatPlan> examScheduleSeatPlanList = new List<ExamScheduleSeatPlan>();
                        int[] seatNumber = new int[100000];//this use for numbering seat(s);
                        int seatIndexing = 0;
                        for (int i = 0; i < examScheduleRoomListMixed.Count; i++)
                        {
                            int seatSequence = 1;
                            RoomInformation tempRoomInfo = roomList.Where(x => x.RoomInfoID == examScheduleRoomListMixed[i].RoomInfoId).FirstOrDefault();
                            for (int j = 0; j < tempRoomInfo.Columns; j++)
                            {
                                for (int k = 0; k < tempRoomInfo.Rows; k++)
                                {
                                    ExamScheduleSeatPlan examSeatPlan = new ExamScheduleSeatPlan();
                                    examSeatPlan.RowFlag = j % 2 == 0 ? "Even" : "Odd";
                                    examSeatPlan.SequenceNo = seatSequence++;
                                    examSeatPlan.RoomNo = tempRoomInfo.RoomInfoID;
                                    examSeatPlan.Attribute1 = tempRoomInfo.RoomName;
                                    examSeatPlan.IsPresent = true;
                                    examSeatPlan.CreatedBy = 99;
                                    examSeatPlan.CreatedDate = DateTime.Now;

                                    //examSeatPlan.ExamScheduleId = examScheduleRoomListMale[i].exa
                                    seatNumber[seatIndexing++] = j % 2;//Assign odd even number for odd Even rows;

                                    examScheduleSeatPlanList.Add(examSeatPlan);
                                }
                            }
                        }
                        seatNumber[seatIndexing] = 2;//2 is the border value;

                        examScheduleList = examScheduleList.OrderByDescending(x => (x.totalMale + x.totalFemale)).ToList();

                        int MaxCourseCountIndex = examScheduleList.Count;
                        int courseCountIndex = MaxCourseCountIndex;
                        ExamSchedule oddExamSchedule = new ExamSchedule();
                        ExamSchedule evenExamSchedule = new ExamSchedule();
                        int evenRowStudent = 0;
                        int oddRowStudent = 0;
                        int evenStudentIndexCount = 0;
                        int oddStudentIndexCount = 0;

                        List<ConflictStudentDTO> evenStudentList = new List<ConflictStudentDTO>();
                        List<ConflictStudentDTO> oddStudentList = new List<ConflictStudentDTO>();
                        if (MaxCourseCountIndex > 0)
                        {
                            evenExamSchedule = examScheduleList[courseCountIndex - MaxCourseCountIndex];
                            MaxCourseCountIndex = MaxCourseCountIndex - 1;
                            evenRowStudent = evenStudentIndexCount = evenExamSchedule.totalMale+evenExamSchedule.totalFemale;

                            evenStudentList = ExamScheduleManager.GetAllStudentRollbyExamSchedule(evenExamSchedule.Id);
                        }
                        if (MaxCourseCountIndex > 0)
                        {
                            oddExamSchedule = examScheduleList[courseCountIndex - MaxCourseCountIndex];
                            MaxCourseCountIndex = MaxCourseCountIndex - 1;
                            oddRowStudent = oddStudentIndexCount = oddExamSchedule.totalMale+oddExamSchedule.totalFemale;

                            oddStudentList = ExamScheduleManager.GetAllStudentRollbyExamSchedule(oddExamSchedule.Id);
                        }

                        #region Mainly Assign Roll and Course Name

                        int realSeatNumber = 0;
                        for (int i = 0; i < seatIndexing; i++)
                        {
                            if (examScheduleSeatPlanList[i] != null)
                            {

                                if (examScheduleSeatPlanList[i].RowFlag == "Even" && evenRowStudent != 0)
                                {
                                    realSeatNumber++;
                                    string courseIndex = evenExamSchedule.CourseId.ToString() + "_" + evenExamSchedule.VersionId.ToString();
                                    string courseCode = hashCourse[courseIndex] == null ? "" : hashCourse[courseIndex].ToString();

                                    examScheduleSeatPlanList[i].ExamScheduleId = evenExamSchedule.Id;
                                    examScheduleSeatPlanList[i].Roll = evenStudentList[evenStudentIndexCount - evenRowStudent].Roll;
                                    examScheduleSeatPlanList[i].CourseCode = courseCode;
                                    evenRowStudent--;
                                    if (evenRowStudent == 0 && MaxCourseCountIndex > 0)
                                    {
                                        evenExamSchedule = examScheduleList[courseCountIndex - MaxCourseCountIndex];
                                        MaxCourseCountIndex = MaxCourseCountIndex - 1;
                                        evenRowStudent = evenStudentIndexCount = evenExamSchedule.totalMale+evenExamSchedule.totalFemale;

                                        evenStudentList = ExamScheduleManager.GetAllStudentRollbyExamSchedule(evenExamSchedule.Id);
                                    }
                                }
                                else if (examScheduleSeatPlanList[i].RowFlag == "Odd" && oddRowStudent != 0)
                                {
                                    realSeatNumber++;
                                    string courseIndex = oddExamSchedule.CourseId.ToString() + "_" + oddExamSchedule.VersionId.ToString();
                                    string courseCode = hashCourse[courseIndex] == null ? "" : hashCourse[courseIndex].ToString();

                                    examScheduleSeatPlanList[i].ExamScheduleId = oddExamSchedule.Id;
                                    examScheduleSeatPlanList[i].Roll = oddStudentList[oddStudentIndexCount - oddRowStudent].Roll;
                                    examScheduleSeatPlanList[i].CourseCode = courseCode;
                                    oddRowStudent--;
                                    if (oddRowStudent == 0 && MaxCourseCountIndex > 0)
                                    {
                                        oddExamSchedule = examScheduleList[courseCountIndex - MaxCourseCountIndex];
                                        MaxCourseCountIndex = MaxCourseCountIndex - 1;
                                        oddRowStudent = oddStudentIndexCount = oddExamSchedule.totalMale+oddExamSchedule.totalFemale;

                                        oddStudentList = ExamScheduleManager.GetAllStudentRollbyExamSchedule(oddExamSchedule.Id);
                                    }
                                }
                                else if (evenRowStudent != 0)
                                {
                                    realSeatNumber++;
                                    string courseIndex = evenExamSchedule.CourseId.ToString() + "_" + evenExamSchedule.VersionId.ToString();
                                    string courseCode = hashCourse[courseIndex] == null ? "" : hashCourse[courseIndex].ToString();

                                    examScheduleSeatPlanList[i].ExamScheduleId = evenExamSchedule.Id;
                                    examScheduleSeatPlanList[i].Roll = evenStudentList[evenStudentIndexCount - evenRowStudent].Roll;
                                    examScheduleSeatPlanList[i].CourseCode = courseCode;
                                    evenRowStudent--;
                                    if (evenRowStudent == 0 && MaxCourseCountIndex > 0)
                                    {
                                        evenExamSchedule = examScheduleList[courseCountIndex - MaxCourseCountIndex];
                                        MaxCourseCountIndex = MaxCourseCountIndex - 1;
                                        evenRowStudent = evenStudentIndexCount = evenExamSchedule.totalMale+evenExamSchedule.totalFemale;

                                        evenStudentList = ExamScheduleManager.GetAllStudentRollbyExamSchedule(evenExamSchedule.Id);
                                    }
                                }
                                else if (oddRowStudent != 0)
                                {
                                    realSeatNumber++;
                                    string courseIndex = oddExamSchedule.CourseId.ToString() + "_" + oddExamSchedule.VersionId.ToString();
                                    string courseCode = hashCourse[courseIndex] == null ? "" : hashCourse[courseIndex].ToString();

                                    examScheduleSeatPlanList[i].ExamScheduleId = oddExamSchedule.Id;
                                    examScheduleSeatPlanList[i].Roll = oddStudentList[oddStudentIndexCount - oddRowStudent].Roll;
                                    examScheduleSeatPlanList[i].CourseCode = courseCode;
                                    oddRowStudent--;
                                    if (evenRowStudent == 0 && MaxCourseCountIndex > 0)
                                    {
                                        oddExamSchedule = examScheduleList[courseCountIndex - MaxCourseCountIndex];
                                        MaxCourseCountIndex = MaxCourseCountIndex - 1;
                                        oddRowStudent = oddStudentIndexCount = oddExamSchedule.totalMale+oddExamSchedule.totalFemale;

                                        oddStudentList = ExamScheduleManager.GetAllStudentRollbyExamSchedule(oddExamSchedule.Id);
                                    }
                                }
                            }
                        }
                        #endregion

                        int flagStudent = 0;
                        for (int i = 0; i < realSeatNumber; i++)
                        {
                            int resultInsert = ExamScheduleSeatPlanManager.Insert(examScheduleSeatPlanList[i]);
                            if (resultInsert > 0)
                                flagStudent++;
                        }
                        lblMsg.Text += " -> " + flagStudent.ToString() + " Students";
                    }
                    else
                    {
                        lblMsg.Text += " -> No Room are assigned for Male Student";
                    }

                    /*
                    //For Female Student Seat Plan
                    if (examScheduleRoomListFemale.Count > 0)
                    {
                        //This list declare for store student Seat information
                        List<ExamScheduleSeatPlan> examScheduleSeatPlanList = new List<ExamScheduleSeatPlan>();

                        //Trace odd even seat number
                        int[] seatNumber = new int[100000];//this use for numbering seat(s);

                        //this indexing value use for store data in ExamScheduleSeatPlan List
                        int seatIndexing = 0;

                        //First Step to prepare ExamScheduleSeatPlan Data but not complete the list
                        for (int i = 0; i < examScheduleRoomListFemale.Count; i++)
                        {
                            int seatSequence = 1;
                            RoomInformation tempRoomInfo = roomList.Where(x => x.RoomInfoID == examScheduleRoomListFemale[i].RoomInfoId).FirstOrDefault();
                            for (int j = 0; j < tempRoomInfo.Columns; j++)
                            {
                                for (int k = 0; k < tempRoomInfo.Rows; k++)
                                {
                                    ExamScheduleSeatPlan examSeatPlan = new ExamScheduleSeatPlan();
                                    examSeatPlan.RowFlag = j % 2 == 0 ? "Even" : "Odd";
                                    examSeatPlan.SequenceNo = seatSequence++;
                                    examSeatPlan.RoomNo = tempRoomInfo.RoomInfoID;
                                    examSeatPlan.Attribute1 = tempRoomInfo.RoomName;
                                    examSeatPlan.IsPresent = true;
                                    examSeatPlan.CreatedBy = 99;
                                    examSeatPlan.CreatedDate = DateTime.Now;

                                    //examSeatPlan.ExamScheduleId = examScheduleRoomListMale[i].exa
                                    seatNumber[seatIndexing++] = j % 2;//Assign odd even number for odd Even rows;

                                    examScheduleSeatPlanList.Add(examSeatPlan);
                                }
                            }
                        }
                        seatNumber[seatIndexing] = 2;//2 is the border value;

                        examScheduleList = examScheduleList.OrderByDescending(x => x.totalFemale).ToList();

                        int MaxCourseCountIndex = examScheduleList.Count;
                        int courseCountIndex = MaxCourseCountIndex;
                        ExamSchedule oddExamSchedule = new ExamSchedule();
                        ExamSchedule evenExamSchedule = new ExamSchedule();
                        int evenRowStudent = 0;
                        int oddRowStudent = 0;
                        int evenStudentIndexCount = 0;
                        int oddStudentIndexCount = 0;

                        List<ConflictStudentDTO> evenStudentList = new List<ConflictStudentDTO>();
                        List<ConflictStudentDTO> oddStudentList = new List<ConflictStudentDTO>();
                        if (MaxCourseCountIndex > 0)
                        {
                            evenExamSchedule = examScheduleList[courseCountIndex - MaxCourseCountIndex];
                            MaxCourseCountIndex = MaxCourseCountIndex - 1;
                            evenRowStudent = evenStudentIndexCount = evenExamSchedule.totalFemale;

                            evenStudentList = ExamScheduleManager.GetAllStudentRollbyExamScheduleGender(evenExamSchedule.Id, "Female");
                        }
                        if (MaxCourseCountIndex > 0)
                        {
                            oddExamSchedule = examScheduleList[courseCountIndex - MaxCourseCountIndex];
                            MaxCourseCountIndex = MaxCourseCountIndex - 1;
                            oddRowStudent = oddStudentIndexCount = oddExamSchedule.totalFemale;

                            oddStudentList = ExamScheduleManager.GetAllStudentRollbyExamScheduleGender(oddExamSchedule.Id, "Female");
                        }

                        #region Mainly Assign Roll and Course Name

                        int realSeatNumber = 0;
                        for (int i = 0; i < seatIndexing; i++)
                        {
                            if (examScheduleSeatPlanList[i] != null)
                            {

                                if (examScheduleSeatPlanList[i].RowFlag == "Even" && evenRowStudent != 0)
                                {
                                    realSeatNumber++;
                                    string courseIndex = evenExamSchedule.CourseId.ToString() + "_" + evenExamSchedule.VersionId.ToString();
                                    string courseCode = hashCourse[courseIndex] == null ? "" : hashCourse[courseIndex].ToString();

                                    examScheduleSeatPlanList[i].ExamScheduleId = evenExamSchedule.Id;
                                    examScheduleSeatPlanList[i].Roll = evenStudentList[evenStudentIndexCount - evenRowStudent].Roll;
                                    examScheduleSeatPlanList[i].CourseCode = courseCode;
                                    evenRowStudent--;
                                    if (evenRowStudent == 0 && MaxCourseCountIndex > 0)
                                    {
                                        evenExamSchedule = examScheduleList[courseCountIndex - MaxCourseCountIndex];
                                        MaxCourseCountIndex = MaxCourseCountIndex - 1;
                                        evenRowStudent = evenStudentIndexCount = evenExamSchedule.totalFemale;

                                        evenStudentList = ExamScheduleManager.GetAllStudentRollbyExamScheduleGender(evenExamSchedule.Id, "Female");
                                    }
                                }
                                else if (examScheduleSeatPlanList[i].RowFlag == "Odd" && oddRowStudent != 0)
                                {
                                    realSeatNumber++;
                                    string courseIndex = oddExamSchedule.CourseId.ToString() + "_" + oddExamSchedule.VersionId.ToString();
                                    string courseCode = hashCourse[courseIndex] == null ? "" : hashCourse[courseIndex].ToString();

                                    examScheduleSeatPlanList[i].ExamScheduleId = oddExamSchedule.Id;
                                    examScheduleSeatPlanList[i].Roll = oddStudentList[oddStudentIndexCount - oddRowStudent].Roll;
                                    examScheduleSeatPlanList[i].CourseCode = courseCode;
                                    oddRowStudent--;
                                    if (oddRowStudent == 0 && MaxCourseCountIndex > 0)
                                    {
                                        oddExamSchedule = examScheduleList[courseCountIndex - MaxCourseCountIndex];
                                        MaxCourseCountIndex = MaxCourseCountIndex - 1;
                                        oddRowStudent = oddStudentIndexCount = oddExamSchedule.totalFemale;

                                        oddStudentList = ExamScheduleManager.GetAllStudentRollbyExamScheduleGender(oddExamSchedule.Id, "Female");
                                    }
                                }
                                else if (evenRowStudent != 0)
                                {
                                    realSeatNumber++;
                                    string courseIndex = evenExamSchedule.CourseId.ToString() + "_" + evenExamSchedule.VersionId.ToString();
                                    string courseCode = hashCourse[courseIndex] == null ? "" : hashCourse[courseIndex].ToString();

                                    examScheduleSeatPlanList[i].ExamScheduleId = evenExamSchedule.Id;
                                    examScheduleSeatPlanList[i].Roll = evenStudentList[evenStudentIndexCount - evenRowStudent].Roll;
                                    examScheduleSeatPlanList[i].CourseCode = courseCode;
                                    evenRowStudent--;
                                    if (evenRowStudent == 0 && MaxCourseCountIndex > 0)
                                    {
                                        evenExamSchedule = examScheduleList[courseCountIndex - MaxCourseCountIndex];
                                        MaxCourseCountIndex = MaxCourseCountIndex - 1;
                                        evenRowStudent = evenStudentIndexCount = evenExamSchedule.totalFemale;

                                        evenStudentList = ExamScheduleManager.GetAllStudentRollbyExamScheduleGender(evenExamSchedule.Id, "Female");
                                    }
                                }
                                else if (oddRowStudent != 0)
                                {
                                    realSeatNumber++;
                                    string courseIndex = oddExamSchedule.CourseId.ToString() + "_" + oddExamSchedule.VersionId.ToString();
                                    string courseCode = hashCourse[courseIndex] == null ? "" : hashCourse[courseIndex].ToString();

                                    examScheduleSeatPlanList[i].ExamScheduleId = oddExamSchedule.Id;
                                    examScheduleSeatPlanList[i].Roll = oddStudentList[oddStudentIndexCount - oddRowStudent].Roll;
                                    examScheduleSeatPlanList[i].CourseCode = courseCode;
                                    oddRowStudent--;
                                    if (evenRowStudent == 0 && MaxCourseCountIndex > 0)
                                    {
                                        oddExamSchedule = examScheduleList[courseCountIndex - MaxCourseCountIndex];
                                        MaxCourseCountIndex = MaxCourseCountIndex - 1;
                                        oddRowStudent = oddStudentIndexCount = oddExamSchedule.totalFemale;

                                        oddStudentList = ExamScheduleManager.GetAllStudentRollbyExamScheduleGender(oddExamSchedule.Id, "Female");
                                    }
                                }
                            }
                        }
                        #endregion

                        int flagFemale = 0;
                        for (int i = 0; i < realSeatNumber; i++)
                        {
                            int resultInsert = ExamScheduleSeatPlanManager.Insert(examScheduleSeatPlanList[i]);
                            if (resultInsert > 0)
                                flagFemale++;
                        }
                        lblMsg.Text += " -> " + flagFemale.ToString() + " Female Student";
                    }
                    else
                    {
                        lblMsg.Text += " -> No Room are assigned for Female Student";
                    }*/
                }
                else //If Room is not assign for ExamSchedule
                {
                    lblMsg.Text += " -> No Room are aasign for Male and Female Student";
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

                pnMaleFemaleCount.Visible = false;
                pnGenerateButton.Visible = false;
                pnSectionAssign.Visible = false;
                gvExamScheduleSeatPlan.Visible = false;
            }
            catch { }
        }

        protected void AcademicCalender_Changed(Object sender, EventArgs e)
        {
            try
            {
                int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
                LoadExamScheduleSet(acaCalId);

                pnMaleFemaleCount.Visible = false;
                pnGenerateButton.Visible = false;
                pnSectionAssign.Visible = false;
                gvExamScheduleSeatPlan.Visible = false;
            }
            catch { }
        }

        protected void ExamScheduleSet_Changed(Object sender, EventArgs e)
        {
            try
            {
                int examScheduleSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);
                LoadExamScheduleDay(examScheduleSetId);
                LoadExamScheduleTimeSlot(examScheduleSetId);

                pnMaleFemaleCount.Visible = false;
                pnGenerateButton.Visible = false;
                pnSectionAssign.Visible = false;
                gvExamScheduleSeatPlan.Visible = false;
            }
            catch { }
        }

        protected void Day_Changed(Object sender, EventArgs e)
        {
            try
            {
                pnMaleFemaleCount.Visible = false;
                pnGenerateButton.Visible = false;
                pnSectionAssign.Visible = false;
                gvExamScheduleSeatPlan.Visible = false;
            }
            catch { }
            finally { }
        }

        protected void TimeSlot_Changed(Object sender, EventArgs e)
        {
            try
            {
                pnMaleFemaleCount.Visible = false;
                pnGenerateButton.Visible = false;
                pnSectionAssign.Visible = false;
                gvExamScheduleSeatPlan.Visible = false;
            }
            catch { }
            finally { }
        }

        protected void btnLoad_Click(Object sender, EventArgs e)
        {
            try
            {
                LoadSectionAssign();
                if (ddlAcademicCalender.SelectedValue == "0" || ddlExamScheduleSet.SelectedValue == "0" || ddlDay.SelectedValue == "0" || ddlTimeSlot.SelectedValue == "0")
                {
                    lblMsg.Text = "Please select all dropdown";
                    return;
                }

                pnMaleFemaleCount.Visible = true;
                pnGenerateButton.Visible = true;

                lblMsg.Text = "";
                int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
                int examSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);
                int dayId = Convert.ToInt32(ddlDay.SelectedValue);
                int timeSlotId = Convert.ToInt32(ddlTimeSlot.SelectedValue);

                LoadExamScheduleData(acaCalId, examSetId, dayId, timeSlotId);
            }
            catch { }
            finally { }
        }

        //protected void ddlOddEvenRows_Changed(Object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int oddRow = 0;
        //        int evenRow = 0;

        //        foreach (GridViewRow row in gvExamScheduleSeatPlan.Rows)
        //        {
        //            DropDownList dropDownList = (DropDownList)row.FindControl("ddlOddEvenRows");
        //            Label label = (Label)row.FindControl("lblStudentNo");
        //            string[] totalStudentNo = label.Text.Split('-');
        //            int totalStudent = Convert.ToInt32(totalStudentNo[0]) + Convert.ToInt32(totalStudentNo[1]);
        //            if (dropDownList.SelectedValue == "1")
        //                oddRow += totalStudent;
        //            else if (dropDownList.SelectedValue == "2")
        //                evenRow += totalStudent;
        //        }
        //        //txtEvenRow.Text = evenRow.ToString();
        //        //txtOddRow.Text = oddRow.ToString();
        //    }
        //    catch { }
        //    finally { }
        //}

        protected void btnGenerateSeatPlan_Click(Object sender, EventArgs e)
        {
            try
            {
                int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
                int examSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);
                int dayId = Convert.ToInt32(ddlDay.SelectedValue);
                int timeSlotId = Convert.ToInt32(ddlTimeSlot.SelectedValue);

                GenerateSeatPlan(acaCalId, examSetId, dayId, timeSlotId);

                //int modifiedBy = 99;
                //HttpCookie aCookie = Request.Cookies[ConstantValue.Cookie_Authentication];
                //string uid = aCookie["UserName"];
                //User user = UserManager.GetByLogInId(uid);
                //if (user != null)
                //    modifiedBy = user.User_ID;

                //foreach (GridViewRow row in gvExamScheduleSeatPlan.Rows)
                //{
                //    HiddenField hf = (HiddenField)row.FindControl("hfId");
                //    int id = Convert.ToInt32(hf.Value);
                //    ExamSchedule exam = ExamScheduleManager.GetById(id);

                //    DropDownList dropDownList = (DropDownList)row.FindControl("ddlOddEvenRows");
                //    if (exam != null)
                //    {
                //        if (dropDownList.SelectedValue == "1")
                //            exam.RowFlag = "Odd";
                //        else if (dropDownList.SelectedValue == "2")
                //            exam.RowFlag = "Even";

                //        exam.ModifiedBy = modifiedBy;
                //        exam.ModifiedDate = DateTime.Now;
                //        bool updateResult = ExamScheduleManager.Update(exam);
                //    }
                //}
                ////
                //int result = ExamScheduleSeatPlanManager.GenerateSeatPlan(Convert.ToInt32(ddlAcademicCalender.SelectedValue), Convert.ToInt32(ddlExamScheduleSet.SelectedValue), Convert.ToInt32(ddlDay.SelectedValue), Convert.ToInt32(ddlTimeSlot.SelectedValue));
                //if (result > 0)
                //    lblMsg.Text = result + " Student Seat Plan is Ready";
            }
            catch { }
            finally { }
        }

        protected void Campus_Changed(Object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(ddlCampus.SelectedValue);

                LoadBuildingComboBox(id);
            }
            catch { }
            finally { }
        }

        protected void Building_Changed(Object sender, EventArgs e)
        {
            try
            {
                int buildingId = Convert.ToInt32(ddlBuilding.SelectedValue);
                int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
                int examScheduleSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);
                int dayId = Convert.ToInt32(ddlDay.SelectedValue);
                int timeSlotId = Convert.ToInt32(ddlTimeSlot.SelectedValue);

                LoadRoomComboBox(buildingId, acaCalId, examScheduleSetId, dayId, timeSlotId);

                LoadRoomListBoxForMaleFemale(acaCalId, examScheduleSetId, dayId, timeSlotId);
            }
            catch { }
            finally { }
        }

        protected void btnRoom_Click(Object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlRoom.SelectedValue) != 0)
                {
                    string roomName = ddlRoom.SelectedItem.Text;

                    int createdBy = 99;
                    string loginId = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
                    User user = UserManager.GetByLogInId(loginId);
                    if (user != null)
                        createdBy = user.User_ID;

                    ExamScheduleRoomInfo examRoom = new ExamScheduleRoomInfo();
                    examRoom.ExamScheduleSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);
                    examRoom.AcaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
                    examRoom.DayId = Convert.ToInt32(ddlDay.SelectedValue);
                    examRoom.TimeSlotId = Convert.ToInt32(ddlTimeSlot.SelectedValue);
                    examRoom.RoomInfoId = Convert.ToInt32(ddlRoom.SelectedValue);
                    examRoom.GenderType = "Mixed";
                    examRoom.CreatedBy = createdBy;
                    examRoom.CreatedDate = DateTime.Now;

                    int resultInsert = ExamScheduleRoomInfoManager.Insert(examRoom);
                    if (resultInsert > 0)
                    {
                        lblMsg.Text = "Room is assigned";
                        lbMaleRoomList.Items.Add(new ListItem(roomName, resultInsert.ToString()));
                        Building_Changed(null, null);
                    }
                }
                else
                {
                    lblMsg.Text = "Please select Room number";
                }
            }
            catch { }
            finally { }
        }


        protected void btnRoomDelete_Click(Object sender, EventArgs e)
        {
            try
            {
                if (lbMaleRoomList.SelectedIndex != -1)
                {
                    int id = Convert.ToInt32(lbMaleRoomList.SelectedValue);
                    bool resultDelete = ExamScheduleRoomInfoManager.Delete(id);

                    if (resultDelete)
                    {
                        lbMaleRoomList.Items.RemoveAt(lbMaleRoomList.SelectedIndex);
                        lblMsg.Text = "Delete successfully";
                        Building_Changed(null, null);
                    }
                }
                else
                {
                    lblMsg.Text = "Please check the room number";
                }
            }
            catch { }
            finally { }
        }

        #endregion
    }
}