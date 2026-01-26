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
using Common;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace EMS.BasicSetup
{
    public partial class ExamBreakup : BasePage
    {
        #region Variables
        private string[] _dataKeyXmBrkUp = new string[1] { "Id" };
        private string[] _dataKeyXmTypNm = new string[1] { "Id" };

        private string SESSION_EXAMMARKSALLOCATIONS = "ExamMarksAllocations";
        private string SESSION_EXAMMARKSALLOCATION = "ExamMarksAllocation";
        private string SESSION_EXAMTYPENAMES = "ExamTypeNames";
        private string SESSION_EXAMTYPENAME = "ExamTypeName";
        private string SESSION_EXAMMARKSALLOCATIONSINDEX = "ExamMarksAllocationsIndex";

        List<ExamMarksAllocationEntity> _exmMarkAllocations;
        ExamMarksAllocationEntity _exmMarkAllocation;
        ExamTypeNameEntity _examTypeName;
        List<ExamTypeNameEntity> _examTypeNames;
        ExamBreakupBAO objExamBreakup;
        ExamTypeNameBAO objExamTypeName;
        TypeDefinitionBAO objTypeDef;
        ExamMarksAllocationBAO objXmMrkAllo; 
        #endregion

        #region Functions
        private void ButtonEditDelete(bool enable)
        {
            btnDelete.Enabled = enable;
            btnEdit.Enabled = enable;
        }

        private void PnlControl(bool enable)
        {
            pnlControl.Enabled = enable;
        }

        private void PnlMaster(bool enable)
        {
            pnlMaster.Enabled = enable;
        }

        private void PnlDetails(bool enable)
        {
            pnlDetails.Enabled = enable;
        }

        private void ClearControls()
        {
            //lblMsg.Text = "";
            ddlTypeDef.Items.Clear();
            txtExamTypeName.Text = "";
            txtTotalMarks.Text = "";
            chkDefault.Checked = true;
            txtExamBreakup.Text = "";
            txtAllottedMarks.Text = "";

            gdvExamBreakup.DataSource = null;
            gdvExamBreakup.DataBind();
        }

        private void CleareMsg()
        {
            lblMsg.Text = "";
        }

        private void LoadDdlTypeDef()
        {
            objTypeDef = new TypeDefinitionBAO();
            List<TypeDefinitionEntity> typedefs = null; //new List<TypeDefinitionEntity>();

            typedefs = objTypeDef.GetTypeDefs("Course");

            ddlTypeDef.Items.Clear();
            if (typedefs != null)
            {
                foreach (TypeDefinitionEntity td in typedefs)
                {
                    ListItem item = new ListItem();
                    item.Value = td.Id.ToString();
                    item.Text = td.Definition;
                    ddlTypeDef.Items.Add(item);
                }
                ddlTypeDef.SelectedIndex = -1;
            }
        }

        private void RemoveAllSession()
        {
            Session.Remove(SESSION_EXAMMARKSALLOCATIONS);
            Session.Remove(SESSION_EXAMTYPENAME);
            Session.Remove(SESSION_EXAMMARKSALLOCATION);
            Session.Remove(SESSION_EXAMTYPENAMES);
            Session.Remove(SESSION_EXAMMARKSALLOCATIONSINDEX);            
        }

        private void FillGvwExamTypeName()
        {
            objExamTypeName = new ExamTypeNameBAO();
            _examTypeName = new ExamTypeNameEntity();
            
            _examTypeNames = objExamTypeName.GetAllExamTypeName(txtSrch.Text.Trim());

            gdvExamTypeName.DataSource = null;
            gdvExamTypeName.DataSource = _examTypeNames;
            gdvExamTypeName.DataKeyNames = _dataKeyXmTypNm;
            gdvExamTypeName.DataBind();
        }

        private void RefreshObject()
        {
            if (Session[SESSION_EXAMTYPENAME] == null)
            {
                _examTypeName = new ExamTypeNameEntity();
            }
            else
            {
                _examTypeName = (ExamTypeNameEntity)Session[SESSION_EXAMTYPENAME];
            }
            _examTypeName.TypeDefinitionID = Convert.ToInt32(ddlTypeDef.SelectedItem.Value);
            _examTypeName.Name = txtExamTypeName.Text.Trim();
            _examTypeName.TotalAllottedMarks = Convert.ToInt32(txtTotalMarks.Text.Trim());
            _examTypeName.Default = chkDefault.Checked;

            if (Session[SESSION_EXAMMARKSALLOCATIONS] != null)
            {
                _exmMarkAllocations = new List<ExamMarksAllocationEntity>();
                _exmMarkAllocations = (List<ExamMarksAllocationEntity>)Session[SESSION_EXAMMARKSALLOCATIONS];
            } 
            
        }

        private bool CheckMarksAllocation(ExamTypeNameEntity _examTypeName, List<ExamMarksAllocationEntity> _exmMarkAllocations)
        {
            int marks = 0;

            if (_exmMarkAllocations == null)
            { return false; }

            foreach (ExamMarksAllocationEntity item in _exmMarkAllocations)
            {
                marks += item.AllottedMarks;
            }

            if (marks == _examTypeName.TotalAllottedMarks)
            { return true; }
            else 
                return false;
        }

        private void FillGdvExamBreakup(List<ExamMarksAllocationEntity> _exmMarkAllos)
        {
            if (Session[SESSION_EXAMMARKSALLOCATIONS] != null)
            {
                Session.Remove(SESSION_EXAMMARKSALLOCATIONS);
            }
            Session[SESSION_EXAMMARKSALLOCATIONS] = _exmMarkAllos;

            gdvExamBreakup.DataSource = null;
            gdvExamBreakup.DataSource = _exmMarkAllos;
            gdvExamBreakup.DataKeyNames = _dataKeyXmBrkUp;
            gdvExamBreakup.DataBind();
        }

        private void FillExamBrkupMaster(ExamTypeNameEntity examTypeNameEntity)
        {
            if (Session[SESSION_EXAMTYPENAME] != null)
            {
                Session.Remove(SESSION_EXAMTYPENAME);
            }
            Session[SESSION_EXAMTYPENAME] = examTypeNameEntity;

            ddlTypeDef.SelectedValue = examTypeNameEntity.TypeDefinitionID.ToString();
            txtExamTypeName.Text = examTypeNameEntity.Name.ToString();
            txtTotalMarks.Text = examTypeNameEntity.TotalAllottedMarks.ToString();
            chkDefault.Checked = examTypeNameEntity.Default;
        }
        #endregion

        #region Events
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
                #region tmp
                //UIUMSUser.CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
                //if (UIUMSUser.CurrentUser != null)
                //{
                //    if (UIUMSUser.CurrentUser.RoleID > 0)
                //    {
                //        Authenticate(UIUMSUser.CurrentUser);
                //    }
                //}
                //else
                //{
                //    Response.Redirect("~/Security/Login.aspx");
                //} 
                #endregion

                base.CheckPage_Load();

                if (!IsPostBack)
                {
                    ButtonEditDelete(false);
                    PnlMaster(false);
                    PnlDetails(false);
                    CleareMsg();
                    RemoveAllSession();                    
                }
                btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete the selected element?');");                                
            }
            catch (Exception Ex)
            {
                lblMsg.Text = Ex.Message;
            }
        }
    
        protected void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                FillGvwExamTypeName();
                ButtonEditDelete(true);
                CleareMsg();
            }
            catch (Exception Ex)
            {
                lblMsg.Text = Ex.Message;
            }
        }        

        protected void brnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                CleareMsg();
                RemoveAllSession();
                PnlMaster(true);
                PnlDetails(true);
                PnlControl(false);
                LoadDdlTypeDef();                
            }
            catch (Exception Ex)
            {
                lblMsg.Text = Ex.Message;
            }
            
        }       

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                PnlMaster(true);
                PnlDetails(true);
                PnlControl(false);
                CleareMsg();

                objExamTypeName = new ExamTypeNameBAO();
                objXmMrkAllo = new ExamMarksAllocationBAO();
                _examTypeName = new ExamTypeNameEntity();
                _exmMarkAllocations = new List<ExamMarksAllocationEntity>();

                if (gdvExamTypeName.SelectedRow == null)
                {
                    lblMsg.Text = "Before trying to edit an item, you must select the desired Item.";
                    return;
                }

                LoadDdlTypeDef();

                //DataKey dtkey = gdvExamTypeName.SelectedPersistedDataKey;
                DataKey dtkey = gdvExamTypeName.SelectedDataKey;//@Sajib

                FillExamBrkupMaster(objExamTypeName.GetExamTypeName(Convert.ToInt32(dtkey.Value)));

                FillGdvExamBreakup(objXmMrkAllo.GetExamMarksAllocation(Convert.ToInt32(dtkey.Value)));
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            bool b=false;
            try
            { 
                objExamTypeName = new ExamTypeNameBAO();

                if (gdvExamTypeName.SelectedRow == null)
                {
                    lblMsg.Text = "Before trying to edit an item, you must select the desired Item.";
                    return;
                }

                DataKey dtkey = gdvExamTypeName.SelectedPersistedDataKey;
                b = objExamTypeName.Delete(Convert.ToInt32(dtkey.Value)); 
            }
            catch (Exception Ex)
            {
                lblMsg.Text = Ex.Message;
            }

            if (b)
            {
                lblMsg.Text = "Data delete successfully.";
            }
            else
            { 
                lblMsg.Text = "Data can't be delet successfully."; 
            }
            FillGvwExamTypeName();
        }

        protected void ddlTypeDef_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAddBreakup_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session[SESSION_EXAMMARKSALLOCATIONS] != null)
                {
                    _exmMarkAllocations = (List<ExamMarksAllocationEntity>)Session[SESSION_EXAMMARKSALLOCATIONS];
                }
                else
                {
                    _exmMarkAllocations = new List<ExamMarksAllocationEntity>();
                }

                _exmMarkAllocation = new ExamMarksAllocationEntity();

                _exmMarkAllocation.ExamName = txtExamBreakup.Text;
                _exmMarkAllocation.AllottedMarks = Convert.ToInt32(txtAllottedMarks.Text);

                _exmMarkAllocations.Add(_exmMarkAllocation);

                if (Session[SESSION_EXAMMARKSALLOCATIONS] != null)
                {
                    Session.Remove(SESSION_EXAMMARKSALLOCATIONS);
                }
                Session[SESSION_EXAMMARKSALLOCATIONS] = _exmMarkAllocations;

                FillGdvExamBreakup(_exmMarkAllocations);
            }
            catch (Exception Ex)
            {
                lblMsg.Text = Ex.Message;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool b = false;
            try
            {
                objExamBreakup = new ExamBreakupBAO();

                RefreshObject();

                if (!CheckMarksAllocation(_examTypeName, _exmMarkAllocations))
                {
                    CustomException ce = new CustomException("Total marks and sum of allotted marks must be same."); 
                    throw  ce;
                }

                if (_examTypeName.Id == 0)
                {
                    _examTypeName.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                    _examTypeName.CreatedDate = DateTime.Now;
                    
                    b = objExamBreakup.Save(_examTypeName, _exmMarkAllocations);
                    lblMsg.Text = "Data save successfully";
                }
                else
                {
                    _examTypeName.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                    _examTypeName.CreatedDate = DateTime.Now;

                    _examTypeName.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                    _examTypeName.ModifiedDate = DateTime.Now;

                    b = objExamBreakup.Update(_examTypeName, _exmMarkAllocations);
                    lblMsg.Text = "Data update successfully";
                }


                if (b)
                {
                    PnlMaster(false);
                    PnlDetails(false);
                    PnlControl(true);
                    ClearControls();
                    ButtonEditDelete(true);
                }

                if (b == false && _examTypeName.Id == 0)
                {
                    lblMsg.Text = "Data can't be save successfully";
                }
                else if (b == false && _examTypeName.Id != 0)
                {
                    lblMsg.Text = "Data can't be update successfully";
                }
            }
            
            catch(CustomException Ce)
            {
                lblMsg.Text = Ce.Message;
            }
            catch (Exception Ex)
            {
                lblMsg.Text = Ex.Message;
            }


            FillGvwExamTypeName();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ButtonEditDelete(true);
                PnlMaster(false);
                PnlDetails(false);
                PnlControl(true);
                ClearControls();
                CleareMsg();
                RemoveAllSession();
                
            }
            catch (Exception Ex)
            {
                lblMsg.Text = Ex.Message;
            }
        }

        protected void gdvExamBreakup_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (Session[SESSION_EXAMMARKSALLOCATIONS] != null)
                {
                    _exmMarkAllocations = (List<ExamMarksAllocationEntity>)Session[SESSION_EXAMMARKSALLOCATIONS];

                    _exmMarkAllocations.RemoveAt(e.RowIndex);


                    if (Session[SESSION_EXAMMARKSALLOCATION] != null)
                    {
                        Session.Remove(SESSION_EXAMMARKSALLOCATION);
                    }
                    if (Session[SESSION_EXAMMARKSALLOCATIONSINDEX] != null)
                    {
                        Session.Remove(SESSION_EXAMMARKSALLOCATIONSINDEX);
                    }

                    if (Session[SESSION_EXAMMARKSALLOCATIONS] != null)
                    {
                        Session.Remove(SESSION_EXAMMARKSALLOCATIONS);
                    }
                    Session[SESSION_EXAMMARKSALLOCATIONS] = _exmMarkAllocations;

                    FillGdvExamBreakup(_exmMarkAllocations);
                }
            }
            catch (Exception Ex)
            {
                lblMsg.Text = Ex.Message;
            }
        }

        protected void gdvExamBreakup_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                if (Session[SESSION_EXAMMARKSALLOCATIONS] != null)
                {
                    if (Session[SESSION_EXAMMARKSALLOCATION] != null)
                    {
                        Session.Remove(SESSION_EXAMMARKSALLOCATION);
                    }
                    if (Session[SESSION_EXAMMARKSALLOCATIONSINDEX] != null)
                    {
                        Session.Remove(SESSION_EXAMMARKSALLOCATIONSINDEX);
                    }
                    gdvExamBreakup.EditIndex = e.NewEditIndex;

                    _exmMarkAllocations = (List<ExamMarksAllocationEntity>)Session[SESSION_EXAMMARKSALLOCATIONS];
                    Session[SESSION_EXAMMARKSALLOCATION] = _exmMarkAllocations[e.NewEditIndex];
                    Session[SESSION_EXAMMARKSALLOCATIONSINDEX] = e.NewEditIndex;
                    FillGdvExamBreakup(_exmMarkAllocations);
                }
            }
            catch (Exception Ex)
            {
                lblMsg.Text = Ex.Message;
            }
        }

        protected void gdvExamBreakup_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                if (e.Exception == null)
                {
                    lblMsg.Text = "Detail successfully edited.";
                }
            }
            catch (Exception Ex)
            {
                lblMsg.Text = Ex.Message;
            }
        }

        protected void gdvExamBreakup_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                if (Session[SESSION_EXAMMARKSALLOCATIONS] != null && Session[SESSION_EXAMMARKSALLOCATION] != null)
                {
                    _exmMarkAllocations = (List<ExamMarksAllocationEntity>)Session[SESSION_EXAMMARKSALLOCATIONS];
                    int selectedIndex = (int)Session[SESSION_EXAMMARKSALLOCATIONSINDEX];
                    _exmMarkAllocations.RemoveAt(selectedIndex);

                    GridViewRow row = gdvExamBreakup.Rows[e.RowIndex];

                    _exmMarkAllocation = new ExamMarksAllocationEntity();
                    _exmMarkAllocation.ExamName = ((TextBox)(row.Cells[2].Controls[0])).Text;
                    _exmMarkAllocation.AllottedMarks = Convert.ToInt32(((TextBox)(row.Cells[3].Controls[0])).Text);

                    _exmMarkAllocations.Add(_exmMarkAllocation);

                    if (Session[SESSION_EXAMMARKSALLOCATION] != null)
                    {
                        Session.Remove(SESSION_EXAMMARKSALLOCATION);
                    }
                    if (Session[SESSION_EXAMMARKSALLOCATIONSINDEX] != null)
                    {
                        Session.Remove(SESSION_EXAMMARKSALLOCATIONSINDEX);
                    }

                    if (Session[SESSION_EXAMMARKSALLOCATIONS] != null)
                    {
                        Session.Remove(SESSION_EXAMMARKSALLOCATIONS);
                    }
                    Session[SESSION_EXAMMARKSALLOCATIONS] = _exmMarkAllocations;

                    gdvExamBreakup.EditIndex = -1;
                    FillGdvExamBreakup(_exmMarkAllocations);
                }
            }
            catch (Exception Ex)
            {
                lblMsg.Text = Ex.Message;
            }
        }

        protected void gdvExamBreakup_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                if (Session[SESSION_EXAMMARKSALLOCATIONS] != null)
                {
                    if (Session[SESSION_EXAMMARKSALLOCATION] != null)
                    {
                        Session.Remove(SESSION_EXAMMARKSALLOCATION);
                    }
                    if (Session[SESSION_EXAMMARKSALLOCATIONSINDEX] != null)
                    {
                        Session.Remove(SESSION_EXAMMARKSALLOCATIONSINDEX);
                    }

                    gdvExamBreakup.EditIndex = -1;

                    _exmMarkAllocations = (List<ExamMarksAllocationEntity>)Session[SESSION_EXAMMARKSALLOCATIONS];
                    FillGdvExamBreakup(_exmMarkAllocations);
                }
            }
            catch (Exception Ex)
            {
                lblMsg.Text = Ex.Message;
            }
        }
        #endregion        
    }
}
