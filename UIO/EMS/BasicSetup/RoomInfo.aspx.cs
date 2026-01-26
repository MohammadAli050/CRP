using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using BussinessObject;
using Common;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using BussinessObject.BasicSetup;

public partial class BasicSetup_RoomInfo : BasePage
{
    #region Session Names
    private const string SESSIONROOMINFO = "ROOMINFO";
    private const string SESSIONROOMTYPEINFO = "ROOMTYPEINFO";
    private const string SESSIONCAMPUS = "CAMPUS";
    private const string SESSIONCAMPUSBUILDING = "CAMPUSBUILDING";
    #endregion

    #region Variable Declaration
    List<RoomInfo> _roomsInfo = null;
    RoomInfo _roomInfo = null;
    private string[] _dataKey = new string[1] { "Id" };
    List<RoomType> _roomType = null;
    List<Campus> _campus = null;
    List<CampusBuilding> _campusBuilding = null;
    #endregion

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Page.Request.ServerVariables["http_user_agent"].ToLower().Contains("safari"))
        {
            Page.ClientTarget = "uplevel";
        }
    }
    private void LoadRoomType()
    {
        ddlRoomType.Items.Clear();
        _roomType = RoomType.GetRoomTypesInfo();

        if (_roomType != null)
        {
            foreach (RoomType rt in _roomType)
            {
                ListItem item = new ListItem();
                item.Value = rt.Id.ToString();
                item.Text = rt.Roomtypename;
                ddlRoomType.Items.Add(item);
            }

            if (Session[SESSIONROOMINFO] != null)
            {
                Session.Remove(SESSIONROOMTYPEINFO);
            }
            Session.Add(SESSIONROOMTYPEINFO, _roomType);

            ddlRoomType.SelectedIndex = 0;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try 
        {
            base.CheckPage_Load();

            if(!IsPostBack)
            {
                InitializeItems(this.Page, false, true);
                LoadRoomType();
                LoadCampus();
                int campusId = Convert.ToInt16(ddlCampusName.SelectedValue);
                LoadCampusBuilding(campusId);
                txtSrch.Focus();
            }
            btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete?');");
        }
        catch(Exception ex)
        {
            Common.Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally{}
    }
    private void FillList()
    {        
        _roomsInfo = RoomInfo.GetRoomsInfo(txtSrch.Text.Trim());       
        if (_roomsInfo == null)
        {
            gvwCollection.DataSource = null;
            gvwCollection.DataBind();
            Utilities.ShowMassage(lblMsg, Color.Blue, lblHeader + " - " + Message.NOTFOUND);
            return;
        }
        gvwCollection.DataSource = _roomsInfo;
        gvwCollection.DataKeyNames = _dataKey;
        gvwCollection.DataBind();

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
            InitializeItems(pnlRoomInfo, true, false);
            txtRoomNo.Focus();
            if (Session[SESSIONROOMINFO] != null)
            {
                Session.Remove(SESSIONROOMINFO);
            }
        }
        catch(Exception ex)
        {
            Common.Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally{}
    }    

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            if (gvwCollection.SelectedRow == null)
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, Message.EDITNOTIFICATION);
                return;
            }
            InitializeItems(pnlRoomInfo, true, false);
            _roomInfo = new RoomInfo();
            _roomInfo = RoomInfo.GetRoomInfo(Convert.ToInt32(gvwCollection.SelectedValue));
            
            if (Session[SESSIONROOMINFO] != null)
            {
                Session.Remove(SESSIONROOMINFO);
            }
            Session.Add(SESSIONROOMINFO, _roomInfo);

            RefreshValue();
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    private void RefreshValue()
    {
        _roomInfo = new RoomInfo();
        _roomInfo = (RoomInfo)Session[SESSIONROOMINFO];
        this.txtRoomID.Text = _roomInfo.Id.ToString();
        this.txtRoomNo.Text = _roomInfo.Roomno;
        this.txtRoomName.Text = _roomInfo.Roomname;
        this.txtCapacity.Text = _roomInfo.Capacity.ToString();
        this.txtAddressId.Text = _roomInfo.AddressId.ToString();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            if (gvwCollection.SelectedRow == null)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, Message.DELETENOTIFICATION);
                return;
            }
            RoomInfo.Delete(Convert.ToInt32(gvwCollection.SelectedValue));
            FillList();
            Utilities.ShowMassage(lblMsg, Color.Blue, lblHeader.Text + " - " + Message.SUCCESSFULLYDELETED);
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 547)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, lblHeader.Text + " has been referenced in other tables, please delete those references first.");
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
    private void RefreshObject()
    {
        _roomInfo = null;
        if (Session[SESSIONROOMINFO] == null)
        {
            _roomInfo = new RoomInfo();
        }
        else
        { 
            _roomInfo = (RoomInfo)Session[SESSIONROOMINFO];
        }
        if (txtRoomID.Text != string.Empty)
        {
            _roomInfo.RoomId = Convert.ToInt32(txtRoomID.Text);
        }
        else
        {
            _roomInfo.RoomId = 0;
        }
        _roomInfo.Roomno = txtRoomNo.Text.Trim();
        _roomInfo.Roomname = txtRoomName.Text.Trim();
        _roomInfo.RoomtypeId = Convert.ToInt32(ddlRoomType.SelectedValue.ToString());
        _roomInfo.Capacity = Convert.ToInt32(txtCapacity.Text.Trim());
        _roomInfo.ExamCapacityId = Convert.ToInt32(txtExamCapacity.Text);
        //_roomInfo.CampusId = Convert.ToInt32(ddlCampusName.SelectedValue);
        _roomInfo.BuildingId = Convert.ToInt32(ddlBuildingName.SelectedValue);
        _roomInfo.AddressId = 1;//Convert.ToInt32(txtAddressId.Text);        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshObject();

            if (RoomInfo.HasDuplicate(_roomInfo))
            {
                Utilities.ShowMassage(lblMsg, Color.Red, Message.DUPLICATEMESSAGE);
                return;
            }

            bool isNewRoonInfo = true;
            if (_roomInfo.RoomId == 0)
            {
                _roomInfo.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _roomInfo.CreatedDate = DateTime.Now;
            }
            else
            {
                isNewRoonInfo = false;
                _roomInfo.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _roomInfo.ModifiedDate = DateTime.Now;
            }

            RoomInfo.Save(_roomInfo);
                
            if (isNewRoonInfo)
            {
                lblMsg.Text = string.Empty;
                Utilities.ShowMassage(lblMsg, Color.Blue, lblHeader.Text + " - " + Message.SUCCESSFULLYSAVED);
                InitializeItems(null, true, false);
            }
            else
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, lblHeader.Text + " - " + Message.SUCCESSFULLYUPDATED);
                InitializeItems(null, true, false);
            }

            FillList(_roomInfo.Roomno);

            if (Session[SESSIONROOMINFO] != null)
            {
                Session.Remove(SESSIONROOMINFO);
            }
            Utilities.ClearControls(pnlRoomInfo);
        }
        catch(Exception ex)
        {
            Common.Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally{}
    }
    private void FillList(string strSearch)
    {
        _roomInfo = RoomInfo.GetRoomInfo(strSearch);
        _roomsInfo = new List<RoomInfo>();
        _roomsInfo.Add(_roomInfo);

        if (_roomsInfo == null)
        {
            //DisableButtons();
            return;
        }

        gvwCollection.DataSource = _roomsInfo;
        gvwCollection.DataKeyNames = _dataKey;
        gvwCollection.DataBind();

        //DisableButtons();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeItems(pnlRoomInfo, false, true);
        txtSrch.Focus();
    }
    /// <summary>
    /// Clearing all controls, access permission setting for panel and grid
    /// </summary>
    /// <param name="ctrl"></param>
    /// <param name="boolPanelClassState"></param>
    /// <param name="boolGridstate"></param>
    private void InitializeItems(Control ctrl, bool boolPanelClassState, bool boolGridstate)
    {
        if (ctrl != null)
        {
            Common.Utilities.ClearControls(ctrl);
        }
        lblMsg.Text = string.Empty;
        pnlRoomInfo.Enabled = boolPanelClassState;
        gvwCollection.Enabled = boolGridstate;
    }

    private void LoadCampus() 
    {
        ddlCampusName.Items.Clear();
        _campus = Campus.GetCampus();

        if (_campus != null)
        {
            foreach (Campus cm in _campus)
            {
                ListItem item = new ListItem();
                item.Value = cm.Id.ToString();
                item.Text = cm.CampusName;
                ddlCampusName.Items.Add(item);
            }

            if (Session[SESSIONCAMPUS] != null)
            {
                Session.Remove(SESSIONCAMPUS);
            }
            Session.Add(SESSIONCAMPUS, _campus);

            ddlCampusName.SelectedIndex = 0;
        }
    }

    private void LoadCampusBuilding(int campusId)
    {
        ddlBuildingName.Items.Clear();
        _campusBuilding = CampusBuilding.GetCampusBuildingByCampusId(campusId);

        if (_campusBuilding != null)
        {
            foreach (CampusBuilding cb in _campusBuilding)
            {
                ListItem item = new ListItem();
                item.Value = cb.Id.ToString();
                item.Text = cb.BuildingName;
                ddlBuildingName.Items.Add(item);
            }

            if (Session[SESSIONCAMPUSBUILDING] != null)
            {
                Session.Remove(SESSIONCAMPUSBUILDING);
            }
            Session.Add(SESSIONCAMPUSBUILDING, _campusBuilding);

            ddlBuildingName.SelectedIndex = 0;
        }
    }

    protected void ddlCampusName_SelectedIndexChanged(object sender, EventArgs e)
    {
        int campusId = Convert.ToInt16(ddlCampusName.SelectedValue);
        LoadCampusBuilding(campusId);
    }
}
