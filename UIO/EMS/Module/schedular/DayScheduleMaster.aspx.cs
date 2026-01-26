using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DayScheduleMaster : BasePage
{
    int userId = 0;
    BussinessObject.UIUMSUser userObj = null;
    string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
        User user = UserManager.GetByLogInId(loginID);
        if (user != null)
            userId = user.User_ID;

        ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
        _scriptMan.AsyncPostBackTimeout = 36000;

        lblMsg.Text = "";

        if (!IsPostBack)
        {
            ucProgram.LoadDropdownWithUserAccess(userId);
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ShowPopUpMessage("", Color.Green);
        try
        {
            GridViewRow gvrow = (GridViewRow)(((Button)sender)).NamingContainer;

            HiddenField hdnId = (HiddenField)gvrow.FindControl("hdnId");

            List<DayScheduleDetail> list = DayScheduleDetailManager.GetAllByDayScheduleMasterId(Convert.ToInt32(hdnId.Value));

            if (list != null)
            {
                gvDayScheduleDetails.DataSource = list;
                gvDayScheduleDetails.DataBind();
                ShowMessage("", Color.Red);
            }

            ModalPopupExtender1.Show();
        }
        catch
        {
            ShowMessage("Something Went Wrong! Please Try Again.", Color.Red);
        }
        finally { }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrow = (GridViewRow)(((Button)sender)).NamingContainer;

            DropDownList ddlMakeUpDay = (DropDownList)gvrow.FindControl("ddlMakeUpDay");
            TextBox txtWeekNo = (TextBox)gvrow.FindControl("txtWeekNo");

            HiddenField hdnId = (HiddenField)gvrow.FindControl("hdnId");

            LogicLayer.BusinessObjects.DayScheduleMaster dsm = DayScheduleMasterManager.GetById(Convert.ToInt32(hdnId.Value));

            if (dsm != null)
            {
                dsm.MakeUpDayId = Convert.ToInt32(ddlMakeUpDay.SelectedValue);
                dsm.WeekNo = Convert.ToInt32(txtWeekNo.Text.Trim());

                dsm.ModifiedBy = BaseCurrentUserObj.Id;
                dsm.ModifiedDate = DateTime.Now;

                bool isUpdate = DayScheduleMasterManager.Update(dsm);
                if (isUpdate)
                {
                    #region Log Insert
                    //LogGeneralManager.Insert(
                    //    DateTime.Now,
                    //    BaseAcaCalCurrent.Code,
                    //    BaseAcaCalCurrent.FullCode,
                    //    BaseCurrentUserObj.LogInID,
                    //    "",
                    //    "",
                    //    "Day Schedule Master Save",
                    //    "",
                    //    "Normal",
                    //    ((int)CommonEnum.PageName.DayScheduleMaster).ToString(),
                    //    CommonEnum.PageName.DayScheduleMaster.ToString(),
                    //    _pageUrl,
                    //    "");
                    #endregion

                    ShowMessage("Successfylly Updated", Color.Green);
                }
            }


        }
        catch (Exception)
        {
        }
    }

    protected void btnSaveAll_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            foreach (GridViewRow gvrow in gvDayScheduleMaster.Rows)
            {
                DropDownList ddlMakeUpDay = (DropDownList)gvrow.FindControl("ddlMakeUpDay");
                TextBox txtWeekNo = (TextBox)gvrow.FindControl("txtWeekNo");

                HiddenField hdnId = (HiddenField)gvrow.FindControl("hdnId");

                LogicLayer.BusinessObjects.DayScheduleMaster dsm = DayScheduleMasterManager.GetById(Convert.ToInt32(hdnId.Value));

                if (dsm != null)
                {
                    dsm.MakeUpDayId = Convert.ToInt32(ddlMakeUpDay.SelectedValue);
                    dsm.WeekNo = Convert.ToInt32(txtWeekNo.Text.Trim());

                    dsm.ModifiedBy = BaseCurrentUserObj.Id;
                    dsm.ModifiedDate = DateTime.Now;

                    bool isUpdate = DayScheduleMasterManager.Update(dsm);
                    if (isUpdate)
                    {
                        count++;

                        #region Log Insert
                        //LogGeneralManager.Insert(
                        //    DateTime.Now,
                        //    BaseAcaCalCurrent.Code,
                        //    BaseAcaCalCurrent.FullCode,
                        //    BaseCurrentUserObj.LogInID,
                        //    "",
                        //    "",
                        //    "Day Schedule Master Save All",
                        //    "",
                        //    "normal",
                        //    ((int)CommonEnum.PageName.DayScheduleMaster).ToString(),
                        //    CommonEnum.PageName.DayScheduleMaster.ToString(),
                        //    _pageUrl,
                        //    "");
                        #endregion
                    }
                }

            }

            ShowMessage("Successfylly Updated : " + count + " Rows ", Color.Green);
        }
        catch (Exception)
        {
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        gvDayScheduleDetails.DataSource = null;
        gvDayScheduleDetails.DataBind();
        ModalPopupExtender1.Hide();

    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(ucProgram.selectedValue);

        if (programId == 0)
        {
            ShowMessage("Please Select Program!", Color.Red);
        }
        else
        {
            int sessionId = Convert.ToInt32(ucSession.selectedValue);

            if (sessionId != 0)
            {
                List<LogicLayer.BusinessObjects.DayScheduleMaster> list = DayScheduleMasterManager.GetAllByProgramSession(programId, sessionId);

                gvDayScheduleMaster.DataSource = list;
                gvDayScheduleMaster.DataBind();

                ShowMessage("", Color.Red);
            }
            else
            {
                ShowMessage("Please Select Session!", Color.Red);
            }

        }

    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }

    protected void chkSelectAllIsActive_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in gvDayScheduleDetails.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("ChkIsActive");
                ckBox.Checked = chk.Checked;
            }

            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnSaveDetail_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrow = (GridViewRow)(((Button)sender)).NamingContainer;

            CheckBox ChkIsActive = (CheckBox)gvrow.FindControl("ChkIsActive");
            HiddenField hdnId = (HiddenField)gvrow.FindControl("hdnId");

            DayScheduleDetail dsd = DayScheduleDetailManager.GetById(Convert.ToInt32(hdnId.Value));

            if (dsd != null)
            {
                dsd.IsActive = ChkIsActive.Checked == true ? true : false;

                dsd.ModifiedBy = BaseCurrentUserObj.Id;
                dsd.ModifiedDate = DateTime.Now;

                bool isUpdate = DayScheduleDetailManager.Update(dsd);
                if (isUpdate)
                {
                    #region Log Insert
                    //LogGeneralManager.Insert(
                    //    DateTime.Now,
                    //    BaseAcaCalCurrent.Code,
                    //    BaseAcaCalCurrent.FullCode,
                    //    BaseCurrentUserObj.LogInID,
                    //    "",
                    //    "",
                    //    "Day Schedule Detail Save",
                    //    "",
                    //    "normal",
                    //    ((int)CommonEnum.PageName.DayScheduleMaster).ToString(),
                    //    CommonEnum.PageName.DayScheduleMaster.ToString(),
                    //    _pageUrl,
                    //    "");
                    #endregion
                    ShowPopUpMessage("Successfylly Updated", Color.Green);
                }
            }

            ModalPopupExtender1.Show();
        }
        catch (Exception)
        {
        }
    }

    protected void btnSaveDetailAll_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            foreach (GridViewRow gvrow in gvDayScheduleDetails.Rows)
            {
                CheckBox ChkIsActive = (CheckBox)gvrow.FindControl("ChkIsActive");
                HiddenField hdnId = (HiddenField)gvrow.FindControl("hdnId");

                DayScheduleDetail dsd = DayScheduleDetailManager.GetById(Convert.ToInt32(hdnId.Value));

                if (dsd != null)
                {
                    dsd.IsActive = ChkIsActive.Checked == true ? true : false;

                    dsd.ModifiedBy = BaseCurrentUserObj.Id;
                    dsd.ModifiedDate = DateTime.Now;

                    bool isUpdate = DayScheduleDetailManager.Update(dsd);
                    if (isUpdate)
                    {
                        #region Log Insert
                        //LogGeneralManager.Insert(
                        //    DateTime.Now,
                        //    BaseAcaCalCurrent.Code,
                        //    BaseAcaCalCurrent.FullCode,
                        //    BaseCurrentUserObj.LogInID,
                        //    "",
                        //    "",
                        //    "Day Schedule Detail Save All",
                        //    "",
                        //    "normal",
                        //    ((int)CommonEnum.PageName.DayScheduleMaster).ToString(),
                        //    CommonEnum.PageName.DayScheduleMaster.ToString(),
                        //    _pageUrl,
                        //    "");
                        #endregion
                        count++;
                    }
                }

            }

            ModalPopupExtender1.Show();
            ShowPopUpMessage("Successfylly Updated : " + count + " Rows ", Color.Green);
        }
        catch (Exception)
        {
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(ucProgram.selectedValue);

        if (programId == 0)
        {
            ShowMessage("Please Select Program!", Color.Red);
        }
        else
        {
            int sessionId = Convert.ToInt32(ucSession.selectedValue);

            if (sessionId != 0)
            {
                string GenerateResult = DayScheduleMasterManager.GenerateDayScheduleMasterByProgramSession(programId, sessionId);
                ShowMessage(GenerateResult, Color.Green);
            }
            else
            {
                ShowMessage("Please Select Session!", Color.Red);
            }

        }
    }

    private void ShowMessage(string Message, Color color)
    {
        lblMsg.Text = Message;
        lblMsg.ForeColor = color;
    }

    private void ShowPopUpMessage(string Message, Color color)
    {
        lblPopUpMessage.Text = Message;
        lblPopUpMessage.ForeColor = color;
    }


}