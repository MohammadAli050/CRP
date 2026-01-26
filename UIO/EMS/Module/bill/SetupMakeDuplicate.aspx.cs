using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SetupMakeDuplicate : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        try
        {
             
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                FillDropdown();
            }
        }
        catch { }
    }

    protected void FillDropdown()
    {
        FillBatchAcaCalCombo();
        FillProgramCombo();
    }

    private void FillBatchAcaCalCombo()
    {
        try
        {
            ddlBatchAcaCalFrom.Items.Clear();
            ddlBatchAcaCalTo.Items.Clear();
            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
            if (academicCalenderList.Count > 0)
            {
                academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();

                ddlBatchAcaCalFrom.Items.Add(new ListItem("-Select-", "0"));
                ddlBatchAcaCalTo.Items.Add(new ListItem("-Select-", "0"));
                ddlBatchAcaCalFrom.AppendDataBoundItems = true;
                ddlBatchAcaCalTo.AppendDataBoundItems = true;

                foreach (AcademicCalender academicCalender in academicCalenderList)
                {
                    ddlBatchAcaCalFrom.Items.Add(new ListItem("[" + academicCalender.Code + "] " + academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                    ddlBatchAcaCalTo.Items.Add(new ListItem("[" + academicCalender.Code + "] " + academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                }

                AcademicCalender acaCal = academicCalenderList.Where(x => x.IsCurrent == true).SingleOrDefault();
                if (acaCal != null)
                    ddlBatchAcaCalFrom.SelectedValue = acaCal.AcademicCalenderID.ToString();
            }
            else
            {
                lblMsg.Text = "Error: 101(Academic Calender not load)";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error: 1021";
        }
        finally { }
    }

    private void FillProgramCombo()
    {
        try
        {
            ddlProgramFrom.Items.Clear();
            ddlProgramTo.Items.Clear();
            List<Program> programList = ProgramManager.GetAll();

            ddlProgramFrom.Items.Add(new ListItem("-All Program-", "0"));
            ddlProgramTo.Items.Add(new ListItem("-All Program-", "0"));
            ddlProgramFrom.AppendDataBoundItems = true;
            ddlProgramTo.AppendDataBoundItems = true;

            if (programList != null)
            {
                ddlProgramFrom.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                ddlProgramFrom.DataTextField = "ShortName";
                ddlProgramFrom.DataValueField = "ProgramId";
                ddlProgramFrom.DataBind();

                ddlProgramTo.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                ddlProgramTo.DataTextField = "ShortName";
                ddlProgramTo.DataValueField = "ProgramId";
                ddlProgramTo.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    #endregion

    #region Event

    protected void chkDisCountSetup_Change(object sender, EventArgs e)
    {
        try
        {

        }
        catch { }
    }
    #endregion
}