using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using CommonUtility;
using System.Drawing;

public partial class StudentActive : BasePage
{
    #region Events
    //protected void Page_Load(object sender, EventArgs e)
    //{
    int userId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
        User user = UserManager.GetByLogInId(loginID);
        if (user != null)
            userId = user.User_ID;
        pnlMessage.Visible = false;
        lblCount.Text = "0";

        if (!IsPostBack)
        {
            ucProgram.LoadDropdownWithUserAccess(userId);
        }
    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }

    protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
    {
        gvStudentList.DataSource = null;
        gvStudentList.DataBind();
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            ClearGrid();

            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucBatch.selectedValue);
            string roll = txtRoll.Text.Trim();

            if (programId == 0 && string.IsNullOrEmpty(roll))
            {
                ShowMessage("Please select Program.");
                return;
            }
            else if (acaCalId == 0 && string.IsNullOrEmpty(roll))
            {
                ShowMessage("Please select Academic Calender.");
                return;
            }
            else
            {
                LoadStudent(programId, acaCalId, roll);
            }

        }
        catch (Exception)
        {
        }
    }

    private void LoadStudent(int programId, int acaCalId, string roll)
    {
        List<Student> studentList = StudentManager.GetAllByProgramOrBatchOrRoll(programId, acaCalId, roll);

        if (studentList != null)
            studentList = studentList.OrderBy(s => s.Roll).ToList();
        
        gvStudentList.DataSource = studentList;
        gvStudentList.DataBind();

        lblCount.Text = studentList.Count().ToString();
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        try
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucBatch.selectedValue);


            foreach (GridViewRow row in gvStudentList.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("ChkActive");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");

                HiddenField hiddenId = (HiddenField)row.FindControl("hdnId");
                Student student = StudentManager.GetById(Convert.ToInt32(hiddenId.Value));

                student.IsActive = ckBox.Checked;
                student.Remarks = txtRemarks.Text;

                bool i = StudentManager.Update(student);
            }
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

            //if (chk.Checked)
            //{
            //    chk.Text = "Unselect All";
            //}
            //else
            //{
            //    chk.Text = "Select All";
            //}

            foreach (GridViewRow row in gvStudentList.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("ChkActive");
                ckBox.Checked = chk.Checked;
            }
        }
        catch (Exception ex)
        {


        }
    }

    #endregion

    #region Methods
    private void ClearGrid()
    {
        gvStudentList.DataSource = null;
        gvStudentList.DataBind();

        lblCount.Text = "0";
    }

   

    private void ShowMessage(string msg)
    {
        pnlMessage.Visible = true;

        lblMessage.Text = msg;
        lblMessage.ForeColor = Color.Red;

    }
    #endregion

}