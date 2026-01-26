using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using CommonUtility;

public partial class Admin_StudentProfile : BasePage
{

    BussinessObject.UIUMSUser userObj = null;
    string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

        if (!IsPostBack)
        {
            try
            {
                LoadStudent(userObj.LogInID);
            }
            catch { }
            finally { }
        }
    }

    private void LoadStudent(string loginId)
    {
        try
        {
            Student student = StudentManager.GetByRoll(loginId);
            LogicLayer.BusinessObjects.StudentRegistration sr = StudentRegistrationManager.GetByStudentId(student.StudentID);

            lblFullName.Text = student.BasicInfo.FullName;
            lblProgram.Text = student.Program.DetailName;
            lblRoll.Text = student.Roll;
            if (sr != null)
            {
                lblRegNo.Text = sr.RegistrationNo;
            }
            lblGuardianName.Text = student.BasicInfo.GuardianName;
            lblCgpa.Text = Convert.ToString(student.CGPA);
            lblFatherName.Text = student.BasicInfo.FatherName;
            lblMotherName.Text = student.BasicInfo.MotherName;
            lblGender.Text = student.BasicInfo.Gender;
            lblEmail.Text = student.BasicInfo.Email;
            lblPhone.Text = student.BasicInfo.SMSContactSelf;
            lblBloodGroup.Text = student.BasicInfo.BloodGroup;
            lblReligion.Text = student.BasicInfo.ReligionName;
            lblMaritalStatus.Text = student.BasicInfo.MatrialStatus;
            lblFatherProfession.Text = student.BasicInfo.FatherProfession;
            lblMotherProfession.Text = student.BasicInfo.MotherProfession;
            lblStudentId.Text = " ,    Student ID : " + student.Roll;
            if (student.BasicInfo.PhotoPath != null)
            {
                imgPhoto.ImageUrl = "~/Upload/Avatar/" + student.BasicInfo.PhotoPath;
            }
            else
            {
                if (student.BasicInfo.Gender.ToLower() == "female")
                    imgPhoto.ImageUrl = "~/Images/photoGirl.png";
                else
                    imgPhoto.ImageUrl = "~/Images/photoBoy.png";
            }

        }
        catch (Exception)
        { }
    }

}