using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TEST_MIUReports_YearlyCollectionReportMonthWise : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
      

        if (!IsPostBack)
        {
            ddList_ProgramIDPopulate();
            ddList_YearPopulate();
            ddListSemester.Enabled = false;
            ddListSemester.Items.Insert(0, new ListItem("All", "0"));
           
            
        }
       
    }

    public void ddList_SemesterPopulate(int ProgramID)
    {

        List<RCalenderUnitTypeID> list = new List<RCalenderUnitTypeID>();
        list = DepartmentManager.RCalenderUnitTypeIDGetAllById(ProgramID);

        if (list.Count != 0)
        {


            ddListSemester.DataSource = list;
            ddListSemester.DataTextField = "TypeName";
            ddListSemester.DataValueField = "CalenderUnitTypeID";
            ddListSemester.DataBind();



        }

    }

    public void ddList_ProgramIDPopulate()
    {
             
            List<RProgramIDCodeDetailName> list = new List<RProgramIDCodeDetailName>();
            list = DepartmentManager.RProgramIDCodeDetailNameGetAll();

            if (list.Count != 0)
            {
                
	            
                ddListProgramID.DataSource = list;            
                ddListProgramID.DataTextField = "DetailName";
                ddListProgramID.DataValueField = "ProgramID";
                ddListProgramID.DataBind();
                ddListProgramID.Items.Insert(0, new ListItem("All", "0"));


            }
        
    }


    public void ddList_YearPopulate()
    {

        List<RYear> list = new List<RYear>();
        list = DepartmentManager.RYearGetAll();

        if (list.Count != 0)
        {


            ddListYear.DataSource = list;
            ddListYear.DataTextField = "Year";
            ddListYear.DataValueField = "Year";
            ddListYear.DataBind();



        }

    }



   

    protected void RunReport_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddListProgramID.SelectedValue) && !string.IsNullOrEmpty(ddListYear.SelectedValue))
        {
            if (string.IsNullOrEmpty(ddListSemester.SelectedValue) || ddListSemester.SelectedValue.ToString()=="") { ddListSemester.SelectedValue = "0"; } 
            LoadR2DepartmentInfo(Convert.ToInt32(ddListProgramID.SelectedValue), Convert.ToInt32(ddListYear.SelectedValue), Convert.ToInt32(ddListSemester.SelectedValue));
            
        }      

    }


    private void LoadR2DepartmentInfo(int ProgramID, int Year, int CalenderUnitTypeID)
    {
        List<RSLMonthCashBankTotal> list = new List<RSLMonthCashBankTotal>();
        list = DepartmentManager.RSLMonthCashBankTotalGetAllById(ProgramID, Year, CalenderUnitTypeID);

        if (list.Count != 0)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSet1", list);
            ReportViewer1.LocalReport.ReportPath = "Test/MIUReports/YearlyCollectionReportMonthWise.rdlc";
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportParameter[] Parameters = new ReportParameter[3];
            Parameters[0] = new ReportParameter("ReportParameterProgram", ddListProgramID.SelectedItem.Text);
            Parameters[1] = new ReportParameter("ReportParameterSemester", ddListSemester.SelectedItem.Text);           
            Parameters[2] = new ReportParameter("ReportParameterYear", ddListYear.SelectedValue);        
           
            ReportViewer1.LocalReport.SetParameters(Parameters);
            ReportViewer1.LocalReport.Refresh();
        }
    }
    protected void ddListProgramID_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddList_SemesterPopulate(Convert.ToInt32(ddListProgramID.SelectedValue));
        ddListSemester.Items.Insert(0, new ListItem("All", "0"));
        ddListSemester.Enabled = true;
    }
}