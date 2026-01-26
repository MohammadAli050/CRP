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

public partial class Report_Transcript : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.CheckPage_Load();

        pnlMessage.Visible = false;
        
        if (!IsPostBack)
        {
            
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        string studentId = Convert.ToString(txtStudentId.Text);

            if (studentId == string.Empty)
            {
                ShowMessage("Please Give a Student Roll.");
                return;
            }
            else
            {
                LoadStudent(studentId);
            }
    }

    //View Report On WebPage
    private void LoadStudent(string studentId)
    {
        List<rStudentTranscript> list = StudentManager.GetStudentTrancriptById(studentId);
      
        List<rStudentGradeDetail> gradeDetailList = StudentGradeDetailManager.GetAllGrade(studentId);

        if (list.Count != 0)
        {
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/miu/student/report/RptStudentTranscript.rdlc");
            ReportDataSource rds = new ReportDataSource("StudentTranscript", list); 
            ReportDataSource rds2 = new ReportDataSource("Grade", gradeDetailList);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.DataSources.Add(rds2);
            lblMessage.Text = "";
           
        }
        else
        {
            ShowMessage("NO Data Found. Enter A Valid Student ID");
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