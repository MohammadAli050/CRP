using EMS.Module;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AcademicCalenderInformation : BasePage
{
    string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
    string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.AcademicCalenderInformation);
    string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.AcademicCalenderInformation));

    UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();

    BussinessObject.UIUMSUser userObj = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            if (!IsPostBack)
            {
                hdnValue.Value = "0";

                LoadCalenderType();

                LoadAcacademicCalenderList();

            }
        }
        catch (Exception Ex)
        {
        }
    }

    private void LoadAcacademicCalenderList()
    {
        try
        {
            List<AcademicCalender> sessionList = new List<AcademicCalender>();
            int CalenderUnitMasterId = 0;
            try
            {
                CalenderUnitMasterId = Convert.ToInt32(ddlCalenderMaster.SelectedValue);
            }
            catch (Exception ex)
            {
            }

            if (CalenderUnitMasterId > 0)
                sessionList = AcademicCalenderManager.GetAll(CalenderUnitMasterId);
            else
                sessionList = AcademicCalenderManager.GetAll();

            if (sessionList != null && sessionList.Count > 0)
            {
                gvSessionList.DataSource = sessionList.OrderByDescending(a => a.AcademicCalenderID).ToList();
                gvSessionList.DataBind();
            }
            else
            {
                gvSessionList.DataSource = null;
                gvSessionList.DataBind();
            }


        }
        catch (Exception ex)
        {
        }
    }

    private void LoadCalenderType()
    {
        try
        {
            var CalenderUnitMasterList = ucamContext.CalenderUnitMasters.ToList();

            ddlCalenderMaster.Items.Clear();
            ddlCalenderMaster.AppendDataBoundItems = true;
            ddlCalenderMaster.Items.Add(new ListItem("All", "0"));

            if (CalenderUnitMasterList != null && CalenderUnitMasterList.Count > 0)
            {
                ddlCalenderMaster.DataTextField = "Name";
                ddlCalenderMaster.DataValueField = "CalenderUnitMasterID";
                ddlCalenderMaster.DataSource = CalenderUnitMasterList.OrderBy(x => x.CalenderUnitMasterID).ToList();
                ddlCalenderMaster.DataBind();
            }

        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlCalenderType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAcacademicCalenderList();
    }

    private void LoadMaxSequence()
    {
        try
        {
            int TypeId = Convert.ToInt32(ddlType.SelectedValue);
            var MaxObj = AcademicCalenderManager.GetAll(TypeId).OrderByDescending(x => x.Sequence).FirstOrDefault();

            if (MaxObj != null)
            {
                txtSequence.Text = Convert.ToString(MaxObj.Sequence + 1);
            }
            else
            {
                txtSequence.Text = "1";
            }

        }
        catch (Exception ex)
        {
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            btnAddNew.Text = "Add New";
            hdnValue.Value = "0";
            LoadModalCalendarType();
            LoadModalCalenderUnitType();
            LoadYear();
            ModalPopupExtender1.Show();
            ClearFields();
        }
        catch (Exception ex)
        {
        }
    }

    private void LoadModalCalendarType()
    {
        try
        {
            var CalenderUnitMasterList = ucamContext.CalenderUnitMasters.ToList();
            ddlType.Items.Clear();
            ddlType.AppendDataBoundItems = true;
            ddlType.Items.Add(new ListItem("Select", "0"));
            if (CalenderUnitMasterList != null && CalenderUnitMasterList.Count > 0)
            {
                ddlType.DataTextField = "Name";
                ddlType.DataValueField = "CalenderUnitMasterID";
                ddlType.DataSource = CalenderUnitMasterList.OrderBy(x => x.CalenderUnitMasterID).ToList();
                ddlType.DataBind();
            }

        }
        catch (Exception ex)
        {
        }
    }

    private void LoadModalCalenderUnitType()
    {
        try
        {
            int TypeId = Convert.ToInt32(ddlType.SelectedValue);

            var CalenderUnitList = ucamContext.CalenderUnitTypes.Where(x => x.CalenderUnitMasterID == TypeId || TypeId == 0).ToList();
            ddlCalenderUnitType.Items.Clear();
            ddlCalenderUnitType.AppendDataBoundItems = true;
            ddlCalenderUnitType.Items.Add(new ListItem("Select", "0"));
            if (CalenderUnitList != null && CalenderUnitList.Count > 0)
            {
                ddlCalenderUnitType.DataTextField = "TypeName";
                ddlCalenderUnitType.DataValueField = "CalenderUnitTypeID";
                ddlCalenderUnitType.DataSource = CalenderUnitList.OrderBy(x => x.CalenderUnitTypeID).ToList();
                ddlCalenderUnitType.DataBind();
            }

        }
        catch (Exception ex)
        {
        }
    }

    private void LoadYear()
    {
        try
        {
            ddlYear.Items.Clear();
            ddlYear.AppendDataBoundItems = true;
            ddlYear.Items.Add(new ListItem("Select", "0"));
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear + 1; i >= 2015; i--)
            {
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
        catch (Exception ex)
        {
            // Log the exception
        }
    }

    private void ClearFields()
    {
        txtSequence.Text = string.Empty;
        ddlCalenderUnitType.SelectedValue = "0";
        ddlType.SelectedValue = "0";
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        ddlYear.SelectedValue = "0";

    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ModalPopupExtender1.Show();
            LoadModalCalenderUnitType();
            LoadMaxSequence();

        }
        catch (Exception ex)
        {
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        try
        {
            int CalenderUnitMasterId = Convert.ToInt32(ddlType.SelectedValue);
            int CalenderUnitTypeId = Convert.ToInt32(ddlCalenderUnitType.SelectedValue);
            int Year = Convert.ToInt32(ddlYear.SelectedValue);
            int Sequence = 0;
            string SessionCode = string.Empty;

            int updatedValue = 0;
            try
            {
                updatedValue = Convert.ToInt32(hdnValue.Value);
            }
            catch (Exception ex)
            {
            }

            SessionCode = txtSessionCode.Text.Trim();

            #region Validation

            try
            {
                Sequence = Convert.ToInt32(txtSequence.Text);
            }
            catch (Exception ex)
            {
            }
            if (CalenderUnitMasterId == 0)
            {
                showAlert("Select Calendar Type.");
                ModalPopupExtender1.Show();
                return;
            }
            if (CalenderUnitTypeId == 0)
            {
                showAlert("Select Calendar Unit Type.");
                ModalPopupExtender1.Show();
                return;
            }
            if (Year == 0)
            {
                showAlert("Select Year.");
                ModalPopupExtender1.Show();
                return;
            }
            if (string.IsNullOrEmpty(txtStartDate.Text))
            {
                showAlert("Select Start Date.");
                ModalPopupExtender1.Show();
                return;
            }
            if (string.IsNullOrEmpty(txtEndDate.Text))
            {
                showAlert("Select End Date.");
                ModalPopupExtender1.Show();
                return;
            }

            DateTime StartDate = DateTime.ParseExact(txtStartDate.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture); //Convert.ToDateTime(txtStartDate.Text);
            DateTime EndDate = DateTime.ParseExact(txtEndDate.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture); //Convert.ToDateTime(txtStartDate.Text);

            if (StartDate > EndDate)
            {
                showAlert("Start Date must be less than End Date.");
                ModalPopupExtender1.Show();
                return;
            }

            #endregion

            AcademicCalender existingList = AcademicCalenderManager.GetAll(CalenderUnitMasterId).Where(x => x.Year == Year && x.CalenderUnitTypeID == CalenderUnitTypeId && x.AcademicCalenderID != updatedValue).FirstOrDefault();

            if (existingList != null)
            {
                showAlert("Data already exists.");
                ModalPopupExtender1.Show();
                return;
            }

            #region Insert 

            string AllInputFields = "CalenderUnitType : " + ddlCalenderUnitType.SelectedItem.ToString() + ", Year : " + Year + ", StartDate : " + StartDate.ToString("dd-MM-yyyy") + ", EndDate: " + EndDate.ToString("dd-MM-yyyy") + ", Sequence: " + Sequence + ", Code: " + SessionCode;

            if (updatedValue == 0)
            {
                AcademicCalender obj = new AcademicCalender();
                obj.CalenderUnitTypeID = CalenderUnitTypeId;
                obj.Year = Year;
                obj.StartDate = StartDate;
                obj.EndDate = EndDate;
                obj.Sequence = Sequence;
                obj.Code = SessionCode;
                obj.CreatedBy = userObj.Id;
                obj.CreatedDate = DateTime.Now;
                obj.ModifiedBy = userObj.Id;
                obj.ModifiedDate = DateTime.Now;
                int result = AcademicCalenderManager.Insert(obj);
                if (result > 0)
                {
                    showAlert("Data saved successfully.");
                    LoadAcacademicCalenderList();
                    ClearFields();
                    hdnValue.Value = "0";

                    MisscellaneousCommonMethods.InsertLog(userObj.LogInID,"Academic Calender Insert",userObj.LogInID+" inserted a new academic calender with information " + AllInputFields, "","",_pageId,_pageName,_pageUrl);
                }
                else
                {
                    showAlert("Data not saved.");
                    ModalPopupExtender1.Show();
                    return;
                }
            }
            #endregion

            #region Update 

            else
            {
                AcademicCalender obj = AcademicCalenderManager.GetById(updatedValue);
                if (obj != null)
                {
                    obj.CalenderUnitTypeID = CalenderUnitTypeId;
                    obj.Year = Year;
                    obj.StartDate = StartDate;
                    obj.EndDate = EndDate;
                    obj.Sequence = Sequence;
                    obj.Code = SessionCode;
                    obj.ModifiedBy = userObj.Id;
                    obj.ModifiedDate = DateTime.Now;
                    bool result = AcademicCalenderManager.Update(obj);
                    if (result)
                    {
                        showAlert("Data updated successfully.");
                        LoadAcacademicCalenderList();
                        ClearFields();
                        hdnValue.Value = "0";

                        MisscellaneousCommonMethods.InsertLog(userObj.LogInID, "Academic Calender Update", userObj.LogInID + " updated an academic calender with information " + AllInputFields, "", "", _pageId, _pageName, _pageUrl);

                    }
                    else
                    {
                        showAlert("Data not updated.");
                        ModalPopupExtender1.Show();
                        return;
                    }
                }
                else
                {
                    showAlert("Data not found.");
                    ModalPopupExtender1.Show();
                    return;
                }
            }
            #endregion

        }
        catch (Exception ex)
        {
        }
    }


    protected void showAlert(string msg)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lb = (LinkButton)sender;
            int AcademicCalenderID = Convert.ToInt32(lb.CommandArgument);
            AcademicCalender obj = AcademicCalenderManager.GetById(AcademicCalenderID);
            if (obj != null)
            {
                btnAddNew.Text = "Update";
                hdnValue.Value = Convert.ToString(obj.AcademicCalenderID);
                LoadModalCalendarType();
                LoadModalCalenderUnitType();
                LoadYear();
                var calenderUnitType = CalenderUnitTypeManager.GetById(obj.CalenderUnitTypeID);
                ddlType.SelectedValue = Convert.ToString(calenderUnitType.CalenderUnitMasterID);
                LoadModalCalenderUnitType();
                ddlCalenderUnitType.SelectedValue = Convert.ToString(obj.CalenderUnitTypeID);
                ddlYear.SelectedValue = Convert.ToString(obj.Year);
                txtStartDate.Text = Convert.ToDateTime(obj.StartDate).ToString("dd-MM-yyyy");
                txtEndDate.Text = Convert.ToDateTime(obj.EndDate).ToString("dd-MM-yyyy");
                txtSequence.Text = Convert.ToString(obj.Sequence);
                txtSessionCode.Text = obj.Code;
                ModalPopupExtender1.Show();
            }
            else
            {
                showAlert("Data not found.");
                return;
            }
        }
        catch (Exception ex)
        {
        }

    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lb = (LinkButton)sender;
            int AcademicCalenderID = Convert.ToInt32(lb.CommandArgument);

            var batchList = ucamContext.Batches.Where(x => x.AcaCalId == AcademicCalenderID).ToList();

            if (batchList != null && batchList.Count > 0)
            {
                showAlert("This session is in use for Batch. You cannot delete it.");
                return;
            }

            bool result = AcademicCalenderManager.Delete(AcademicCalenderID);
            if (result)
            {
                showAlert("Data deleted successfully.");
                LoadAcacademicCalenderList();
            }
            else
            {
                showAlert("Data not deleted.");
            }
        }
        catch (Exception ex)
        {
        }
    }
}