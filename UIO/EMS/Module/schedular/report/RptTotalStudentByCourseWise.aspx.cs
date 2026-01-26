using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_RptTotalStudentByCourseWise : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack)
        {
            LoadComboBox();
        }
    }

    protected void LoadComboBox()
    {
        ddlAcademicCalender.Items.Clear();
        ddlAcademicCalender.Items.Add(new ListItem("Select", "0"));

        LoadProgramCombo();
        //LoadAcademicCalender();
        //LoadExamScheduleSet();
    }

    protected void LoadProgramCombo()
    {
        try
        {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Add(new ListItem("Select", "0"));
            ddlProgram.AppendDataBoundItems = true;

            List<Program> calenderUnitMasterList = ProgramManager.GetAll();

            if (calenderUnitMasterList.Count > 0 && calenderUnitMasterList != null)
            {
                ddlProgram.DataValueField = "ProgramID";
                ddlProgram.DataTextField = "ShortName";
                ddlProgram.DataSource = calenderUnitMasterList;
                ddlProgram.DataBind();
            }
        }
        catch { }
        finally { }
    }

    protected void LoadAcademicCalender(int calenderTypeId)
    {
        try
        {
            ddlAcademicCalender.Items.Clear();
            ddlAcademicCalender.Items.Add(new ListItem("Select", "0"));
            ddlAcademicCalender.AppendDataBoundItems = true;

            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll(calenderTypeId);

            if (academicCalenderList.Count > 0 && academicCalenderList != null)
            {
                foreach (AcademicCalender academicCalender in academicCalenderList)
                    ddlAcademicCalender.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));

                academicCalenderList = academicCalenderList.Where(x => x.IsActiveRegistration == true).ToList();
                ddlAcademicCalender.SelectedValue = academicCalenderList[0].AcademicCalenderID.ToString();
            }
        }
        catch { }
    }

    #endregion

    #region Event

    protected void ddlProgram_Changed(Object sender, EventArgs e)
    {
        try
        {
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);
            Program program = ProgramManager.GetById(programId);
            if (program != null)
                LoadAcademicCalender(program.CalenderUnitMasterID);
        }
        catch { }
    }

    protected void btnLoad_Click(Object sender, EventArgs e)
    {
        try
        {
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);

            List<CoursePredictDetails> coursePredictDetailsList = CoursePredictDetailsManager.GetAll(acaCalId, programId);
            if (coursePredictDetailsList.Count > 0 && coursePredictDetailsList != null)
            {
                List<Program> programList = ProgramManager.GetAll();
                Hashtable hashProgram = null;
                if(programList.Count > 0 && programList != null)
                {
                    hashProgram = new Hashtable();
                    foreach(Program program in programList)
                        hashProgram.Add(program.ProgramID, program.ShortName);
                }

                List<CoursePredictMaster> coursePredictMasterList = CoursePredictMasterManager.GetAll(acaCalId, programId);
                Hashtable hashCoursePredictMaster = null;
                if (coursePredictMasterList.Count > 0 && coursePredictMasterList != null)
                {
                    hashCoursePredictMaster = new Hashtable();
                    foreach (CoursePredictMaster coursePredictMaster in coursePredictMasterList)
                    {
                        string programBatch = hashProgram[coursePredictMaster.ProgramId] == null ? coursePredictMaster.BatchNo.ToString() : "[" + hashProgram[coursePredictMaster.ProgramId].ToString() + "-" +  coursePredictMaster.BatchNo.ToString() + "] ";
                        hashCoursePredictMaster.Add(coursePredictMaster.Id, programBatch);
                    }
                        //hashCoursePredictMaster.Add();
                }

                Hashtable hashCourse = null;
                List<Course> courseList = CourseManager.GetAll();
                if (courseList.Count > 0 && courseList != null)
                {
                    hashCourse = new Hashtable();
                    foreach (Course course in courseList)
                        hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.FormalCode + ":" + course.Title);
                }

                foreach (CoursePredictDetails coursePredictDetails in coursePredictDetailsList)
                {
                    string programBatch = hashCoursePredictMaster[coursePredictDetails.CoursePredictMasterId] == null ? "" : hashCoursePredictMaster[coursePredictDetails.CoursePredictMasterId].ToString();
                    string courseIndex = coursePredictDetails.CourseId.ToString() + "_" + coursePredictDetails.VersionId.ToString();
                    coursePredictDetails.CourseName = hashCourse[courseIndex] == null ? programBatch + coursePredictDetails.NodeLinkName : programBatch + hashCourse[courseIndex].ToString();
                }
            }

            ReportDataSource rds = new ReportDataSource("TotalStudentByCourseWise", coursePredictDetailsList);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
        }
        catch { }
    }

    #endregion
}