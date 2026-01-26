using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_StudentDiscounts : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        try
        {
            
            if (!IsPostBack)
            {
                LoadDropDown();

                List<StudentDiscountAndScholarshipPerSession> list = new List<StudentDiscountAndScholarshipPerSession>();
                ReportDataSource rds = new ReportDataSource("DataSet1", list);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void LoadDropDown()
    {
        FillAcademicCalenderCombo();
        FillBatchCombo();
        FillProgramCombo();
    }
   
    protected void btnViewBill_Click(object sender, EventArgs e)
    {
        LoadData();
    }

    private void LoadData()
    {

        //int session = Convert.ToInt32(ddlAcaCalSession.SelectedValue);
        //int program = Convert.ToInt32(ddlProgram.SelectedValue);

        //List<StudentDiscountAndScholarshipPerSession> list = StudentDiscountAndScholarshipPerSessionManager.GetAllByAcaCalIDProgramID(sessionId, program);

        List<StudentDiscountAndScholarshipPerSession> list = StudentDiscountAndScholarshipPerSessionManager.GetAll();

        int session = Convert.ToInt32(ddlAcaCalSession.SelectedValue);
        int program = Convert.ToInt32(ddlProgram.SelectedValue);

        if (session != 0)
        {
            list = list.Where(l =>l.AcaCalSession == session).ToList();
        }
        if (program != 0)
        {
            list = list.Where(l => l.Student.ProgramID == program).ToList();
        }

        ReportDataSource rds = new ReportDataSource("DataSet1", list);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(rds);
        ReportViewer1.LocalReport.Refresh();
    }

    private void FillAcademicCalenderCombo()
    {
        try
        {
            ddlAcaCalSession.Items.Clear();
            ddlAcaCalSession.Items.Add(new ListItem("Select", "0"));

            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
            academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();

            ddlAcaCalSession.AppendDataBoundItems = true;

            if (academicCalenderList != null)
            {
                int count = academicCalenderList.Count;
                foreach (AcademicCalender academicCalender in academicCalenderList)
                {
                    ddlAcaCalSession.Items.Add(new ListItem("[" + academicCalender.Code + "] " + UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                }
            }
        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    private void FillBatchCombo()
    {
        try
        {
            ddlBatch.Items.Clear();
            ddlBatch.Items.Add(new ListItem("Select", "0"));

            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
            academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();

            ddlBatch.AppendDataBoundItems = true;

            if (academicCalenderList != null)
            {
                int count = academicCalenderList.Count;
                foreach (AcademicCalender academicCalender in academicCalenderList)
                {
                    ddlBatch.Items.Add(new ListItem("[" + academicCalender.Code + "] " + UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                }
            }
        }
        catch (Exception ex)
        {
        }
        finally { }
    }
    
    private void FillProgramCombo()
    {
        try
        {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Add(new ListItem("Select", "0"));
            List<Program> programList = ProgramManager.GetAll();

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
}