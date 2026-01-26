using System;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using System.Drawing;
using BussinessObject;
using Common;
using System.Data;
using System.Data.SqlTypes;

namespace EMS.Bill
{
    public partial class BillCollection : BasePage
    {
        private List<AccountsHead> _accountsHead = null;
        Voucher _voucher = null;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Page.Request.ServerVariables["http_user_agent"].ToLower().Contains("safari"))
            {
                Page.ClientTarget = "uplevel";
            }
        }
        private void Initialize()
        {
            //pnlCheque.Visible = false;
            lblMsg.Text = "";
            pnlContainer.Enabled = false;     
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UIUMSUser CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
                if (CurrentUser != null)
                {
                    if (CurrentUser.RoleID > 0)
                    {
                        AuthenticateHome(CurrentUser);
                    }
                }
                else
                {
                    Response.Redirect("~/Security/Login.aspx");
                }
                if (!IsPostBack && !IsCallback)
                {
                    //btnSave.Attributes.Add("onclick", "return confirm('Do you want to save?');");
                    FillAccCombo();
                    Initialize();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void FillAccCombo()
        {
            ddlBankAcc.Items.Clear();

            _accountsHead = AccountsHead.GetByParentID(144);

            if (_accountsHead != null)
            {
                ListItem item = new ListItem();
                item.Value = "0";
                item.Text = "NA";
                ddlBankAcc.Items.Add(item);

                foreach (AccountsHead accountsHead in _accountsHead)
                {
                    //if (accountsHead.Name == "BankAccount")
                    //{
                        item = new ListItem();
                        item.Value = accountsHead.Id.ToString();
                        item.Text = accountsHead.Name;

                        ddlBankAcc.Items.Add(item);
                    //}
                }

                ddlBankAcc.SelectedIndex = 0;
            }
        }       

        protected void btnShowName_Click(object sender, EventArgs e)
        {
            string studentName = Student.GetStudentNameByRoll(txtRoll.Text.Trim());

            if (studentName == string.Empty)
            {
                CleareControl();
                Utilities.ShowMassage(lblMsg, Color.Red, "No students found");
                pnlContainer.Enabled = false;
            }
            else 
            {
                txtName.Text = studentName;
                lblMsg.Text = "";
                pnlContainer.Enabled = true;
            }
        }        

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ddlBankAcc.SelectedValue == "0")
            {
                Utilities.ShowMassage(lblMsg, Color.Blue, "Select Bank account");
                return;
            }

           _voucher= RefreshObject();

          int effectedRow = Voucher.Save(_voucher);

          if (effectedRow > 0)
          {
              CleareControl();
              Utilities.ShowMassage(lblMsg, Color.Blue, "Data Saved Succesfully");
          }
          else
          {
              Utilities.ShowMassage(lblMsg, Color.Blue, "Data can not be Saved");
          }
        }

        private Voucher RefreshObject()
        {
            Voucher voucher = new Voucher();

            voucher.VouPrefix = "Bill_COllection";
            voucher.DrAcHeadsId = Convert.ToInt32( ddlBankAcc.SelectedValue);
            voucher.CrAcHeadsId = AccountsHead.GetByname(txtRoll.Text.Trim()).Id;//for crAccHead id.
            voucher.Amount = Convert.ToDecimal(txtAmount.Text.Trim());
            voucher.ChequeNo = txtChequeNo.Text.Trim();
            voucher.ChequeBankName = txtBankName.Text.Trim();
            voucher.ChequeDate = dtpCheque.Date.ToString() == "1/1/0001 12:00:00 AM" ? Convert.ToDateTime(SqlDateTime.MinValue.ToString()) : dtpCheque.Date;
            voucher.Remarks = txtRemarks.Text.Trim();
            voucher.ReferenceNo = txtReferenceNo.Text.Trim();


           // string str = SqlDateTime.MinValue.ToString();

            voucher.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
            voucher.CreatedDate = DateTime.Now;

            return voucher;            
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            CleareControl();
        }

        private void CleareControl()
        {
            lblMsg.Text = "";
            dtpCheque.Text = "";
            Utilities.ClearControls(this);
            pnlContainer.Enabled = false;
        }                
    }
}
