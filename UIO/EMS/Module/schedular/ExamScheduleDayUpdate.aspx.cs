using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExamRoutine_ExamScheduleDayUpdate : BasePage
{
    BussinessObject.UIUMSUser userObj = null;
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        if (!IsPostBack)
        {
            if (this.Request.QueryString["ExamScheduleDay"] != null)
            {
                int id = int.Parse(this.Request.QueryString["ExamScheduleDay"]);
                btnUpdate.CommandArgument = id.ToString();
                LoadingExamScheduleDay(id);
            }
        }
    }

    protected void LoadingExamScheduleDay(int id)
    {
        try
        {
            ExamScheduleDay examScheduleDay = ExamScheduleDayManager.GetById(id);
            if (examScheduleDay != null)
            {
                txtDate.Text = Convert.ToDateTime(examScheduleDay.DayDate).ToString("dd/MM/yyyy");
                LoadExamScheduleSet(examScheduleDay.ExamScheduleSetId);
            }
        }
        catch
        {
        }
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

            LoadExamScheduleDay(examScheduleSet.TotalDay);
        }
        catch { }
    }

    protected void LoadExamScheduleDay(int totalDay)
    {
        try
        {
            ddlDay.Items.Clear();
            ddlDay.Items.Add(new ListItem("Select", "0"));

            for (int i = 1; i <= totalDay; i++)
                ddlDay.Items.Add(new ListItem(i.ToString(), i.ToString()));

            ddlDay.SelectedValue = totalDay.ToString();
            ddlDay.Enabled = false;
        }
        catch { }
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
            ExamScheduleDay examScheduleDay = ExamScheduleDayManager.GetById(id);
            examScheduleDay.DayDate = string.IsNullOrEmpty(txtDate.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(txtDate.Text.Trim(), "dd/MM/yyyy", null);
            examScheduleDay.ModifiedBy = modifiedBy;
            examScheduleDay.ModifiedDate = DateTime.Now;

            bool resultUpdate = ExamScheduleDayManager.Update(examScheduleDay);
            if (resultUpdate)
            {
                lblMsg.Text = "Insert Successfully";
                Response.Redirect("ExamScheduleDayList.aspx");
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