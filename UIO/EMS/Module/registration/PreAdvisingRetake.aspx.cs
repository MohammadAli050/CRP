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

public partial class PreAdvisingRetake : BasePage
{
    BussinessObject.UIUMSUser userObj = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        lblMessage.Text = "";
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        if (!IsPostBack)
        {
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            CleareGrid();

            if (string.IsNullOrEmpty(txtStudent.Text))
            {
                lblMessage.Text = "Insert student Roll";
                return;
            }

            Student student = StudentManager.GetByRoll(txtStudent.Text.Trim());
            if (student != null)
            {
                if (AccessAuthentication(userObj, student.Roll.Trim()))
                {
                    FillStudentInfo(student);

                    LoadAutoOpenCourseForRetake(student.StudentID);
                }
                else
                {
                    lblMessage.Text = "Access Permission Denied.";
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void FillStudentInfo(Student student)
    {
        lblProgram.Text = student.Program.ShortName;
        lblBatch.Text = student.Batch.BatchNO.ToString();
        lblName.Text = student.BasicInfo.FullName;
    }

    private void CleareGrid()
    {
        gvCoursePreRegistrationRetake.DataSource = null;
        gvCoursePreRegistrationRetake.DataBind();
    }

    private void LoadAutoOpenCourseForRetake(int id)
    {
        List<RegistrationWorksheet> collection = null;
        collection = RegistrationWorksheetManager.GetByStudentID(id);

        if (collection != null)
        {
            collection = collection.Where(c => c.IsCompleted == true && c.IsAutoOpen != true).ToList();

            if (collection != null)
            {
                gvCoursePreRegistrationRetake.DataSource = collection.OrderBy(c => c.Priority).ToList();
                gvCoursePreRegistrationRetake.DataBind();
            }
        }
    }

    protected void btnPreRegistrationRetake_Click(object sender, EventArgs e)
    {
        try
        {
            ShowMessage("");

            LinkButton btn = (LinkButton)sender;
            int id = int.Parse(btn.CommandArgument.ToString());
            RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(id);
            Student student = StudentManager.GetById(registrationWorksheet.StudentID);

            if (registrationWorksheet.IsAutoOpen == false)
            {
                if (IsAlreadyTakeThisCourse(registrationWorksheet.StudentID, registrationWorksheet.CourseID, registrationWorksheet.VersionID))
                {
                    ShowMessage("Course has been taken already. Please select others...");
                    lblMessage.Focus();
                    return;
                }
            }

            if (registrationWorksheet.IsAutoOpen == true)
            {
                registrationWorksheet.IsAutoOpen = false;
            }
            else
            {
                registrationWorksheet.IsAutoOpen = true;
            }

            registrationWorksheet.ModifiedBy = registrationWorksheet.StudentID;
            registrationWorksheet.ModifiedDate = DateTime.Now;
            bool result = RegistrationWorksheetManager.UpdateForAssignCourseRetake(registrationWorksheet);

            LoadAutoOpenCourseForRetake(registrationWorksheet.StudentID);
        }
        catch (Exception)
        {

        }
    }

    private bool IsAlreadyTakeThisCourse(int studentId, int courseId, int versionId)
    {
        bool result = false;

        List<RegistrationWorksheet> collection = new List<RegistrationWorksheet>();
        collection = RegistrationWorksheetManager.GetAllOpenCourseByStudentID(studentId);

        int count = collection.Where(c => c.CourseID == courseId && c.VersionID == versionId && c.IsAutoAssign == true).ToList().Count();
        if (count > 0)
        {
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    private void ShowMessage(string msg)
    {
        lblMessage.Text = msg;
    }
}