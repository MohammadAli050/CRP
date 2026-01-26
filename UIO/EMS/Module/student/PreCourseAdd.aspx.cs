using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;

public partial class StudentManagement_PreCourseAdd : BasePage
{
    int studentId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
         
        try
        {
            if (Request.QueryString["param1"] != null)
            {
                studentId = Convert.ToInt32(Request.QueryString["param1"].ToString());

                LoadStudentData(studentId);
            }

            if (!IsPostBack)
            {
                LoadCourse();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void LoadStudentData(int StudentId)
    {
        List<StudentPreCourse> collection = StudentPreCourseManager.GetByStudentId(StudentId);

        if (collection != null)
        {
            gvPreCourse.DataSource = collection.ToList();
            gvPreCourse.DataBind();
        }
    }

    private void LoadCourse()
    {
        try
        {
            ddlPreCourse.Items.Clear();
            ddlMainCourse.Items.Clear();
            List<Course> courseList = CourseManager.GetAll();

            ddlPreCourse.Items.Add(new ListItem("-Select-", "0"));
            ddlMainCourse.Items.Add(new ListItem("-Select-", "0"));

            ddlPreCourse.AppendDataBoundItems = true;
            ddlMainCourse.AppendDataBoundItems = true;

            foreach (Course course in courseList)
            {
                ListItem item = new ListItem();
                item.Value = course.CourseID.ToString() + "," + course.VersionID.ToString();
                item.Text = course.FormalCode + " - " + course.Title;
                ddlPreCourse.Items.Add(item);
                ddlMainCourse.Items.Add(item);
            }

            ddlPreCourse.SelectedIndex = 0;
            ddlMainCourse.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
        }
        finally { }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            int id = int.Parse(btn.CommandArgument.ToString());

            bool result = StudentPreCourseManager.Delete(id);

            LoadStudentData(studentId);
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string preCourse=string.Empty;
            string mainCourse = string.Empty;

            if (ddlPreCourse.SelectedItem.Value.ToString() != "0")
            {
                preCourse = ddlPreCourse.SelectedItem.Value.ToString();
            }
            else
            {
                return;
            }
            if (ddlMainCourse.SelectedItem.Value.ToString() != "0")
            {
                mainCourse = ddlMainCourse.SelectedItem.Value.ToString();
            }
            string[] preCourseSplit = preCourse.Split(',');
            string[] mainCourseSplit = mainCourse.Split(',');


            StudentPreCourse studentPreCourse = new StudentPreCourse();

            studentPreCourse.StudentId = studentId;
            if (ddlPreCourse.SelectedItem.Value.ToString() != "0")
            {
                studentPreCourse.PreCourseId = Convert.ToInt32(preCourseSplit[0]);
                studentPreCourse.PreVersionId = Convert.ToInt32(preCourseSplit[1]);
            }
            if (ddlMainCourse.SelectedItem.Value.ToString() != "0")
            {
                studentPreCourse.MainCourseId = Convert.ToInt32(mainCourseSplit[0]);
                studentPreCourse.MainVersionId = Convert.ToInt32(mainCourseSplit[1]);
            }
            studentPreCourse.PreNodeCourseId = 0;
            studentPreCourse.ManiNodeCourseId = 0;
            studentPreCourse.IsBundle = chkIsbundle.Checked;
            studentPreCourse.CreatedBy = -1;
            studentPreCourse.CreatedDate = DateTime.Now;
            studentPreCourse.ModifiedBy = -1;
            studentPreCourse.ModifiedDate = DateTime.Now;

            int id = StudentPreCourseManager.Insert(studentPreCourse);

            LoadStudentData(studentId);
        }
        catch (Exception)
        {

            throw;
        }

    }
}