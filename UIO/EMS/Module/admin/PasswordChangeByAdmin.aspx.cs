using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PasswordChangeByAdmin : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        CleanMessage();

        if (!IsPostBack)
        {
            LoadDropDown();
        }
    }

    protected void CleanMessage()
    {
        lblMsg.Text = "";
    }

    protected void CleanText()
    {
        lblPassword.Text = "";
        lblMailStatus.Text = "";
    }

    protected void LoadDropDown()
    {
        FillUserDropDown("");
    }

    protected void FillUserDropDown(string searchKey)
    {
        try
        {
            ddlUser.Items.Clear();
            ddlUser.Items.Add(new ListItem("Select", "-1"));

            List<User> userList = UserManager.GetAll();
            if (userList.Count > 0 && userList != null)
            {
                if (searchKey != "")
                    userList = userList.Where(x => x.LogInID.ToUpper().Contains(searchKey.ToUpper())).ToList();

                if (userList.Count > 0 && userList != null)
                {
                    userList = userList.OrderBy(x => x.LogInID).ToList();
                    ddlUser.AppendDataBoundItems = true;

                    ddlUser.DataSource = userList;
                    ddlUser.DataBind();
                }
            }
        }
        catch { }
    }

    public string CreatePassword(int length)
    {
        string valid = "123456789ABCDEFGHIJKLMNPQRSTUVWXYZabcdefghijklmnpqrstuvwxyz";
        string res = "";
        Random rnd = new Random();
        while (0 < length--)
            res += valid[rnd.Next(valid.Length)];

        return res;
    }

    #endregion

    #region Event

    protected void btnUserSearch_Click(object sender, EventArgs e)
    {
        FillUserDropDown(txtUserSearch.Text);
    }

    protected void ddlUser_Selected(object sender, EventArgs e)
    {
        try
        {
            CleanText();

            int userId = Convert.ToInt32(ddlUser.SelectedValue);
            if (userId != -1)
            {
                User user = UserManager.GetById(userId);
                if (user != null)
                {
                    if (user.Person != null)
                    {
                        lblName.Text = user.Person.FullName;
                        lblEamil.Text = user.Person.Email;
                    }
                }
            }
            else
            {
                txtUserSearch.Text = "";
                lblName.Text = "";
                lblEamil.Text = "";
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
        finally
        {
            ddlUser.Focus();
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            string password = string.Empty;
            while (true)
            {
                password = CreatePassword(8);
                if (Regex.IsMatch(password, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$"))
                    break;
            }

            int userId = Convert.ToInt32(ddlUser.SelectedValue);
            if (userId != -1)
            {
                User user = UserManager.GetById(userId);
                if (user != null)
                {
                    user.Password = password;
                    user.ModifiedBy = 101;
                    user.ModifiedDate = DateTime.Now;

                    bool resultUpdate = UserManager.Update(user);
                    if (resultUpdate)
                    {
                        lblPassword.Text = password;
                        return;
                    }
                    else    lblMsg.Text = "Error: 303";
                }
            }
            else    lblMsg.Text = "Please select User ID";
        }
        catch(Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
    }

    protected void btnMail_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddlUser.SelectedValue) != -1)
            {
                if (lblPassword.Text == "")
                {
                    lblMsg.Text = "Please Generate Password";
                    return;
                }
                if (lblEamil.Text != "")
                {
                    bool resultMail = Sendmail.sendEmail("Admin", lblEamil.Text, "", "New Password", "Your New Password is : " + lblPassword.Text);
                    if (resultMail)
                        lblMailStatus.Text = "mail Send";
                    else    lblMailStatus.Text = "mail Send Fail";
                }
                else    lblMsg.Text = "this User Profile has no email address in his data. Please update his profile with E-mail";
            }
            else    lblMsg.Text = "Please select User ID";
        }
        catch(Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
    }

    #endregion
}