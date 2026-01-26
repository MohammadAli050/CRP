using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Common;
using BussinessObject;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

public partial class BasicSetup_Teacher : BasePage
{
    #region Variable Declaration
    List<Teacher> _teachers = null;
    Teacher _teacher = null;
    private string[] _dataKey = new string[1] { "Id" };
    List<Department> _departments = null;
    List<UIUMSUser> _users = null;
    #endregion

    #region Constants

    #region Session Names
    private const string SESSIONTEACH = "TEACHER";
    private const string SESSIONTEACHS = "TEACHERS";
    private const string SESSIONDEPTS = "DEPTS";
    private const string SESSIONUSERID = "USERID";
    #endregion

    #endregion

    #region Functions
    private void ClearForm()
    {
        this.txtCode.Text = "";
        this.txtFirstName.Text = "";
        this.txtMiddleName.Text = "";
        this.txtLastName.Text = "";
        this.txtOtherName.Text = "";
        this.ddlGender.SelectedIndex = 0;
        this.ddlDept.SelectedIndex = 0;
        this.ddlUserId.SelectedIndex = 0;
        this.ddlPrefix.SelectedIndex = 0;
        this.clrDOB.SelectedDate = DateTime.Today;
        this.clrDOB.VisibleDate = DateTime.Today;
    }

    private void FillDepCombo()
    {
        ddlDept.Items.Clear();

        _departments = Department.GetDepts();

        if (_departments != null)
        {
            foreach (Department dept in _departments)
            {
                ListItem item = new ListItem();
                item.Value = dept.Id.ToString();
                item.Text = dept.Name;
                ddlDept.Items.Add(item);
            }

            if (Session[SESSIONDEPTS] != null)
            {
                Session.Remove(SESSIONDEPTS);
            }
            Session.Add(SESSIONDEPTS, _departments);

            ddlDept.SelectedIndex = 0;
        }
    }
    private void FillGenderCombo()
    {
        ddlGender.Items.Clear();
        foreach (string item in Enum.GetNames(typeof(Gender)))
        {
            ddlGender.Items.Add(item);
        }
        ddlGender.SelectedIndex = 0;
    }
    private void FillPrefixCombo()
    {
        ddlPrefix.Items.Clear();

        foreach (string item in Enum.GetNames(typeof(Prefix)))
        {
            ddlPrefix.Items.Add(item);
        }
        ddlPrefix.SelectedIndex = 0;
    }
    private void FillUserIdCombo()
    {
        ddlUserId.Items.Clear();

        _users = UIUMSUser.GetUserIdAndLoginId();

        if (_users != null)
        {
            foreach (UIUMSUser user in _users)
            {
                ListItem item = new ListItem();
                item.Value = user.Id.ToString();
                item.Text = user.LogInID;
                ddlUserId.Items.Add(item);
            }

            if (Session[SESSIONUSERID] != null)
            {
                Session.Remove(SESSIONUSERID);
            }
            Session.Add(SESSIONUSERID, _departments);

            ddlUserId.SelectedIndex = 0;
        }
    }
    private void FillList()
    {
        gvwCollection.DataSource = null;
        gvwCollection.DataBind();

        if (txtSrch.Text.Trim().Length > 0)
        {
            _teachers = Teacher.Gets(txtSrch.Text.Trim());
        }
        else
        {
            _teachers = Teacher.Gets();
        }

        if (_teachers == null)
        {
            gvwCollection.DataSource = null;
            gvwCollection.DataBind();
            Utilities.ShowMassage(lblMsg, Color.Blue, "No Teachers Found.");

            DisableButtons();
            return;
        }

        gvwCollection.DataSource = _teachers;
        gvwCollection.DataKeyNames = _dataKey;
        gvwCollection.DataBind();

        DisableButtons();
    }

    private void FillList(string code)
    {
        _teacher = Teacher.Get(code);
        _teachers = new List<Teacher>();
        _teachers.Add(_teacher);

        if (_teachers == null)
        {

            DisableButtons();
            return;
        }

        gvwCollection.DataSource = _teachers;
        gvwCollection.DataKeyNames = _dataKey;
        gvwCollection.DataBind();

        DisableButtons();
    }

