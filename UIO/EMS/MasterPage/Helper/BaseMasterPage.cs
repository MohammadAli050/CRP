using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web;
using Common;
using BussinessObject;
using CommonUtility;


public class BaseMasterPage : System.Web.UI.MasterPage
{
    #region Constructor
    public BaseMasterPage()
    {
        //
        // TODO: Add constructor logic here
        //
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

    public void LoadChild(List<UIUMSMenu> childMenus, MenuItem mnuItem)
    {
        if (childMenus != null && childMenus.Count > 0)
        {
            foreach (UIUMSMenu childMenu in childMenus)
            {
                if (childMenu.URL.Trim().Length > 0)
                {
                    MenuItem mnuChild = new MenuItem(childMenu.Name, childMenu.Id.ToString());
                    mnuItem.NavigateUrl = childMenu.URL + "?mmi=" + UtilityManager.Encrypt(childMenu.Id.ToString());
                    mnuItem.ChildItems.Add(mnuChild);
                    LoadChild(childMenu.ChildMenus, mnuChild);
                }
            }
        }
    }

    public void LoadRoot(UIUMSUser user, Menu mnuAdmin, int menuID)
    {
        MenuItem mnuItem = null;
        if (user.RoleID > 0)
        {
            var menuList = from element in user.RoleMenus
                           where element.Menu.ParentID == menuID
                           orderby element.Menu.Sequence //----------
                           select element;

            mnuAdmin.Items.Clear();

            if (menuList.Count() > 0)
            {
                foreach (Role_Menu rMenu in menuList)
                {
                    mnuItem = new MenuItem(rMenu.Menu.Name, rMenu.Menu.Id.ToString());
                    mnuItem.NavigateUrl = rMenu.Menu.URL + "?mmi=" + UtilityManager.Encrypt(menuID.ToString());
                    mnuAdmin.Items.Add(mnuItem);
                    LoadChild(rMenu.Menu.ChildMenus, mnuItem, user.RoleMenus, menuID.ToString(), user.Id);
                }
            }
        }
        else
        {
            List<UIUMSMenu> menus = (List<UIUMSMenu>)GetFromSession(Constants.SESSIONSUPERUSERMENU);
            if (menus != null)
            {
                var menuList = from element in menus
                               where element.ParentID == menuID
                               select element;

                mnuAdmin.Items.Clear();

                if (menuList.Count() > 0)
                {
                    foreach (UIUMSMenu menu in menuList)
                    {
                        mnuItem = new MenuItem(menu.Name, menu.Id.ToString());
                        mnuItem.NavigateUrl = menu.URL + "?mmi=" + UtilityManager.Encrypt(menuID.ToString());
                        mnuAdmin.Items.Add(mnuItem);
                        LoadChild(menu.ChildMenus, mnuItem, user.RoleMenus, menuID.ToString(), user.Id);
                    }
                }
            }
        }
    }

    private void LoadChild(List<UIUMSMenu> childMenus, MenuItem mnuItem, List<Role_Menu> RoleMenuList, string menuId, int userId)
    {
        if (childMenus != null && childMenus.Count > 0)
        {

            #region Remove menu from Role by access permission
            List<LogicLayer.BusinessObjects.UserMenu> userMenuList = LogicLayer.BusinessLogic.UserMenuManager.GetAll(userId);
            foreach (LogicLayer.BusinessObjects.UserMenu item in userMenuList)
            {
                childMenus.RemoveAll(cm => cm.Id == item.MenuId && item.AddRemove == -1);
            }
            #endregion

            childMenus = childMenus.OrderBy(cm => cm.Sequence).ToList();

            foreach (UIUMSMenu childMenu in childMenus)
            {
                #region Filter menu by assign role
                Role_Menu emn = RoleMenuList.Where(r => r.MenuID == childMenu.Id).SingleOrDefault();
                #endregion


                if (emn != null)
                {
                    if (childMenu.URL.Trim().Length > 0)
                    {
                        MenuItem mnuChild = new MenuItem(childMenu.Name, childMenu.Id.ToString());
                        mnuChild.NavigateUrl = childMenu.URL + "?mmi=" +UtilityManager.Encrypt( menuId);
                        mnuItem.ChildItems.Add(mnuChild);
                    }
                }
            }
        }
    }

    public void LoadSpecialMenu(UIUMSUser user, Menu menuMain)
    {
        List<LogicLayer.BusinessObjects.UserMenu> userMenus = LogicLayer.BusinessLogic.UserMenuManager.GetAll(user.Id).Where(d => d.ValidTo.Value.Date >= DateTime.Now.Date).ToList();

        MenuItem submenu = new MenuItem("Others *", "special");
        foreach (LogicLayer.BusinessObjects.UserMenu item in userMenus)
        {
            if (item.AddRemove == 1)
            {
                MenuItem mnuChild = new MenuItem(item.Menu.Name, item.Menu.Menu_ID.ToString());
                mnuChild.NavigateUrl = item.Menu.URL + "?mmi=" + UtilityManager.Encrypt("-1");
                submenu.ChildItems.Add(mnuChild);
            }
        }

        menuMain.Items.Add(submenu);
    }
}

