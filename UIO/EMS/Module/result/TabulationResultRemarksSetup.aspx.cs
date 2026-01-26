using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.result
{
    public partial class TabulationResultRemarksSetup : BasePage
    {
        int userId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
            User user = UserManager.GetByLogInId(loginID);
            if (user != null)
                userId = user.User_ID;

            ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
            _scriptMan.AsyncPostBackTimeout = 36000;

            lblMsg.Text = "";

            if (!IsPostBack)
            {
                gvRemarks.Visible = false;
                ucProgram.LoadDropdownWithUserAccess(userId);
            }
        }

        #region Event

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvrow = (GridViewRow)(((Button)sender)).NamingContainer;

                HiddenField hdnId = (HiddenField)gvrow.FindControl("hdnId");
                HiddenField hdnStudentId = (HiddenField)gvrow.FindControl("hdnStudentId");
                HiddenField hdnAcaCalId = (HiddenField)gvrow.FindControl("hdnAcaCalId");
                TextBox txtTabulationRemarks = (TextBox)gvrow.FindControl("txtTabulationRemarks");
                TextBox txtResultRemarks = (TextBox)gvrow.FindControl("txtResultRemarks");
                int id = Convert.ToInt32(hdnId.Value);
                int acacalId = Convert.ToInt32(hdnAcaCalId.Value);
                int studentId = Convert.ToInt32(hdnStudentId.Value);

                if (id == 0)
                {
                    TabulationResultRemarks trr = new TabulationResultRemarks();
                    trr.StudentId = studentId;
                    trr.AcaCalId = acacalId;
                    trr.TabulationRemarks = txtTabulationRemarks.Text.Trim();
                    trr.ResultRemarks = txtResultRemarks.Text.Trim();

                    trr.CreatedBy = BaseCurrentUserObj.Id;
                    trr.CreatedDate = DateTime.Now;

                    id = TabulationResultRemarksManager.Insert(trr);

                    if (id != 0)
                    {
                        ShowMessage("Successfylly Saved!", Color.Red);
                    }

                }
                else
                {
                    TabulationResultRemarks trr = TabulationResultRemarksManager.GetById(id);
                    trr.TabulationRemarks = txtTabulationRemarks.Text.Trim();
                    trr.ResultRemarks = txtResultRemarks.Text.Trim();

                    trr.ModifiedBy = BaseCurrentUserObj.Id;
                    trr.ModifiedDate = DateTime.Now;

                    bool isUpdate = TabulationResultRemarksManager.Update(trr);

                    if (isUpdate)
                    {
                        ShowMessage("Successfylly Updated!", Color.Red);
                    }
                }

            }
            catch (Exception)
            {
            }
        }

        protected void btnSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                int iCount = 0;
                int uCount = 0;

                foreach (GridViewRow gvrow in gvRemarks.Rows)
                {
                    HiddenField hdnId = (HiddenField)gvrow.FindControl("hdnId");
                    HiddenField hdnStudentId = (HiddenField)gvrow.FindControl("hdnStudentId");
                    HiddenField hdnAcaCalId = (HiddenField)gvrow.FindControl("hdnAcaCalId");
                    TextBox txtTabulationRemarks = (TextBox)gvrow.FindControl("txtTabulationRemarks");
                    TextBox txtResultRemarks = (TextBox)gvrow.FindControl("txtResultRemarks");
                    int id = Convert.ToInt32(hdnId.Value);
                    int acacalId = Convert.ToInt32(hdnAcaCalId.Value);
                    int studentId = Convert.ToInt32(hdnStudentId.Value);

                    if (id == 0)
                    {
                        TabulationResultRemarks trr = new TabulationResultRemarks();
                        trr.StudentId = studentId;
                        trr.AcaCalId = acacalId;
                        trr.TabulationRemarks = txtTabulationRemarks.Text.Trim();
                        trr.ResultRemarks = txtResultRemarks.Text.Trim();

                        trr.CreatedBy = BaseCurrentUserObj.Id;
                        trr.CreatedDate = DateTime.Now;

                        id = TabulationResultRemarksManager.Insert(trr);

                        if (id != 0)
                        {
                            iCount++;
                        }

                    }
                    else
                    {
                        TabulationResultRemarks trr = TabulationResultRemarksManager.GetById(id);
                        trr.TabulationRemarks = txtTabulationRemarks.Text.Trim();
                        trr.ResultRemarks = txtResultRemarks.Text.Trim();

                        trr.ModifiedBy = BaseCurrentUserObj.Id;
                        trr.ModifiedDate = DateTime.Now;

                        bool isUpdate = TabulationResultRemarksManager.Update(trr);

                        if (isUpdate)
                        {
                            uCount++;
                        }
                    }

                }

                ShowMessage("Successfylly Saved : " + iCount + " Rows. And " + "Successfylly Updated : " + uCount + " Rows ", Color.Red);
            }
            catch (Exception)
            {
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int sessionId = Convert.ToInt32(ucSession.selectedValue);

                string txtRoll = txtStudentId.Text.Trim();

                
                if ((programId != 0 || sessionId != 0 ) && !string.IsNullOrEmpty(txtRoll))
                {
                    ShowMessage("Please Select Roll or ( Program Semester Batch ) !", Color.Red);
                    gvRemarks.DataSource = null;
                    gvRemarks.DataBind();
                }
                else if ((programId == 0 || sessionId == 0 ) && string.IsNullOrEmpty(txtRoll))
                {
                    ShowMessage("Invalid Selection. Please Select Roll or ( Program Semester Batch ) !", Color.Red);
                    gvRemarks.DataSource = null;
                    gvRemarks.DataBind();
                }
                else if ((programId == 0 && sessionId == 0 ) && !string.IsNullOrEmpty(txtRoll))
                {
                    Student student = StudentManager.GetByRoll(txtRoll);
                    if (student != null)
                    {
                        ShowMessage("", Color.Red);
                        List<TabulationResultRemarksDOT> list = TabulationResultRemarksManager.GetAllByProgramSessionBatchStudentId(student.ProgramID, sessionId, student.BatchId, student.StudentID);

                        if (list != null && list.Count > 0)
                        {
                            gvRemarks.Visible = true;

                            gvRemarks.DataSource = list;
                            gvRemarks.DataBind();
                        }
                        else
                        {
                            gvRemarks.DataSource = null;
                            gvRemarks.DataBind();
                        }
                    }
                }
                else
                {
                    int batchId = Convert.ToInt32(ucBatch.selectedValue);
                    if (batchId != 0)
                    {
                        ShowMessage("", Color.Red);
                        List<TabulationResultRemarksDOT> list = TabulationResultRemarksManager.GetAllByProgramSessionBatchStudentId(programId, sessionId, batchId, 0);

                        if (list != null && list.Count > 0)
                        {
                            gvRemarks.Visible = true;

                            gvRemarks.DataSource = list;
                            gvRemarks.DataBind();
                        }
                        else
                        {
                            gvRemarks.DataSource = null;
                            gvRemarks.DataBind();
                        }
                    }
                    else
                    {
                        ShowMessage("Please Select Batch!", Color.Red);
                        gvRemarks.DataSource = null;
                        gvRemarks.DataBind();
                    }


                }

            }
            catch (Exception)
            { }
        }

        protected void btnTabulationApply_Click(object sender, EventArgs e)
        {
            try
            {
                string RemarksText = "";
                int remarksId = Convert.ToInt32(ddlRemarksText.SelectedValue);

                if (remarksId != 0)
                {
                    RemarksText = ddlRemarksText.SelectedItem.Text;
                }
                else
                {
                    RemarksText = txtOtherRemarks.Text.Trim();
                }

                foreach (GridViewRow row in gvRemarks.Rows)
                {
                    TextBox txtTabulationRemarks = (TextBox)row.FindControl("txtTabulationRemarks");
                    txtTabulationRemarks.Text = RemarksText;
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnResultApply_Click(object sender, EventArgs e)
        {
            try
            {
                string RemarksText = "";
                int remarksId = Convert.ToInt32(ddlRemarksText.SelectedValue);

                if (remarksId != 0)
                {
                    RemarksText = ddlRemarksText.SelectedItem.Text;
                }
                else
                {
                    RemarksText = txtOtherRemarks.Text.Trim();
                }

                foreach (GridViewRow row in gvRemarks.Rows)
                {
                    TextBox txtResultRemarks = (TextBox)row.FindControl("txtResultRemarks");
                    txtResultRemarks.Text = RemarksText;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ShowMessage(string Message, Color color)
        {
            lblMsg.Text = Message;
            lblMsg.ForeColor = color;
        }

        #endregion


    }
}