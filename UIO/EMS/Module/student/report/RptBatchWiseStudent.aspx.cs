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

public partial class RptBatchWiseStudent : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        pnlMessage.Visible = false;

        if (!IsPostBack)
        {
            ucProgram.LoadDropDownList();
            //lblCount.Text = "0";
            LoadDropDown();
            chkStudentID.Checked = true;
            chkFullName.Checked = true;
            chkGender.Checked = true;
            chkDateOfBirth.Checked = true;

        }
    }

    protected void LoadDropDown()
    {
        //Load Year
        //LoadGender
        ddlGender.Items.Add(new ListItem("-All-", "0"));
        ddlGender.Items.Add(new ListItem("Male", "1"));
        ddlGender.Items.Add(new ListItem("Female", "2"));
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {

            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int batchId = 0;
            if (programId != 0)
                batchId = Convert.ToInt32(ucBatch.selectedValue);
            //int semesterId = Convert.ToInt32(ddlSemester.SelectedValue);
            //string year = ddlYear.SelectedValue;
            LoadStudent(programId, batchId);

        }
        catch (Exception)
        {
        }
    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }

    protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void LoadStudent(int programId, int batchId)
    {
        string program = ucProgram.selectedValue == "0" ? "-All-" : ucProgram.selectedText;
        string batch = string.IsNullOrEmpty(ucBatch.selectedText) ? "-All-" : ucBatch.selectedText;
        Person person = new Person();
        ReportParameter p1 = new ReportParameter("Show_Program", program);
        ReportParameter p2 = new ReportParameter("Show_Batch", batch);
        ReportParameter p3 = new ReportParameter("StudentID", chkStudentID.Checked.ToString());
        ReportParameter p4 = new ReportParameter("FullName", chkFullName.Checked.ToString());
        ReportParameter p5 = new ReportParameter("DateOfBirth", chkDateOfBirth.Checked.ToString());
        ReportParameter p6 = new ReportParameter("Gender", chkGender.Checked.ToString());
        ReportParameter p7 = new ReportParameter("Phone", chkPhone.Checked.ToString());
        ReportParameter p8 = new ReportParameter("Email", chkEmail.Checked.ToString());
        ReportParameter p9 = new ReportParameter("Photo", chkPhoto.Checked.ToString());
        //ReportParameter p10 = new ReportParameter("Year", ddlYear.SelectedItem.Text);
        //ReportParameter p11 = new ReportParameter("AdmissionSemester", ddlSemester.SelectedItem.Text);
        ReportParameter p12 = new ReportParameter("PresentAddress", chkPresentAddress.Checked.ToString());
        ReportParameter p13 = new ReportParameter("PermanentAddress", chkPermanentAddress.Checked.ToString());


        List<rStudentBatchWise> studentList = StudentManager.GetStudentProgramOrBatch(programId, batchId);
        if (studentList.Count != 0)
        {
            /* foreach (rStudentBatchWise sPB in studentList)
             {
                 list.Add(sPB);
             }*/

            if (ddlGender.SelectedValue == "1")
                studentList = studentList.Where(x => x.Gender != null && x.Gender.Equals("Male")).ToList();
            else if (ddlGender.SelectedValue == "2")
                studentList = studentList.Where(x => x.Gender != null && x.Gender.Equals("Female")).ToList();

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Module/student/report/RptBatchWiseStudent.rdlc");
            this.ReportViewer1.LocalReport.EnableExternalImages = true;
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p12, p13 });
            ReportDataSource rds = new ReportDataSource("BatchWiseStudent", studentList);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            lblMessage.Text = "";
            //lblCount.Text = studentList.Count().ToString();
        }
        else
        {
            ShowMessage("NO Data Found. Enter Valid Program And Batch");
            ReportViewer1.LocalReport.DataSources.Clear();
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