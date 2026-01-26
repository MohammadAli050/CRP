using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using System.Drawing;

public partial class RptDayScheduleMaster : BasePage
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
                List<rDayScheduleDetails> list = DayScheduleDetailManager.GetDayScheduleDetailReportByProgramIdSessionId(programId, sessionId);

                if (list != null && list.Count != 0)
                {
                    string program = ucProgram.selectedText;
                    string session = ucSession.selectedText;

                    ReportParameter p1 = new ReportParameter("Program", program);
                    ReportParameter p2 = new ReportParameter("Session", session);


                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/miu/schedular/report/RptDayScheduleMaster.rdlc");
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
                    ReportDataSource rds = new ReportDataSource("DayScheduleDetailsDataSet", list);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ShowMessage("", Color.Red);

                }
                else
                {
                    ShowMessage("NO Data Found. Enter A Valid Program And Session.", Color.Red);
                    return;
                }
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

    private void ShowMessage(string Message, Color color)
    {
        lblMsg.Text = Message;
        lblMsg.ForeColor = color;
    }


}