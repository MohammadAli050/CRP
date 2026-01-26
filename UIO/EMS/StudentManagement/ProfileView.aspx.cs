using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BussinessObject;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;

namespace EMS.StudentManagement
{
    public partial class ProfileView : BasePage
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Page.Request.ServerVariables["http_user_agent"].ToLower().Contains("safari"))
            {
                Page.ClientTarget = "uplevel";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //base.CheckPage_Load();
            }
            catch (Exception Ex)
            {
                throw Ex;
                //Utilities.ShowMassage(this.lblErr, Color.Red, Ex.Message);
            }
        }
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            ClearAll();
            pnlStudentLoad.Visible = true;
            pnlStudentBasicInfo.Visible = false;
            if (string.IsNullOrEmpty(txtStudent.Text.Trim()))
            {
                try
                {
                    List<LogicLayer.BusinessObjects.Student> stdList = StudentManager.GetAllByProgramIdBatchId(Convert.ToInt32(ucProgram.selectedValue), Convert.ToInt32(ucBatch.selectedValue));
                    if (stdList != null)
                    {
                        GvStudent.DataSource = stdList;
                        GvStudent.DataBind();
                    }
                }
                catch { }
            }
            else
            {
                try
                {
                    LogicLayer.BusinessObjects.Student student = StudentManager.GetByRoll(txtStudent.Text.Trim());
                    if (student != null)
                    {
                        List<LogicLayer.BusinessObjects.Student> stdlist = new List<LogicLayer.BusinessObjects.Student>();
                        stdlist.Add(student);
                        GvStudent.DataSource = stdlist;
                        GvStudent.DataBind();
                    }
                    else
                        lblMessage.Text = "Student not found!";
                }
                catch { }
            }
        }

        private void ClearAll()
        {
            GvStudent.DataSource = null;
            GvStudent.DataBind();
        }
        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }
        protected void GvStudent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int studentId = Convert.ToInt32(e.CommandArgument);
            LogicLayer.BusinessObjects.Student studentObj = new LogicLayer.BusinessObjects.Student();
            studentObj = StudentManager.GetById(studentId);
            //HiddenStudentId.Value = Convert.ToString(studentId);
            LogicLayer.BusinessObjects.Person person = PersonManager.GetById(studentObj.PersonID);
            if (e.CommandName == "StudentDetails")
            {
                if (person != null)
                {
                    pnlStudentLoad.Visible = false;
                    pnlStudentBasicInfo.Visible = true;
                    lblStudentName.Text = person.FullName;
                    lblStudentRoll.Text = studentObj.Roll;
                    if(!string.IsNullOrEmpty(person.FatherName))
                        lblFatherName.Text = person.FatherName;
                    if (!string.IsNullOrEmpty(person.MotherName))
                        lblMotherName.Text = person.MotherName;
                    if (!string.IsNullOrEmpty(person.Email))
                        lblMotherName.Text = person.Email;
                    DateTime dt = (DateTime)person.DOB;
                    if(dt!=null)
                        lblDOB.Text = dt.ToString("dd/MM/yyyy");
                    List<Address> addressList = AddressManager.GetAddressByPersonId(person.PersonID);
                    Address address = addressList.Where(x => x.AddressTypeId == 1).FirstOrDefault();
                    if (address != null)
                        lblPresentAddress.Text = address.AddressLine + ", " + address.District + ", " + address.Division;
                    address = addressList.Where(x => x.AddressTypeId == 2).FirstOrDefault();
                    if (address != null)
                        lblPermanentAddress.Text = address.AddressLine + ", " + address.District + ", " + address.Division;
                    if (!string.IsNullOrEmpty(person.Phone))
                        lblPhone.Text = person.Phone;

                }
            }
        }
        protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAll();
        }

       
    }
}