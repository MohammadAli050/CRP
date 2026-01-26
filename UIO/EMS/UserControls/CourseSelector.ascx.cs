using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BussinessObject;

public partial class UserControls_CourseSelector : System.Web.UI.UserControl
{
    #region Variables
    public event EventHandler PickCourse;

    private Course _course = null;
    private List<Course> _courses = null;

    private const string SESSIONPICKEDCOURSE = "PickedCourse";
    private const string SESSIONSELECTEDCOURSES = "SelectedCourses";
    private bool IsValueChanged = false; 
    #endregion

    #region Properties
    public Course PickedCourse
    {
        get
        {
            return _course;
        }
        set
        {
            _course = value;
            if (Session[SESSIONPICKEDCOURSE] != null)
            {
                Session.Remove(SESSIONPICKEDCOURSE);
            }
            Session.Add(SESSIONPICKEDCOURSE, _course);
            ResetCourse();

            if (!IsValueChanged)
            {
                List<Course> courses = new List<Course>();
                courses.Add(_course);
                SelectedCourses = courses;
            }
        }
    }
    public List<Course> SelectedCourses
    {
        get
        {
            return _courses;
        }
        set
        {
            if (Session[SESSIONSELECTEDCOURSES] != null)
            {
                Session.Remove(SESSIONSELECTEDCOURSES);
            }
            Session.Add(SESSIONSELECTEDCOURSES, _courses);
            _courses = value;
            ResetCourses();
        }
    }

    public short TextCodeTabIndex
    {
        get
        {
            return this.txtCode.TabIndex;
        }
        set
        {
            this.txtCode.TabIndex = value;
        }
    }
    public short FindButtonTabIndex
    {
        get
        {
            return this.btnFind.TabIndex;
        }
        set
        {
            this.btnFind.TabIndex = value;
        }
    }
    public short DropDownListTabIndex
    {
        get
        {
            return this.ddlCourse.TabIndex;
        }
        set
        {
            this.ddlCourse.TabIndex = value;
        }
    } 
    #endregion

    #region Functions
    public void Clear()
    {
        txtCode.Text = string.Empty;
        //txtTitle.Text = string.Empty;

        if (ddlCourse.Items != null)
        {
            ddlCourse.SelectedIndex = -1;
        }

        ddlCourse.Items.Clear();
        if (Session[SESSIONPICKEDCOURSE] != null)
        {
            Session.Remove(SESSIONPICKEDCOURSE);
        }
        _course = null;
    }

    public override void Focus()
    {
        txtCode.Focus();
    }

    private void FillCombo(List<Course> courses)
    {
        ddlCourse.Items.Clear();
        foreach (Course course in _courses)
        {
            ListItem item = new ListItem();
            item.Value = course.Id.ToString() + "," + course.VersionID.ToString();
            item.Text = course.FormalCode + " - " + course.VersionCode + " - " + course.Title + " [ " + course.OwnerProgram.ShortName + " ]";
            ddlCourse.Items.Add(item);
        }
        ddlCourse.SelectedIndex = 0;
    }

    protected override object SaveViewState()
    {
        this.ViewState["PickedCourse"] = _course;

        return base.SaveViewState();
    }

    protected override void LoadViewState(object savedState)
    {
        if (savedState != null)
        {
            base.LoadViewState(savedState);

            if (this.ViewState["PickedCourse"] != null)
                _course = (Course)this.ViewState["PickedCourse"];
        }
    }

    public void AddToViewState(string viewStateVariableName, object viewStateVariableValue)
    {
        if (IsViewStateVariableExists(viewStateVariableName) == false)
        {
            ViewState.Add(viewStateVariableName, viewStateVariableValue);
        }
        else
        {
            ViewState[viewStateVariableName] = viewStateVariableValue;
        }
    }

    public void RemoveFromViewState(string viewStateVariableName)
    {
        if (IsViewStateVariableExists(viewStateVariableName) == true)
        {
            ViewState.Remove(viewStateVariableName);
        }
    }

    public object GetFromViewState(string viewStateVariableName)
    {
        object viewStateValue = null;
        try
        {
            if (IsViewStateVariableExists(viewStateVariableName) == true)
            {
                viewStateValue = ViewState[viewStateVariableName];
            }
        }
        catch (FormatException formatException)
        {
            throw formatException;
        }
        catch (Exception exp)
        {
            throw exp;
        }
        return viewStateValue;
    }

    public bool IsViewStateVariableExists(string viewStateVariableName)
    {
        if (ViewState[viewStateVariableName] == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void ResetCourse()
    {
        if (_course == null)
        {
            txtCode.Text = string.Empty;
        }
        else
        {
            txtCode.Text = _course.VersionCode;
        }
    }
    private void ResetCourses()
    {
        if (_courses == null)
        {
            if (ddlCourse.Items != null)
            {
                ddlCourse.SelectedIndex = -1;
            }
            ddlCourse.Items.Clear();
        }
        else
        {
            ddlCourse.Items.Clear();
            foreach (Course course in _courses)
            {
                ListItem item = new ListItem();
                item.Value = course.Id.ToString() + "," + course.VersionID.ToString();
                item.Text =   course.FormalCode + " - " + course.VersionCode + " - " + course.Title + " [ " + course.OwnerProgram.ShortName + " ]" + " [ " + course.Credits + " ]"; 
                ddlCourse.Items.Add(item);
            }
            ddlCourse.SelectedIndex = 0;

            IsValueChanged = true;

            string[] courseIDnVerID = new string[2];
            courseIDnVerID = ddlCourse.SelectedValue.ToString().Split(',');
            PickedCourse = Course.GetCourse(Int32.Parse(courseIDnVerID[0]), Int32.Parse(courseIDnVerID[1]));
            AddToViewState("PickedCourse", PickedCourse);
        }
    } 
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            IsValueChanged = true;

            string[] courseIDnVerID = new string[2];
            courseIDnVerID = ddlCourse.SelectedValue.ToString().Split(',');
            //nodeCourse.ChildCourseID = Int32.Parse(courseIDnVerID[0]);
            //nodeCourse.ChildVersionID = Int32.Parse(courseIDnVerID[1]);
            PickedCourse = Course.GetCourse(Int32.Parse(courseIDnVerID[0]), Int32.Parse(courseIDnVerID[1]));
            AddToViewState("PickedCourse", PickedCourse);
        }
        catch (Exception Ex)
        {

            throw Ex;
        }
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCode.Text.Trim().Length > 0)
            {
                SelectedCourses = Course.GetCoursesByVCOde(txtCode.Text.Trim());
            }
            if (PickCourse != null)
            {
                PickCourse(this, e);
            }

        }
        catch (Exception Ex)
        {

            throw Ex;
        }
    }
    protected void txtCode_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    if (txtCode.Text.Trim().Length > 0)
        //    {
        //        SelectedCourses = Course.GetCoursesByVCOde(txtCode.Text.Trim());
        //    }
        //    if (PickCourse != null)
        //    {
        //        PickCourse(this, e);
        //    }

        //}
        //catch (Exception Ex)
        //{

        //    throw Ex;
        //}
    }
    #endregion
}
