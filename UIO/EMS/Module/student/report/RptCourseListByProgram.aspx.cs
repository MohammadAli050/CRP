using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using CommonUtility;
using Microsoft.Reporting.WebForms;
using LogicLayer.BusinessObjects.RO;

public partial class Report_RptCourseListByProgram : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        pnlMessage.Visible = false;
        
        if (!IsPostBack)
        {
            //lblCount.Text = "0";
            LoadDropDown();
       
        }
    }

    private void LoadDropDown()
    {
        FillProgramCombo();
        int programId = Convert.ToInt32(ddlProgram.SelectedItem.Value);

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

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            int programId = Convert.ToInt32(ddlProgram.SelectedItem.Value);            

            if (programId == 0)
            {
                ShowMessage("Please Select Program.");
                return;
            }           
            else
            {
                LoadProgram(programId);
            }

        }
        catch (Exception)
        {
        }
    }

    private void LoadProgram(int programId)
    {
        List<rCourseListByProgram> list = StudentManager.GetAllByProgram(programId);

        if (list.Count != 0)
        {
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/student/report/RptCourseListByProgram.rdlc");
            ReportDataSource rds = new ReportDataSource("RptCourseListByProgram", list);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            lblMessage.Text = "";
            //lblCount.Text = list.Count().ToString();
        }
        else 
        {
            ShowMessage("NO Data Found. Enter A Valid Program.");
            return;
        }
    }
  

    private void ShowMessage(string msg)
    {
        pnlMessage.Visible = true;

        lblMessage.Text = msg;
        lblMessage.ForeColor = Color.Red;

    }
}