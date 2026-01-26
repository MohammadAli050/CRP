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
    public partial class LogGeneralPage : BasePage
    {
        private string _LogGeneralList = "_LogGeneral_List";

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
            List<LogGeneral> logList = LogGeneralManager.GetByDateRange(DateTime.ParseExact(DateFromTextBox.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(DateToTextBox.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            LoadGrid(logList);
        }

        private void LoadGrid(List<LogGeneral> logList)
        {
            gvLogGeneral.DataSource = logList;
            gvLogGeneral.DataBind();
            if (logList != null)
            {
                SessionManager.SaveListToSession<LogGeneral>(logList, _LogGeneralList);
            }
            gvLogGeneral.PageIndex = 0;
            return;
        }

        protected void gvLogGeneral_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            List<LogGeneral> logList = SessionManager.GetListFromSession<LogGeneral>(_LogGeneralList);
            gvLogGeneral.PageIndex = e.NewPageIndex;
            LoadGrid(logList);
        }


        protected void btnFilter_Click(object sender, EventArgs e)
        {
            List<LogGeneral> logList = SessionManager.GetListFromSession<LogGeneral>(_LogGeneralList);

            if (!string.IsNullOrEmpty(txtFilterRoll.Text.Trim()))
            {
                logList = logList.Where(c => c.StudentRoll != null && c.StudentRoll.ToLower().Contains(txtFilterRoll.Text.ToLower().Trim())).ToList();
            }
            if (!string.IsNullOrEmpty(txtFilterEvent.Text.Trim()))
            {
                logList = logList.Where(c => c.EventName != null && c.EventName.ToLower().Contains(txtFilterEvent.Text.ToLower().Trim())).ToList();
            }
            if (!string.IsNullOrEmpty(txtFilterUser.Text.Trim()))
            {
                logList = logList.Where(c => c.UserLoginId != null && c.UserLoginId.ToLower().Contains(txtFilterUser.Text.ToLower().Trim())).ToList();
            }
            if (!string.IsNullOrEmpty(txtFilteCourse.Text.Trim()))
            {
                logList = logList.Where(c => c.CourseFormalCode != null && c.CourseFormalCode.ToLower().Contains(txtFilteCourse.Text.ToLower().Trim())).ToList();
            }
            if (!string.IsNullOrEmpty(txtFilterMsg.Text.Trim()))
            {
                logList = logList.Where(c => c.Message != null && c.Message.ToLower().Contains(txtFilterMsg.Text.ToLower().Trim())).ToList();
            }
            LoadGrid(logList);
        }

        protected void gvLogGeneral_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
    }
}