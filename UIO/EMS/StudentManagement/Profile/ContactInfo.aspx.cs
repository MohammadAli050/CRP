using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;

namespace EMS.bup
{
    public partial class ContactInfo : BasePage
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
                    txtMobile.Text = person.Phone;
                    txtEmail.Text = person.Email;
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
                    person.Phone = txtMobile.Text;
                    person.Email = txtEmail.Text;
                    //parent contact
                    //guardian contact
                    bool resultUpdatePerson = PersonManager.Update(person);
                    if (resultUpdatePerson)
                    {
                        lblMessage.Text = "Updated";
                    }
                    else
                    {
                        lblMessage.Text = "Something went wrong! Try again";
                    }
                }
            }
            catch {
                lblMessage.Text = "Something went wrong! Try again.";
            }
        }
    }
}