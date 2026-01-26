using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Admin_PasswordChangeByUser : BasePage
{
    BussinessObject.UIUMSUser userObj = null;

    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        lblMsg.Text = "Password must contain eight(8) digits including at least one uppercase alphabet, one lowercase alphabet and one number";
    }

    protected void Logout()
    {        
        Response.Redirect("~/Security/login.aspx");
    }

    #endregion

    #region Event

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string uid = "";
            string pwd = "";

            uid = userObj.LogInID;
            pwd = userObj.Password;

            //HttpCookie aCookie = Request.Cookies[ConstantValue.Cookie_Authentication];

            //string uid = aCookie["UserName"];
            //string pwd = aCookie["UserPassword"];

            if (pwd != txtCurrentPassword.Text)
                lblMsg.Text = "current password and your login password are not matched";
            else if (txtCurrentPassword.Text == txtPassword.Text)
                lblMsg.Text = "current password and new password MUST be different";
            else if (!Regex.IsMatch(txtPassword.Text, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$"))
                lblMsg.Text = "password must be contain atlest one number, one uppercase character and one lowercase character";// EXCEPT 0, o and O
            else
            {

                User user = UserManager.GetByLogInId(uid);
                if (user != null)
                {
                    user.Password = txtPassword.Text;
                    user.ModifiedBy = 100;
                    user.ModifiedDate = DateTime.Now;
                    bool resultUpdate = UserManager.Update(user);
                    if (resultUpdate)
                    {
                        lblMsg.Text = "new password updated";
                        Logout();
                    }
                    else
                    {
                        lblMsg.Text = "Error: 1002";
                    }
                }
                else
                {
                    lblMsg.Text = "Error: 1001";
                }
            }
        }
        catch { }
        finally { }
    }
        
    #endregion
}