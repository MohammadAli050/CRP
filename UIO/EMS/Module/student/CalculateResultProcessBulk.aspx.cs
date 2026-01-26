using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CalculateResultProcessBulk : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
    }

    #endregion

    #region Event

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        try
        {
            int resultProcess = StudentACUDetailManager.Calculate_GPAandCGPA_Bulk();

            if (resultProcess != 0)
                lblMsg.Text = "Process effect";
            else
                lblMsg.Text = "No effect";
        }
        catch { }
    }

    #endregion
}