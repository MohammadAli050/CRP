using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SearchPersonProfile : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack)
        {
            LoadDropDown();
        }
    }

    protected void LoadDropDown()
    {
        FillPersonTypeDropDown();
    }

    protected void FillPersonTypeDropDown()
    {
        try
        {
            ddlPersonType.Items.Clear();
            ddlPersonType.Items.Add(new ListItem("Select", "0"));

            List<ValueSet> valueSetList = ValueSetManager.GetAll();
            if (valueSetList.Count > 0 && valueSetList != null)
            {
                ValueSet valueSet = valueSetList.Where(x => x.ValueSetName == "PersonType").SingleOrDefault();
                if (valueSet != null)
                {
                    List<Value> valueList = ValueManager.GetByValueSetId(valueSet.ValueSetID);
                    if (valueList.Count > 0 && valueList != null)
                    {
                        valueList = valueList.OrderBy(x => x.ValueID).ToList();
                        ddlPersonType.AppendDataBoundItems = true;

                        ddlPersonType.DataSource = valueList;
                        ddlPersonType.DataBind();
                    }
                }
            }
        }
        catch { }
    }

    #endregion

    #region Event

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        try
        {
            gvPerson.DataSource = null;
            int persionType = Convert.ToInt32(ddlPersonType.SelectedValue);
            string searchName = txtSearch.Text;

            List<Person> personList = PersonManager.GetAll();
            if (personList.Count > 0 && personList != null)
            {
                if (ddlPersonType.SelectedValue != "0" && ddlPersonType.SelectedValue != "")
                    personList = personList.Where(x => x.TypeId == persionType).ToList();

                if (personList.Count > 0 && personList != null)
                    if (searchName != "")
                        personList = personList.Where(x => x.FullName.ToUpper().Contains(searchName.ToUpper())).ToList();

                if (personList.Count > 0 && personList != null)
                {
                    personList = personList.OrderBy(x => x.FullName).ToList();
                    gvPerson.DataSource = personList;
                }
            }
            gvPerson.DataBind();
        }
        catch { }
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkButton = new LinkButton();
            linkButton = (LinkButton)sender;
            int id = Convert.ToInt32(linkButton.CommandArgument);

            Response.Redirect("UserProfile.aspx?findingID=" + id);
        }
        catch { }
    }

    #endregion
}