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

public partial class Admin_PersonUserRelation : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        CleanMessage();

        if (!IsPostBack)
        {
            LoadDropDown();
            //pnlSearch.Visible = false;
        }
    }

    protected void CleanMessage()
    {
        lblMsg.Text = "";
    }

    protected void CleanText()
    {
        lblFatherName.Text = "";
        lblDOB.Text = "";
        lblCode.Text = "";
    }

    protected void LoadDropDown()
    {
        //FillPersonDropDown("");
        //FillUserDropDown("");
        FillPersonTypeDropDown();
        InitialDropDown();
    }

    protected void InitialDropDown()
    {
        ddlPersonStudent.Items.Clear();
        ddlPersonStudent.Items.Add(new ListItem("Select", "0"));

        ddlUser.Items.Clear();
        ddlUser.Items.Add(new ListItem("Select", "-1"));
    }

    protected void FillPersonDropDown(string searchKey, string searchType)
    {
        try
        {
            ddlPersonStudent.Items.Clear();
            ddlPersonStudent.Items.Add(new ListItem("Select", "0"));
            ddlPersonStudent.AppendDataBoundItems = true;

            if (searchType == "Person")
            {
                List<Person> personList = PersonManager.GetAll();
                if (personList.Count > 0 && personList != null)
                {
                    //if (ddlPersonType.SelectedValue != "0" && ddlPersonType.SelectedValue != "")
                    //    personList = personList.Where(x => x.TypeId == Convert.ToInt32(ddlPersonType.SelectedValue)).ToList();

                    if (personList.Count > 0 && personList != null)
                    {
                        if (searchKey != "")
                            personList = personList.Where(x => x.FullName.ToUpper().Contains(searchKey.ToUpper())).ToList();

                        if (personList.Count > 0 && personList != null)
                        {
                            personList = personList.OrderBy(x => x.FullName).ToList();
                            ddlPersonStudent.AppendDataBoundItems = true;

                            foreach (Person person in personList)
                                person.FullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(person.FullName.ToLower());

                            ddlPersonStudent.DataSource = personList;
                            ddlPersonStudent.DataTextField = "FullName";
                            ddlPersonStudent.DataValueField = "PersonID";
                            ddlPersonStudent.DataBind();
                        }
                    }
                }
            }
            else
            {
                List<Employee> empList = EmployeeManager.GetAllByCode(txtInitialSearch.Text);
                if (empList.Count > 0 && empList != null)
                {
                    foreach (Employee emp in empList)
                    {
                        try
                        {
                            Person person = PersonManager.GetById(emp.PersonId);
                            if (person != null)
                                ddlPersonStudent.Items.Add(new ListItem(person.FullName, Convert.ToString(person.PersonID)));
                        }
                        catch { }
                    }
                }
            }
        }
        catch { }
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

    protected void FillUserDropDown(List<User> userList)
    {
        try
        {
            ddlUser.Items.Clear();
            ddlUser.Items.Add(new ListItem("Select", "-1"));

            if (userList.Count > 0 && userList != null)
            {
                if (userList.Count > 0 && userList != null)
                {
                    userList = userList.OrderBy(x => x.LogInID).ToList();
                    ddlUser.AppendDataBoundItems = true;

                    ddlUser.DataSource = userList;
                    ddlUser.DataBind();
                }
                if (userList.Count == 1)
                    ddlUser.SelectedValue = userList[0].User_ID.ToString();
            }
        }
        catch { }
    }

    protected void FillPersonTypeDropDown()
    {
        try
        {
            //ddlPersonType.Items.Clear();
            //ddlPersonType.Items.Add(new ListItem("Select", "0"));

            //List<ValueSet> valueSetList = ValueSetManager.GetAll();
            //if (valueSetList.Count > 0 && valueSetList != null)
            //{
            //    ValueSet valueSet = valueSetList.Where(x => x.ValueSetName == "PersonType").SingleOrDefault();
            //    if (valueSet != null)
            //    {
            //        List<Value> valueList = ValueManager.GetByValueSetId(valueSet.ValueSetID);
            //        if (valueList.Count > 0 && valueList != null)
            //        {
            //            valueList = valueList.OrderBy(x => x.ValueID).ToList();
            //            ddlPersonType.AppendDataBoundItems = true;

            //            ddlPersonType.DataSource = valueList;
            //            ddlPersonType.DataBind();
            //        }
            //    }
            //}
        }
        catch { }
    }

    #endregion

    #region Event

    protected void btnRelate_Click(object sender, EventArgs e)
    {

        int personId = Convert.ToInt32(ddlPersonStudent.SelectedValue);
        int userId = Convert.ToInt32(ddlUser.SelectedValue);

        List<UserInPerson> userInPersonList = UserInPersonManager.GetByPersonId(personId);
        if (userInPersonList.Count > 0 && userInPersonList != null)
        {
            userInPersonList = userInPersonList.Where(x => x.User_ID == userId).ToList();
            if (userInPersonList.Count > 0 && userInPersonList != null)
            {
                lblMsg.Text = "This relation is already EXIST. Try another";
            }
            else
            {
                UserInPerson userInPerson = new UserInPerson();
                userInPerson.User_ID = userId;
                userInPerson.PersonID = personId;
                userInPerson.CreatedBy = 99;
                userInPerson.CreatedDate = DateTime.Now;
                userInPerson.ModifiedBy = 100;
                userInPerson.ModifiedDate = DateTime.Now;

                int resultUserInPerson = UserInPersonManager.Insert(userInPerson);

                lblMsg.Text = "Relate Complete";
            }
        }
        else
        {
            UserInPerson userInPerson = new UserInPerson();
            userInPerson.User_ID = userId;
            userInPerson.PersonID = personId;
            userInPerson.CreatedBy = 99;
            userInPerson.CreatedDate = DateTime.Now;
            userInPerson.ModifiedBy = 100;
            userInPerson.ModifiedDate = DateTime.Now;

            int resultUserInPerson = UserInPersonManager.Insert(userInPerson);

            lblMsg.Text = "Relate Complete";
        }
    }

    protected void btnUserSearch_Click(object sender, EventArgs e)
    {
        FillUserDropDown(txtUserSearch.Text);
        ddlUser.Focus();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtPersonSearch.Text != "")
            {
                FillPersonDropDown(txtPersonSearch.Text, "Person");
                ddlPersonStudent.Focus();
            }
            else if (txtInitialSearch.Text != "")
            {
                FillPersonDropDown(txtPersonSearch.Text, "Initial");
                ddlPersonStudent.Focus();
            }
            else
            {
                lblMsg.Text = "Search key please";
            }
        }
        catch { }
        finally { }
    }

    protected void ddlPersonStudent_Selected(object sender, EventArgs e)
    {
        try
        {
            CleanText();

            int personId = Convert.ToInt32(ddlPersonStudent.SelectedValue);
            Person person = PersonManager.GetById(personId);
            Employee employee = null;
            if (person != null)
            {
                //pnlSearch.Visible = true;
                lblFatherName.Text = person.FatherName;
                lblDOB.Text = Convert.ToDateTime(person.DOB).ToString("dd-MMM-yyyy");
                employee = EmployeeManager.GetByPersonId(personId);
                if (employee != null)
                {
                    lblCode.Text = employee.Code;
                }

                if (person.Users.Count > 0 && person.Users != null)
                {
                    FillUserDropDown(person.Users);
                }

                ddlPersonStudent.Focus();
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
    }

    protected void ddlPersonType_Selected(object sender, EventArgs e)
    {
        //txtPersonSearch.Text = "";
        //pnlSearch.Visible = false;
        //FillPersonDropDown(txtPersonSearch.Text);
    }

    #endregion
}