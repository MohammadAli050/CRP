using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CalculateResult : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        if (!IsPostBack)
        {
            LoadCamboBox();
        }
    }

    void LoadCamboBox()
    {
        FillAcademicCalenderCombo();
        FillProgramComboBox();
    }

    void FillProgramComboBox()
    {
        try
        {
            ddlProgram.Items.Clear();
            List<Program> programList = ProgramManager.GetAll();

            ddlProgram.Items.Add(new ListItem("Select", "0"));
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

    void FillAcademicCalenderCombo()
    {
        try
        {
            ddlSemester.Items.Clear();
            ddlBatch.Items.Clear();
            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll().OrderByDescending(x => x.AcademicCalenderID).ToList();

            ddlSemester.Items.Add(new ListItem("Select", "0"));
            ddlBatch.Items.Add(new ListItem("Select", "0"));
            ddlSemester.AppendDataBoundItems = true;
            ddlBatch.AppendDataBoundItems = true;

            if (academicCalenderList != null)
            {
                foreach (AcademicCalender academicCalender in academicCalenderList)
                {
                    ddlSemester.Items.Add(new ListItem("[" + academicCalender.Code + "] " + academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                    ddlBatch.Items.Add(new ListItem("[" + academicCalender.Code + "] " + academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                }
            }

        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    #endregion

    #region Event

    protected void btnExecute_Click(object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ddlSemester.SelectedValue);
            int batchId = Convert.ToInt32(ddlBatch.SelectedValue);
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);

            string roll = txtStudentId.Text;
            if (roll != "")
            {
            }
        }
        catch { }
        finally { }
    }

    #endregion
}