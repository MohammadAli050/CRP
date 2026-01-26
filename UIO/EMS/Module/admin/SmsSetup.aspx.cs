using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;

namespace EMS.miu.admin
{
    public partial class SmsSetup : BasePage
    {
        BussinessObject.UIUMSUser BaseCurrentUserObj = null;
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

        protected void Page_Load(object sender, EventArgs e)
        {
            BaseCurrentUserObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            base.CheckPage_Load();
            if(!IsPostBack)
                 LoadData();
        }

        private void LoadData()
        {
            SMSBasicSetup smsSetupObj = SMSBasicSetupManager.Get();
            List<SMSBasicSetup> list = new List<SMSBasicSetup>();
            list.Add(smsSetupObj);

            gvSetup.DataSource = list;
            gvSetup.DataBind();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow row = gvSetup.Rows[0];
            CheckBox chkRegStat = (CheckBox)row.FindControl("chkRegStat");
            CheckBox chkResultCorrectionStatus = (CheckBox)row.FindControl("chkResultCorrectionStatus");
            CheckBox chkBillCollection = (CheckBox)row.FindControl("chkBillCollection");
            CheckBox chkLateFine = (CheckBox)row.FindControl("chkLateFine");
            CheckBox chkWaiverPosting = (CheckBox)row.FindControl("chkWaiverPosting");
            CheckBox chkAdmitCardStatus = (CheckBox)row.FindControl("chkAdmitCardStatus");
            CheckBox chkResultPubStatus = (CheckBox)row.FindControl("chkResultPubStatus");
            CheckBox chkCustomSmsStatus = (CheckBox)row.FindControl("chkCustomSmsStatus");

            SMSBasicSetup smsSetupObj = SMSBasicSetupManager.Get();
            smsSetupObj.RegistrationStatus = chkRegStat.Checked;
            smsSetupObj.LateFineStatus = chkLateFine.Checked;
            smsSetupObj.ResultCorrectionStatus = chkResultCorrectionStatus.Checked;
            smsSetupObj.ResultPubStatus = chkResultPubStatus.Checked;
            smsSetupObj.WaiverPostingStatus = chkWaiverPosting.Checked;
            smsSetupObj.AdmitCardStatus = chkAdmitCardStatus.Checked;
            smsSetupObj.BillCollectionStatus = chkBillCollection.Checked;
            smsSetupObj.CustomSmsStatus = chkCustomSmsStatus.Checked;

            bool isUpdated = SMSBasicSetupManager.Update(smsSetupObj);
            if (isUpdated)
            {
                ShowAlertMessage("Updated");
                LogGeneralManager.Insert(DateTime.Now, "", "", BaseCurrentUserObj.LogInID, "", "", "SMS Setup Update", BaseCurrentUserObj.LogInID + " updated SMS Setup", "", ((int)CommonUtility.CommonEnum.PageName.SMSSetup).ToString(), CommonUtility.CommonEnum.PageName.SMSSetup.ToString(), _pageUrl, "");
            }

        }

        private void ShowAlertMessage(string msg)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ServerControlScript", "alert('" + msg + "');", true);
        }
    }
}