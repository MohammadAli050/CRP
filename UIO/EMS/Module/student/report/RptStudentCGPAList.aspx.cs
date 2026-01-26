using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using LogicLayer.BusinessObjects.RO;
using System.Drawing;

public partial class Report_RptStudentCGPAList : BasePage
{
    BussinessObject.UIUMSUser userObj = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        pnlMessage.Visible = false;

        if (!IsPostBack)
        {
            if (userObj.RoleID == 9)
            {
                User user = UserManager.GetById(userObj.Id);
                Student student = StudentManager.GetBypersonID(user.Person.PersonID);

                txtStudentId.Text = student.Roll;
                txtStudentId.ReadOnly = true;
            }

        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.Refresh();

        string studentId = Convert.ToString(txtStudentId.Text);

        

        if (string.IsNullOrEmpty(txtStudentId.Text))
        {
            lblMessage.Text = "Please select any student's roll.";
            return;
        }

        Student student = StudentManager.GetByRoll(txtStudentId.Text);
        //if (student.Block.IsResultBlock == true)
        //{
        //    lblMessage.Text = "Please CLEAR YOUR DUES to see Result, CONTACT ACCOUNTS DEPARTMENT.";
        //    return;
        //}

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
            LoadStudent(studentId);
        }
    }

    private void LoadStudent(string studentId)
    {
        string processResult = StudentACUDetailManager.Calculate_GpaCgpa(0, 0, 0, studentId);

        List<rStudentGPACGPA> list = StudentManager.GetStudentGPACGPAByRoll(studentId);

        if (list.Count != 0)
        {
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/miu/student/report/RptStudentCGPAList.rdlc");
            ReportDataSource rds = new ReportDataSource("RptStudentCGPAList", list);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
        }
        else
        {
            ShowMessage("Student Have To Complete At Least One Exam For GPA");
        }
    }

    private void ShowMessage(string msg)
    {
        pnlMessage.Visible = true;

        lblMessage.Text = msg;
        lblMessage.ForeColor = Color.Red;
    }
}