using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using BussinessObject;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Net;

namespace EMS.miu.bill
{
    public partial class BillCollection : BasePage
    {
        UIUMSUser userObj = null;
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
            _scriptMan.AsyncPostBackTimeout = 36000;
            if (!IsPostBack)
            {
                DateTime dd = DateTime.Now;
                string date = dd.ToString("dd/MM/yyyy");
                PostingDateTextBox.Text = date;
                PostingDateTextBox.Enabled = false;
            }
            //lblMsg.Text = null;
        }

        protected void LoadStudentButton_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = null;
                string studentRoll = txtStudentRoll.Text.Trim();
                LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetByRoll(studentRoll);
                if (studentObj != null)
                {
                    txtStudentName.Text = studentObj.Name;
                    txtPhoneNo.Text = studentObj.BasicInfo.SMSContactSelf;
                    ucSession.LoadDropDownList(Convert.ToInt32(studentObj.ProgramID));
                    PersonBlockDTO person = PersonBlockManager.GetByRoll(studentObj.Roll);
                    // txtDue.Text = person!=null?person.Dues.ToString():"";
                }
                else
                {
                    lblMsg.Text = "Student not found.";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void SendSMS(string PhoneNo, string roll, string msg)
        {
            SMSBasicSetup smsSetup = SMSBasicSetupManager.Get();
            bool updated = SMSBasicSetupManager.Update(smsSetup);
            if (!string.IsNullOrEmpty(PhoneNo) && PhoneNo.Count() == 14 && PhoneNo.Contains("+") && smsSetup.RemainingSMS > 0 && smsSetup.BillCollectionStatus == true)
                SMSManager.Send(PhoneNo, roll, msg, ResultCallBack);
            else
                LogSMSManager.Insert(DateTime.Now, userObj.LogInID.ToString(),roll, "Number format or setup related error", false);
        }

        void ResultCallBack(string[] data)
        {
            if (data[2].Contains("<status>0</status>"))
            {
                LogSMSManager.Insert(DateTime.Now, userObj.LogInID.ToString(), data[0], data[1], true);
            }
            else
            {
                LogSMSManager.Insert(DateTime.Now, userObj.LogInID.ToString(), data[0], data[1], false);
            }
            SMSBasicSetup smsSetup = SMSBasicSetupManager.Get();
            smsSetup.RemainingSMS = smsSetup.RemainingSMS -1 ;
            bool updated = SMSBasicSetupManager.Update(smsSetup);
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                string studentRoll = txtStudentRoll.Text.Trim();
                LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetByRoll(studentRoll);
                int studentId = studentObj.StudentID;
                string typeDefinition = "BillCollection";
                LogicLayer.BusinessObjects.TypeDefinition typeDefinitionObj = TypeDefinitionManager.GetAll(typeDefinition).FirstOrDefault();
                decimal amount = Convert.ToDecimal(txtTotal.Text.Trim());

                if (Convert.ToInt32(ucSession.selectedValue) > 0)
                {
                    if (DateTextBox.Text != string.Empty)
                    {
                        DateTime collectionDate = DateTime.ParseExact(DateTextBox.Text.Replace("/", string.Empty), "ddMMyyyy", null);
                        if (amount > 0)
                        {

                            CollectionHistory billCollectionObj = new CollectionHistory();
                            billCollectionObj.StudentId = studentId;
                            billCollectionObj.MoneyReciptSerialNo = txtSlipNo.Text.Trim();
                            billCollectionObj.AcaCalId = Convert.ToInt32(ucSession.selectedValue);
                            billCollectionObj.Amount = amount;
                            billCollectionObj.PaymentType = Convert.ToString(rdlPaymentType.SelectedValue);
                            billCollectionObj.CollectionDate = collectionDate;
                            billCollectionObj.TypeDefinitionId = typeDefinitionObj.TypeDefinitionID;
                            billCollectionObj.CreatedBy = userObj.Id;
                            billCollectionObj.CreatedDate = DateTime.Now;
                            billCollectionObj.ModifiedBy = userObj.Id;
                            billCollectionObj.ModifiedDate = DateTime.Now;
                            if (IsDuplicateMoneyReceipt(billCollectionObj.MoneyReciptSerialNo, billCollectionObj.PaymentType))
                            {
                                int result = CollectionHistoryManager.Insert(billCollectionObj);

                                if (result > 0)
                                {
                                    #region Log Insert

                                    LogGeneralManager.Insert(
                                                                         DateTime.Now,
                                                                         "",
                                                                         ucSession.selectedText,
                                                                         userObj.LogInID,
                                                                         "",
                                                                         "",
                                                                         "Bill Collection",
                                                                         userObj.LogInID + " collected " + billCollectionObj.Amount.ToString() + " Tk from " + studentRoll + (string.IsNullOrEmpty(txtRemark.Text) ? "" : " with remarks " + txtRemark.Text) +" for semester "+ucSession.selectedText,
                                                                         userObj.LogInID + " is Load Page",
                                                                          ((int)CommonEnum.PageName.BillCollection).ToString(),
                                                                         CommonEnum.PageName.BillCollection.ToString(),
                                                                         _pageUrl,
                                                                         studentRoll);
                                    #endregion

                                    #region SMS Sending
                                    string sms = "ID-" + studentObj.Roll + ", you have paid Tk. " + amount + " on " + collectionDate.ToString("dd/M/yyyy") + ".Thanks for your payment.";
                                    SendSMS(studentObj.BasicInfo.SMSContactSelf, studentObj.Roll, sms);
                                    #endregion

                                    hdnCollectionHistoryId.Value = Convert.ToString(result);
                                    bool isUpdated = false;
                                    PersonBlockDTO person = PersonBlockManager.GetByRoll(studentObj.Roll);
                                    if (person != null && person.Dues <= 0)
                                    {
                                        if (person.IsAdmitCardBlock == true || person.IsMasterBlock == true || person.IsRegistrationBlock == true || person.IsResultBlock == true)
                                        {
                                            PersonBlock pB = PersonBlockManager.GetByPersonId(studentObj.PersonID);
                                            pB.Remarks = "Removed block for full payment clearance";
                                            pB.IsRegistrationBlock = false;
                                            pB.IsMasterBlock = false;
                                            pB.IsResultBlock = false;
                                            pB.IsAdmitCardBlock = false;
                                            pB.ModifiedBy = userObj.Id;
                                            pB.ModifiedDate = DateTime.Now;
                                            isUpdated = PersonBlockManager.Update(pB);

                                            if (isUpdated)
                                            {
                                                #region Log Insert

                                                LogGeneralManager.Insert(
                                                                                     DateTime.Now,
                                                                                     "",
                                                                                     "",
                                                                                     userObj.LogInID,
                                                                                     "",
                                                                                     "",
                                                                                     "Person Block Remove",
                                                                                     userObj.LogInID + " removed person block of " + studentRoll,
                                                                                     userObj.LogInID + " is Load Page",
                                                                                      ((int)CommonEnum.PageName.BillCollection).ToString(),
                                                                                     CommonEnum.PageName.BillCollection.ToString(),
                                                                                     _pageUrl,
                                                                                     studentRoll);
                                                #endregion
                                            }
                                        }


                                    }
                                    if (isUpdated)
                                        ShowAlertMessage("Collection inserted successfully and Person block is removed.");
                                    else
                                        ShowAlertMessage("Collection inserted successfully.");
                                    ClearAll();
                                }
                                else
                                {
                                    ShowAlertMessage("Collection could not inserted successfully.");
                                }
                            }
                            else
                            {
                                ShowAlertMessage("Money receipt no is duplicate.");
                            }
                        }
                        else
                        {
                            ShowAlertMessage("Please give positive amount.");
                        }
                    }
                    else
                    {
                        ShowAlertMessage("Please select a collection date.");
                    }
                }
                else
                {
                    ShowAlertMessage("Please select a semester.");
                }
            }
            catch (Exception ex)
            {
                ShowAlertMessage(ex.Message);
            }
        }


