using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using BussinessObject;
using Common;
using System.Text;

public partial class Accounts_AccountsHead : BasePage
{
    private int _id;

    #region Session Names
    private const string SESSIONTREE = "Tree";
    private const string SESSIONSELECTEDVALUEPATH = "ValuePath";
    #endregion

    #region Methods

    private void loadTagCombo()
    { 
        AccountsHead acHead=new AccountsHead();

        ddlTag.DataSource = null;
        ddlTag.DataSource = acHead.TAG_CBO;
        ddlTag.DataBind();
    }

    private void LoadRoot()
    {
        List<AccountsHead> acHead = new List<AccountsHead>();
        acHead = AccountsHead.GetRoots();
        tvwAccountsHead.Nodes.Clear();
        LoadNode(null, acHead);
    }

    private void LoadNode(TreeNode parentNode, List<AccountsHead> acHead)
    {
        if (acHead != null)
        {
            foreach (AccountsHead item in acHead)
            {
                TreeNode node = new TreeNode();
                node.Text = item.Name;
                node.Value = item.Id.ToString();
                node.ToolTip = item.Tag.ToString();
                node.Target = item.SysMandatory.ToString();

                node.ExpandAll();
                if (parentNode == null)
                {
                    tvwAccountsHead.Nodes.Add(node);
                }
                else
                {
                    parentNode.ChildNodes.Add(node);
                }
            }
        }
    }

