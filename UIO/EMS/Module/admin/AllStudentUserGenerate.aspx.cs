using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AllStudentUserGenerate : BasePage
{
    #region Function

    // Page Load Function
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        lblMsg.Text = "";

        if (!IsPostBack)
        {
            LoadComboBox();
        }
    }
    
    // Call All ComboBox Function to load the dropdown
    protected void LoadComboBox()
    {
        try
        {
            ddlBatch.Items.Clear();
            ddlBatch.Items.Add(new ListItem("All Batch", "0"));

            ProgramComboBox();
        }
        catch { }
        finally { }
    }
    
    // Load program dropdown
    protected void ProgramComboBox()
    {
        try
        {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Add(new ListItem("All Program", "0"));
            ddlProgram.AppendDataBoundItems = true;

            List<Program> programList = ProgramManager.GetAll();

            if (programList.Count > 0 && programList != null)
            {
                foreach (Program program in programList)
                    ddlProgram.Items.Add(new ListItem(program.ShortName + " [" + program.DetailName + "]", program.ProgramID.ToString()));

                Program_Change(null, null);
            }
        }
        catch { }
        finally { }
    }
    
    // Load batch dropdown
    protected void LoadBatchCombo(int programId)
    {
        try
        {
            ddlBatch.Items.Clear();
            ddlBatch.Items.Add(new ListItem("All Batch", "0"));
            ddlBatch.AppendDataBoundItems = true;

            List<Batch> batchList = BatchManager.GetAll();
            if (batchList.Count > 0 && batchList != null)
            {
                batchList = batchList.Where(x => x.ProgramId == programId).ToList();
                if (batchList.Count > 0 && batchList != null)
                {
                    batchList = batchList.OrderByDescending(x => x.BatchNO).ToList();

                    ddlBatch.DataTextField = "BatchNO";
                    ddlBatch.DataValueField = "BatchId";
                    ddlBatch.DataSource = batchList;
                    ddlBatch.DataBind();
                }
            }
        }
        catch { }
        finally { }
    }

    #endregion
    
    #region Event

    // Generate userName and password
    protected void Generate_Click(Object sender, EventArgs e)
    {
        try
        {
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);
            int batchId = Convert.ToInt32(ddlBatch.SelectedValue);

            int resultInsert = UserManager.GenerateUserByProgram(programId, batchId);

            if (resultInsert > 0)
                lblMsg.Text = "Total " + resultInsert + " user created";
            else
                lblMsg.Text = "No user created";
        }
        catch { }
        finally { }
    }
    
    // View the report
    protected void View_Click(Object sender, EventArgs e)
    {
        try
        {
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);
            int batchId = Convert.ToInt32(ddlBatch.SelectedValue);
            List<User> userList = UserManager.GetAllByProgramId(programId, batchId);

            if (userList.Count > 0 && userList != null)
            {
                ReportDataSource rds = new ReportDataSource("UserList", userList);

                ReportViewer01.LocalReport.DataSources.Clear();
                ReportViewer01.LocalReport.DataSources.Add(rds);
            }
            else
            {
                lblMsg.Text = "Not found. Please generate first then view";
            }

             
        }
        catch { }
        finally { }
    }
    
    // Change the program dropdown value
    protected void Program_Change(Object sender, EventArgs e)
    {
        try
        {
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);
            LoadBatchCombo(programId);
        }
        catch { }
        finally { }
    }
    
    // Change the batch dropdown value
    protected void Batch_Change(Object sender, EventArgs e)
    {
        try
        {
        }
        catch { }
        finally { }
    }

    #endregion
}