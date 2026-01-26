using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_RptExamSeatPlan : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack)
        {
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

    protected void CalenderType_Changed(Object sender, EventArgs e)
    {
        try
        {
            int calenderTypeId = Convert.ToInt32(ddlCalenderType.SelectedValue);
            LoadAcademicCalender(calenderTypeId);
        }
        catch { }
    }

    protected void AcademicCalender_Changed(Object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            LoadExamScheduleSet(acaCalId);
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
        }
        catch { }
    }



    protected void btnLoad_Click(object sender, EventArgs e)
    {
        int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
        string examScheduleSetId = Convert.ToString(ddlExamScheduleSet.SelectedItem);
        int calenderUnitMasterId = Convert.ToInt32(ddlCalenderType.SelectedValue);
        int dayId = Convert.ToInt32(ddlDay.SelectedValue);
        int timeSlotId = Convert.ToInt32(ddlTimeSlot.SelectedValue);


        LoadExamSeatPlan(acaCalId, examScheduleSetId, calenderUnitMasterId, dayId, timeSlotId);
    }

    private void LoadExamSeatPlan(int acaCalId, string examScheduleSetId, int calenderUnitMasterId, int dayId, int timeSlotId)
    {
        List<rExamSeatPlan> examSeatplan = ExamScheduleSeatPlanManager.GetExamSeatPlan(acaCalId, examScheduleSetId, calenderUnitMasterId, dayId, timeSlotId);

        for (int i = 0; i < examSeatplan.Count; i++ )
        {
            if (examSeatplan[i].SequenceNo.Equals("6") || examSeatplan[i].SequenceNo.Equals("11"))
            {
                rExamSeatPlan a = new rExamSeatPlan();
                a.Roll = "";
                a.RoomNo = examSeatplan[i].RoomNo;
                a.CourseCode = "";
                a.SequenceNo = "";
                examSeatplan.Insert(i + 1, a);
                i = i + 1;
            }
        }
        ReportDataSource rds = new ReportDataSource("RptExamSeatPlan", examSeatplan);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(rds);

        lblCount.Text = examSeatplan.Count().ToString();
    }
}