using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BussinessObject;
using Common;
using System.Drawing;
using System.Data.SqlClient;


public partial class BasicSetup_TypeDefinition : BasePage
{
    #region Variable Declaration
    TypeDefinition _typeDef = null;
    List<TypeDefinition> _typeDefs = null;
    private List<AccountsHead> _accountsHead = null;
    private string[] _dataKey = new string[1] { "ID" };
    #endregion

    #region Session Names
    private const string SESSIONTYPEDEF = "TYPEDEFINITION";
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
                FillAccCombo();
                DisableButtons();
                txtSrch.Focus();
                ClearSession();
            }
            btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete?');");
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }

    private void ClearSession()
    {
        RemoveFromSession(SESSIONTYPEDEF);
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

            if (Session[SESSIONTYPEDEF] != null)
            {
                Session.Remove(SESSIONTYPEDEF);
            }

            DisableCollection(false);
            DisablePnlTypeDef(true);
            FillAccCombo();
            ResetDdl();


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

            _typeDef = new TypeDefinition();
            _typeDef = TypeDefinition.GetTypeDef(Convert.ToInt32(gvwCollection.SelectedValue));

            if (Session[SESSIONTYPEDEF] != null)
            {
                Session.Remove(SESSIONTYPEDEF);
            }
            Session.Add(SESSIONTYPEDEF, _typeDef);

            DisableCollection(false);
            DisablePnlTypeDef(true);
            DisableButtons(false);
            ResetDdl();
            FillAccCombo();

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
            TypeDefinition.Delete(Convert.ToInt32(gvwCollection.SelectedValue));
            FillList();
            Utilities.ShowMassage(lblMsg, Color.Blue, "Type defination information successfully deleted");
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 547)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "This type defination has been referenced in other tables, please delete those references first.");
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

            if (TypeDefinition.HasDuplicateCode(_typeDef))
            {
                throw new Exception("Duplicate defination are not allowed.");
            }

            bool isNewDept = true;
            if (_typeDef.Id == 0)
            {
                if (_typeDef.Type == "Fee_PCA")// manual checking for save
                {
                    if (TypeDefinition.IsExist(_typeDef.Type))
                    {
                        throw new Exception("Fee_PCA allowed only once.");
                    }
                }

                _typeDef.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _typeDef.CreatedDate = DateTime.Now;
            }
            else
            {
                isNewDept = false;
                _typeDef.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _typeDef.ModifiedDate = DateTime.Now;
            }

            TypeDefinition.Save(_typeDef);

            if (isNewDept)
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "Discount Type information successfully saved");
                ClearForm();
            }
            else
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "Discount Type information successfully updated");
                ClearForm();
                DisableCollection(true);
                DisablePnlTypeDef(false);
                DisableButtons();
                txtSrch.Focus();
            }

            FillList(_typeDef.Type, _typeDef.Definition);

            if (Session[SESSIONTYPEDEF] != null)
            {
                Session.Remove(SESSIONTYPEDEF);
            }
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 2627)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "Duplicate defination are not allowed");
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
            ClearSession();
            DisableCollection(true);
            DisablePnlTypeDef(false);
            DisableButtons();
            txtSrch.Focus();
            ResetDdl();
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }

    #endregion

    #region Function
    private void FillAccCombo()
    {
        ddlAccHead.Items.Clear();

        _accountsHead = AccountsHead.GetLeafAccounts();

        if (_accountsHead != null)
        {
            foreach (AccountsHead accountsHead in _accountsHead)
            {
                ListItem item = new ListItem();
                item.Value = accountsHead.Id.ToString();
                item.Text = accountsHead.Name;

                ddlAccHead.Items.Add(item);
            }

            ddlAccHead.SelectedIndex = 0;
        }
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

    private void ResetDdl()
    {
        this.ddlType.SelectedIndex = 0;
        this.ddlAccHead.SelectedIndex = 0;
    }

    private void DisablePnlTypeDef(bool enable)
    {
        pnlTypeDefinition.Enabled = enable;
    }

    private void DisableCollection(bool enable)
    {
        pnlCollection.Enabled = enable;
        gvwCollection.Enabled = enable;
    }

    private void RefreshValue()
    {
        _typeDef = new TypeDefinition();
        _typeDef = (TypeDefinition)Session[SESSIONTYPEDEF];
        this.ddlType.Text = _typeDef.Type;
        this.txtDefinition.Text = _typeDef.Definition;
        this.txtPriority.Text = _typeDef.Priority.ToString();

        if (_typeDef.Type == "Fee_PCA" && _typeDef.Definition != "")// manual checking for edit
        {
            txtDefinition.Enabled = false;
        }

    }

    private void FillList()
    {
        if (txtSrch.Text.Trim().Length > 0)
        {
            _typeDefs = TypeDefinition.GetTypes(txtSrch.Text.Trim());
        }
        else
        {
            _typeDefs = TypeDefinition.GetTypes();
        }

        if (_typeDefs == null)
        {
            gvwCollection.DataSource = null;
            gvwCollection.DataBind();
            Utilities.ShowMassage(lblMsg, Color.Blue, "No type defination Found.");

            DisableButtons();
            return;
        }

        _typeDefs = _typeDefs.OrderBy(td => td.Type).ToList();
        gvwCollection.DataSource = _typeDefs;
        gvwCollection.DataKeyNames = _dataKey;
        gvwCollection.DataBind();

        DisableButtons();
    }

    private void FillList(string type, string defination)
    {
        _typeDefs = TypeDefinition.GetTypes(type, defination);
        
        if (_typeDefs == null)
        {
            gvwCollection.DataSource = null;
            gvwCollection.DataBind();
            Utilities.ShowMassage(lblMsg, Color.Blue, "No type defination Found.");

            DisableButtons();
            return;
        }

        gvwCollection.DataSource = _typeDefs;
        gvwCollection.DataKeyNames = _dataKey;
        gvwCollection.DataBind();

        DisableButtons();
    }

    private void RefreshObject()
    {
        _typeDef = null;

        if (Session[SESSIONTYPEDEF] == null)
        {
            _typeDef = new TypeDefinition();
        }
        else
        {
            _typeDef = (TypeDefinition)Session[SESSIONTYPEDEF];
        }

       

        _typeDef.Type = ddlType.SelectedItem.Text.Trim();
        _typeDef.Definition = txtDefinition.Text.Trim();
        _typeDef.AccountsID = Convert.ToInt32(ddlAccHead.SelectedValue);
        _typeDef.IsCourseSpecific = chkIsCourseSpecificBilling.Checked;
        _typeDef.IsLifetimeOnce = chkIsLifetimeOnceBilling.Checked;
        _typeDef.IsPerAcaCal = chkIsPerAcaCalBilling.Checked;       
        _typeDef.Priority = string.IsNullOrEmpty(txtPriority.Text.Trim()) ? 0 : Convert.ToInt32(txtPriority.Text.Trim());
    }

    private void ClearForm()
    {
        //this.txtDiscountType.Text = "";
        this.txtDefinition.Text = string.Empty;
        this.txtSrch.Text = string.Empty;
        this.txtPriority.Text = string.Empty;

        txtDefinition.Enabled = true;
    }

    #endregion

    protected void txtDefinition_TextChanged(object sender, EventArgs e)
    {

    }
}
