using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RptClassRoutine : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        
        pnlMessage.Visible = false;

        if (!IsPostBack)
        {
            ucProgram.LoadDropDownList();
            //lblCount.Text = "0";
        }
    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }

    protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {       
        int programId = Convert.ToInt32(ucProgram.selectedValue);
        int acaCalId = Convert.ToInt32(ucSession.selectedValue);

        if (programId == 0)
        {
            ShowMessage("Please Select A Program.");
            return;
        }
        else if (acaCalId == 0)
        {
            ShowMessage("Please Select A Session.");
            return;
        }
        else
        {
            LoadClassRoutine(programId, acaCalId);
        }
       
    }
      
    protected void LoadClassRoutine(int programID, int acaCalID)
    {        
        List<rClassRoutineByProgram> list = ClassRoutineManager.GetClassRoutineByProgramAndAcaCalId(programID, acaCalID);

        if (list.Count != 0)
        {
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/miu/schedular/report/RptClassRoutine.rdlc");
            ReportDataSource rds = new ReportDataSource("RptClassRoutineByProgram", list);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            lblMessage.Text = "";
            //lblCount.Text = list.Count().ToString();

        }
        else 
        {
            ShowMessage("NO Data Found. Enter A Valid Program And Session.");
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