using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_InstitutionUserControl : System.Web.UI.UserControl
{
    public event EventHandler InstitutionSelectedIndexChanged;
    public string selectedValue = string.Empty;
    public string selectedText = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //LoadDropDownList();               
            }
            selectedValue = ddlInstitution.SelectedValue;
            selectedText = ddlInstitution.SelectedItem.Text;
        }
        catch (Exception)
        {   
        }       
    }

    public void LoadDropDownList()
    {
        List<AffiliatedInstitution> instituteList = new List<AffiliatedInstitution>();
        instituteList = AffiliatedInstitutionManager.GetAll();

        ddlInstitution.Items.Clear();
        ddlInstitution.AppendDataBoundItems = true;

        if (instituteList != null)
        {
            ddlInstitution.Items.Add(new ListItem("-Select-", "0"));
            ddlInstitution.DataTextField = "NameAndAddress";
            ddlInstitution.DataValueField = "Id";

            ddlInstitution.DataSource = instituteList;
            ddlInstitution.DataBind();
        }
    }

    protected void ddlInstitution_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedValue = ddlInstitution.SelectedValue;
        selectedText = ddlInstitution.SelectedItem.Text;

        if (InstitutionSelectedIndexChanged != null)
            InstitutionSelectedIndexChanged(this, e);
    }

    internal void SelectedValue(int id)
    {
        ddlInstitution.SelectedValue = id.ToString();
    }

    internal void IndexO()
    {
        ddlInstitution.SelectedIndex = 0;
    }

    //public void LoadDropdownWithUserAccess(int userID)
    //{
    //    ddlProgram.Items.Clear();
    //    ddlProgram.AppendDataBoundItems = true;
    //    try
    //    {
    //        UserAccessProgram uapObj = UserAccessProgramManager.GetByUserId(userID);
    //        if (uapObj != null && !string.IsNullOrEmpty(uapObj.AccessPattern))
    //        {
    //            List<Program> programList = new List<Program>();
    //            string[] accessCode = uapObj.AccessPattern.Split('-');
    //            foreach (string s in accessCode)
    //            {
    //                if (!string.IsNullOrEmpty(s))
    //                {
    //                    Program program = ProgramManager.GetById(Convert.ToInt32(s));
    //                    programList.Add(program);
    //                }
    //            }
    //            if (programList != null)
    //            {
    //                ddlProgram.Items.Add(new ListItem("-Select-", "0"));
    //                ddlProgram.DataTextField = "ShortName";
    //                ddlProgram.DataValueField = "ProgramID";

    //                ddlProgram.DataSource = programList;
    //                ddlProgram.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            LoadDropDownList();
    //        }
    //    }
    //    catch { LoadDropDownList(); }
        
    //}
}