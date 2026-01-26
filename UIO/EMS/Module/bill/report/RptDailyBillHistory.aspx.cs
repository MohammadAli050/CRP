using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
public partial class RptDailyBillHistory : BasePage
{

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack && !IsCallback)
        {

            LoadComboBox();
            DateTime fromDate = DateTime.Now;
            string strFromDate = fromDate.ToString("dd/MM/yyyy");
            txtFromDate.Text = strFromDate;
            DateTime toDate = DateTime.Now;
            string strToDate = toDate.ToString("dd/MM/yyyy");
            txtToDate.Text = strToDate;
        }
    }
    void LoadComboBox()
    {
        FillProgramComboBox();

    }

    void FillProgramComboBox()
    {
        try
        {
            //ddlProgram.Items.Clear();
            List<Program> programList = ProgramManager.GetAll();

            ddlProgram.Items.Add(new ListItem("Select All", "0"));
            ddlProgram.AppendDataBoundItems = true;

            if (programList != null)
            {
                ddlProgram.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                ddlProgram.DataBind();
            }

        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    protected void GetDailyBillHistory_Click(Object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);
        DateTime FromDate = DateTime.ParseExact(txtFromDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);
        DateTime ToDate = DateTime.ParseExact(txtToDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);

        LoadDailyBillHistory(FromDate, ToDate, programId);
    }


    protected void LoadDailyBillHistory(DateTime fromDate, DateTime toDate, int programId)
    {

        try
        {
            string postingFromDate = fromDate.ToString("dd/MM/yyyy");
            string postingToDate = toDate.ToString("dd/MM/yyyy");

            string programName = ddlProgram.SelectedItem.Text.Trim();
            if (programId == 0)
            {
                programName = "All";
            }

            List<rDailyBillHistory> list = DailyCollectionManager.GetDailyBillHistoryByProgramAndDate(fromDate, toDate, programId);
            if (list != null && list.Count > 0)
            {

                ReportParameter p1 = new ReportParameter("PostingFromDate", postingFromDate);
                ReportParameter p2 = new ReportParameter("PostingToDate", postingToDate);
                ReportParameter p3 = new ReportParameter("Program", programName);
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3});
                ReportDataSource rds = new ReportDataSource("DailyBillHistoryDataSet", list);  

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.Visible = true;
            }
            else
            {
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = null;
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.Visible = false;
            }


        }
        catch (Exception ex)
        {
            lblMessage.Text = "No Data Found !";
        }
    }
}
