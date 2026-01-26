using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.Module.admin.Setup
{
    public partial class SystemImageConfiguration : BasePage
    {

        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.SystemImageConfiguration);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.SystemImageConfiguration));
        BussinessObject.UIUMSUser userObj = null;


        UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();

        #region Function

        protected void Page_Load(object sender, EventArgs e)
        {
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            base.CheckPage_Load();

            //lblMsg.Text = "";

            if (!IsPostBack)
            {
                LoadCalenderType();
                LoadSession();
                LoadCamboBox();
            }
        }

        private void LoadCalenderType()
        {
            try
            {
                var CalenderUnitMasterList = ucamContext.CalenderUnitMasters.ToList();

                ddlCalenderType.Items.Clear();
                ddlCalenderType.AppendDataBoundItems = true;
                ddlCalenderType.Items.Add(new ListItem("Select", "0"));

                if (CalenderUnitMasterList != null && CalenderUnitMasterList.Count > 0)
                {
                    ddlCalenderType.DataTextField = "Name";
                    ddlCalenderType.DataValueField = "CalenderUnitMasterID";
                    ddlCalenderType.DataSource = CalenderUnitMasterList.OrderBy(x => x.CalenderUnitMasterID).ToList();
                    ddlCalenderType.DataBind();
                }

            }
            catch (Exception ex)
            {
            }
        }

        protected void ddlCalenderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSession();
            LoadProgramByUnitMaster();
        }

        void LoadCamboBox()
        {
            FillProgramComboBox();
            FillActivityTypeCombo();
        }

        void FillProgramComboBox()
        {
            int CalenderMasterId = Convert.ToInt32(ddlCalenderType.SelectedValue);
            ucAccessableProgram.LoadDropdownWithUserAccessByCalenderMaster(userObj.Id, CalenderMasterId);
            ucBatch.LoadDropDownList(0);
        }

        void LoadSession()
        {
            try
            {

                List<AcademicCalender> academicCalenderList = new List<AcademicCalender>();
                int CalenderUnitMasterId = 0;
                try
                {
                    CalenderUnitMasterId = Convert.ToInt32(ddlCalenderType.SelectedValue);
                }
                catch (Exception ex)
                {
                }

                if (CalenderUnitMasterId > 0)
                    academicCalenderList = AcademicCalenderManager.GetAll(CalenderUnitMasterId);


                ddlAcaCalBatch.Items.Clear();
                ddlAcaCalBatch.Items.Add(new ListItem("- Select -", "0"));
                ddlAcaCalBatch.AppendDataBoundItems = true;

                if (academicCalenderList != null && academicCalenderList.Any())
                {
                    academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();
                    foreach (AcademicCalender academicCalender in academicCalenderList)
                        ddlAcaCalBatch.Items.Add(new ListItem(academicCalender.FullCode, academicCalender.AcademicCalenderID.ToString()));
                }

            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        void FillActivityTypeCombo()
        {
            try
            {
                var imageMetaList = ucamContext.SystemImageTypeMetaTables.ToList();

                ddlActivityType.Items.Clear();
                ddlActivityType.Items.Add(new ListItem("Select", "0"));
                ddlActivityType.AppendDataBoundItems = true;

                if (imageMetaList != null && imageMetaList.Any())
                {
                    ddlActivityType.DataTextField = "ImageType";
                    ddlActivityType.DataValueField = "Id";
                    ddlActivityType.DataSource = imageMetaList;
                    ddlActivityType.DataBind();
                }

            }
            catch { }
            finally { }
        }

        #endregion

        #region Event


        private void LoadProgramByUnitMaster()
        {
            try
            {
                int CUMId = 0;
                try
                {
                    CUMId = Convert.ToInt32(ddlCalenderType.SelectedValue);
                }
                catch (Exception ex)
                {
                }

                ucAccessableProgram.LoadDropdownWithUserAccessByCalenderMaster(BaseCurrentUserObj.Id, CUMId);
            }
            catch (Exception ex)
            {
            }
        }

        protected void ActivityType_Change(Object sender, EventArgs e)
        {
            try
            {
            }
            catch { }
            finally { }
        }
        protected void Load_Click(Object sender, EventArgs e)
        {
            try
            {
                int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
                int programId = Convert.ToInt32(ucAccessableProgram.selectedValue);
                int typeId = Convert.ToInt32(ddlActivityType.SelectedValue);

                if (acaCalId == 0)
                {
                    showAlert("Please select semester/trimester");
                    return;
                }

                if (typeId == 0)
                {
                    showAlert("Please select a type");
                    return;
                }

                LoadImages();
            }
            catch
            {
                showAlert("Please Select Program and Semester.");
                return;
            }
            finally { }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlActivityType.SelectedValue = "0";
            ucBatch.LoadDropDownList(Convert.ToInt32(ucAccessableProgram.selectedValue));
        }

        protected void ucBatch_BatchSelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void AcaCal_Change(Object sender, EventArgs e)
        {
            try
            {

                ddlActivityType.SelectedValue = "0";
            }
            catch { }
            finally { }
        }
        #endregion


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                try
                {
                    int AcacalId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
                    int ProgramId = Convert.ToInt32(ucAccessableProgram.selectedValue);
                    int BatchId = Convert.ToInt32(ucBatch.selectedValue);
                    int ImageTypeId = Convert.ToInt32(ddlActivityType.SelectedValue);
                    int UserId = Convert.ToInt32(userObj.Id);

                    if (AcacalId == 0)
                    {
                        showAlert("Please select a session");
                        return;
                    }

                    if(ImageTypeId==0)
                    {
                        showAlert("Please select a image type");
                        return;
                    }

                    // Validate file type
                    string fileExtension = Path.GetExtension(FileUpload1.FileName).ToLower();
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

                    bool isValidExtension = false;
                    foreach (string ext in allowedExtensions)
                    {
                        if (fileExtension == ext)
                        {
                            isValidExtension = true;
                            break;
                        }
                    }

                    if (!isValidExtension)
                    {
                        lblMessage.Text = "Please select a valid image file (jpg, jpeg, png, gif, bmp)";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    // Validate file size (max 2MB)
                    if (FileUpload1.PostedFile.ContentLength > 2097152)
                    {
                        lblMessage.Text = "File size must be less than 2MB";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    // Convert image to byte array
                  
                    string base64String = "";
                    if (FileUpload1.HasFile)
                    {
                        byte[] logoBytes = FileUpload1.FileBytes;
                        base64String = Convert.ToBase64String(logoBytes);
                    }

                    List<SqlParameter> parameters1 = new List<SqlParameter>();
                    parameters1.Add(new SqlParameter { ParameterName = "@AcacalId", SqlDbType = System.Data.SqlDbType.Int, Value = AcacalId });
                    parameters1.Add(new SqlParameter { ParameterName = "@ProgramId", SqlDbType = System.Data.SqlDbType.Int, Value = ProgramId });
                    parameters1.Add(new SqlParameter { ParameterName = "@BatchId", SqlDbType = System.Data.SqlDbType.Int, Value = BatchId });
                    parameters1.Add(new SqlParameter { ParameterName = "@ImageTypeId", SqlDbType = System.Data.SqlDbType.Int, Value = ImageTypeId });
                    parameters1.Add(new SqlParameter { ParameterName = "@UserId", SqlDbType = System.Data.SqlDbType.Int, Value = UserId });
                    parameters1.Add(new SqlParameter { ParameterName = "@ImageBytes", SqlDbType = System.Data.SqlDbType.NVarChar, Value = base64String });

                    DataTable dtImageList = DataTableManager.GetDataFromQuery("InsertOrUpdateSystemImage", parameters1);

                    if (dtImageList != null && dtImageList.Rows.Count > 0)
                    {
                        showAlert("Image uploaded successfully!");
                    }
                    else
                    {
                        showAlert("Image upload failed");
                    }

                    // Reload grid
                    LoadImages();
                }
                catch (Exception ex)
                {
                    showAlert("Error: " + ex.Message);
                }
            }
            else
            {
                showAlert("Please select an image file");
            }
        }

        private void LoadImages()
        {
            try
            {
                int AcacalId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
                int ProgramId = Convert.ToInt32(ucAccessableProgram.selectedValue);
                int BatchId = Convert.ToInt32(ucBatch.selectedValue);
                int ActivityTypeId = Convert.ToInt32(ddlActivityType.SelectedValue);

                var imageTypeList = (from data in ucamContext.SystemImageConfigurations
                                     join meta in ucamContext.SystemImageTypeMetaTables on data.ImageTypeId equals meta.Id
                                     join prg in ucamContext.Programs on data.ProgramId equals prg.ProgramID
                                     join btc in ucamContext.Batches on data.BatchId equals btc.BatchId
                                     where (data.ProgramId == ProgramId || ProgramId == 0)
                                     && (data.BatchId == BatchId || BatchId == 0)
                                     && data.AcacalId == AcacalId
                                     && data.ImageTypeId == ActivityTypeId
                                     select new
                                     {
                                         prg.ShortName,
                                         btc.BatchNO,
                                         data.ImageBytes
                                     }).ToList();

                if (imageTypeList != null && imageTypeList.Any())
                {
                    gvUploadedImage.DataSource = imageTypeList;
                    gvUploadedImage.DataBind();
                }
                else
                {
                    gvUploadedImage.DataSource = null;
                    gvUploadedImage.DataBind();
                }


            }
            catch (Exception ex)
            {
            }
        }

        protected void showAlert(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);

        }

        protected void gvUploadedImage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Image imgLogo = (Image)e.Row.FindControl("lblLogo");
                    Label lblImageBytes = (Label)e.Row.FindControl("lblImageBytes");

                    if (lblImageBytes != null && lblImageBytes.Text != "")
                    {
                        string base64String = lblImageBytes.Text;
                        imgLogo.ImageUrl = "data:image/png;base64," + base64String;
                    }
                    else
                    {
                        imgLogo.ImageUrl = "~/images/placeholder.png"; // optional fallback
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnCancelUpload_Click(object sender, EventArgs e)
        {

        }
    }
}