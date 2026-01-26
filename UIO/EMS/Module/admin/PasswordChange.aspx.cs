using BussinessObject;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PasswordChange : Page
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string loginID = UtilityManager.Decrypt(Request.QueryString["loginid"]);
            if (!string.IsNullOrEmpty(loginID))
            {
                UIUMSUser CurrentUser = UIUMSUser.GetByLogInID(loginID, true);
                if (Session[Constants.SESSIONCURRENT_USER] != null)
                {
                    Session.Remove(Constants.SESSIONCURRENT_USER);
                }
                Session[Constants.SESSIONCURRENT_USER] = CurrentUser;
            }

            LoadUserInfo();
        }
    }

    protected void LoadUserInfo()
    {
        try
        {
            //HttpCookie aCookie = Request.Cookies[ConstantValue.Cookie_Authentication];

            //string uid = aCookie["UserName"];
            //string pwd = aCookie["UserPassword"];
            string loginID = UtilityManager.Decrypt(Request.QueryString["loginid"]);

            if (!string.IsNullOrEmpty(loginID))
            {
                User user = UserManager.GetByLogInId(loginID);


                if (user != null)
                {
                    if (user.Person != null)
                    {
                        lblName.Text = user.Person.FullName;
                        if (user.Person.Student != null)
                        {
                            lblID.Text = user.Person.Student.Roll;
                            txtMobileNo.Text = user.Person.SMSContactSelf;
                        }

                        LogicLayer.BusinessObjects.StudentRegistration sr = StudentRegistrationManager.GetByStudentId(user.Person.Student.StudentID);

                        if (sr != null)
                        {
                            lblRegistration.Text = sr.RegistrationNo;
                        }
                    }
                }
            }
        }
        catch { }
    }

    #endregion

    #region Event

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string loginID = UtilityManager.Decrypt(Request.QueryString["loginid"]);

            if (!string.IsNullOrEmpty(loginID))
            {
                User user = UserManager.GetByLogInId(loginID);
                if (user != null)
                {
                    string uid = user.LogInID;
                    string pwd = user.Password;

                    if (txtPassword.Text == pwd)
                    {
                        lblMsg.Text = "User Name and Password must be defferent";
                        return;
                    }

                    if (user.Person != null)
                    {
                        LogicLayer.BusinessObjects.Person person = user.Person;
                        person.SMSContactSelf = txtMobileNo.Text.Trim();
                        bool resultUpdatePerson = PersonManager.Update(person);

                        user.Password = txtPassword.Text;
                        user.ModifiedBy = 101;
                        user.ModifiedDate = DateTime.Now;

                        bool resultUpdate = UserManager.Update(user);
                        if (resultUpdate && resultUpdatePerson)
                        {
                            lblMsg.Text = "New Password Updated.";

                            if (user.Person != null)
                            {
                                if ((string.IsNullOrEmpty(user.Person.FatherName) || string.IsNullOrEmpty(user.Person.MotherName) || string.IsNullOrEmpty(user.Person.GuardianName) || string.IsNullOrEmpty(user.Person.SMSContactSelf)))
                                {
                                    Response.Redirect("~/Module/Admin/PersonalProfileFillUpPage.aspx?loginid=" + UtilityManager.Encrypt(loginID));
                                }
                                else
                                {
                                    Response.Redirect("~/Security/Home.aspx" + "?mmi=" + UtilityManager.Encrypt("-1"), false);
                                }

                            }
                            else
                            {
                                Response.Redirect("~/Security/Home.aspx" + "?mmi=" + UtilityManager.Encrypt("-1"), false);
                            }


                        }
                        else
                        {
                            lblMsg.Text = "Error: 1122";
                            return;
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Please Contact System Admin because your Date Of Birth field is Empty.";
                        return;
                    }
                }

            }
        }
        catch { }
    }

    #endregion
}