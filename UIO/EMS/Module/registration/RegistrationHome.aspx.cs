using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BussinessObject;
using Common;
using System.Drawing;

public partial class Registration_RegistrationHome : BasePage
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
}
