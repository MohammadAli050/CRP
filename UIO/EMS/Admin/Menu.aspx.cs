using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BussinessObject;
using Common;
using System.Drawing;

public partial class Admin_Menu : BasePage
{
    #region Declaration

    private int _id;
    private string[] _idAndTier = new string[2];
    #endregion

    #region Constants

    #region Session Names
    private const string SESSIONMENU = "Menu";
    private const string SESSIONSELECTEDVALUEPATH = "ValuePath";
    #endregion

    #endregion

    #region Methods
    #region Other Functions
    private void MasterDetailEnabler(bool enbaleMaster, bool enableDetail)
    {
        pnlDetail.Enabled = enableDetail;
        pnlMaster.Enabled = enbaleMaster;
    }
    private void SetDefault()
    {
        txtName.Text = "";
        txtURL.Text = "";
        chkSysAdmnAcs.Checked = false;
    }
    private void ClearMenuProperties()
    {
        base.RemoveFromSession(SESSIONMENU);
        SetDefault();
        MasterDetailEnabler(true, false);
        ChangeTreeSelection(null, "");
    }
    private void SetMenuProperties(bool newMenu)
    {
        UIUMSMenu menu = new UIUMSMenu();
        if (newMenu)
        {
            menu.ParentID = Convert.ToInt32(_id);
        }
        else
        {
            menu = UIUMSMenu.Get(Convert.ToInt32(_id));
        }

        txtName.Text = menu.Name;
        txtURL.Text = menu.URL;
        chkSysAdmnAcs.Checked = menu.IsSysAdminAccesible;

        if (base.IsSessionVariableExists(SESSIONMENU))
        {
            base.RemoveFromSession(SESSIONMENU);
        }
        base.AddToSession(SESSIONMENU, menu);
    }
    #endregion

