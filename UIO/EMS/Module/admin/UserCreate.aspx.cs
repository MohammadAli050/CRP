using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UserCreate : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            LoadDropDown();
        }
    }

    protected void LoadDropDown()
    {
        FillRoleDropDown();
    }

    protected void FillRoleDropDown()
    {
        List<Role> roleList = RoleManager.GetAll();
        if (roleList.Count > 0 && roleList != null)
        {
            ddlRole.Items.Add(new ListItem("Select", "-1"));
            ddlRole.AppendDataBoundItems = true;

            ddlRole.DataSource = roleList;
            ddlRole.DataBind();
        }
    }

    #endregion

    #region Event

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            User user = new User();
            user.User_ID = 0;//Modify by SP
            user.LogInID = txtUserName.Text;
            user.Password = txtPassword.Text;
            user.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
            if (chkIsActive.Checked)
                user.IsActive = true;
            else
                user.IsActive = false;
            user.CreatedBy = 99;
            user.CreatedDate = DateTime.Now;
            user.ModifiedBy = 100;
            user.ModifiedDate = DateTime.Now;

            int resultUser = UserManager.Insert(user);
            if (resultUser > 0)
            {
                lblMsg.Text = "User Created";
                txtUserName.Text = "";
                txtPassword.Text = "";
                ddlRole.SelectedValue = "-1";
                chkIsActive.Checked = false;
            }
        }
        catch { }
    }

    #endregion

    #region Ajax

    [WebMethod]
    public static string AvailabilityCheck(string UserName)
    {
        try
        {

            User user = UserManager.GetByLogInId(UserName);

            if (user != null)
            {
                return "found";
            }
            else
                return "null";
        }
        catch
        {
            return "error";
        }
    }

    #endregion
}