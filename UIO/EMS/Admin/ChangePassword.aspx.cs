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

public partial class Admin_ChangePassword : BasePage
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
            Utilities.ShowMassage(lblChngPwd, Color.Red, Ex.Message);
        }
    }
    protected void chpswdAdmin_CancelButtonClick(object sender, EventArgs e)
    {
        try
        {
            
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblChngPwd, Color.Red, Ex.Message);
        }
    }
    //protected void chpswdAdmin_ChangedPassword(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        UIUMSUser.CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
    //        UIUMSUser.CurrentUser.Password = Utilities.Encrypt(chpswdAdmin.NewPassword);
    //        RemoveFromSession(Constants.SESSIONCURRENT_USER);
    //        AddToSession(Constants.SESSIONCURRENT_USER, UIUMSUser.CurrentUser);

    //    }
    //    catch (Exception Ex)
    //    {
    //        Utilities.ShowMassage(lblChngPwd, Color.Red, Ex.Message);
    //    }
    //}
    protected void chpswdAdmin_ChangingPassword(object sender, LoginCancelEventArgs e)
    {
        try
        {
            UIUMSUser CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            UIUMSUser user = UIUMSUser.Get(CurrentUser.Id, false);
            if (user != null && user.Password != "x")
            {
                if (chpswdAdmin.CurrentPassword != Utilities.Decrypt(user.Password))
                {
                    Utilities.ShowMassage(lblChngPwd, Color.Red, "Invalid Old Password");
                    e.Cancel = true;
                }
                user.Password = Utilities.Encrypt(chpswdAdmin.NewPassword.Trim());
                UIUMSUser.ChangePasswordSave(user);
                RemoveFromSession(Constants.SESSIONCURRENT_USER);
                Response.Redirect("~/Security/Login.aspx");
            }
            else
            {
                Utilities.ShowMassage(lblChngPwd, Color.Red, "Old password did not matched");
            }
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblChngPwd, Color.Red, Ex.Message);
        }
    }

    protected void chpswdAdmin_ChangedPassword(object sender, EventArgs e)
    {

    }
}
