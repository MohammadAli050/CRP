using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using CommonUtility;
using Microsoft.Reporting.WebForms;

public partial class Report_RptStudentActive : BasePage  
{
    protected void Page_Load(object sender, EventArgs e)
    {
         base.CheckPage_Load();

        pnlMessage.Visible = false;
        lblCount.Text = "0";

        if (!IsPostBack)
        {
            LoadDropDown();
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            int programId = Convert.ToInt32(ddlProgram.SelectedItem.Value);
            int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedItem.Value);
            string roll = "";

            if (programId == 0)
            {
                ShowMessage("Please select Program.");
                return;
            }
            else if (acaCalId == 0)
            {
                ShowMessage("Please select Academic Calender.");
                return;
            }
            else
            {
                LoadStudent(programId, acaCalId, roll);
            }
        }
        catch (Exception)
        {
        }
    }

    private void LoadStudent(int programId, int acaCalId, string roll)
    {
        List<Student> studentList = StudentManager.GetAllByProgramOrBatchOrRoll(programId, acaCalId, roll);

        if (studentList != null)
            studentList = studentList.OrderBy(s => s.Roll).ToList();
        
        ReportDataSource rds = new ReportDataSource("StudentActive", studentList);

        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(rds);

        lblCount.Text = studentList.Count().ToString();
    }
    
    private void LoadDropDown()
    {
        //follow the order of combo loding
        FillAcademicCalenderCombo();
        FillProgramCombo();
        int programId = Convert.ToInt32(ddlProgram.SelectedItem.Value);
    }

    private void FillAcademicCalenderCombo()
    {
        try
        {
            ddlAcaCalBatch.Items.Clear();
            ddlAcaCalBatch.Items.Add(new ListItem("Select", "0"));
            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
            academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();
            
            ddlAcaCalBatch.AppendDataBoundItems = true;

            if (academicCalenderList != null)
            {
                int count = academicCalenderList.Count;
                foreach (AcademicCalender academicCalender in academicCalenderList)
                {
                    ddlAcaCalBatch.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
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

    private void ShowMessage(string msg)
    {
        pnlMessage.Visible = true;

        lblMessage.Text = msg;
        lblMessage.ForeColor = Color.Red;
    }  
}