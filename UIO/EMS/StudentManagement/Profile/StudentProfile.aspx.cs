using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessLogic;
using System.Globalization;
using CommonUtility;

namespace EMS.bup
{
    public partial class StudentProfile : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //base.CheckPage_Load();
            if (!IsPostBack)
            {
                LoadData();

            }
        }
        protected void LoadData()
        {
            try
            {
                Student student = SessionManager.GetObjFromSession<Student>("StudentEdit");
                Person person = PersonManager.GetById(student.PersonID);
                txtStudentName.Text = person.FullName;

                lblStudentId.Text = " ,    Student ID : " + student.Roll;
                ddlBloodGroup.SelectedValue = string.IsNullOrEmpty(person.BloodGroup) ? "0" : person.BloodGroup;
                ddlGender.SelectedValue = string.IsNullOrEmpty(person.Gender) ? "0" : person.Gender;
                ddlMaritialStatus.SelectedValue = string.IsNullOrEmpty(person.MatrialStatus) ? "0" : person.MatrialStatus;
                ddlReligion.SelectedValue = Convert.ToString(person.ReligionId);
                txtNationality.Text = person.Nationality;

                try
                {
                    DateTime dt = (DateTime)person.DOB;
                    txtDOB.Text = dt.ToString("dd/MM/yyyy");
                }
                catch (Exception ex)
                {
                }

            }
            catch { }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            try
            {
                Student student = SessionManager.GetObjFromSession<Student>("StudentEdit");
                Person person = PersonManager.GetById(student.PersonID);

                if (person != null)
                {
                    person.FullName = txtStudentName.Text;
                    person.DOB = string.IsNullOrEmpty(txtDOB.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(txtDOB.Text.Trim(), "dd/MM/yyyy", null);
                    person.BloodGroup = ddlBloodGroup.SelectedValue != "0" ? ddlBloodGroup.SelectedItem.Text : null;
                    person.Gender = ddlGender.SelectedValue != "0" ? ddlGender.SelectedItem.Text : null;
                    person.MatrialStatus = ddlMaritialStatus.SelectedValue != "0" ? ddlMaritialStatus.SelectedItem.Text : null;
                    person.ReligionId = Convert.ToInt32(ddlReligion.SelectedItem.Value);
                    person.Nationality = txtNationality.Text;
                    //txtNID.Text;
                    bool resultUpdatePerson = PersonManager.Update(person);
                    if (resultUpdatePerson)
                    {
                        lblMessage.Text = "Updated";
                    }
                    else
                    {
                        lblMessage.Text = "Something went wrong! Try again.";
                    }
                }
            }
            catch
            {
                lblMessage.Text = "Something went wrong! Try again.";
            }
        }
    }
}