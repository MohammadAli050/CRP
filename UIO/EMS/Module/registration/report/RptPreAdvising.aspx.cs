using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Common;
using Microsoft.Reporting.WebForms;

public partial class Report_RptPreAdvising : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack && !IsCallback)
        {
            FillProgramListCombo();
            FillBatchListCombo();
        }
    }

    private void FillProgramListCombo()
    {
        try
        {
            programListCombo.Items.Clear();

            List<Program> programList = ProgramManager.GetAll();

            programListCombo.AppendDataBoundItems = true;
            programListCombo.Items.Add(new ListItem("Select", "0"));
            if (programList != null)
            {
                programListCombo.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                programListCombo.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void FillBatchListCombo()
    {
        try
        {
            batchListCombo.Items.Clear();
            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();

            //ddlAcaCalBatch.Items.Add(new ListItem("Select", "0"));
            batchListCombo.AppendDataBoundItems = true;

            if (academicCalenderList != null)
            {
                int count = academicCalenderList.Count;
                batchListCombo.Items.Add(new ListItem("Select", "0"));
                foreach (AcademicCalender academicCalender in academicCalenderList)
                {
                    batchListCombo.Items.Add(new ListItem(academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                    count = academicCalender.AcademicCalenderID;
                }
                //batchListCombo.SelectedValue = count.ToString();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void loadButton_Click(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(programListCombo.SelectedValue);
        int batchId = Convert.ToInt32(batchListCombo.SelectedValue);
        string batchName="";
        string programName="";
        List<RegistrationWorksheet> regWork = RegistrationWorksheetManager.GetPreAdByProgCal(Convert.ToInt32(programListCombo.SelectedValue), Convert.ToInt32(batchListCombo.SelectedValue));
        ReportParameter p1 = new ReportParameter("Count", regWork.Count.ToString());
        batchName = batchListCombo.SelectedItem.Text;
        programName = programListCombo.SelectedItem.Text;
        if (programListCombo.SelectedValue.Equals("0"))
            programName = "All Program";
        if (batchListCombo.SelectedValue.Equals("0"))
            batchName = "All Batch";

        ReportParameter p2 = new ReportParameter("ProgramName",programName);
        ReportParameter p3 = new ReportParameter("BatchName", batchName);
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1,p2,p3 });
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportDataSource rds = new ReportDataSource("PreAdvising", regWork);
        ReportViewer1.LocalReport.DataSources.Add(rds);
        ReportViewer1.LocalReport.Refresh();
    
    }
}