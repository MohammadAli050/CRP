using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BussinessObject;
using DataAccess;

public partial class SyllabusMan_CalenderUnitCreator : BasePage
{
    #region Variables
    private List<CalendarUnitMaster> _calendarMasters = null;
    private List<CalenderUnitDistribution> _calendarDetails = null;
    private CalendarUnitMaster _calendarMaster = null;
    private CalenderUnitDistribution _calendarDetail = null;
    private string[] _dataKey = new string[1] { "Id" };
    private string[] _dataKeyDet = new string[1] { "Id" };
    #endregion

    #region Functions
    private void ClearFormEntry()
    {
        this.txtCalenderName.Text = "";
        this.txtDetailName.Text = "";
        this.lblDetName.Text = string.Empty;

        gdvCalDet.DataSource = null;
        gdvCalDet.DataBind();
    }
    private void FillMasterList()
    {
        if (txtSrch.Text.Trim().Length > 0)
        {
            _calendarMasters = CalendarUnitMaster.GetCalendarMasters(txtSrch.Text.Trim());
        }
        else
        {
            _calendarMasters = CalendarUnitMaster.GetCalendarMasters();
        }

        if (Session["CalendarUnitMasters"] != null)
        {
            Session.Remove("CalendarUnitMasters");
        }
        Session.Add("CalendarUnitMasters", _calendarMasters);

        gvwCalenderMaster.DataSource = _calendarMasters;
        gvwCalenderMaster.DataKeyNames = _dataKey;

        gvwCalenderMaster.DataBind();
        if (gvwCalenderMaster.Rows.Count <= 0)
        {
            lblMsg.Text = string.Empty;
            lblMsg.Text = "No records found";
            CollectionButtonController(false);
        }
        else
        {
            CollectionButtonController(true);
        }
    }
    private void FillMasterList(string calendarName)
    {

        _calendarMasters = CalendarUnitMaster.GetCalendarMastersByName(calendarName);


        if (Session["CalendarUnitMasters"] != null)
        {
            Session.Remove("CalendarUnitMasters");
        }
        Session.Add("CalendarUnitMasters", _calendarMasters);

        gvwCalenderMaster.DataSource = _calendarMasters;
        gvwCalenderMaster.DataKeyNames = _dataKey;

        gvwCalenderMaster.DataBind();
        if (gvwCalenderMaster.Rows.Count <= 0)
        {
            CollectionButtonController(false);
        }
        else
        {
            CollectionButtonController(true);
        }
    }
    private void CollectionButtonController(bool enable)
    {
        btnEdit.Enabled = enable;
        btnDelete.Enabled = enable;
    }
    private void AddEditController(bool enable)
    {
        pnlEntry.Enabled = !enable;
        pnlControl.Enabled = enable;
    }

    private void FillDetailList()
    {
        if (Session["CalendarUnitMaster"] != null)
        {
            _calendarMaster = (CalendarUnitMaster)Session["CalendarUnitMaster"];

            if (Session["CalendarUnitDetails"] != null)
            {
                Session.Remove("CalendarUnitDetails");
            }
            Session.Add("CalendarUnitDetails", _calendarMaster.CalendarDetails);

            gdvCalDet.DataSource = _calendarMaster.CalendarDetails;
            gdvCalDet.DataKeyNames = _dataKeyDet;

            gdvCalDet.DataBind(); 
        }
    }
    private void FillDetailList(List<CalenderUnitDistribution> calendarDetails)
    {
        if (Session["CalendarUnitDetails"] != null)
        {
            Session.Remove("CalendarUnitDetails");
        }
        Session.Add("CalendarUnitDetails", calendarDetails);

        gdvCalDet.DataSource = calendarDetails;
        gdvCalDet.DataKeyNames = _dataKeyDet;

        gdvCalDet.DataBind();
    }

    private void DetailAddEditController(bool enable)
    {
        pnlCalendarDet.Enabled = enable;
        butSave.Enabled = enable;
    }


