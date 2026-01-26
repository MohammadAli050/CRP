using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_BatchUserControl : System.Web.UI.UserControl
{
    public event EventHandler BatchSelectedIndexChanged;
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

            selectedValue = ddlBatch.SelectedValue;
            selectedText = ddlBatch.SelectedItem.Text;
        }
        catch (Exception)
        {
        }
    }

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedValue = ddlBatch.SelectedValue;
        selectedText = ddlBatch.SelectedItem.Text;

        if (BatchSelectedIndexChanged != null)
            BatchSelectedIndexChanged(this, e);
    }

    private void LoadDropDownList()
    {
        ddlBatch.Items.Clear();
        ddlBatch.Items.Add(new ListItem("-Select-", "0"));
    }

    public void LoadDropDownList(int Id)
    {
        List<Batch> batchList = new List<Batch>();
        batchList = BatchManager.GetAll();

        ddlBatch.Items.Clear();
        ddlBatch.AppendDataBoundItems = true;
        ddlBatch.Items.Add(new ListItem("-Select-", "0"));

        if (batchList != null)
        {
            batchList = batchList.Where(b => b.ProgramId == Id).ToList();

            ddlBatch.DataTextField = "BatchExtended";
            ddlBatch.DataValueField = "BatchId";
            if (batchList != null)
            {
                ddlBatch.DataSource = batchList.OrderByDescending(b => b.BatchNO).ToList(); ;
                ddlBatch.DataBind();
            }
        }
    }


    public void LoadDropDownListWithAll(int Id)
    {
        List<Batch> batchList = new List<Batch>();
        batchList = BatchManager.GetAll();

        ddlBatch.Items.Clear();
        ddlBatch.AppendDataBoundItems = true;
        ddlBatch.Items.Add(new ListItem("All", "0"));

        if (batchList != null)
        {
            batchList = batchList.Where(b => b.ProgramId == Id).ToList();

            ddlBatch.DataTextField = "BatchExtended";
            ddlBatch.DataValueField = "BatchId";
            if (batchList != null)
            {
                ddlBatch.DataSource = batchList.OrderByDescending(b => b.BatchNO).ToList(); ;
                ddlBatch.DataBind();
            }
        }
    }
    public void LoadDropDownListForClassRoutine(int Id)
    {
        List<Batch> batchList = new List<Batch>();
        batchList = BatchManager.GetAll();

        ddlBatch.Items.Clear();
        ddlBatch.AppendDataBoundItems = true;

        if (batchList != null)
        {
            batchList = batchList.Where(b => b.ProgramId == Id).ToList();

            ddlBatch.Items.Add(new ListItem("-Select-", "0"));
            ddlBatch.Items.Add(new ListItem("ALL", "-1"));
            ddlBatch.DataTextField = "BatchNO";
            ddlBatch.DataValueField = "BatchId";
            if (batchList != null)
            {
                ddlBatch.DataSource = batchList.OrderByDescending(b => b.BatchNO).ToList(); ;
                ddlBatch.DataBind();
            }
        }
    }

    internal void SelectedValue(int id)
    {
        ddlBatch.SelectedValue = id.ToString();
    }

    internal void SelectedIndex0()
    {
        ddlBatch.SelectedIndex = 0;
    }
}