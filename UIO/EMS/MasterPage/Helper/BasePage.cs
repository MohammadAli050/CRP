using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BussinessObject;
using Common;
using System.Text.RegularExpressions;
using CommonUtility;
using System.Web.Security;



public class BasePage : System.Web.UI.Page
{
    public int currentUserRoleId = 0;
    public LogicLayer.BusinessObjects.AcademicCalender BaseAcaCalCurrent = null;
    public BussinessObject.UIUMSUser BaseCurrentUserObj = null;
    public string currentPageUrl = "";

    #region Constructor
    public BasePage()
    {
        BaseAcaCalCurrent = LogicLayer.BusinessLogic.AcademicCalenderManager.GetIsCurrent();
        currentPageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
    }
    #endregion

    #region Session Handle
    public void AddToSession(string sessionVariableName, object sessionVariableValue)
    {
        if (IsSessionVariableExists(sessionVariableName) == false)
        {
            Session.Add(sessionVariableName, sessionVariableValue);
        }
        else
        {
            Session[sessionVariableName] = sessionVariableValue;
        }
    }
    public void RemoveFromSession(string sessionVariableName)
    {
        if (IsSessionVariableExists(sessionVariableName) == true)
        {
            Session.Remove(sessionVariableName);
        }
    }
    public object GetFromSession(string sessionVariableName)
    {
        object sessionValue = null;
        try
        {
            if (IsSessionVariableExists(sessionVariableName) == true)
            {
                sessionValue = Session[sessionVariableName];
            }
        }
        catch (FormatException formatException)
        {
            throw formatException;
        }
        catch (Exception exp)
        {
            throw exp;
        }
        return sessionValue;
    }
    public bool IsSessionVariableExists(string sessionVariableName)
    {
        if (Session[sessionVariableName] == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion

    #region Security
    public void Authenticate(UIUMSUser CurrentUser)
    {
        try
        {
            if (DateTime.Today >= CurrentUser.RoleExistStartDate && DateTime.Today <= CurrentUser.RoleExistEndDate)
            {
                if (Authenticate(CurrentUser.RoleMenus, Convert.ToInt32(GetFromSession(Constants.SESSIONMENUID))))
                {
                    return;
                }
                else
                {
                    RemoveFromSession(Constants.SESSIONCURRENT_USER);
                    Response.Redirect("~/Security/Login.aspx");
                    return;
                }
            }
            else
            {
                RemoveFromSession(Constants.SESSIONCURRENT_USER);
                Response.Redirect("~/Security/Login.aspx");
                return;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    internal bool Authenticate(List<Role_Menu> rMenus, int menuID)
    {
        foreach (Role_Menu rm in rMenus)
        {
            if (rm.MenuID == menuID)
            {
                return true;
            }
        }

        if (menuID == -1)
        {
            return true;
        }



        return false;
    }

    public void AuthenticateHome(UIUMSUser CurrentUser)
    {
        try
        {
            if (Authenticate(CurrentUser.RoleMenus, Convert.ToInt32(GetFromSession(Constants.SESSIONMSTRMENUID))))
            {
                return;
            }
            else
            {
                RemoveFromSession(Constants.SESSIONCURRENT_USER);
                Response.Redirect("~/Security/Login.aspx");
                return;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool AccessAuthentication(UIUMSUser currentUser, string strPattern)
    {
        UIUMSUser obUser = new UIUMSUser();
        obUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        
        bool boolReturn = false;
        if (obUser.RoleID > 0)
        {
            if (currentUser.UserPermissions != null)
            {
                foreach (UserPermission permission in currentUser.UserPermissions)
                {
                    if (Regex.IsMatch(strPattern, permission.AccessIDPattern) && DateTime.Today <= permission.EndDate && DateTime.Today >= permission.StartDate)
                    {
                        boolReturn = true;
                        return boolReturn;
                    }
                }
            }
        }
        else
        {
            boolReturn = true;
        }

        return boolReturn;
    }

    public bool ProgramAccessAuthentication(UIUMSUser currentUser, int programId)
    {
        UIUMSUser obUser = new UIUMSUser();
        obUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        LogicLayer.BusinessObjects.UserAccessProgram uapObj = LogicLayer.BusinessLogic.UserAccessProgramManager.GetByUserId(currentUser.Id);
        string[] accessCode = uapObj.AccessPattern.Split('-');
        bool boolReturn = false;
        if (obUser.RoleID > 0)
        {
                foreach (string s in accessCode)
                {
                    if (Convert.ToInt32(s) == programId)
                    {
                        boolReturn = true;
                        return boolReturn;
                    }
                }
            
        }
        else
        {
            boolReturn = true;
        }

        return boolReturn;
    }
    #endregion

    #region Change Tracking Control
    //public void MonitorChanges(WebControl wc)
    //{
    //    if (wc == null)
    //    {
    //        return;

    //    }
    //    if (wc is CheckBoxList || wc is RadioButtonList)
    //    {
    //        for (int i = 0; i <= ((ListControl)(wc)).Items.Count - 1; i++)
    //        {
    //            Page.RegisterArrayDeclaration("monitorChangesIDs", "\"" + string.Concat(wc.ClientID, "_", i) + "\"");
    //            Page.RegisterArrayDeclaration("monitorChangesValues", "null");
    //        }
    //    }
    //    else
    //    {
    //        Page.RegisterArrayDeclaration("monitorChangesIDs", "\"" + wc.ClientID + "\"");
    //        Page.RegisterArrayDeclaration("monitorChangesValues", "null");
    //    }
    //    AssignMonitorChangeValuesOnPageLoad();
    //}
    //private void AssignMonitorChangeValuesOnPageLoad()
    //{
    //    if (!(Page.IsStartupScriptRegistered("monitorChangesAssignment")))
    //    {
    //        Page.RegisterStartupScript("monitorChangesAssignment", "<script language=\"JavaScript\">assignInitialValuesForMonitorChanges();</script>");
    //        Page.RegisterClientScriptBlock("monitorChangesAssignmentFunction", "<script language=\"JavaScript\">function assignInitialValuesForMonitorChanges() {for (var i = 0; i < monitorChangesIDs.length; i++) {var elem = document.getElementById(monitorChangesIDs[i]);if (elem) if (elem.type == 'checkbox' || elem.type == 'radio') monitorChangesValues[i] = elem.checked; else monitorChangesValues[i] = elem.value; }}var needToConfirm = true;window.onbeforeunload = confirmClose; function confirmClose() {if (!needToConfirm) return; for (var i = 0; i < monitorChangesValues.length; i++) { var elem = document.getElementById(monitorChangesIDs[i]); if (elem) if (((elem.type == 'checkbox' || elem.type == 'radio') && elem.checked != monitorChangesValues[i]) || (elem.type != 'checkbox' && elem.type != 'radio' && elem.value != monitorChangesValues[i])) { needToConfirm = false; setTimeout('resetFlag()', 750); return \"You have modified the data entry fields since last savings. If you leave this page, any changes will be lost. To save these changes, click Cancel to return to the page, and then Save the data.\"; } }} function resetFlag() { needToConfirm = true; } </script>");

    //    }
    //}
    //public void BypassModifiedMethod(WebControl wc)
    //{
    //    wc.Attributes.Add("onclick", "javascript:" + GetBypassModifiedMethodScript());
    //    wc.Attributes["onclick"] = "javascript:" + GetBypassModifiedMethodScript();
    //}
    //public string GetBypassModifiedMethodScript()
    //{
    //    return "needToConfirm = false;";
    //}
    #endregion

    //protected override void OnLoad(EventArgs e)
    //{
    //    base.OnLoad(e);

    //    try
    //    {
    //        if (Request.QueryString["mmi"] != null)
    //        {
    //            string mmnu = Request.QueryString["mmi"].ToString();

    //            if (IsSessionVariableExists(Constants.SESSIONMSTRMENUID))
    //            {
    //                RemoveFromSession(Constants.SESSIONMSTRMENUID);
    //            }
    //            AddToSession(Constants.SESSIONMSTRMENUID, Convert.ToInt32(mmnu));
    //        }

    //        UIUMSUser.CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
    //        if (UIUMSUser.CurrentUser != null)
    //        {
    //            if (UIUMSUser.CurrentUser.RoleID > 0)
    //            {
    //                AuthenticateHome(UIUMSUser.CurrentUser);
    //            }
    //        }
    //        else
    //        {
    //            Response.Redirect("~/Security/Login.aspx");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    public void CheckPage_Load()
    {
        try
        {
            // string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

            string uid = "";
            string pwd = "";

            // UIUMSUser obUser = new UIUMSUser();
            //obUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            BaseCurrentUserObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            currentUserRoleId = BaseCurrentUserObj.RoleID;


            LogicLayer.BusinessObjects.User user = LogicLayer.BusinessLogic.UserManager.GetByLogInId(BaseCurrentUserObj.LogInID);


            if (BaseCurrentUserObj.RoleID == 4) // Hardcode: Check that user is student. 
            {
                int flag = 0;
                string loginid = BaseCurrentUserObj.LogInID;
                if (BaseCurrentUserObj.LogInID == BaseCurrentUserObj.Password)
                {
                    BaseCurrentUserObj = null;
                    //userInfoCookie.Expires = DateTime.Now;                       
                    //.RemoveFromSession(Constants.SESSIONCURRENT_USER);
                    flag = 1;
                }


                if (flag != 1 && user.Person != null)
                {
                    if ((string.IsNullOrEmpty(user.Person.FatherName) || string.IsNullOrEmpty(user.Person.MotherName) || string.IsNullOrEmpty(user.Person.GuardianName) || string.IsNullOrEmpty(user.Person.SMSContactSelf)))
                    {
                        flag = 2;
                        //base.RemoveFromSession(Constants.SESSIONCURRENT_USER);
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
            }


            if (Request.QueryString["mmi"] != null)
            {
                string mmnu = UtilityManager.Decrypt(Request.QueryString["mmi"].ToString());

                if (IsSessionVariableExists(Constants.SESSIONMSTRMENUID))
                {
                    RemoveFromSession(Constants.SESSIONMSTRMENUID);
                }
                AddToSession(Constants.SESSIONMSTRMENUID, Convert.ToInt32(mmnu));
            }
            else
            {
                if (IsSessionVariableExists(Constants.SESSIONMSTRMENUID))
                {
                    RemoveFromSession(Constants.SESSIONMSTRMENUID);
                }
                // AddToSession(Constants.SESSIONMSTRMENUID, Convert.ToInt32(-1));
            }


            if (HttpContext.Current.User.Identity.AuthenticationType == "Forms")
            {
                FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                FormsAuthenticationTicket ticket = (id.Ticket);
                uid = UtilityManager.Decrypt(ticket.Name);
                pwd = UtilityManager.Decrypt(ticket.UserData);
                if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(pwd))
                {
                    if (BaseCurrentUserObj == null)
                    {
                        UIUMSUser.Login(uid, pwd);
                        AddToSession(Constants.SESSIONCURRENT_USER, UIUMSUser.GetByLogInID(uid, true));
                    }
                }
            }

            if (BaseCurrentUserObj != null)
            {
                if (BaseCurrentUserObj.RoleID > 0)
                {
                    AuthenticateHome(BaseCurrentUserObj);
                }
            }
            else
            {
                Response.Redirect("~/Security/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Security/Login.aspx");
        }
    }

    public void CheckPageAccess(string _pageUrl)
    {

        BaseCurrentUserObj = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

        if (BaseCurrentUserObj != null)
        {
            List<LogicLayer.BusinessObjects.Menu> menuList = LogicLayer.BusinessLogic.MenuManager.GetByRoleId(BaseCurrentUserObj.RoleID);
            menuList = menuList.Where(a => a.URL != null && a.URL.Contains(GetPage(_pageUrl))).ToList();
            if (menuList != null && menuList.Count == 0)
                Response.Redirect("~/Security/Login.aspx");
        }
        else
        {
            Response.Redirect("~/Security/Login.aspx");
        }
    }

    private string GetPage(string _pageUrl)
    {
        string[] fragments = null;
        int count = 0;

        if (_pageUrl.Contains("mmnuId"))
        {
            fragments = _pageUrl.Split(new char[] { '/', '?' });
            count = fragments.Count() - 1;
        }
        else
        {
            fragments = _pageUrl.Split(new char[] { '/' });
            count = fragments.Count();
        }

        if (count > 0)
            return fragments[count - 1];
        else
            return string.Empty;

    }
}