    private void RefreshValueMaster()
    {
        _calendarMaster = new CalendarUnitMaster();
        _calendarMaster = (CalendarUnitMaster)Session["CalendarUnitMaster"];
        if (_calendarMaster.CalendarDetails != null)
        {
            FillDetailList(_calendarMaster.CalendarDetails);
        }
        this.txtCalenderName.Text = _calendarMaster.Name;
        lblDetName.Text = string.Empty;
        lblDetName.Text = txtCalenderName.Text + " Name:";
        this.txtCalenderName.Focus();
    }

    private void RefreshObject()
    {
        _calendarMaster = null;
        if (Session["CalendarUnitMaster"] == null)
        {
            _calendarMaster = new CalendarUnitMaster();
        }
        else
        {
            _calendarMaster = (CalendarUnitMaster)Session["CalendarUnitMaster"];
        }

        _calendarMaster.Name = txtCalenderName.Text.Trim();

        if (Session["CalendarUnitDetails"] != null)
        {
            //if (_calendarMaster.CalendarDetails != null)
            //{
            //    _calendarMaster.CalendarDetails.Clear(); 
            //}
            _calendarDetails = (List<CalenderUnitDistribution>)Session["CalendarUnitDetails"];
            _calendarMaster.CalendarDetails = _calendarDetails;
        }
    }

    private void SessionCleaner()
    {
        if (Session["CalendarUnitMaster"] != null)
        {
            Session.Remove("CalendarUnitMaster");
        }

        if (Session["CalendarUnitDetails"] != null)
        {
            Session.Remove("CalendarUnitDetails");
        }

        if (Session["CalendarUnitDetail"] != null)
        {
            Session.Remove("CalendarUnitDetail");
        }

        if (Session["CalendarDetailIndex"] != null)
        {
            Session.Remove("CalendarDetailIndex");
        }
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
                CollectionButtonController(false); 
            }
            btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete the selected element?');");

