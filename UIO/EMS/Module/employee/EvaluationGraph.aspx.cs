using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ServayByStudent_EvaluationGraph : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        try
        {
            
            if (!IsPostBack)
            {
                FillDropdown();
            }
        }
        catch { }
    }

    protected void FillDropdown()
    {
        FillProgramCombo();
        FillAcaCalSectionCombo();
    }

 
    private void FillProgramCombo()
    {
        try
        {
            ddlProgram.Items.Clear();
            List<Program> programList = ProgramManager.GetAll();

            ddlProgram.Items.Add(new ListItem("-Select-", "0"));
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

    void FillAcaCalSectionCombo()
    {
        ddlAcaCalSection.Items.Add(new ListItem("-Select-", "0"));
    }

    void FillAcaCalSectionCombo(int acaCalId, int programId, string searchKey)
    {
        try
        {
            List<AcademicCalenderSection> acaCalSectionList = AcademicCalenderSectionManager.GetAllByAcaCalId(acaCalId);
            if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
            {
                ddlAcaCalSection.Items.Clear();
                ddlAcaCalSection.Items.Add(new ListItem("-Select-", "0"));

                if (programId != 0)
                    acaCalSectionList = acaCalSectionList.Where(x => x.AcademicCalenderID == acaCalId && (x.ProgramID == programId )).ToList();
                else if (acaCalId == 0)
                    acaCalSectionList = acaCalSectionList.Where(x => x.ProgramID == programId  ).ToList();
                else if (programId == 0)
                    acaCalSectionList = acaCalSectionList.Where(x => x.AcademicCalenderID == acaCalId).ToList();

                if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
                {
                    List<Course> courseList = CourseManager.GetAll();
                    Hashtable hashCourse = new Hashtable();
                    foreach (Course course in courseList)
                        hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.Title + ":" + course.FormalCode);

                    Dictionary<string, string> dicAcaCalSec = new Dictionary<string, string>();
                    foreach (AcademicCalenderSection acaCalSection in acaCalSectionList)
                    {
                        string courseVersion = acaCalSection.CourseID.ToString() + "_" + acaCalSection.VersionID.ToString();
                        try
                        {
                            dicAcaCalSec.Add(hashCourse[courseVersion] + "(" + acaCalSection.SectionName + ") ", acaCalSection.AcaCal_SectionID.ToString());
                        }
                        catch { }
                    }
                    var acaCalSecList = dicAcaCalSec.Where(c => c.Key.ToUpper().Contains(searchKey.ToUpper())).OrderBy(x => x.Key).ToList();
                    foreach (var temp in acaCalSecList)
                        ddlAcaCalSection.Items.Add(new ListItem(temp.Key, temp.Value));
                }
            }
        }
        catch { }
    }

    #endregion

    #region Event

    protected void uclBatch_Change(Object sender, EventArgs e)
    {
        int acaCalId = Convert.ToInt32(uclAcaCal.selectedValue);
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);

        if (acaCalId != 0)
            FillAcaCalSectionCombo(acaCalId, programId, "");
        else
            lblMsg.Text = "Please Select Semester";
    }

    protected void ddlProgram_Change(Object sender, EventArgs e)
    {
        int acaCalId = Convert.ToInt32(uclAcaCal.selectedValue);
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);
        uclAcaCal.LoadDropDownList(programId);

        if (acaCalId != 0)
            FillAcaCalSectionCombo(acaCalId, programId, "");
        else
            lblMsg.Text = "Please Select Semester";
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int acaCalId = Convert.ToInt32(uclAcaCal.selectedValue);
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);
        string searchKey = txtSearchKey.Text;

        if (acaCalId == 0 || programId == 0)
        {
            lblMsg.Text = "Please Select Batch and Program.";
            return;
        }

        FillAcaCalSectionCombo(acaCalId, programId, searchKey);
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(uclAcaCal.selectedValue);
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);
            int acaCalSecId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);

            if (acaCalId == 0 || programId == 0 || acaCalSecId == 0)
            {
                string warning = string.Empty;
                if (acaCalId == 0)
                    warning += " - Semester";
                if (programId == 0)
                    warning += " - Program";
                if (acaCalSecId == 0)
                    warning += " - Course";

                lblMsg.Text = "Please select " + warning + " Dropdown List";
            }
            else
            {
                List<EvaluationForm> evaluationFormList = EvaluationFormManager.GetAll(acaCalSecId);
            }
        }
        catch { }
    }

    #endregion
}