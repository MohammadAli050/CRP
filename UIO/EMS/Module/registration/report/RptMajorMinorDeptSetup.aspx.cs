using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;


public partial class Report_RptMajorMinorDeptSetup : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

       // lblCount.Text = "0";
        //ShowMessage("");
       

        if (!IsPostBack)
        {
            LoadDropDown();


            List<rStudentMajorDefine> studentList = new List<rStudentMajorDefine>();
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("StudentMajor", studentList);
            ReportViewer1.LocalReport.DataSources.Add(rds);
        }        
      }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(ddlProgram.SelectedItem.Value);
        int batchId = Convert.ToInt32(ddlAcaCalBatch.SelectedItem.Value);
        string roll = txtRoll.Text.Trim();

        if (programId == 0 && batchId == 0 & string.IsNullOrEmpty(roll))
        {
            lblMessage.Text = "Select at lest one option among Program, batch or Student Id";
            return;
        }

        List<rStudentMajorDefine> studentList = StudentManager.GetAllStudentByMajorDefine(programId, batchId, roll);
        studentList = studentList.OrderBy(s => s.Roll).ToList();

        if (chkMajor1.Checked)
        {
            studentList = studentList.Where(s => !String.IsNullOrEmpty(s.Major1Name)).ToList(); 
        }

        if (chkMajor2.Checked)
        {
            studentList = studentList.Where(s => !String.IsNullOrEmpty(s.Major2Name)).ToList();
        }

        ReportViewer1.LocalReport.DataSources.Clear();
        ReportDataSource rds = new ReportDataSource("StudentMajor", studentList);
        ReportViewer1.LocalReport.DataSources.Add(rds);
        ReportViewer1.LocalReport.Refresh();

        lblCount.Text = studentList.Count().ToString();

    }    

    #region Method

    private void LoadDropDown()
    {
        //follow the order of combo loding
        FillAcademicCalenderCombo();
        FillProgramCombo();
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
                ddlAcaCalBatch.DataSource = academicCalenderList.OrderByDescending(d => d.Code).ToList();
                ddlAcaCalBatch.DataBind();
                //int count = academicCalenderList.Count;
                //foreach (AcademicCalender academicCalender in academicCalenderList)
                //{
                //    ddlAcaCalBatch.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));

                //}

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

    #endregion

    
   
}