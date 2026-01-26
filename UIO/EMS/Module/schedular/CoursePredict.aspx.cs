using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CoursePredict : BasePage
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

    protected void btnGenerate_Click(Object sender, EventArgs e)
    {
        try
        {
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);

            bool resultStatus = CoursePredictMasterManager.InsertByAcaCalProgram(acaCalId, programId);
            if (resultStatus)
                lblMsg.Text = "Successfully Generate Course for " + ddlProgram.SelectedItem.Text + " in " + ddlAcademicCalender.SelectedItem.Text;
            else
                lblMsg.Text = "Fail";
        }
        catch { }
    }

    #endregion
}