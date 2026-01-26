using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Common;
using BussinessObject;
using CommonUtility;
using LogicLayer.BusinessLogic;

public partial class Security_Default : BasePage
{
    static readonly object lockThis = new object();

    #region Functions
    private UIUMSUser MakeSysAdmin(UIUMSUser su)
    {
        su = new UIUMSUser();
        su.LogInID = "SuperUser";
        su.Password = "x";
        su.IsActive = true;
        su.RoleID = 0;
        su.RoleExistStartDate = DateTime.Now;
        su.RoleExistEndDate = DateTime.MaxValue;
        su.CreatorID = -2;
        su.CreatedDate = DateTime.Now;
        //sysAdmin.ModifierID = -1;
        //sysAdmin.ModifiedDate = DateTime.Now;

        UIUMSUser.SaveSysAdmin(su);
        return su;
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

    }
    protected void logMain_Authenticate(object sender, AuthenticateEventArgs e)
    {
        try
        {

            lock (lockThis)
            {
                //LogicLayer.BusinessObjects.StudentRegistration sr = StudentRegistrationManager.GetByRegistrationNo(logMain.UserName);
                //if (sr != null)
                //{
                //    logMain.UserName = sr.Roll;
                //}
                //else
                //{
                //    LogicLayer.BusinessObjects.User usr = UserManager.GetAdminUserByLogInId(logMain.UserName);
                //    if (usr == null)
                //    {
                //        logMain.FailureText = "You are not authorized to run this application!</br>Try with "
                //        + "correct user name and password. ";
                //        LogLoginLogoutManager.Insert(DateTime.Now, logMain.UserName, "Login", "Failed ", GetIPAddress(), logMain.Password);
                //    }
                //}

                if (UIUMSUser.Login(logMain.UserName, logMain.Password))
                {
                    UIUMSUser userObj = UIUMSUser.GetByLogInID(logMain.UserName, true);

                    LogLoginLogoutManager.Insert(DateTime.Now, userObj.LogInID, "Login", "Successful", GetIPAddress(), UtilityManager.Encrypt(logMain.Password));
                    base.AddToSession(Constants.SESSIONCURRENT_USER, userObj);
                    base.AddToSession(Constants.SESSIONCURRENT_LOGINID, userObj.LogInID);
                    base.AddToSession(Constants.SESSIONCURRENT_ROLEID, userObj.RoleID);




                    /////////////////////////////////////////////////////////////////



                    CheckBox ckBox = (CheckBox)logMain.FindControl("chkRememberMe");
                    if (ckBox.Checked)
                    {
                        string uid = UtilityManager.Encrypt(logMain.UserName);
                        string pwd = UtilityManager.Encrypt(logMain.Password);
                        FormsAuthenticationTicket ticket_uid = new FormsAuthenticationTicket(1, uid, DateTime.Now, DateTime.Now.AddMinutes(60), false, pwd, FormsAuthentication.FormsCookiePath);

                        string encTicket_uid = FormsAuthentication.Encrypt(ticket_uid);

                        Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket_uid));
                    }
                    /////////////////////////////////////////////////////////////////

                    LogicLayer.BusinessObjects.User user = LogicLayer.BusinessLogic.UserManager.GetByLogInId(userObj.LogInID);


                    if (userObj.RoleID == 4) // Hardcode: Check that user is student. 
                    {
                        int flag = 0;
                        string loginid = userObj.LogInID;
                        if (userObj.LogInID == userObj.Password)
                        { 
                            userObj = null;
                            //userInfoCookie.Expires = DateTime.Now;                       
                            base.RemoveFromSession(Constants.SESSIONCURRENT_USER);
                            flag = 1; 
                        }


                        if ( flag!=1 && user.Person != null)
                        {
                            if ((string.IsNullOrEmpty(user.Person.FatherName) || string.IsNullOrEmpty(user.Person.MotherName) || string.IsNullOrEmpty(user.Person.GuardianName) || string.IsNullOrEmpty(user.Person.SMSContactSelf)))
                            {
                                flag = 2;
                                base.RemoveFromSession(Constants.SESSIONCURRENT_USER);
                            }

                        }

                        if (flag == 1)
                        {
                            Response.Redirect("~/Module/Admin/PasswordChange.aspx?loginid=" + UtilityManager.Encrypt(loginid));
                            
                        }
                        else if (flag == 2)
                        { 
                            Response.Redirect("~/Module/Admin/PersonalProfileFillUpPage.aspx?loginid=" + UtilityManager.Encrypt(loginid));                            
                        }
                        else
                        {
                            Response.Redirect("Home.aspx" + "?mmi=" + UtilityManager.Encrypt("-1"), false);
                        }
                    }
                    else
                    {
                        Response.Redirect("Home.aspx" + "?mmi=" + UtilityManager.Encrypt("-1"), false);
                    }
                }
                else
                {
                    logMain.FailureText = "You are not authorized to run this application!</br>Try with "
                        + "correct user name and password. ";
                    LogLoginLogoutManager.Insert(DateTime.Now, logMain.UserName, "Login", "Failed ", GetIPAddress(), logMain.Password);
                }

            }
        }
        catch (Exception Ex)
        {
            logMain.FailureText = Ex.Message;
            LogLoginLogoutManager.Insert(DateTime.Now, logMain.UserName, "Login", "Failed ", GetIPAddress(), logMain.Password);
        }
    }

    protected string GetIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }

        return context.Request.ServerVariables["REMOTE_ADDR"];

    }
    #endregion
}
