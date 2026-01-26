using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RoomInfoSetup : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        //base.CheckPage_Load();

        if (!IsPostBack)
        {
            LoadComboBox();
        }
    }

    protected void LoadComboBox()
    {
        try
        {
            LoadRoomTypeComboBox();
            LoadCampusComboBox();
        }
        catch { }
        finally { }
    }

    protected void LoadRoomTypeComboBox()
    {
        try
        {
            ddlRoomType.Items.Clear();
            //ddlCourse.Items.Add(new ListItem("Select", "0_0"));
            ddlRoomType.AppendDataBoundItems = true;

            List<RoomType> roomTypeList = RoomTypeManager.GetAll();
            if (roomTypeList.Count > 0 && roomTypeList != null)
            {
                ddlRoomType.DataSource = roomTypeList;
                ddlRoomType.DataTextField = "TypeName";
                ddlRoomType.DataValueField = "RoomTypeID";
                ddlRoomType.DataBind();
            }
        }
        catch { }
        finally { }
    }

    protected void LoadCampusComboBox()
    {
        try
        {
            ddlCampus.Items.Clear();
            //ddlCampus.Items.Add(new ListItem("Select", "0_0"));
            ddlCampus.AppendDataBoundItems = true;

            List<Campus> campusList = CampusManager.GetAll();
            if (campusList.Count > 0 && campusList != null)
            {
                ddlCampus.DataSource = campusList;
                ddlCampus.DataValueField = "CampusId";
                ddlCampus.DataTextField = "CampusName";
                ddlCampus.DataBind();

                Campus_Changed(null, null);
            }
        }
        catch { }
        finally { }
    }

    protected void LoadBuildingComboBox(int campusId)
    {
        try
        {
            ddlBuilding.Items.Clear();
            //ddlBuilding.Items.Add(new ListItem("Select", "0_0"));
            ddlBuilding.AppendDataBoundItems = true;

            List<Building> buildingList = BuildingManager.GetAll();
            if (buildingList.Count > 0 && buildingList != null)
            {
                buildingList = buildingList.Where(x => x.CampusId == campusId).ToList();
                if (buildingList.Count > 0 && buildingList != null)
                {
                    ddlBuilding.DataSource = buildingList;
                    ddlBuilding.DataValueField = "BuildingId";
                    ddlBuilding.DataTextField = "BuildingName";
                    ddlBuilding.DataBind();
                }
            }
        }
        catch { }
        finally { }
    }

    protected void LoadAllRoomInformation()
    {
        try
        {
            List<RoomType> roomTypeList = RoomTypeManager.GetAll();
            Hashtable hashRoomType = new Hashtable();
            foreach (RoomType roomType in roomTypeList)
                hashRoomType.Add(roomType.RoomTypeID.ToString(), roomType.TypeName);

            List<Campus> campusList = CampusManager.GetAll();
            Hashtable hashCampus = new Hashtable();
            foreach (Campus campus in campusList)
                hashCampus.Add(campus.CampusId.ToString(), campus.CampusName);

            List<Building> buildingList = BuildingManager.GetAll();
            Hashtable hashBuilding = new Hashtable();
            foreach (Building building in buildingList)
                hashBuilding.Add(building.BuildingId.ToString(), building.BuildingName);

            List<RoomInformation> roomInfoList = RoomInformationManager.GetAll();
            if (roomInfoList.Count > 0 && roomInfoList != null)
            {
                foreach (RoomInformation roomInfo in roomInfoList)
                {
                    roomInfo.RomeTypeName = hashRoomType[roomInfo.RoomTypeID.ToString()] == null ? "" : hashRoomType[roomInfo.RoomTypeID.ToString()].ToString();
                    roomInfo.CampusName = hashCampus[roomInfo.AddressID.ToString()] == null ? "" : hashCampus[roomInfo.AddressID.ToString()].ToString();
                    roomInfo.BuildingName = hashBuilding[roomInfo.BuildingId.ToString()] == null ? "" : hashBuilding[roomInfo.BuildingId.ToString()].ToString();
                }

                if (roomInfoList.Count > 0 && roomInfoList != null)
                {
                    gvRoomInfoSetup.DataSource = roomInfoList;
                    gvRoomInfoSetup.DataBind();
                }
            }
        }
        catch { }
        finally { }
    }

    protected void LoadingRoomInformation(int id)
    {
        try
        {
            RoomInformation roomInfo = RoomInformationManager.GetById(id);
            if (roomInfo != null)
            {
                if (roomInfo.AddressID != 0)
                    LoadBuildingComboBox(roomInfo.AddressID);

                hfRoomInfoID.Value = id.ToString();

                txtRoomNumber.Text = roomInfo.RoomNumber;
                txtRoomName.Text = roomInfo.RoomName;
                if (roomInfo.RoomTypeID != 0)
                    ddlRoomType.SelectedValue = roomInfo.RoomTypeID.ToString();
                if (roomInfo.AddressID != 0)
                    ddlCampus.SelectedValue = roomInfo.AddressID.ToString();
                if (roomInfo.BuildingId != 0)
                    ddlBuilding.SelectedValue = roomInfo.BuildingId.ToString();
                txtClassCapacity.Text = roomInfo.Capacity.ToString();
                txtExamCapacity.Text = roomInfo.ExamCapacity.ToString();
                txtTotalRows.Text = roomInfo.Rows.ToString();
                txtTotalColumns.Text = roomInfo.Columns.ToString();

                btnSave.Text = "Update";
                btnLoad.Text = "Cancel";
            }
        }
        catch { }
        finally { }
    }

    protected void clearField()
    {
        try
        {
            txtRoomNumber.Text = "";
            txtRoomName.Text = "";
            txtClassCapacity.Text = "";
            txtExamCapacity.Text = "";
            txtTotalRows.Text = "";
            txtTotalColumns.Text = "";
            LoadComboBox();
        }
        catch { }
        finally { }
    }

    protected void CreateRoomInformation()
    {
        try
        {
            int createdBy = 99;
            string loginId = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
            User user = UserManager.GetByLogInId(loginId);
            if (user != null)
                createdBy = user.User_ID;

            RoomInformation roomInformation = new RoomInformation();

            roomInformation.RoomNumber = txtRoomNumber.Text;
            roomInformation.RoomName = txtRoomName.Text;
            roomInformation.RoomTypeID = Convert.ToInt32(ddlRoomType.SelectedValue);
            roomInformation.Capacity = txtClassCapacity.Text == "" ? 0 : Convert.ToInt32(txtClassCapacity.Text);
            roomInformation.ExamCapacity = txtExamCapacity.Text == "" ? 0 : Convert.ToInt32(txtExamCapacity.Text);
            roomInformation.Rows = txtTotalRows.Text == "" ? 0 : Convert.ToInt32(txtTotalRows.Text);
            roomInformation.Columns = txtTotalColumns.Text == "" ? 0 : Convert.ToInt32(txtTotalColumns.Text);
            roomInformation.BuildingId = Convert.ToInt32(ddlBuilding.SelectedValue);
            roomInformation.AddressID = Convert.ToInt32(ddlCampus.SelectedValue);
            roomInformation.CreatedBy = createdBy;
            roomInformation.CreatedDate = DateTime.Now;
            roomInformation.ModifiedBy = createdBy;
            roomInformation.ModifiedDate = DateTime.Now;

            int resultInsert = RoomInformationManager.Insert(roomInformation);

            if (resultInsert > 0)
            {
                clearField();
                lblMsg.Text = "Room Create Successfully";

                lblMsg.Focus();
            }
            else
            {
                lblMsg.Text = "Room Create Fail";

                lblMsg.Focus();
            }
        }
        catch { }
        finally { }
    }

    protected void UpdateRoomInformation()
    {
        try
        {
            int modifiedId = 99;
            string loginId = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
            User user = UserManager.GetByLogInId(loginId);
            if (user != null)
                modifiedId = user.User_ID;

            int id = Convert.ToInt32(hfRoomInfoID.Value);

            RoomInformation roomInformation = RoomInformationManager.GetById(id);

            if (roomInformation != null)
            {
                roomInformation.RoomNumber = txtRoomNumber.Text;
                roomInformation.RoomName = txtRoomName.Text;
                roomInformation.RoomTypeID = Convert.ToInt32(ddlRoomType.SelectedValue);
                roomInformation.Capacity = Convert.ToInt32(txtClassCapacity.Text);
                roomInformation.ExamCapacity = Convert.ToInt32(txtExamCapacity.Text);
                roomInformation.Rows = Convert.ToInt32(txtTotalRows.Text);
                roomInformation.Columns = Convert.ToInt32(txtTotalColumns.Text);
                roomInformation.BuildingId = Convert.ToInt32(ddlBuilding.SelectedValue);
                roomInformation.AddressID = Convert.ToInt32(ddlCampus.SelectedValue);
                roomInformation.ModifiedBy = modifiedId;
                roomInformation.ModifiedDate = DateTime.Now;

                bool resultUpdate = RoomInformationManager.Update(roomInformation);

                if (resultUpdate)
                {
                    clearField();
                    lblMsg.Text = "Room Updated Successfully";

                    btnLoad.Text = "Load";
                    btnSave.Text = "Save";

                    btnLoad_Click(null, null);

                    lblMsg.Focus();
                }
                else
                {
                    lblMsg.Text = "Room Update Fail";

                    lblMsg.Focus();
                }
            }
        }
        catch { }
        finally { }
    }

    #endregion

    #region Event

    protected void Campus_Changed(Object sender, EventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(ddlCampus.SelectedValue);

            LoadBuildingComboBox(id);
        }
        catch { }
        finally { }
    }

    protected void btnSave_Click(Object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
                CreateRoomInformation();
            else
                UpdateRoomInformation();
        }
        catch { }
        finally { }
    }

    protected void btnLoad_Click(Object sender, EventArgs e)
    {
        try
        {
            if (btnLoad.Text == "Load")
                LoadAllRoomInformation();
            else
            {
                clearField();
                btnLoad.Text = "Load";
                btnSave.Text = "Save";
            }
        }
        catch { }
        finally { }
    }

    protected void lbEdit_Click(Object sender, EventArgs e)
    {
        try
        {
            clearField();

            LinkButton linkButton = new LinkButton();
            linkButton = (LinkButton)sender;
            int id = Convert.ToInt32(linkButton.CommandArgument);

            LoadingRoomInformation(id);
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

            bool resultDelete = RoomInformationManager.Delete(id);

            if (resultDelete)
            {
                clearField();
                lblMsg.Text = "Room Deleted Successfully";

                btnLoad.Text = "Load";
                btnSave.Text = "Save";

                btnLoad_Click(null, null);

                lblMsg.Focus();
            }
            else
            {
                lblMsg.Text = "Room Deleted Fail";
                lblMsg.Focus();
            }
        }
        catch { }
        finally { }
    }

    #endregion
}