using bo = BussinessObject;
using CommonUtility;
using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessObjects;

public partial class Admin_Site : BaseMasterPage
{
    #region Function
    protected void Page_Load(object sender, EventArgs e)
    {
      
        try
        {


            if (!IsPostBack)
            {

             //  string str = Request.Form["hdnId"].ToString();

                bo.UIUMSUser.CurrentUser = (bo.UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER);
                if (bo.UIUMSUser.CurrentUser != null)
                {
                    LoadRoot(bo.UIUMSUser.CurrentUser, menuMain, Convert.ToInt32(base.GetFromSession(Constants.SESSIONMSTRMENUID)));
                    if (Convert.ToInt32(base.GetFromSession(Constants.SESSIONMSTRMENUID)) == -1)
                    {
                        base.LoadSpecialMenu(bo.UIUMSUser.CurrentUser, menuMain);
                    }
                    lbtnUserName.Text = bo.UIUMSUser.CurrentUser.LogInID;
                    LoadCalenderIsActive();
                    LoadCalenderIsRegistration();
                }
            }
        }
        catch (Exception Ex)
        {
            // Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    #endregion

    protected void mnuAdmin_MenuItemClick(object sender, MenuEventArgs e)
    {
        //try
        //{
        //    if (menuMain.SelectedItem.ToolTip.Trim().Length > 0)
        //    {
        //        if (base.IsSessionVariableExists(Constants.SESSIONMENUID))
        //        {
        //            base.RemoveFromSession(Constants.SESSIONMENUID);
        //        }
        //        base.AddToSession(Constants.SESSIONMENUID, menuMain.SelectedItem.Value);

        //        HttpCookie aCookie = Request.Cookies[ConstantValue.Cookie_Authentication];
        //        aCookie["MainMenue"] = base.GetFromSession(Constants.SESSIONMSTRMENUID).ToString();
        //        Response.Cookies.Add(aCookie);

        //        string url = string.Format("../../" + menuMain.SelectedItem.ToolTip);
        //        Response.Redirect(url, false);
        //    }

        //}
        //catch (Exception Ex)
        //{
        //    //  Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        //}
    }

    protected void lbtnLogOut_Click(object sender, EventArgs e)
    {
        base.RemoveFromSession(Constants.SESSIONCURRENT_USER);
        base.RemoveFromSession(Constants.SESSIONMENUID);
        base.RemoveFromSession(Constants.SESSIONMSTRMENUID);
        HttpCookie aCookie = Request.Cookies[ConstantValue.Cookie_Authentication];
        aCookie.Expires = DateTime.Now;
        Response.Redirect("~/Security/login.aspx");
    }

    protected void lbtnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Security/Home.aspx");
    }

    protected void lbtnUserName_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../usermanagement/Admin/userprofile.aspx");
    }

    private void LoadCalenderIsActive()
    {
        List<AcademicCalender> collection = AcademicCalenderManager.GetIsCurrentAllSession().ToList();

        string currentSession = "";
        int f = 0;

        foreach (AcademicCalender item in collection)
        {
            if (f != 0)
            {
                currentSession += " / ";
            }

            currentSession += item.FullCode;

            if (item.CalenderUnitTypeID >= 1 && item.CalenderUnitTypeID <= 3)
            {
                currentSession += " - Tri";
            }
            else
            {
                currentSession += " - Sem";
            }

            f = 1;
        }

        lblCurrent.Text = currentSession;
    }

    private void LoadCalenderIsRegistration()
    {

        List<AcademicCalender> collection = AcademicCalenderManager.GetIsActiveRegistrationAllSession().ToList();

        string currentSession = "";
        int f = 0;

        foreach (AcademicCalender item in collection)
        {
            if (f != 0)
            {
                currentSession += " / ";
            }

            currentSession += item.FullCode;

            if (item.CalenderUnitTypeID >= 1 && item.CalenderUnitTypeID <= 3)
            {
                currentSession += " - Tri";
            }
            else
            {
                currentSession += " - Sem";
            }

            f = 1;
        }

        lblRegistration.Text = currentSession;

    }
}
