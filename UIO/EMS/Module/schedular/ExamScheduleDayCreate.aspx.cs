using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExamRoutine_ExamScheduleDayCreate : BasePage
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
        ddlDay.Items.Clear();
        ddlDay.Items.Add(new ListItem("Select", "0"));

        LoadCalenderType();
        //LoadAcademicCalender();
        //LoadExamScheduleSet();
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

            ExamScheduleSet examScheduleSet = ExamScheduleSetManager.GetById(examScheduleSetId);
            if (examScheduleSet != null)
                for (int i = 1; i <= examScheduleSet.TotalDay; i++)
                    ddlDay.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        catch { }
    }

    protected void ClearField()
    {
        ddlExamScheduleSet.SelectedValue = "0";
        ddlDay.Items.Clear();
        ddlDay.Items.Add(new ListItem("Select", "0"));
        txtDate.Text = "";
    }

    #endregion

    #region Event

    protected void btnBackLink_Click(Object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("ExamScheduleDayList.aspx");
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
        }
        catch { }
    }

    protected void btnCreate_Click(Object sender, EventArgs e)
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

            ExamScheduleDay examScheduleDay = new ExamScheduleDay();
            examScheduleDay.ExamScheduleSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);
            examScheduleDay.DayNo = Convert.ToInt32(ddlDay.SelectedValue);
            examScheduleDay.DayDate = string.IsNullOrEmpty(txtDate.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(txtDate.Text.Trim(), "dd/MM/yyyy", null);
            //Convert.ToDateTime(txtDate.Text);
            examScheduleDay.CreatedBy = createdBy;
            examScheduleDay.CreatedDate = DateTime.Now;

            int resultInsert = ExamScheduleDayManager.Insert(examScheduleDay);
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