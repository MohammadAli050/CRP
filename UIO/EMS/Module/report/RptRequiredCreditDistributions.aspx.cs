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
using System.Drawing;
public partial class RptRequiredCreditDistributions : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        pnlMessage.Visible = false;

        if (!IsPostBack)
        {
            GetAttendance_Click(null, null);
        }
    }

    protected void GetAttendance_Click(Object sender, EventArgs e)
    {
        try
        {
            List<rCreditDistribution> list = ReportManager.GetAllCreditDstributionList();
            
            if (list!=null && list.Count != 0)
            {

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/miu/report/RptRequiredCreditDistributions.rdlc");
                ReportDataSource rds = new ReportDataSource("RequiredCreditDistributionsDataSet", list);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                lblMessage.Text = "";
            }
            else
            {
                ReportViewer1.LocalReport.DataSources.Clear();
                ShowMessage("No Data Found. Please Enter Valid Session, Program And Course");
                return;
            }

        }
        catch (Exception ex)
        {

            lblMessage.Text = "Error Code: 01101";
        }

    }

    private void ShowMessage(string msg)
    {
        pnlMessage.Visible = true;

        lblMessage.Text = msg;
        lblMessage.ForeColor = Color.Red;

    }

}
