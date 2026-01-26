using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;

namespace EMS.Admin
{
    public partial class UserPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetPassword_Click(object sender, EventArgs e)
        {
            LogicLayer.BusinessObjects.User user = new LogicLayer.BusinessObjects.User();
            if (!string.IsNullOrEmpty(txtUserId.Text.Trim()))
            {
                user = UserManager.GetByLogInId(txtUserId.Text.Trim());
                if (user != null)
                {
                    txtPassword.Text = user.Password;
                }
                else
                {
                    txtPassword.Text = "";
                    lblMsg.Text = "User not found";
                }
            }
            else
            {
                txtPassword.Text = "";
                lblMsg.Text = "Please input User Login ID";
            }
        }
    }
}