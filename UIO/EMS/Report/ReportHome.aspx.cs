using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BussinessObject;
using Common;
using System.Drawing;

public partial class ReportHome : BasePage
{
    BussinessObject.UIUMSUser userObj = null;
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
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            if (Request.QueryString["uid"] != null && Request.QueryString["pwd"] != null && Request.QueryString["mmnu"] != null)
            {
                string uid = Request.QueryString["uid"].ToString();
                string pwd = Request.QueryString["pwd"].ToString();
                string mmnu = Request.QueryString["mmnu"].ToString();

                if (base.IsSessionVariableExists(Constants.SESSIONMSTRMENUID))
                {
                    base.RemoveFromSession(Constants.SESSIONMSTRMENUID);
                }
                base.AddToSession(Constants.SESSIONMSTRMENUID, Convert.ToInt32(mmnu));

                string dPdw = Utilities.Decrypt(pwd);

                if (BussinessObject.UIUMSUser.Login(uid, dPdw))
                    base.AddToSession(Constants.SESSIONCURRENT_USER, userObj);
            }


            if (userObj != null)
            {
                if (userObj.RoleID > 0)
                {
                    AuthenticateHome(userObj);
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
