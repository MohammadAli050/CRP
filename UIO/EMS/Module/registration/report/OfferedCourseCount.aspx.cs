using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessLogic;
using System.Data;
using System.IO;
using System.Net;

public partial class OfferedCourseCount : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        if (!IsPostBack)
        {
            List<rOfferedCourseCount> list = new List<rOfferedCourseCount>();
            rvOfferedCourseCount.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSet", list);
            rvOfferedCourseCount.LocalReport.DataSources.Add(rds);
            rvOfferedCourseCount.LocalReport.Refresh();
        }
    }

    protected void loadButton_Click(object sender, EventArgs e)
    {
        try
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);

            if (programId > 0)
            {
                List<rOfferedCourseCount> list = ReportManager.GetOfferedCourseCountByProgramID(programId);
                if (list != null)
                {
                    rvOfferedCourseCount.LocalReport.DataSources.Clear();
                    ReportDataSource rds = new ReportDataSource("DataSet", list);
                    rvOfferedCourseCount.LocalReport.DataSources.Add(rds);
                    rvOfferedCourseCount.LocalReport.Refresh();
                }
            }
            else
            {
                lblMsg.Text = "Select the program.";
                ucProgram.Focus();
                return;
            }
        }
        catch (Exception exp)
        {
        }
    }


}