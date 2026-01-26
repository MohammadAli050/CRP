using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;

public partial class Report_RptClassRoutine : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack)
        {
            FillAcademicCalenderCombo();
            //FillProgramCombo();
            FillProgramCheckBoxList();
            chkCoursecode.Checked = true;
            chkTitle.Checked = true;
            chkSection.Checked = true;
            chkDay.Checked = true;
            chkTime.Checked = true;
            chkRoomNo.Checked = true;
            chkFaculty.Checked = true;
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
                    ddlAcaCalBatch.Items.Add(new ListItem("[" + academicCalender.Code + "] " + academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                    count = academicCalender.AcademicCalenderID;
                }
            }

        }
        catch (Exception ex)
        {
        }
    }

    private void FillProgramCheckBoxList()
    {
        try
        {
            cblProgram.Items.Clear();
            List<Program> programList = ProgramManager.GetAll();

            if (programList != null)
            {
                cblProgram.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                cblProgram.DataValueField = "ProgramID";
                cblProgram.DataTextField = "ShortName";
                cblProgram.DataBind();
            }

        }
        catch (Exception ex)
        {
        }
    }
    protected void btnClassRoutine(object sender, EventArgs e)
    {
        List<rClassRoutine> classRoutine = new List<rClassRoutine>();
        int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
        string trimester = ddlAcaCalBatch.SelectedItem.Text;
        int[] programIdList = new int[50];
        string[] programNameList = new string[70];
        string program = "";
        
        for (int c = 0; c < cblProgram.Items.Count; c++) {
            if (cblProgram.Items[c].Selected)
            {
                programNameList[c] = cblProgram.Items[c].Text;
                program = program + programNameList[c] + ", ";
            }
        }
        //string program = cblProgram.SelectedItem.Text;

       
        ReportParameter p1 = new ReportParameter("CourseCode_visible", chkCoursecode.Checked.ToString());
        

        ReportParameter p2 = new ReportParameter("Title_visible", chkTitle.Checked.ToString());
        ReportParameter p3 = new ReportParameter("Section_visible", chkSection.Checked.ToString());
        ReportParameter p4 = new ReportParameter("Credit_visible", chkCredit.Checked.ToString());
        ReportParameter p5 = new ReportParameter("Day_visible", chkDay.Checked.ToString());
        ReportParameter p6 = new ReportParameter("Time_visible", chkTime.Checked.ToString());
        ReportParameter p7 = new ReportParameter("RoomNo_visible", chkRoomNo.Checked.ToString());
        ReportParameter p8 = new ReportParameter("Faculty_visible", chkFaculty.Checked.ToString());
        ReportParameter p9 = new ReportParameter("Program_visible", chkProgram.Checked.ToString());
        ReportParameter p10 = new ReportParameter("SharedPrograms_visible", chkSharedPrograms.Checked.ToString());
        ReportParameter p11 = new ReportParameter("Program", program);
        ReportParameter p12 = new ReportParameter("Trimester", trimester);


        for (int j = 0; j < cblProgram.Items.Count; j++)
        {
            if (cblProgram.Items[j].Selected)
            {
                programIdList[j] = Convert.ToInt32(cblProgram.Items[j].Value);
                int ProgID = programIdList[j];
                List<rClassRoutine> classRoutineList = ReportManager.GetByAcaCalIDAndProgramID(acaCalId, ProgID);
                foreach (rClassRoutine routine in classRoutineList)
                {
                    classRoutine.Add(routine);
                }

            }

        }

        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12 });
        ReportDataSource rds = new ReportDataSource("RptClassRoutine", classRoutine);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(rds);

    }
}