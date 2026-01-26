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

public partial class Admin_Role : BasePage
{
    #region Variable Declaration
    List<Role> _roles = null;
    Role _role = null;
    private string[] _dataKey = new string[1] { "Id" };
    #endregion

    #region Constants

    #region Session Names
    private const string SESSIONROLE = "role";
    #endregion

    #endregion

    #region Functions
    private void LoadTree(TreeNode parentNode, List<UIUMSMenu> menus)
    {
        if (menus == null)
        {
            menus = new List<UIUMSMenu>();
            menus = UIUMSMenu.GetRoots();
            tvwMenu.Nodes.Clear();
        }

        foreach (UIUMSMenu menu in menus)
        {
            TreeNode node = new TreeNode();
            node.Text = menu.Name;
            node.Value = menu.Id.ToString();

            if (base.IsSessionVariableExists(SESSIONROLE))
            {
                if (gdvRoles.SelectedRow != null)
                {
                    //DataKey dtkey = gdvRoles.SelectedPersistedDataKey;
                    DataKey dtkey = gdvRoles.SelectedDataKey;//@Sajib

                    IOrderedDictionary odict = dtkey.Values;

                    Role role = Role.Get(Convert.ToInt32(odict[0]));
                    node.Checked = Role_Menu.HasPermission(role.Id, menu.Id);
                }
            }

            node.NavigateUrl = "javascript:void(0)";
            node.ExpandAll();
            if (parentNode == null)
            {
                //Root Node
                tvwMenu.Nodes.Add(node);
            }
            else
            {
                //Child Node
                parentNode.ChildNodes.Add(node);
            }
            //Check running menu has child menus
            if (menu.ChildMenus != null && menu.ChildMenus.Count > 0)
            {
                //If has, than recall the void again
                LoadTree(node, menu.ChildMenus);
            }
        }
    }
    private void GetChildNodePermission(TreeNode parentNode, Role role)
    {
        foreach (TreeNode node in parentNode.ChildNodes)
        {
            if (node.Checked)
            {
                Role_Menu roleMenu = new Role_Menu();
                roleMenu.MenuID = Convert.ToInt32(node.Value);
                roleMenu.RoleID = Convert.ToInt32(role.Id);

                role.RoleMenus.Add(roleMenu);
                if (node.ChildNodes.Count > 0)
                {
                    GetChildNodePermission(node, role);
                }
            }
        }
    }
    private void CheckedParent(TreeNode treeNode, bool checkValue)
    {
        if (treeNode.Parent != null)
        {
            if (checkValue)
            {
                treeNode.Parent.Checked = checkValue;
            }
            else
            {
                if (IsCheckedAnyChilds(treeNode.Parent) == false)
                {
                    treeNode.Parent.Checked = checkValue;
                }
            }
            CheckedParent(treeNode.Parent, checkValue);
        }
    }
    private void CheckedChilds(TreeNode treeNode, bool checkValue)
    {
        foreach (TreeNode node in treeNode.ChildNodes)
        {
            node.Checked = checkValue;
            CheckedChilds(node, node.Checked);
        }
    }
    private bool IsCheckedAnyChilds(TreeNode treeNode)
    {
        foreach (TreeNode node in treeNode.ChildNodes)
        {
            if (node.Checked)
            {
                return true;
            }
        }
        return false;
    }
    private void ClearForm()
    {
        base.RemoveFromSession(SESSIONROLE);
        this.txtRole.Text = "";
        this.txtSessionTime.Text = "";
        this.tvwMenu.Nodes.Clear();
        LoadTree(null, null);
    }
    private void FillList()
    {
        List<Role> roles = null;
        if (txtSearchParam.Text.Trim().Length > 0)
        {
            roles = Role.GetByRoleName(txtSearchParam.Text.Trim());

            if (roles == null)
            {
                roles = Role.Get();
            }
        }
        else
        {
            roles = Role.Get();
        }

        if (Session["roles"] != null)
        {
            Session.Remove("roles");
        }

        if (roles != null)
        {
            Session.Add("roles", roles);
            gdvRoles.DataSource = roles;
            gdvRoles.DataKeyNames = _dataKey;

            gdvRoles.DataBind();

            //DisableButtons();

            //if (_roles.Count <= 0)
            //{
            //    gdvRoles.DataSource = null;

            //    gdvRoles.DataBind();
            //    Utilities.ShowMassage(lblErr, Color.Red, "No records found");
            //}
        }
        else
        {
            gdvRoles.DataSource = null;

            gdvRoles.DataBind();
            Utilities.ShowMassage(lblErr, Color.Red, "No records found");
        }
    }
    private void FillList(int roleID)
    {
        _roles = new List<Role>();
        _roles.Add(BussinessObject.Role.Get(roleID));

        if (Session["roles"] != null)
        {
            Session.Remove("roles");
        }
        Session.Add("roles", _roles);

        if (_roles != null || _roles.Count > 0)
        {
            gdvRoles.DataSource = _roles;
            gdvRoles.DataKeyNames = _dataKey;

            gdvRoles.DataBind();

            //DisableButtons();

            //if (_roles.Count <= 0)
            //{
            //    gdvRoles.DataSource = null;

            //    gdvRoles.DataBind();
            //    Utilities.ShowMassage(lblErr, Color.Red, "No records found");
            //}
        }
        else
        {
            gdvRoles.DataSource = null;

            gdvRoles.DataBind();
            Utilities.ShowMassage(lblErr, Color.Red, "No records found");
        }
    }
    private void MasterDetailEnabler(bool enbaleMaster, bool enableDetail)
    {
        pnlDetails.Enabled = enableDetail;
        pnlMaster.Enabled = enbaleMaster;
    }
    private void RefreshValue()
    {

        _role = (Role)GetFromSession(SESSIONROLE);

        if (_role == null)
        {
            _role = new Role();
        }

        this.txtRole.Text = _role.RoleName;
        this.txtSessionTime.Text = (_role.SessionTime != 0) ? _role.SessionTime.ToString() : string.Empty;//_role.Password.ToString();

        LoadTree(null, null);

        this.txtRole.Focus();
    }
    private Role RefreshObject()
    {
        Role role = null;
        if (!IsSessionVariableExists(SESSIONROLE))
        {
            role = new Role();
        }
        else
        {
            role = (Role)GetFromSession(SESSIONROLE);
        }

        role.RoleName = txtRole.Text.Trim();
        role.SessionTime = (txtSessionTime.Text != string.Empty) ? Int32.Parse(txtSessionTime.Text.Trim()) : 0;

        if (role.RoleMenus != null)
        {
            role.RoleMenus.Clear();
        }
        else
        {
            role.RoleMenus = new List<Role_Menu>();
        }

        foreach (TreeNode node in tvwMenu.Nodes)
        {
            if (node.Checked)
            {
                Role_Menu roleMenu = new Role_Menu();
                roleMenu.MenuID = Convert.ToInt32(node.Value);
                roleMenu.RoleID = Convert.ToInt32(role.Id);

                role.RoleMenus.Add(roleMenu);
                if (node.Parent == null)
                {
                    GetChildNodePermission(node, role);
                }
            }
        }
        return role;
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

            if (!IsPostBack)
            {
                MasterDetailEnabler(true, false);
            }
            btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete?');");
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblErr, Color.Red, Ex.Message);
        }
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        try
        {
            FillList();
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblErr, Color.Red, Ex.Message);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsSessionVariableExists(SESSIONROLE))
            {
                RemoveFromSession(SESSIONROLE);
            }

            MasterDetailEnabler(false, true);
            RefreshValue();
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblErr, Color.Red, Ex.Message);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            if (gdvRoles.SelectedRow == null)
            {
                Utilities.ShowMassage(lblErr, Color.Red, "Before trying to edit an item, you must select the desired Item.");
                return;
            }
            _role = new Role();

            //DataKey dtkey = gdvRoles.SelectedPersistedDataKey;
            DataKey dtkey = gdvRoles.SelectedDataKey;//@Sajib

            IOrderedDictionary odict = dtkey.Values;
            _role = Role.Get(Convert.ToInt32(odict[0]));

            if (IsSessionVariableExists(SESSIONROLE))
            {
                RemoveFromSession(SESSIONROLE);
            }
            AddToSession(SESSIONROLE, _role);
            MasterDetailEnabler(false, true);
            RefreshValue();
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblErr, Color.Red, Ex.Message);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (gdvRoles.SelectedRow == null)
            {
                Utilities.ShowMassage(lblErr, Color.Red, "Before trying to delete an item, you must select the desired Item.");
                return;
            }
            _role = new Role();

            DataKey dtkey = gdvRoles.SelectedDataKey;

            IOrderedDictionary odict = dtkey.Values;
            Role.Delete(Convert.ToInt32(odict[0]));

            Utilities.ShowMassage(lblErr, Color.Blue, "role information successfully deleted");
            FillList();
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 547)
            {
                Utilities.ShowMassage(lblErr, Color.Red, "This has been referenced in other tables, please delete those references first.");
            }
            else
            {
                Utilities.ShowMassage(lblErr, Color.Red, SqlEx.Message);
            }
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblErr, Color.Red, Ex.Message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string oldCode = string.Empty;
            if (txtRole.Text.Trim() == string.Empty)
            {
                txtRole.Focus();
                return;
            }

            _role = null;
            _role = RefreshObject();

            bool isNewrole = true;
            if (_role.Id == 0)
            {
                _role.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _role.CreatedDate = DateTime.Now;
            }
            else
            {
                isNewrole = false;
                _role.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _role.ModifiedDate = DateTime.Now;
            }

            //if (Role.HasDuplicateLogInID(_role))
            //{
            //    throw new Exception("Duplicate logInIDs are not allowed.");
            //}

            Role.Save(_role);

            FillList(_role.Id);

            if (isNewrole)
            {
                Utilities.ShowMassage(lblErr, Color.Blue, "Role information successfully saved");
                ClearForm();
                this.txtRole.Focus();
            }
            else
            {
                Utilities.ShowMassage(lblErr, Color.Blue, "Role information successfully updated");
                ClearForm();
                MasterDetailEnabler(false, true);
                this.txtRole.Focus();
            }

            if (Session["Role"] != null)
            {
                Session.Remove("Role");
            }
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 2627)
            {
                Utilities.ShowMassage(lblErr, Color.Red, "Duplicate LogInIDs are not allowed");
            }
            else
            {
                Utilities.ShowMassage(lblErr, Color.Red, SqlEx.Message);
            }
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblErr, Color.Red, Ex.Message);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsSessionVariableExists(SESSIONROLE))
            {
                RemoveFromSession(SESSIONROLE);
            }
            ClearForm();
            MasterDetailEnabler(true, false);
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblErr, Color.Red, Ex.Message);
        }
    }
    protected void tvwMenu_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        try
        {
            CheckedParent(e.Node, e.Node.Checked);
            CheckedChilds(e.Node, e.Node.Checked);
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblErr, Color.Red, Ex.Message);
        }
    }
    protected void btnCopy_Click(object sender, EventArgs e)
    {
        btnEdit_Click(null, null);
        txtRole.Text = string.Empty;
        if (IsSessionVariableExists(SESSIONROLE))
        {
            RemoveFromSession(SESSIONROLE);
        }
    }
    #endregion
}
