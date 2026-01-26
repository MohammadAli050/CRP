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

public partial class ExamStudentPresent : BasePage
{
    BussinessObject.UIUMSUser userObj = null;
    string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);


        lblMsg.Text = "";
        if (!IsPostBack)
        {
            pnSubmitStudentAttendanceTop.Visible = false;
            pnSubmitStudentAttendanceBottom.Visible = false;
            gvExamStudentPresent.Visible = false;
            LoadComboBox();
        }
    }

    protected void LoadComboBox()
    {
        ddlDay.Items.Clear();
        ddlDay.Items.Add(new ListItem("Select", "0"));
        ddlTimeSlot.Items.Clear();
        ddlTimeSlot.Items.Add(new ListItem("Select", "0"));
        ddlRoom.Items.Clear();
        ddlRoom.Items.Add(new ListItem("Select", "0"));

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

    protected void LoadRoom(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId)
    {
        try
        {
            ddlRoom.Items.Clear();
            ddlRoom.Items.Add(new ListItem("Select", "0"));
            ddlRoom.AppendDataBoundItems = true;


            List<RoomInformation> roomList = RoomInformationManager.GetAll();

            if (roomList.Count > 0 && roomList != null)
            {
                Hashtable hashRoomList = new Hashtable();
                foreach (RoomInformation roomInformation in roomList)
                    hashRoomList.Add(roomInformation.RoomInfoID, roomInformation.RoomName + " (" + roomInformation.ExamCapacity + "Seats[R:" + roomInformation.Rows + ", C:" + roomInformation.Columns + "])");

                List<ExamScheduleRoomInfo> examScheduleRoomList = ExamScheduleRoomInfoManager.GetAllByAcaCalExamSetDayTimeSlot(acaCalId, examScheduleSetId, dayId, timeSlotId);

                if (examScheduleRoomList.Count > 0 && examScheduleRoomList != null)
                {
                    List<ExamScheduleRoomInfo> examScheduleRoomListMale = examScheduleRoomList.Where(x => x.GenderType == "Male").ToList();
                    if (examScheduleRoomListMale.Count > 0 && examScheduleRoomListMale != null)
                    {
                        foreach (ExamScheduleRoomInfo temp in examScheduleRoomListMale)
                            ddlRoom.Items.Add(new ListItem(hashRoomList[temp.RoomInfoId] == null ? "" : hashRoomList[temp.RoomInfoId].ToString() + "-Male", temp.RoomInfoId.ToString()));
                    }

                    List<ExamScheduleRoomInfo> examScheduleRoomListFemale = examScheduleRoomList.Where(x => x.GenderType == "Female").ToList();
                    if (examScheduleRoomListFemale.Count > 0 && examScheduleRoomListFemale != null)
                    {
                        foreach (ExamScheduleRoomInfo temp in examScheduleRoomListFemale)
                            ddlRoom.Items.Add(new ListItem(hashRoomList[temp.RoomInfoId] == null ? "" : hashRoomList[temp.RoomInfoId].ToString() + "Female", temp.RoomInfoId.ToString()));
                    }

                    List<ExamScheduleRoomInfo> examScheduleRoomListMixed = examScheduleRoomList.Where(x => x.GenderType == "Mixed").ToList();
                    if (examScheduleRoomListMixed.Count > 0 && examScheduleRoomListMixed != null)
                    {
                        foreach (ExamScheduleRoomInfo temp in examScheduleRoomListMixed)
                            ddlRoom.Items.Add(new ListItem(hashRoomList[temp.RoomInfoId] == null ? "" : hashRoomList[temp.RoomInfoId].ToString() + "Mixed", temp.RoomInfoId.ToString()));
                    }
                }
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

            pnSubmitStudentAttendanceTop.Visible = false;
            pnSubmitStudentAttendanceBottom.Visible = false;
            gvExamStudentPresent.Visible = false;
        }
        catch { }
    }

    protected void AcademicCalender_Changed(Object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            LoadExamScheduleSet(acaCalId);

            pnSubmitStudentAttendanceTop.Visible = false;
            pnSubmitStudentAttendanceBottom.Visible = false;
            gvExamStudentPresent.Visible = false;
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

            pnSubmitStudentAttendanceTop.Visible = false;
            pnSubmitStudentAttendanceBottom.Visible = false;
            gvExamStudentPresent.Visible = false;
        }
        catch { }
    }

    protected void Day_Changed(Object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            int examSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);
            int dayId = Convert.ToInt32(ddlDay.SelectedValue);
            int timeSlotId = Convert.ToInt32(ddlTimeSlot.SelectedValue);

            LoadRoom(acaCalId, examSetId, dayId, timeSlotId);

            pnSubmitStudentAttendanceTop.Visible = false;
            pnSubmitStudentAttendanceBottom.Visible = false;
            gvExamStudentPresent.Visible = false;
        }
        catch { }
        finally { }
    }

    protected void TimeSlot_Changed(Object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            int examSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);
            int dayId = Convert.ToInt32(ddlDay.SelectedValue);
            int timeSlotId = Convert.ToInt32(ddlTimeSlot.SelectedValue);

            LoadRoom(acaCalId, examSetId, dayId, timeSlotId);

            pnSubmitStudentAttendanceTop.Visible = false;
            pnSubmitStudentAttendanceBottom.Visible = false;
            gvExamStudentPresent.Visible = false;
        }
        catch { }
        finally { }
    }

    protected void btnLoad_Click(Object sender, EventArgs e)
    {
        try
        {
            if (ddlAcademicCalender.SelectedValue == "0" || ddlExamScheduleSet.SelectedValue == "0" || ddlDay.SelectedValue == "0" || ddlTimeSlot.SelectedValue == "0" || ddlRoom.SelectedValue == "0")
            {
                lblMsg.Text = "Please select all dropdown";
                return;
            }

            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            int examSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);
            int dayId = Convert.ToInt32(ddlDay.SelectedValue);
            int timeSlotId = Convert.ToInt32(ddlTimeSlot.SelectedValue);
            int roomId = Convert.ToInt32(ddlRoom.SelectedValue);

            List<ExamScheduleSeatPlan> examScheduleSeatPlanList = ExamScheduleSeatPlanManager.GetAllByAcaCalExamSetDayTimeSlotRoom(acaCalId, examSetId, dayId, timeSlotId, roomId);

            if (examScheduleSeatPlanList.Count > 0 && examScheduleSeatPlanList != null)
            {
                pnSubmitStudentAttendanceTop.Visible = true;
                pnSubmitStudentAttendanceBottom.Visible = true;
                gvExamStudentPresent.Visible = true;

                gvExamStudentPresent.DataSource = examScheduleSeatPlanList;
                gvExamStudentPresent.DataBind();
            }
            else
            {
                gvExamStudentPresent.DataSource = null;
                gvExamStudentPresent.DataBind();

                pnSubmitStudentAttendanceTop.Visible = false;
                pnSubmitStudentAttendanceBottom.Visible = false;
                gvExamStudentPresent.Visible = false;
            }
        }
        catch { }
        finally { }
    }

    protected void SubmitAllAttendance_Click(Object sender, EventArgs e)
    {
        try
        {
            int userId = 99;

            string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();

            User user = UserManager.GetByLogInId(loginID);
            if (user != null) userId = user.User_ID;

            int flagPresent = 0;
            int flagAbsence = 0;
            foreach (GridViewRow row in gvExamStudentPresent.Rows)
            {
                HiddenField hfId = (HiddenField)row.FindControl("hfId");
                CheckBox chkIsPresent = (CheckBox)row.FindControl("chkIsPresent");

                ExamScheduleSeatPlan examScheduleSeatPlan = ExamScheduleSeatPlanManager.GetById(Convert.ToInt32(hfId.Value));
                examScheduleSeatPlan.IsPresent = chkIsPresent.Checked;
                bool resultUpdate = ExamScheduleSeatPlanManager.Update(examScheduleSeatPlan);

                if (resultUpdate && chkIsPresent.Checked)
                    flagPresent++;
                else if (resultUpdate && !chkIsPresent.Checked)
                    flagAbsence++;

            }
            #region Log Insert

            LogGeneralManager.Insert(
                        DateTime.Now,
                        ddlAcademicCalender.SelectedValue,
                        ddlAcademicCalender.SelectedItem.Text,
                        userObj.LogInID,
                        "",
                        "",
                        "Save",
                        userObj.LogInID + " saved student's attendance for " + ddlExamScheduleSet.SelectedItem.Text + ", " + ddlDay.SelectedItem.Text + ", " + ddlTimeSlot.SelectedItem.Text + ", " + ddlRoom.SelectedItem.Text,
                        userObj.LogInID + " saved attendance ",
                        ((int)CommonEnum.PageName.ExamStudentPresent).ToString(),
                        CommonEnum.PageName.ExamStudentPresent.ToString(),
                        _pageUrl,
                        "");
            #endregion
            lblMsg.Text = flagPresent + " Student are Present and " + flagAbsence + " Student are Absent";
        }
        catch { }
        finally { }
    }

    #endregion
}