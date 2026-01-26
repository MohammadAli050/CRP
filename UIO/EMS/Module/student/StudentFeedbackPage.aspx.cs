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

public partial class StudentFeedbackPage : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack)
        {
            try
            {
                string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
                LoadingPersonInformation(loginID);

            }
            catch { }
            finally { }
        }
    }


    private void LoadingPersonInformation(string loginID)
    {
        try
        {
            User user = UserManager.GetByLogInId(loginID);
            if (user != null)
            {
                UserInPerson userInPerson = UserInPersonManager.GetById(user.User_ID);
                if (userInPerson != null)
                {
                    Student student = StudentManager.GetBypersonID(userInPerson.PersonID);
                    lblName.Text = student.BasicInfo.FullName;
                    if (student != null)
                    {
                        List<StudentFeedback> list = StudentFeedbackManager.GetAllByStdentId(student.StudentID);
                        gvPreviousFeedback.DataSource = list;
                        gvPreviousFeedback.DataBind(); 

                    }

                }
                else
                {
                    lblMsg.Text = "Not found";
                }
            }
            else
            {
                lblMsg.Text = "Not found";
            }
        }
        catch { }
        finally { }
    }


    #endregion


    protected void btnSubmitFeedback_Click(object sender, EventArgs e)
    {
        try
        {
            string loginId = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
            User user = UserManager.GetByLogInId(loginId);

            string message = txtMessage.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                StudentFeedback feedback = new StudentFeedback();
                feedback.Message = message;
                feedback.StudentId = user.Person.Student.StudentID;
                feedback.CreatedBy = user.User_ID;
                feedback.CreatedDate = DateTime.Now;

                int isInsert = StudentFeedbackManager.Insert(feedback);
                if (isInsert > 0)
                {
                    lblMsg.Text = "Your Feedback submitted successfully !";
                }
                else
                {
                    lblMsg.Text = "Unsccessful !";
                }
            }
            else
            {
                lblMsg.Text = "Can't Be Empty!";
            }

            LoadingPersonInformation(loginId);
        }
        catch (Exception)
        { }
    }
}