using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System.Net.Mail;
using System.Net;
using CommonUtility;


namespace EMS.miu.admin
{

    public partial class SMSService : BasePage
    {
        BussinessObject.UIUMSUser BaseCurrentUserObj = null;

        private int count = 0, total = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            BaseCurrentUserObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            txtMSG.Attributes.Add("onkeyup", "CharacterCount(this);");
            
            if (!IsPostBack)
            {
                LoadDDL();
               
            }

        }

        private void LoadDDL()
        {
            ddlType.Items.Add(new ListItem("Teacher", "1"));
            ddlType.Items.Add(new ListItem("Student", "2"));
        }

        protected void chkAllStudent_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlType.SelectedValue.Equals("2"))
                {
                    CheckBox chkHeader = (CheckBox)GridViewStudent.HeaderRow.FindControl("chkAllStudentHeader");
                    if (chkHeader.Checked)
                    {
                        for (int i = 0; i < GridViewStudent.Rows.Count; i++)
                        {
                            GridViewRow row = GridViewStudent.Rows[i];
                            CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBoxStudent");
                            studentCheckd.Checked = true;
                        }
                    }
                    if (!chkHeader.Checked)
                    {
                        for (int i = 0; i < GridViewStudent.Rows.Count; i++)
                        {
                            GridViewRow row = GridViewStudent.Rows[i];
                            CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBoxStudent");
                            studentCheckd.Checked = false;
                        }
                    }
                }
                else
                {
                    CheckBox chkHeader = (CheckBox)GridViewTeacher.HeaderRow.FindControl("chkAllTeacherHeader");
                    if (chkHeader.Checked)
                    {
                        for (int i = 0; i < GridViewTeacher.Rows.Count; i++)
                        {
                            GridViewRow row = GridViewTeacher.Rows[i];
                            CheckBox teacherCheckd = (CheckBox)row.FindControl("CheckBoxTeacher");
                            teacherCheckd.Checked = true;
                        }
                    }
                    if (!chkHeader.Checked)
                    {
                        for (int i = 0; i < GridViewTeacher.Rows.Count; i++)
                        {
                            GridViewRow row = GridViewTeacher.Rows[i];
                            CheckBox teacherCheckd = (CheckBox)row.FindControl("CheckBoxTeacher");
                            teacherCheckd.Checked = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }



        protected void LoadButton_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (ddlType.SelectedValue.Equals("1"))
            {
                if (string.IsNullOrEmpty(txtTeacherCode.Text))
                {
                    List<Employee> list = EmployeeManager.GetAll();
                    GridViewTeacher.DataSource = list;
                    GridViewTeacher.DataBind();
                }
                else
                {
                    List<Employee> list = EmployeeManager.GetAllByCode(txtTeacherCode.Text);
                    GridViewTeacher.DataSource = list;
                    GridViewTeacher.DataBind();
                }

                GridViewStudent.DataSource = null;
                GridViewStudent.DataBind();

            }
            else
            {
                List<Student> list = StudentManager.GetAllByProgramIdBatchId(Convert.ToInt32(ucProgram.selectedValue), Convert.ToInt32(ucBatch.selectedValue));
                GridViewStudent.DataSource = list;
                GridViewStudent.DataBind();

                GridViewTeacher.DataSource = null;
                GridViewTeacher.DataBind();

            }
            
        }
        protected void btnSendSMS_Click(object sender, EventArgs e)
        {
            // List<string> studentPhoneNos = new List<string>();
            try
            {
                if (ddlType.SelectedValue.Equals("2"))
                {
                    for (int i = 0; i < GridViewStudent.Rows.Count; i++)
                    {
                        GridViewRow row = GridViewStudent.Rows[i];
                        CheckBox checkd = (CheckBox)row.FindControl("CheckBoxStudent");
                        if (checkd.Checked == true)
                        {
                            total++;
                        }
                    }
                    for (int i = 0; i < GridViewStudent.Rows.Count; i++)
                    {
                        GridViewRow row = GridViewStudent.Rows[i];
                        CheckBox checkd = (CheckBox)row.FindControl("CheckBoxStudent");

                        if (checkd.Checked == true)
                        {
                            Label lblPhoneNo = (Label)row.FindControl("lblStudentPhone");
                            Label lblRoll = (Label)row.FindControl("lblClassRoll");
                            if (lblPhoneNo.Text != "" && txtMSG.Text != "" && txtMSG.Text.Count()<=160)
                            {
                                SendSMS(lblPhoneNo.Text, lblRoll.Text, txtMSG.Text);
                            }
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < GridViewTeacher.Rows.Count; i++)
                    {
                        GridViewRow row = GridViewTeacher.Rows[i];
                        CheckBox checkd = (CheckBox)row.FindControl("CheckBoxTeacher");
                        if (checkd.Checked == true)
                        {
                            total++;
                        }
                    }
                    for (int i = 0; i < GridViewTeacher.Rows.Count; i++)
                    {
                        GridViewRow row = GridViewTeacher.Rows[i];
                        CheckBox checkd = (CheckBox)row.FindControl("CheckBoxTeacher");

                        if (checkd.Checked == true)
                        {
                            Label lblPhoneNo = (Label)row.FindControl("lblTeacherPhone");
                            Label lblCode = (Label)row.FindControl("lblTeacherCode");
                            if (lblPhoneNo.Text != "" && txtMSG.Text != "" && txtMSG.Text.Count() <= 160)
                            {
                                SendSMS(lblPhoneNo.Text, lblCode.Text, txtMSG.Text);
                            }
                           
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        private void SendSMS(string PhoneNo, string roll, string msg)
        {
            SMSBasicSetup smsSetup = SMSBasicSetupManager.Get();
            if(!string.IsNullOrEmpty(PhoneNo) && PhoneNo.Count()==14 && PhoneNo.Contains("+") && smsSetup.RemainingSMS >0 && smsSetup.CustomSmsStatus==true)
                SMSManager.Send(PhoneNo, roll, msg, ResultCallBack);
            else
                LogSMSManager.Insert(DateTime.Now, BaseCurrentUserObj.LogInID.ToString(), roll, "Number format or setup related error", false);
        }

        void ResultCallBack(string[] data)
        {
            if (data[2].Contains("<status>0</status>")) 
            {
                count++;
                lblMsg.Text = "Sent " + count.ToString() + " of " + total;
                LogSMSManager.Insert(DateTime.Now, BaseCurrentUserObj.LogInID.ToString(), data[0], data[1], true);
            }
            else
            {
                LogSMSManager.Insert(DateTime.Now, BaseCurrentUserObj.LogInID.ToString(), data[0], data[1], false);
            }
            if (count == total)
                lblMsg.Text = "Successfull!";
            else
                lblMsg.Text = "SMS sending unsuccessfull for some students. Pls check phone number format!";
            
            SMSBasicSetup smsSetup = SMSBasicSetupManager.Get();
            smsSetup.RemainingSMS = smsSetup.RemainingSMS - 1;
            bool updated = SMSBasicSetupManager.Update(smsSetup);
        }
        /*  void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
          {
              if (e.Result.ToString().Equals("SUBMIT_SUCCESS"))
              {
                  count++;
                  lblMsg.Text = "Sent " + count.ToString() + " of " + total;
                  if (count == total)
                      lblMsg.Text = "Successfull!";
              }
              else
                  lblMsg.Text = e.Result.ToString();

          }*/
        /* protected void btnSendEmail_Click(object sender, EventArgs e)
         {
             List<string> studentEmailAddrs = new List<string>();
             try
             {

                 for (int i = 0; i < GridViewStudent.Rows.Count; i++)
                 {
                     GridViewRow row = GridViewStudent.Rows[i];
                     CheckBox checkd = (CheckBox)row.FindControl("CheckBox");

                     if (checkd.Checked == true)
                     {
                         Label lblEmailAddr = (Label)row.FindControl("lblParentEmail");
                         if (lblEmailAddr.Text != "")
                         {
                             studentEmailAddrs.Add(lblEmailAddr.Text);
                         }
                     }

                 }

                 //Calling SMtP Client
                 SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                 smtpClient.UseDefaultCredentials = false;
                 smtpClient.Credentials = new System.Net.NetworkCredential("saimoom.uiu@gmail.com", "unitedinternatio");
                 smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                 smtpClient.EnableSsl = true;
                 smtpClient.SendCompleted += smtpClient_SendCompleted;
                 MailMessage mail = new MailMessage();
                 mail.From = new MailAddress("saimoom.uiu@gmail.com", "LakeHead School");

                 if (!txtMSG.Text.Equals("") && studentEmailAddrs.Count > 0)
                 {
                     foreach (string addrs in studentEmailAddrs)
                     {
                         mail.To.Add(new MailAddress(addrs));
                     }
                     mail.Body = txtMSG.Text;
                     mail.Subject = "Important!";
                     smtpClient.Send(mail);
                     lblMsg.Text = "Email sending successful!";
                 }
                 else
                     lblMsg.Text = "Plz enter email body text and make sure you have selected at least one student with official email address!";

             }
             catch (Exception ex)
             {
                 lblMsg.Text = ex.Message;
             }
         }*/

        void smtpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
                lblMsg.Text = e.Error.Message;
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
        {
            // ClearGrid();
        }

        private void ClearGrid()
        {
            GridViewStudent.Columns.Clear();
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue.Equals("1"))
            {
                txtTeacherCode.Visible = true;
                ucBatch.Visible = false;
                ucProgram.Visible = false;
                GridViewStudent.Visible = false;
                GridViewTeacher.Visible = true;
            }
            else if (ddlType.SelectedValue.Equals("2"))
            {
                txtTeacherCode.Visible = false;
                ucBatch.Visible = true;
                ucProgram.Visible = true;
                GridViewStudent.Visible = true;
                GridViewTeacher.Visible = false;
            }
        }
    }
}
