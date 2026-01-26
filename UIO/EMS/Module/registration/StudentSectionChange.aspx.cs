using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_StudentSectionChange : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            LoadGrid();
        }
        catch (Exception ex)
        {
        }
    }

    private void LoadGrid()
    {
        string[] courseAndVersion = ddlCourse.SelectedValue.Split('_');

        List<StudentCourseHistory> StudentCourseHistoryList = StudentCourseHistoryManager.GetAllRegisteredStudentByProgramSessionCourse(Convert.ToInt32(ucProgram.selectedValue),
                                                                                                                                        Convert.ToInt32(ucSession.selectedValue),
                                                                                                                                        Convert.ToInt32(courseAndVersion[0]),
                                                                                                                                        Convert.ToInt32(courseAndVersion[1]));

        if (StudentCourseHistoryList != null)
        {
            StudentCourseHistoryList = StudentCourseHistoryList.Where(x => x.CourseStatusID != 6).ToList();

            gvStudentList.DataSource = StudentCourseHistoryList.OrderBy(a=>a.StudentInfo.Roll);
            gvStudentList.DataBind();

            LoadSectionCombo();
        }
        else
        {
            gvStudentList.DataSource = null;
            gvStudentList.DataBind();
        }

        lblCount.Text = StudentCourseHistoryList.Count().ToString();
    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }
        catch (Exception ex)
        {
        }
    }

    protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearGrid();
        }
        catch (Exception ex)
        {
        }
    }

    protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadCourseCombo();
        }
        catch (Exception ex)
        {
        }
    }

    private void LoadCourseCombo()
    {
        List<Course> courseList = CourseManager.GetAllCourseByProgramAndSessionFromStudentCourseHistoryTable(Convert.ToInt32(ucProgram.selectedValue), Convert.ToInt32(ucSession.selectedValue));

        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("-Select-", "0_0"));
        ddlCourse.AppendDataBoundItems = true;
        if (courseList != null)
        {
            foreach (Course course in courseList)
            {
                ddlCourse.Items.Add(new ListItem(course.CourseFullInfo.ToString(), course.CourseID + "_" + course.VersionID));
            }
        }
    }

    private void ClearGrid()
    {
        gvStudentList.DataSource = null;
        gvStudentList.DataBind();
    }

    protected void btnSectionUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlSection = (DropDownList)gvStudentList.HeaderRow.FindControl("ddlSection");

            string[] courseAndVersion = ddlCourse.SelectedValue.Split('_');

            foreach (GridViewRow row in gvStudentList.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("ChkSelect");
                if (ckBox.Checked)
                {
                    HiddenField Id = (HiddenField)row.FindControl("hdnId");

                    StudentCourseHistoryManager.UpdateSectionBy(Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(Id.Value));
                }
            }

            LoadGrid();
        }
        catch (Exception)
        {
        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearGrid();
        }
        catch (Exception ex)
        {
        }
    }

    private void LoadSectionCombo()
    {
        DropDownList ddlSection = (DropDownList)gvStudentList.HeaderRow.FindControl("ddlSection");

        string[] courseAndVersion = ddlCourse.SelectedValue.Split('_');

        List<AcademicCalenderSection> list = AcademicCalenderSectionManager.GetByAcaCalCourseVersion(Convert.ToInt32(ucSession.selectedValue),
                                                                                                     Convert.ToInt32(courseAndVersion[0]),
                                                                                                     Convert.ToInt32(courseAndVersion[1]));

        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("-Section-", "0"));
        ddlSection.AppendDataBoundItems = true;
        if (list != null)
        {
            foreach (AcademicCalenderSection item in list)
            {
                ddlSection.Items.Add(new ListItem(item.SectionName.ToString(), item.AcaCal_SectionID.ToString()));
            }
        }
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;            

            foreach (GridViewRow row in gvStudentList.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("chkSelect");
                ckBox.Checked = chk.Checked;
            }
        }
        catch (Exception ex)
        {
        }
    }
}