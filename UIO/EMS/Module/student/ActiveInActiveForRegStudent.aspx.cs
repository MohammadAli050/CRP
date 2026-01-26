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

public partial class ActiveInActiveForRegStudent : BasePage
{
    #region Events
    
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        pnlMessage.Visible = false;
        lblCount.Text = "0";

        if (!IsPostBack)
        {
            LoadDropDown();
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            ClearGrid();

            int programId = Convert.ToInt32(ddlProgram.SelectedItem.Value);
            int acaCalBatchId = Convert.ToInt32(ddlAcaCalBatch.SelectedItem.Value);
            int acaCalSessionId = Convert.ToInt32(ddlAcaCalSession.SelectedItem.Value);
            string roll = txtRoll.Text.Trim();
            
            LoadStudent(programId, acaCalBatchId, acaCalSessionId, roll);           

        }
        catch (Exception)
        {
        }
    }

    private void LoadStudent(int programId, int acaCalBatchId, int acaCalSessionId, string roll)
    {
        List<Student> studentListMain = StudentManager.GetAllByProgramOrBatchOrRoll(programId, acaCalBatchId,  roll);
        List<Student> studentListReg = StudentManager.GetAllRegisteredStudentBySession(acaCalSessionId);

        if (studentListReg != null)
        {
            foreach (Student item in studentListMain)
            {
                if (studentListReg.Exists(x => x.StudentID == item.StudentID))
                {
                    item.IsRegistered = true;
                }
            }
        }

        int isReg = Convert.ToInt32(ddlRegistered.SelectedValue);
        if (isReg == 1)
        {
            studentListMain = studentListMain.Where(s => s.IsRegistered == true).ToList();
        }
        else if (isReg == 2)
        {
            studentListMain = studentListMain.Where(s => s.IsRegistered == false).ToList();
        }

        int isActive = Convert.ToInt32(ddlActive.SelectedValue);
        if (isActive == 1)
        {
            studentListMain = studentListMain.Where(s => s.IsActive == true).ToList();
        }
        else if (isActive == 2)
        {
            studentListMain = studentListMain.Where(s => s.IsActive == false).ToList();
        }


        if (studentListMain != null)
            studentListMain = studentListMain.OrderBy(s => s.Roll).ToList();
                
        gvStudentList.DataSource = studentListMain;
        gvStudentList.DataBind();

        lblCount.Text = studentListMain.Count().ToString();
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        try
        {
            int programId = Convert.ToInt32(ddlProgram.SelectedItem.Value);
            int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedItem.Value);


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

    private void LoadDropDown()
    {
        //follow the order of combo loding
        FillAcaCalBatch();
        FillAcaCalSession();
        FillProgramCombo();
        int programId = Convert.ToInt32(ddlProgram.SelectedItem.Value);

    }

    private void FillAcaCalSession()
    {
        try
        {
            ddlAcaCalSession.Items.Clear();
            ddlAcaCalSession.Items.Add(new ListItem("Select", "0"));

            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
            academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();

            ddlAcaCalSession.AppendDataBoundItems = true;

            if (academicCalenderList != null)
            {
                ddlAcaCalSession.DataSource = academicCalenderList.OrderByDescending(d => d.Code).ToList();
                ddlAcaCalSession.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void FillAcaCalBatch()
    {
        try
        {
            ddlAcaCalBatch.Items.Clear();
            ddlAcaCalBatch.Items.Add(new ListItem("Select", "0"));

            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
            academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();

            ddlAcaCalBatch.AppendDataBoundItems = true;

            if (academicCalenderList != null)
            {
                ddlAcaCalBatch.DataSource = academicCalenderList.OrderByDescending(d => d.Code).ToList();
                ddlAcaCalBatch.DataBind();
            }

            //ddlAcaCalBatch.Items.Clear();
            //ddlAcaCalBatch.Items.Add(new ListItem("Select", "0"));
            //List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
            //academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();


            //ddlAcaCalBatch.AppendDataBoundItems = true;

            //if (academicCalenderList != null)
            //{
            //    int count = academicCalenderList.Count;
            //    foreach (AcademicCalender academicCalender in academicCalenderList)
            //    {
            //        ddlAcaCalBatch.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));

            //    }

            //}

        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    private void FillProgramCombo()
    {
        try
        {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Add(new ListItem("Select", "0"));
            List<Program> programList = ProgramManager.GetAll();

            ddlProgram.AppendDataBoundItems = true;

            if (programList != null)
            {
                ddlProgram.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                ddlProgram.DataBind();
            }

        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    private void ShowMessage(string msg)
    {
        pnlMessage.Visible = true;

        lblMessage.Text = msg;
        lblMessage.ForeColor = Color.Red;

    }
    #endregion

}