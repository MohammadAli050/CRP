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
    public partial class AccountAccessDetails : BasePage
    {
        private string _LogInOutGeneralList = "_LogInOutGeneral_List";

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            if (!Page.IsPostBack)
            {
                DateTime dd = DateTime.Now;
                string date = dd.ToString("dd/MM/yyyy");
                DateFromTextBox.Text = date;
                DateToTextBox.Text = date;
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            List<LogLoginLogout> logList = LogLoginLogoutManager.GetByDateRange(DateTime.ParseExact(DateFromTextBox.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(DateToTextBox.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            LoadGrid(logList);

        }

        private void LoadGrid(List<LogLoginLogout> logList)
        {
            gvLogInOutGeneral.DataSource = logList;
            gvLogInOutGeneral.DataBind();
            if (logList != null)
            {
                SessionManager.SaveListToSession<LogLoginLogout>(logList, _LogInOutGeneralList);
            }
            gvLogInOutGeneral.PageIndex = 0;
            return;
        }

        protected void gvLogGeneral_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            List<LogLoginLogout> logList = SessionManager.GetListFromSession<LogLoginLogout>(_LogInOutGeneralList);
            gvLogInOutGeneral.PageIndex = e.NewPageIndex;
            LoadGrid(logList);
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            List<LogLoginLogout> logList = SessionManager.GetListFromSession<LogLoginLogout>(_LogInOutGeneralList);

            if (!string.IsNullOrEmpty(txtFilterLoginID.Text.Trim()))
            {
                logList = logList.Where(c => c.LoginID != null && c.LoginID.ToLower().Contains(txtFilterLoginID.Text.ToLower().Trim())).ToList();
            }
            if (!string.IsNullOrEmpty(txtFilterInOut.Text.Trim()))
            {
                logList = logList.Where(c => c.LogInLogOut != null && c.LogInLogOut.ToLower().Contains(txtFilterInOut.Text.ToLower().Trim())).ToList();
            }
            if (!string.IsNullOrEmpty(txtFilterStatus.Text.Trim()))
            {
                logList = logList.Where(c => c.loginStatus != null && c.loginStatus.ToLower().Contains(txtFilterStatus.Text.ToLower().Trim())).ToList();
            }
            LoadGrid(logList);
        }

        protected void gvLogGeneral_SelectedIndexChanged(object sender, EventArgs e)
        {

        }      
    }
}