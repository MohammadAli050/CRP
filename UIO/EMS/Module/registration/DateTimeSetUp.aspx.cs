using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.CommonLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;


public partial class DateTimeSetUp : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        lblMsg.Text = "";

        if (!IsPostBack)
        {
            ucProgram.LoadDropDownList();
            //  string result = DateSetUp.PreAdvising("011082011");
            gvDateTimeSetUp.Visible = false;
            LoadCamboBox();
            btnCancel.Visible = false;
        }
    }

    void LoadCamboBox()
    {
        FillActivityTypeCombo();
    }



    void FillActivityTypeCombo()
    {
        try
        {
            ddlActivityType.Items.Clear();
            ddlActivityType.Items.Add(new ListItem("- All -", "0"));
            ddlActivityType.AppendDataBoundItems = true;

            List<Value> valueList = ValueManager.GetByValueSetId(8);
            ddlActivityType.DataSource = valueList.Where(x=>x.ValueID==28).ToList();
            ddlActivityType.DataBind();

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
                    lblMsg.Text = "Already Exist";
                }
                else
                {
                    int resultSave = SetUpDateForProgramManager.Insert(temp);
                    if (resultSave > 0)
                    {
                        lblMsg.Text = "Save Successfully";
                        ClearField();
                        Load(Convert.ToInt32(ucSession.selectedValue), Convert.ToInt32(ucProgram.selectedValue), 0);
                    }
                    else
                        lblMsg.Text = "Save Fail";
                }
            }
            else
            {
                bool resultUpdate = SetUpDateForProgramManager.Update(temp);
                if (resultUpdate)
                {
                    lblMsg.Text = "Update Successfully";
                    btnSaveUpdate.Text = "Save";
                    btnCancel.Visible = false;
                    btnSaveUpdate.CommandArgument = "";
                    ClearField();
                    EnabledDropDown();
                    Load(Convert.ToInt32(ucSession.selectedValue), Convert.ToInt32(ucProgram.selectedValue), 0);
                }
                else
                    lblMsg.Text = "Update Fail";
            }
        }
        catch { }
        finally { }
    }

    bool CheckFieldStatus()
    {
        try
        {
            if (ucSession.selectedValue == "0" || ucProgram.selectedValue == "0" || ddlActivityType.SelectedValue == "0")
            {
                lblMsg.Text = "Please select dropdown value";
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
        ucSession.selectedValue = "0";
        ucProgram.selectedValue = "0";
        ddlActivityType.SelectedValue = "0";
    }

    void DisabledDropDown()
    {
        ucSession.Visible = false;
        ucProgram.Visible = false;
        lblProgram.Visible = false;
        lblSem.Visible = false;
        ddlActivityType.Enabled = false;
    }

    void EnabledDropDown()
    {
        ucSession.Visible = true;
        ucProgram.Visible = true;
        lblProgram.Visible = true;
        lblSem.Visible = true;
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
            List<SetUpDateForProgram> setUpDateForProgramList = SetUpDateForProgramManager.GetAll(acaCalId, programId, typeId);

            if (setUpDateForProgramList.Count > 0 & setUpDateForProgramList != null)
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

                gvDateTimeSetUp.DataSource = setUpDateForProgramList;
                gvDateTimeSetUp.DataBind();
            }
            else
            {
                gvDateTimeSetUp.Visible = true;
                gvDateTimeSetUp.DataSource = null;
                gvDateTimeSetUp.DataBind();
            }
        }
        catch { }
        finally { }
    }

    private void ShowAlertMessage(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ServerControlScript", "alert('" + msg + "');", true);
    }

    #endregion

    #region Event

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }

    protected void AcaCal_Change(Object sender, EventArgs e)
    {
        try
        {
            GridInvisible();
        }
        catch { }
        finally { }
    }

    protected void Program_Change(Object sender, EventArgs e)
    {
        try
        {
            GridInvisible();
        }
        catch { }
        finally { }
    }

    protected void ActivityType_Change(Object sender, EventArgs e)
    {
        try
        {
            GridInvisible();
        }
        catch { }
        finally { }
    }

    protected void Load_Click(Object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int typeId = Convert.ToInt32(ddlActivityType.SelectedValue);

            Load(acaCalId, programId, typeId);
        }
        catch
        {
            GridInvisible();
            lblMsg.Text = "Error 9e01d1";
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

            if (!string.IsNullOrEmpty(txtEndDate.Text) && !string.IsNullOrEmpty(txtStartDate.Text))
            {
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
                                lblMsg.Text = "Error: 1210";
                                return;
                            }
                        }

                        int acaCalId = Convert.ToInt32(ucSession.selectedValue);
                        int programId = Convert.ToInt32(ucProgram.selectedValue);
                        int typeId = Convert.ToInt32(ddlActivityType.SelectedValue);
                        temp.ActivityTypeId = typeId;
                        temp.AcaCalId = acaCalId;
                        temp.ProgramId = programId;
                        temp.StartDate = DateTime.ParseExact(txtStartDate.Text, "d/M/yyyy", CultureInfo.InvariantCulture);
                        temp.EndDate = DateTime.ParseExact(txtEndDate.Text, "d/M/yyyy", CultureInfo.InvariantCulture);
                        temp.IsActive = chkIsActive.Checked;

                        InsertUpdate(temp);
                    }
                }
                else
                {
                    Response.Redirect("~/Security/Login.aspx");
                }
            }
            else
            {
                ShowAlertMessage("Plz enter Start and End Date!");
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
                btnCancel.Visible = true;
                btnSaveUpdate.Text = "Update";
                btnSaveUpdate.CommandArgument = id.ToString();

                ucSession.selectedValue = temp.AcaCalId.ToString();
                ucProgram.selectedValue = temp.ProgramId.ToString();
                ddlActivityType.SelectedValue = temp.ActivityTypeId.ToString();
                txtStartDate.Text = temp.StartDate.ToString("d/M/yyyy");
                txtEndDate.Text = temp.EndDate.ToString("d/M/yyyy");
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

            bool resultDelte = SetUpDateForProgramManager.Delete(id);
            if (resultDelte)
            {
                lblMsg.Text = "Delete Successfully";
                Load_Click(null, null);
            }
            else
            {
                lblMsg.Text = "Delete Fail";
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
        }
        catch { }
        finally { }
    }

    #endregion
}
