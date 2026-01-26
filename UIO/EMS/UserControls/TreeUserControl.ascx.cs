using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_TreeUserControl : System.Web.UI.UserControl
{
    public event EventHandler TreeSelectedIndexChanged;
    public string selectedValue = string.Empty;
    public string selectedText = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LoadDropDownList();
            }

            selectedValue = ddlTree.SelectedValue;
            selectedText = ddlTree.SelectedItem.Text;
        }
        catch (Exception)
        {   
        }       
    }

    protected void ddlTree_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedValue = ddlTree.SelectedValue;
        selectedText = ddlTree.SelectedItem.Text;

        if (TreeSelectedIndexChanged != null)
            TreeSelectedIndexChanged(this, e);
    }

    private void LoadDropDownList()
    {
        ddlTree.Items.Clear();
        ddlTree.Items.Add(new ListItem("Select Program", "0"));       
    }

    public void LoadDropDownList(int Id)
    {       
        try
        {
            ddlTree.Items.Clear();
            ddlTree.Items.Add(new ListItem("Select", "0"));
            List<TreeMaster> treeMasterList = TreeMasterManager.GetAll();

            treeMasterList = treeMasterList.Where(t => t.ProgramID == Id).ToList();

            ddlTree.Items.Clear();
            ddlTree.AppendDataBoundItems = true;
            ddlTree.Items.Add(new ListItem("-Select-", "0"));
            ddlTree.DataTextField = "Node_Name";
            ddlTree.DataValueField = "TreeMasterID";

            if (treeMasterList != null)
            {
                ddlTree.DataSource = treeMasterList.OrderBy(d => d.TreeMasterID).ToList();
                ddlTree.DataBind();
            }

        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    internal void SelectedValue(int id)
    {
        ddlTree.SelectedValue = id.ToString();
    }

    internal void IndexO()
    {
        ddlTree.SelectedIndex = 0;
    }

}