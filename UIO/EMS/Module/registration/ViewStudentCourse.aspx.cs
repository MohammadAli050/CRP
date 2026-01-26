using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessLogic;
using CommonUtility;

public partial class ViewStudentCourse : BasePage
{
    List<Student> _studentListWithCGPA = null;

    #region Event
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
        _scriptMan.AsyncPostBackTimeout = 36000;

        if (!IsPostBack)
        {
            lblCount.Text = "0";
            LoadDropDown();
        }

        //if (!string.IsNullOrEmpty(SessionManager.GetObjFromSession<string>(SessionName.Common_Session_Student_Roll)))
        //{
        //    if (string.IsNullOrEmpty(txtStudent.Text))
        //        txtStudent.Text = SessionManager.GetObjFromSession<string>(SessionName.Common_Session_Student_Roll);
        //}
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            lblCount.Text = "0";
            _studentListWithCGPA = new List<Student>();
            List<Student> studentList = null;
            gvStudentList.PageIndex = 0;

            int programId = Convert.ToInt32(ddlProgram.SelectedItem.Value);
            int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedItem.Value);

            if (!string.IsNullOrEmpty(txtStudent.Text.Trim()))
            {
                studentList = StudentManager.GetFromRegWorksheetByStudentRoll(txtStudent.Text.Trim());
            }
            else
            {
                studentList = StudentManager.GetAllFromRegWorksheetByProgramAndBatch(programId, acaCalId);
            }

            if (studentList != null)
            {
                gvStudentList.DataSource = studentList;
                gvStudentList.DataBind();
                lblCount.Text = studentList.Count().ToString();
            }
            else
            {
                gvStudentList.DataSource = null;
                gvStudentList.DataBind();
                lblCount.Text = "0";
            }
        }
        catch (Exception ex)
        {
        }
    }


    #endregion

    #region Method


    private void LoadDropDown()
    {
        //follow the order of combo loding
        FillAcademicCalenderCombo();
        FillProgramCombo();
        //FillLinkCalender();
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

    #endregion
}