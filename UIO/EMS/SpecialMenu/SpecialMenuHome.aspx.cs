using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BussinessObject;
using Common;
using System.Drawing;

public partial class SpecialMenuHome : BasePage
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
        }
        catch (Exception Ex)
        {
            throw Ex;

        }
    }

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Request.QueryString["mmnuId"] != null)
    //        {
    //            string mmnu = Request.QueryString["mmnuId"].ToString();

    //            if (IsSessionVariableExists(Constants.SESSIONMSTRMENUID))
    //            {
    //                RemoveFromSession(Constants.SESSIONMSTRMENUID);
    //            }
    //            AddToSession(Constants.SESSIONMSTRMENUID, Convert.ToInt32(mmnu));


    //        }
    //        else
    //        {
    //            Response.Redirect("~/Security/Login.aspx");
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        throw Ex;
    //        //Utilities.ShowMassage(this.lblErr, Color.Red, Ex.Message);
    //    }
    //}
}
