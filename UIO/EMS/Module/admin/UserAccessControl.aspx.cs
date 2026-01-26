using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;    

public partial class Admin_UserAccessControl : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

    }

    protected static List<Person> PersonDropDown(string searchKey)
    {
        try
        {

            List<Person> personList = PersonManager.GetAll();
            if (personList.Count > 0 && personList != null)
            {
                //if (ddlPersonType.SelectedValue != "0" && ddlPersonType.SelectedValue != "")
                //    personList = personList.Where(x => x.TypeId == Convert.ToInt32(ddlPersonType.SelectedValue)).ToList();

                if (personList.Count > 0 && personList != null)
                {
                    if (searchKey != "")
                        personList = personList.Where(x => x.FullName.ToUpper().Contains(searchKey.ToUpper())).ToList();

                    if (personList.Count > 0 && personList != null)
                        personList = personList.OrderBy(x => x.FullName).ToList();
                }
            }
            return personList;
        }
        catch { return null; }
    }

    protected static List<User> UserDropDown(string searchKey)
    {
        try
        {
            List<User> userList = UserManager.GetAll();
            if (userList.Count > 0 && userList != null)
            {
                if (searchKey != "")
                    userList = userList.Where(x => x.LogInID.ToUpper().Contains(searchKey.ToUpper())).ToList();

                if (userList.Count > 0 && userList != null)
                    userList = userList.OrderBy(x => x.LogInID).ToList();
            }
            return userList;
        }
        catch { return null; }
    }

    #endregion

    #region Event

    [WebMethod]
    public static string LoadRoleDropDown()
    {
        try
        {
            string result = string.Empty;
            List<Role> roleList = RoleManager.GetAll();
            if (roleList.Count > 0)
            {
                foreach (Role role in roleList)
                {
                    result += role.ID + "," + role.RoleName + ";";
                }
            }
            else
            {
                result = "Program Not Found !";
            }
            return result;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    [WebMethod]
    public static string LoadPersonDropDown(string SearchKey, string UserId)
    {
        try
        {
            string result = string.Empty;
            List<Person> personList = null;

            if (Convert.ToInt32(UserId) == 0)
                personList = PersonDropDown(SearchKey);
            else
            {
                User user = UserManager.GetById(Convert.ToInt32(UserId));
                if (user != null)
                {
                    personList = new List<Person>();
                    if (user.Person != null)
                        personList.Add(user.Person);
                }
            }
            if (personList.Count > 0)
                foreach (Person person in personList)
                    result += person.PersonID + "," + person.FullName + ";";
            else
                result = "Empty";

            return result;
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }

    [WebMethod]
    public static string LoadUserDropDown(string SearchKey, string PersonId)
    {
        try
        {
            string result = string.Empty;

            List<User> userList = null;

            if(Convert.ToInt32(PersonId) == 0)
                userList = UserDropDown(SearchKey);
            else
            {
                Person person = PersonManager.GetById(Convert.ToInt32(PersonId));
                if (person != null)
                    if (person.Users.Count > 0 && person.Users != null)
                        userList = person.Users;
            }

            if (userList.Count > 0)
                foreach (User user in userList)
                    result += user.User_ID + "," + user.LogInID + ";";
            else
                result = "Empty";

            return result;
        }
        catch (Exception ex) { return "Error"; }
    }

    [WebMethod]
    public static string LoadMenuDropDown()
    {
        try
        {
            string result = string.Empty;
            List<LogicLayer.BusinessObjects.Menu> menuList = MenuManager.GetAll();

            if (menuList.Count > 0)
            {
                menuList = menuList.Where(x => x.Tier == 3).OrderBy(m=> m.Name).ToList();

                if (menuList.Count > 0)
                    foreach (LogicLayer.BusinessObjects.Menu menu in menuList)
                        result += menu.Menu_ID + "," + menu.Name + ";";
            }
            else
                result = "Empty";

            return result;
        }
        catch (Exception ex) { return "Error"; }
    }

    [WebMethod]
    public static string RetrieveRoleInfo(string UserId)
    {
        try
        {
            string roleData = string.Empty;
            User user = UserManager.GetById(Convert.ToInt32(UserId));
            if (user != null)
            {
                roleData = user.User_ID + "," + user.RoleID + "," + Convert.ToDateTime(user.RoleExistStartDate).ToString("dd/MM/yyyy") + "," + Convert.ToDateTime(user.RoleExistEndDate).ToString("dd/MM/yyyy");
                return roleData;
            }
            else
                return "";
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message;
            return "Error";
        }
    }

    [WebMethod]
    public static string SaveRoleInfo(string UserId, string RoleId, string ValidFrom, string ValidTo)
    {
        try
        {
            User user = UserManager.GetById(Convert.ToInt32(UserId));
            if (user != null)
            {
                user.RoleID = Convert.ToInt32(RoleId);
                user.RoleExistStartDate = string.IsNullOrEmpty(ValidFrom.Trim()) ? DateTime.Now : DateTime.ParseExact(ValidFrom.Trim(), "dd/MM/yyyy", null);
                //Convert.ToDateTime(ValidFrom);
                user.RoleExistEndDate = string.IsNullOrEmpty(ValidTo.Trim()) ? DateTime.Now : DateTime.ParseExact(ValidTo.Trim(), "dd/MM/yyyy", null); 
                //Convert.ToDateTime(ValidTo);
                user.ModifiedBy = 101;
                user.ModifiedDate = DateTime.Now;

                bool resutlUpdate = UserManager.Update(user);
                if (resutlUpdate)
                    return "Success";
                else
                    return "Fail";
            }
            else
                return "Empty";
        }
        catch (Exception ex)
        { 
            //lblMsg.Text = ex.Message;
            return "Error";
        }
    }

    [WebMethod]
    public static string SaveUserMenuWithDateRange(string UserId, string MenuId, string ValidFrom, string ValidTo)
    {
        try
        {
            UserMenu userMenu = new UserMenu();
            userMenu.MenuId = Convert.ToInt32(MenuId);
            userMenu.UserId = Convert.ToInt32(UserId);
            userMenu.ValidFrom = string.IsNullOrEmpty(ValidFrom.Trim()) ? DateTime.Now : DateTime.ParseExact(ValidFrom.Trim(), "dd/MM/yyyy", null);
            userMenu.ValidTo = string.IsNullOrEmpty(ValidTo.Trim()) ? DateTime.Now : DateTime.ParseExact(ValidTo.Trim(), "dd/MM/yyyy", null);
            userMenu.AddRemove = 1;
            userMenu.CreatedBy = 99;
            userMenu.CreatedDate = DateTime.Now;

            int resultInsert = UserMenuManager.Insert(userMenu);
            if (resultInsert > 0)
                return resultInsert.ToString();
            else
                return "Error";
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }

    [WebMethod]
    public static string SaveUserMenu(string UserId, string MenuId)
    {
        try
        {
            UserMenu userMenu = new UserMenu();
            userMenu.MenuId = Convert.ToInt32(MenuId);
            userMenu.UserId = Convert.ToInt32(UserId);
            userMenu.AddRemove = -1;
            userMenu.CreatedBy = 99;
            userMenu.CreatedDate = DateTime.Now;

            int resultInsert = UserMenuManager.Insert(userMenu);
            if (resultInsert > 0)
                return resultInsert.ToString();
            else
                return "Error";
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }

    [WebMethod]
    public static string DeleteUserMenu(string Id)
    {
        try
        {
            bool resultDelete = UserMenuManager.Delete(Convert.ToInt32(Id));
            if (resultDelete)
                return "True";
            else
                return "False";
        }
        catch (Exception ex) {
            //lblMsg.Text = ex.Message;
            return "Error";
        }
    }

    [WebMethod]
    public static string LoadButtonListDropDown()
    {
        try
        {
            string result = string.Empty;
            List<Value> valueList = ValueManager.GetByValueSetId(4);

            if (valueList.Count > 0)
                foreach (Value value in valueList)
                    result += value.ValueID + "," + value.ValueName + ";";
            else
                result = "Empty";

            return result;
        }
        catch (Exception ex) { return "Error"; }
    }

    [WebMethod]
    public static string SaveUserObjectControl(string UserId, string ButtonId, string ValidFrom, string ValidTo)
    {
        try
        {
            UserObjectControl userObjectControl = new UserObjectControl();
            userObjectControl.ObjectControlId = Convert.ToInt32(ButtonId);
            userObjectControl.UserId = Convert.ToInt32(UserId);
            userObjectControl.ValidFrom = string.IsNullOrEmpty(ValidFrom.Trim()) ? DateTime.Now : DateTime.ParseExact(ValidFrom.Trim(), "dd/MM/yyyy", null);
            userObjectControl.ValidTo = string.IsNullOrEmpty(ValidTo.Trim()) ? DateTime.Now : DateTime.ParseExact(ValidTo.Trim(), "dd/MM/yyyy", null);
            userObjectControl.CreatedBy = 99;
            userObjectControl.CreatedDate = DateTime.Now;

            int resultInsert = UserObjectControlManager.Insert(userObjectControl);
            if (resultInsert > 0)
                return resultInsert.ToString();
            else
                return "Error";
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }

    [WebMethod]
    public static string DeleteUserObjectControl(string Id)
    {
        try
        {
            bool resultDelete = UserObjectControlManager.Delete(Convert.ToInt32(Id));
            if (resultDelete)
                return "True";
            else
                return "False";
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message;
            return "Error";
        }
    }

    [WebMethod]
    public static string RetrieveMenuList(string UserId)
    {
        try
        {
            List<LogicLayer.BusinessObjects.Menu> menuList = MenuManager.GetAll().OrderBy(m => m.Name).ToList();
            Hashtable menuHash = new Hashtable();
            foreach (LogicLayer.BusinessObjects.Menu menu in menuList)
                menuHash.Add(menu.Menu_ID, menu.Name);

            string menuData = string.Empty;
            List<UserMenu> userMenuList = UserMenuManager.GetAll(Convert.ToInt32(UserId));
            if (userMenuList.Count > 0 && userMenuList != null)
            {
                userMenuList = userMenuList.Where(x => x.AddRemove == 1).ToList();
                if (userMenuList.Count > 0)
                    foreach (UserMenu userMenu in userMenuList)
                        menuData += userMenu.Id + "," + menuHash[userMenu.MenuId] + "," + Convert.ToDateTime(userMenu.ValidFrom).ToString("dd/MM/yyyy") + "," + Convert.ToDateTime(userMenu.ValidTo).ToString("dd/MM/yyyy") + ';';

                return menuData;
            }
            else
                return "NULL";
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }

    [WebMethod]
    public static string RetrieveButtonList(string UserId)
    {
        try
        {
            List<Value> valueList = ValueManager.GetByValueSetId(4);
            Hashtable valueHash = new Hashtable();
            foreach (Value value in valueList)
                valueHash.Add(value.ValueID, value.ValueName);

            string buttonData = string.Empty;
            List<UserObjectControl> userObjectControlList = UserObjectControlManager.GetAll(Convert.ToInt32(UserId));
            if (userObjectControlList.Count > 0 && userObjectControlList != null)
            {
                foreach (UserObjectControl userObjectControl in userObjectControlList)
                    buttonData += userObjectControl.Id + "," + valueHash[userObjectControl.ObjectControlId] + "," + Convert.ToDateTime(userObjectControl.ValidFrom).ToString("dd/MM/yyyy") + "," + Convert.ToDateTime(userObjectControl.ValidTo).ToString("dd/MM/yyyy") + ';';

                return buttonData;
            }
            else
                return "NULL";
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }

    [WebMethod]
    public static string RetrieveRoleMenuList(string RoleId, string UserId)
    {
        try
        {
            int flag = 0;
            List<LogicLayer.BusinessObjects.Menu> menuList = MenuManager.GetAll().ToList();
            if (menuList.Count > 0 && menuList != null)
                menuList = menuList.Where(x => x.Tier == 2).OrderBy(m => m.Name).ToList();

            Hashtable menuHash = new Hashtable();
            foreach (LogicLayer.BusinessObjects.Menu menu in menuList)
                menuHash.Add(menu.Menu_ID, menu.Name);
            List<UserMenu> userMenuList = UserMenuManager.GetAll(Convert.ToInt32(UserId)).ToList();

            string roleMenuData = string.Empty;
            List<RoleMenu> roleMenuList = RoleMenuManager.GetAll().OrderBy(r=> r.MenuName).ToList();
            if (roleMenuList.Count > 0 && roleMenuList != null)
            {
                roleMenuList = roleMenuList.Where(x => x.RoleID == Convert.ToInt32(RoleId)).OrderBy(m=> m.MenuName).ToList();
                if (roleMenuList.Count > 0)
                {
                    foreach (RoleMenu roleMenu in roleMenuList)
                    {
                        flag = 0;
                        if (userMenuList.Count > 0)
                        {
                            for (int i = 0; i < userMenuList.Count; i++)
                            {
                                if (roleMenu.MenuID == userMenuList[i].MenuId)
                                {
                                    flag = 1;
                                    roleMenuData += roleMenu.MenuID + "," + menuHash[roleMenu.MenuID] + "," + userMenuList[i].Id + ';';
                                    break;
                                }
                            }
                        }
                        if (flag == 0)
                        {
                            if (menuHash[roleMenu.MenuID] != "" && menuHash[roleMenu.MenuID] != null)
                                roleMenuData += roleMenu.MenuID + "," + menuHash[roleMenu.MenuID] + "," + 0 + ';';
                        }
                    }
                }
                return roleMenuData;
            }
            else
                return "NULL";
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }

    #endregion
}