    #region Tree Control Functions
    private void LoadRoot()
    {
        List<UIUMSMenu> menus = new List<UIUMSMenu>();
        menus = UIUMSMenu.GetRoots();
        tvwMenus.Nodes.Clear();
        LoadNode(null, menus);
    }
    private void SetID(TreeNode selectednode)
    {
        if (selectednode == null)
        {
            _id = 0;
        }
        else
        {
            _id = Convert.ToInt32(selectednode.Value);
        }
    }
    private void LoadChildrens(TreeNode node)
    {
        SetID(node);

        List<UIUMSMenu> menus = new List<UIUMSMenu>();
        try
        {
            menus = UIUMSMenu.GetByParentID(Convert.ToInt32(_id.ToString()));
            node.ChildNodes.Clear();
            LoadNode(node, menus);
        }
        catch (Exception exp)
        {
            Utilities.ShowMassage(lblErr, Color.Red, exp.Message);
            return;
        }
        finally
        {
            if (node.ChildNodes.Count > 0)
            {
                btnDelete.Attributes.Remove("onclick");
            }
            else
            {
                btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete the selected item?');");
            }
        }
    }
    private void LoadNode(TreeNode parentNode, List<UIUMSMenu> menus)
    {
        if (menus != null)
        {
            foreach (UIUMSMenu menu in menus)
            {
                TreeNode node = new TreeNode();
                node.Text = menu.Name;
                node.Value = menu.Id.ToString();
                node.ExpandAll();
                if (parentNode == null)
                {
                    tvwMenus.Nodes.Add(node);
                }
                else
                {
                    parentNode.ChildNodes.Add(node);
                }
            } 
        }
    }
    private void ChangeTreeSelection(TreeNode parentNode, string selectionValue)
    {
        TreeNodeCollection nodes = null;
        if (parentNode == null)
        {
            nodes = tvwMenus.Nodes;
        }
        else
        {
            nodes = parentNode.ChildNodes;
        }
        foreach (TreeNode node in nodes)
        {
            node.NavigateUrl = selectionValue;
            if (node.ChildNodes.Count > 0)
            {
                ChangeTreeSelection(node, selectionValue);
            }
        }
    }
    #endregion 
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
                LoadRoot();
                MasterDetailEnabler(true, false);
            }
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(this.lblErr, Color.Red, Ex.Message);
        }
    }
    protected void btnAddRoot_Click(object sender, EventArgs e)
    {
        try
        {
            SetID(null);

            MasterDetailEnabler(false, true);
            SetMenuProperties(true);
            ChangeTreeSelection(null, "javascript:void(0)");
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(this.lblErr, Color.Red, Ex.Message);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (tvwMenus.SelectedNode == null)
            {
                Utilities.ShowMassage(this.lblErr, Color.Red, "Before addiing any child menu, you must select the parent/root menu.");
                return;
            }

            SetID(tvwMenus.SelectedNode);

            if (base.IsSessionVariableExists(SESSIONSELECTEDVALUEPATH))
            {
                base.RemoveFromSession(SESSIONSELECTEDVALUEPATH);
            }
            base.AddToSession(SESSIONSELECTEDVALUEPATH, tvwMenus.SelectedNode.ValuePath);

            MasterDetailEnabler(false, true);
            SetMenuProperties(true);
            ChangeTreeSelection(null, "javascript:void(0)");
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(this.lblErr, Color.Red, Ex.Message);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            if (tvwMenus.SelectedNode == null)
            {
                Utilities.ShowMassage(this.lblErr, Color.Red, "Before edit menu, you must select a menu.");
                return;
            }

            SetID(tvwMenus.SelectedNode);

            if (base.IsSessionVariableExists(SESSIONSELECTEDVALUEPATH))
            {
                base.RemoveFromSession(SESSIONSELECTEDVALUEPATH);
            }
            base.AddToSession(SESSIONSELECTEDVALUEPATH, tvwMenus.SelectedNode.ValuePath);

            MasterDetailEnabler(false, true);
            SetMenuProperties(false);
            ChangeTreeSelection(null, "javascript:void(0)");
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(this.lblErr, Color.Red, Ex.Message);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (tvwMenus.SelectedNode == null)
            {
                Utilities.ShowMassage(this.lblErr, Color.Red, "Before delete menu, you must select a menu.");
                return;
            }
            if (tvwMenus.SelectedNode.ChildNodes.Count > 0)
            {
                Utilities.ShowMassage(this.lblErr, Color.Red, "You cann't delete the selected menu, because this menu contain child menus.");
                return;
            }

            SetID(tvwMenus.SelectedNode);
            UIUMSMenu.Delete(Convert.ToInt32(_id));
            if (tvwMenus.SelectedNode.Parent == null)
            {
                LoadRoot();
                if (tvwMenus.Nodes.Count == 0)
                {
                    btnDelete.Attributes.Remove("onclick");
                }
            }
            else
            {
                LoadChildrens(tvwMenus.SelectedNode.Parent);
            }
            Utilities.ShowMassage(this.lblErr, Color.Blue, "Menu information successfully deleted");
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(this.lblErr, Color.Red, Ex.Message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            UIUMSMenu menu = (UIUMSMenu)base.GetFromSession(SESSIONMENU);
            TreeNode node = new TreeNode();


            if (menu.Parent!= null)
            {
                if (chkSysAdmnAcs.Checked && !menu.Parent.IsSysAdminAccesible)
                {
                    Utilities.ShowMassage(this.lblErr, Color.Blue, "Please make parent Sys Admin Accesible first.");
                    return;
                } 
            }

            menu.Name = txtName.Text;
            //menu.MenuKey = txtMenuKey.Text;
            menu.URL = txtURL.Text;
            menu.IsSysAdminAccesible = chkSysAdmnAcs.Checked;

            bool isNew = false;
            if (menu.Id == 0)
            {
                isNew = true;
                menu.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                menu.CreatedDate = DateTime.Now;
                if (menu.Parent != null)
                {
                    menu.Tier = menu.Parent.Tier + 1;
                }
            }
            else
            {
                isNew = false;
                menu.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                menu.ModifiedDate = DateTime.Now;
            }


            UIUMSMenu.Save(menu);

            node.Text = menu.Name;
            node.Value = menu.Id.ToString();
            node.ExpandAll();
            node.NavigateUrl = "javascript:void(0)";

            if (!isNew)
            {
                tvwMenus.FindNode(base.GetFromSession(SESSIONSELECTEDVALUEPATH).ToString()).Text = menu.Name;
                ClearMenuProperties();
                Utilities.ShowMassage(this.lblErr, Color.Blue, "Menu information successfully updated");
            }
            else
            {
                if (menu.ParentID == 0)
                {
                    tvwMenus.Nodes.Add(node);
                    SetID(null);
                }
                else
                {
                    tvwMenus.FindNode(base.GetFromSession(SESSIONSELECTEDVALUEPATH).ToString()).ChildNodes.Add(node);
                    TreeNode treeNode = tvwMenus.FindNode(base.GetFromSession(SESSIONSELECTEDVALUEPATH).ToString());
                    SetID(treeNode);
                    LoadChildrens(treeNode);

                }
                SetMenuProperties(true);
                Utilities.ShowMassage(this.lblErr, Color.Blue, "Menu information successfully saved");
            }
            tvwMenus.Focus();
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(this.lblErr, Color.Red, Ex.Message);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearMenuProperties();
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(this.lblErr, Color.Red, Ex.Message);
        }
    }
    protected void tvwMenus_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            LoadChildrens(tvwMenus.SelectedNode);
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(this.lblErr, Color.Red, Ex.Message);
        }
    } 
    #endregion
}
