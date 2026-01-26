using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;

public partial class Report_RptStudentResultHistoryList : BasePage
{
    BussinessObject.UIUMSUser userObj = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        if (!IsPostBack)
        {

        }
    }

    protected void btnStudentCGPAList(object sender, EventArgs e)
    {
        string studentId = txtStudentID.Text.Trim();
        if (studentId != "")
        {
            if (AccessAuthentication(userObj, studentId.Trim()))
            {
                LoadData(studentId);
            }
            else
            {
                lblMessage.Text = "Access Permission Denied.";
            }
        }
        else
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            lblMessage.Text = "Please Enter a Student ID";
        }
    }

    private void LoadData(string studentId)
    {

        List<rStudentResultHistory> list = new List<rStudentResultHistory>();
        list = ReportManager.GetStudentResultHistoryListByStudentID(studentId);
        lblMessage.Text = "";

        if (list.Count != 0)
        {
            ReportDataSource rds = new ReportDataSource("RptStudentResultHistoryList", list);


            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
        }

        else
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            lblMessage.Text = "No Data Found. Please Enter a Valid Student ID";
        }
    }
}