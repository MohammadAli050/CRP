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

public partial class RptStudentTranscriptNew2 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        pnlMessage.Visible = false;

        if (!IsPostBack)
        {

        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        string studentId = Convert.ToString(txtStudentId.Text.Trim());

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
        string processResult = StudentACUDetailManager.Calculate_GpaCgpa(0, 0, 0, studentId);
        List<rStudentGradeDetail> gradeDetailList = StudentGradeDetailManager.GetAllGrade(studentId);

        List<rStudentTranscriptNew> list = StudentManager.GetStudentTrancriptByIdNew(studentId);
        double credit = 0;
        int acaCalId = 0;
        foreach (rStudentTranscriptNew item in list)
        {
            if (item.AcaCalId != acaCalId)
            {
                acaCalId = item.AcaCalId;
                credit = item.TranscriptCredit + credit;
                item.TranscriptCredit = credit;
            }
            else
            {
                item.TranscriptCredit = 0;
            }

        }

        rStudentTranscriptGeneralInfo studentGenInfo = StudentManager.GetStudentTrancriptGeneralInfoById(studentId);

        string DOB = studentGenInfo.DOB.ToString("dd-MMM-yyyy");

        if (list.Count != 0)
        {
            ReportParameter p1 = new ReportParameter("Roll", studentId.ToUpper());
            ReportParameter p2 = new ReportParameter("FullName", studentGenInfo.FullName);
            ReportParameter p3 = new ReportParameter("FatherName", studentGenInfo.FatherName);
            ReportParameter p4 = new ReportParameter("DepartmentName", studentGenInfo.DepartmentName);
            ReportParameter p5 = new ReportParameter("ProgramName", studentGenInfo.ProgramName);
            ReportParameter p6 = new ReportParameter("DOB", DOB);
            ReportParameter p7 = new ReportParameter("Major", studentGenInfo.Major);

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/miu/student/report/RptStudentTranscriptNew2.rdlc");
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6,p7 });
            ReportDataSource rds = new ReportDataSource("StudentTranscriptDataSetNew", list);
            ReportDataSource rds2 = new ReportDataSource("GradeDataSet", gradeDetailList);

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