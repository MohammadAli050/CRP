using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_CourseByProgramUserControl : System.Web.UI.UserControl
{
    public event EventHandler CourseByProgramSelectedIndexChanged;
    public string selectedValue = string.Empty;
    public string selectedText = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LoadDropDownList();
            }

            selectedValue = ddlCourseByProgram.SelectedValue;
            selectedText = ddlCourseByProgram.SelectedItem.Text;
        }
        catch (Exception)
        {   
        }       
    }

    protected void ddlCourseByProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedValue = ddlCourseByProgram.SelectedValue;
        selectedText = ddlCourseByProgram.SelectedItem.Text;

        if (CourseByProgramSelectedIndexChanged != null)
            CourseByProgramSelectedIndexChanged(this, e);
    }

    private void LoadDropDownList()
    {
        ddlCourseByProgram.Items.Clear();
        ddlCourseByProgram.Items.Add(new ListItem("Select Program", "0"));       
    }

    public void LoadDropDownList(int Id)
    {
        List<Course> courseList = new List<Course>();
        courseList = CourseManager.GetAllByProgram(Id);

        ddlCourseByProgram.Items.Clear();
        ddlCourseByProgram.AppendDataBoundItems = true;

        if (courseList != null)
        {
            courseList = courseList.OrderBy(c => c.FormalCode).ToList();

            ddlCourseByProgram.Items.Add(new ListItem("-Select-", "0"));
            ddlCourseByProgram.DataTextField = "CourseFullInfo";
            ddlCourseByProgram.DataValueField = "CoureIdVersionId";

            ddlCourseByProgram.DataSource = courseList;
            ddlCourseByProgram.DataBind();
        }
    }

    internal void SelectedValue(int id)
    {
        ddlCourseByProgram.SelectedValue = id.ToString();
    }

    internal void IndexO()
    {
        ddlCourseByProgram.SelectedIndex = 0;
    }

}