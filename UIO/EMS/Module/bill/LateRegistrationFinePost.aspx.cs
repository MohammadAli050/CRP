using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using CommonUtility;
using System.Drawing;
using BussinessObject;

namespace EMS.miu.bill
{
    public partial class LateRegistrationFinePost : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            if (!IsPostBack)
            {
                lblCount.Text = "0";
                lblInserted.Text = "0";
                lblUpdated.Text = "0";
                lblNotInserted.Text = "0";
                lblNotUpdated.Text = "0";
                DateTextBox.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtPostingDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                ClearGrid();

                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int registrationSession = Convert.ToInt32(ucRegistrationSession.selectedValue);
                string roll = txtRoll.Text.Trim();
                DateTime regiatrationLastDate = DateTime.ParseExact(DateTextBox.Text.Replace("/", string.Empty), "ddMMyyyy", null);

                if (programId == 0 && ucRegistrationSession.selectedValue == "0")
                {
                    ShowAlertMessage("Please select Program and Registration Session");
                    return;
                }
                else
                {
                    LoadStudent(programId, registrationSession, regiatrationLastDate, roll);
                }
            }
            catch (Exception)
            {
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucRegistrationSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void ucRegistrationSession_SessionSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogicLayer.BusinessObjects.AcademicCalender ac = AcademicCalenderManager.GetById(Convert.ToInt32(ucRegistrationSession.selectedValue));
                DateTextBox.Text = ac.EndDate.ToString("dd/MM/yyyy");
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)sender;

                if (chk.Checked)
                {
                    chk.Text = "Unselect All";
                }
                else
                {
                    chk.Text = "Select All";
                }

