using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessLogic;
using CommonUtility;
using System.Drawing;

public partial class OpenAndAssignCourse : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        lblMessage.Text = "";

        if (!IsPostBack)
        {
            if (Request.QueryString["stdId"] != null)
            {
                Student student = StudentManager.GetById(Convert.ToInt32(Request.QueryString["stdId"]));
                FillStudentInfo(student);
                LoadCourse(student.StudentID);
            }
        }
    }

    private void FillStudentInfo(Student student)
    {
        lblProgram.Text = student.Program.ShortName;
        lblBatch.Text = student.Batch.BatchNO.ToString();
        lblName.Text = student.BasicInfo.FullName;
        txtStudentId.Text = student.Roll;
    }

    private void CleareGrid()
    {
        gvCoursePreRegistration.DataSource = null;
        gvCoursePreRegistration.DataBind();
    }

    private void LoadCourse(int studentId)
    {
        List<RegistrationWorksheet> collection = RegistrationWorksheetManager.GetByStudentID(studentId);

        if (collection != null)
        {
            collection = collection.Where(x => x.IsOfferedCourse == true && x.ObtainedGPA < (decimal)3.25 && x.CourseStatusId != 9).ToList();
            if (collection != null)
            {
                gvCoursePreRegistration.DataSource = collection.OrderBy(c => c.Priority).ToList();
                gvCoursePreRegistration.DataBind();
            }
        }
    }

    protected void btnOpenCourse_Click(object sender, EventArgs e)
    {
        try
        {
            ShowMessage("");

            LinkButton btn = (LinkButton)sender;
            int id = int.Parse(btn.CommandArgument.ToString());
            RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(id);

            if (registrationWorksheet.IsAutoOpen == true)
            {
                registrationWorksheet.IsAutoOpen = false;
            }
            else
            {
                registrationWorksheet.IsAutoOpen = true;
                registrationWorksheet.CourseStatusId = 0;
            }
            registrationWorksheet.ModifiedBy = registrationWorksheet.StudentID;
            registrationWorksheet.ModifiedDate = DateTime.Now;
            bool result = RegistrationWorksheetManager.UpdateForAssignCourseNew(registrationWorksheet);

            LoadCourse(registrationWorksheet.StudentID);

        }
        catch (Exception)
        {
        }
    }

    private void ShowMessage(string msg)
    {
        lblMessage.Text = msg;
    }

    protected void btnAssignCourse_Click(object sender, EventArgs e)
    {
        try
        {
            ShowMessage("");

            LinkButton btn = (LinkButton)sender;
            int id = int.Parse(btn.CommandArgument.ToString());
            RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(id);

            if (registrationWorksheet.IsAutoAssign == true)
            {
                registrationWorksheet.IsAutoAssign = false;
            }
            else
            {
                registrationWorksheet.IsAutoAssign = true;
            }
            registrationWorksheet.ModifiedBy = registrationWorksheet.StudentID;
            registrationWorksheet.ModifiedDate = DateTime.Now;
            bool result = RegistrationWorksheetManager.UpdateForAssignCourseNew(registrationWorksheet);

            LoadCourse(registrationWorksheet.StudentID);

        }
        catch (Exception)
        {
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtStudentId.Text.Trim()))
            {
                Student student = StudentManager.GetByRoll(txtStudentId.Text.Trim());
                FillStudentInfo(student);
                if (student.IsActive == false)
                {
                    lblMessage.Text = "This Student ID is Inactive. Please contact with system admin.";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Focus();
                    return;
                }
                if (student.IsBlock == true)
                {
                    lblMessage.Text = "This Student ID is blocked. Please contact with system admin.";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Focus();
                    return;
                }
                LoadCourse(student.StudentID);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error : 9011";
        }
    }
}