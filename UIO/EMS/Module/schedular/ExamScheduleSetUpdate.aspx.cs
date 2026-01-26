using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExamRoutine_ExamScheduleSetUpdate : BasePage
{
    BussinessObject.UIUMSUser userObj = null;
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        if (!IsPostBack)
        {
            if (this.Request.QueryString["ExamScheduleSet"] != null)
            {
                int id = int.Parse(this.Request.QueryString["ExamScheduleSet"]);
                btnUpdate.CommandArgument = id.ToString();
                LoadExamScheduleSet(id);
            }
        }
    }

    protected void LoadExamScheduleSet(int id)
    {
        try
        {
            ExamScheduleSet examScheduleSet = ExamScheduleSetManager.GetById(id);
            txtExamSetName.Text = examScheduleSet.SetName;
            txtTotalDay.Text = examScheduleSet.TotalDay.ToString();
            txtTotalTimeSlot.Text = examScheduleSet.TotalTimeSlot.ToString();
        }
        catch
        {
        }
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

    protected void btnUpdate_Click(Object sender, EventArgs e)
    {
        try
        {
            int modifiedBy = 100;
            //HttpCookie aCookie = Request.Cookies[ConstantValue.Cookie_Authentication];
            string loginID = userObj.LogInID;
            User user = UserManager.GetByLogInId(loginID);
            if (user != null)
                modifiedBy = user.User_ID;


            int id = Convert.ToInt32(btnUpdate.CommandArgument);
            ExamScheduleSet examScheduleSet = ExamScheduleSetManager.GetById(id);

            examScheduleSet.SetName = txtExamSetName.Text;
            examScheduleSet.TotalDay = Convert.ToInt32(txtTotalDay.Text);
            examScheduleSet.TotalTimeSlot = Convert.ToInt32(txtTotalTimeSlot.Text);
            examScheduleSet.ModifiedBy = modifiedBy;
            examScheduleSet.ModifiedDate = DateTime.Now;

            bool resultUpdate = ExamScheduleSetManager.Update(examScheduleSet);
            if (resultUpdate)
            {
                lblMsg.Text = "Insert Successfully";
                Response.Redirect("ExamScheduleSetList.aspx");
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