using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.bill
{
    public partial class PaymentPosting : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            if (!IsPostBack)
            {
                ucProgram.LoadDropdownWithUserAccess(userObj.Id);
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
                ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            }
            catch (Exception ex)
            {
                lblMsg.Text = "On Program Selected Index Changed Error.";
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                LoadPayment();
            }
            catch(Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void LoadPayment()
        {
            try
            {
                List<PaymentPostingDTO> paymentPostingList = new List<PaymentPostingDTO>();

                if (string.IsNullOrEmpty(txtStudentRoll.Text) && Convert.ToInt32(ucProgram.selectedValue) > 0)
                {
                    int programId = Convert.ToInt32(ucProgram.selectedValue);
                    int sessionId = Convert.ToInt32(ucSession.selectedValue);
                    int batchId = Convert.ToInt32(ucBatch.selectedValue);
                    int studentId = 0;

                    paymentPostingList = BillHistoryManager.GetBillForPaymentPosting(programId, sessionId, batchId, studentId);
                }
                else if (!string.IsNullOrEmpty(txtStudentRoll.Text))
                {
                    int programId = 0;
                    int sessionId = 0;
                    int batchId = 0;
                    int studentId = 0;

                    string studentRoll = Convert.ToString(txtStudentRoll.Text);
                    Student studentObj = StudentManager.GetByRoll(studentRoll);
                    if (studentObj != null)
                    {
                        studentId = studentObj.StudentID;
                        paymentPostingList = BillHistoryManager.GetBillForPaymentPosting(programId, sessionId, batchId, studentId);
                    }
                }
                else
                {
                    lblMsg.Text = "Please select a program for payment posting.";
                    //int programId = 0;
                    //int sessionId = 0;
                    //int batchId = 0;
                    //int studentId = 0;
                    //paymentPostingList = BillHistoryManager.GetBillForPaymentPostingByStudentId(studentId);
                }
                
                if (paymentPostingList != null && paymentPostingList.Count > 0)
                {
                    GvBillPosting.DataSource = paymentPostingList;
                    GvBillPosting.DataBind();
                }
                else 
                {
                    lblMsg.Text = "No bill found for payment.";
                    GvBillPosting.DataSource = null;
                    GvBillPosting.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void chkAllStudent_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkHeader = (CheckBox)GvBillPosting.HeaderRow.FindControl("chkAllStudentHeader");
                if (chkHeader.Checked)
                {
                    for (int i = 0; i < GvBillPosting.Rows.Count; i++)
                    {
                        GridViewRow row = GvBillPosting.Rows[i];
                        Label studentId = (Label)row.FindControl("lblStudentId");
                        CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBox");
                        studentCheckd.Checked = true;
                    }
                }
                if (!chkHeader.Checked)
                {
                    for (int i = 0; i < GvBillPosting.Rows.Count; i++)
                    {
                        GridViewRow row = GvBillPosting.Rows[i];
                        Label studentId = (Label)row.FindControl("lblStudentId");
                        CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBox");
                        studentCheckd.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnPostPayment_Click(object sender, EventArgs e)
        {
            PostPayment();
        }

        private void PostPayment()
        {
            try
            {
                for (int i = 0; i < GvBillPosting.Rows.Count; i++)
                {
                    GridViewRow row = GvBillPosting.Rows[i];
                    Label lblStudentId = (Label)row.FindControl("lblStudentId");
                    Label lblBillHistoryMasterId = (Label)row.FindControl("lblBillHistoryMasterId");
                    TextBox txtBankSerialNo = (TextBox)row.FindControl("txtBankSerialNo");
                    CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBox");
                    if (studentCheckd.Checked == true)
                    {
                        BillHistoryMaster billHistoryMasterObj = BillHistoryMasterManager.GetById(Convert.ToInt32(lblBillHistoryMasterId.Text));
                        if (billHistoryMasterObj != null)
                        {
                            List<BillHistory> billHistoryList = BillHistoryManager.GetByBillHistoryMasterId(billHistoryMasterObj.BillHistoryMasterId);

                            for (int j = 0; j < billHistoryList.Count; j++)
                            {
                                CollectionHistory collectionHistoryObj = new CollectionHistory();
                                collectionHistoryObj.StudentId = billHistoryList[j].StudentId;
                                collectionHistoryObj.Amount = billHistoryList[j].Fees;
                                //collectionHistoryObj.PaymentType = Convert.ToString((CommonUtility.CommonEnum.PaymentType)Enum.Parse(typeof(CommonUtility.CommonEnum.PaymentType), "Cash"));
                                collectionHistoryObj.TypeDefinitionId = billHistoryList[j].FeeTypeId;
                                collectionHistoryObj.BillHistoryId = Convert.ToInt32(billHistoryList[j].BillHistoryId);
                                collectionHistoryObj.Comments = billHistoryList[j].Remark;
                                collectionHistoryObj.MoneyReciptSerialNo = txtBankSerialNo.Text;
                                collectionHistoryObj.BillHistoryMasterId = billHistoryMasterObj.BillHistoryMasterId;
                                collectionHistoryObj.CollectionDate = DateTime.Now;
                                collectionHistoryObj.CreatedBy = userObj.Id;
                                collectionHistoryObj.CreatedDate = DateTime.Now;
                                collectionHistoryObj.ModifiedBy = userObj.Id;
                                collectionHistoryObj.ModifiedDate = DateTime.Now;

                                int result = CollectionHistoryManager.Insert(collectionHistoryObj);
                                if (result > 0)
                                {
                                    lblMsg.Text = "Payment inserted successfully.";
                                    //
                                }
                                else
                                {
                                    lblMsg.Text = "Payment could not inserted successfully.";
                                }
                            }
                        }
                    }
                }
                LoadPayment();
            }
            catch (Exception ex) 
            {
                lblMsg.Text = ex.Message;
            }
        }
    }
}