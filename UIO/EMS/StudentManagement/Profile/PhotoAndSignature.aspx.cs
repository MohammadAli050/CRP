using CommonUtility;
using EMS.Module;
using LogicLayer.BusinessObjects;
using System;
using System.Linq;
using System.Web.UI;

namespace EMS.bup
{
    public partial class PhotoAndSignature : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;

        UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            //base.CheckPage_Load();
            if (!IsPostBack)
            {
                hdnPersonId.Value = "0";
                LoadImagesAndSignature();
            }
        }
        private void LoadImagesAndSignature()
        {
            try
            {
                Student student = SessionManager.GetObjFromSession<Student>("StudentEdit");

                if (student != null)
                {
                    lblStudentId.Text = " ,    Student ID : " + student.Roll;
                    hdnPersonId.Value = student.PersonID.ToString();

                    var person = ucamContext.People.Where(x => x.PersonID == student.PersonID).FirstOrDefault();
                    if (person != null)
                    {
                        imgPhoto.ImageUrl = person.PhotoBytes != null ? "data:image/png;base64," + person.PhotoBytes : (student.Gender == "Female" ? "../Upload/Avatar/Female.jpg" : "../Upload/Avatar/Male.jpg");
                        imgSig.ImageUrl = person.SignatureBytes != null ? "data:image/png;base64," + person.SignatureBytes : (student.Gender == "Female" ? "../Upload/Avatar/Female_Signature.jpg" : "../Upload/Avatar/Male_Signature.jpg");
                    }

                }

            }
            catch (Exception ex)
            {
                // Log the exception
            }
        }

        protected void btnUploadPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                int PersonID = Convert.ToInt32(hdnPersonId.Value);
                if (PersonID == 0)
                {
                    showAlert("Person not found");
                    return;
                }

                string base64String = "";
                if (FileUploadPhoto.HasFile)
                {
                    #region Check File Size is less than 200kb and File type is .jpeg,.jpg or .png
                    if (!MisscellaneousCommonMethods.IsFileSizeOk(FileUploadPhoto, 200))
                    {
                        showAlert("File size must be less than 200kb");
                        return;
                    }

                    string[] allowedExtensions = { ".jpeg", ".jpg", ".png" };
                    if (!MisscellaneousCommonMethods.IsFileTypeOk(FileUploadPhoto, allowedExtensions))
                    {
                        showAlert("Only .jpeg, .jpg or .png file types are allowed");
                        return;
                    }
                    #endregion

                    byte[] photoBytes = FileUploadPhoto.FileBytes;
                    base64String = Convert.ToBase64String(photoBytes);

                    var person = ucamContext.People.Where(x => x.PersonID == PersonID).FirstOrDefault();
                    if (person != null)
                    {
                        person.PhotoBytes = base64String;
                        person.ModifiedBy = userObj.Id;
                        person.ModifiedDate = DateTime.Now;
                        ucamContext.SaveChanges();
                        showAlert("Photo Upload Complete");

                        // Show the uploaded photo in imgPhoto control
                        imgPhoto.ImageUrl = "data:image/png;base64," + base64String;
                        imgPhoto.Width = imgPhoto.Width;
                        imgPhoto.Height = imgPhoto.Height;
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
                int PersonID = Convert.ToInt32(hdnPersonId.Value);
                if (PersonID == 0)
                {
                    showAlert("Plz save teacher info first");
                    return;
                }

                #region Check File Size is less than 100kb and File type is .jpeg,.jpg or .png
                if (!MisscellaneousCommonMethods.IsFileSizeOk(FileUploadSignature, 100))
                {
                    showAlert("File size must be less than 100kb");
                    return;
                }

                string[] allowedExtensions = { ".jpeg", ".jpg", ".png" };
                if (!MisscellaneousCommonMethods.IsFileTypeOk(FileUploadSignature, allowedExtensions))
                {
                    showAlert("Only .jpeg, .jpg or .png file types are allowed");
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
                        person.ModifiedBy = userObj.Id;
                        person.ModifiedDate = DateTime.Now;
                        ucamContext.SaveChanges();
                        showAlert("Signature Upload Complete");

                        // Show the uploaded photo in imgPhoto control
                        imgSig.ImageUrl = "data:image/png;base64," + base64String;
                        imgSig.Width = imgSig.Width;
                        imgSig.Height = imgSig.Height;

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
    }

}