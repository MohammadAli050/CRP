using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExamRoutine_ExamScheduleTimeSlotCreate : BasePage
{
    BussinessObject.UIUMSUser userObj = null;
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        if (!IsPostBack)
        {
            LoadComboBox();
        }
    }

    protected void LoadComboBox()
    {
        ddlExamScheduleSet.Items.Clear();
        ddlExamScheduleSet.Items.Add(new ListItem("Select", "0"));
        ddlTimeSlot.Items.Clear();
        ddlTimeSlot.Items.Add(new ListItem("Select", "0"));

        LoadCalenderType();
        //LoadAcademicCalender();
        //LoadExamScheduleSet();
        LoadComboTime();
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

    protected void LoadExamScheduleTimeSlot(int examScheduleSetId)
    {
        try
        {
            ddlTimeSlot.Items.Clear();
            ddlTimeSlot.Items.Add(new ListItem("Select", "0"));

            ExamScheduleSet examScheduleSet = ExamScheduleSetManager.GetById(examScheduleSetId);
            if (examScheduleSet != null)
                for (int i = 1; i <= examScheduleSet.TotalTimeSlot; i++)
                    ddlTimeSlot.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        catch { }
    }

    protected void LoadComboTime()
    {
        try
        {
            ddlStartHour.Items.Clear();
            ddlStartHour.Items.Add(new ListItem("----", "0"));
            ddlStartHour.AppendDataBoundItems = true;
            ddlStartMin.Items.Clear();
            ddlStartMin.Items.Add(new ListItem("----", "0"));
            ddlStartMin.AppendDataBoundItems = true;
            ddlStartAmPm.Items.Clear();
            ddlStartAmPm.Items.Add(new ListItem("----", "0"));
            ddlStartAmPm.AppendDataBoundItems = true;

            ddlEndHour.Items.Clear();
            ddlEndHour.Items.Add(new ListItem("----", "0"));
            ddlEndHour.AppendDataBoundItems = true;
            ddlEndMin.Items.Clear();
            ddlEndMin.Items.Add(new ListItem("----", "0"));
            ddlEndMin.AppendDataBoundItems = true;
            ddlEndAmPm.Items.Clear();
            ddlEndAmPm.Items.Add(new ListItem("----", "0"));
            ddlEndAmPm.AppendDataBoundItems = true;

            for (int i = 1; i <= 12; i++)
            {
                ddlStartHour.Items.Add(new ListItem(i.ToString().PadLeft(2, '0'), i.ToString()));
                ddlEndHour.Items.Add(new ListItem(i.ToString().PadLeft(2, '0'), i.ToString())); ;
            }
            for (int i = 1; i < 60; i++)
            {
                ddlStartMin.Items.Add(new ListItem(i.ToString().PadLeft(2, '0'), i.ToString()));
                ddlEndMin.Items.Add(new ListItem(i.ToString().PadLeft(2, '0'), i.ToString()));
            }
            ddlStartMin.Items.Add(new ListItem("00", "00"));
            ddlEndMin.Items.Add(new ListItem("00", "00"));

            ddlStartAmPm.Items.Add(new ListItem("AM", "1"));
            ddlStartAmPm.Items.Add(new ListItem("PM", "2"));

            ddlEndAmPm.Items.Add(new ListItem("AM", "1"));
            ddlEndAmPm.Items.Add(new ListItem("PM", "2"));
        }
        catch { }
    }

    protected void ClearField()
    {
        //ddlAcademicCalender.SelectedValue = "0";
        ddlExamScheduleSet.SelectedValue = "0";
        ddlTimeSlot.Items.Clear();
        ddlTimeSlot.Items.Add(new ListItem("Select", "0"));
        ddlStartHour.SelectedValue = "0";
        ddlStartMin.SelectedValue = "0";
        ddlStartAmPm.SelectedValue = "0";
        ddlEndHour.SelectedValue = "0";
        ddlEndMin.SelectedValue = "0";
        ddlEndAmPm.SelectedValue = "0";
    }

    #endregion

    #region Event

    protected void btnBackLink_Click(Object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("ExamScheduleTimeSlotList.aspx");
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
            LoadExamScheduleTimeSlot(examScheduleSetId);
        }
        catch { }
    }

    protected void btnInsert_Click(Object sender, EventArgs e)
    {
        try
        {
            int createdBy = 99;
            //HttpCookie aCookie = Request.Cookies[ConstantValue.Cookie_Authentication];
            //string uid = aCookie["UserName"];
            string loginID = userObj.LogInID;
            User user = UserManager.GetByLogInId(loginID);
            if (user != null)
                createdBy = user.User_ID;

            ExamScheduleTimeSlot examScheduleTimeSlot = new ExamScheduleTimeSlot();
            examScheduleTimeSlot.ExamScheduleSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);
            examScheduleTimeSlot.TimeSlotNo = Convert.ToInt32(ddlTimeSlot.SelectedValue);
            examScheduleTimeSlot.StartTime = ddlStartHour.SelectedItem.Text + ":" + ddlStartMin.SelectedItem.Text + ":" + ddlStartAmPm.SelectedItem.Text;
            examScheduleTimeSlot.EndTime = ddlEndHour.SelectedItem.Text + ":" + ddlEndMin.SelectedItem.Text + ":" + ddlEndAmPm.SelectedItem.Text;
            examScheduleTimeSlot.CreatedBy = createdBy;
            examScheduleTimeSlot.CreatedDate = DateTime.Now;

            int resultInsert = ExamScheduleTimeSlotManager.Insert(examScheduleTimeSlot);
            if (resultInsert > 0)
            {
                lblMsg.Text = "Insert Successfully";
                ClearField();
            }
            else
                lblMsg.Text = "Insert Fail";
        }
        catch
        {
            lblMsg.Text = "Error 901";
        }
    }

    #endregion
}