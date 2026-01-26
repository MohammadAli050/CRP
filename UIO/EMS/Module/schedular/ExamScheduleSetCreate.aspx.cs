using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExamRoutine_ExamScheduleSetCreate : BasePage
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
        LoadCalenderType();
        //LoadAcademicCalender();
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
            }
        }
        catch { }
    }

    protected void ClearField()
    {
        ddlAcademicCalender.SelectedValue = "0";
        txtExamSetName.Text = "";
        txtTotalDay.Text = "";
        txtTodayTimeSlot.Text = "";
    }

    #endregion

    #region Event

    protected void btnBackLink_Click(Object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("ExamScheduleSetList.aspx");
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

            ExamScheduleSet examScheduleSet = new ExamScheduleSet();
            examScheduleSet.AcaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            examScheduleSet.SetName = txtExamSetName.Text;
            examScheduleSet.TotalDay = Convert.ToInt32(txtTotalDay.Text);
            examScheduleSet.TotalTimeSlot = Convert.ToInt32(txtTodayTimeSlot.Text);
            examScheduleSet.CreatedBy = createdBy;
            examScheduleSet.CreatedDate = DateTime.Now;

            int resultInsert = ExamScheduleSetManager.Insert(examScheduleSet);
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