using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UserProfile : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            if (this.Request.QueryString["findingID"] != null)
            {
                int Id = int.Parse(this.Request.QueryString["findingID"]);
                btnCreate.Text = "Update";
                LoadPerson(Id);
            }
            else
            {
                btnCreate.Text = "Create";
            }
        }
    }

    protected int InsertPerson()
    {
        try
        {
            int flagCheck = 0;
            List<Employee> employeeList = EmployeeManager.GetAll();
            if (employeeList.Count > 0 && employeeList != null)
            {
                employeeList = employeeList.Where(x => x.Code == txtCode.Text).ToList();
                if (employeeList.Count > 0)
                    flagCheck = 1;
            }
            if (flagCheck == 0)
            {
                Person person = new Person();
                person.FullName = txtFullName.Text;
                person.DOB = string.IsNullOrEmpty(txtDOB.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(txtDOB.Text.Trim(), "dd/MM/yyyy", null);

                if (rbMale.Checked)
                    person.Gender = "Male";
                else if (rbFemale.Checked)
                    person.Gender = "Female";

                if (rbSingle.Checked)
                    person.MatrialStatus = "Single";
                else if (rbMarried.Checked)
                    person.MatrialStatus = "Married";

                if (ddlBloodGroup.SelectedValue != "0")
                {
                    for (int i = 1; i < ddlBloodGroup.Items.Count; i++)
                    {
                        if (ddlBloodGroup.Items[i].Selected)
                        {
                            person.BloodGroup = ddlBloodGroup.Items[i].Text;
                            break;
                        }
                    }
                }

                if (ddlReligion.SelectedValue != "0")
                {
                    for (int i = 1; i < ddlReligion.Items.Count; i++)
                    {
                        if (ddlReligion.Items[i].Selected)
                        {
                            person.ReligionId = Convert.ToInt32(ddlReligion.SelectedItem.Value);
                            break;
                        }
                    }
                }

                person.Nationality = txtNationality.Text;
                person.Phone = txtPhone.Text;
                person.Email = txtEmail.Text;
               
                person.CreatedBy = 99;
                person.CreatedDate = DateTime.Now;
                person.ModifiedBy = 100;
                person.ModifiedDate = DateTime.Now;
                person.TypeId = 0;

                int resultPerson = PersonManager.Insert(person);
                Employee employee = new Employee();
                if (resultPerson > 0)
                {
                    employee.Code = txtCode.Text;

                    employee.DeptID = 1;//Must be 0 or NULL
                    employee.CreatedBy = 99;
                    employee.CreatedDate = DateTime.Now;
                    employee.ModifiedBy = 100;
                    employee.ModifiedDate = DateTime.Now;
                    employee.SchoolId = 0;
                    employee.PersonId = resultPerson;

                    int resultEmployee = EmployeeManager.Insert(employee);
                    if (resultEmployee > 0)
                    {
                        lblMsg.Text = "Employee DATA Save";

                        txtFullName.Text = "";
                        txtCode.Text = "";
                        txtDOB.Text = "";
                        rbMale.Checked = false;
                        rbFemale.Checked = false;
                        rbSingle.Checked = false;
                        rbMarried.Checked = false;
                        ddlBloodGroup.SelectedValue = "0";
                        ddlReligion.SelectedValue = "0";
                        txtNationality.Text = "";
                        txtPhone.Text = "";
                        txtEmail.Text = "";

                        return resultEmployee;
                    }
                    else if (resultEmployee == 0)
                    {
                        bool deletePerson = PersonManager.Delete(resultPerson);
                    }
                }
            }
            else
            {
                lblMsg.Text = "Already Exist";
            }
            return 0;
        }
        catch { return 0; }
    }

    protected bool UpdatePerson()
    {
        try
        {
            if (this.Request.QueryString["findingID"] != null)
            {
                int Id = int.Parse(this.Request.QueryString["findingID"]);

                Person person = PersonManager.GetById(Id);

                if (person != null)
                {
                    person.FullName = txtFullName.Text;
                    person.DOB = string.IsNullOrEmpty(txtDOB.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(txtDOB.Text.Trim(), "dd/MM/yyyy", null);

                    if (rbMale.Checked)
                        person.Gender = "Male";
                    else if (rbFemale.Checked)
                        person.Gender = "Female";

                    if (rbSingle.Checked)
                        person.MatrialStatus = "Single";
                    else if (rbMarried.Checked)
                        person.MatrialStatus = "Married";

                    if (ddlBloodGroup.SelectedValue != "0")
                    {
                        for (int i = 1; i < ddlBloodGroup.Items.Count; i++)
                        {
                            if (ddlBloodGroup.Items[i].Selected)
                            {
                                person.BloodGroup = ddlBloodGroup.Items[i].Text;
                                break;
                            }
                        }
                    }

                    if (ddlReligion.SelectedValue != "0")
                    {
                        for (int i = 1; i < ddlReligion.Items.Count; i++)
                        {
                            if (ddlReligion.Items[i].Selected)
                            {
                                person.ReligionId = Convert.ToInt32(ddlReligion.SelectedItem.Value);
                                break;
                            }
                        }
                    }

                    person.Nationality = txtNationality.Text;
                    person.Phone = txtPhone.Text;
                    person.Email = txtEmail.Text;

                    person.ModifiedBy = 101;
                    person.ModifiedDate = DateTime.Now;

                    bool updateResult = PersonManager.Update(person);

                    if (updateResult)
                    {
                        lblMsg.Text = "Updated";

                        //txtFullName.Text = "";
                        //txtCode.Text = "";
                        //txtDOB.Text = "";
                        //rbMale.Checked = false;
                        //rbFemale.Checked = false;
                        //rbSingle.Checked = false;
                        //rbMarried.Checked = false;
                        //ddlBloodGroup.SelectedValue = "0";
                        //ddlReligion.SelectedValue = "0";
                        //txtNationality.Text = "";
                        //txtPhone.Text = "";
                        //txtEmail.Text = "";
                    }
                    else
                    {
                        lblMsg.Text = "Error: 101";
                    }
                    return updateResult;
                }
            }
            return false;
        }
        catch { return false; }
    }

    protected void LoadPerson(int personId)
    {
        try
        {
            Person person = PersonManager.GetById(personId);
            if (person != null)
            {
                txtFullName.Text = person.FullName;
                txtDOB.Text = Convert.ToDateTime(person.DOB).ToString("dd/MM/yyyy");
                if (person.Gender == "Male") rbMale.Checked = true;
                else if (person.Gender == "Female") rbFemale.Checked = true;
                if (person.MatrialStatus == "Single") rbSingle.Checked = true;
                else if (person.MatrialStatus == "Married") rbMarried.Checked = true;
                if (person.BloodGroup != null) ddlBloodGroup.Items.FindByText(person.BloodGroup).Selected = true;
                if (string.IsNullOrEmpty(person.ReligionName) != false) ddlReligion.SelectedValue = person.ReligionId.ToString();
                txtNationality.Text = person.Nationality;
                txtPhone.Text = person.Phone;
                txtEmail.Text = person.Email;

                Employee emp = EmployeeManager.GetByPersonId(personId);
                if (emp != null)
                {
                    txtCode.Text = emp.Code;
                    txtCode.Enabled = false;
                    
                }
                else
                {
                    pnlCode.Visible = false;
                }
            }
        }
        catch { }
    }

    #endregion

    #region Event

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnCreate.Text == "Create")
            {
                int insertReslut = InsertPerson();
                if (insertReslut == 0)
                    lblMsg.Text = "Error: 1110";
            }
            else if (btnCreate.Text == "Update")
            {
                bool updateResult = UpdatePerson();
            }
        }
        catch { }
    }

    #endregion

    #region Ajax

    [WebMethod]
    public static string AvailabilityCheck(string Code)
    {
        try
        {
            List<Employee> employeeList = EmployeeManager.GetAll();

            Employee employee = employeeList.Where(x => x.Code.ToUpper() == Code.ToUpper()).SingleOrDefault();
            if (employee != null)
            {
                return "found";
            }
            else
                return "null";
        }
        catch
        {
            return "error";
        }
    }

    #endregion
}