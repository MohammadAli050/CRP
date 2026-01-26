using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.result
{
    public partial class ExamTemplateCalculativeItemSetup : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            if (!IsPostBack)
            {
                LoadExamTemplateDDL();
                LoadExamMetaTypeDDL();
                LoadExamCalculationType();
                lblMsg.Text = null;
            }
        }

        private void LoadExamTemplateDDL()
        {
            ddlExamTemplateName.DataSource = ExamTemplateMasterManager.GetAll().Where(d => d.ExamTemplateMasterTypeId == (int)CommonUtility.CommonEnum.ExamTemplateType.Calculative).ToList();
            ddlExamTemplateName.DataTextField = "ExamTemplateMasterName";
            ddlExamTemplateName.DataValueField = "ExamTemplateMasterId";
            ddlExamTemplateName.DataBind();
            ListItem item = new ListItem("-Select Exam Template-", "0");
            ddlExamTemplateName.Items.Insert(0, item);
        }

        private void LoadExamMetaTypeDDL()
        {
            ddlExamMetaType.DataSource = ExamMetaTypeManager.GetAll();
            ddlExamMetaType.DataTextField = "ExamMetaTypeName";
            ddlExamMetaType.DataValueField = "ExamMetaTypeId";
            ddlExamMetaType.DataBind();
            ListItem item = new ListItem("-Select Exam Meta Type-", "0");
            ddlExamMetaType.Items.Insert(0, item);
        }

        private void LoadExamCalculationType() 
        {
            ddlExamCalculationType.DataSource = ExamTemplateCalculationTypeManager.GetAll();
            ddlExamCalculationType.DataTextField = "ExamCalculationTypeName";
            ddlExamCalculationType.DataValueField = "ExamCalculationTypeId";
            ddlExamCalculationType.DataBind();
            ListItem item = new ListItem("-Select Exam Calculation Type-", "0");
            ddlExamCalculationType.Items.Insert(0, item);
        }

        protected void ddlTemplateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Text = null;
            int examTemplateMasterId = Convert.ToInt32(ddlExamTemplateName.SelectedValue);
            ExamTemplateMaster examTemplateMasterObj = ExamTemplateMasterManager.GetById(examTemplateMasterId);
            LoadGvExamTemplateItem(examTemplateMasterId);
        }

        private void LoadGvExamTemplateItem(int examTemplateMasterId)
        {
            GvExamTemplateItem.DataSource = ExamTemplateCalculativeFormulaManager.GetByExamTemplateMasterId(examTemplateMasterId).ToList();
            GvExamTemplateItem.DataBind();
        }

        protected void GvExamTemplateItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblMsg.Text = null;
            int examTemplateCalculativeFormulaId = Convert.ToInt16(e.CommandArgument);
            ExamTemplateCalculativeFormula examTemplateCalculativeFormulaObj = new ExamTemplateCalculativeFormula();
            examTemplateCalculativeFormulaObj = ExamTemplateCalculativeFormulaManager.GetById(examTemplateCalculativeFormulaId);

            if (e.CommandName == "ExamTemplateItemEdit")
            {
                lblExamTemplateCalculativeFormulaId.Text = Convert.ToString(examTemplateCalculativeFormulaId);
                LoadExamTemplateItem(examTemplateCalculativeFormulaObj);
                UpdateButton.Visible = true;
                CancelButton.Visible = true;
                AddButton.Visible = false;
            }
            if (e.CommandName == "ExamTemplateItemDelete")
            {
                ExamTemplateCalculativeFormulaManager.Delete(examTemplateCalculativeFormulaId);
                int examTemplateMasterId = Convert.ToInt32(ddlExamTemplateName.SelectedValue);
                LoadGvExamTemplateItem(examTemplateMasterId);
            }
        }

        private void LoadExamTemplateItem(ExamTemplateCalculativeFormula examTemplateCalculativeFormulaObj)
        {
            ddlExamMetaType.SelectedValue = Convert.ToString(examTemplateCalculativeFormulaObj.ExamMetaTypeId);
            ddlExamCalculationType.SelectedValue = Convert.ToString(examTemplateCalculativeFormulaObj.CalculationType);
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlExamMetaType.SelectedValue) > 0 && Convert.ToInt32(ddlExamCalculationType.SelectedValue) > 0)
            {
                ExamTemplateCalculativeFormula examTemplateCalculativeFormulaObj = new ExamTemplateCalculativeFormula();

                examTemplateCalculativeFormulaObj.ExamTemplateMasterId = Convert.ToInt16(ddlExamTemplateName.SelectedValue);
                examTemplateCalculativeFormulaObj.ExamMetaTypeId = Convert.ToInt32(ddlExamMetaType.SelectedValue);
                examTemplateCalculativeFormulaObj.CalculationType = Convert.ToInt32(ddlExamCalculationType.SelectedValue);
                examTemplateCalculativeFormulaObj.CreatedBy = userObj.Id;
                examTemplateCalculativeFormulaObj.CreatedDate = DateTime.Now;
                examTemplateCalculativeFormulaObj.ModifiedBy = userObj.Id;
                examTemplateCalculativeFormulaObj.ModifiedDate = DateTime.Now;

                int result = ExamTemplateCalculativeFormulaManager.Insert(examTemplateCalculativeFormulaObj);

                if (result > 0)
                {
                    LoadGvExamTemplateItem(examTemplateCalculativeFormulaObj.ExamTemplateMasterId);
                    lblMsg.Text = "Exam template item  added successful.";
                    ClearAll();
                }
                else
                {
                    lblMsg.Text = "Exam template item could not added successfully.";
                }
            }
            else
            {
                lblMsg.Text = "Please provide all ncessary field (Mark, Exam Type, Column Sequence).";
            }
        }

        private void ClearAll()
        {
            //LoadExamTemplateDDL();
            LoadExamMetaTypeDDL();
            LoadExamCalculationType();

            UpdateButton.Visible = false;
            CancelButton.Visible = false;
            AddButton.Visible = true;
            lblMsg.Text = null;
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            int examTemplateCalculativeFormulaId = Convert.ToInt32(lblExamTemplateCalculativeFormulaId.Text);
            if (Convert.ToInt32(ddlExamMetaType.SelectedValue) > 0 && Convert.ToInt32(ddlExamCalculationType.SelectedValue) > 0)
            {
                ExamTemplateCalculativeFormula examTemplateCalculativeFormulaObj = ExamTemplateCalculativeFormulaManager.GetById(examTemplateCalculativeFormulaId);


                examTemplateCalculativeFormulaObj.ExamMetaTypeId = Convert.ToInt32(ddlExamMetaType.SelectedValue);
                examTemplateCalculativeFormulaObj.CalculationType = Convert.ToInt32(ddlExamCalculationType.SelectedValue);
                examTemplateCalculativeFormulaObj.ModifiedBy = userObj.Id;
                examTemplateCalculativeFormulaObj.ModifiedDate = DateTime.Now;

                bool result = ExamTemplateCalculativeFormulaManager.Update(examTemplateCalculativeFormulaObj);
                if (result)
                {
                    LoadGvExamTemplateItem(examTemplateCalculativeFormulaObj.ExamTemplateMasterId);
                    lblMsg.Text = "Exam template item edited successfully.";
                    ClearAll();
                }
                else
                {
                    lblMsg.Text = "Exam template item could not edited successfully.";
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

    }
}