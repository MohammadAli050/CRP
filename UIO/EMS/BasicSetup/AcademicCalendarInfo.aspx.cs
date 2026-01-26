using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Common;
using BussinessObject;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

public partial class BasicSetup_AcademicCalendarInfo : BasePage
{
    #region Variables
    private List<AcademicCalender> _trimesterInfos = null;
    private AcademicCalender _trimesterInfo = null;
    List<CalendarUnitMaster> _calendarUnitMasters = null;
    private string[] _dataKey = new string[1] { "Id" };
    List<CalenderUnitType> _types = null;
    #endregion

    #region Session Names
    private const string SESSIONTRIMESTERINFO = "TrimesterInfo";
    private const string SESSIONTYPES = "Types";
    private const string SESSIONCUMS = "CALENDARUNITMASTERS";
    #endregion

    #region Functions
    private void FillCalCombo()
    {
        ddlCalType.Items.Clear();

        _types = CalenderUnitType.GetCalUTypes();
        _types = _types.Where(c => c.CalenderUnitMasterID == Convert.ToInt32(DDListTriSem.SelectedValue)).ToList();

        
        if (_types != null)
        {
            int f = 0;
            foreach (CalenderUnitType type in _types)
            {
                ListItem item = new ListItem();

                if (f == 0)
                {
                    ListItem itemBlank = new ListItem();
                    f = 1;
                    itemBlank.Value = "";
                    itemBlank.Text = "";
                    ddlCalType.Items.Add(itemBlank);                    
                }                
                
                item.Value = type.Id.ToString();
                item.Text = type.TypeName;
                ddlCalType.Items.Add(item);
            }

            if (Session[SESSIONTYPES] != null)
            {
                Session.Remove(SESSIONTYPES);
            }
            Session.Add(SESSIONTYPES, _types);

            ddlCalType.SelectedIndex = 0;
        }
    }   

    private void ClearForm()
    {
        this.ddlCalType.SelectedIndex = 0;
        this.txtYear.Text = "";
        this.txtBatch.Text = "";
        this.chkIsCUrr.Checked = false;
        this.clrEndDate.Text = "";
        this.clrStartDate.Text = "";
        //this.clrAdmissionEndDate.Text = "";
        //this.clrAdmissionStartDate.Text = "";
        //this.clrRegistrationEndDate.Text = "";
        //this.clrRegistrationStartDate.Text = "";        
        this.chkAdmiIsActive.Checked = false;
        this.chkRegiIsActive.Checked = false;
    }

    //private void FillList()
    //{
    //    if (txtSrch.Text.Trim().Length > 0)
    //    {
    //        _trimesterInfos = AcademicCalender.Gets(txtSrch.Text.Trim());
    //    }
    //    else
    //    {
    //        _trimesterInfos = AcademicCalender.Gets();
    //    }

    //    if (_trimesterInfos == null)
    //    {
    //        gvwStudents.DataSource = null;
    //        gvwStudents.DataBind();
    //        lblMsg.Text = "No Trimister Found.";
    //        return;
    //    }


    //    gvwStudents.DataSource = _trimesterInfos;
    //    gvwStudents.DataKeyNames = _dataKey;
    //    gvwStudents.DataBind();

    //    DisableButtons();
    //}

    private void FillList()
    {
        try
        {

            _trimesterInfos = new List<AcademicCalender>();

            if (txtSrch.Text != string.Empty)
            {
                _trimesterInfos = AcademicCalender.GetsByTremisterNSearchText(DDListTriSem.SelectedValue, Convert.ToString(txtSrch.Text));
            }
            else
            {
                _trimesterInfos = AcademicCalender.Gets(DDListTriSem.SelectedValue);
            }


            gvwStudents.DataSource = _trimesterInfos;
            gvwStudents.DataKeyNames = _dataKey;
            gvwStudents.DataBind();

            GridRebind();
            DisableButtons();

        }
        catch (Exception ex)
        { }
    }

    private void GridRebind()
    {
        for (int i = 0; i < gvwStudents.Rows.Count; i++)
        {
            GridViewRow row = gvwStudents.Rows[i];
            //Label amount = (Label)row.FindControl("PaidAmount");
            bool isCurrent = Convert.ToBoolean(row.Cells[4].Text);
            bool isAdmission = Convert.ToBoolean(row.Cells[5].Text);
            bool isRegistration = Convert.ToBoolean(row.Cells[6].Text);
            if (isCurrent)
            {
                row.Cells[4].Text = "Active";
            }
            else 
            {
                row.Cells[4].Text = "";
            }

            if (isAdmission)
            {
                row.Cells[5].Text = "Active";
            }
            else
            {
                row.Cells[5].Text = "";
            }

            if (isRegistration)
            {
                row.Cells[6].Text = "Active";
            }
            else
            {
                row.Cells[6].Text = "";
            }
        }
    }

    private void FillList(string batchCode)
    {
        _trimesterInfos = AcademicCalender.GetsbyBatch(batchCode);

        if (_trimesterInfos == null)
        {
            return;
        }

        gvwStudents.DataSource = _trimesterInfos;
        gvwStudents.DataKeyNames = _dataKey;
        gvwStudents.DataBind();

        DisableButtons();
    }

