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

public partial class PersonalProfileFillUpPage : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string loginID = UtilityManager.Decrypt(Request.QueryString["loginid"]);
            if (!string.IsNullOrEmpty(loginID))
            {
                BussinessObject.UIUMSUser CurrentUser = BussinessObject.UIUMSUser.GetByLogInID(loginID, true);
                if (Session[Constants.SESSIONCURRENT_USER] != null)
                {
                    Session.Remove(Constants.SESSIONCURRENT_USER);
                }
                Session[Constants.SESSIONCURRENT_USER] = CurrentUser;
                 
                SetRoleSession(loginID);
                LoadingCheck(loginID);
                LoadingPersonInformation(loginID);
            }

        }
    }    

    private void SetRoleSession(string loginID)
    {
        try
        {
            User user = UserManager.GetByLogInId(loginID);
            if (user != null)
            {
                Role role = RoleManager.GetById(user.RoleID);
                if (role != null)
                {
                    Session["Role"] = role.RoleName;
                }
            }
        }
        catch { }
        finally { }
    }

    private void LoadingCheck(string loginID)
    {
        try
        {
            if (Session["Role"].ToString().ToLower() == "student")
            {
                pnIsVisible.Visible = true;
                pnStudentIDZone.Visible = false;
                txtFullName.Enabled = false;
                LoadingPersonInformation(loginID);
            }
            else
            {
                pnIsVisible.Visible = true;
                pnStudentIDZone.Visible = true;
                txtFullName.Enabled = true;
                //LoadingPersonInformation(loginID);

            }
        }
        catch { }
        finally { }
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
                    Person person = PersonManager.GetById(userInPerson.PersonID);
                    Student student = StudentManager.GetBypersonID(userInPerson.PersonID);
                    List<AddressType> addressTypeList = AddressTypeManager.GetAll();
                    List<Address> addressList = AddressManager.GetAddressByPersonId(userInPerson.PersonID);

                    if (person != null && student != null)
                    {
                        

                        pnIsVisible.Visible = true;

                        #region Hidden Field Value Assign
                        hfPerson.Value = person.PersonID.ToString();
                        hfStudent.Value = student.StudentID.ToString();
                        hfRoll.Value = student.Roll;
                        #endregion

                        txtFullName.Text = person.FullName == null ? "" : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(person.FullName.ToString().ToLower());
                        ddlGender.SelectedValue = person.Gender == null ? "" : person.Gender.ToLower();
                        ddlBloodGroup.SelectedValue = person.BloodGroup == null ? "" : person.BloodGroup.ToLower();
                        txtPhone.Text = person.Phone == null ? "" : person.Phone;
                        txtNationality.Text = person.Nationality == null ? "" : person.Nationality;
                        txtFatherName.Text = person.FatherName == null ? "" : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(person.FatherName.ToString().ToLower());
                        txtMotherName.Text = person.MotherName == null ? "" : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(person.MotherName.ToString().ToLower());
                        txtDOB.Text = person.DOB == null ? "" : Convert.ToDateTime(person.DOB).ToString("dd/MM/yyyy");
                        ddlMatrialStatus.SelectedValue = person.MatrialStatus == null ? "" : person.MatrialStatus.ToLower();
                        ddlReligion.SelectedValue = person.ReligionId.ToString();
                        txtEmail.Text = person.Email == null ? "" : person.Email;
                        txtGuardianName.Text = person.GuardianName == null ? "" : person.GuardianName;
                        txtFatherProfession.Text = person.FatherProfession == null ? "" : person.FatherProfession;
                        txtMotherProfession.Text = person.MotherProfession == null ? "" : person.MotherProfession;
                        txtSMSContactSelf.Text = person.SMSContactSelf;
                        txtSMSContactGuardian.Text = person.SMSContactGuardian;

                        if (person.PhotoPath != null)
                        {
                            imgPhoto.ImageUrl = "~/Upload/Avatar/" + person.PhotoPath;
                        }
                        else
                        {
                            if (person.Gender.ToLower() == "female")
                                imgPhoto.ImageUrl = "~/Images/photoGirl.png";
                            else
                                imgPhoto.ImageUrl = "~/Images/photoBoy.png";
                        }

                        if (addressList.Count > 0 && addressList != null)
                        {
                            foreach (AddressType addressType in addressTypeList)
                            {
                                List<Address> tempAddressList = addressList.Where(x => x.AddressTypeId == addressType.AddressTypeId).ToList();

                                if (tempAddressList.Count > 0 && tempAddressList != null)
                                {
                                    if (addressType.TypeName == "Present")
                                    {
                                        txtPresent.Text = tempAddressList[0].AddressLine;
                                        hfPresent.Value = tempAddressList[0].AddressId.ToString();
                                    }
                                    else if (addressType.TypeName == "Permanent")
                                    {
                                        txtPermanent.Text = tempAddressList[0].AddressLine;
                                        hfPermanent.Value = tempAddressList[0].AddressId.ToString();
                                    }
                                    else if (addressType.TypeName == "Guardian")
                                    {
                                        txtGuardian.Text = tempAddressList[0].AddressLine;
                                        hfGuardian.Value = tempAddressList[0].AddressId.ToString();
                                    }
                                    else if (addressType.TypeName == "MailingAddress")
                                    {
                                        txtMailing.Text = tempAddressList[0].AddressLine;
                                        hfMailing.Value = tempAddressList[0].AddressId.ToString();
                                    }
                                }
                                if (addressType.TypeName == "Present") { hfPresentType.Value = addressType.AddressTypeId.ToString(); }
                                else if (addressType.TypeName == "Permanent") { hfPermanentType.Value = addressType.AddressTypeId.ToString(); }
                                else if (addressType.TypeName == "Guardian") { hfGuardianType.Value = addressType.AddressTypeId.ToString(); }
                                else if (addressType.TypeName == "MailingAddress") { hfMailingType.Value = addressType.AddressTypeId.ToString(); }
                            }
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
            else
            {
                pnIsVisible.Visible = false;
                lblMsg.Text = "Not found";
            }
        }
        catch { }
        finally { }
    }

    private string UploadImage(string tempDirectoryPath, string fullDirectoryPath, FileUpload fileUploadPhoto, int y, int x)
    {
        string uniqueFileNameString = hfRoll.Value;
        try
        {
            string orginalFileName = fileUploadPhoto.PostedFile.FileName;

            string tempfileName = uniqueFileNameString + orginalFileName;

            string tempPhotoPath = Path.Combine(tempDirectoryPath, tempfileName);
            if (!Directory.Exists(Server.MapPath(tempDirectoryPath)))
            {
                Directory.CreateDirectory(Server.MapPath(tempDirectoryPath));
            }
            if (File.Exists(tempPhotoPath))
            {
                File.Delete(tempfileName);
                //tempfileName = "1" + tempfileName;
                tempPhotoPath = Path.Combine(tempDirectoryPath, tempfileName);
            }
            fileUploadPhoto.PostedFile.SaveAs(Server.MapPath(tempPhotoPath));

            string fileExtension = Path.GetExtension(tempPhotoPath).ToLower();

            string fileNameToSave = uniqueFileNameString + fileExtension;

            string fullPhotoPath = Path.Combine(fullDirectoryPath, fileNameToSave);
            if (!Directory.Exists(Server.MapPath(fullDirectoryPath)))
            {
                Directory.CreateDirectory(Server.MapPath(fullDirectoryPath));
                imgPhoto.ImageUrl = fullDirectoryPath;
            }
            if (File.Exists(Server.MapPath(fullPhotoPath)))
            {
                System.IO.File.Delete(Server.MapPath(fullPhotoPath));
            }

            ImageUtility.ResizeImage(Server.MapPath(tempPhotoPath), Server.MapPath(fullPhotoPath), y, x);

            try
            {
                System.IO.File.Delete(Server.MapPath(tempPhotoPath));

            }
            catch { }
        }
        catch { return ""; }
        finally { }

        return uniqueFileNameString;
    }

    private string PhotoUploadProcess()
    {
        string photoName = "";
        try
        {
            if (this.FileUploadPhoto.HasFile)
            {
                #region Check File Type

                string fileName = FileUploadPhoto.FileName;
                string fileExtension = Path.GetExtension(fileName);
                fileExtension = fileExtension.ToLower();

                string[] acceptedFileTypes = new string[3];
                acceptedFileTypes[0] = ".jpg";
                acceptedFileTypes[1] = ".jpeg";
                acceptedFileTypes[2] = ".png";

                bool acceptFile = false;
                for (int i = 0; i < 3; i++)
                {
                    if (fileExtension == acceptedFileTypes[i])
                    {
                        acceptFile = true;
                        break;
                    }
                }
                if (!acceptFile)
                {
                    lblMsg.Text = "The file you are trying to upload is not a permitted file type!";
                    return "";
                }

                #endregion

                #region Check File Size

                int fileSize = FileUploadPhoto.PostedFile.ContentLength / 1024;
                if (fileSize >= 200)
                {
                    lblMsg.Text = "Filesize of image is too large. Maximum file size permitted is 200KB";
                    return "";
                }
                else
                {
                    //lblMsg.Text = fileSize.ToString();
                }

                #endregion

                string tempDirectoryPath = "~/Upload/TempAvatar/";
                string fullDirectoryPath = "~/Upload/Avatar/";

                photoName = UploadImage(tempDirectoryPath, fullDirectoryPath, FileUploadPhoto, 150, 150);
                if (photoName != "")
                {
                    hfPhotoRoll.Value = photoName + fileExtension;
                    imgPhoto.ImageUrl = fullDirectoryPath + photoName + fileExtension;
                    lblMsg.Text = "Photo Upload Complete";
                    photoName += fileExtension;
                }
            }
            else
            {
                lblMsg.Text = "Please select PHOTO first";
            }
        }
        catch { }
        finally { }

        return photoName;
    }

    private void CleanField()
    {
        try
        {
            txtStudentID.Text = "";

            hfPhotoRoll.Value = "";
            hfStudent.Value = "0";
            hfRoll.Value = "";
            hfPerson.Value = "0";
            hfPresent.Value = "0";
            hfPermanent.Value = "0";
            hfGuardian.Value = "0";
            hfMailing.Value = "0";
            hfPresentType.Value = "0";
            hfPermanentType.Value = "0";
            hfPermanentType.Value = "0";
            hfMailingType.Value = "0";

            txtFullName.Text = "";
            ddlGender.SelectedValue = "";
            ddlBloodGroup.SelectedValue = "";
            txtPhone.Text = "";
            txtNationality.Text = "";
            txtFatherName.Text = "";
            txtMotherName.Text = "";
            txtDOB.Text = "";
            ddlMatrialStatus.SelectedValue = "";
            ddlReligion.SelectedValue = "";
            txtEmail.Text = "";
            txtGuardianName.Text = "";
            txtFatherProfession.Text = "";
            txtMotherProfession.Text = "";
            txtPresent.Text = "";
            txtPermanent.Text = "";
            txtGuardian.Text = "";
            txtMailing.Text = "";
            txtSMSContactGuardian.Text = "";
            txtSMSContactSelf.Text = "";
        }
        catch { }
        finally { }
    }

    #endregion

    #region Event

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string phoneName = PhotoUploadProcess();
        }
        catch { }
        finally { }
    }

    protected void btnSave_Click(Object sender, EventArgs e)
    {
        try
        {
            int modifiedId = 99;
            string loginId = UtilityManager.Decrypt(Request.QueryString["loginid"]);
            User user = UserManager.GetByLogInId(loginId);
            if (user != null)
                modifiedId = user.User_ID;

            int personId = Convert.ToInt32(hfPerson.Value);
            int studentId = Convert.ToInt32(hfStudent.Value);
            int addressIdPresent = Convert.ToInt32(hfPresent.Value);
            int addressIdPermanent = Convert.ToInt32(hfPermanent.Value);
            int addressIdGuardian = Convert.ToInt32(hfGuardian.Value);
            int addressIdMailingAddress = Convert.ToInt32(hfMailing.Value);

            

            Person person = PersonManager.GetById(personId);
            if (person != null)
            {

                person.FullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtFullName.Text.ToString().ToLower());
                person.Gender = ddlGender.SelectedValue != "" ? ddlGender.SelectedItem.Text : "";
                person.BloodGroup = ddlBloodGroup.SelectedValue != "" ? ddlBloodGroup.SelectedItem.Text : "";
                person.Phone = txtPhone.Text;
                person.Nationality = txtNationality.Text;
                person.FatherName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtFatherName.Text.ToString().ToLower());
                person.MotherName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtMotherName.Text.ToString().ToLower());
                person.DOB = string.IsNullOrEmpty(txtDOB.Text.Trim()) ? DateTime.Now : DateTime.ParseExact(txtDOB.Text.Trim(), "dd/MM/yyyy", null);
                person.MatrialStatus = ddlMatrialStatus.SelectedValue != "" ? ddlMatrialStatus.SelectedItem.Text : "";
                person.ReligionId = Convert.ToInt32(ddlReligion.SelectedItem.Value);
                person.Email = txtEmail.Text;
                person.GuardianName = txtGuardianName.Text;
                person.FatherProfession = txtFatherProfession.Text;
                person.MotherProfession = txtMotherProfession.Text;
                person.SMSContactGuardian = txtSMSContactGuardian.Text;
                person.SMSContactSelf = txtSMSContactSelf.Text;

                if (PhotoUploadProcess() != "")
                    person.PhotoPath = PhotoUploadProcess();
                else
                {
                    if (hfPhotoRoll.Value != "")
                        person.PhotoPath = hfPhotoRoll.Value;
                }
                person.ModifiedBy = modifiedId;
                person.ModifiedDate = DateTime.Now;

                bool resultUpdatePerson = PersonManager.Update(person);
                if (resultUpdatePerson)
                {
                    #region Present Address INSERT/UPDATE
                    int presentAddressId = Convert.ToInt32(hfPresent.Value);
                    if (presentAddressId != 0)
                    {
                        Address presentAddress = AddressManager.GetById(presentAddressId);
                        if (txtPresent.Text != "")
                        {
                            presentAddress.AddressLine = txtPresent.Text;
                            presentAddress.ModifiedBy = modifiedId;
                            presentAddress.ModifiedDate = DateTime.Now;

                            bool resultPresentAddress = AddressManager.Update(presentAddress);
                        }
                    }
                    else
                    {
                        Address presentAddress = new Address();
                        if (txtPresent.Text != "")
                        {
                            presentAddress.PersonId = Convert.ToInt32(hfPerson.Value);
                            presentAddress.AddressTypeId = Convert.ToInt32(hfPresentType.Value);
                            presentAddress.AddressLine = txtPresent.Text;
                            presentAddress.CreatedBy = modifiedId;
                            presentAddress.CreatedDate = DateTime.Now;

                            int resultPresentAddress = AddressManager.Insert(presentAddress);
                        }
                    }
                    #endregion

                    #region Permanent Address INSERT/UPDATE

                    int permanentAddressId = Convert.ToInt32(hfPermanent.Value);
                    if (permanentAddressId != 0)
                    {
                        Address permanentAddress = AddressManager.GetById(permanentAddressId);
                        if (txtPermanent.Text != "")
                        {
                            permanentAddress.AddressLine = txtPermanent.Text;
                            permanentAddress.ModifiedBy = modifiedId;
                            permanentAddress.ModifiedDate = DateTime.Now;

                            bool resultPermanentAddress = AddressManager.Update(permanentAddress);
                        }
                    }
                    else
                    {
                        Address permanentAddress = new Address();
                        if (txtPermanent.Text != "")
                        {
                            permanentAddress.PersonId = Convert.ToInt32(hfPerson.Value);
                            permanentAddress.AddressTypeId = Convert.ToInt32(hfPermanentType.Value);
                            permanentAddress.AddressLine = txtPermanent.Text;
                            permanentAddress.CreatedBy = modifiedId;
                            permanentAddress.CreatedDate = DateTime.Now;

                            int resultPermanentAddress = AddressManager.Insert(permanentAddress);
                        }
                    }

                    #endregion

                    #region Guardian Address INSERT/UPDATE

                    int guardianAddressId = Convert.ToInt32(hfGuardian.Value);
                    if (guardianAddressId != 0)
                    {
                        Address guardianAddress = AddressManager.GetById(guardianAddressId);
                        if (txtGuardian.Text != "")
                        {
                            guardianAddress.AddressLine = txtGuardian.Text;
                            guardianAddress.ModifiedBy = modifiedId;
                            guardianAddress.ModifiedDate = DateTime.Now;

                            bool resultGuardianAddress = AddressManager.Update(guardianAddress);
                        }
                    }
                    else
                    {
                        Address guardianAddress = new Address();
                        if (txtGuardian.Text != "")
                        {
                            guardianAddress.PersonId = Convert.ToInt32(hfPerson.Value);
                            guardianAddress.AddressTypeId = Convert.ToInt32(hfGuardianType.Value);
                            guardianAddress.AddressLine = txtGuardian.Text;
                            guardianAddress.CreatedBy = modifiedId;
                            guardianAddress.CreatedDate = DateTime.Now;

                            int resultGuardianAddress = AddressManager.Insert(guardianAddress);
                        }
                    }

                    #endregion

                    #region Mailing Address INSERT/UPDATE

                    int mailingAddressId = Convert.ToInt32(hfMailing.Value);
                    if (mailingAddressId != 0)
                    {
                        Address mailingAddress = AddressManager.GetById(mailingAddressId);
                        if (txtMailing.Text != "")
                        {
                            mailingAddress.AddressLine = txtMailing.Text;
                            mailingAddress.ModifiedBy = modifiedId;
                            mailingAddress.ModifiedDate = DateTime.Now;

                            bool resultMailingAddress = AddressManager.Update(mailingAddress);
                        }
                    }
                    else
                    {
                        Address mailingAddress = new Address();
                        if (txtMailing.Text != "")
                        {
                            mailingAddress.PersonId = Convert.ToInt32(hfPerson.Value);
                            mailingAddress.AddressTypeId = Convert.ToInt32(hfMailingType.Value);
                            mailingAddress.AddressLine = txtMailing.Text;
                            mailingAddress.CreatedBy = modifiedId;
                            mailingAddress.CreatedDate = DateTime.Now;

                            int resultMailingAddress = AddressManager.Insert(mailingAddress);
                        }
                    }

                    #endregion

                    lblMsg.Text = "Saved";
                    if (Session["Role"].ToString().ToLower() != "student")
                        CleanField();
                }
            }


            if ((string.IsNullOrEmpty(user.Person.FatherName) || string.IsNullOrEmpty(user.Person.MotherName) || string.IsNullOrEmpty(user.Person.GuardianName) || string.IsNullOrEmpty(user.Person.SMSContactSelf)))
            {
                lblMsg.Text = "Exam Center ,Father Name, Mother Name, Self SMS Phone Number, Guardian Name Must not be empty";
                return;
            }

        }
        catch { }
        finally { }
    }

    protected void btnLoad_Click(Object sender, EventArgs e)
    {
        try
        {
            if (txtStudentID.Text != "")
                LoadingPersonInformation(txtStudentID.Text);
            else
                lblMsg.Text = "Please input STUDENT ID";
        }
        catch { }
        finally { }
    }

    #endregion
}