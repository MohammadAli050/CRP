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

public partial class BasicSetup_School : BasePage
{
    #region Variable Declaration
    List<School> _schools = null;
    School _school = null;
    private string[] _dataKey = new string[1] { "Id" };
    #endregion

    #region Constants

    #region Session Names
    private const string SESSIONSCHOOL = "SCHOOL";
    private const string SESSIONSCHOOLS = "SCHOOLS";
    #endregion

    #endregion

    #region Functions
    private void ClearForm()
    {
        this.txtName.Text = "";
        this.txtCode.Text = "";
    }

    private void FillList()
    {
        if (txtSrch.Text.Trim().Length > 0)
        {
            _schools = School.GetSchools(txtSrch.Text.Trim());
        }
        else
        {
            _schools = School.GetSchools();
        }

        if (_schools == null)
        {
            gvwCollection.DataSource = null;
            gvwCollection.DataBind();
            Utilities.ShowMassage(lblMsg, Color.Blue, "No School Found.");

            DisableButtons();
            return;
        }

        gvwCollection.DataSource = _schools;
        gvwCollection.DataKeyNames = _dataKey;
        gvwCollection.DataBind();

        DisableButtons();
    }

    private void FillList(string code)
    {
        _school = School.GetSchool(code);
        _schools = new List<School>();
        _schools.Add(_school);

        if (_schools == null)
        {

            DisableButtons();
            return;
        }

        gvwCollection.DataSource = _schools;
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
        _school = new School();
        _school = (School)Session[SESSIONSCHOOL];
        this.txtName.Text = _school.Name;
        this.txtCode.Text = _school.Code;
    }
    private void RefreshObject()
    {
        _school = null;
        if (Session[SESSIONSCHOOL] == null)
        {
            _school = new School();
        }
        else
        {
            _school = (School)Session[SESSIONSCHOOL];
        }

        _school.Name = txtName.Text.Trim();
        _school.Code = txtCode.Text.Trim();

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
            School.Delete(Convert.ToInt32(gvwCollection.SelectedValue));
            FillList();
            Utilities.ShowMassage(lblMsg, Color.Blue, "School information successfully deleted");
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 547)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "This School has been referenced in other tables, please delete those references first.");
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            if (Session[SESSIONSCHOOL] != null)
            {
                Session.Remove(SESSIONSCHOOL);
            }
            DisableCollection(false);
            DisableCourse(true);
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

            if (School.HasDuplicateCode(_school))
            {
                throw new Exception("Duplicate code are not allowed.");
            }

            bool isNewSchool = true;
            if (_school.Id == 0)
            {
                _school.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _school.CreatedDate = DateTime.Now;
            }
            else
            {
                isNewSchool = false;
                _school.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _school.ModifiedDate = DateTime.Now;
            }

            School.Save(_school);

            if (isNewSchool)
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "School information successfully saved");
                ClearForm();
            }
            else
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "School information successfully updated");
                ClearForm();
                DisableCollection(true);
                DisableCourse(false);
                DisableButtons();
                txtSrch.Focus();
            }

            FillList(_school.Code);

            if (Session[SESSIONSCHOOL] != null)
            {
                Session.Remove(SESSIONSCHOOL);
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

            _school = new School();
            _school = School.GetSchool(Convert.ToInt32(gvwCollection.SelectedValue));

            if (Session[SESSIONSCHOOL] != null)
            {
                Session.Remove(SESSIONSCHOOL);
            }
            Session.Add(SESSIONSCHOOL, _school);

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
    #endregion
}
