using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CalculateResultProcessByBatch : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack)
        {
            FillDropdown();
        }
    }

    protected void FillDropdown()
    {
        FillBatchCombo();
    }

    private void FillBatchCombo()
    {
        try
        {

            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
            if (academicCalenderList.Count > 0)
            {
                academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();

                ddlBatch.Items.Clear();
                ddlBatch.Items.Add(new ListItem("-Select-", "0"));
                ddlBatch.AppendDataBoundItems = true;

                foreach (AcademicCalender academicCalender in academicCalenderList)
                    ddlBatch.Items.Add(new ListItem("[" + academicCalender.Code + "] " + academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
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

    #endregion

    #region Event

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        try
        {
            int batchId = Convert.ToInt32(ddlBatch.SelectedValue);
            if (batchId != 0)
            {
                string[] batchName = ddlBatch.SelectedItem.Text.Remove(0, 1).Split(']');
                string batch = batchName[0];
                int resultProcess = StudentACUDetailManager.Calculate_GPAandCGPAByBatch(batch);

                if (resultProcess != 0)
                    lblMsg.Text = "Process effect";
                else
                    lblMsg.Text = "No effect";
            }            
        }
        catch { }
    }

    #endregion
}