using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.CommonLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.Module.admin.Setup
{
    public partial class DateTimeSetupNew : BasePage
    {

        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.DateTimeSetupNew);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.DateTimeSetupNew));
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
                divSaveUpdateButton.Visible = false;
                btnUpdateSelected.Visible = false;
                // string result = DateSetUp.PreAdvising("011082011");
                LoadCalenderType();
                LoadSession();
                gvDateTimeSetUp.Visible = false;
                LoadCamboBox();
                btnSaveUpdate.Text = "Save";
                btnCancel.Visible = false;
                DateTime startDate = DateTime.Now;
                DateTime endDate = DateTime.Now;

                DateFromTextBox.Text = startDate.ToString("dd/MM/yyyy");
                DateToTextBox.Text = endDate.ToString("dd/MM/yyyy");
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
            divSaveUpdateButton.Visible = false;
            LoadSession();
            LoadProgramByUnitMaster();
        }

        void LoadCamboBox()
        {
            FillProgramComboBox();
            FillActivityTypeCombo();
            FillStartTime("09:00 AM");
            FillEndTime("11:59 PM");

        }

        void FillStartTime(string time)
        {
            try
            {
                DateTime dt = DateTime.Parse(time);
                MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
                if (dt.ToString("tt") == "AM")
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                }
                else
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                }
                TimeSelector1.SetTime(dt.Hour, dt.Minute, am_pm);

            }
            catch (Exception ex)
            { }
        }

        void FillEndTime(string time)
        {
            try
            {
                DateTime dt = DateTime.Parse(time);
                MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
                if (dt.ToString("tt") == "AM")
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                }
                else
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                }
                TimeSelector2.SetTime(dt.Hour, dt.Minute, am_pm);

            }
            catch (Exception ex)
            { }
        }

        void FillProgramComboBox()
        {
            int CalenderMasterId = Convert.ToInt32(ddlCalenderType.SelectedValue);
            ucAccessableProgram.LoadDropdownWithUserAccessByCalenderMaster(userObj.Id, CalenderMasterId);
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
                ddlActivityType.Items.Clear();
                ddlActivityType.Items.Add(new ListItem("- All -", "0"));
                ddlActivityType.AppendDataBoundItems = true;

                List<Value> valueList = ValueManager.GetByValueSetId(2);
                if (valueList != null && valueList.Count > 0)
                {
                    ddlActivityType.DataSource = valueList;
                    ddlActivityType.DataBind();
                }
            }
            catch { }
            finally { }
        }

        void InsertUpdate(SetUpDateForProgram temp)
        {
            try
            {
                if (btnSaveUpdate.Text == "Save")
                {
                    List<SetUpDateForProgram> setUpDateForProgramList = SetUpDateForProgramManager.GetAll(Convert.ToInt32(temp.AcaCalId), temp.ProgramId, temp.ActivityTypeId);
                    if (setUpDateForProgramList.Count > 0 && setUpDateForProgramList != null)
                    {
                        showAlert("Already Exist");
                        return;
                    }
                    else
                    {
                        int resultSave = SetUpDateForProgramManager.Insert(temp);
                        if (resultSave > 0)
                        {
                            InsertLog("New Date Time Add", userObj.LogInID + " Added a datetime for Program : " + ucAccessableProgram.selectedText.ToString() + " , Session : " + ddlAcaCalBatch.SelectedItem.Text.ToString() + " and Type : " + ddlActivityType.SelectedItem.Text.ToString()
                                + " with From DateTime : " + temp.StartDate.ToString("dd/MM/yyyy") + "_" + Convert.ToDateTime(temp.StartTime).ToString("hh:mm:ss tt") + " and To DateTime : " + temp.EndDate.ToString("dd/MM/yyyy") + "_" + Convert.ToDateTime(temp.EndTime).ToString("hh:mm:ss tt"));

                            showAlert("Save Successfully");

                            ClearField();
                            Load(Convert.ToInt32(ddlAcaCalBatch.SelectedValue), Convert.ToInt32(ucAccessableProgram.selectedValue), 0);
                            return;
                        }
                        else
                        {

                            showAlert("Save Fail");
                            return;
                        }
                    }
                }
                else
                {
                    string st = "Active";
                    if (!temp.IsActive)
                        st = "In-" + st;

                    bool resultUpdate = SetUpDateForProgramManager.Update(temp);
                    if (resultUpdate)
                    {
                        InsertLog("Update Date Time", userObj.LogInID + " Updated datetime for Program : " + ucAccessableProgram.selectedText.ToString() + " , Session : " + ddlAcaCalBatch.SelectedItem.Text.ToString() + ", Type : " + ddlActivityType.SelectedItem.Text.ToString() + " and Status : " + st
                         + " with From DateTime : " + temp.StartDate.ToString("dd/MM/yyyy") + "_" + Convert.ToDateTime(temp.StartTime).ToString("hh:mm:ss tt") + " and To DateTime : " + temp.EndDate.ToString("dd/MM/yyyy") + "_" + Convert.ToDateTime(temp.EndTime).ToString("hh:mm:ss tt"));

                        showAlert("Update Successfully");
                        btnSaveUpdate.Text = "Save";
                        btnCancel.Visible = false;
                        btnSaveUpdate.CommandArgument = "";
                        ClearField();
                        EnabledDropDown();
                        Load(Convert.ToInt32(ddlAcaCalBatch.SelectedValue), Convert.ToInt32(ucAccessableProgram.selectedValue), 0);
                        return;
                    }
                    else
                    {

                        showAlert("Update Fail");
                        return;
                    }
                }
            }
            catch { }
            finally { }
        }

        bool CheckFieldStatus()
        {
            try
            {
                if (ddlAcaCalBatch.SelectedValue == "0" || ddlActivityType.SelectedValue == "0")
                {
                    showAlert("Please select dropdown value");
                    return false;
                }

                else
                    return true;
            }
            catch { return false; }
            finally { }
        }

        void ClearField()
        {
            try
            {
                chkIsActive.Checked = false;
            }
            catch { }
            finally { }
        }

        void ClearDropDown()
        {
            //ddlAcaCalBatch.SelectedValue = "0";
            //ucAccessableProgram.SelectedValue(0);
            ddlActivityType.SelectedValue = "0";
        }

        void DisabledDropDown()
        {
            ddlAcaCalBatch.Enabled = false;
            ucAccessableProgram.DropDownDisable();
            ddlActivityType.Enabled = false;
        }

        void EnabledDropDown()
        {
            ddlAcaCalBatch.Enabled = true;
            ucAccessableProgram.DropDownEnable();
            ddlActivityType.Enabled = true;
        }

        void GridInvisible()
        {
            gvDateTimeSetUp.DataSource = null;
            gvDateTimeSetUp.DataBind();
            gvDateTimeSetUp.Visible = false;
        }

        void Load(int acaCalId, int programId, int typeId)
        {
            try
            {
                divSaveUpdateButton.Visible = true;
                List<SetUpDateForProgram> setUpDateForProgramList = SetUpDateForProgramManager.GetAll(acaCalId, programId, typeId);
                if (setUpDateForProgramList != null && setUpDateForProgramList.Any())
                {

                    gvDateTimeSetUp.Visible = true;

                    #region Hash Table

                    List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
                    Hashtable acaCalHash = new Hashtable();
                    foreach (AcademicCalender acaCal in academicCalenderList)
                        acaCalHash.Add(acaCal.AcademicCalenderID, acaCal.FullCode);

                    List<Program> programList = ProgramManager.GetAll();
                    Hashtable programHash = new Hashtable();
                    foreach (Program program in programList)
                        programHash.Add(program.ProgramID, program.ShortName);

                    Hashtable valueHash = new Hashtable();
                    List<ValueSet> valueSetList = ValueSetManager.GetAll();
                    if (valueSetList.Count > 0 && valueSetList != null)
                    {
                        valueSetList = valueSetList.Where(x => x.ValueSetName == "ActivityType").ToList();
                        if (valueSetList.Count > 0 && valueSetList != null)
                        {
                            List<Value> valueList = ValueManager.GetByValueSetId(valueSetList[0].ValueSetID);
                            if (valueList.Count > 0 && valueList != null)
                            {
                                foreach (Value value in valueList)
                                    valueHash.Add(value.ValueID, value.ValueName);
                            }
                        }
                    }

                    #endregion

                    foreach (SetUpDateForProgram temp in setUpDateForProgramList)
                    {
                        temp.AcaCalName = acaCalHash.ContainsKey(temp.AcaCalId) == true ? acaCalHash[temp.AcaCalId].ToString() : "";
                        temp.ProgramName = programHash.ContainsKey(temp.ProgramId) == true ? programHash[temp.ProgramId].ToString() : "";
                        temp.TypeName = valueHash.ContainsKey(temp.ActivityTypeId) == true ? valueHash[temp.ActivityTypeId].ToString() : "";
                    }

                    gvDateTimeSetUp.DataSource = setUpDateForProgramList.OrderBy(x => x.ProgramName).ToList();
                    gvDateTimeSetUp.DataBind();
                }
                else
                {
                    gvDateTimeSetUp.Visible = true;
                    gvDateTimeSetUp.DataSource = null;
                    gvDateTimeSetUp.DataBind();
                }
            }
            catch (Exception ex) { }
            finally { }
        }

        #endregion

        #region Event

        protected void AcaCal_Change(Object sender, EventArgs e)
        {
            try
            {
                GridInvisible();

                ddlActivityType.SelectedValue = "0";
            }
            catch { }
            finally { }
        }

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

        protected void Program_Change(Object sender, EventArgs e)
        {
            try
            {
                divSaveUpdateButton.Visible = false;
                GridInvisible();
            }
            catch { }
            finally { }
        }

        protected void ActivityType_Change(Object sender, EventArgs e)
        {
            try
            {
                divSaveUpdateButton.Visible = false;
                GridInvisible();
                //InsertNewEntry();
            }
            catch { }
            finally { }
        }

        private void InsertNewEntry()
        {
            try
            {
                int AcacalId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
                int ProgramId = Convert.ToInt32(ucAccessableProgram.selectedValue);
                int TypeId = Convert.ToInt32(ddlActivityType.SelectedValue);

                if (ProgramId != 0)
                {
                    InsertNewEntryIfNotExists(AcacalId, ProgramId, TypeId);
                }
                else
                {
                    List<Program> programList = new List<Program>();
                    if (BaseCurrentUserObj.RoleID == 1)
                        programList = ProgramManager.GetAll();

                    AcademicCalender ac = AcademicCalenderManager.GetById(AcacalId);

                    if (programList != null && programList.Any())
                    {
                        programList = programList.Where(x => x.CalenderUnitMasterID == ac.CalenderUnitType.CalenderUnitMasterID).ToList();

                        foreach (var item in programList)
                        {
                            InsertNewEntryIfNotExists(AcacalId, item.ProgramID, TypeId);
                        }

                    }

                }

            }
            catch (Exception ex)
            {
            }
        }

        private void InsertNewEntryIfNotExists(int AcacalId, int ProgramId, int TypeId)
        {
            try
            {
                SetUpDateForProgram setUpDateForProgram = null;
                var SetupList = SetUpDateForProgramManager.GetAll(AcacalId, ProgramId, TypeId);
                if (SetupList != null && SetupList.Any())
                    setUpDateForProgram = SetupList[0];


                if (setUpDateForProgram == null)
                {
                    SetUpDateForProgram newObj = new SetUpDateForProgram();

                    newObj.ActivityTypeId = TypeId;
                    newObj.AcaCalId = AcacalId;
                    newObj.ProgramId = ProgramId;
                    newObj.IsActive = false;
                    newObj.StartDate = Convert.ToDateTime("01/01/1900");
                    newObj.EndDate = Convert.ToDateTime("01/01/1900");
                    newObj.StartTime = DateTime.Now;
                    newObj.EndTime = DateTime.Now;
                    newObj.CreatedBy = BaseCurrentUserObj.Id;
                    newObj.CreatedDate = DateTime.Now;

                    SetUpDateForProgramManager.Insert(newObj);

                    InsertLog("New Date Time Add", userObj.LogInID + " Added a datetime for Program : " + ucAccessableProgram.selectedText.ToString() + " , Session : " + ddlAcaCalBatch.SelectedItem.Text.ToString() + " and Type : " + ddlActivityType.SelectedItem.Text.ToString()
                                + " with From DateTime : " + newObj.StartDate.ToString("dd/MM/yyyy") + " and To DateTime : " + newObj.EndDate.ToString("dd/MM/yyyy"));
                }


            }
            catch (Exception ex)
            {
            }
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

                Load(acaCalId, programId, typeId);
            }
            catch
            {
                GridInvisible();
                showAlert("Please Select Program and Semester.");
                return;
            }
            finally { }
        }

        protected void SaveUpdate_Click(Object sender, EventArgs e)
        {
            try
            {
                bool checkStatus = CheckFieldStatus();
                if (!checkStatus)
                    return;

                BussinessObject.UIUMSUser userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
                if (userObj != null)
                {
                    User user = UserManager.GetByLogInId(userObj.LogInID);
                    if (user != null)
                    {
                        int userId = user.User_ID;

                        SetUpDateForProgram temp = new SetUpDateForProgram();

                        if (btnSaveUpdate.Text == "Save")
                        {
                            temp.CreatedBy = userId;
                            temp.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            int id = Convert.ToInt32(btnSaveUpdate.CommandArgument);
                            temp = SetUpDateForProgramManager.GetById(id);
                            if (temp != null)
                            {
                                temp.ModifiedBy = userId;
                                temp.ModifiedDate = DateTime.Now;
                            }
                            else
                            {
                                showAlert("Error: 1210");
                                return;
                            }
                        }

                        int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
                        int programId = Convert.ToInt32(ucAccessableProgram.selectedValue);
                        int typeId = Convert.ToInt32(ddlActivityType.SelectedValue);

                        temp.ActivityTypeId = typeId;
                        temp.AcaCalId = acaCalId;
                        temp.ProgramId = programId;
                        temp.IsActive = chkIsActive.Checked;

                        DateTime startTime = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", TimeSelector1.Hour, TimeSelector1.Minute, TimeSelector1.Second, TimeSelector1.AmPm));
                        DateTime endTime = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", TimeSelector2.Hour, TimeSelector2.Minute, TimeSelector2.Second, TimeSelector2.AmPm));

                        temp.StartTime = startTime;
                        temp.EndTime = endTime;

                        DateTime FromDate = DateTime.ParseExact(DateFromTextBox.Text.Replace("/", string.Empty), "ddMMyyyy", null);
                        DateTime ToDate = DateTime.ParseExact(DateToTextBox.Text.Replace("/", string.Empty), "ddMMyyyy", null);

                        temp.StartDate = FromDate;
                        temp.EndDate = ToDate;

                        InsertUpdate(temp);
                    }
                }
                else
                {
                    Response.Redirect("~/Security/Login.aspx");
                }
            }
            catch { }
            finally { }
        }

        protected void lbUpdate_Click(Object sender, EventArgs e)
        {
            try
            {
                LinkButton linkButton = new LinkButton();
                linkButton = (LinkButton)sender;
                int id = Convert.ToInt32(linkButton.CommandArgument);

                SetUpDateForProgram temp = SetUpDateForProgramManager.GetById(id);
                if (temp != null)
                {
                    btnSaveUpdate.Text = "Update";
                    btnCancel.Visible = true;
                    btnSaveUpdate.CommandArgument = id.ToString();

                    ddlAcaCalBatch.SelectedValue = temp.AcaCalId.ToString();
                    //ucAccessableProgram.LoadDropDownList();

                    LoadProgramByUnitMaster();

                    ucAccessableProgram.SelectedValue(temp.ProgramId);
                    ddlActivityType.SelectedValue = temp.ActivityTypeId.ToString();

                    DateTime startDate = (DateTime)temp.StartDate;
                    DateTime endDate = (DateTime)temp.EndDate;

                    DateFromTextBox.Text = startDate.ToString("dd/MM/yyyy");
                    DateToTextBox.Text = endDate.ToString("dd/MM/yyyy");

                    DateTime StartTime = (DateTime)temp.StartTime;
                    DateTime EndTime = (DateTime)temp.EndTime;

                    FillStartTime(StartTime.Hour + ":" + StartTime.Minute + "" + StartTime.ToString("tt", CultureInfo.InvariantCulture));
                    FillEndTime(EndTime.Hour + ":" + EndTime.Minute + "" + EndTime.ToString("tt", CultureInfo.InvariantCulture));

                    DisabledDropDown();
                    chkIsActive.Checked = temp.IsActive;
                }
            }
            catch { }
            finally { }
        }

        protected void lbDelete_Click(Object sender, EventArgs e)
        {
            try
            {
                LinkButton linkButton = new LinkButton();
                linkButton = (LinkButton)sender;
                int id = Convert.ToInt32(linkButton.CommandArgument);
                SetUpDateForProgram temp = SetUpDateForProgramManager.GetById(id);

                bool resultDelte = SetUpDateForProgramManager.Delete(id);
                if (resultDelte)
                {
                    InsertLog("Delete Date Time", userObj.LogInID + " deleted a datetime for Program : " + ucAccessableProgram.selectedText.ToString() + " , Session : " + ddlAcaCalBatch.SelectedItem.Text.ToString() + " and Type : " + ddlActivityType.SelectedItem.Text.ToString()
                        + " with From DateTime : " + temp.StartDate.ToString("dd/MM/yyyy") + "_" + Convert.ToDateTime(temp.StartTime).ToString("hh:mm:ss tt") + " and To DateTime : " + temp.EndDate.ToString("dd/MM/yyyy") + "_" + Convert.ToDateTime(temp.EndTime).ToString("hh:mm:ss tt"));

                    showAlert("Delete Successfully");
                    Load_Click(null, null);
                    return;
                }
                else
                {
                    showAlert("Delete Fail");
                    return;
                }
            }
            catch { }
            finally { }
        }

        protected void Cancel_Click(Object sender, EventArgs e)
        {
            try
            {
                ClearDropDown();
                ClearField();
                EnabledDropDown();
                btnSaveUpdate.Text = "Save";
                btnCancel.Visible = false;
                btnSaveUpdate.Visible = true;
                uncheckedAllCheckBox();
            }
            catch { }
            finally { }
        }

        private void uncheckedAllCheckBox()
        {
            try
            {
                foreach (GridViewRow row in gvDateTimeSetUp.Rows)
                {
                    CheckBox ckBox = (CheckBox)row.FindControl("ChkActive");
                    if (ckBox.Checked)
                    {
                        ckBox.Checked = false;
                    }
                }
                btnCancel.Visible = false;
                btnUpdateSelected.Visible = false;
            }
            catch (Exception ex)
            {
            }
        }
        private void InsertLog(string EventName, string Message)
        {
            LogGeneralManager.Insert(
                                      DateTime.Now,
                                      "",
                                      "",
                                      userObj.LogInID,
                                      "",
                                      "",
                                      EventName,
                                      Message,
                                      "Normal",
                                      _pageId,
                                      _pageName,
                                      _pageUrl,
                                      "");
        }
        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            divSaveUpdateButton.Visible = false;
            GridInvisible();
            ddlActivityType.SelectedValue = "0";
            //FillAcademicCalenderCombo(Convert.ToInt32(ucAccessableProgram.selectedValue));
        }
        #endregion




        protected void showAlert(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);

        }



        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)sender;

                if (chk.Checked)
                {
                    chk.Text = "Unselect All";
                    btnUpdateSelected.Visible = true;
                    btnSaveUpdate.Visible = false;
                    btnCancel.Visible = true;
                }
                else
                {
                    chk.Text = "Select All";
                    btnUpdateSelected.Visible = false;
                    btnSaveUpdate.Visible = true;
                    btnCancel.Visible = false;

                }

                foreach (GridViewRow row in gvDateTimeSetUp.Rows)
                {
                    CheckBox ckBox = (CheckBox)row.FindControl("ChkActive");
                    ckBox.Checked = chk.Checked;
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void ChkActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int Count = 0;

                foreach (GridViewRow row in gvDateTimeSetUp.Rows)
                {
                    CheckBox ckBox = (CheckBox)row.FindControl("ChkActive");
                    if (ckBox.Checked)
                    {
                        Count++;
                    }
                }
                if (Count > 0)
                {
                    btnUpdateSelected.Visible = true;
                    btnSaveUpdate.Visible = false;
                    btnCancel.Visible = true;
                }
                else
                {
                    btnUpdateSelected.Visible = false;
                    btnSaveUpdate.Visible = true;
                    btnCancel.Visible = false;

                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnUpdateSelected_Click(object sender, EventArgs e)
        {
            try
            {
                int Count = 0;
                foreach (GridViewRow row in gvDateTimeSetUp.Rows)
                {
                    CheckBox ckBox = (CheckBox)row.FindControl("ChkActive");
                    if (ckBox.Checked)
                    {

                        SetUpDateForProgram temp = new SetUpDateForProgram();


                        HiddenField Id = (HiddenField)row.FindControl("hdnId");

                        int SetupId = Convert.ToInt32(Id.Value);
                        if (SetupId > 0)
                        {

                            temp = SetUpDateForProgramManager.GetById(SetupId);

                            temp.IsActive = chkIsActive.Checked;

                            DateTime startTime = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", TimeSelector1.Hour, TimeSelector1.Minute, TimeSelector1.Second, TimeSelector1.AmPm));
                            DateTime endTime = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", TimeSelector2.Hour, TimeSelector2.Minute, TimeSelector2.Second, TimeSelector2.AmPm));

                            temp.StartTime = startTime;
                            temp.EndTime = endTime;

                            DateTime FromDate = DateTime.ParseExact(DateFromTextBox.Text.Replace("/", string.Empty), "ddMMyyyy", null);
                            DateTime ToDate = DateTime.ParseExact(DateToTextBox.Text.Replace("/", string.Empty), "ddMMyyyy", null);

                            temp.StartDate = FromDate;
                            temp.EndDate = ToDate;

                            temp.ModifiedBy = BaseCurrentUserObj.Id;
                            temp.ModifiedDate = DateTime.Now;


                            string st = "Active";
                            if (!temp.IsActive)
                                st = "In-" + st;

                            bool resultUpdate = SetUpDateForProgramManager.Update(temp);
                            if (resultUpdate)
                            {
                                InsertLog("Update Date Time", userObj.LogInID + " Updated datetime for Program : " + ucAccessableProgram.selectedText.ToString() + " , Session : " + ddlAcaCalBatch.SelectedItem.Text.ToString() + ", Type : " + ddlActivityType.SelectedItem.Text.ToString() + " and Status : " + st
                                 + " with From DateTime : " + temp.StartDate.ToString("dd/MM/yyyy") + "_" + Convert.ToDateTime(temp.StartTime).ToString("hh:mm:ss tt") + " and To DateTime : " + temp.EndDate.ToString("dd/MM/yyyy") + "_" + Convert.ToDateTime(temp.EndTime).ToString("hh:mm:ss tt"));

                                Count++;
                            }

                        }

                    }
                }

                if (Count > 0)
                {
                    showAlert("Updated Successfully");
                    btnCancel.Visible = false;
                    btnUpdateSelected.Visible = false;
                    btnSaveUpdate.Visible = true;
                    ClearField();
                    uncheckedAllCheckBox();
                    Load_Click(null, null);
                }

            }
            catch (Exception ex)
            {
            }
        }


    }
}