                foreach (GridViewRow row in gvStudentList.Rows)
                {

                    CheckBox ckBox = (CheckBox)row.FindControl("ChkStudent");
                    ckBox.Checked = chk.Checked;

                }
            }
            catch (Exception ex)
            {


            }
        }

        protected void chkSelectAllLateFineEntry_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)sender;

                foreach (GridViewRow row in gvStudentList.Rows)
                {
                    CheckBox ckBox = (CheckBox)row.FindControl("ChkIsLateFineEntry");
                    ckBox.Checked = chk.Checked;
                }

                lblCount.Text = SessionManager.GetObjFromSession<string>("StudentLateFine-count");
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int inserted = 0, updated = 0;
            int notInserted = 0, notUpdated = 0;
            UIUMSUser userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            try
            {
                string insertStudents = "";
                string updateStudents = "";
                string notInsertStudents = "";
                string notUpdateStudents = "";
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int acaCalId = Convert.ToInt32(ucRegistrationSession.selectedValue);
                DateTime postingDate = DateTime.ParseExact(txtPostingDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);

                if (programId != 0 && acaCalId != 0)
                {
                    foreach (GridViewRow gvrow in gvStudentList.Rows)
                    {
                        CheckBox checkLateEntry = (CheckBox)gvrow.FindControl("ChkStudent");
                        HiddenField hdnId = (HiddenField)gvrow.FindControl("hdnId");
                        HiddenField hdnBillId = (HiddenField)gvrow.FindControl("hdnBillId");
                        Label roll = (Label)gvrow.FindControl("lblRoll");
                        Label misMatch = (Label)gvrow.FindControl("lblMisMatch");
                        Label calculativeFine = (Label)gvrow.FindControl("lblCalculativeFine");

                        decimal misMatchAmount = Convert.ToDecimal(misMatch.Text);
                        int billId = Convert.ToInt32(hdnBillId.Value);

                        if (checkLateEntry.Checked && misMatchAmount != 0 && billId == 0)
                        {
                            BillHistory bill = new BillHistory();
                            bill.FeeTypeId = 24;
                            bill.StudentId = Convert.ToInt32(hdnId.Value);
                            bill.AcaCalId = acaCalId;
                            bill.Fees = Convert.ToDecimal(calculativeFine.Text);
                            bill.BillingDate = postingDate;
                            bill.CreatedBy = userObj.Id;
                            bill.CreatedDate = DateTime.Now;
                            bill.ModifiedBy = userObj.Id;
                            bill.ModifiedDate = DateTime.Now;
                            int id = BillHistoryManager.Insert(bill);
                            if (id > 0)
                            {
                                inserted++;
                                insertStudents += roll.Text + "," + calculativeFine.Text + " ";
                            }
                            else
                            {
                                notInserted++;
                                notInsertStudents += roll.Text + "," + calculativeFine.Text + " ";
                            }

                        }
                        //else if (checkLateEntry.Checked && billId != 0)
                        //{

                        //    BillHistory bill = BillHistoryManager.GetById(billId);

                        //    if (misMatchAmount != 0 || postingDate.Date != bill.BillingDate.Date)
                        //    {
                        //        bill.Fees = Convert.ToDecimal(calculativeFine.Text);
                        //        bill.BillingDate = postingDate;
                        //        bill.ModifiedBy = userObj.Id;
                        //        bill.ModifiedDate = DateTime.Now;
                        //        bool isUpdate = BillHistoryManager.Update(bill);
                        //        if (isUpdate)
                        //        {
                        //            updated++;
                        //            updateStudents += roll.Text + "," + calculativeFine.Text + " ";
                        //        }
                        //        else
                        //        {
                        //            notUpdated++;
                        //            notUpdateStudents += roll.Text + "," + calculativeFine.Text + " ";
                        //        }
                        //    }
                        //}

                    }
                    if (inserted + updated != 0)
                    {
                        #region Log Insert

                        LogGeneralManager.Insert(DateTime.Now,
                         "",
                         ucRegistrationSession.selectedText,
                         userObj.LogInID,
                         "",
                         "",
                         "Late Registration Fine Entry",
                         userObj.LogInID + " assign late Registration fine to students " + " Inserted List: " + insertStudents + " ; Updated List : " + updateStudents,
                         userObj.LogInID + " is Load Page",
                         ((int)CommonEnum.PageName.LateRegistrationFineEntry).ToString(),
                         CommonEnum.PageName.LateRegistrationFineEntry.ToString(),
                         _pageUrl,
                         "");

                        #endregion
                        ShowAlertMessage("Saved successfully!");
                        ClearGrid();
                    }
                    else
                    {
                        ShowAlertMessage("Nothing to update!");
                    }
                    if (notInserted + notUpdated != 0)
                    {
                        lblMessage.Text = "Not Inserted Bill with StudentId: " + notInsertStudents + "Not Updated Bill with StudentId: " + notUpdateStudents;
                        #region Log Insert

                        LogGeneralManager.Insert(DateTime.Now,
                         "",
                         ucRegistrationSession.selectedText,
                         userObj.LogInID,
                         "",
                         "",
                         "Late Registration Fine Entry",
                         userObj.LogInID + " assign late Registration fine to students " + " Not Inserted List: " + notInsertStudents + " ; Not Updated List : " + notUpdateStudents,
                         userObj.LogInID + " is Load Page",
                         ((int)CommonEnum.PageName.LateRegistrationFineEntry).ToString(),
                         CommonEnum.PageName.LateRegistrationFineEntry.ToString(),
                         _pageUrl,
                         "");

                        #endregion
                    }
                }
                else
                {
                    ShowAlertMessage("Please select Program and Registration Session!");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            btnLoad_Click(null,null);
            lblInserted.Text = inserted.ToString();
            lblUpdated.Text = updated.ToString();
            lblNotInserted.Text = notInserted.ToString();
            lblNotUpdated.Text = notUpdated.ToString();
            
        }

        #endregion

        #region Methods

        private void ClearGrid()
        {
            gvStudentList.DataSource = null;
            gvStudentList.DataBind();

            lblCount.Text = "0";
        }

        private void ShowMessage(string msg)
        {
            lblMessage.Text = msg;
            lblMessage.ForeColor = Color.Red;
        }

        private void ShowAlertMessage(string msg)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ServerControlScript", "alert('" + msg + "');", true);
        }

        private void LoadStudent(int programId, int registrationSession, DateTime registrationLastDate, string Roll)
        {
            List<LateRegistrationFineStudent> studentList = null; // BillHistoryManager.GetLateRegistrationFineStudentList(programId, registrationSession, registrationLastDate);
            if (string.IsNullOrEmpty(Roll) == false)
            {
                studentList = studentList.Where(s => s.Roll == Roll).ToList();
            }

            ClearAllMessages();
            if (studentList != null)
            {
                studentList = studentList.OrderBy(s => s.Roll).ToList();
                gvStudentList.DataSource = studentList;
                gvStudentList.DataBind();

                lblCount.Text = studentList.Count().ToString();
                SessionManager.SaveObjToSession<string>(studentList.Count().ToString(), "StudentLateFine-count");
                
            }
            else
            {
                lblMessage.Text = "No Data Found!";
            }
        }

        private void ClearAllMessages()
        {
            lblMessage.Text = "";
            lblCount.Text = "0";
            lblInserted.Text = "0";
            lblUpdated.Text = "0";
            lblNotInserted.Text = "0";
            lblNotUpdated.Text = "0";
        }

        #endregion
    }
}