using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CalculateResultProcess : BasePage
{
    #region Funciton

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
            string roll = txtStudentId.Text;
            int resultProcess = StudentACUDetailManager.Calculate_GPAandCGPAByRoll(roll);

            if (resultProcess != 0)
                lblMsg.Text = "Process effect" + resultProcess + " Semester.";
            else
                lblMsg.Text = "No effect.";
        }
        catch { }
    }
    #endregion
}