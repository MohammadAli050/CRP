using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;

public partial class Report_RptStudentRoadmap : BasePage
{
    BussinessObject.UIUMSUser userObj = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        if (!IsPostBack)
        {
            if (userObj.RoleID == 9)
            {
                User user = UserManager.GetById(userObj.Id);
                Student student = StudentManager.GetBypersonID(user.Person.PersonID);

                txtStudentID.Text = student.Roll;
                txtStudentID.ReadOnly = true;
            }

        }
    }
    protected void btnStudentRoadmap(object sender, EventArgs e)
    {
        
        string studentId = txtStudentID.Text.Trim();
        if (studentId != "")
        {

            Student student = StudentManager.GetByRoll(txtStudentID.Text.Trim());

            if (student != null)
            {
                if (student.Block != null)
                {
                    if (student.Block.IsResultBlock == true)
                    {
                        lblMessage.Text = "Result is blocked because of DUES. (PAY DUES before deadline to avoid LATE FINE & Result/Registration/AdmitCard BLOCK)";
                        return;
                    }
                }
                lblMessage.Text = "";
                FillStudentInfo(student);
            }
          
            if (AccessAuthentication(userObj, studentId.Trim()))
            {
                
                LoadData(studentId);
            }
            else
            {
                lblMessage.Text = "Access Permission Denied.";
                lblMessage.Visible = true;
            }
        }
        else
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            lblMessage.Text = "Please Enter a Student ID";
        }
    }

    private void FillStudentInfo(Student student)
    {
        if (student.Program == null)
        {
            lblProgram.Text = "";
        }
        else
        {
            lblProgram.Text = student.Program.ShortName;
        }
        if (student.Batch == null)
        {
            lblBatch.Text = "";
        }
        else
        {
            lblBatch.Text = student.Batch.BatchNO.ToString();
        }

        lblName.Text = student.FullName;
    }

    private void LoadData(string studentId)
    {
        List<rStudentRoadmap> list = new List<rStudentRoadmap>();
        list = ReportManager.GetStudentRoadmapByStudentID(studentId);
        lblMessage.Text = "";

        if (list.Count != 0)
        {
            ReportDataSource rds = new ReportDataSource("RptStudentRoadmap", list);


            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
        }

        else
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            lblMessage.Text = "No Data Found. Please Enter a Valid Student ID";
            lblMessage.Visible = true;
        }
    }
  
}