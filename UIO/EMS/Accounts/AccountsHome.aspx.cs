using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BussinessObject;

public partial class Accounts_AccountsHome : BasePage
{
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
            UIUMSUser CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            if (CurrentUser != null)
            {
                if (CurrentUser.RoleID > 0)
                {
                    AuthenticateHome(CurrentUser);
                }
            }
            else
            {
                Response.Redirect("~/Security/Login.aspx");
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
            //Utilities.ShowMassage(this.lblErr, Color.Red, Ex.Message);
        }  
    }
}
