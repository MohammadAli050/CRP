using Common;
using CommonUtility;
//using BussinessObject;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MainMasterPage : BaseMasterPage
{
    private List<MenuViewModel> _allFlatMenus;

    public class MenuViewModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string MenuName { get; set; }
        public string Url { get; set; }
        public int Sequence { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (base.IsSessionVariableExists(Constants.SESSIONCURRENT_USER))
            {
                BussinessObject.UIUMSUser boUser = (BussinessObject.UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER);
                LoadMenuData(boUser);
                LoadUserProfile(boUser);
            }
            else
            {
                Response.Redirect(Constants.URLLOGINPAGE);
            }
        }
    }

    private void LoadMenuData(BussinessObject.UIUMSUser boUser)
    {
        _allFlatMenus = new List<MenuViewModel>();

        // Fetch Raw Data
        if (boUser.RoleID > 0)
        {
            var roleMenus = BussinessObject.Role_Menu.GetMenusByRoleID(boUser.RoleID);
            if (roleMenus != null)
            {
                _allFlatMenus = roleMenus.Select(m => new MenuViewModel
                {
                    Id = m.Menu.Id,
                    ParentId = m.Menu.ParentID,
                    MenuName = m.Menu.Name,
                    Url = m.Menu.URL,
                    Sequence = m.Menu.Sequence
                }).ToList();
            }
        }
        else
        {
            var superMenus = BussinessObject.UIUMSMenu.Get();
            if (superMenus != null)
            {
                _allFlatMenus = superMenus.Select(m => new MenuViewModel
                {
                    Id = m.Id,
                    ParentId = m.ParentID,
                    MenuName = m.Name,
                    Url = m.URL,
                    Sequence = 0
                }).ToList();
            }
        }

        // Generate Recursive HTML
        StringBuilder sb = new StringBuilder();
        var topLevelItems = _allFlatMenus.Where(x => x.ParentId == 0).OrderBy(x => x.Sequence).ToList();

        foreach (var item in topLevelItems)
        {
            sb.Append(BuildMenuRecursive(item));
        }

        litDynamicMenu.Text = sb.ToString();
    }

    private string BuildMenuRecursive(MenuViewModel item)
    {
        var children = _allFlatMenus.Where(x => x.ParentId == item.Id).OrderBy(x => x.Sequence).ToList();
        bool hasChildren = children.Any();

        StringBuilder html = new StringBuilder();

        html.Append("<li class='nav-item'>");

        // Link logic
        string linkHref = hasChildren ? "javascript:void(0);" : GetMenuUrl(item.Url, item.Id);
        string iconClass = (item.ParentId == 0) ? "fas fa-th" : "far fa-circle";

        html.AppendFormat("<a href='{0}' class='nav-link'>", linkHref);
        html.AppendFormat("<i class='nav-icon {0}'></i> ", iconClass);
        html.AppendFormat("<p>{0}", item.MenuName);

        if (hasChildren)
        {
            html.Append("<i class='right fas fa-angle-left'></i>");
        }

        html.Append("</p></a>");

        // Sub-menus logic
        if (hasChildren)
        {
            html.Append("<ul class='nav nav-treeview'>");
            foreach (var child in children)
            {
                // Recursive call for any level
                html.Append(BuildMenuRecursive(child));
            }
            html.Append("</ul>");
        }

        html.Append("</li>");
        return html.ToString();
    }

    private string GetMenuUrl(string url, int id)
    {
        if (string.IsNullOrEmpty(url) || url == "#") return "#";
        string resolved = Page.ResolveUrl(url);
        return string.Format("{0}?mmi={1}", resolved, UtilityManager.Encrypt(id.ToString()));
    }

    private void LoadUserProfile(BussinessObject.UIUMSUser boUser)
    {
        lbtnUserName.Text = boUser.LogInID;
        User user = UserManager.GetByLogInId(boUser.LogInID);
        if (user != null)
        {
            Person person = PersonManager.GetByUserId(user.User_ID);
            if (person != null && !string.IsNullOrEmpty(person.PhotoPath))
            {
                imgAvatar.ImageUrl = "~/Upload/Avatar/" + person.PhotoPath;
            }
            else
            {
                imgAvatar.ImageUrl = "~/Images/avatarMale.png";
            }
        }
    }

    protected void lbtnLogOut_Click(object sender, EventArgs e)
    {
        string loginId = SessionManager.GetObjFromSession<string>(Constants.SESSIONCURRENT_LOGINID);
        LogLoginLogoutManager.Insert(DateTime.Now, loginId, "Log out", "Successful", "", "");
        Session.Abandon();
        FormsAuthentication.SignOut();
        Response.Redirect("~/Security/Login.aspx");
    }
}