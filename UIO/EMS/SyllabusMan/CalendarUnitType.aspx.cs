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

public partial class SyllabusMan_CalendarUnitType : BasePage
{
    #region Variable Declaration
    List<CalenderUnitType> _calenderUnitTypes = null;
    List<CalendarUnitMaster> _calendarUnitMasters = null;
    CalenderUnitType _calenderUnitType = null;
    private string[] _dataKey = new string[1] { "Id" };
    #endregion

    #region Constants

    #region Session Names
    private const string SESSIONCUT = "CALENDERUNITTYPE";
    private const string SESSIONCUTS = "CALENDERUNITTYPES";
    private const string SESSIONCUMS = "CALENDARUNITMASTERS";
    #endregion

    #endregion

    #region Functions
    private void ClearForm()
    {
        this.txtName.Text = "";
        this.ddlParent.SelectedIndex = 0;
    }
    private void FillParntCombo()
    {
        ddlParent.Items.Clear();

        _calendarUnitMasters = CalendarUnitMaster.GetCalendarMasters();

        if (_calendarUnitMasters != null)
        {
            foreach (CalendarUnitMaster calendarUnitMaster in _calendarUnitMasters)
            {
                ListItem item = new ListItem();
                item.Value = calendarUnitMaster.Id.ToString();
                item.Text = calendarUnitMaster.Name;
                ddlParent.Items.Add(item);
            }

            if (Session[SESSIONCUMS] != null)
            {
                Session.Remove(SESSIONCUMS);
            }
            Session.Add(SESSIONCUMS, _calendarUnitMasters);

            ddlParent.SelectedIndex = 0;
        }
    }

    private void FillList()
    {
        if (txtSrch.Text.Trim().Length > 0)
        {
            _calenderUnitTypes = CalenderUnitType.GetCalUTypes(txtSrch.Text.Trim());
        }
        else
        {
            _calenderUnitTypes = CalenderUnitType.GetCalUTypes();
        }

        if (_calenderUnitTypes == null)
        {
            gvwCollection.DataSource = null;
            gvwCollection.DataBind();
            Utilities.ShowMassage(lblMsg, Color.Blue, "No Calender Unit Type Found.");

            DisableButtons();
            return;
        }

        gvwCollection.DataSource = _calenderUnitTypes;
        gvwCollection.DataKeyNames = _dataKey;
        gvwCollection.DataBind();

        DisableButtons();
    }

    private void FillList(string name)
    {
        _calenderUnitType = CalenderUnitType.GetCalUType(name);
        _calenderUnitTypes = new List<CalenderUnitType>();
        _calenderUnitTypes.Add(_calenderUnitType);

        if (_calenderUnitTypes == null)
        {

            DisableButtons();
            return;
        }

        gvwCollection.DataSource = _calenderUnitTypes;
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
        _calenderUnitType = new CalenderUnitType();
        _calenderUnitType = (CalenderUnitType)Session[SESSIONCUT];
        this.txtName.Text = _calenderUnitType.TypeName;
        this.ddlParent.SelectedValue = _calenderUnitType.CalenderUnitMasterID.ToString();
        this.ddlParent.ToolTip = ddlParent.SelectedItem.Text;
    }
    private void RefreshObject()
    {
        _calenderUnitType = null;
        if (Session[SESSIONCUT] == null)
        {
            _calenderUnitType = new CalenderUnitType();
        }
        else
        {
            _calenderUnitType = (CalenderUnitType)Session[SESSIONCUT];
        }

        _calenderUnitType.TypeName = txtName.Text.Trim();
        _calenderUnitType.CalenderUnitMasterID = Int32.Parse(ddlParent.SelectedValue);

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
                FillParntCombo();
                DisableButtons();
                txtSrch.Focus();
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            if (Session[SESSIONCUT] != null)
            {
                Session.Remove(SESSIONCUT);
            }
            DisableCollection(false);
            DisableCourse(true);
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

            _calenderUnitType = new CalenderUnitType();
            _calenderUnitType = CalenderUnitType.GetCalUType(Convert.ToInt32(gvwCollection.SelectedValue));

            if (Session[SESSIONCUT] != null)
            {
                Session.Remove(SESSIONCUT);
            }
            Session.Add(SESSIONCUT, _calenderUnitType);

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
            CalenderUnitType.Delete(Convert.ToInt32(gvwCollection.SelectedValue));
            FillList();
            Utilities.ShowMassage(lblMsg, Color.Blue, "Calender Unit Type information successfully deleted");
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 547)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "This Calender Unit Type has been referenced in other tables, please delete those references first.");
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            RefreshObject();

            if (CalenderUnitType.HasDuplicateCode(_calenderUnitType))
            {
                throw new Exception("Duplicate Calender Unit Type Names are not allowed.");
            }

            bool isNewDept = true;
            if (_calenderUnitType.Id == 0)
            {
                _calenderUnitType.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _calenderUnitType.CreatedDate = DateTime.Now;
            }
            else
            {
                isNewDept = false;
                _calenderUnitType.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _calenderUnitType.ModifiedDate = DateTime.Now;
            }

            CalenderUnitType.Save(_calenderUnitType);

            if (isNewDept)
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "Calender Unit Type information successfully saved");
                ClearForm();
            }
            else
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "Calender Unit Type information successfully updated");
                ClearForm();
                DisableCollection(true);
                DisableCourse(false);
                DisableButtons();
                txtSrch.Focus();
            }

            FillList(_calenderUnitType.TypeName);

            if (Session[SESSIONCUT] != null)
            {
                Session.Remove(SESSIONCUT);
            }
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 2627)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "Calender Unit Type codes are not allowed");
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
    #endregion
}
