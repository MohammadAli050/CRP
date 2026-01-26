using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BussinessObject;

namespace EMS.GradeSheet
{
    public partial class GradeSheetHome :  BasePage
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
                base.CheckPage_Load();

                //UIUMSUser.CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
                //if (UIUMSUser.CurrentUser != null)
                //{
                //    if (UIUMSUser.CurrentUser.RoleID > 0)
                //    {
                //        AuthenticateHome(UIUMSUser.CurrentUser);
                //    }
                //}
                //else
                //{
                //    Response.Redirect("~/Security/Login.aspx");
                //}
            }
            catch (Exception Ex)
            {
                throw Ex;
                //Utilities.ShowMassage(this.lblErr, Color.Red, Ex.Message);
            }
        }

        
    }
}
