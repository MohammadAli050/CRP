using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ForgetPassword : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        lblMsg.Text = "";
    }

    public string CreatePassword(int length)
    {
        string valid = "1234567890ABCXYZ";
        string res = "";
        Random rnd = new Random();
        while (0 < length--)
            res += valid[rnd.Next(valid.Length)];
        return res;
    }

    #endregion

    #region Event

    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            string userId = txtUserID.Text;
            string email = txtEmail.Text;
            string password = CreatePassword(6);

            User user = UserManager.GetByLogInId(userId);
            if (user != null)
            {
                if (user.Person != null)
                {
                    if (user.Person.Email != "")
                    {
                        if (user.Person.Email == email)
                        {
                            bool resultMail = Sendmail.sendEmail("Admin", user.Person.Email, "", "New Password", "Your New Password is : " + password);
                            if (resultMail)
                            {
                                lblMsg.Text = "Check Your Mail";
                                user.Password = password;
                                user.ModifiedBy = 101;
                                user.ModifiedDate = DateTime.Now;

                                bool resultUpdate = UserManager.Update(user);
                            }
                            else    lblMsg.Text = "mail Send Fail.Try AGAIN!!";
                        }
                        else    lblMsg.Text = "Your provided email not match in profile email address.";
                    }
                    else    lblMsg.Text = "Your Profile has no email address in your data. Please update your profile with E-mail";
                }
            }
            else
                lblMsg.Text = "User ID not exist";
        }
        catch(Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
    }

    #endregion
}