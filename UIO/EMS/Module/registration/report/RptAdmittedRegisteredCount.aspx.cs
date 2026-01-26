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

public partial class RptAdmittedRegisteredCount : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        pnlMessage.Visible = false;

        if (!IsPostBack && !IsCallback)
        {
            FillYearDropDown();
        }
    }

    private void ShowMessage(string msg)
    {
        pnlMessage.Visible = true;

        lblMessage.Text = msg;
        lblMessage.ForeColor = Color.Red;

    }

    private void FillYearDropDown()
    {
        List<rYear> yearList = AcademicCalenderManager.GetAllYear();
        ddlYearFrom.DataSource = yearList;
        ddlYearFrom.DataTextField = "Year";
        ddlYearFrom.DataValueField = "Year";
        ddlYearFrom.DataBind();

        ddlYearTo.DataSource = yearList;
        ddlYearTo.DataTextField = "Year";
        ddlYearTo.DataValueField = "Year";
        ddlYearTo.DataBind();
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int YearFrom = Convert.ToInt32(ddlYearFrom.SelectedValue);
            int YearTo = Convert.ToInt32(ddlYearTo.SelectedValue);

            if (programId == 0)
            {
                ShowMessage("Please Select a Progeam.");
                ReportViewer1.Visible = false;
            }
            else
            {
                List<rAdmittedRegisteredCount> list = AdmissionRegistrationCountManager.GetAdmittedRegisteredCountByProgramYearWise(programId, YearFrom, YearTo);

                ReportParameter p1 = new ReportParameter("Program", ucProgram.selectedText);
                ReportParameter p2 = new ReportParameter("Year", YearFrom + "-"+ YearTo);

                if (list != null && list.Count != 0)
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/miu/registration/report/RptAdmittedRegisteredCount.rdlc");
                    ReportDataSource rds = new ReportDataSource("AdmittedRegisteredCountDataSet", list);
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
                    
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.Visible = true;
                    lblMessage.Text = "";
                }
                else
                {
                    ShowMessage("No Data Found!");
                    ReportViewer1.Visible = false;
                }
            }


        }
        catch (Exception)
        {
            ShowMessage("Error : Contact With Admin");
            ReportViewer1.Visible = false;
        }
    }

}
