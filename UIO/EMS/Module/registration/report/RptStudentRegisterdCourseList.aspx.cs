using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RptStudentRegistyerdCourseList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        pnlMessage.Visible = false;

        if (!IsPostBack)
        {
            ucProgram.LoadDropDownList();
        }

    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }
    protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
    {
        //ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }
    
    protected void buttonView_Click(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(ucProgram.selectedValue);
        int acaCalId = Convert.ToInt32(ucSession.selectedValue);
        int batchId = Convert.ToInt32(ucBatch.selectedValue);

        if (programId != 0 && acaCalId != 0 && batchId != 0) // && batchId != 0)
        {
            LoadRunningStudentCourseList(programId, acaCalId, batchId);
        }
        else {
            ShowMessage("Invalid Selection.");
        }

           
    }

    private void LoadRunningStudentCourseList(int programId, int acaCalId, int batchId)
    {
        List<rStudentCourse> list = StudentManager.GetRunningStudentCourseByProgramIdAcaCalId(programId, acaCalId, batchId);

        if (list.Count != 0)
        {
            ReportParameter p1 = new ReportParameter("Program", ucProgram.selectedText);
            ReportParameter p2 = new ReportParameter("Session", ucSession.selectedText);
            ReportParameter p3 = new ReportParameter("Batch", ucBatch.selectedText);
            RegisteredCourses.LocalReport.ReportPath = Server.MapPath("~/miu/registration/report/RptStudentRegisterdCourseList.rdlc");
            this.RegisteredCourses.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3 });
            RegisteredCourses.LocalReport.EnableExternalImages = true;
            ReportDataSource rds = new ReportDataSource("StudentRegisteredCourse", list);

            RegisteredCourses.LocalReport.DataSources.Clear();
            RegisteredCourses.LocalReport.DataSources.Add(rds);
            lblMessage.Text = "";            
        }
        else
        {
            ShowMessage("NO Data Found. Enter Valid Program And Batch");
            return;
        }
    }

    //View Report In PDF For Print
    protected void buttonPrint_Click(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(ucProgram.selectedValue);
        int acaCalId = Convert.ToInt32(ucSession.selectedValue);
        int batchId = Convert.ToInt32(ucBatch.selectedValue);
        if (programId != 0 && acaCalId != 0) // && batchId != 0)
        {
            PrintReport(programId, acaCalId, 0);
        }
        else
        {
            ShowMessage("Invalid Selection.");
        }
    }

    private void PrintReport(int programId, int acaCalId, int batchId)
    {
        

        List<rStudentCourse> list = StudentManager.GetRunningStudentCourseByProgramIdAcaCalId(programId, acaCalId, batchId);

        if (list.Count != 0)
        {
            ReportParameter p1 = new ReportParameter("Program", ucProgram.selectedText);
            ReportParameter p2 = new ReportParameter("Session", ucSession.selectedText);
            ReportParameter p3 = new ReportParameter("Batch", ucBatch.selectedText);
            RegisteredCourses.LocalReport.ReportPath = Server.MapPath("~/miu/registration/report/RptStudentRegisterdCourseList.rdlc");

            this.RegisteredCourses.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3 });
            RegisteredCourses.LocalReport.EnableExternalImages = true;
            ReportDataSource rds = new ReportDataSource("StudentRegisteredCourse", list);

            RegisteredCourses.LocalReport.DataSources.Clear();
            RegisteredCourses.LocalReport.DataSources.Add(rds);
            lblMessage.Text = "";   

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;

            byte[] bytes = RegisteredCourses.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

            using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportPDF/" + "StudentRegisterdCourseList" + ".pdf"), FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }

            string path = Server.MapPath("~/Upload/ReportPDF/" + "StudentRegistyerdCourseList" + ".pdf");

            WebClient client = new WebClient();   // Open PDF File in Web Browser 

            Byte[] buffer = client.DownloadData(path);
            if (buffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
            }
        }
        else
        {
            ShowMessage("NO Data Found. Enter Valid Program And Batch");
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