    private void DisableButtons()
    {
        if (gvwStudents.Rows.Count <= 0)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }
        else
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }
    }

    private void DisableButtons(bool enable)
    {
        btnEdit.Enabled = enable;
        btnDelete.Enabled = enable;
    }

    private void DisableCollection(bool enable)
    {
        pnlStudents.Enabled = enable;
        gvwStudents.Enabled = enable;
    }

    private void DisableCal(bool enable)
    {
        if (!enable)
        {
            this.ddlCalType.SelectedIndex = 0;
        }
        pnlStudent.Enabled = enable;
        clrStartDate.Enabled = enable;
        clrEndDate.Enabled = enable;
    }

    private void RefreshValue()
    {
        _trimesterInfo = new AcademicCalender();
        _trimesterInfo = (AcademicCalender)Session["TrimesterInfo"];
        this.txtYear.Text = _trimesterInfo.Year.ToString();
        this.txtBatch.Text = _trimesterInfo.Code.ToString();
        this.chkIsCUrr.Checked = _trimesterInfo.IsCurrent;
        this.ChkIsNext.Checked = _trimesterInfo.IsNext;
        this.clrStartDate.Date = _trimesterInfo.StartDate;
        this.clrEndDate.Date = _trimesterInfo.EndDate;
        this.ddlCalType.SelectedValue = _trimesterInfo.CalenderUnitTypeID.ToString();
        this.ddlCalType.ToolTip = ddlCalType.SelectedItem.Text;

        //this.clrAdmissionStartDate.Date = _trimesterInfo.AdmiStartDate;
        //this.clrAdmissionEndDate.Date = _trimesterInfo.AdmiEndDate;
        this.chkAdmiIsActive.Checked = _trimesterInfo.IsActiveAdmi;
        //this.clrRegistrationStartDate.Date = _trimesterInfo.RegiStartDate;
        //this.clrRegistrationEndDate.Date = _trimesterInfo.RegiEndDate;
        this.chkRegiIsActive.Checked = _trimesterInfo.IsActiveRegi;

        //this.RadioButtonList1. = "";

    }

    private void RefreshObject()
    {
        _trimesterInfo = null;
        if (Session["TrimesterInfo"] == null)
        {
            _trimesterInfo = new AcademicCalender();
        }
        else
        {
            _trimesterInfo = (AcademicCalender)Session["TrimesterInfo"];
        }

        _trimesterInfo.CalenderUnitTypeID = Int32.Parse(ddlCalType.SelectedValue);
        //_trimesterInfo.BatchCode = txtBatch.Text.Trim();


        int BCode = 0;
        if (Int32.TryParse(txtBatch.Text.Trim(), out BCode))
        {
            if (BCode == 0)
            {
                throw new Exception("Batch code can not be zero.");
            }
            _trimesterInfo.Code = BCode.ToString();
        }
        else
        {
            throw new Exception("Batch code can only be non zero number.");
        }

        int year = 0;
        if (Int32.TryParse(txtYear.Text.Trim(), out year))
        {
            if (year == 0)
            {
                throw new Exception("Year can not be zero.");
            }
            _trimesterInfo.Year = year;
        }
        else
        {
            throw new Exception("Year can only be non zero number.");
        }

        _trimesterInfo.IsCurrent = chkIsCUrr.Checked;
        _trimesterInfo.IsNext = ChkIsNext.Checked;

        if (clrStartDate.Date != null && clrStartDate.Date != DateTime.MinValue)
        {
            _trimesterInfo.StartDate = clrStartDate.Date;
        }
        else
        {
            throw new Exception("Please select start Date.");
        }

        if (clrEndDate.Date != null && clrEndDate.Date != DateTime.MinValue)
        {
            _trimesterInfo.EndDate = clrEndDate.Date;
        }
        else
        {
            throw new Exception("Please select end Date");
        }

        //if (clrAdmissionStartDate.Date != null && clrAdmissionStartDate.Date != DateTime.MinValue)
        //{
        //    _trimesterInfo.AdmiStartDate = clrAdmissionStartDate.Date;
        //}
        //else
        //{
        //    _trimesterInfo.AdmiStartDate = DateTime.MinValue;
        //    //throw new Exception("Please select admission start Date.");
        //}

        //if (clrAdmissionEndDate.Date != null && clrAdmissionEndDate.Date != DateTime.MinValue)
        //{
        //    _trimesterInfo.AdmiEndDate = clrAdmissionEndDate.Date;
        //}
        //else
        //{
        //    _trimesterInfo.AdmiEndDate = DateTime.MinValue;
        //    //throw new Exception("Please select admission end Date");
        //}

        _trimesterInfo.IsActiveAdmi = chkAdmiIsActive.Checked;

        //if (clrRegistrationStartDate.Date != null && clrRegistrationStartDate.Date != DateTime.MinValue)
        //{
        //    _trimesterInfo.RegiStartDate = clrRegistrationStartDate.Date;
        //}
        //else
        //{
        //    _trimesterInfo.RegiStartDate = DateTime.MinValue;
        //    //throw new Exception("Please select registration start Date.");
        //}

        //if (clrRegistrationEndDate.Date != null && clrRegistrationEndDate.Date != DateTime.MinValue)
        //{
        //    _trimesterInfo.RegiEndDate = clrRegistrationEndDate.Date;
        //}
        //else
        //{
        //    _trimesterInfo.RegiEndDate = DateTime.MinValue;
        //    //throw new Exception("Please select registration end Date");
        //}

        _trimesterInfo.IsActiveRegi = chkRegiIsActive.Checked;
        
    }
    #endregion

    #region Events
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
            base.CheckPage_Load();
            #region tmp
            //UIUMSUser.CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            //if (UIUMSUser.CurrentUser != null)
            //{
            //    if (UIUMSUser.CurrentUser.RoleID > 0)
            //    {
            //        Authenticate(UIUMSUser.CurrentUser);
            //    }
            //}
            //else
            //{
            //    Response.Redirect("~/Security/Login.aspx");
            //} 
            #endregion
            if (!IsPostBack)
            {
               
                FillDDListTriSem();
                DisableButtons();
                //txtSrch.Focus();
            }
            btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete?');");
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }

    private void FillDDListTriSem()
    {
        DDListTriSem.Items.Clear();

        _calendarUnitMasters = CalendarUnitMaster.GetCalendarMasters();

        if (_calendarUnitMasters != null)
        {
            foreach (CalendarUnitMaster calendarUnitMaster in _calendarUnitMasters)
            {
                ListItem item = new ListItem();
                item.Value = calendarUnitMaster.Id.ToString();
                item.Text = calendarUnitMaster.Name;
                DDListTriSem.Items.Add(item);
            }

            if (Session[SESSIONCUMS] != null)
            {
                Session.Remove(SESSIONCUMS);
            }
            Session.Add(SESSIONCUMS, _calendarUnitMasters);

            DDListTriSem.SelectedIndex = 0;

            DDListTriSem_SelectedIndexChanged(null, null);
        }
    }

    protected void Page_PreRenderComplete(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                DisableCal(false);
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            FillList();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            if (Session["TrimesterInfo"] != null)
            {
                Session.Remove("TrimesterInfo");
            }
            DisableCollection(false);
            DisableCal(true);
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            if (gvwStudents.SelectedRow == null)
            {
                lblMsg.Text = "Before trying to edit an item, you must select the desired Item.";
                return;
            }

            _trimesterInfo = new AcademicCalender();
            _trimesterInfo = AcademicCalender.Get(Convert.ToInt32(gvwStudents.SelectedValue));

            if (Session["TrimesterInfo"] != null)
            {
                Session.Remove("TrimesterInfo");
            }
            Session.Add("TrimesterInfo", _trimesterInfo);

            DisableCollection(false);
            DisableCal(true);
            DisableButtons(false);

            RefreshValue();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            if (gvwStudents.SelectedRow == null)
            {
                lblMsg.Text = "Before deleting an item, you must select the Item.";
                return;
            }
            AcademicCalender.Delete(Convert.ToInt32(gvwStudents.SelectedValue));
            FillList();
            lblMsg.Text = "Trimester information successfully deleted";
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 547)
            {
                lblMsg.Text = "This Trimester has been referenced in other tables, please delete those references first.";
            }
            else
            {
                lblMsg.Text = SqlEx.Message;
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }

    protected void butSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            RefreshObject();

            if (TypeDefinition.HasDuplicateCode(_trimesterInfo))
            {
                throw new Exception("Duplicate batch code are not allowed.");
            }

            bool isNewCal = true;
            if (_trimesterInfo.Id == 0)
            {
                _trimesterInfo.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _trimesterInfo.CreatedDate = DateTime.Now;
            }
            else
            {
                isNewCal = false;
                _trimesterInfo.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _trimesterInfo.ModifiedDate = DateTime.Now;
            }

            AcademicCalender.Save(_trimesterInfo);
            
            FillList(_trimesterInfo.Code);

            if (isNewCal)
            {
                lblMsg.Text = "Trimester information successfully saved";
                ClearForm();
            }
            else
            {
                lblMsg.Text = "Trimester information successfully updated";
                ClearForm();
                DisableCollection(true);
                DisableCal(false);
                DisableButtons();
                //txtSrch.Focus();
            }

            if (Session["TrimesterInfo"] != null)
            {
                Session.Remove("TrimesterInfo");
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();
        DisableCollection(true);
        DisableCal(false);
        DisableButtons();
        //txtSrch.Focus();
    }
    
    #endregion    

    protected void DDListTriSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCalCombo();

        gvwStudents.DataSource = null;
        gvwStudents.DataBind();
    }

    protected void ddlCalType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtBatch.Text = txtYear.Text.Substring(2, 2) + ddlCalType.SelectedValue.PadLeft(2, '0');
    }
}
