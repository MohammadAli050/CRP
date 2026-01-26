using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.Result
{
    public partial class ExamTemplateItemSetup : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.ExamTemplateItemSetup);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.ExamTemplateItemSetup));
        BussinessObject.UIUMSUser userObj = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            if (!IsPostBack)
            {
                LoadExamTemplateDDL();
                LoadExamMetaTypeDDL();

                ddlExamType.DataSource = null;
                ddlExamType.DataBind();
                ListItem item = new ListItem("-Select Exam Type-", "0");
                ddlExamType.Items.Insert(0, item);
            }
        }

        private void LoadExamTemplateDDL()
        {
            ddlExamTemplateName.DataSource = ExamTemplateMasterManager.GetAll().Where(d => d.ExamTemplateMasterTypeId == (int)CommonUtility.CommonEnum.ExamTemplateType.Basic).ToList();
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

        private void LoadExamTypeDDL(int examMetaTypeId)
        {
            ddlExamType.DataSource = ExamTypeManager.GetAll().Where(d => d.ExamMetaTypeId == examMetaTypeId).ToList();
            ddlExamType.DataTextField = "ExamTypeName";
            ddlExamType.DataValueField = "ExamTypeId";
            ddlExamType.DataBind();
            ListItem item = new ListItem("-Select Exam Type-", "0");
            ddlExamType.Items.Insert(0, item);
        }

        protected void ddlTemplateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int examTemplateMasterId = Convert.ToInt32(ddlExamTemplateName.SelectedValue);
            ExamTemplateMaster examTemplateMasterObj = ExamTemplateMasterManager.GetById(examTemplateMasterId);
            txtTemplateMark.Text = Convert.ToString(examTemplateMasterObj.ExamTemplateMasterTotalMark);
            LoadGvExamTemplateItem(examTemplateMasterId);
        }

        protected void ddlExamMetaType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int examMetaTypeId = Convert.ToInt32(ddlExamMetaType.SelectedValue);
            LoadExamTypeDDL(examMetaTypeId);
            if (Convert.ToInt32(ddlExamType.SelectedValue) == 0)
                ddlExamType.SelectedValue = examMetaTypeId.ToString();
            if (string.IsNullOrEmpty(txtExamName.Text))
                txtExamName.Text = ddlExamMetaType.SelectedItem.ToString();
        }

        private void LoadGvExamTemplateItem(int examTemplateMasterId)
        {
            GvExamTemplateItem.DataSource = ExamTemplateBasicItemDetailsManager.GetByExamTemplateMasterId(examTemplateMasterId).ToList();
            GvExamTemplateItem.DataBind();
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtExamMark.Text) && Convert.ToDecimal(txtExamMark.Text) > 0 && Convert.ToInt32(ddlExamType.SelectedValue) > 0 && !string.IsNullOrEmpty(txtSequence.Text)
                && !string.IsNullOrEmpty(txtConvertTo.Text) && Convert.ToDecimal(txtConvertTo.Text) > 0
                )
            {
                string s = "Visible";
                if (ddlIsVisible.SelectedValue == "0")
                    s = "In-" + s;
                ExamTemplateBasicItemDetails examTemplateBasicItemDetailsObj = new ExamTemplateBasicItemDetails();

                examTemplateBasicItemDetailsObj.ExamTemplateMasterId = Convert.ToInt16(ddlExamTemplateName.SelectedValue);
                examTemplateBasicItemDetailsObj.ExamTypeId = Convert.ToInt32(ddlExamType.SelectedValue);
                if (!string.IsNullOrEmpty(txtExamName.Text))
                {
                    examTemplateBasicItemDetailsObj.ExamTemplateBasicItemName = txtExamName.Text;
                }
                else
                {
                    examTemplateBasicItemDetailsObj.ExamTemplateBasicItemName = Convert.ToString(ddlExamType.SelectedItem);
                }
                examTemplateBasicItemDetailsObj.ExamTemplateBasicItemMark = Convert.ToDecimal(txtExamMark.Text);
                examTemplateBasicItemDetailsObj.ColumnSequence = Convert.ToInt32(txtSequence.Text);

                examTemplateBasicItemDetailsObj.Attribute1 = ddlIsVisible.SelectedValue;

                examTemplateBasicItemDetailsObj.Attribute2 = Convert.ToDecimal(txtConvertTo.Text.Trim()).ToString("0.00");

                examTemplateBasicItemDetailsObj.CreatedBy = userObj.Id;
                examTemplateBasicItemDetailsObj.CreatedDate = DateTime.Now;
                examTemplateBasicItemDetailsObj.ModifiedBy = userObj.Id;
                examTemplateBasicItemDetailsObj.ModifiedDate = DateTime.Now;

                int result = ExamTemplateBasicItemDetailsManager.Insert(examTemplateBasicItemDetailsObj);

                if (result > 0)
                {
                    InsertLog("Add New Item In Template", userObj.LogInID + " added a new item in template with Template Name : " + ddlExamTemplateName.SelectedItem.Text.ToString() + ", Meta Type : " + ddlExamType.SelectedItem.Text.ToString() + ",Exam Name : " + txtExamName.Text.ToString()
                           + ", Item Mark : " + txtExamMark.Text.ToString() + ", Convert to Marks : " + txtConvertTo.Text.ToString() + ",and Status : " + s);
                    showAlert("Exam template created successfully.");
                    LoadGvExamTemplateItem(examTemplateBasicItemDetailsObj.ExamTemplateMasterId);
                    showAlert("Exam template item  added successful.");
                    ClearAll();
                }
                else
                {
                    showAlert("Exam template item could not added successfully.");
                }
            }
            else
            {
                showAlert("Please provide all ncessary field (Marks,Convert to marks, Exam Type, Column Sequence).");
            }
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            string s = "Visible", PrevInfo = "", prevS = "Visible";
            if (ddlIsVisible.SelectedValue == "0")
                s = "In-" + s;
            int examTemplateBasicItemId = Convert.ToInt32(lblExamTemplateBasicItemId.Text);
            if (examTemplateBasicItemId > 0 && !string.IsNullOrEmpty(txtExamMark.Text) && Convert.ToInt32(ddlExamType.SelectedValue) > 0 && !string.IsNullOrEmpty(txtSequence.Text)
                 && !string.IsNullOrEmpty(txtConvertTo.Text))
            {
                ExamTemplateBasicItemDetails examTemplateBasicItemDetailsObj = ExamTemplateBasicItemDetailsManager.GetById(examTemplateBasicItemId);
                if (examTemplateBasicItemDetailsObj.Attribute1 == "0")
                    prevS = "In-" + prevS;
                PrevInfo = "Template Name : " + examTemplateBasicItemDetailsObj.ExamTemplateMaster.ExamTemplateMasterName + ", Exam Name : " + examTemplateBasicItemDetailsObj.ExamTemplateBasicItemName
                    + ", Mark : " + examTemplateBasicItemDetailsObj.ExamTemplateBasicItemMark + ", convert to marks : " + examTemplateBasicItemDetailsObj.Attribute2 + ", Status : " + prevS;


                examTemplateBasicItemDetailsObj.ExamTypeId = Convert.ToInt32(ddlExamType.SelectedValue);
                if (!string.IsNullOrEmpty(txtExamName.Text))
                {
                    examTemplateBasicItemDetailsObj.ExamTemplateBasicItemName = txtExamName.Text;
                }
                else
                {
                    examTemplateBasicItemDetailsObj.ExamTemplateBasicItemName = Convert.ToString(ddlExamType.SelectedItem);
                }
                examTemplateBasicItemDetailsObj.ExamTemplateBasicItemMark = Convert.ToDecimal(txtExamMark.Text);
                examTemplateBasicItemDetailsObj.ColumnSequence = Convert.ToInt32(txtSequence.Text);


                examTemplateBasicItemDetailsObj.Attribute1 = ddlIsVisible.SelectedValue;
                examTemplateBasicItemDetailsObj.Attribute2 = Convert.ToDecimal(txtConvertTo.Text.Trim()).ToString("0.00");


                examTemplateBasicItemDetailsObj.ModifiedBy = userObj.Id;
                examTemplateBasicItemDetailsObj.ModifiedDate = DateTime.Now;

                bool result = ExamTemplateBasicItemDetailsManager.Update(examTemplateBasicItemDetailsObj);
                if (result)
                {
                    InsertLog("Update Item In Template", userObj.LogInID + " Updated item in template with Template Name : " + ddlExamTemplateName.SelectedItem.Text.ToString() + ", Meta Type : " + ddlExamType.SelectedItem.Text.ToString() + ",Exam Name : " + txtExamName.Text.ToString()
                           + ", Item Mark : " + txtExamMark.Text.ToString() + ", convert to marks : " + txtConvertTo.Text.ToString() + ",and Status : " + s + " with previous info : " + PrevInfo);
                    LoadGvExamTemplateItem(examTemplateBasicItemDetailsObj.ExamTemplateMasterId);
                    showAlert("Exam template item edited successfully.");
                    ClearAll();
                }
                else
                {
                    showAlert("Exam template item could not edited successfully.");
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            //LoadExamTemplateDDL();
            LoadExamMetaTypeDDL();
            LoadExamTypeDDL(0);
            txtExamName.Text = string.Empty;
            txtExamMark.Text = string.Empty;
            txtConvertTo.Text = string.Empty;
            txtSequence.Text = string.Empty;

            UpdateButton.Visible = false;
            CancelButton.Visible = false;
            AddButton.Visible = true;
        }


        protected void showAlert(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
        }
        protected void GvExamTemplateItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                ddlIsVisible.SelectedValue = "0";

                int examTemplateBasicItemId = Convert.ToInt16(e.CommandArgument);
                ExamTemplateBasicItemDetails examTemplateBasicItemDetailsObj = new ExamTemplateBasicItemDetails();
                examTemplateBasicItemDetailsObj = ExamTemplateBasicItemDetailsManager.GetById(examTemplateBasicItemId);

                if (e.CommandName == "ExamTemplateItemEdit")
                {
                    lblExamTemplateBasicItemId.Text = Convert.ToString(examTemplateBasicItemId);
                    LoadExamTemplateItem(examTemplateBasicItemDetailsObj);

                    if (examTemplateBasicItemDetailsObj != null && Convert.ToInt32(examTemplateBasicItemDetailsObj.Attribute1) > 0)
                    {
                        ddlIsVisible.SelectedValue = examTemplateBasicItemDetailsObj.Attribute1;
                    }


                    UpdateButton.Visible = true;
                    CancelButton.Visible = true;
                    AddButton.Visible = false;
                }
                if (e.CommandName == "ExamTemplateItemDelete")
                {
                    string PrevInfo = "", prevS = "Visible";
                    if (examTemplateBasicItemDetailsObj.Attribute1 == "0")
                        prevS = "In-" + prevS;
                    PrevInfo = "Template Name : " + examTemplateBasicItemDetailsObj.ExamTemplateMaster.ExamTemplateMasterName + ", Exam Name : " + examTemplateBasicItemDetailsObj.ExamTemplateBasicItemName
                        + ", Mark : " + examTemplateBasicItemDetailsObj.ExamTemplateBasicItemMark + ", Status : " + prevS;
                    ExamTemplateBasicItemDetailsManager.Delete(examTemplateBasicItemId);
                    InsertLog("Delete Item from Template", userObj.LogInID + " deleted a item from Template with Information : " + PrevInfo);
                    int examTemplateMasterId = Convert.ToInt32(ddlExamTemplateName.SelectedValue);
                    LoadGvExamTemplateItem(examTemplateMasterId);
                }
            }
            catch (Exception ex)
            { }
        }

        private void LoadExamTemplateItem(ExamTemplateBasicItemDetails examTemplateBasicItemDetailsObj)
        {
            ExamType examObj = ExamTypeManager.GetById(examTemplateBasicItemDetailsObj.ExamTypeId);
            if (examTemplateBasicItemDetailsObj.ExamTypeId > 0)
            {
                ExamTypeManager.GetById(examTemplateBasicItemDetailsObj.ExamTypeId);
                LoadExamTypeDDL(examObj.ExamMetaTypeId);
            }

            ddlExamTemplateName.SelectedValue = Convert.ToString(examTemplateBasicItemDetailsObj.ExamTemplateMasterId);
            ddlExamMetaType.SelectedValue = Convert.ToString(examObj.ExamMetaTypeId);
            ddlExamType.SelectedValue = Convert.ToString(examObj.ExamTypeId);
            //ExamTemplate examTemplateObj = ExamTemplateBasicItemDetailsManager.GetById(examTemplateBasicItemDetailsObj.ExamTemplateBasicItemId);
            txtTemplateMark.Text = Convert.ToString(examTemplateBasicItemDetailsObj.ExamTemplateMaster.ExamTemplateMasterTotalMark);
            txtExamName.Text = Convert.ToString(examTemplateBasicItemDetailsObj.ExamTemplateBasicItemName);
            txtExamMark.Text = Convert.ToString(examTemplateBasicItemDetailsObj.ExamTemplateBasicItemMark);
            txtSequence.Text = Convert.ToString(examTemplateBasicItemDetailsObj.ColumnSequence);
            txtConvertTo.Text = Convert.ToString(examTemplateBasicItemDetailsObj.Attribute2);
        }
        private void InsertLog(string EventName, string Message)
        {
            LogGeneralManager.Insert(
                                      DateTime.Now,
                                      "",
                                      "",
                                      userObj.LogInID,
                                      "",
                                      "",
                                      EventName,
                                      Message,
                                      "Normal",
                                      _pageId,
                                      _pageName,
                                      _pageUrl,
                                      "");
        }

        protected void GvExamTemplateItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                decimal totalMarks = 0, convertedTotal = 0;

                // Calculate sum of all ExamTemplateBasicItemMark values
                foreach (GridViewRow row in GvExamTemplateItem.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        // Get the mark value from the cell (column index 3)
                        string markText = row.Cells[3].Text;
                        decimal mark;
                        if (decimal.TryParse(markText, out mark))
                        {
                            totalMarks += mark;
                        }

                        // Get the mark value from the cell (column index 4)
                        string convertmarkText = row.Cells[4].Text;
                        decimal convertmark;
                        if (decimal.TryParse(convertmarkText, out convertmark))
                        {
                            convertedTotal += convertmark;
                        }

                    }
                }

                // Display the total in the footer
                e.Row.Cells[3].Text = totalMarks.ToString("0.##");
                e.Row.Cells[3].Font.Bold = true;

                // Display the total in the footer
                e.Row.Cells[4].Text = convertedTotal.ToString("0.##");
                e.Row.Cells[4].Font.Bold = true;

            }
        }
    }
}