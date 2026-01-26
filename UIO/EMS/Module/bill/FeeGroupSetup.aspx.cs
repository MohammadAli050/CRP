using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.bill
{
    public partial class FeeGroupSetup : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            if (!IsPostBack)
            {
                ucProgram.LoadDropdownWithUserAccess(userObj.Id);
                LoadFeeGroup();
                lblMsg.Text = "";
            }
        }

        private void LoadFeeGroup()
        {
            try
            {
                List<FeeGroupMaster> feeGroupList = FeeGroupMasterManager.GetAll();
                if (feeGroupList != null && feeGroupList.Count > 0)
                {
                    GvFeeGroup.DataSource = feeGroupList;
                    GvFeeGroup.DataBind();
                }
                else 
                {
                    lblMsg.Text = "No fee group found.";
                }
            }
            catch(Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnAddFeeGroup_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                this.ModalFeeGroupPopupExtender.Show();
                ucProgramFeeGroup.LoadDropdownWithUserAccess(userObj.Id);
                ucBatchFeeGroup.LoadDropDownList(0);
                txtFeeGroupName.Text = null;
                ddlFundType.Items.Clear();
                ddlFundType.Items.Add(new ListItem("-Select Fund-", "0"));
                ddlFundType.AppendDataBoundItems = true;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                List<FeeGroupMaster> feeGroupList = FeeGroupMasterManager.GetAll();
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);
                if (feeGroupList!= null && feeGroupList.Count > 0 && programId > 0 && batchId > 0)
                {
                    if (programId > 0)
                    {
                        feeGroupList = feeGroupList.Where(d => d.ProgramId == programId).ToList();
                    }
                    if (batchId > 0)
                    {
                        feeGroupList = feeGroupList.Where(d => d.BatchId == batchId).ToList();
                    }
                }

                GvFeeGroup.DataSource = feeGroupList;
                GvFeeGroup.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                this.ModalShowFeeGroupTypePopupExtender.Show();
                lblShowFeeGroupTypeMsg.Text = null;
                txtFeeGroupTypeAmount.Text = null;
                LoadFeeTypeDDL();
                LinkButton btn = (LinkButton)sender;
                int feeGroupMasterId = int.Parse(btn.CommandArgument.ToString());
                if (feeGroupMasterId > 0)
                {
                    FeeGroupMaster feeGroupMasterObj = FeeGroupMasterManager.GetById(feeGroupMasterId);
                    if (feeGroupMasterObj != null)
                    {
                        lblFeeGroupName.Text = feeGroupMasterObj.FeeGroupName;
                        lblFeeGroupMasterId.Text = Convert.ToString(feeGroupMasterObj.FeeGroupMasterId);
                        List<FeeGroupDetail> feeGroupDetailList = FeeGroupDetailManager.GetByFeeGroupMasterId(feeGroupMasterObj.FeeGroupMasterId);
                        if (feeGroupDetailList.Count > 0 && feeGroupDetailList != null)
                        {
                            GvFeeGroupType.DataSource = feeGroupDetailList;
                            GvFeeGroupType.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void LoadFeeTypeDDL()
        {
            this.ModalShowFeeGroupTypePopupExtender.Show();
            lblShowFeeGroupTypeMsg.Text = string.Empty;
            List<FeeType> feeTypeList = FeeTypeManager.GetAll();
            ddlFeeType.Items.Clear();
            ddlFeeType.Items.Add(new ListItem("-Select Fee-", "0"));
            ddlFeeType.AppendDataBoundItems = true;
            if (feeTypeList != null && feeTypeList.Count > 0)
            {
                ddlFeeType.DataSource = feeTypeList;
                ddlFeeType.DataValueField = "FeeTypeId";
                ddlFeeType.DataTextField = "FeeName";
                ddlFeeType.DataBind();
            }
        }

        protected void ucProgramFeeGroup_ProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            this.ModalFeeGroupPopupExtender.Show();
            lblMessage.Text = string.Empty;
            ucBatchFeeGroup.LoadDropDownList(Convert.ToInt32(ucProgramFeeGroup.selectedValue));
        }

        protected void ucBatchFeeGroup_BatchSelectedIndexChanged(object sender, EventArgs e)
        {
            this.ModalFeeGroupPopupExtender.Show();
            lblMessage.Text = string.Empty;
            List<FundType> fundTypeList = FundTypeManager.GetAll();
            ddlFundType.Items.Clear();
            ddlFundType.Items.Add(new ListItem("-Select Fund-", "0"));
            ddlFundType.AppendDataBoundItems = true;
            if (fundTypeList != null && fundTypeList.Count > 0)
            {
                ddlFundType.DataSource = fundTypeList;
                ddlFundType.DataValueField = "FundTypeId";
                ddlFundType.DataTextField = "FundName";
                ddlFundType.DataBind();
            }
        }

        protected void btnShowFeeGroupCancel_Click(object sender, EventArgs e)
        {

        }

        protected void ddlFundType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ModalFeeGroupPopupExtender.Show();
            lblMessage.Text = string.Empty;
        }

        protected void btnSaveFeeGroup_Click(object sender, EventArgs e)
        {
            this.ModalFeeGroupPopupExtender.Show();
            FeeGroupMaster feeGroupMasterObj = new FeeGroupMaster();
            if (CheckFeeGroupInsertField())
            {
                feeGroupMasterObj.ProgramId = Convert.ToInt32(ucProgramFeeGroup.selectedValue);
                feeGroupMasterObj.BatchId = Convert.ToInt32(ucBatchFeeGroup.selectedValue);
                feeGroupMasterObj.FundTypeId = Convert.ToInt32(ddlFundType.SelectedValue);
                feeGroupMasterObj.FeeGroupName = Convert.ToString(txtFeeGroupName.Text);
                feeGroupMasterObj.CreatedBy = userObj.Id;
                feeGroupMasterObj.CreatedDate = DateTime.Now;
                feeGroupMasterObj.ModifiedBy = userObj.Id;
                feeGroupMasterObj.ModifiedDate = DateTime.Now;
                int result = FeeGroupMasterManager.Insert(feeGroupMasterObj);
                if (result > 0)
                {
                    lblMessage.Text = "Fee group created sucessfully.";
                }
                else
                {
                    lblMessage.Text = "Fee group could not created, please try again providing all necessary field.";
                }
            }
            else 
            {
                lblMessage.Text = "Please provied all necessary field to create Fee Group.";
            }
        }

        private bool CheckFeeGroupInsertField()
        {
            if (Convert.ToInt32(ucProgramFeeGroup.selectedValue) > 0) 
            {
                if (Convert.ToInt32(ucBatchFeeGroup.selectedValue) > 0) 
                {
                    if (Convert.ToInt32(ddlFundType.SelectedValue) > 0) 
                    {
                        if (!string.IsNullOrEmpty(txtFeeGroupName.Text)) 
                        {
                            return true;
                        }
                        else { return false; }
                    }
                    else { return false; }
                }
                else { return false; }
            }
            else { return false; }
        }

        protected void btnFeeGroupTypeCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnFeeGroupTypeSave_Click(object sender, EventArgs e)
        {
            this.ModalShowFeeGroupTypePopupExtender.Show();
            lblShowFeeGroupTypeMsg.Text = null;
            FeeGroupDetail feeGroupDetailObj = new FeeGroupDetail();
            if (CheckFeeGroupTypeInsertField())
            {
                feeGroupDetailObj.FeeGroupMasterId = Convert.ToInt32(lblFeeGroupMasterId.Text);
                feeGroupDetailObj.FeeTypeId = Convert.ToInt32(ddlFeeType.SelectedValue);
                feeGroupDetailObj.Amount = Convert.ToDecimal(txtFeeGroupTypeAmount.Text);
                feeGroupDetailObj.CreatedBy = userObj.Id;
                feeGroupDetailObj.CreatedDate = DateTime.Now;
                feeGroupDetailObj.ModifiedBy = userObj.Id;
                feeGroupDetailObj.ModifiedDate = DateTime.Now;
                List<FeeGroupDetail> feeGroupDetailList = FeeGroupDetailManager.GetByFeeGroupMasterId(Convert.ToInt32(lblFeeGroupMasterId.Text));
                if (feeGroupDetailList.Count > 0 && feeGroupDetailList != null)
                {
                    FeeGroupDetail feeGroupDetailObj2 = feeGroupDetailList.Where(d => d.FeeTypeId == Convert.ToInt32(ddlFeeType.SelectedValue)).FirstOrDefault();
                    if (feeGroupDetailObj2 == null)
                    {
                        int result = FeeGroupDetailManager.Insert(feeGroupDetailObj);
                        if (result > 0)
                        {
                            lblShowFeeGroupTypeMsg.Text = "Fee group type added sucessfully.";

                            GvFeeGroupType.DataSource = feeGroupDetailList;
                            GvFeeGroupType.DataBind();
                        }
                        else
                        {
                            lblShowFeeGroupTypeMsg.Text = "Fee group type could not added, please try again providing all necessary field.";
                        }
                    }
                    else 
                    {
                        lblShowFeeGroupTypeMsg.Text = "Fee group type already exist, please try again using other fee type.";
                    }
                }
                else
                {
                    int result = FeeGroupDetailManager.Insert(feeGroupDetailObj);
                    if (result > 0)
                    {
                        lblShowFeeGroupTypeMsg.Text = "Fee group type added sucessfully.";
                        
                        GvFeeGroupType.DataSource = feeGroupDetailList;
                        GvFeeGroupType.DataBind();
                    }
                    else
                    {
                        lblShowFeeGroupTypeMsg.Text = "Fee group type could not added, please try again providing all necessary field.";
                    }
                }
            }
            else
            {
                lblShowFeeGroupTypeMsg.Text = "Please provied all necessary field to add Fee Group.";
            }
        }

        private bool CheckFeeGroupTypeInsertField()
        {
            if (Convert.ToInt32(lblFeeGroupMasterId.Text) > 0)
            {
                if (Convert.ToInt32(ddlFeeType.SelectedValue) > 0)
                {
                    if (!string.IsNullOrEmpty(txtFeeGroupTypeAmount.Text) && Convert.ToDecimal(txtFeeGroupTypeAmount.Text) > 0)
                    {
                        return true;
                    }
                    else { return false; }
                }
                else { return false; }
            }
            else { return false; }
        }

        protected void ddlFeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ModalShowFeeGroupTypePopupExtender.Show();
            lblShowFeeGroupTypeMsg.Text = null;
        }

        protected void btnRemoveFeeGroupType_Click(object sender, EventArgs e)
        {
            this.ModalShowFeeGroupTypePopupExtender.Show();
            lblShowFeeGroupTypeMsg.Text = null;
            LinkButton btn = (LinkButton)sender;
            int feeGroupDetailId = int.Parse(btn.CommandArgument.ToString());
            if (feeGroupDetailId>0) 
            {
                FeeGroupDetail feeGroupDetailObj = FeeGroupDetailManager.GetById(feeGroupDetailId);
                bool result = FeeGroupDetailManager.Delete(feeGroupDetailId);
                if (result)
                {
                    int feeGroupMasterId = feeGroupDetailObj.FeeGroupMasterId;
                    lblShowFeeGroupTypeMsg.Text = "Fee group type added sucessfully.";
                    List<FeeGroupDetail> feeGroupDetailList = FeeGroupDetailManager.GetByFeeGroupMasterId(feeGroupMasterId);
                    if (feeGroupDetailList.Count > 0 && feeGroupDetailList != null)
                    {
                        GvFeeGroupType.DataSource = feeGroupDetailList;
                        GvFeeGroupType.DataBind();
                    }
                }
                else
                {
                    lblShowFeeGroupTypeMsg.Text = "Fee group type could not added, please try again providing all necessary field.";
                }
            }
        }
    }
}