            //String scriptText = "";
            //scriptText += "function DisplayCharCount(){";
            //scriptText += " document.forms[0].lblDetailName.value = " + //ctl00_cpHolMas_lblDetailName
            //    " document.forms[0].txtCalenderName.value"; //ctl00_cpHolMas_txtCalenderName
            //scriptText += "}";
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
            //   "CounterScript", scriptText, true);
            //txtCalenderName.Attributes.Add("onkeyup", "DisplayCharCount()");
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }
    protected void butFind_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            FillMasterList();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }
    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            SessionCleaner();
            ClearFormEntry();
            AddEditController(false);
            DetailAddEditController(true);

            butSave.Enabled = true;
            this.txtCalenderName.Focus();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            if (gvwCalenderMaster.SelectedRow == null)
            {
                lblMsg.Text = "Before trying to edit an item, you must select the desired Item.";
                return;
            }

            _calendarMaster = new CalendarUnitMaster();

            //DataKey dtkey = gvwCalenderMaster.SelectedPersistedDataKey;
            DataKey dtkey = gvwCalenderMaster.SelectedDataKey;//@Sajib

            IOrderedDictionary odict = dtkey.Values;
            _calendarMaster = CalendarUnitMaster.GetCalendarMaster(Convert.ToInt32(odict[0]));

            if (TreeCalendarMaster.GetByCalMaster(_calendarMaster.Id) != null)
            {
                lblMsg.Text = "This Calendar unit has already been linked with one or more trees please delete those links first, then edit it.";
                #region Viewing
                if (Session["CalendarUnitMaster"] != null)
                {
                    Session.Remove("CalendarUnitMaster");
                }
                Session.Add("CalendarUnitMaster", _calendarMaster);

                AddEditController(true);
                if (_calendarMaster.CalendarDetails != null)
                {
                    if (_calendarMaster.CalendarDetails.Count <= 0)
                    {
                        DetailAddEditController(false);
                    }
                    else
                    {
                        DetailAddEditController(true);
                    }
                }
                else
                {
                    DetailAddEditController(true);
                }

                RefreshValueMaster(); 
                #endregion
                return;
            }
            

            if (Session["CalendarUnitMaster"] != null)
            {
                Session.Remove("CalendarUnitMaster");
            }
            Session.Add("CalendarUnitMaster", _calendarMaster);

            AddEditController(false);
            if (_calendarMaster.CalendarDetails != null)
            {
                if (_calendarMaster.CalendarDetails.Count <=0)
                {
                    DetailAddEditController(false);
                }
                else
                {
                    DetailAddEditController(true);
                }
            }
            else
            {
                DetailAddEditController(true);
            }

            RefreshValueMaster();

            this.txtCalenderName.Focus();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (gvwCalenderMaster.SelectedRow == null)
            {
                lblMsg.Text = "Before deleting an item, you must select the Item.";
                return;
            }

            DataKey dtkey = gvwCalenderMaster.SelectedDataKey;

            IOrderedDictionary odict = dtkey.Values;

            if (TreeCalendarMaster.GetByCalMaster(Convert.ToInt32(odict[0])) != null)
            {
                lblMsg.Text = "This Calendar unit has already been linked with one or more trees please delete those links first, then delete it.";
                return;
            }

            CalendarUnitMaster.Delete(Convert.ToInt32(odict[0]));
            FillMasterList();
            lblMsg.Text = "Calendar information successfully deleted";
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 547)
            {
                lblMsg.Text = "This Calendar has been referenced in other tables, please delete those references first.";
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

    protected void btnAddDet_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (Session["CalendarUnitDetail"] != null)
            {
                Session.Remove("CalendarUnitDetail");
            }

            if (Session["CalendarUnitDetails"] != null)
            {
                _calendarDetails = (List<CalenderUnitDistribution>)Session["CalendarUnitDetails"];
            }
            else
            {
                _calendarDetails = new List<CalenderUnitDistribution>();
            }

            _calendarDetail = new CalenderUnitDistribution();
            _calendarDetail.Name = this.txtDetailName.Text.Trim();
            _calendarDetails.Add(_calendarDetail);

            if (Session["CalendarUnitDetails"] != null)
            {
                Session.Remove("CalendarUnitDetails");
            }
            Session.Add("CalendarUnitDetails", _calendarDetails);

            FillDetailList(_calendarDetails);
            this.txtDetailName.Text = string.Empty;
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }

    protected void butSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            string oldName = string.Empty;

            if (Session["CalendarUnitMaster"] != null)
            {
                oldName = ((CalendarUnitMaster)Session["CalendarUnitMaster"]).Name;
            }

            RefreshObject();

            if (CalendarUnitMaster.HasDuplicateName(_calendarMaster, oldName))
            {
                throw new Exception("Duplicate calendar names are not allowed.");
            }

            bool isNewCal = true;
            if (_calendarMaster.Id > 0)
            {
                isNewCal = false;
                _calendarMaster.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _calendarMaster.ModifiedDate = DateTime.Now;
            }
            else
            {
                _calendarMaster.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _calendarMaster.CreatedDate = DateTime.Now;
            }

            //foreach (CalenderUnitDistribution caldist in _calendarMaster.CalendarDetails)
            //{
            //    caldist.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
            //    caldist.CreatedDate = DateTime.Now;
            //}

            CalendarUnitMaster.Save(_calendarMaster);

            FillMasterList(_calendarMaster.Name);

            if (isNewCal)
            {
                lblMsg.Text = "Calendar information successfully saved";
                this.txtCalenderName.Focus();
            }
            else
            {
                lblMsg.Text = "Calendar information successfully updated";

                AddEditController(true);
                if (gvwCalenderMaster.Rows.Count <= 0)
                {
                    CollectionButtonController(false);
                }
                else
                {
                    CollectionButtonController(true);
                }
            }

            ClearFormEntry();
            SessionCleaner();
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 2627)
            {
                lblMsg.Text = "Duplicate codes are not allowed";
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
    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        SessionCleaner();
        lblMsg.Text = string.Empty;
        ClearFormEntry();
        AddEditController(true);
        if (gvwCalenderMaster.Rows.Count <= 0)
        {
            CollectionButtonController(false);
        }
        else
        {
            CollectionButtonController(true);
        }
    } 

    protected void gdvCalDet_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (Session["CalendarUnitDetails"] != null)
            {
                if (Session["CalendarUnitDetail"] != null)
                {
                    Session.Remove("CalendarUnitDetail");
                }
                if (Session["CalendarDetailIndex"] != null)
                {
                    Session.Remove("CalendarDetailIndex");
                }
                gdvCalDet.EditIndex = e.NewEditIndex;

                _calendarDetails = (List<CalenderUnitDistribution>)Session["CalendarUnitDetails"];
                Session.Add("CalendarUnitDetail", _calendarDetails[e.NewEditIndex]);
                Session.Add("CalendarDetailIndex", e.NewEditIndex);
                FillDetailList(_calendarDetails);
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }
    protected void gdvCalDet_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (Session["CalendarUnitDetails"] != null)
            {
                _calendarDetails = (List<CalenderUnitDistribution>)Session["CalendarUnitDetails"];

                _calendarDetails.RemoveAt(e.RowIndex);


                if (Session["CalendarUnitDetail"] != null)
                {
                    Session.Remove("CalendarUnitDetail");
                }
                if (Session["CalendarDetailIndex"] != null)
                {
                    Session.Remove("CalendarDetailIndex");
                }

                if (Session["CalendarUnitDetails"] != null)
                {
                    Session.Remove("CalendarUnitDetails");
                }
                Session.Add("CalendarUnitDetails", _calendarDetails);

                FillDetailList(_calendarDetails);
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }

    protected void gdvCalDet_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (Session["CalendarUnitDetails"] != null)
            {
                if (Session["CalendarUnitDetail"] != null)
                {
                    Session.Remove("CalendarUnitDetail");
                }
                if (Session["CalendarDetailIndex"] != null)
                {
                    Session.Remove("CalendarDetailIndex");
                }

                gdvCalDet.EditIndex = -1;

                _calendarDetails = (List<CalenderUnitDistribution>)Session["CalendarUnitDetails"];
                FillDetailList(_calendarDetails);
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }
    protected void gdvCalDet_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (Session["CalendarUnitDetails"] != null && Session["CalendarUnitDetail"] != null)
            {
                _calendarDetails = (List<CalenderUnitDistribution>)Session["CalendarUnitDetails"];
                int selectedIndex = (int)Session["CalendarDetailIndex"];
                _calendarDetails.RemoveAt(selectedIndex);

                GridViewRow row = gdvCalDet.Rows[e.RowIndex];

                _calendarDetail = new CalenderUnitDistribution();
                _calendarDetail.Name = ((TextBox)(row.Cells[2].Controls[0])).Text;
                _calendarDetails.Add(_calendarDetail);

                if (Session["CalendarUnitDetail"] != null)
                {
                    Session.Remove("CalendarUnitDetail");
                }
                if (Session["CalendarDetailIndex"] != null)
                {
                    Session.Remove("CalendarDetailIndex");
                }

                if (Session["CalendarUnitDetails"] != null)
                {
                    Session.Remove("CalendarUnitDetails");
                }
                Session.Add("CalendarUnitDetails", _calendarDetails);

                gdvCalDet.EditIndex = -1;
                FillDetailList(_calendarDetails);
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }

    protected void gdvCalDet_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (e.Exception == null)
            {
                lblMsg.Text = "Detail successfully edited.";
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }

    protected void btnSet_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            lblDetName.Text = string.Empty;
            lblDetName.Text = txtCalenderName.Text + " Name:";
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    #endregion

    
}
