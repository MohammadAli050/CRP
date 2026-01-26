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
using System.Web.Security;

public partial class Admin_GenerateWorksheet : BasePage
{
    #region Event
    int userId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
        User user = UserManager.GetByLogInId(loginID);
        if (user != null)
            userId = user.User_ID;
        ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
        _scriptMan.AsyncPostBackTimeout = 36000;

        if (!IsPostBack)
        {
            ucProgram.LoadDropdownWithUserAccess(userId);
            lblCount.Text = "0"; 
        }
    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }

    protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
    {
        CleareGrid();
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        lblCount.Text = "0";
        gvStudentList.PageIndex = 0;

        int programId = Convert.ToInt32(ucProgram.selectedValue);
        int batchId = Convert.ToInt32(ucBatch.selectedValue);
        string roll = txtStudent.Text.Trim();

        if (batchId == 0 && string.IsNullOrEmpty(roll))
        {
            lblMessage.Text = "Please select Batch.";
            lblMessage.Focus();
            return;
        }
        else if (programId == 0 && string.IsNullOrEmpty(roll))
        {
            lblMessage.Text = "Please select Program.";
            lblMessage.Focus();
            return;
        }
        else
        {
            LoadStudent(programId, batchId, roll);
        }
    }

    protected void gvStudentList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvStudentList.PageIndex = e.NewPageIndex;

        }
        catch (Exception ex)
        {

        }
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Checked)
            {
                chk.Text = "Unselect All";
            }
            else
            {
                chk.Text = "Select All";
            }

            foreach (GridViewRow row in gvStudentList.Rows)
            {
                Label lb = (Label)row.FindControl("lblRegStatus");
                CheckBox ckBox = (CheckBox)row.FindControl("ChkActive");
                if(lb.Text != "Yes")
                    ckBox.Checked = chk.Checked;
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnGenetareWorksheet_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in gvStudentList.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("ChkActive");
                if (ckBox.Checked)
                {
                    RegistrationWorksheetParam rwParam = new RegistrationWorksheetParam();

                    HiddenField hdId = (HiddenField)row.FindControl("hdnId");
                    int id = Convert.ToInt32(hdId.Value);

                    Student student = StudentManager.GetById(id);

                    rwParam.StudentId = student.StudentID;
                    rwParam.BatchID = student.BatchId; 
                    rwParam.DepartmentID = student.Program.DeptID;
                    rwParam.ProgramID = Convert.ToInt32(student.ProgramID);
                    rwParam.TreeCalendarMasterID = Convert.ToInt32(student.TreeCalendarMasterID);
                    rwParam.TreeMasterID = Convert.ToInt32(student.TreeMasterID);

                    if (ddlGenerationType.SelectedValue == "1") // Close
                    {
                        rwParam.CourseOpenType = 1;
                    }
                    else if (ddlGenerationType.SelectedValue == "2") // Open
                    {
                        rwParam.CourseOpenType = 2;
                    }
                    else if (ddlGenerationType.SelectedValue == "3") // Full
                    {
                        rwParam.CourseOpenType = 3;
                    }
                    int semeterNumber = Convert.ToInt32(ddlSemesterNumber.SelectedValue);
                    bool result = RegistrationWorksheetManager.RegistrationWorksheetGeneratePerStudent(rwParam, semeterNumber);
                }
            }

            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucBatch.selectedValue);
            string roll = txtStudent.Text.Trim();

            LoadStudent(programId, acaCalId, roll);
        }
        catch (Exception)
        {
        }
    }

    #endregion

    #region Method

    private void LoadStudent(int programId, int batchId, string roll)
    {
        List<Student> studentList = StudentManager.GetAllByProgramOrBatchOrRoll(programId, batchId, roll);
        if (studentList != null)
            studentList = studentList.Where(s => s.IsActive == true).ToList();

        int isBlock = Convert.ToInt32(ddlBlock.SelectedValue);
        int isMajor = Convert.ToInt32(ddlMajor.SelectedValue);

        if (isBlock == 0) // 0 = not Block students
        {
            if (studentList != null)
                studentList = studentList.Where(s => s.IsBlock == false).ToList();
        }
        else if (isBlock == 1) // 1 = Block Student
        {
            if (studentList != null)
                studentList = studentList.Where(s => s.IsBlock == true).ToList();
        }

        if (isMajor == 1) // 0 = Major node declared students
        {
            if (studentList != null)
                studentList = studentList.Where(s => !string.IsNullOrEmpty(s.Major1NodeID.ToString())).ToList();
        }
        else if (isMajor == 2) // 1 = Major node not declared Student
        {
            if (studentList != null)
                studentList = studentList.Where(s => string.IsNullOrEmpty(s.Major1NodeID.ToString())).ToList();
        }

        if (studentList != null)
            studentList = studentList.OrderBy(s => s.Roll).ToList();

        gvStudentList.DataSource = studentList;
        gvStudentList.DataBind();

        lblCount.Text = studentList.Count().ToString();
    }

    private void CleareGrid()
    {
        gvStudentList.DataSource = null;
        gvStudentList.DataBind();
    }

    #endregion
}