using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;

public partial class Report_RptGradeWiseRetakeDiscount : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        try
        {
            
            if (!IsPostBack)
            {
                LoadDropDown();
                ReportViewer1.LocalReport.Refresh();

            }

        }
        catch (Exception ex)
        {

        }
    }

    private void LoadDropDown()
    {
        FillAcademicCalenderCombo();
        FillProgramCombo();
    }

    protected void btnViewBill_Click(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(ddlProgram.SelectedItem.Value);
        int acaCalId = Convert.ToInt32(ddlAcaCalSession.SelectedItem.Value);
        LoadData(acaCalId, programId);
    }

    private void LoadData(int acaCalID, int programId)
    {
        List<GradeWiseRetakeDiscount> gradeWiseRetakeDiscountList = GradeWiseRetakeDiscountManager.GetAllBy(programId, acaCalID);

        ReportViewer1.LocalReport.DataSources.Clear();
        ReportDataSource rds = new ReportDataSource("GradeRetake", gradeWiseRetakeDiscountList);
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