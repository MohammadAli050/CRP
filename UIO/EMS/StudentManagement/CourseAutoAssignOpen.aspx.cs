using System;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using System.Drawing;
using BussinessObject;
using Common;
using System.Data;

//namespace EMS.StudentManagement
//{
public partial class StudentManagement_CourseAutoAssignOpen : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UIUMSUser CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            if (CurrentUser != null)
            {
                if (CurrentUser.RoleID > 0)
                {
                    AuthenticateHome(CurrentUser);
                }
            }
            else
            {
                Response.Redirect("~/Security/Login.aspx");
            }
            if (!IsPostBack && !IsCallback)
            {
                FillProgramCombo();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
    private void FillProgramCombo()
    {
        try
        {
            cboProgram.Items.Clear();
            lblMsg.Text = string.Empty;
            List<Program> _programs = Program.GetPrograms();
            if (_programs == null)
            {
                return;
            }
            foreach (Program prog in _programs)
            {
                ListEditItem item = new ListEditItem();
                item.Value = prog.Id.ToString();
                item.Text = prog.ShortName;
                cboProgram.Items.Add(item);
            }
            cboProgram.SelectedIndex = 0;
            cboProgram_SelectedIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    private List<StudentEntity> RefreshObject()
    {
        List<StudentEntity> stds = new List<StudentEntity>();
        foreach (GridViewRow rowItem in gvwCollection.Rows)
        {
            CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("chk"));
            if (chk.Checked)
            {
                StudentEntity std = new StudentEntity();
                std.Id = Int32.Parse(rowItem.Cells[1].Text.Trim());
                std.TreeMasterID = Int32.Parse(rowItem.Cells[4].Text.Trim());
                std.ProgramID = int.Parse(cboProgram.SelectedItem.Value.ToString());
                std.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                std.CreatedDate = DateTime.Now;

                stds.Add(std);
            }
        }
        return stds;
    }

    protected void btnAutoAssignandOpen_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            List<StudentEntity> stds = RefreshObject();
            if (stds == null)
            {
                Common.Utilities.ShowMassage(lblMsg, Color.Red, "Student(s) are needed to select before this process.");
                return;
            }
            int ret = Student_CalCourseProgNode_BAO.Save(stds);
            if (ret == 0)
            {
                Common.Utilities.ShowMassage(lblMsg, Color.Blue, "Successfully not saved");
            }
            else
            {
                Common.Utilities.ShowMassage(lblMsg, Color.Blue, "Successfully saved");
            }
        }
        catch (Exception ex)
        {

            Common.Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
    }

    protected void cboProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvwCollection.DataSource = null;
        gvwCollection.DataBind();
        lblMsg.Text = string.Empty;

        UIUMSUser CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        

        try
        {
            List<Student> stds = Student.GetStudentsByProgID(int.Parse(cboProgram.SelectedItem.Value.ToString()));
            if (stds == null)
            {
                Common.Utilities.ShowMassage(lblMsg, Color.Blue, "Students are not found.");
                return;
            }
            List<Student> permittedStudents = new List<Student>();
            foreach (Student std in stds)
            {
                if (AccessAuthentication(CurrentUser, std.Roll.Trim()))
                {
                    permittedStudents.Add(std);
                }
            }
            //btnAutoAssignandOpen.Enabled = true;
            gvwCollection.DataSource = permittedStudents;
            gvwCollection.DataBind();
            gvwCollection.Columns[1].Visible = false;
            gvwCollection.Columns[4].Visible = false;

        }
        catch (Exception ex)
        {
            Common.Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;

        CheckBox chk = (CheckBox)sender;
        if (chk.Checked)
        {
            chk.Text = " Unselect All ";
        }
        else
        {
            chk.Text = " Select All ";
        }
        foreach (GridViewRow rowItem in gvwCollection.Rows)
        {
            chk = (CheckBox)(rowItem.Cells[0].FindControl("chk"));
            chk.Checked = ((CheckBox)sender).Checked;
            if (rowItem.Cells[4].Text.Trim() == "0" || rowItem.Cells[4].Text.Trim() == "")
            {
                chk.Checked = false;
            }

            if (chk.Checked)
            {
                rowItem.BackColor = System.Drawing.Color.LightSalmon;
            }
            else
            {
                rowItem.BackColor = System.Drawing.Color.Empty;
                chk.Checked = false;
            }
        }
    }
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        CheckBox ch = (CheckBox)sender;
        GridViewRow row = (GridViewRow)ch.NamingContainer;
        ch.Checked = ((CheckBox)sender).Checked;

        if (ch.Checked)
        {
            row.BackColor = System.Drawing.Color.LightSalmon;
        }
        else
        {
            row.BackColor = System.Drawing.Color.Empty;
        }
    }
}
//}
