using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
namespace EMS.miu.admin
{
    public partial class MenuSearch : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<LogicLayer.BusinessObjects.Menu> menuList = MenuManager.GetAll();
            if (menuList != null)
            {
                menuList = menuList.Where(x => x.Name.ToLower().Contains(txtMenuName.Text.ToLower())).ToList();
            }
            gvMenuList.DataSource = menuList;
            gvMenuList.DataBind();
            LoadLinks();
        }

        private void LoadLinks()
        {
            foreach (GridViewRow row in gvMenuList.Rows)
            {
                Label lblurl = (Label)row.FindControl("Label1");
                string url = lblurl.Text;
                LinkButton linkButton = (LinkButton)row.FindControl("lnkGo");
                linkButton.Attributes.Add("href", "../../" + url.Remove(0, 2) + "?mmi=41485d2c6c554d494e63");
                linkButton.Attributes.Add("target", "_blank");
            }
        }

        //protected void lnkGo_Click(object sender, EventArgs e)
        //{
        //    LinkButton linkButton = new LinkButton();
        //    linkButton = (LinkButton)sender;
        //    string url = Convert.ToString(linkButton.CommandArgument);
        //    linkButton.Attributes.Add("href", "../../"+url.Remove(0, 2) + "?mmi=41485d2c6c554d494e63");
        //    linkButton.Attributes.Add("target", "_blank");
        //    //Response.Redirect(url+"?mmi=41485d2c6c554d494e63");
        //}
    }
}