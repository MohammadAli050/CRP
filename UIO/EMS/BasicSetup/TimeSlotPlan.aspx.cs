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

public partial class BasicSetup_TimeSlotPlan : BasePage
{
    #region Variable Declaration
    List<TimeSlotPlan> _timeslots = null;
    TimeSlotPlan _timeslot = null;
    private string[] _dataKey = new string[1] { "Id" };
    #endregion

    #region Constants

    #region Session Names
    private const string SESSIONTIMESLOT = "TIMESLOT";
    private const string SESSIONTIMESLOTS = "TIMESLOTS";
    #endregion

    #endregion

    #region Functions
    private void FillAMPMCombos()
    {
        ddlSt.Items.Clear();
        ddlEt.Items.Clear();
        foreach (string item in Enum.GetNames(typeof(AMPM)))
        {
            ddlSt.Items.Add(item);
            ddlEt.Items.Add(item);
        }
        ddlSt.SelectedIndex = 0;
        ddlEt.SelectedIndex = 0;
    }
    private void FillTypeCombo()
    {
        ddlType.Items.Clear();

        foreach (string item in Enum.GetNames(typeof(TimeSlotType)))
        {
            ddlType.Items.Add(item);
        }
        ddlType.SelectedIndex = 0;
    }

    private void ClearForm()
    {
        this.txtEtHr.Text = "";
        this.txtEtMin.Text = "";
        this.txtStHr.Text = "";
        this.txtStMin.Text = "";
        this.ddlSt.SelectedIndex = 0;
        this.ddlEt.SelectedIndex = 0;
        this.ddlType.SelectedIndex = 0;
    }

    private void FillList()
    {
        if (txtSrch.Text.Trim().Length > 0)
        {
            _timeslots = TimeSlotPlan.GetTimeSlotPlans(txtSrch.Text.Trim());
        }
        else
        {
            _timeslots = TimeSlotPlan.GetTimeSlotPlans();
        }

        if (_timeslots == null)
        {
            gvwCollection.DataSource = null;
            gvwCollection.DataBind();
            Utilities.ShowMassage(lblMsg, Color.Blue, "No School Found.");

            DisableButtons();
            return;
        }

        gvwCollection.DataSource = _timeslots;
        gvwCollection.DataKeyNames = _dataKey;
        gvwCollection.DataBind();

        DisableButtons();
    }

    private void FillList(int stratHour, int startMin, int endHour, int endMin, AMPM startAMPM, AMPM endAMPM, TimeSlotType type)
    {
        _timeslot = TimeSlotPlan.GetTimeSlotPlan(stratHour,
                                            startMin,
                                            endHour,
                                            endMin,
                                            startAMPM,
                                            endAMPM,
                                            type);
        _timeslots = new List<TimeSlotPlan>();
        _timeslots.Add(_timeslot);

        if (_timeslots == null)
        {

            DisableButtons();
            return;
        }

        gvwCollection.DataSource = _timeslots;
        gvwCollection.DataKeyNames = _dataKey;
        gvwCollection.DataBind();

        DisableButtons();
    }

