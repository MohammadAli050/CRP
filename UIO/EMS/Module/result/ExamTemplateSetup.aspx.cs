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
    public partial class ExamTemplateSetup : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.ExamTemplateSetup);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.ExamTemplateSetup));
        BussinessObject.UIUMSUser userObj = null;
        UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            if (!IsPostBack)
            {
                LoadExamTemplateGrid();
                lblMsg.Text = string.Empty;
                LoadProgram();
            }
        }

        private void LoadProgram()
        {
            try
            {
                var programList = ucamContext.Programs.ToList();

                ddlProgram.Items.Clear();
                ddlProgram.AppendDataBoundItems = true;
                ddlProgram.Items.Add(new ListItem("-Select-", "0"));

                if (programList != null)
                {
                    ddlProgram.DataTextField = "ShortName";
                    ddlProgram.DataValueField = "ProgramID";

                    ddlProgram.DataSource = programList;
                    ddlProgram.DataBind();
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string s = "Active";
                if (ddlIsActive.SelectedValue == "0")
                    s = "In-" + s;
                this.mp1.Show();
                ExamTemplateMaster examTemplateMaster = new ExamTemplateMaster();
                examTemplateMaster.ExamTemplateMasterName = txtExamTemplateName.Text;
                examTemplateMaster.ExamTemplateMasterTotalMark = Convert.ToInt32(txtExamTemplateMark.Text);
                examTemplateMaster.ExamTemplateMasterTypeId = Convert.ToInt32(ddlExamTemplateType.SelectedValue);

                examTemplateMaster.ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
                examTemplateMaster.Attribute1 = ddlIsActive.SelectedValue;


                examTemplateMaster.CreatedBy = userObj.Id;
                examTemplateMaster.CreatedDate = DateTime.Now;
                examTemplateMaster.ModifiedBy = userObj.Id;
                examTemplateMaster.ModifiedDate = DateTime.Now;

                if (ExamTemplateMasterManager.GetExamTemplateMasterByName(examTemplateMaster.ExamTemplateMasterName))
                {
                    int result = ExamTemplateMasterManager.Insert(examTemplateMaster);

                    if (result > 0)
                    {
                        InsertLog("Add New Template", userObj.LogInID + " added a new template with Name : " + txtExamTemplateName.Text.ToString() + ", Template Mark : " + txtExamTemplateMark.Text.ToString()
                            + ", Template Type : " + ddlExamTemplateType.SelectedItem.Text.ToString() + ", Status : " + s + " and Program Name : " + ddlProgram.SelectedItem.Text.ToString());
                        lblMsg.Text = "Exam template created successfully.";
                        mp1.Hide();
                        LoadExamTemplateGrid();
                    }
                    else
                    {
                        lblMsg.Text = "Exam template could not created.";
                    }
                }
                else
                {
                    lblMsg.Text = "Exam template already exist.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string s = "Active", PrevInfo = "", prevS = "Active", Type = "Basic";
                if (ddlIsActive.SelectedValue == "0")
                    s = "In-" + s;
                this.mp1.Show();
                int examTemplateMasterId = Convert.ToInt32(lblExamTemplateMasterId.Text);
                ExamTemplateMaster examTemplateMaster = new ExamTemplateMaster();
                examTemplateMaster = ExamTemplateMasterManager.GetById(examTemplateMasterId);
                if (examTemplateMaster.ExamTemplateMasterTypeId == 2)
                    Type = "Calculative";
                if (examTemplateMaster.Attribute1 == "0")
                    prevS = "In-" + prevS;
                PrevInfo = "Name : " + examTemplateMaster.ExamTemplateMasterName + ", Mark : " + examTemplateMaster.ExamTemplateMasterTotalMark + ", Type : " + Type + ", Status : " + prevS + " and Program : " + examTemplateMaster.ShortName;
                examTemplateMaster.ExamTemplateMasterName = txtExamTemplateName.Text;
                examTemplateMaster.ExamTemplateMasterTotalMark = Convert.ToInt32(txtExamTemplateMark.Text);
                examTemplateMaster.ExamTemplateMasterTypeId = Convert.ToInt32(ddlExamTemplateType.SelectedValue);

                examTemplateMaster.ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
                examTemplateMaster.Attribute1 = ddlIsActive.SelectedValue;


                examTemplateMaster.ModifiedBy = userObj.Id;
                examTemplateMaster.ModifiedDate = DateTime.Now;

                ExamTemplateMaster newExamTemplateMasterObj = new ExamTemplateMaster();
                newExamTemplateMasterObj = ExamTemplateMasterManager.GetById(examTemplateMasterId);
                if (examTemplateMaster.ExamTemplateMasterName == newExamTemplateMasterObj.ExamTemplateMasterName)
                {
                    if (ExamTemplateMasterManager.Update(examTemplateMaster))
                    {
                        InsertLog("Update Template", userObj.LogInID + " updated a template with Name : " + txtExamTemplateName.Text.ToString() + ", Template Mark : " + txtExamTemplateMark.Text.ToString()
                            + ", Template Type : " + ddlExamTemplateType.SelectedItem.Text.ToString() + ", Status : " + s + " and Program Name : " + ddlProgram.SelectedItem.Text.ToString()
                            + " with previous information " + PrevInfo);
                        lblMsg.Text = "Course exam template edited successfully.";
                        mp1.Hide();
                        LoadExamTemplateGrid();
                    }
                    else
                    {
                        lblMsg.Text = "Course exam template could not edited.";
                    }
                }
                else
                {
                    if (ExamTemplateMasterManager.GetExamTemplateMasterByName(examTemplateMaster.ExamTemplateMasterName))
                    {
                        if (ExamTemplateMasterManager.Update(examTemplateMaster))
                        {
                            InsertLog("Update Template", userObj.LogInID + " updated a template with Name : " + txtExamTemplateName.Text.ToString() + ", Template Mark : " + txtExamTemplateMark.Text.ToString()
                            + ", Template Type : " + ddlExamTemplateType.SelectedItem.Text.ToString() + ", Status : " + s + " and Program Name : " + ddlProgram.SelectedItem.Text.ToString()
                            + " with previous information " + PrevInfo);
                            lblMsg.Text = "Course exam template edited successfully.";
                            LoadExamTemplateGrid();
                        }
                        else
                        {
                            lblMsg.Text = "Course exam template could not edited.";
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Course exam template already exist.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            this.mp1.Hide();
            txtExamTemplateName.Text = string.Empty;
            txtExamTemplateMark.Text = string.Empty;
            lblMsg.Text = string.Empty;
            btnUpdate.Visible = false;
            btnClose.Visible = true;
            btnSave.Visible = true;
        }

        protected void GvExamTemplate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string PrevInfo = "", prevS = "Active", Type = "Basic";

                ddlIsActive.SelectedValue = "0";
                lblMsg.Text = "";

                int examTemplateMasterId = Convert.ToInt32(e.CommandArgument);
                ExamTemplateMaster examTemplateMasterObj = new ExamTemplateMaster();
                examTemplateMasterObj = ExamTemplateMasterManager.GetById(examTemplateMasterId);
                lblExamTemplateMasterId.Text = Convert.ToString(examTemplateMasterId);

                if (e.CommandName == "ExamTemplateEdit")
                {
                    this.mp1.Show();
                    LoadAllTextBox(examTemplateMasterObj);

                    if (examTemplateMasterObj != null && examTemplateMasterObj.ProgramId > 0)
                    {
                        ddlProgram.SelectedValue = examTemplateMasterObj.ProgramId.ToString();
                    }

                    if (examTemplateMasterObj != null && Convert.ToInt32(examTemplateMasterObj.Attribute1) > 0)
                    {
                        ddlIsActive.SelectedValue = examTemplateMasterObj.Attribute1;
                    }

                    btnUpdate.Visible = true;
                    btnClose.Visible = true;
                    btnSave.Visible = false;
                }
                if (e.CommandName == "ExamTemplateDelete")
                {
                    if (examTemplateMasterObj.ExamTemplateMasterTypeId == 2)
                        Type = "Calculative";
                    if (examTemplateMasterObj.Attribute1 == "0")
                        prevS = "In-" + prevS;
                    PrevInfo = "Name : " + examTemplateMasterObj.ExamTemplateMasterName + ", Mark : " + examTemplateMasterObj.ExamTemplateMasterTotalMark + ", Type : " + Type + ", Status : " + prevS + " and Program : " + examTemplateMasterObj.ShortName;
                    ExamTemplateMasterManager.Delete(examTemplateMasterId);
                    InsertLog("Delete Template", userObj.LogInID + " deleted a template with info " + PrevInfo);
                    LoadExamTemplateGrid();
                }
            }
            catch (Exception ex)
            { }
        }

        public void LoadExamTemplateGrid()
        {
            GvExamTemplate.DataSource = ExamTemplateMasterManager.GetAll();//.OrderBy(o => o.ExamTemplateMasterName).ToList();
            GvExamTemplate.DataBind();
        }

        private void LoadAllTextBox(ExamTemplateMaster examTemplateMasterObj)
        {
            txtExamTemplateName.Text = examTemplateMasterObj.ExamTemplateMasterName;
            txtExamTemplateMark.Text = Convert.ToString(examTemplateMasterObj.ExamTemplateMasterTotalMark);
            ddlExamTemplateType.SelectedValue = Convert.ToString(examTemplateMasterObj.ExamTemplateMasterTypeId);
        }

        protected void GvExamTemplate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvExamTemplate.PageIndex = e.NewPageIndex;
            LoadExamTemplateGrid();
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                LoadProgram();
                ddlIsActive.SelectedValue = "0";
            }
            catch (Exception ex)
            { }
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

        protected void showAlert(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
        }

    }
}