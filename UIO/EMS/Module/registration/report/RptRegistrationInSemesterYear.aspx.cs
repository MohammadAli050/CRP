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

public partial class RptRegistrationInSemesterYear : BasePage
{


    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack && !IsCallback)
        {

            LoadYear();
        }
    }
    private void LoadYear()
    {
        List<rYear> yearList = AcademicCalenderManager.GetAllYear();
        ddlYear.DataSource = yearList;
        ddlYear.DataTextField = "Year";
        ddlYear.DataValueField = "Year";
        ddlYear.DataBind();
    }
    protected void GetRegistrationInSemesterYear_Click(object sender, EventArgs e)
    {
        try
        {
            string yearName = Convert.ToString(ddlSession.SelectedItem);
            int year = Convert.ToInt32(ddlYear.SelectedValue);

            int semesterAcaCalId = 0;
            int trimesterAcaCalId = 0;

            if (yearName == "Spring")
            {
                trimesterAcaCalId = 1;

                semesterAcaCalId = 4;
            }
            else if (yearName == "Summer")
            {

                trimesterAcaCalId = 2;

                semesterAcaCalId = 4;
            }
            else if (yearName == "Fall")
            {

                trimesterAcaCalId = 3;

                semesterAcaCalId = 5;
            }
            else { }


            string semester = yearName+ " " + year;

            LoadDailyCollection(semester, year, trimesterAcaCalId, semesterAcaCalId);

        }
        catch (Exception ex) { }
    }


    protected void LoadDailyCollection(string semester,int year, int trimesterAcaCalId, int semesterAcaCalId)
    {
        
        try
        {

           // string AcaCalId = ddlProgram.SelectedItem.Text.Trim();
            List<rRegistrationInSemesterYear> list = RegistrationInSemisterYearManager.GetAllBySemesterYear(year, trimesterAcaCalId, semesterAcaCalId);
            if (list != null && list.Count > 0)
            {

                ReportParameter p1 = new ReportParameter("Semester", semester);
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1});

                ReportDataSource rds = new ReportDataSource("RegistrationInSemesterYearDataSet", list);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);

            }
            else
            {
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = null;
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }


        }
        catch (Exception ex)
        {
        }
    }
}
