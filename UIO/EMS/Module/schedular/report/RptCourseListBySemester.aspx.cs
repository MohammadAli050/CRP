using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BussinessObject;
using als = LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using LogicLayer.BusinessLogic;
using System.Drawing;

public partial class Report_RptCourseListBySemester : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {     
        base.CheckPage_Load();

        pnlMessage.Visible = false;

        if (!IsPostBack)
        {
            //lblCount.Text = "0";
            FillProgCombo();
            FillTreeCombo();              
            FillLinkedCalendars();
                
        }

    }

    #region Variables

    private List<BussinessObject.Program> _programs = new List<BussinessObject.Program>();
    private List<TreeCalendarMaster> _treeCalMasters = null;

    #endregion

    #region Methods

    private void FillProgCombo()
    {
        ddlPrograms.Items.Clear();

        _programs = BussinessObject.Program.GetPrograms();

        if (_programs != null)
        {
            foreach (BussinessObject.Program program in _programs)
            {
                ListItem item = new ListItem();
                item.Value = program.Id.ToString();
                item.Text = program.ShortName;
                ddlPrograms.Items.Add(item);
            }

            if (Session["Programs"] != null)
            {
                Session.Remove("Programs");
            }
            Session.Add("Programs", _programs);

            ddlPrograms.SelectedIndex = 0;
        }
    }

    private void FillTreeCombo()
    {
        ddlTreeMasters.Items.Clear();

        if (ddlPrograms.SelectedIndex >= 0)
        {
            if (Session["Programs"] != null)
            {
                List<Program> programs = (List<Program>)Session["Programs"];
                var program = from prog in programs where prog.Id == Int32.Parse(ddlPrograms.SelectedValue) select prog;

                List<TreeMaster> treeMasters = TreeMaster.GetByProgram(program.ElementAt(0).Id);

                if (treeMasters != null)
                {
                    ddlTreeMasters.Enabled = true;

                    foreach (TreeMaster treeMaster in treeMasters)
                    {
                        ListItem item = new ListItem();
                        item.Value = treeMaster.Id.ToString();
                        item.Text = treeMaster.RootNode.Name;
                        ddlTreeMasters.Items.Add(item);
                    }

                    if (Session["TreeMaster"] != null)
                    {
                        Session.Remove("TreeMaster");
                    }
                    if (Session["TreeMasters"] != null)
                    {
                        Session.Remove("TreeMasters");
                    }
                    Session.Add("TreeMasters", treeMasters);

                    ddlTreeMasters.SelectedIndex = 0;
                    ddlTreeMasters_SelectedIndexChanged(null, null);
                }
                else
                {
                    if (Session["TreeMaster"] != null)
                    {
                        Session.Remove("TreeMaster");
                    }
                    if (Session["TreeMasters"] != null)
                    {
                        Session.Remove("TreeMasters");
                    }
                    if (Session["TreeCalMaster"] != null)
                    {
                        Session.Remove("TreeCalMaster");
                    }
                    if (Session["TreeCalMasters"] != null)
                    {
                        Session.Remove("TreeCalMasters");
                    }
                    ddlTreeMasters.Enabled = false;
                    ddlLinkedCalendars.Items.Clear();
                    ddlLinkedCalendars.Enabled = false;

                    //ShowMessage("No Tree found for the selected Program", Color.Red);
                }
            }
        }
    }

    private void FillLinkedCalendars()
    {
        ddlLinkedCalendars.Items.Clear();

        if (ddlTreeMasters.SelectedIndex >= 0)
        {
            if (Session["TreeMasters"] != null)
            {
                List<TreeMaster> treeMasters = (List<TreeMaster>)Session["TreeMasters"];
                var treeMaster = from treeMas in treeMasters where treeMas.Id == Int32.Parse(ddlTreeMasters.SelectedValue) select treeMas;

                _treeCalMasters = TreeCalendarMaster.GetByTreeMaster(treeMaster.ElementAt(0).Id);
                Session.Add("TreeMaster", treeMaster.ElementAt(0));

                if (_treeCalMasters != null)
                {
                    ddlLinkedCalendars.Enabled = true;

                    ListItem item = new ListItem();
                    item.Value = 0.ToString();
                    item.Text = "<New Link>";
                    ddlLinkedCalendars.Items.Add(item);

                    foreach (TreeCalendarMaster treeCalMaster in _treeCalMasters)
                    {
                        item = new ListItem();
                        item.Value = treeCalMaster.Id.ToString();
                        item.Text = treeCalMaster.Name + " - " + treeCalMaster.CalendarMaster.Name;
                        ddlLinkedCalendars.Items.Add(item);
                    }

                    if (Session["TreeCalMaster"] != null)
                    {
                        Session.Remove("TreeCalMaster");
                    }
                    if (Session["TreeCalMasters"] != null)
                    {
                        Session.Remove("TreeCalMasters");
                    }
                    Session.Add("TreeCalMasters", _treeCalMasters);

                    ddlLinkedCalendars.SelectedIndex = 0;
                }
                else
                {
                    if (Session["TreeCalMaster"] != null)
                    {
                        Session.Remove("TreeCalMaster");
                    }
                    if (Session["TreeCalMasters"] != null)
                    {
                        Session.Remove("TreeCalMasters");
                    }
                    ddlLinkedCalendars.Enabled = false;
                    //ShowMessage("No linked calendars were found for the selected tree", Color.Red);
                }
            }
        }
    }

    #endregion 

    #region Events

    protected void ddlPrograms_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           
             FillTreeCombo();
        }
        catch (Exception Ex)
        {
            //ShowMessage(Ex.Message, Color.Red);
        }
    }

    protected void ddlTreeMasters_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {         
            FillLinkedCalendars();
            
        }
        catch (Exception Ex)
        {
            //ShowMessage(Ex.Message, Color.Red);
        }
    }

    protected void ddlLinkedCalendars_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (true)
            {
                
            }
        }
        catch (Exception Ex)
        {
            //ShowMessage(Ex.Message, Color.Red);
        }
    }


    
    protected void btnLoad_Click(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(ddlPrograms.SelectedValue);
        string[] x = ddlLinkedCalendars.SelectedItem.Text.Split('-');
        string treeCalendarMasterId = x[0];

        LoadTreeDistribution(programId, treeCalendarMasterId);  
    }

    private void LoadTreeDistribution(int programId, string treeCalendarMasterId)
    {
        List<als.rTreeDistribution> treeDistribution = CourseManager.GetTreeDistributionByProgram(programId, treeCalendarMasterId);

        if (treeDistribution.Count != 0)
        {
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/miu/schedular/report/RptCourseListBySemester.rdlc");
            ReportDataSource rds = new ReportDataSource("RptTreeDistribution", treeDistribution);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            lblMessage.Text = "";
            //lblCount.Text = treeDistribution.Count().ToString();
        }
        else
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            ShowMessage("NO Data Found. Please Enter Valid Program, Trees And Distribution");
            return;
        }
    }

    private void ShowMessage(string msg)
    {
        pnlMessage.Visible = true;

        lblMessage.Text = msg;
        lblMessage.ForeColor = Color.Red;

    }

    #endregion
}