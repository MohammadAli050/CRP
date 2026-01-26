using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;

public partial class Report_RptStudentClassRoutine : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack)
        {
            FillAcademicCalenderCombo();
        }
    }

    private void FillAcademicCalenderCombo()
    {
        try
        {
            ddlAcaCalBatch.Items.Clear();
            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();

            ddlAcaCalBatch.Items.Add(new ListItem("Select", "0"));
            ddlAcaCalBatch.AppendDataBoundItems = true;

            if (academicCalenderList != null)
            {
                int count = academicCalenderList.Count;
                foreach (AcademicCalender academicCalender in academicCalenderList)
                {
                    ddlAcaCalBatch.Items.Add(new ListItem(academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                    count = academicCalender.AcademicCalenderID;
                }
            }

        }
        catch (Exception ex)
        {
        }
    }
    protected void btnGenerateStudentClassRoutine(object sender, EventArgs e)
    {
        string studentId = txtStudentID.Text.Trim();
        int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
        if (studentId != "")
        {
            LoadData(studentId, acaCalId);
        }
        else
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            lblMessage.Text = "Please Enter Student ID And Trimester";
        }
    }

    private void LoadData(string studentId, int acaCalId)
    {
        List<rStudentClassRoutine> list = new List<rStudentClassRoutine>();
        list = ReportManager.GetStudentClassRoutineByStudentIDAndAcaCalID(studentId, acaCalId);
        lblMessage.Text = "";

        if (list.Count != 0)
        {
            ReportDataSource rds = new ReportDataSource("RptStudentClassRoutine", list);


            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
        }

        else
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            lblMessage.Text = "No Data Found. Please Enter Valid Student ID And Trimester";
        }
    }

   
}