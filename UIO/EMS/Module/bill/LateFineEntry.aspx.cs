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
    public partial class LateFineEntry : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            pnlMessage.Visible = false;
         
            if (!IsPostBack)
            {
               // LoadDropDown();
                lblCount.Text = "0";
                DateTextBox.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                ClearGrid();

                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int registrationSession = Convert.ToInt32(ucRegistrationSession.selectedValue);
                DateTime uptoDate = DateTime.ParseExact(DateTextBox.Text.Replace("/", string.Empty), "ddMMyyyy", null);
                decimal amountFrom = string.IsNullOrEmpty(txtAmountFrom.Text) ? 0 : Convert.ToDecimal(txtAmountFrom.Text);
                decimal amountTo = string.IsNullOrEmpty(txtAmountTo.Text) ? 0 : Convert.ToDecimal(txtAmountTo.Text);

                if (programId == 0 && ucRegistrationSession.selectedValue == "0")
                {
                    ShowAlertMessage("Please select Program and Registration Session");
                    return;
                }
                //else if (batchId == 0 && string.IsNullOrEmpty(roll))
                //{
                //    ShowMessage("Please select batch.");
                //    return;
                //}
                else
                {
                    LoadStudent(programId, registrationSession, uptoDate, amountFrom, amountTo);
                }
            }
            catch (Exception)
            {
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucRegistrationSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            ucPostSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
        {
            CleareGrid();
        }

        private void CleareGrid()
        {
            gvStudentList.DataSource = null;
            gvStudentList.DataBind();
        }

        private void LoadStudent(int programId, int registrationSession,DateTime uptoDate,decimal amountFrom, decimal amountTo)
        {
            List<LateFineStudentDTO> studentList = null; //BillHistoryManager.GetLateFineStudentDTO(programId, registrationSession, uptoDate);
               
            studentList = studentList.Where(s => s.Dues > 0).ToList();

            if (amountFrom == 0 && amountTo != 0)
                studentList = studentList.Where(s => s.Dues >= amountFrom && s.Dues <= amountTo).ToList();
            if (amountFrom != 0 && amountTo == 0)
                studentList = studentList.Where(s => s.Dues >= amountFrom && s.Dues <= amountTo).ToList();
            if (amountFrom != 0 && amountTo != 0)
                studentList = studentList.Where(s => s.Dues >= amountFrom && s.Dues <= amountTo).ToList();

            if (studentList != null)
                studentList = studentList.OrderBy(s => s.Roll).ToList();

            gvStudentList.DataSource = studentList;
            gvStudentList.DataBind();

            lblCount.Text = studentList.Count().ToString();
            SessionManager.SaveObjToSession<string>(studentList.Count().ToString(), "StudentLateFine-count");
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

                    CheckBox ckBox = (CheckBox)row.FindControl("ChkBlock");
                    ckBox.Checked = chk.Checked;

                }
            }
            catch (Exception ex)
            {


            }
        }

        #region Methods

        private void ClearGrid()
        {
            gvStudentList.DataSource = null;
            gvStudentList.DataBind();

            lblCount.Text = "0";
        }

        private void LoadDropDown()
        {
        }

        private void ShowMessage(string msg)
        {
            pnlMessage.Visible = true;

            lblMessage.Text = msg;
            lblMessage.ForeColor = Color.Red;

        }

        #endregion       

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

        protected void ucRegistrationSession_SessionSelectedIndexChanged()
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           UIUMSUser userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
           try
           {
               string students = "";
               int programId = Convert.ToInt32(ucProgram.selectedValue);
               int acaCalId = Convert.ToInt32(ucRegistrationSession.selectedValue);
               int postingSession = Convert.ToInt32(ucPostSession.selectedValue);

               if (ucPostSession.selectedValue != "0" && !string.IsNullOrEmpty(txtFees.Text))
               {
                   int checker1=0,checker2=0;
                   foreach (GridViewRow gvrow in gvStudentList.Rows)
                   {
                       CheckBox checkLateEntry = (CheckBox)gvrow.FindControl("ChkIsLateFineEntry");
                       HiddenField hdnId = (HiddenField)gvrow.FindControl("hdnId");
                       Label roll = (Label)gvrow.FindControl("lblRoll");
                       if (checkLateEntry.Checked == true)
                       {
                           checker1++;
                           BillHistory bill = new BillHistory();
                           bill.FeeTypeId = 20;
                           bill.Remark = string.IsNullOrEmpty(txtRemarks.Text) ? null : txtRemarks.Text;
                           bill.Fees = Convert.ToDecimal(txtFees.Text);
                           bill.StudentId = Convert.ToInt32(hdnId.Value);
                           bill.AcaCalId = postingSession;
                           bill.BillingDate = DateTime.Now;
                           bill.CreatedBy = userObj.Id;
                           bill.CreatedDate = DateTime.Now;
                           bill.ModifiedBy = userObj.Id;
                           bill.ModifiedDate = DateTime.Now;
                           int id = BillHistoryManager.Insert(bill);
                           if (id > 0)
                           {
                               checker2++;

                               students += roll.Text +" ";
                           }
                       }
                      
                   }
                   if (checker1 == checker2 && checker1 != 0)
                   {
                       #region Log Insert

                       LogGeneralManager.Insert(
                                                            DateTime.Now,
                                                            "",
                                                            ucRegistrationSession.selectedText,
                                                            userObj.LogInID,
                                                            "",
                                                            "",
                                                            "Late Fine Entry",
                                                            userObj.LogInID + " assign late fine to students " + students + " and amount " + txtFees.Text +" for session "+ ucPostSession.selectedText,
                                                            userObj.LogInID + " is Load Page",
                                                             ((int)CommonEnum.PageName.LateFineEntry).ToString(),
                                                            CommonEnum.PageName.LateFineEntry.ToString(),
                                                            _pageUrl,
                                                            students);
                       #endregion
                       ShowAlertMessage("Saved successfully!");
                       ClearGrid();
                   }
                   else if (checker1 == 0)
                   {
                       ShowAlertMessage("Nothing to update!");
                   }
                   else
                   {
                       ShowAlertMessage("Some data can't be saved!");
                   }
                   
                   
               }
               else
               {
                  // lblMessage.Text = "Plz enter posting session and fees!";

                   ShowAlertMessage("Plz enter posting session and fees!");
               }
           }
            

           catch (Exception ex)
           {

           }
        }

        private void ShowAlertMessage(string msg)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ServerControlScript", "alert('" + msg + "');", true);
        }

    }
}