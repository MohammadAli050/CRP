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


namespace EMS.BasicSetup
{
    public partial class BillableCourse : BasePage
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Page.Request.ServerVariables["http_user_agent"].ToLower().Contains("safari"))
            {
                Page.ClientTarget = "uplevel";
            }
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
                    Initialize();
                    lblMsg.Text = string.Empty;
                    FillAcademicCalenderCombo();
                    FillProgramCombo();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        private void FillProgramCombo()
        {
            cboProgram.Items.Clear();

            List<Program> programs = BussinessObject.Program.GetPrograms();

            if (programs != null)
            {
                foreach (BussinessObject.Program program in programs)
                {
                    ListItem item = new ListItem();
                    item.Value = program.Id.ToString();
                    item.Text = program.ShortName;
                    cboProgram.Items.Add(item);
                }
                cboProgram.SelectedIndex = 0;
            }
        }
        private void Initialize()
        {
            pnlDataArea.Enabled = false;
            Common.Utilities.ClearControls(pnlDataArea);
            chkIsCredit.Checked = false;
            chkIsCredit2.Checked = true;
            btnEdit.Enabled = true;
            btnView.Enabled = true;
            btnAdd.Enabled = true;
            txtID1.Text = "0";
            txtID2.Text = "0";
        }
        private void FillAcademicCalenderCombo()
        {
            try
            {
                List<AcademicCalender> _trimesterInfos = AcademicCalender.Gets();
                if (_trimesterInfos == null)
                {
                    return;
                }
                foreach (AcademicCalender ac in _trimesterInfos)
                {
                    ListItem lei = new ListItem();
                    lei.Value = ac.Id.ToString();
                    lei.Text = ac.CalenderUnitType.TypeName.ToString() + " " + ac.Year.ToString();
                    cboAcaCalender.Items.Add(lei);
                }                
                cboAcaCalender.SelectedIndex = cboAcaCalender.Items.Count - 1;
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
            }
            finally { }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (cboAcaCalender.Items.Count > 0 && cboProgram.Items.Count > 0)
            {
                Initialize();
                pnlDataArea.Enabled = true;
                btnEdit.Enabled = false;
                btnView.Enabled = false;
            }
        }
        private List<BillableCourseEntity> RefreshObject()
        {
            List<BillableCourseEntity> bills = new List<BillableCourseEntity>();
            BillableCourseEntity bl = new BillableCourseEntity();
            bl.Id = Convert.ToInt32(txtID1.Text.Trim());
            bl.AcaCalID = Convert.ToInt32(cboAcaCalender.SelectedValue);
            bl.ProgramID = Convert.ToInt32(cboProgram.SelectedValue);
            bl.BillStartFromRetakeNo = Convert.ToInt32(txtBillStartFrom.Text.Trim());
            bl.IsCreditCourse = chkIsCredit.Checked;
            if (btnAdd.Enabled)
            {
                bl.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
            }
            else
            {
                bl.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
            }

            bills.Add(bl);

            BillableCourseEntity bl2 = new BillableCourseEntity();
            bl2.Id = Convert.ToInt32(txtID2.Text.Trim());
            bl2.AcaCalID = Convert.ToInt32(cboAcaCalender.SelectedValue);
            bl2.ProgramID = Convert.ToInt32(cboProgram.SelectedValue);
            bl2.BillStartFromRetakeNo = Convert.ToInt32(txtBillStart2.Text.Trim());
            bl2.IsCreditCourse = chkIsCredit2.Checked;
            if (btnAdd.Enabled)
            {
                bl2.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
            }
            else
            {
                bl2.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
            }

            bills.Add(bl2);
            return bills;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                if (txtBillStartFrom.Text == string.Empty || txtBillStart2.Text == string.Empty)
                {
                    Utilities.ShowMassage(lblMsg, Color.Red, "Please enter value.");
                    return;
                }
                List<BillableCourseEntity> bills = RefreshObject();

                if (btnAdd.Enabled)
                {
                    int counter = BillableCourse_BAO.Save(bills);
                    if (counter > 0)
                    {
                        Utilities.ShowMassage(lblMsg, Color.Blue, Message.SUCCESSFULLYSAVED);
                    }
                    else if (counter < 0)
                    {
                        Utilities.ShowMassage(lblMsg, Color.DarkGreen, "Data is there for this academic calender and program. Click 'View'.");
                    }
                    else
                    {
                        Utilities.ShowMassage(lblMsg, Color.Red, Message.NOTSAVED);
                    }
                }
                else if (btnEdit.Enabled)
                {
                    if (BillableCourse_BAO.Update(bills) > 0)
                    {
                        Utilities.ShowMassage(lblMsg, Color.Blue, Message.SUCCESSFULLYUPDATED);
                    }
                    else
                    {
                        Utilities.ShowMassage(lblMsg, Color.Red, Message.NOTUPDATED);
                    }
                } 
                Initialize();                                             
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Initialize();
            lblMsg.Text = string.Empty;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Enabled = true && txtBillStartFrom.Text.Trim() == string.Empty && txtBillStart2.Text.Trim() == string.Empty)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "There is no data to edit.");
                return;
            }
            pnlDataArea.Enabled = true;
            btnAdd.Enabled = false;
            btnView.Enabled = false;
            btnEdit.Enabled = true;
        }


        protected void btnView_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            if (cboAcaCalender.Items.Count > 0 && cboProgram.Items.Count > 0)
            {
                List<BillableCourseEntity> bills = BillableCourse_BAO.Gets(cboAcaCalender.SelectedValue, cboProgram.SelectedValue);
                if (bills == null)
                {
                    Utilities.ShowMassage(lblMsg, Color.Red, Message.NOTFOUND);
                    return;
                }
                txtBillStartFrom.Text = bills[0].BillStartFromRetakeNo.ToString();
                txtID1.Text = bills[0].Id.ToString();
                txtBillStart2.Text = bills[1].BillStartFromRetakeNo.ToString();
                txtID2.Text = bills[1].Id.ToString();
                
            }
        }

        protected void cboAcaCalender_SelectedIndexChanged(object sender, EventArgs e)
        {
            Initialize();
            lblMsg.Text = string.Empty;
        }

        protected void cboProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            Initialize();
        }

        protected void DDLNonCreditCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }
        
    }
}
