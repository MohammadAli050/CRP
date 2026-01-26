using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BussinessObject;
using System.Collections.Generic;
using Common;
using System.Drawing;
using DevExpress.Web.ASPxEditors;

namespace EMS.Bill
{
    public partial class RegistrationBill : BasePage
    {
        private List<AcademicCalender> _trimesterInfos = null;
        private string[] _dataKey = new string[1] { "Id" };

        #region SESSION
        private const string SESSIONSTD = "STUDENT_BILL";
        #endregion

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
                    FillAcademicCalenderCombo();
                    FillProgramCombo();
                    lblMsg.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }              

        protected void btnView_Click(object sender, EventArgs e)
        {
            List<Student> stds = Student.GetRegisteredStudents(Int32.Parse(cboProgram.SelectedItem.Value.ToString()), Int32.Parse(cboAcaCalender.SelectedItem.Value.ToString()));
            if (stds == null)
            {
                return;
            }
            gvwCollection.DataSource = stds;
            gvwCollection.DataBind();
            gvwCollection.Columns[1].Visible = false;
            if (IsSessionVariableExists(SESSIONSTD))
            {
                RemoveFromSession(SESSIONSTD);
            }
            AddToSession(SESSIONSTD, stds);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsSessionVariableExists(SESSIONSTD))
                {
                    List<Student> stds = (List<Student>)GetFromSession(SESSIONSTD);
                    List<Student> AssignedStds = new List<Student>();
                    for (int i = 0; i < gvwCollection.Rows.Count; i++)
                    {
                        CheckBox chk = (CheckBox)gvwCollection.Rows[i].Cells[0].FindControl("chk");
                        if (chk.Checked)
                        {
                            //stds[i].TreeMasterID = Int32.Parse(gvwCollection.Rows[i].Cells[4].Text.Trim());
                            stds[i].ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                            stds[i].ModifiedDate = DateTime.Now;
                            AssignedStds.Add(stds[i]);
                        }
                    }

                    int effectedRows = Registration_BAO.GenerateBill(AssignedStds);
                    btnView_Click(null, null);
                    //RemoveFromSession(SESSIONSTD);

                    Utilities.ShowMassage(lblMsg, Color.Blue, Message.SUCCESSFULLYSAVED);
                }
            }
            catch (Exception ex)
            {
                Common.Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            FillAcademicCalenderCombo();
            FillProgramCombo();
            lblMsg.Text = "";
            gvwCollection.DataSource = null;
        }

        private void FillAcademicCalenderCombo()
        {
            try
            {
                cboAcaCalender.Items.Clear();

                ListEditItem currentTrimester = new ListEditItem();

                _trimesterInfos = AcademicCalender.Gets();

                if (_trimesterInfos == null)
                {
                    return;
                }

                foreach (AcademicCalender ac in _trimesterInfos)
                {
                    if (ac.IsCurrent)
                    {
                        currentTrimester.Text = ac.CalenderUnitType.TypeName.ToString() + " " + ac.Year.ToString();
                    }

                    ListEditItem lei = new ListEditItem();
                    lei.Value = ac.Id.ToString();
                    lei.Text = ac.CalenderUnitType.TypeName.ToString() + " " + ac.Year.ToString();
                    cboAcaCalender.Items.Add(lei);
                }
                //cboAcaCalender.SelectedIndex = cboAcaCalender.Items.Count - 1;
                cboAcaCalender.SelectedIndex = cboAcaCalender.Items.FindByText(currentTrimester.Text).Index;
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
            }
            finally { }
        }
        protected void cboAcaCalender_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            gvwCollection.DataSource = null;
            gvwCollection.DataBind();
        }
        private void FillProgramCombo()
        {
            try
            {
                cboProgram.Items.Clear();
                lblMsg.Text = string.Empty;
                gvwCollection.DataSource = null;
                List<Program> _programs = Program.GetPrograms();
                if (_programs == null)
                {
                    pnlCalender.Enabled = false;
                    return;
                }
                pnlCalender.Enabled = true;
                foreach (Program prog in _programs)
                {
                    ListEditItem item = new ListEditItem();
                    item.Value = prog.Id.ToString();
                    item.Text = prog.ShortName;
                    cboProgram.Items.Add(item);
                }
                cboProgram.SelectedIndex = 0;
                cboProgram_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
            }
            finally { }
        }
        protected void cboProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            gvwCollection.DataSource = null;
            gvwCollection.DataBind();
        }

        protected void chk_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ch = (CheckBox)sender;
            GridViewRow row = (GridViewRow)ch.NamingContainer;
            ch.Checked = ((CheckBox)sender).Checked;

            if (ch.Checked)
            {
                row.BackColor = System.Drawing.Color.LightSalmon;
            }
            else
            {
                row.BackColor = System.Drawing.Color.Empty;
            }
        }

        protected void chkAll_CheckedChanged1(object sender, EventArgs e)
        {
            foreach (GridViewRow rowItem in gvwCollection.Rows)
            {
                CheckBox chk = (CheckBox)sender;
                chk = (CheckBox)(rowItem.Cells[4].FindControl("chk"));
                chk.Checked = ((CheckBox)sender).Checked;
                if (chk.Checked)
                {
                    rowItem.BackColor = System.Drawing.Color.LightSalmon;
                }
                else
                {
                    rowItem.BackColor = System.Drawing.Color.Empty;
                }
            }
        }
    }
}
