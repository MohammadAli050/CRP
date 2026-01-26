using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using bo = BussinessObject;
using System.Drawing;
using LogicLayer.BusinessLogic;
using CommonUtility;
using LogicLayer.BusinessObjects;
using System.Globalization;

public partial class PublicMasterPage : BaseMasterPage
{
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
            }
        }
        catch (Exception Ex)
        {
            
        }
    }
    
    #endregion
}
