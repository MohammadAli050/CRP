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
    public partial class FeeTypeSetup : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            if (!IsPostBack)
            {
                LoadFeeType();
            }
        }

        private void LoadFeeType()
        {
            lblMsg.Text = string.Empty;
            try
            {
                List<FeeType> feeTypeList = FeeTypeManager.GetAll();
                if (feeTypeList != null && feeTypeList.Count > 0)
                {
                    GvFeeType.DataSource = feeTypeList;
                    GvFeeType.DataBind();
                }
                else 
                {
                    lblMsg.Text = "No fee type found.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void GvFeeType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblMsg.Text = string.Empty;
            try
            {
                int feeTypeId = Convert.ToInt16(e.CommandArgument);
                FeeType feeTypeObj = new FeeType();
                feeTypeObj = FeeTypeManager.GetById(feeTypeId);
                lblFeeTypeId.Text = Convert.ToString(feeTypeId);

                if (e.CommandName == "feeTypeDefinitionEdit")
                {
                    this.ModalFeeTypePopupExtender.Show();
                    LoadFeeType(feeTypeObj);
                    btnFeeTypeSave.Visible = false;
                    btnFeeTypeUpdate.Visible = true;
                }
                if (e.CommandName == "feeTypeDefinitionDelete")
                {
                    FeeTypeManager.Delete(feeTypeObj.FeeTypeId);
                    LoadFeeType();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void LoadFeeType(FeeType feeTypeObj)
        {
            lblMsg.Text = string.Empty;
            try
            {
                txtFeeName.Text = feeTypeObj.FeeName;
                txtFeeDefinition.Text = feeTypeObj.FeeDefinition;
                rbIsCourseSpecific.Checked = feeTypeObj.IsCourseSpecific;
                rbIsLifeTimeOnceSpecific.Checked = feeTypeObj.IsLifetimeOnce;
                rbIsPerSemesterSpecific.Checked = feeTypeObj.IsPerSemester;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnAddFeeType_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            try
            {
                this.ModalFeeTypePopupExtender.Show();
                rbIsCourseSpecific.Checked = false;
                rbIsLifeTimeOnceSpecific.Checked = false;
                rbIsPerSemesterSpecific.Checked = false;
                btnFeeTypeSave.Visible = true;
                btnFeeTypeUpdate.Visible = false;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void rbIsCourseSpecific_CheckedChanged(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            try
            {
                this.ModalFeeTypePopupExtender.Show();
                rbIsCourseSpecific.Checked = true;
                rbIsLifeTimeOnceSpecific.Checked = false;
                rbIsPerSemesterSpecific.Checked = false;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void rbIsLifeTimeOnceSpecific_CheckedChanged(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            try
            {
                this.ModalFeeTypePopupExtender.Show();
                rbIsCourseSpecific.Checked = false;
                rbIsLifeTimeOnceSpecific.Checked = true;
                rbIsPerSemesterSpecific.Checked = false;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void rbIsPerSemesterSpecific_CheckedChanged(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            try
            {
                this.ModalFeeTypePopupExtender.Show();
                rbIsCourseSpecific.Checked = false;
                rbIsLifeTimeOnceSpecific.Checked = false;
                rbIsPerSemesterSpecific.Checked = true;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnFeeTypeSave_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            this.ModalFeeTypePopupExtender.Show();
            try
            {
                if (CheckFeeType())
                {
                    FeeType feeTypeObj = new FeeType();
                    feeTypeObj.FeeName = Convert.ToString(txtFeeName.Text);
                    feeTypeObj.FeeDefinition = Convert.ToString(txtFeeDefinition.Text);
                    feeTypeObj.IsCourseSpecific = rbIsCourseSpecific.Checked;
                    feeTypeObj.IsLifetimeOnce = rbIsLifeTimeOnceSpecific.Checked;
                    feeTypeObj.IsPerSemester = rbIsPerSemesterSpecific.Checked;
                    feeTypeObj.CreatedBy = userObj.Id;
                    feeTypeObj.CreatedDate = DateTime.Now;
                    feeTypeObj.ModifiedBy = userObj.Id;
                    feeTypeObj.ModifiedDate = DateTime.Now;
                    int result = FeeTypeManager.Insert(feeTypeObj);
                    if (result > 0)
                    {
                        lblMessage.Text = "Fee type created sucessfully.";
                        LoadFeeType();
                    }
                    else
                    {
                        lblMessage.Text = "Fee type could not created, please try again.";
                    }
                }
                else
                {
                    lblMessage.Text = "Fee type could not created, please try again providing all necessary field.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private bool CheckFeeType()
        {
            bool result = false;
            if (!string.IsNullOrEmpty(txtFeeName.Text)) 
            {
                result = true;
            }
            if (!string.IsNullOrEmpty(txtFeeDefinition.Text))
            {
                result = true;
            }
            return result;
        }

        protected void btnFeeTypeCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnFeeTypeUpdate_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            try 
            { 
                FeeType feeTypeObj = new FeeType();
                feeTypeObj = FeeTypeManager.GetById(Convert.ToInt16(lblFeeTypeId.Text));
                if(feeTypeObj!= null)
                {
                    feeTypeObj.FeeName = Convert.ToString(txtFeeName.Text);
                    feeTypeObj.FeeDefinition = Convert.ToString(txtFeeDefinition.Text);
                    feeTypeObj.IsCourseSpecific = rbIsCourseSpecific.Checked;
                    feeTypeObj.IsLifetimeOnce = rbIsLifeTimeOnceSpecific.Checked;
                    feeTypeObj.IsPerSemester = rbIsPerSemesterSpecific.Checked;
                    feeTypeObj.ModifiedBy = userObj.Id;
                    feeTypeObj.ModifiedDate = DateTime.Now;
                    bool result = FeeTypeManager.Update(feeTypeObj);
                    if (result)
                    {
                        lblMessage.Text = "Fee type updated sucessfully.";
                        LoadFeeType();
                    }
                    else
                    {
                        lblMessage.Text = "Fee type could not updated, please try again.";
                    }
                }
                else
                {
                    lblMessage.Text = "Fee type could not updated, please try again.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}