    private void DisableButtons()
    {
        if (gvwCollection.Rows.Count <= 0)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }
        else
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }
    }
    private void DisableButtons(bool enable)
    {
        btnEdit.Enabled = enable;
        btnDelete.Enabled = enable;
    }
    private void DisableCollection(bool enable)
    {
        pnlCollection.Enabled = enable;
        gvwCollection.Enabled = enable;
    }
    private void DisableCourse(bool enable)
    {
        pnlClass.Enabled = enable;
    }
    private void RefreshValue()
    {
        _teacher = new Teacher();
        _teacher = (Teacher)Session[SESSIONTEACH];

        this.txtCode.Text = _teacher.Code;
        //Close By Sajib
        //this.txtFirstName.Text = _teacher.FirstName;
        //this.txtMiddleName.Text = _teacher.MiddleName;
        //this.txtLastName.Text = _teacher.LastName;
        //this.txtOtherName.Text = _teacher.NickOrOtherName;
        //Close By Sajib
        this.ddlDept.SelectedValue = _teacher.DeptID.ToString();        
        this.ddlDept.ToolTip = ddlDept.SelectedItem.Text;
        //Close By Sajib
        //this.ddlUserId.SelectedValue = _teacher.UserID.ToString();
        //this.ddlGender.SelectedIndex = ((int)_teacher.Gender) - 1;
        //this.ddlPrefix.SelectedIndex = ((int)_teacher.Prefix) - 1;
        //this.clrDOB.SelectedDate = _teacher.DateOfBirth;
        //this.clrDOB.VisibleDate = _teacher.DateOfBirth;
        //Close By Sajib
    }
    private void RefreshObject()
    {
        _teacher = null;
        if (Session[SESSIONTEACH] == null)
        {
            _teacher = new Teacher();
        }
        else
        {
            _teacher = (Teacher)Session[SESSIONTEACH];
        }

        _teacher.Code = this.txtCode.Text.Trim();
        //Close By Sajib
        //_teacher.FirstName = this.txtFirstName.Text.Trim();
        //_teacher.MiddleName = this.txtMiddleName.Text.Trim();
        //_teacher.LastName = this.txtLastName.Text.Trim();
        //_teacher.NickOrOtherName = this.txtOtherName.Text.Trim();
        //Close By Sajib
        _teacher.DeptID = Int32.Parse(ddlDept.SelectedValue);
        //Close By Sajib
        //_teacher.UserID = Int32.Parse(ddlUserId.SelectedValue);
        //_teacher.Prefix = (Prefix)Enum.Parse(typeof(Prefix), ddlPrefix.SelectedItem.Text);
        //_teacher.Gender = (Gender)Enum.Parse(typeof(Gender), ddlGender.SelectedItem.Text);
        //Close By Sajib

        if (this.clrDOB.SelectedDate != null && this.clrDOB.SelectedDate != DateTime.MinValue)
        {
            //Close By Sajib
            //_teacher.DateOfBirth = this.clrDOB.SelectedDate;
            //Close By Sajib
        }
        else
        {
            throw new Exception("Date of birth can not be empty.");
        }
    }
    #endregion

    #region Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Page.Request.ServerVariables["http_user_agent"].ToLower().Contains("safari"))
        {
            Page.ClientTarget = "uplevel";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            base.CheckPage_Load();
            if (!IsPostBack)
            {
                FillDepCombo();
                FillPrefixCombo();
                FillGenderCombo();
                DisableButtons();
                txtSrch.Focus();
                FillUserIdCombo();
            }
            btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete?');");
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            FillList();
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            if (Session[SESSIONTEACH] != null)
            {
                Session.Remove(SESSIONTEACH);
            }
            DisableCollection(false);
            DisableCourse(true);
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            if (gvwCollection.SelectedRow == null)
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "Before trying to edit an item, you must select the desired Item.");
                return;
            }

            _teacher = new Teacher();
            _teacher = Teacher.Get(Convert.ToInt32(gvwCollection.SelectedValue));

            if (Session[SESSIONTEACH] != null)
            {
                Session.Remove(SESSIONTEACH);
            }
            Session.Add(SESSIONTEACH, _teacher);

            DisableCollection(false);
            DisableCourse(true);
            DisableButtons(false);

            RefreshValue();
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            if (gvwCollection.SelectedRow == null)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "Before deleting an item, you must select the Item.");
                return;
            }
            Teacher.Delete(Convert.ToInt32(gvwCollection.SelectedValue));
            FillList();
            Utilities.ShowMassage(lblMsg, Color.Blue, "Program information successfully deleted");
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 547)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "This Program has been referenced in other tables, please delete those references first.");
            }
            else
            {
                Utilities.ShowMassage(lblMsg, Color.Red, SqlEx.Message);
            }
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            RefreshObject();

            if (Teacher.HasDuplicateCode(_teacher))
            {
                throw new Exception(Message.DUPLICATECODENOTALLOWED);
            }

            bool isNewObj = true;
            if (_teacher.Id == 0)
            {
                _teacher.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _teacher.CreatedDate = DateTime.Now;
            }
            else
            {
                isNewObj = false;
                _teacher.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _teacher.ModifiedDate = DateTime.Now;
            }

            Teacher.Save(_teacher);

            if (isNewObj)
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, Message.SUCCESSFULLYSAVED);
                ClearForm();
            }
            else
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, Message.SUCCESSFULLYUPDATED);
                ClearForm();
                DisableCollection(true);
                DisableCourse(false);
                DisableButtons();
                txtSrch.Focus();
            }

            FillList(_teacher.Code);

            if (Session[SESSIONTEACH] != null)
            {
                Session.Remove(SESSIONTEACH);
            }
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 2627)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, Message.DUPLICATECODENOTALLOWED);
            }
            else
            {
                Utilities.ShowMassage(lblMsg, Color.Red, SqlEx.Message);
            }
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            ClearForm();
            DisableCollection(true);
            DisableCourse(false);
            DisableButtons();
            txtSrch.Focus();
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    #endregion
}
