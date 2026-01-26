using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System.Globalization;

namespace EMS.miu.log
{
    public partial class LogSMSPage : BasePage
    {
        private string _LogSMSList = "_LogSMSList";

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            if (!Page.IsPostBack)
            {
                LoadCreditBalance();
                DateTime dd = DateTime.Now;
                string date = dd.ToString("dd/MM/yyyy");
                DateFromTextBox.Text = date;
                DateToTextBox.Text = date;
            }
        }

        private void LoadCreditBalance()
        {
           // SMSManager.GetBalance(ResultCallBack);

            SMSBasicSetup smsSetup = SMSBasicSetupManager.Get();
            lblCredits.Text = smsSetup.RemainingSMS + " SMS Remaining";
        }

        void ResultCallBack(string balance)
        {
            lblCredits.Text = balance +" Tk.";
        }
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            List<LogSMS> logList = LogSMSManager.GetByDateRange(DateTime.ParseExact(DateFromTextBox.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),DateTime.ParseExact(DateToTextBox.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));

            if (logList != null)
            {
                SessionManager.SaveListToSession<LogSMS>(logList, _LogSMSList);
            }

            LoadGrid(logList);
        }

        private void LoadGrid(List<LogSMS> logList)
        {
            gvLogGeneral.DataSource = logList;
            gvLogGeneral.DataBind();
            return;
        }

        protected void gvLogGeneral_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            List<LogSMS> logList = SessionManager.GetListFromSession<LogSMS>(_LogSMSList);
            gvLogGeneral.PageIndex = e.NewPageIndex;
            LoadGrid(logList);
        }


        protected void btnFilter_Click(object sender, EventArgs e)
        {
            List<LogSMS> logList = SessionManager.GetListFromSession<LogSMS>(_LogSMSList);

             if (!string.IsNullOrEmpty(txtFilterSender.Text.Trim()))
             {
                 logList = logList.Where(c => c.Sender.ToLower().Contains(txtFilterSender.Text.ToLower().Trim())).ToList();
             }
             if (!string.IsNullOrEmpty(txtFilterRecipient.Text.Trim()))
             {
                 logList = logList.Where(c => c.Receipient.ToLower().Contains(txtFilterRecipient.Text.ToLower().Trim())).ToList();
             }
             if (!string.IsNullOrEmpty(txtFilterMsg.Text.Trim()))
             {
                 logList = logList.Where(c => c.Message.ToLower().Contains(txtFilterMsg.Text.ToLower().Trim())).ToList();
             }
             if (!string.IsNullOrEmpty(txtFilterStatus.Text.Trim()))
             {
                 logList = logList.Where(c => c.Status==Convert.ToBoolean(txtFilterStatus.Text)).ToList();
             }

            LoadGrid(logList);

            gvLogGeneral.PageIndex=0;
        }

        protected void gvLogGeneral_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }       
    }
   
}