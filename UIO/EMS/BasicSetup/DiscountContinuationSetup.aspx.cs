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
    public partial class DiscountContinuationSetup : BasePage
    {
        #region
        private const string SESSIONDISCOUNT = "DISCOUNT";   
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
                base.CheckPage_Load();
                if (!IsPostBack && !IsCallback)
                {
                    Initialize();
                    FillAcademicCalenderCombo();
                    FillProgramCombo();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void Initialize()
        {
            pnlDataArea.Enabled = false;
            pnlSelectArea.Enabled = true;
            lblMsg.Text = string.Empty;
            Common.Utilities.ClearControls(pnlSelectArea);
            Common.Utilities.ClearControls(pnlDataArea);
            RemoveFromSession(SESSIONDISCOUNT);
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
        private void FillProgramCombo()
        {
            try
            {
                cboProgram.Items.Clear();
                lblMsg.Text = string.Empty;
                List<Program> _programs = Program.GetPrograms();
                if (_programs == null)
                {
                    return;
                }
                foreach (Program prog in _programs)
                {
                    ListItem item = new ListItem();
                    item.Value = prog.Id.ToString();
                    item.Text = prog.ShortName;
                    cboProgram.Items.Add(item);
                }
                cboProgram.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
            }
            finally { }
        }
        private void FillTypeDefDiscount()
        {
            try
            {
                ddlTypeDefinition.Items.Clear();
                lblMsg.Text = string.Empty;
                List<TypeDefinition> _typeDefs = TypeDefinition.GetTypes("Discount");
                if (_typeDefs == null)
                {
                    return;
                }
                foreach (TypeDefinition prog in _typeDefs)
                {
                    ListItem item = new ListItem();
                    item.Value = prog.Id.ToString();
                    item.Text = prog.Definition;
                    ddlTypeDefinition.Items.Add(item);
                }
                ddlTypeDefinition.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
            }
            finally { }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            pnlDataArea.Enabled = true;
            pnlSelectArea.Enabled = false;

            FillTypeDefDiscount();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                if (txtCGPA.Text.Trim() == string.Empty || txtMaxCredits.Text.Trim() == string.Empty || txtMinCredits.Text.Trim() == string.Empty)
                {
                    lblMsg.Text = "Please enter valid value";
                    return;
                }
                List<DiscountContinuationSetupEntity> dcses;
                if (Session[SESSIONDISCOUNT] == null)
                {
                    dcses = new List<DiscountContinuationSetupEntity>();
                }
                else
                {
                    dcses = (List<DiscountContinuationSetupEntity>)GetFromSession(SESSIONDISCOUNT);
                    var result = from res in dcses
                                 where res.Typedefinitionid == Convert.ToInt32(ddlTypeDefinition.SelectedValue)
                                 select res;
                    if (result.Count<DiscountContinuationSetupEntity>() > 0)
                    {
                        Utilities.ShowMassage(lblMsg, Color.Red, "This type is already in use. Please select another.");
                        return;
                    }
                }
                DiscountContinuationSetupEntity dcs = new DiscountContinuationSetupEntity();
                dcs.Acacalid = Convert.ToInt32(cboAcaCalender.SelectedValue);
                dcs.Programid = Convert.ToInt32(cboProgram.SelectedValue);
                dcs.Typedefinitionid = Convert.ToInt32(ddlTypeDefinition.SelectedValue);
                dcs.Mincredits = Convert.ToDecimal(txtMinCredits.Text.Trim());
                dcs.Maxcredits = Convert.ToDecimal(txtMaxCredits.Text.Trim());
                dcs.Mincgpa = Convert.ToDecimal(txtCGPA.Text.Trim());
                dcs.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;

                dcses.Add(dcs);

                gvSelect.DataSource = null;
                gvSelect.DataSource = dcses;
                gvSelect.DataBind();
                gvSelect.Columns[1].Visible = false;
                gvSelect.Columns[2].Visible = false;

                if (Session[SESSIONDISCOUNT] != null)
                {
                    RemoveFromSession(SESSIONDISCOUNT);
                }
                AddToSession(SESSIONDISCOUNT, dcses);

                txtCGPA.Text = string.Empty;
                txtMaxCredits.Text = string.Empty;
                txtMinCredits.Text = string.Empty;
                ddlTypeDefinition.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);                                       
            }
        }

        protected void gvSelect_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                DiscountContinuationSetupEntity dt = (DiscountContinuationSetupEntity)e.Row.DataItem;
                TypeDefinition td = TypeDefinition.GetTypeDef(dt.Typedefinitionid);
                if (td == null)
                {
                    return;
                }
                GridViewRow row = e.Row;
                row.Cells[3].Text = td.Definition;
                e.Row.DataItem = null;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;

                List<DiscountContinuationSetupEntity> dcses;
                if (Session[SESSIONDISCOUNT] == null)
                {
                    dcses = new List<DiscountContinuationSetupEntity>();
                }
                else
                {
                    dcses = (List<DiscountContinuationSetupEntity>)GetFromSession(SESSIONDISCOUNT);
                }
                if (DiscountContinuationSetup_BAO.Save(dcses) > 0)
                {
                    Utilities.ShowMassage(lblMsg, Color.Blue, Message.SUCCESSFULLYSAVED);
                }
                else
                {
                    Utilities.ShowMassage(lblMsg, Color.Red, "Data was not Saved");
                }
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg,Color.Red, ex.Message);
            }
        }

        protected void gvSelect_RowEditing(object sender, GridViewEditEventArgs e)
        {
            lblMsg.Text = string.Empty;

            gvSelect.EditIndex = e.NewEditIndex;

            List<DiscountContinuationSetupEntity> dcses = (List<DiscountContinuationSetupEntity>)Session[SESSIONDISCOUNT];

            gvSelect.DataSource = dcses;
            gvSelect.DataBind();
        }

        protected void gvSelect_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            List<DiscountContinuationSetupEntity> dcses = (List<DiscountContinuationSetupEntity>)Session[SESSIONDISCOUNT];
            dcses.RemoveAt(e.RowIndex);
            GridViewRow row = gvSelect.Rows[e.RowIndex];

            DiscountContinuationSetupEntity dcse = new DiscountContinuationSetupEntity();
            dcse.Mincgpa = Convert.ToDecimal(((TextBox)(row.Cells[6].Controls[0])).Text);
            dcse.Mincredits = Convert.ToDecimal(((TextBox)(row.Cells[4].Controls[0])).Text);
            dcse.Maxcredits = Convert.ToDecimal(((TextBox)(row.Cells[5].Controls[0])).Text);

            dcse.Id = Convert.ToInt32(((TextBox)(row.Cells[1].Controls[0])).Text);
            dcse.Typedefinitionid = Convert.ToInt32(((TextBox)(row.Cells[2].Controls[0])).Text);

            dcses.Add(dcse);

            gvSelect.EditIndex = -1;

            gvSelect.DataSource = null;
            gvSelect.DataSource = dcses;
            gvSelect.DataBind();

            RemoveFromSession(SESSIONDISCOUNT);
            AddToSession(SESSIONDISCOUNT, dcses);
        }

        protected void gvSelect_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSelect.EditIndex = -1;
            List<DiscountContinuationSetupEntity> dcses = (List<DiscountContinuationSetupEntity>)Session[SESSIONDISCOUNT];
            gvSelect.DataSource = null;
            gvSelect.DataSource = dcses;
            gvSelect.DataBind();
        }

        protected void gvSelect_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<DiscountContinuationSetupEntity> dcses = (List<DiscountContinuationSetupEntity>)Session[SESSIONDISCOUNT];
            dcses.RemoveAt(e.RowIndex);
            gvSelect.DataSource = null;
            gvSelect.DataSource = dcses;
            gvSelect.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Utilities.ClearControls(pnlDataArea);
            Initialize();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            gvSelect.DataSource = null;
            List<DiscountContinuationSetupEntity> dcses = DiscountContinuationSetup_BAO.Gets(cboAcaCalender.SelectedValue, cboProgram.SelectedValue);
            if (dcses == null)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, Message.NOTFOUND);
                return;
            }
            gvSelect.DataSource = dcses;
            gvSelect.DataBind();
        }      
    }
}