    private void DisableButtons()
    {
        if (gvwCollection.Rows.Count <= 0)
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
        pnlCollection.Enabled = enable;
        gvwCollection.Enabled = enable;
    }
    private void DisableCourse(bool enable)
    {
        pnlClass.Enabled = enable;
    }
    private void RefreshValue()
    {
        _timeslot = new TimeSlotPlan();
        _timeslot = (TimeSlotPlan)Session[SESSIONTIMESLOT];
        this.txtEtHr.Text = _timeslot.EndHour.ToString();
        this.txtEtMin.Text = _timeslot.EndMin.ToString();
        this.txtStHr.Text = _timeslot.StartHour.ToString();
        this.txtStMin.Text = _timeslot.StartMin.ToString();
        this.ddlSt.SelectedIndex = ((int)_timeslot.StartAMPM) - 1;
        this.ddlEt.SelectedIndex = ((int)_timeslot.EndAMPM) - 1;
        this.ddlType.SelectedIndex = (int)_timeslot.Type;
    }
    private TimeSlotPlan RefreshObject()
    {
        TimeSlotPlan OldTimeSlot = new TimeSlotPlan();
        _timeslot = null;
        if (Session[SESSIONTIMESLOT] == null)
        {
            _timeslot = new TimeSlotPlan();
        }
        else
        {
            _timeslot = (TimeSlotPlan)Session[SESSIONTIMESLOT];

            OldTimeSlot.Id = _timeslot.Id;
            OldTimeSlot.StartHour = _timeslot.StartHour;
            OldTimeSlot.StartMin = _timeslot.StartMin;
            OldTimeSlot.StartAMPM = _timeslot.StartAMPM;
            OldTimeSlot.EndHour = _timeslot.EndHour;
            OldTimeSlot.EndMin = _timeslot.EndMin;
            OldTimeSlot.EndAMPM = _timeslot.EndAMPM;
            OldTimeSlot.Type = _timeslot.Type;
            OldTimeSlot.CreatorID = _timeslot.CreatorID;
            OldTimeSlot.CreatedDate = _timeslot.CreatedDate;
            OldTimeSlot.ModifierID = _timeslot.ModifierID;
            OldTimeSlot.ModifiedDate = _timeslot.ModifiedDate;

        }

        int parserHelper = 0;
        if (Int32.TryParse(this.txtEtHr.Text.Trim(), out parserHelper))
        {
            _timeslot.EndHour = parserHelper;
        }
        else
        {
            throw new Exception("Hours can only be valid integers");
        }

        parserHelper = 0;
        if (Int32.TryParse(this.txtEtMin.Text.Trim(), out parserHelper))
        {
            _timeslot.EndMin = parserHelper;        
        }
        else
        {
            throw new Exception("Minutes can only be valid integers");
        }

        parserHelper = 0;
        if (Int32.TryParse(this.txtStHr.Text.Trim(), out parserHelper))
        {
            _timeslot.StartHour = parserHelper;        
        }
        else
        {
            throw new Exception("Hours can only be valid integers");
        }

        parserHelper = 0;
        if (Int32.TryParse(this.txtStMin.Text.Trim(), out parserHelper))
        {
            _timeslot.StartMin = parserHelper;        
        }
        else
        {
            throw new Exception("Hours can only be valid integers");
        }

        _timeslot.StartAMPM = (AMPM)Enum.Parse(typeof(AMPM), ddlSt.SelectedItem.Text);
        _timeslot.EndAMPM = (AMPM)Enum.Parse(typeof(AMPM), ddlEt.SelectedItem.Text);
        _timeslot.Type = (TimeSlotType)Enum.Parse(typeof(TimeSlotType), ddlType.SelectedItem.Text);

        return OldTimeSlot;
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

            if (!IsPostBack)
            {
                FillAMPMCombos();
                FillTypeCombo();

                DisableButtons();
                txtSrch.Focus();
                DisableCourse(false);
            }
            btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete?');");
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
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
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            if (gvwCollection.SelectedRow == null)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "Before deleting an item, you must select the Item.");
                return;
            }
            TimeSlotPlan.Delete(Convert.ToInt32(gvwCollection.SelectedValue));
            FillList();
            Utilities.ShowMassage(lblMsg, Color.Blue, "School information successfully deleted");
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 547)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "This School has been referenced in other tables, please delete those references first.");
            }
            else
            {
                Utilities.ShowMassage(lblMsg, Color.Red, SqlEx.Message);
            }
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            if (Session[SESSIONTIMESLOT] != null)
            {
                Session.Remove(SESSIONTIMESLOT);
            }
            DisableCollection(false);
            DisableCourse(true);
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            TimeSlotPlan oldTimeSlot = RefreshObject();

            if (TimeSlotPlan.HasDuplicateCode(_timeslot,
                                            oldTimeSlot.StartHour,
                                            oldTimeSlot.StartMin,
                                            oldTimeSlot.EndHour,
                                            oldTimeSlot.EndMin,
                                            oldTimeSlot.StartAMPM,
                                            oldTimeSlot.EndAMPM,
                                            oldTimeSlot.Type))
            {
                throw new Exception("Duplicate code are not allowed.");
            }

            bool isNewSchool = true;
            if (_timeslot.Id == 0)
            {
                _timeslot.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _timeslot.CreatedDate = DateTime.Now;
            }
            else
            {
                isNewSchool = false;
                _timeslot.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _timeslot.ModifiedDate = DateTime.Now;
            }

            TimeSlotPlan.Save(_timeslot,
                            oldTimeSlot.StartHour,
                            oldTimeSlot.StartMin,
                            oldTimeSlot.EndHour,
                            oldTimeSlot.EndMin,
                            oldTimeSlot.StartAMPM,
                            oldTimeSlot.EndAMPM,
                            oldTimeSlot.Type);

            if (isNewSchool)
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "School information successfully saved");
                ClearForm();
            }
            else
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "School information successfully updated");
                ClearForm();
                DisableCollection(true);
                DisableCourse(false);
                DisableButtons();
                txtSrch.Focus();
            }

            FillList(_timeslot.StartHour,
                            _timeslot.StartMin,
                            _timeslot.EndHour,
                            _timeslot.EndMin,
                            _timeslot.StartAMPM,
                            _timeslot.EndAMPM,
                            _timeslot.Type);

            if (Session[SESSIONTIMESLOT] != null)
            {
                Session.Remove(SESSIONTIMESLOT);
            }
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 2627)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "Duplicate codes are not allowed");
            }
            else
            {
                Utilities.ShowMassage(lblMsg, Color.Red, SqlEx.Message);
            }
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            ClearForm();
            DisableCollection(true);
            DisableCourse(false);
            DisableButtons();
            txtSrch.Focus();
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            if (gvwCollection.SelectedRow == null)
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "Before trying to edit an item, you must select the desired Item.");
                return;
            }

            _timeslot = new TimeSlotPlan();
            _timeslot = TimeSlotPlan.GetTimeSlotPlan(Convert.ToInt32(gvwCollection.SelectedValue));

            if (Session[SESSIONTIMESLOT] != null)
            {
                Session.Remove(SESSIONTIMESLOT);
            }
            Session.Add(SESSIONTIMESLOT, _timeslot);

            DisableCollection(false);
            DisableCourse(true);
            DisableButtons(false);

            RefreshValue();
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    #endregion
}
