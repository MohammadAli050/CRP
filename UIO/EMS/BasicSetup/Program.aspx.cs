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

public partial class BasicSetup_Program : BasePage
{
    #region Variable Declaration
    List<Program> _programs = null;
    Program _program = null;
    private string[] _dataKey = new string[1] { "Id" };
    List<Department> _departments = null;
    List<ProgramType> _types = null;
    #endregion

    #region Constants

    #region Session Names
    private const string SESSIONPROG = "PROGRAM";
    private const string SESSIONPROGS = "PROGRAMS";
    private const string SESSIONTYPES = "TYPES";
    private const string SESSIONDEPTS = "DEPTS";
    #endregion

    #endregion

    #region Functions
    private void ClearForm()
    {
        this.txtName.Text = "";
        this.txtCode.Text = "";
        this.ddlType.SelectedIndex = 0;
        this.ddlParent.SelectedIndex = 0;
        this.txtCredits.Text = string.Empty;
        this.txtDetailName.Text = string.Empty;
    }

    private void FillDepCombo()
    {
        ddlParent.Items.Clear();

        _departments = Department.GetDepts();

        if (_departments != null)
        {
            foreach (Department dept in _departments)
            {
                ListItem item = new ListItem();
                item.Value = dept.Id.ToString();
                item.Text = dept.Name;
                ddlParent.Items.Add(item);
            }

            if (Session[SESSIONDEPTS] != null)
            {
                Session.Remove(SESSIONDEPTS);
            }
            Session.Add(SESSIONDEPTS, _departments);

            ddlParent.SelectedIndex = 0;
        }
    }
    private void FillTypeCombo()
    {
        ddlType.Items.Clear();

        _types = ProgramType.GetProgramTypes();

        if (_types != null)
        {
            foreach (ProgramType type in _types)
            {
                ListItem item = new ListItem();
                item.Value = type.ProgramTypeID.ToString();
                item.Text = type.TypeDescription;
                ddlType.Items.Add(item);
            }

            if (Session[SESSIONTYPES] != null)
            {
                Session.Remove(SESSIONTYPES);
            }
            Session.Add(SESSIONTYPES, _types);

            ddlType.SelectedIndex = 0;
        }
    }
    private void FillList()
    {
        gvwCollection.DataSource = null;
        gvwCollection.DataBind();

        if (txtSrch.Text.Trim().Length > 0)
        {
            _programs = Program.GetPrograms(txtSrch.Text.Trim());
        }
        else
        {
            _programs = Program.GetPrograms();
        }

        if (_programs == null)
        {
            gvwCollection.DataSource = null;
            gvwCollection.DataBind();
            Utilities.ShowMassage(lblMsg, Color.Blue, "No Programs Found.");

            DisableButtons();
            return;
        }

        gvwCollection.DataSource = _programs;
        gvwCollection.DataKeyNames = _dataKey;
        gvwCollection.DataBind();

        DisableButtons();
    }

    private void FillList(string code)
    {
        _program = Program.GetProgram(code);
        _programs = new List<Program>();
        _programs.Add(_program);

        if (_programs == null)
        {

            DisableButtons();
            return;
        }

        gvwCollection.DataSource = _programs;
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
        _program = new Program();
        _program = (Program)Session[SESSIONPROG];
        this.txtName.Text = _program.ShortName;
        this.txtCode.Text = _program.Code;
        this.txtDetailName.Text = _program.DetailName;
        if (_program.TotalCredit > 0)
        {
            this.txtCredits.Text = Math.Round(_program.TotalCredit, 2).ToString();
        }
        this.ddlParent.SelectedValue = _program.DeptID.ToString();
        this.ddlParent.ToolTip = ddlParent.SelectedItem.Text;
        this.ddlType.SelectedValue = _program.ProgramTypeID.ToString();
        this.ddlType.ToolTip = ddlType.SelectedItem.Text;
    }
    private void RefreshObject()
    {
        _program = null;
        if (Session[SESSIONPROG] == null)
        {
            _program = new Program();
        }
        else
        {
            _program = (Program)Session[SESSIONPROG];
        }

        _program.ShortName = txtName.Text.Trim();
        _program.Code = txtCode.Text.Trim();
        _program.DetailName = txtName.Text.Trim();

        if (txtCredits.Text.Trim().Length > 0)
        {
            _program.TotalCredit = Math.Round(Decimal.Parse(txtCredits.Text.Trim()), 2);
        }

        _program.DeptID = Int32.Parse(ddlParent.SelectedValue);
        _program.ProgramTypeID = Int32.Parse(ddlType.SelectedValue);
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
                FillTypeCombo();
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

            if (Session[SESSIONPROG] != null)
            {
                Session.Remove(SESSIONPROG);
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

            _program = new Program();
            _program = Program.GetProgram(Convert.ToInt32(gvwCollection.SelectedValue));

            if (Session[SESSIONPROG] != null)
            {
                Session.Remove(SESSIONPROG);
            }
            Session.Add(SESSIONPROG, _program);

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
            Program.Delete(Convert.ToInt32(gvwCollection.SelectedValue));
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

            if (txtCredits.Text.Trim().Length > 0)
            {
                double totCredits = 0;
                if (!Double.TryParse(txtCredits.Text.Trim(), out totCredits))
                {
                    Utilities.ShowMassage(lblMsg, Color.Red, "Credits can only be number.");
                    txtCredits.Focus();
                    return;
                }
            }


            RefreshObject();

            if (Program.HasDuplicateCode(_program))
            {
                throw new Exception("Duplicate code are not allowed.");
            }

            bool isNewProg = true;
            if (_program.Id == 0)
            {
                _program.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _program.CreatedDate = DateTime.Now;
            }
            else
            {
                isNewProg = false;
                _program.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _program.ModifiedDate = DateTime.Now;
            }

            Program.Save(_program);

            if (isNewProg)
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "Program information successfully saved");
                ClearForm();
            }
            else
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "Program information successfully updated");
                ClearForm();
                DisableCollection(true);
                DisableCourse(false);
                DisableButtons();
                txtSrch.Focus();
            }

            FillList(_program.Code);

            if (Session[SESSIONPROG] != null)
            {
                Session.Remove(SESSIONPROG);
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
    #endregion
}
