using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonUtility;
using Common;

public partial class AssignPreCourse : BasePage
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
            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll().OrderByDescending(o => o.AcademicCalenderID).ToList() ;

            ddlAcaCalBatch.Items.Add(new ListItem("Select", "0"));
            ddlAcaCalBatch.AppendDataBoundItems = true;

            if (academicCalenderList != null)
            {
                ddlAcaCalBatch.DataSource = academicCalenderList.OrderByDescending(d => d.Code).ToList();
                ddlAcaCalBatch.DataBind();
                //int count = academicCalenderList.Count;
                //foreach (AcademicCalender academicCalender in academicCalenderList)
                //{
                //    ddlAcaCalBatch.Items.Add(new ListItem(academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                //    count = academicCalender.AcademicCalenderID;
                //}
                //ddlAcaCalBatch.SelectedValue = count.ToString();
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

            ddlProgram.Items.Add(new ListItem("Select", "0"));
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

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {

            int programID = Convert.ToInt32(ddlProgram.SelectedValue);
            int academicCalenderID = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);

            LoadGridView(programID, academicCalenderID);
        }
        catch (Exception)
        {
        }
    }

    private void LoadGridView(int programID, int academicCalenderID)
    {
        List<Student> studentList = StudentManager.GetAllByProgramIdBatchId(programID, academicCalenderID);
        gvStudent.DataSource = studentList.OrderBy(s=> s.Roll);
        gvStudent.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvStudent.Rows.Count; i++)
            {
                Student student = new Student();
                int id = 0;
                CheckBox chk = (CheckBox)gvStudent.Rows[i].Cells[0].FindControl("chk");
                if (chk.Checked)
                {
                    id = Int32.Parse(gvStudent.Rows[i].Cells[1].Text.Trim());
                    student = StudentManager.GetById(id);
                    student.TreeMasterID = Int32.Parse(gvStudent.Rows[i].Cells[4].Text.Trim());
                    // student.Prefix = Int32.Parse(gvStudent.Rows[i].Cells[5].Text.Trim());
                    student.ModifiedBy = 0;
                    student.ModifiedDate = DateTime.Now;

                    bool result = StudentManager.Update(student);
                }
            }
        }
        catch (Exception)
        {


        }
    }
    protected void btnPreCourseAdd_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            int id = int.Parse(btn.CommandArgument.ToString());

            string redirectURL = string.Format("{0}/Admin/PreCourseAdd.aspx?param1={1}", AppPath.ApplicationPath, id);
            //Response.Redirect(redirectURL, "_blank", "menubar=0,scrollbars=1,width=800,height=400,top=10");
        }
        catch (Exception)
        {


        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {

            int programID = Convert.ToInt32(ddlProgram.SelectedValue);
            int academicCalenderID = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);

            LoadGridView(programID, academicCalenderID);
        }
        catch (Exception)
        {
        }
    }
}