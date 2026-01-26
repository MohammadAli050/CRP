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

public partial class BasicSetup_RoomType : BasePage
{
    #region Session Names
    private const string SESSIONROOMTYPEINFO = "ROOMTYPEINFO";
    #endregion

    #region Variable Declaration
    List<RoomType> _roomTypesInfo = null;
    RoomType _roomTypeInfo = null;
    private string[] _dataKey = new string[1] { "Id" };
    #endregion

    #region Functions
    private void InitializeItems(Control ctrl, bool boolPanelClassState, bool boolGridstate, bool boolBtnFindState, bool boolBtnAddState, bool boolBtnEditState, bool boolBtnDeleteState)
    {
        if (ctrl != null)
        {
            Common.Utilities.ClearControls(ctrl);
        }
        lblMsg.Text = string.Empty;
        pnlRoomType.Enabled = boolPanelClassState;
        gvwCollection.Enabled = boolGridstate;

        btnFind.Enabled = boolBtnFindState;
        btnAdd.Enabled = boolBtnAddState;
        btnEdit.Enabled = boolBtnEditState;
        btnDelete.Enabled = boolBtnDeleteState;
    }
    private void RefreshValue()
    {
        _roomTypeInfo = new RoomType();
        _roomTypeInfo = (RoomType)Session[SESSIONROOMTYPEINFO];
        this.txtRoomTypeID.Text = _roomTypeInfo.Id.ToString();
        this.txtRoomType.Text = _roomTypeInfo.Roomtypename;
    }
    /// <summary>
    /// find all data
    /// </summary>
    private void FillList()
    {
        try
        {
            _roomTypesInfo = RoomType.GetRoomTypesInfo(txtSrch.Text.Trim());
            if (_roomTypesInfo == null)
            {
                gvwCollection.DataSource = null;
                gvwCollection.DataBind();
                Utilities.ShowMassage(lblMsg, Color.Blue, lblHeader.Text + " - " + Message.NOTFOUND);
                return;
            }
            gvwCollection.DataSource = _roomTypesInfo;
            gvwCollection.DataKeyNames = _dataKey;
            gvwCollection.DataBind();
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
        finally
        { }
    }
    private void RefreshObject()
    {
        _roomTypeInfo = null;
        if (Session[SESSIONROOMTYPEINFO] == null)
        {
            _roomTypeInfo = new RoomType();
        }
        else
        {
            _roomTypeInfo = (RoomType)Session[SESSIONROOMTYPEINFO];
        }
        if (txtRoomTypeID.Text != string.Empty)
        {
            _roomTypeInfo.RoomtypeId = Convert.ToInt32(txtRoomTypeID.Text);
        }
        else
        {
            _roomTypeInfo.RoomtypeId = 0;
        }
        _roomTypeInfo.Roomtypename = txtRoomType.Text.Trim();
    }
    /// <summary>
    /// find specifiq data
    /// </summary>
    /// <param name="strSearch"></param>
    private void FillList(string strSearch)
    {
        _roomTypeInfo = RoomType.GetRoomType(strSearch);
        _roomTypesInfo = new List<RoomType>();
        _roomTypesInfo.Add(_roomTypeInfo);

        if (_roomTypesInfo == null)
        {
            //DisableButtons();
            return;
        }

        gvwCollection.DataSource = _roomTypesInfo;
        gvwCollection.DataKeyNames = _dataKey;
        gvwCollection.DataBind();

        //DisableButtons();
    } 
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            base.CheckPage_Load();
            if (!IsPostBack)
            {
                InitializeItems(this.Page, false, true, true, true, false, false);
                txtSrch.Focus();
            }
            btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete?');");
            
        }
        catch (Exception ex)
        {
            Common.Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        FillList();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            InitializeItems(pnlRoomType, true, false, false, false, false, false);
            txtRoomType.Focus();
            if (Session[SESSIONROOMTYPEINFO] != null)
            {
                Session.Remove(SESSIONROOMTYPEINFO);
            }
        }
        catch (Exception ex)
        {
            Common.Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshObject();

            if (RoomType.HasDuplicateType(_roomTypeInfo))
            {
                Utilities.ShowMassage(lblMsg, Color.Red, Message.DUPLICATEMESSAGE);
                txtRoomType.Focus();
                return;
            }

            bool isNewRoomTypeInfo = true;
            if (_roomTypeInfo.RoomtypeId == 0)
            {
                _roomTypeInfo.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _roomTypeInfo.CreatedDate = DateTime.Now;
            }
            else
            {
                isNewRoomTypeInfo = false;
                _roomTypeInfo.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _roomTypeInfo.ModifiedDate = DateTime.Now;
            }

            RoomType.Save(_roomTypeInfo);
            InitializeItems(null, true, false, false, false, false, false);
            if (isNewRoomTypeInfo)
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, lblHeader.Text + " - " + Message.SUCCESSFULLYSAVED);
            }
            else
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, lblHeader.Text + " - " + Message.SUCCESSFULLYUPDATED);
            }
            FillList(_roomTypeInfo.Roomtypename);
            if (Session[SESSIONROOMTYPEINFO] != null)
            {
                Session.Remove(SESSIONROOMTYPEINFO);
            }
            Utilities.ClearControls(pnlRoomType);
            lblMsg.Text = string.Empty;
        }
        catch (Exception ex)
        {
            Common.Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
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
            InitializeItems(pnlRoomType, true, false, false, false, false, false);

            _roomTypeInfo = new RoomType();
            _roomTypeInfo = RoomType.GetRoomType(Convert.ToInt32(gvwCollection.SelectedValue));

            if (Session[SESSIONROOMTYPEINFO] != null)
            {
                Session.Remove(SESSIONROOMTYPEINFO);
            }
            Session.Add(SESSIONROOMTYPEINFO, _roomTypeInfo);

            RefreshValue();
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitializeItems(pnlRoomType, false, true, true, true, true, true);
        txtSrch.Focus();
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
            RoomType.Delete(Convert.ToInt32(gvwCollection.SelectedValue));
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
    protected void gvwCollection_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitializeItems(pnlRoomType, false, true, true, true, true, true);
    }
    protected void txtRoomType_TextChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
    }
    #endregion
}