        private void ClearAll()
        {
            try
            {
                txtSlipNo.Text = null;
                DateTextBox.Text = null;
                txtStudentRoll.Text = null;
                txtStudentName.Text = null;
                txtTotal.Text = null;
                txtRemark.Text = null;
                txtPhoneNo.Text = null;
                // txtDue.Text = null;
                chkbox1.Checked = false;
                chkbox2.Checked = false;
                chkbox3.Checked = false;
                chkbox4.Checked = false;
                chkbox5.Checked = false;
                chkbox6.Checked = false;
                chkbox7.Checked = false;
                ucSession.LoadDropDownList();
            }
            catch (Exception ex) { }
        }

        private bool IsDuplicateMoneyReceipt(string moneyReceiptNo, string paymentType)
        {
            bool isDuplicate = CollectionHistoryManager.IsDuplicateMoneyReceipt(moneyReceiptNo, paymentType);
            return isDuplicate;
        }

        //View Report In PDF For Print
        protected void PrintButton_Click(object sender, EventArgs e)
        {
            string name = txtStudentName.Text.Trim();
            string roll = txtStudentRoll.Text.Trim();
            string user = userObj.LogInID;
            string remarks = txtRemark.Text.Trim();

            ReportParameter p1 = new ReportParameter("Name", name);
            ReportParameter p2 = new ReportParameter("Roll", roll);
            ReportParameter p3 = new ReportParameter("User", user);
            ReportParameter p4 = new ReportParameter("Remarks", remarks);

            try
            {
                int collectionHistoryId = Convert.ToInt32(hdnCollectionHistoryId.Value);
                CollectionHistory collectionHistoryObj = CollectionHistoryManager.GetById(collectionHistoryId);
                List<CollectionHistory> list = new List<CollectionHistory>();
                list.Add(collectionHistoryObj);

                StudentBillCollection.LocalReport.ReportPath = Server.MapPath("~/miu/bill/report/RptBillCollection.rdlc");
                this.StudentBillCollection.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4 });
                ReportDataSource rds = new ReportDataSource("StudentBillCollection", list);

                StudentBillCollection.LocalReport.DataSources.Clear();
                StudentBillCollection.LocalReport.DataSources.Add(rds);

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;

                byte[] bytes = StudentBillCollection.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportPDF/" + "StudentBillCollection" + ".pdf"), FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }

                string path = Server.MapPath("~/Upload/ReportPDF/" + "StudentBillCollection" + ".pdf");

                WebClient client = new WebClient();   // Open PDF File in Web Browser 

                Byte[] buffer = client.DownloadData(path);
                if (buffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                }

            }
            catch (Exception ex) { }
        }

        private void ShowAlertMessage(string msg)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ServerControlScript", "alert('" + msg + "');", true);
        }
        protected void Remarks_CheckedChanged(Object sender, EventArgs args)
        {
            CheckBox chk = sender as CheckBox;
            if (chk.Checked)
            {
                txtRemark.Text += chk.Text.ToString() + " ";
            }
            else
            {
                txtRemark.Text = txtRemark.Text.Replace(chk.Text + " ", "");
            }

        }
    }
}