using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;

namespace EMS.bup
{
    public partial class FamilyInfo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //base.CheckPage_Load();
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            try
            {
                Student student = SessionManager.GetObjFromSession<Student>("StudentEdit");
                Person person = PersonManager.GetById(student.PersonID);
                if (person != null)
                {
                    lblStudentId.Text = " ,    Student ID : " + student.Roll;
                    txtFatherName.Text = person.FatherName;
                    txtMotherName.Text = person.MotherName;
                    txtFatherProfession.Text = person.FatherProfession;
                    txtMotherProfession.Text = person.MotherProfession;
                    txtGuardianName.Text = person.GuardianName;
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
                    person.FatherName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtFatherName.Text.ToString().ToLower());
                    person.MotherName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtMotherName.Text.ToString().ToLower());
                    person.FatherProfession = txtFatherProfession.Text;
                    person.MotherProfession = txtMotherProfession.Text;
                    person.GuardianName = txtGuardianName.Text;
                    //txtSpouseName.Text;
                    //txtMonthlyIncome.Text;
                    bool resultUpdatePerson = PersonManager.Update(person);
                    if (resultUpdatePerson)
                    {
                        lblMessage.Text = "Updated";
                    }
                }
                LoadData();
            }
            catch {
                lblMessage.Text = "Something went wrong! Try again.";
            }
        }
    }
}