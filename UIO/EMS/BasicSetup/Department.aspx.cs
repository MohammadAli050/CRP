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

public partial class BasicSetup_Department : BasePage
{
    #region Variable Declaration
    List<Department> _depts = null;
    List<School> _schools = null;
    Department _dept = null;
    private string[] _dataKey = new string[1] { "Id" };
    #endregion

    #region Constants

    #region Session Names
    private const string SESSIONDEPT = "DEPARTMENT";
    private const string SESSIONDEPTS = "DEPARTMENTS";
    private const string SESSIONSCHOOLS = "SCHOOLS";
    #endregion

    #endregion

    #region Functions
    private void ClearForm()
    {
        this.txtName.Text = "";
        this.txtCode.Text = "";
        this.txtDetailName.Text = string.Empty;
        this.chkIsOpen.Checked = false;
        this.chkIsClosed.Checked = false;
        //this.ddlParent.SelectedIndex = 0;
        this.clrEndDate.SelectedDate = DateTime.Today;
        this.clrEndDate.VisibleDate = DateTime.Today;
        this.clrStartDate.SelectedDate = DateTime.Today;
        this.clrStartDate.VisibleDate = DateTime.Today;
    }
    private void FillSchCombo()
    {
        //ddlParent.Items.Clear();
        //List<Department> 
        //_depts = Department.GetDepts();

        //if (_depts != null)
        //{
         //   foreach (Department department in _depts)
         //   {
        //        ListItem item = new ListItem();
        //        item.Value = department.Id.ToString();
        //        item.Text = department.Name;
          //      ddlParent.Items.Add(item);
       //     }

        //    if (Session[SESSIONSCHOOLS] != null)
        //    {
        //        Session.Remove(SESSIONSCHOOLS);
        //    }
       //     Session.Add(SESSIONSCHOOLS, _schools);

       //     ddlParent.SelectedIndex = 0;
      //  }
    }

    private void FillList()
    {
        if (txtSrch.Text.Trim().Length > 0)
        {
            _depts = Department.GetDepts(txtSrch.Text.Trim());
        }
        else
        {
            _depts = Department.GetDepts();
        }

        if (_depts == null)
        {
            gvwCollection.DataSource = null;
            gvwCollection.DataBind();
            Utilities.ShowMassage(lblMsg, Color.Blue, "No Department Found.");

            DisableButtons();
            return;
        }

        gvwCollection.DataSource = _depts;
        gvwCollection.DataKeyNames = _dataKey;
        gvwCollection.DataBind();

        DisableButtons();
    }

    private void FillList(string code)
    {
        _dept = Department.GetDept(code);
        _depts = new List<Department>();
        _depts.Add(_dept);

        if (_depts == null)
        {

            DisableButtons();
            return;
        }

        gvwCollection.DataSource = _depts;
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
        _dept = new Department();
        _dept = (Department)Session[SESSIONDEPT];
        this.txtName.Text = _dept.Name;
        this.txtCode.Text = _dept.Code;
        this.txtDetailName.Text = _dept.DetailedName;
        //this.ddlParent.SelectedValue = _dept.Id.ToString();
        //this.ddlParent.ToolTip = ddlParent.SelectedItem.Text;

        if (_dept.OpeningDate != DateTime.MinValue)
        {
            this.chkIsOpen.Checked = true;
            this.clrStartDate.Enabled = true;
            this.clrStartDate.SelectedDate = _dept.OpeningDate;
            this.clrStartDate.VisibleDate = _dept.OpeningDate;
        }

        if (_dept.ClosingDate != DateTime.MinValue)
        {
            this.chkIsClosed.Checked = true;
            this.clrEndDate.Enabled = true;
            this.clrEndDate.SelectedDate = _dept.ClosingDate;
            this.clrEndDate.VisibleDate = _dept.ClosingDate;
        }
    }
    private void RefreshObject()
    {
        _dept = null;
        if (Session[SESSIONDEPT] == null)
        {
            _dept = new Department();
        }
        else
        {
            _dept = (Department)Session[SESSIONDEPT];
        }

        _dept.Name = txtName.Text.Trim();
        _dept.Code = txtCode.Text.Trim();
        _dept.DetailedName = txtDetailName.Text.Trim();
        //_dept.SchoolID = Int32.Parse(ddlParent.SelectedValue);
        _dept.OpeningDate = this.clrStartDate.SelectedDate;

        if (chkIsClosed.Checked)
        {
            _dept.ClosingDate = this.clrEndDate.SelectedDate;
        }
        else
        {
            _dept.ClosingDate = DateTime.MinValue;
        }

        if (chkIsOpen.Checked)
        {
            _dept.OpeningDate = this.clrStartDate.SelectedDate;
        }
        else
        {
            _dept.OpeningDate = DateTime.MinValue;
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
            #region tmp
            //UIUMSUser.CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            //if (UIUMSUser.CurrentUser != null)
            //{
            //    if (UIUMSUser.CurrentUser.RoleID > 0)
            //    {
            //        Authenticate(UIUMSUser.CurrentUser);
            //    }
            //}
            //else
            //{
            //    Response.Redirect("~/Security/Login.aspx");
            //} 
            #endregion

            base.CheckPage_Load();

            if (!IsPostBack)
            {
                FillSchCombo();
                DisableButtons();
                txtSrch.Focus();
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

            if (Session[SESSIONDEPT] != null)
            {
                Session.Remove(SESSIONDEPT);
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

            _dept = new Department();
            _dept = Department.GetDept(Convert.ToInt32(gvwCollection.SelectedValue));

            if (Session[SESSIONDEPT] != null)
            {
                Session.Remove(SESSIONDEPT);
            }
            Session.Add(SESSIONDEPT, _dept);

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
            Department.Delete(Convert.ToInt32(gvwCollection.SelectedValue));
            FillList();
            Utilities.ShowMassage(lblMsg, Color.Blue, "Department information successfully deleted");
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 547)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "This Department has been referenced in other tables, please delete those references first.");
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

            if (Department.HasDuplicateCode(_dept))
            {
                throw new Exception("Duplicate code are not allowed.");
            }

            bool isNewDept = true;
            if (_dept.Id == 0)
            {
                _dept.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _dept.CreatedDate = DateTime.Now;
            }
            else
            {
                isNewDept = false;
                _dept.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _dept.ModifiedDate = DateTime.Now;
            }

            Department.Save(_dept);

            if (isNewDept)
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "Department information successfully saved");
                ClearForm();
            }
            else
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "Department information successfully updated");
                ClearForm();
                DisableCollection(true);
                DisableCourse(false);
                DisableButtons();
                txtSrch.Focus();
            }

            FillList(_dept.Code);

            if (Session[SESSIONDEPT] != null)
            {
                Session.Remove(SESSIONDEPT);
            }
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 2627)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "Duplicate codes are not allowed");
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

    protected void chkIsClosed_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkIsClosed.Checked)
            {
                clrEndDate.Enabled = true;
            }
            else
            {
                clrEndDate.Enabled = false;
            }
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    protected void chkIsOpen_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkIsOpen.Checked)
            {
                clrStartDate.Enabled = true;
            }
            else
            {
                clrStartDate.Enabled = false;
            }
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    #endregion

    protected void txtDetailName_TextChanged(object sender, EventArgs e)
    {

    }
}
