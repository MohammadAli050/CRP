using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_StudentInfoEdit : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack)
        {
            //LoadComboBox();
        }
    }

    //protected void LoadComboBox()
    //{
    //    LoadGenderCombo();
    //}

    //protected void LoadGenderCombo()
    //{
    //    try
    //    {
    //        ddlGender.Items.Clear();
    //        ddlGender.Items.Add(new ListItem("Select", "0"));
    //        ddlGender.AppendDataBoundItems = true;

    //        ddlGender.Items.Add(new ListItem("Male", "1"));
    //        ddlGender.Items.Add(new ListItem("Male", "2"));
    //    }
    //    catch { }
    //}

    #endregion

    #region Event

    protected void btnSearch_Click(Object sender, EventArgs e)
    {      
            if (!string.IsNullOrEmpty(txtStudentID.Text))
            {
                LoadStudentById(Convert.ToString(txtStudentID.Text.Trim()));
            }       
    }

    private void LoadStudentById(string roll)
    {                       
       LoadStudentByIdDTO student = StudentManager.GetByRollEdit(roll);
            if (student != null)
            {
                txtName.Text = student.FullName;
                txtFatherName.Text = student.FatherName;
                txtMotherName.Text = student.MotherName;
                txtDOB.Text = student.DOB.ToShortDateString();
                txtGender.Text = student.Gender;
                txtContact.Text = student.Phone;
                txtEmail.Text = student.Email;               
            }

       List<AddressByRoll> address = AddressManager.GetAddressByRoll(roll);

       List<String> addressList = new List<String>();

       for (int i = 0; i < address.Count; i++)
       {
           //Label newLabel = new Label();
           //TextBox txtBox = new TextBox();
          
           //newLabel.ID = "newLabel" + i.ToString();
           //newLabel.Visible = false;
           //newLabel.Text = Convert.ToString(address[i].AddressTypeId);

           //txtBox.ID = "txtbox" + i.ToString();
           //txtBox.Text = address[i].AddressLine;

           addressList.Add(address[i].AddressLine);
           
           //Panel1.Controls.Add(newLabel);
           //Panel1.Controls.Add(txtBox);
       }
       GridView1.DataSource = addressList;
       GridView1.DataBind();
       
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        DataBind();
    }



    protected void btnUpdate_Click(Object sender, EventArgs e)
    {
    //    if (!string.IsNullOrEmpty(txtName.Text) && 
    //        !string.IsNullOrEmpty(txtFatherName.Text) &&
    //        !string.IsNullOrEmpty(txtMotherName.Text) &&
    //        !string.IsNullOrEmpty(txtDOB.Text) &&
    //        !string.IsNullOrEmpty(txtGender.Text) &&
    //        !string.IsNullOrEmpty(txtContact.Text) &&
    //        !string.IsNullOrEmpty(txtEmail.Text))
           
    //    {
    //        Department department = new Department();

    //        department.DeptID =Convert.ToInt32(txtDeptID.Text.Trim());
    //        department.Code = txtCode.Text.Trim();
    //        department.Name = txtName.Text.Trim();
    //        department.OpeningDate = Convert.ToDateTime(txtOpenDate.Text.Trim());
    //        department.SchoolID = Convert.ToInt32(txtSchoolID.Text.Trim());
    //        department.DetailedName = txtDetailedName.Text.Trim();
    //        department.CreatedBy = Convert.ToInt32(txtCreatedBy.Text.Trim());
    //        department.CreatedDate = Convert.ToDateTime(txtCreatedDate.Text.Trim());

    //        DepartmentManager.Update(department);
    //    }
    }

    #endregion

    public Control txtbox { get; set; }

}