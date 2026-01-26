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
    public partial class ResultRemarksUpdate : BasePage
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
                lblResult.Visible = false;
                gvResult.Visible = false; 
                ucProgram.LoadDropdownWithUserAccess(userId);
            }
        }         

        #region Event

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        { 
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        } 

        protected void ViewGroup_Click(Object sender, EventArgs e)
        {
            try
            {
                int semesterNo = Convert.ToInt32(ddlSemesterNo.SelectedValue);
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);

                List<StudentACUDetail> studentACUDetailList = StudentACUDetailManager.GetAllByAcaCalProgramBatchStudentForRemarks(semesterNo, programId, batchId, "");
                if (studentACUDetailList.Count > 0 && studentACUDetailList != null)
                {
                    lblResult.Visible = true;
                    gvResult.Visible = true;

                    gvResult.DataSource = studentACUDetailList;
                    gvResult.DataBind();
                }
                else
                {
                    lblResult.Visible = false;
                    gvResult.DataSource = null;
                    gvResult.DataBind();
                }
            }
            catch { }
            finally { }
        }

        protected void ViewStudent_Click(Object sender, EventArgs e)
        {
            try
            {
                int acaCalId = Convert.ToInt32(ddlAcaCal2.SelectedValue);
                string studentId = txtStudentId.Text;

                if (studentId.Length == 0)
                {
                    lblMsg.Text = "Please Student ID";

                    lblResult.Visible = false;
                    gvResult.Visible = false;
                }
                //else if (studentId.Length != 12)
                //{
                //    lblMsg.Text = "Student ID format is not Correct";

                //    lblResult.Visible = false;
                //    gvResult.Visible = false;
                //}
                else
                {
                    List<StudentACUDetail> studentACUDetailList = StudentACUDetailManager.GetAllByAcaCalProgramBatchStudent(acaCalId, 0, 0, studentId);
                    if (studentACUDetailList.Count > 0 && studentACUDetailList != null)
                    {
                        lblResult.Visible = true;
                        gvResult.Visible = true;

                        gvResult.DataSource = studentACUDetailList;
                        gvResult.DataBind();
                    }
                    else
                    {
                        lblResult.Visible = false;
                        gvResult.DataSource = null;
                        gvResult.DataBind();
                    }
                }
            }
            catch { }
            finally { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvrow = (GridViewRow)(((Button)sender)).NamingContainer;
                 
                HiddenField hdnId = (HiddenField)gvrow.FindControl("hdnId");
                TextBox txtRemarks = (TextBox)gvrow.FindControl("txtRemarks");

                StudentACUDetail acu = StudentACUDetailManager.GetById(Convert.ToInt32(hdnId.Value));

                if (acu != null)
                {
                    acu.Remarks = txtRemarks.Text.Trim();

                    acu.ModifiedBy = BaseCurrentUserObj.Id;
                    acu.ModifiedDate = DateTime.Now;

                    bool isUpdate = StudentACUDetailManager.Update(acu);
                    if (isUpdate)
                    { 
                        lblMsg.Text = "Successfylly Updated";
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
                int count = 0;
                foreach (GridViewRow gvrow in gvResult.Rows)
                {
                    HiddenField hdnId = (HiddenField)gvrow.FindControl("hdnId");
                    TextBox txtRemarks = (TextBox)gvrow.FindControl("txtRemarks");

                    StudentACUDetail acu = StudentACUDetailManager.GetById(Convert.ToInt32(hdnId.Value));

                    if (acu != null)
                    {
                        acu.Remarks = txtRemarks.Text.Trim();

                        acu.ModifiedBy = BaseCurrentUserObj.Id;
                        acu.ModifiedDate = DateTime.Now;

                        bool isUpdate = StudentACUDetailManager.Update(acu);
                         
                        if (isUpdate)
                        { 
                            count++;
                        }
                    }

                }

                lblMsg.Text = "Successfylly Updated : " + count + " Rows ";
            }
            catch (Exception)
            {
            }
        }


        #endregion 
         
        protected void btnApply_Click(object sender, EventArgs e)
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

                foreach (GridViewRow row in gvResult.Rows)
                {
                    TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                    txtRemarks.Text = RemarksText;
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}