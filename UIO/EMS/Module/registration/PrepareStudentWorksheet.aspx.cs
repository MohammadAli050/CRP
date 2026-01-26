using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;

public partial class StudentRegistration_PrepareStudentWorksheet : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        if (!IsPostBack && !IsCallback)
        {
            FillAcademicCalenderCombo();
            FillProgramCombo();
        }
    }

    private void FillAcademicCalenderCombo()
    {
        try
        {
            ddlAcaCalBatch.Items.Clear();
            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();

            //ddlAcaCalBatch.Items.Add(new ListItem("Select", "0"));
            ddlAcaCalBatch.AppendDataBoundItems = true;

            if (academicCalenderList != null)
            {
                int count = academicCalenderList.Count;
                foreach (AcademicCalender academicCalender in academicCalenderList)
                {
                    ddlAcaCalBatch.Items.Add(new ListItem(academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                    count = academicCalender.AcademicCalenderID;
                }
                ddlAcaCalBatch.SelectedValue = count.ToString();
            }

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
            List<Program> programList = ProgramManager.GetAll();

            //ddlProgram.Items.Add(new ListItem("Select", "0"));
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

    protected void btnView_Click(object sender, EventArgs e)
    {

        int programID = Convert.ToInt32(ddlProgram.SelectedValue);
        int academicCalenderID = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);

        List<Student> studentList = StudentManager.GetAllByProgramIdBatchId(programID, academicCalenderID);
        gvwCollection.DataSource = studentList;
        gvwCollection.DataBind();
        gvwCollection.Columns[1].Visible = false;
        gvwCollection.Columns[4].Visible = false;
        gvwCollection.Columns[5].Visible = false;
        

        BussinessObject.DeptRegSetUp _deptRegSetUp = null;
        _deptRegSetUp = BussinessObject.DeptRegSetUp.GetBProgramID(programID);

        if (_deptRegSetUp.LocalCredit1.HasValue)
        {
            txtAutoOpen.Text = _deptRegSetUp.LocalCredit1.Value.ToString();
        }
        if (_deptRegSetUp.LocalCredit2.HasValue)
        {
            txtProAutoOpen.Text = _deptRegSetUp.LocalCredit2.Value.ToString();
        }

        if (_deptRegSetUp.AutoPreRegCredit1.HasValue)
        {
            txtAutoPreReg.Text = _deptRegSetUp.AutoPreRegCredit1.Value.ToString();
        }
        if (_deptRegSetUp.AutoPreRegCredit2.HasValue)
        {
            txtProAutoPreReg.Text = _deptRegSetUp.AutoPreRegCredit2.Value.ToString();
        }

        if (_deptRegSetUp.ManCredit1.HasValue)
        {
            txtAutoMandatory.Text = _deptRegSetUp.ManCredit1.Value.ToString();
        }
        if (_deptRegSetUp.ManCredit2.HasValue)
        {
            txtProAutoMandatory.Text = _deptRegSetUp.ManCredit2.Value.ToString();
        }
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ch = (CheckBox)sender;
        GridViewRow row = (GridViewRow)ch.NamingContainer;
        ch.Checked = ((CheckBox)sender).Checked;

        //if (ch.Checked)
        //{
        //    row.BackColor = System.Drawing.Color.LightSalmon;
        //    row.Cells[4].Text = ddlCourseTree.SelectedItem.Value.ToString();
        //    row.Cells[5].Text = ddlLinkedCalendars.SelectedItem.Value.ToString();
        //    row.Cells[6].Text = ddlCourseTree.SelectedItem.Text + " »» " + ddlLinkedCalendars.SelectedItem.Text;
        //}
        //else
        //{
        //    row.Cells[4].Text = string.Empty;
        //    row.Cells[5].Text = string.Empty;
        //    row.Cells[6].Text = string.Empty;
        //    row.BackColor = System.Drawing.Color.Empty;
        //}
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow rowItem in gvwCollection.Rows)
        {
            CheckBox chk = (CheckBox)sender;
            chk = (CheckBox)(rowItem.Cells[4].FindControl("chk"));
            chk.Checked = ((CheckBox)sender).Checked;
            //if (chk.Checked)
            //{
            //    rowItem.Cells[4].Text = ddlCourseTree.SelectedItem.Value.ToString();
            //    rowItem.Cells[5].Text = ddlLinkedCalendars.SelectedItem.Value.ToString();
            //    rowItem.Cells[6].Text = ddlCourseTree.SelectedItem.Text + " »» " + ddlLinkedCalendars.SelectedItem.Text;
            //    rowItem.BackColor = System.Drawing.Color.LightSalmon;
            //}
            //else
            //{
            //    rowItem.Cells[4].Text = string.Empty;
            //    rowItem.Cells[5].Text = string.Empty;
            //    rowItem.Cells[6].Text = string.Empty;
            //    rowItem.BackColor = System.Drawing.Color.Empty;
            //}
        }
    }

    protected void btnFillAll_Click(object sender, EventArgs e)
    {
        string textValue = txtAutoOpen.Text;
        foreach (GridViewRow rowItem in gvwCollection.Rows)
        {
            TextBox txt = (TextBox)(rowItem.Cells[7].FindControl("txt"));
            txt.Text = textValue;
        }
    }

    protected void btnGenerateWorkSheet_Click(object sender, EventArgs e)
    {
        int progID = Convert.ToInt32(ddlProgram.SelectedValue);
        for (int i = 0; i < gvwCollection.Rows.Count; i++)
        {
            int studentId, treeCalendarMasterID, treeMasterID, openCourse, academicCalenderID, programID;

            CheckBox chk = (CheckBox)gvwCollection.Rows[i].Cells[0].FindControl("chk");
            if (chk.Checked)
            {
                studentId = Int32.Parse(gvwCollection.Rows[i].Cells[1].Text.Trim());
                treeCalendarMasterID = Int32.Parse(gvwCollection.Rows[i].Cells[5].Text.Trim()); //Prefix
                treeMasterID = Int32.Parse(gvwCollection.Rows[i].Cells[4].Text.Trim());
                TextBox txt = (TextBox)(gvwCollection.Rows[i].Cells[7].FindControl("txt"));
                openCourse = Int32.Parse(txt.Text.Trim());
                academicCalenderID = 1;
                programID = progID;

                int result = GenerateWorksheetManager.Insert(studentId, treeCalendarMasterID, treeMasterID, openCourse, academicCalenderID, programID);
            }
        }
    }
}