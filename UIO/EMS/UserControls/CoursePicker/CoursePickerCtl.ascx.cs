using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BussinessObject;

public partial class UserControls_CoursePicker_CoursePickerCtl : System.Web.UI.UserControl
{
    public event EventHandler PickCourse;

    private Course _course = null;

    private const string SESSIONPICKEDCOURSE = "PickedCourse";

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
        }
    }

    public void Clear()
    {
        lblCouseTitle.Text = string.Empty;
        if (Session[SESSIONPICKEDCOURSE] != null)
        {
            Session.Remove(SESSIONPICKEDCOURSE);
        }
        _course = null;
    }

    public override void Focus()
    {
        btnPick.Focus();
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
            lblCouseTitle.Text = string.Empty;
        }
        else
        {
            lblCouseTitle.Text = _course.VersionCode + "-" + _course.Title;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        btnPick.Attributes.Add("onclick", "OpenModal();");
    }

    protected void btnPick_Click(object sender, EventArgs e)
    {
        if (Session[SESSIONPICKEDCOURSE] != null)
        {
            _course = (Course)Session[SESSIONPICKEDCOURSE];
        }
        ResetCourse();
        if (PickCourse != null)
        {
            PickCourse(this, e);
        }
    }
}