    private void LoadChildrens(TreeNode node)
    {
        SetID(node);

        List<AccountsHead> accounts = new List<AccountsHead>();
        try
        {
            accounts = AccountsHead.GetByParentID(_id);
            node.ChildNodes.Clear();
            LoadNode(node, accounts);
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

    private void MasterDetailEnabler(bool enbaleMaster, bool enableDetail)
    {
        pnlDetail.Enabled = enableDetail;
        pnlMaster.Enabled = enbaleMaster;
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

    private void SetTreeProperties(bool newTree)
    {
        AccountsHead acHead = new AccountsHead();

        if (newTree)
        {
            acHead.ParentId = _id;

            txtName.Text = acHead.Name;
            txtRemarks.Text = acHead.Remarks;
            chkIsLeaf.Checked = acHead.IsLeaf;
        }
        else
        {
            acHead = AccountsHead.Get(_id);

            txtName.Text = acHead.Name;
            ddlTag.SelectedValue = acHead.Tag.ToString();
            txtRemarks.Text = acHead.Remarks;
            chkIsLeaf.Checked = acHead.IsLeaf;
        }
                   
        if (Session[SESSIONTREE]!=null)
        {
             Session.Remove(SESSIONTREE);
        }
        Session[SESSIONTREE]= acHead;
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
            if (!IsPostBack && !IsCallback)
            {
                loadTagCombo();
                LoadRoot();
                MasterDetailEnabler(true, false);
            }
            btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete the selected element?');");
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
            if (tvwAccountsHead.SelectedNode == null)
            {
                Utilities.ShowMassage(this.lblErr, Color.Red, "Before addiing any child menu, you must select the parent/root menu.");
                return;
            }

            if (tvwAccountsHead.SelectedNode.Depth == 1)
            {
                Utilities.ShowMassage(this.lblErr, Color.Red, "Only 2 level Tree.");
                return;
            }

            SetID(tvwAccountsHead.SelectedNode);

            if (base.IsSessionVariableExists(SESSIONSELECTEDVALUEPATH))
            {
                base.RemoveFromSession(SESSIONSELECTEDVALUEPATH);
            }
            base.AddToSession(SESSIONSELECTEDVALUEPATH, tvwAccountsHead.SelectedNode.ValuePath);
            
            MasterDetailEnabler(false, true);
            SetTreeProperties(true);            
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
            SetTreeProperties(true);           
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
            if (tvwAccountsHead.SelectedNode == null)
            {
                Utilities.ShowMassage(this.lblErr, Color.Red, "Before edit Tree, you must select a node.");
                return;
            }

            SetID(tvwAccountsHead.SelectedNode);
            if (base.IsSessionVariableExists(SESSIONSELECTEDVALUEPATH))
            {
                base.RemoveFromSession(SESSIONSELECTEDVALUEPATH);
            }
            base.AddToSession(SESSIONSELECTEDVALUEPATH, tvwAccountsHead.SelectedNode.ValuePath);

            MasterDetailEnabler(false, true);
            SetTreeProperties(false);            
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
            AccountsHead acHead = new AccountsHead();

            if (tvwAccountsHead.SelectedNode == null)
            {
                Utilities.ShowMassage(this.lblErr, Color.Red, "Before delete tree, you must select a node.");
                return;
            }
            if (tvwAccountsHead.SelectedNode.ChildNodes.Count > 0)
            {
                Utilities.ShowMassage(this.lblErr, Color.Red, "You cann't delete the selected node, because this menu contain child node.");
                return;
            }

            SetID(tvwAccountsHead.SelectedNode);

            #region Delete
            //if (tvwAccountsHead.SelectedNode.ToolTip.ToString() == acHead.TAG_CBO[0])
            //{
            //    if (tvwAccountsHead.SelectedNode.Parent != null)
            //    {
            //        if (tvwAccountsHead.SelectedNode.Parent.ToolTip.ToString() == acHead.TAG_CBO[0])
            //        {
            //            AccountsHead.Delete(Convert.ToInt32(_id)); //temporary blocked
            //        }
            //        else
            //        {
            //            Utilities.ShowMassage(this.lblErr, Color.Red, "You cann't delete the selected node, because this is system default node.");
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        AccountsHead.Delete(Convert.ToInt32(_id)); //temporary blocked
            //    }
            //}
            if(tvwAccountsHead.SelectedNode.Target.ToString() == "False")
            {
                AccountsHead.Delete(Convert.ToInt32(_id));
            }
            else
            {
                Utilities.ShowMassage(this.lblErr, Color.Red, "You cann't delete the selected node, because this is system default node.");
                return;
            }
            #endregion

            if (tvwAccountsHead.SelectedNode.Parent == null)
            {
                LoadRoot();
                if (tvwAccountsHead.Nodes.Count == 0)
                {
                    btnDelete.Attributes.Remove("onclick");
                }
            }
            else
            {
                LoadChildrens(tvwAccountsHead.SelectedNode.Parent);
            }
            Utilities.ShowMassage(this.lblErr, Color.Blue, "Tree information successfully deleted");
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
            AccountsHead acHead = (AccountsHead) Session[SESSIONTREE];
            TreeNode node = new TreeNode();                        

            acHead.Name = txtName.Text;
            acHead.Tag = ddlTag.SelectedItem.Text;
            acHead.IsLeaf = chkIsLeaf.Checked;
            acHead.SysMandatory = chkSysMandatory.Checked;


            if (txtRemarks.Text == "")
            {
                acHead.Remarks = "Entry from Accounts Head Form.";
            }
            else
            {
                acHead.Remarks = txtRemarks.Text;
            }

            if (AccountsHead.HasDuplicateName(acHead))
            {
                throw new Exception("Duplicate Name are not allowed.");
            }

            if (acHead.Tag != acHead.TAG_CBO[0])
            {
                if (AccountsHead.HasDuplicateTag(acHead))
                {
                    throw new Exception("Duplicate Tag are not allowed.");
                }
            }

            bool isNew = false;
            if (acHead.Id == 0)
            {
                isNew = true;
                acHead.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                acHead.CreatedDate = DateTime.Now;                
            }
            else
            {
                isNew = false;
                acHead.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                acHead.ModifiedDate = DateTime.Now;
            }

            AccountsHead.Save(acHead);

            if (!isNew)//update
            {
                node.Text = acHead.Name;                
                node.Value = acHead.Id.ToString();
                node.ToolTip = acHead.Tag;
                node.ExpandAll();
            }
            else//insert
            {
                if (acHead.ParentId == 0)
                {
                    node.Text = acHead.Name;
                    node.Value = Convert.ToString(AccountsHead.GetMaxRootId());
                    node.ToolTip = acHead.Tag;
                    node.ExpandAll();
                }
                else 
                {
                    node.Text = acHead.Name;
                    node.Value = acHead.Id.ToString();
                    node.ToolTip = acHead.Tag;
                    node.ExpandAll();
                }
            }

            if (!isNew)//update
            {                  
                tvwAccountsHead.FindNode(base.GetFromSession(SESSIONSELECTEDVALUEPATH).ToString()).Text = acHead.Name;
                MasterDetailEnabler(true, false);               

                SetID(null);
                Utilities.ShowMassage(this.lblErr, Color.Blue, "Tree information successfully updated");
            }
            else//insert
            {
                if (acHead.ParentId == 0)
                {                    
                    tvwAccountsHead.Nodes.Add(node);
                    SetID(null);
                }
                else 
                {
                    TreeNode treeNode = tvwAccountsHead.FindNode(base.GetFromSession(SESSIONSELECTEDVALUEPATH).ToString());
                    SetID(treeNode);
                    LoadChildrens(treeNode);
                    SetID(null);
                }

                MasterDetailEnabler(true, false);               
                Utilities.ShowMassage(this.lblErr, Color.Blue, "Tree information successfully saved");
            }

            ClearTreeProperties();
            tvwAccountsHead.Focus();
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(this.lblErr, Color.Red, Ex.Message);
        }
    }

    private string GetChildCode(int parentId)
    {
        string code = "";

        int count = AccountsHead.GetChildCount(parentId);
        code = PadSequence(count);

        return code;
    }

    private string GetRootCode()
    {
        string code = "";

        int count = AccountsHead.GetRootCount();
        code = PadSequence(count);

        return code;
    }

    private string PadSequence(int newCodeSequence)
    {
        StringBuilder sequence = null;
        if (newCodeSequence < 10)
        {
            sequence = new StringBuilder("00" + newCodeSequence.ToString());
        }
        else if (newCodeSequence >= 10 && newCodeSequence < 100)
        {
            sequence = new StringBuilder("0" + newCodeSequence.ToString());
        }
        else
        {
            sequence = new StringBuilder(newCodeSequence.ToString());
        }
        return sequence.ToString();
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearTreeProperties();
            CleareErrorMsg();
            MasterDetailEnabler(true, false);
            LoadRoot();
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(this.lblErr, Color.Red, Ex.Message);
        }
    }

    private void ClearTreeProperties()
    {
        base.RemoveFromSession(SESSIONTREE);
        base.RemoveFromSession(SESSIONSELECTEDVALUEPATH);
        SetDefault();        
    }

    private void CleareErrorMsg()
    {
        lblErr.Text = "";
    }

    private void SetDefault()
    {      
        txtName.Text = "";
        txtRemarks.Text = "";
        chkIsLeaf.Checked = false;
        chkSysMandatory.Checked = false;
        loadTagCombo();
    }

    protected void tvwAccountsHead_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            LoadChildrens(tvwAccountsHead.SelectedNode);
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(this.lblErr, Color.Red, Ex.Message);
        }
    }
    
    #endregion        
}
