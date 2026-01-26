using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HelpPage : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack)
        {
            divFormFillUpManual.Visible = true;
            divNoticeForBaBssExam.Visible = true;
            divAdminManual.Visible = true;
            divUpdatedNoticeForBaBssExam.Visible = true;
            if (currentUserRoleId == (int)CommonEnum.Role.Student) //|| currentUserRoleId == (int)CommonEnum.Role.Coordinator)
            {
                divAdminManual.Visible = false;
            }

        }
    }


    #endregion


}