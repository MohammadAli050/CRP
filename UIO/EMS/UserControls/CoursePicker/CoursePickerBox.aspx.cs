using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BussinessObject;
using System.Drawing;

public partial class UserControls_CoursePicker_CoursePickerBox : System.Web.UI.Page
{
    private const string SESSIONPICKEDCOURSE = "PickedCourse";
    private const string SESSIONFOUNDCOURSES = "SearchedCourses";

    private void FillGrid()
    {
        List<Course> courses = Course.GetCourses(txtFormalCode.Text.Trim(), txtVersionCode.Text.Trim(), txtTitle.Text.Trim());

        if (Session[SESSIONFOUNDCOURSES] != null)
        {
            Session.Remove(SESSIONFOUNDCOURSES);
        }
        Session.Add(SESSIONFOUNDCOURSES, courses);

        grdResult.DataSource = courses;
        grdResult.DataBind();
    }

    private void FillGrid(Course course)
    {
        List<Course> courses = new List<Course>();
        courses.Add(course);

        if (Session[SESSIONFOUNDCOURSES] != null)
        {
            Session.Remove(SESSIONFOUNDCOURSES);
        }
        Session.Add(SESSIONFOUNDCOURSES, courses);

        grdResult.DataSource = courses;
        grdResult.DataBind();
    }

    private void ShowMessage(string message, Color color)
    {
        lblMsg.Text = string.Empty;
        lblMsg.Text = message;
        lblMsg.ForeColor = color;
    }

    private void FillGridAll()
    {
        List<Course> courses = Course.GetCourses();

        if (Session[SESSIONFOUNDCOURSES] != null)
        {
            Session.Remove(SESSIONFOUNDCOURSES);
        }
        Session.Add(SESSIONFOUNDCOURSES, courses);

        grdResult.DataSource = courses;
        grdResult.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        //btnOK.Attributes.Add("onclick", "CloseModal();");
        if (!IsPostBack)
        {
            btnClose.Attributes.Add("onclick", "CloseModal();");
            //btnOK.Attributes.Add("onclick", "CloseModal();");
            if (Session[SESSIONPICKEDCOURSE] != null)
            {
                FillGrid((Course)Session[SESSIONPICKEDCOURSE]);
            }
        }

        //string reference = Page.ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerData", "context");
        //string callbackScript = "function CallServer(arg, context)" + Environment.NewLine + "{" + Environment.NewLine + reference.ToString() + ";" + Environment.NewLine + "}";
        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallServer", callbackScript, true);
    }
    protected void btnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, Color.Red);
        }
    }
    protected void btnAll_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            FillGridAll();
        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, Color.Red);
        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            //Course course = null;
            //if (grdResult.SelectedIndex >= 0 && Session[SESSIONFOUNDCOURSES] != null)
            //{
            //    List<Course> courses = (List<Course>)Session[SESSIONFOUNDCOURSES];

            //    course = courses[grdResult.SelectedIndex];
            //}

            //if (course != null)
            //{
            //    if (Session[SESSIONPICKEDCOURSE] != null)
            //    {
            //        Session.Remove(SESSIONPICKEDCOURSE);
            //    }
            //    Session.Add(SESSIONPICKEDCOURSE, course);
            //}

            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "h", "<script>CloseModal();</script>");
        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, Color.Red);
        }
    }
    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, Color.Red);
        }
    }
    protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Course course = null;
            if (grdResult.SelectedIndex >= 0 && Session[SESSIONFOUNDCOURSES] != null)
            {
                List<Course> courses = (List<Course>)Session[SESSIONFOUNDCOURSES];

                course = courses[grdResult.SelectedIndex];
            }

            if (course != null)
            {
                if (Session[SESSIONPICKEDCOURSE] != null)
                {
                    Session.Remove(SESSIONPICKEDCOURSE);
                }
                Session.Add(SESSIONPICKEDCOURSE, course);
            }

            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "h", "<script>CloseModal();</script>");
        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, Color.Red);
        }
    }
}
