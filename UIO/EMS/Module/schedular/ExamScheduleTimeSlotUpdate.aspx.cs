using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExamRoutine_ExamScheduleTimeSlotUpdate : BasePage
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
            if (this.Request.QueryString["ExamScheduleTimeSlot"] != null)
            {
                int id = int.Parse(this.Request.QueryString["ExamScheduleTimeSlot"]);
                btnUpdate.CommandArgument = id.ToString();
                LoadingExamScheduleTimeSlot(id);
            }            
        }
    }

    protected void LoadComboBox()
    {
        LoadComboTime();
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
                ddlEndHour.Items.Add(new ListItem(i.ToString().PadLeft(2, '0'), i.ToString()));
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

    protected void LoadingExamScheduleTimeSlot(int id)
    {
        try
        {
            ExamScheduleTimeSlot examScheduleTimeSlot = ExamScheduleTimeSlotManager.GetById(id);
            if (examScheduleTimeSlot != null)
            {
                string[] startTime = examScheduleTimeSlot.StartTime.Split(':');
                ddlStartHour.Items.FindByText(startTime[0]).Selected = true;
                ddlStartMin.Items.FindByText(startTime[1]).Selected = true;
                ddlStartAmPm.Items.FindByText(startTime[2]).Selected = true;

                string[] endTime = examScheduleTimeSlot.EndTime.Split(':');
                ddlEndHour.Items.FindByText(endTime[0]).Selected = true;
                ddlEndMin.Items.FindByText(endTime[1]).Selected = true;
                ddlEndAmPm.Items.FindByText(endTime[2]).Selected = true;

                LoadExamScheduleSet(examScheduleTimeSlot.ExamScheduleSetId);
                ddlTimeSlot.SelectedValue = examScheduleTimeSlot.TimeSlotNo.ToString();
            }
        }
        catch{}
    }

    protected void LoadExamScheduleSet(int examScheduleSetId)
    {
        try
        {
            ddlExamScheduleSet.Items.Clear();
            ddlExamScheduleSet.Items.Add(new ListItem("Select", "0"));
            ddlExamScheduleSet.AppendDataBoundItems = true;

            ExamScheduleSet examScheduleSet = ExamScheduleSetManager.GetById(examScheduleSetId);
            ddlExamScheduleSet.Items.Add(new ListItem(examScheduleSet.SetName, examScheduleSet.Id.ToString()));

            ddlExamScheduleSet.SelectedValue = examScheduleSetId.ToString();
            ddlExamScheduleSet.Enabled = false;

            LoadExamScheduleTimeSlot(examScheduleSet.TotalTimeSlot);
        }
        catch { }
    }

    protected void LoadExamScheduleTimeSlot(int totalTimeSlot)
    {
        try
        {
            ddlTimeSlot.Items.Clear();
            ddlTimeSlot.Items.Add(new ListItem("Select", "0"));

            for (int i = 1; i <= totalTimeSlot; i++)
                ddlTimeSlot.Items.Add(new ListItem(i.ToString(), i.ToString()));

            ddlTimeSlot.Enabled = false;
        }
        catch { }
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

    protected void btnUpdate_Click(Object sender, EventArgs e)
    {
        try
        {
            int modifiedBy = 100;
            //HttpCookie aCookie = Request.Cookies[ConstantValue.Cookie_Authentication];
            //string uid = aCookie["UserName"];
            string loginID = userObj.LogInID;
            User user = UserManager.GetByLogInId(loginID);
            if (user != null)
                modifiedBy = user.User_ID;


            int id = Convert.ToInt32(btnUpdate.CommandArgument);
            ExamScheduleTimeSlot examScheduleTimeSlot = ExamScheduleTimeSlotManager.GetById(id);
            examScheduleTimeSlot.StartTime = ddlStartHour.SelectedItem.Text + ":" + ddlStartMin.SelectedItem.Text + ":" + ddlStartAmPm.SelectedItem.Text;
            examScheduleTimeSlot.EndTime = ddlEndHour.SelectedItem.Text + ":" + ddlEndMin.SelectedItem.Text + ":" + ddlEndAmPm.SelectedItem.Text;
            examScheduleTimeSlot.ModifiedBy = modifiedBy;
            examScheduleTimeSlot.ModifiedDate = DateTime.Now;

            bool resultUpdate = ExamScheduleTimeSlotManager.Update(examScheduleTimeSlot);
            if (resultUpdate)
            {
                lblMsg.Text = "Insert Successfully";
                Response.Redirect("ExamScheduleTimeSlotList.aspx");
            }
            else
                lblMsg.Text = "Update Fail";
        }
        catch
        {
            lblMsg.Text = "Error 902";
        }
    }

    #endregion
}