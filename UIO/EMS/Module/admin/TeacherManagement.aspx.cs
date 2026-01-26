using BussinessObject;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.Module.admin
{
    public partial class TeacherManagement : BasePage
    {
        UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.TeacherManagement);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.TeacherManagement));

        private string _TeacherList = "SessionTeacherList";
        BussinessObject.UIUMSUser BaseCurrentUserObj = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            BaseCurrentUserObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            if (!IsPostBack)
            {
                divPhotoSignature.Visible = false;
                if (BaseCurrentUserObj.RoleID == 1)
                {
                    btnAddNew.Visible = true;
                    txtTeacherCode.Enabled = true;
                    btnValidate.Enabled = true;
                    ddlEmployeeType.Enabled = true;
                }
                else
                {
                    btnAddNew.Visible = false;
                    txtTeacherCode.Enabled = false;
                    btnValidate.Enabled = false;
                    ddlEmployeeType.Enabled = false;
                }

                LoadDropDowns();
                LoadInstitute();
            }

        }

        private void LoadDropDowns()
        {
            ddlGender.Items.Clear();
            ddlGender.Items.Add(new ListItem("Select", "0"));
            ddlGender.Items.Add(new ListItem("Male", "Male"));
            ddlGender.Items.Add(new ListItem("Female", "Female"));

            ddlReligion.Items.Clear();
            ddlReligion.Items.Add(new ListItem("Select", "0"));
            ddlReligion.Items.Add(new ListItem("Islam", "1"));
            ddlReligion.Items.Add(new ListItem("Hindu", "2"));
            ddlReligion.Items.Add(new ListItem("Buddha", "3"));
            ddlReligion.Items.Add(new ListItem("Christian", "4"));

            ddlMaritalStat.Items.Clear();
            ddlMaritalStat.Items.Add(new ListItem("Select", "0"));
            ddlMaritalStat.Items.Add(new ListItem("Married", "Married"));
            ddlMaritalStat.Items.Add(new ListItem("Unmarried", "Unmarried"));

            ddlStatus.Items.Clear();
            ddlStatus.Items.Add(new ListItem("Select", "0"));
            ddlStatus.Items.Add(new ListItem("FullTime", "1"));
            ddlStatus.Items.Add(new ListItem("PartTime", "2"));
            ddlStatus.Items.Add(new ListItem("HalfTime", "3"));

            #region Exam

            List<PreviousExamType> examTypeList = PreviousExamTypeManager.GetAll();
            List<PreviousExamType> collection = new List<PreviousExamType>();

            #endregion


            #region Employee Type
            ddlEmployeeType.Items.Clear();
            ddlEmployeeType.Items.Add(new ListItem("-Select-", "0"));
            ddlEmployeeType.AppendDataBoundItems = true;
            List<EmployeeType> empTypeList = EmployeeTypeManager.GetAll();
            if (empTypeList != null)
            {
                ddlEmployeeType.DataValueField = "EmployeeTypeId";
                ddlEmployeeType.DataTextField = "EmployeTypeName";
                ddlEmployeeType.DataSource = empTypeList;
                ddlEmployeeType.DataBind();
            }
            #endregion



        }

        private void LoadInstitute()
        {
            var institute = ucamContext.Institutions.ToList();

            ddlInstitute.Items.Clear();
            ddlInstitute.Items.Add(new ListItem("-Select-", "0"));
            ddlInstitute.AppendDataBoundItems = true;

            if (institute != null && institute.Count > 0)
            {
                ddlInstitute.DataValueField = "InstituteId";
                ddlInstitute.DataTextField = "InstituteName";

                ddlInstitute.DataSource = institute;
                ddlInstitute.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
            if (btnPopUpSave.Text.Equals("Update"))
            {
                if (txtTeacherCode.Text == hfTeacherCodeChanged.Value)
                {
                    SaveOrEdit("Edit");
                }
                else
                {
                    if (lblValidationStat.Text.Equals("Available"))
                        SaveOrEdit("Edit");
                    else
                        lblValidationStat.Text = "Plz validate first!";
                }
            }
            else if (btnPopUpSave.Text.Equals("Save"))
            {
                if (lblValidationStat.Text.Equals("Available"))
                    SaveOrEdit("Save");
                else
                    lblValidationStat.Text = "Plz validate first!";
            }
        }

        private void SaveOrEdit(string mode)
        {
            try
            {

                int PersonID = 0;

                PersonID = Convert.ToInt32(hfPersonID.Value);

                if (!string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    //int userId = Convert.ToInt32(GetFromSession(Constants.SESSIONCURRENT_USERID));
                    if (PersonID == 0)
                    {
                        if (string.IsNullOrEmpty(txtTeacherCode.Text))
                        {
                            showAlert("Please input Teacher Code and try again.");
                            return;
                        }

                        #region Insert Into Person Table

                        UCAMDAL.Person person = new UCAMDAL.Person();
                        person.FullName = txtName.Text;
                        person.FatherName = txtFatherName.Text;
                        person.MotherName = txtMotherName.Text;
                        person.Nationality = ddlNationality.SelectedValue;
                        person.Email = txtEmailOfficial.Text;
                        person.SMSContactSelf = txtSMSContact.Text;
                        person.DOB = (!txtDob.Text.Trim().Equals("")) ? DateTime.ParseExact(txtDob.Text, "d/M/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        if (!ddlReligion.SelectedValue.Equals("0"))
                            person.Religion = ddlReligion.SelectedValue.ToString();
                        if (!ddlGender.SelectedValue.Equals("0"))
                            person.Gender = ddlGender.SelectedValue;
                        if (!ddlMaritalStat.SelectedValue.Equals("0"))
                            person.MatrialStatus = ddlMaritalStat.SelectedValue;
                        person.TypeId = 2; // Teacher
                        person.CreatedBy = BaseCurrentUserObj.CreatorID;
                        person.CreatedDate = DateTime.Now;
                        ucamContext.People.Add(person);
                        ucamContext.SaveChanges();
                        PersonID = person.PersonID;
                        hfPersonID.Value = PersonID.ToString();

                        #endregion

                        #region Insert Into Employee Table
                        UCAMDAL.Employee teacher = new UCAMDAL.Employee();
                        teacher.PersonId = PersonID;
                        teacher.Code = txtTeacherCode.Text;
                        teacher.Remarks = txtRemarks.Text;
                        //teacher.Designation = txtDesignation.Text.Trim();
                        teacher.DOJ = (!txtDoj.Text.Trim().Equals("")) ? DateTime.ParseExact(txtDoj.Text, "d/M/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        teacher.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                        teacher.Program = ddlProgram.SelectedValue.ToString();

                        teacher.LibraryCardNo = txtLibCard.Text;
                        teacher.EmployeeTypeId = Convert.ToInt32(ddlEmployeeType.SelectedValue);
                        teacher.CreatedBy = BaseCurrentUserObj.Id;
                        teacher.CreatedDate = DateTime.Now;
                        teacher.InstituteId = Convert.ToInt32(ddlInstitute.SelectedValue);
                        ucamContext.Employees.Add(teacher);
                        ucamContext.SaveChanges();
                        #endregion

                        #region User And UserInPerson

                        UCAMDAL.User userObj = new UCAMDAL.User();

                        userObj.LogInID = txtTeacherCode.Text;
                        userObj.Password = "123456wsx";
                        userObj.RoleID = 4;
                        userObj.IsActive = true;
                        userObj.RoleExistEndDate = DateTime.Now;
                        userObj.RoleExistStartDate = DateTime.Now;
                        userObj.CreatedBy = BaseCurrentUserObj.Id;
                        userObj.CreatedDate = DateTime.Now;
                        userObj.ModifiedBy = BaseCurrentUserObj.Id;
                        userObj.ModifiedDate = DateTime.Now;

                        ucamContext.Users.Add(userObj);
                        ucamContext.SaveChanges();


                        int insertedUserID = userObj.User_ID;

                        if (insertedUserID != 0)
                        {
                            UCAMDAL.UserInPerson userInPerson = new UCAMDAL.UserInPerson();
                            userInPerson.User_ID = insertedUserID;
                            userInPerson.PersonID = PersonID;
                            userInPerson.CreatedBy = BaseCurrentUserObj.Id;
                            userInPerson.CreatedDate = DateTime.Now;
                            userInPerson.ModifiedBy = BaseCurrentUserObj.Id;
                            userInPerson.ModifiedDate = DateTime.Now;
                            ucamContext.UserInPersons.Add(userInPerson);
                            ucamContext.SaveChanges();
                        }

                        #endregion


                        ContactAndAddressInsertOrUpdate(PersonID);
                    }
                    else
                    {
                        var PersonObj = ucamContext.People.Where(x => x.PersonID == PersonID).FirstOrDefault();
                        if (PersonObj != null)
                        {
                            #region Update Person Table

                            PersonObj.FullName = txtName.Text;
                            PersonObj.FatherName = txtFatherName.Text;
                            PersonObj.MotherName = txtMotherName.Text;
                            PersonObj.Nationality = ddlNationality.SelectedValue;
                            PersonObj.Email = txtEmailOfficial.Text;
                            PersonObj.SMSContactSelf = txtSMSContact.Text;
                            PersonObj.DOB = (!txtDob.Text.Trim().Equals("")) ? DateTime.ParseExact(txtDob.Text, "d/M/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            if (!ddlReligion.SelectedValue.Equals("0"))
                                PersonObj.Religion = ddlReligion.SelectedValue.ToString();
                            if (!ddlGender.SelectedValue.Equals("0"))
                                PersonObj.Gender = ddlGender.SelectedValue;
                            if (!ddlMaritalStat.SelectedValue.Equals("0"))
                                PersonObj.MatrialStatus = ddlMaritalStat.SelectedValue;

                            PersonObj.ModifiedBy = BaseCurrentUserObj.CreatorID;
                            PersonObj.ModifiedDate = DateTime.Now;

                            ucamContext.SaveChanges();

                            #endregion

                            #region Employee Table

                            var teacher = ucamContext.Employees.Where(x => x.PersonId == PersonID).FirstOrDefault();
                            if (teacher != null)
                            {
                                teacher.Remarks = txtRemarks.Text;
                                //teacher.Designation = txtDesignation.Text.Trim();
                                teacher.DOJ = (!txtDoj.Text.Trim().Equals("")) ? DateTime.ParseExact(txtDoj.Text, "d/M/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                teacher.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                                teacher.Program = ddlProgram.SelectedValue.ToString();
                                teacher.LibraryCardNo = txtLibCard.Text;
                                teacher.ModifiedBy = BaseCurrentUserObj.Id;
                                teacher.ModifiedDate = DateTime.Now;
                                teacher.Code = txtTeacherCode.Text;
                                teacher.EmployeeTypeId = Convert.ToInt32(ddlEmployeeType.SelectedValue);

                                teacher.InstituteId = Convert.ToInt32(ddlInstitute.SelectedValue);
                                ucamContext.SaveChanges();

                            }

                            #endregion

                            ContactAndAddressInsertOrUpdate(PersonID);
                        }

                    }
                    LoadGridView();
                    Clear();
                    ModalPopupExtender1.Hide();
                    ShowAlertMessage("Succesfull!");
                }
                else
                {
                    ShowAlertMessage("Please fill all the fields properly!");
                }

            }
            catch
            {
                ShowAlertMessage("Something went wrong! Plz try again!");
            }
        }

        private void ContactAndAddressInsertOrUpdate(int personID)
        {
            if (!string.IsNullOrEmpty(hdnMailing.Value))
            {
                Address address = AddressManager.GetById(Convert.ToInt32(hdnMailing.Value));
                address.AddressLine = txtMailingAddress.Text;
                address.ModifiedBy = BaseCurrentUserObj.Id;
                address.ModifiedDate = DateTime.Now;
                AddressManager.Update(address);
            }
            else
            {
                Address address = new Address();
                address.AddressLine = txtMailingAddress.Text;
                address.CreatedBy = BaseCurrentUserObj.Id;
                address.CreatedDate = DateTime.Now;
                address.ModifiedBy = BaseCurrentUserObj.Id;
                address.ModifiedDate = DateTime.Now;
                address.PersonId = personID;
                address.AddressTypeId = 4;
                AddressManager.Insert(address);
            }

            if (!string.IsNullOrEmpty(hdnContact.Value))
            {
                ContactDetails contact = ContactDetailsManager.GetContactDetailsByPersonID(Convert.ToInt32(hdnContact.Value));
                contact.Mobile1 = txtMobile1.Text;
                contact.Mobile2 = txtMobile2.Text;
                contact.PhoneEmergency = txtPhnEmergency.Text;
                contact.PhoneOffice = txtPhnOff.Text;
                contact.PhoneResidential = txtPhnRes.Text;
                contact.EmailPersonal = txtEmailPersonal.Text;
                contact.EmailOther = txtEmailOther.Text;
                contact.EmailOffice = txtEmailOfficial.Text;
                ContactDetailsManager.Update(contact);
            }
            else
            {
                ContactDetails contact = new ContactDetails();
                contact.PersonID = personID;
                contact.Mobile1 = txtMobile1.Text;
                contact.Mobile2 = txtMobile2.Text;
                contact.PhoneEmergency = txtPhnEmergency.Text;
                contact.PhoneOffice = txtPhnOff.Text;
                contact.PhoneResidential = txtPhnRes.Text;
                contact.EmailPersonal = txtEmailPersonal.Text;
                contact.EmailOther = txtEmailOther.Text;
                contact.EmailOffice = txtEmailOfficial.Text;
                ContactDetailsManager.Insert(contact);
            }
        }

        private void Clear()
        {
            divPhotoSignature.Visible = false;
            txtName.Text = "";
            txtTeacherCode.Text = "";
            txtFatherName.Text = "";
            txtMotherName.Text = "";
            ddlNationality.SelectedValue = "1";
            ddlProgram.SelectedValue = "0";
            txtDob.Text = null;
            ddlGender.SelectedIndex = 0;
            ddlMaritalStat.SelectedIndex = 0;
            ddlReligion.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;

            txtRemarks.Text = string.Empty;
            txtDoj.Text = string.Empty;
            lblValidationStat.Text = "";
            ViewState.Remove("TeacherEditId");
            hdnContact.Value = string.Empty;
            txtMobile1.Text = string.Empty;
            txtMobile2.Text = string.Empty;
            txtSMSContact.Text = string.Empty;
            txtPhnOff.Text = string.Empty;
            txtPhnRes.Text = string.Empty;
            txtPhnEmergency.Text = string.Empty;
            txtEmailOfficial.Text = string.Empty;
            txtEmailOther.Text = string.Empty;
            txtEmailPersonal.Text = string.Empty;
            hdnMailing.Value = string.Empty;
            txtMailingAddress.Text = string.Empty;
            txtLibCard.Text = string.Empty;
            txtDesignationRank.Text = string.Empty;
            ddlInstitute.SelectedValue = "0";
            LoadDropDowns();
            LoadInstitute();
            LoadPrograms();
            hfPersonID.Value = "0";
            imgPhoto.ImageUrl = "";
            imgSig.ImageUrl = "";
        }

        private void LoadPrograms()
        {
            try
            {
                ddlProgram.Items.Clear();
                ddlProgram.Items.Add(new ListItem("-Select-", "0"));
                ddlProgram.AppendDataBoundItems = true;
                var programList = ucamContext.Programs.ToList();
                if (programList != null && programList.Count > 0)
                {
                    ddlProgram.DataValueField = "ProgramID";
                    ddlProgram.DataTextField = "ShortName";
                    ddlProgram.DataSource = programList;
                    ddlProgram.DataBind();
                }

            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                btnPopUpSave.Text = "Update";
                //txtTeacherCode.Enabled = false;
                LinkButton linkButton = new LinkButton();
                linkButton = (LinkButton)sender;
                string id = Convert.ToString(linkButton.CommandArgument);

                imgPhoto.ImageUrl = "";
                imgSig.ImageUrl = "";

                var EmpObj = ucamContext.Employees.Where(x => x.EmployeeID.ToString() == id).FirstOrDefault();
                if (EmpObj == null)
                {
                    ShowAlertMessage("Teacher Info Not Found!");
                    return;
                }
                var PersonObj = ucamContext.People.Where(x => x.PersonID == EmpObj.PersonId).FirstOrDefault();

                if (EmpObj != null && PersonObj != null)
                {
                    divPhotoSignature.Visible = true;


                    txtName.Text = PersonObj.FullName;
                    txtTeacherCode.Text = EmpObj.Code.ToString();
                    //txtDesignation.Text = teacher.Designation;
                    hfTeacherCodeChanged.Value = EmpObj.Code.ToString();
                    txtLibCard.Text = EmpObj.LibraryCardNo;

                    txtName.Text = PersonObj.FullName;
                    ddlEmployeeType.SelectedValue = Convert.ToString(EmpObj.EmployeeTypeId);
                    txtFatherName.Text = PersonObj.FatherName;
                    txtMotherName.Text = PersonObj.MotherName;
                    ddlNationality.SelectedValue = PersonObj.Nationality == null ? "0" : PersonObj.Nationality;
                    txtSMSContact.Text = PersonObj.SMSContactSelf;
                    PersonObj.TypeId = 12;

                    #region Show Image And Signature

                    try
                    {
                        imgPhoto.ImageUrl = PersonObj.PhotoBytes == null ? "" : "data:image/png;base64," + PersonObj.PhotoBytes;
                        imgPhoto.Width = imgPhoto.Width;
                        imgPhoto.Height = imgPhoto.Height;
                        imgSig.ImageUrl = PersonObj.SignatureBytes == null ? "" : "data:image/png;base64," + PersonObj.SignatureBytes;
                        imgSig.Width = imgSig.Width;
                        imgSig.Height = imgSig.Height;
                    }
                    catch (Exception ex)
                    {
                    }

                    #endregion

                    #region Hidden Field Value Assign
                    hfPersonID.Value = PersonObj.PersonID.ToString();
                    #endregion

                    try
                    {
                        ddlInstitute.SelectedValue = EmpObj.InstituteId.ToString();
                    }
                    catch (Exception ex)
                    {
                    }
                    txtDob.Text = (PersonObj.DOB != null) ? PersonObj.DOB.Value.ToString("d/M/yyyy") : null;
                    ddlReligion.SelectedValue = string.IsNullOrEmpty(PersonObj.Religion) == true ? "0" : PersonObj.Religion.ToString();
                    ddlGender.SelectedValue = ddlGender.Items.FindByValue(PersonObj.Gender) != null ? PersonObj.Gender : "0";
                    ddlMaritalStat.SelectedValue = ddlMaritalStat.Items.FindByValue(PersonObj.MatrialStatus) != null ? PersonObj.MatrialStatus : "0";
                    ddlStatus.SelectedValue = EmpObj.Status != null ? EmpObj.Status.ToString() : "0";
                    ddlProgram.SelectedValue = EmpObj.Program != null && EmpObj.Program != "" ? EmpObj.Program.ToString() : "0";
                    txtRemarks.Text = EmpObj.Remarks;
                    txtDoj.Text = (EmpObj.DOJ != null) ? EmpObj.DOJ.Value.ToString("d/M/yyyy") : null;

                    #region Contact Information


                    Address mailAddress = AddressManager.GetAddressByPersonId(PersonObj.PersonID).Where(x => x.AddressTypeId == 4).FirstOrDefault();
                    if (mailAddress != null)
                    {
                        hdnMailing.Value = mailAddress.AddressId.ToString();
                        txtMailingAddress.Text = mailAddress.AddressLine;
                    }
                    ContactDetails contact = ContactDetailsManager.GetContactDetailsByPersonID(PersonObj.PersonID);
                    if (contact != null)
                    {
                        hdnContact.Value = contact.PersonID.ToString();
                        txtMobile1.Text = contact.Mobile1;
                        txtMobile2.Text = contact.Mobile2;
                        txtPhnOff.Text = contact.PhoneOffice;
                        txtPhnRes.Text = contact.PhoneResidential;
                        txtPhnEmergency.Text = contact.PhoneEmergency;
                        txtEmailOfficial.Text = contact.EmailOffice;
                        txtEmailOther.Text = contact.EmailOther;
                        txtEmailPersonal.Text = contact.EmailPersonal;
                    }
                    #endregion

                    ModalPopupExtender1.Show();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }

        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            LoadGridView();
        }

        private void LoadGridView()
        {
            int roleID = (int)GetFromSession(Constants.SESSIONCURRENT_ROLEID);

            List<Employee> teacherList = new List<Employee>();


            teacherList = EmployeeManager.GetAllByNameOrCode(txtSearchTeacherName.Text, txtSearchCode.Text.Trim());

            gvTeacherList.DataSource = teacherList;
            gvTeacherList.DataBind();

        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Clear();
            ModalPopupExtender1.Show();
            btnPopUpSave.Text = "Save";
        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
            if (!string.IsNullOrEmpty(txtTeacherCode.Text.Trim()) && EmployeeManager.ValidateEmployee(txtTeacherCode.Text.Trim()) && UserManager.ValidateUser(txtTeacherCode.Text.Trim()))
            {
                lblValidationStat.Text = "Available";

            }
            else
            {
                lblValidationStat.Text = "Unavailable";
            }
        }

        private void ShowAlertMessage(string msg)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ServerControlScript", "alert('" + msg + "');", true);

        }

        protected void btnUploadPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                int PersonID = Convert.ToInt32(hfPersonID.Value);
                if (PersonID == 0)
                {
                    ModalPopupExtender1.Show();
                    ShowAlertMessage("Plz save teacher info first");
                    return;
                }

                string base64String = "";
                if (FileUploadPhoto.HasFile)
                {
                    #region Check File Size is less than 200kb and File type is .jpeg,.jpg or .png
                    if (!MisscellaneousCommonMethods.IsFileSizeOk(FileUploadPhoto, 200))
                    {
                        ModalPopupExtender1.Show();
                        ShowAlertMessage("File size must be less than 200kb");
                        return;
                    }

                    string[] allowedExtensions = { ".jpeg", ".jpg", ".png" };
                    if (!MisscellaneousCommonMethods.IsFileTypeOk(FileUploadPhoto, allowedExtensions))
                    {
                        ModalPopupExtender1.Show();
                        ShowAlertMessage("Only .jpeg, .jpg or .png file types are allowed");
                        return;
                    }
                    #endregion

                    byte[] photoBytes = FileUploadPhoto.FileBytes;
                    base64String = Convert.ToBase64String(photoBytes);

                    var person = ucamContext.People.Where(x => x.PersonID == PersonID).FirstOrDefault();
                    if (person != null)
                    {
                        person.PhotoBytes = base64String;
                        person.ModifiedBy = BaseCurrentUserObj.Id;
                        person.ModifiedDate = DateTime.Now;
                        ucamContext.SaveChanges();
                        showAlert("Photo Upload Complete");

                        // Show the uploaded photo in imgPhoto control
                        imgPhoto.ImageUrl = "data:image/png;base64," + base64String;
                        imgPhoto.Width = imgPhoto.Width;
                        imgPhoto.Height = imgPhoto.Height;

                        ModalPopupExtender1.Show();
                    }
                }

            }
            catch (Exception ex) { }
            finally { }
        }
        protected void btnUploadSig_Click(object sender, EventArgs e)
        {
            try
            {
                int PersonID = Convert.ToInt32(hfPersonID.Value);
                if (PersonID == 0)
                {
                    ModalPopupExtender1.Show();
                    ShowAlertMessage("Plz save teacher info first");
                    return;
                }

                #region Check File Size is less than 100kb and File type is .jpeg,.jpg or .png
                if (!MisscellaneousCommonMethods.IsFileSizeOk(FileUploadSignature, 100))
                {
                    ModalPopupExtender1.Show();
                    ShowAlertMessage("File size must be less than 100kb");
                    return;
                }

                string[] allowedExtensions = { ".jpeg", ".jpg", ".png" };
                if (!MisscellaneousCommonMethods.IsFileTypeOk(FileUploadSignature, allowedExtensions))
                {
                    ModalPopupExtender1.Show();
                    ShowAlertMessage("Only .jpeg, .jpg or .png file types are allowed");
                    return;
                }
                #endregion

                string base64String = "";
                if (FileUploadSignature.HasFile)
                {
                    byte[] signBytes = FileUploadSignature.FileBytes;
                    base64String = Convert.ToBase64String(signBytes);

                    var person = ucamContext.People.Where(x => x.PersonID == PersonID).FirstOrDefault();
                    if (person != null)
                    {
                        person.SignatureBytes = base64String;
                        person.ModifiedBy = BaseCurrentUserObj.Id;
                        person.ModifiedDate = DateTime.Now;
                        ucamContext.SaveChanges();
                        showAlert("Signature Upload Complete");

                        // Show the uploaded photo in imgPhoto control
                        imgSig.ImageUrl = "data:image/png;base64," + base64String;
                        imgSig.Width = imgSig.Width;
                        imgSig.Height = imgSig.Height;

                        ModalPopupExtender1.Show();
                    }
                }

            }
            catch (Exception ex) { }
            finally { }
        }
        protected void showAlert(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
        }

        protected void gvTeacherList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Image imgLogo = (Image)e.Row.FindControl("imgPhotoGrid");
                    Image imgSign = (Image)e.Row.FindControl("imgSignGrid");
                    HiddenField hdnPersonId = (HiddenField)e.Row.FindControl("hdnPersonId");

                    if (hdnPersonId != null && hdnPersonId.Value != "")
                    {
                        var PersonObj = ucamContext.People.Where(x => x.PersonID.ToString() == hdnPersonId.Value).FirstOrDefault();
                        string base64String = PersonObj.PhotoBytes!=null ? PersonObj.PhotoBytes : "";
                        imgLogo.ImageUrl = "data:image/png;base64," + base64String;
                        imgLogo.Width = imgLogo.Width;
                        imgLogo.Height = imgLogo.Height;

                        if (PersonObj.SignatureBytes != null)
                        {
                            imgSign.ImageUrl = "data:image/png;base64," + PersonObj.SignatureBytes;
                        }
                        imgSign.Width = imgSign.Width;
                        imgSign.Height = imgSign.Height;

                    }
                    else
                    {
                        imgLogo.ImageUrl = "~/images/placeholder.png"; // optional fallback
                        imgLogo.Width = imgLogo.Width;
                        imgLogo.Height = imgLogo.Height;
                        imgSign.ImageUrl = "~/images/placeholder.png"; // optional fallback
                        imgSign.Width = imgSign.Width;
                        imgSign.Height = imgSign.Height;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}