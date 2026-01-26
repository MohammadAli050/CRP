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

namespace EMS.BasicSetup
{
    public partial class GradeDetails : BasePage
    {
        #region GLOBAL VARIABLE
        private string[] _dataKey = new string[1] { "Id" };

        private const string SESSION_GRADEDETAILS = "GradeDetails";
        private const string SESSION_GRADEDETAIL = "GradeDetail";
        private const string SESSION_FLAG = "flagAddUpdate";
        private const string SESSION_ACACALID = "AcaCalId";
        private const string SESSION_PROGRAMID = "ProgramId";

        List<GradeDetailsEntity> _gradeDetailsEntities = null;
        GradeDetailsEntity _gradeDetailsEntity = null;
        private bool _flagAddUpdate = false;
        #endregion

        #region EVENTS
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

                if (!IsPostBack)
                {
                    Initialize();
                    FillAcademicCalenderCombo();
                    FillProgramCombo();
                    
                }
                //btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete the selected element?');");
            }
            catch (Exception Ex)
            {
                // lblMsg.Text = Ex.Message;
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";

                RemoveFromSession(SESSION_ACACALID);
                AddToSession(SESSION_ACACALID, Convert.ToInt32(cboAcaCalender.SelectedValue));
                RemoveFromSession(SESSION_PROGRAMID);
                AddToSession(SESSION_PROGRAMID, Convert.ToInt32(cboProgram.SelectedValue));

                _gradeDetailsEntities = new List<GradeDetailsEntity>();
                _gradeDetailsEntities = GradeDetails_BAO.Load(Convert.ToInt32(cboAcaCalender.SelectedValue), Convert.ToInt32(cboProgram.SelectedValue));

                if (Session[SESSION_GRADEDETAILS] != null)
                {
                    Session.Remove(SESSION_GRADEDETAILS);
                }
                Session[SESSION_GRADEDETAILS] = _gradeDetailsEntities;

                gvGradeDetails.DataSource = null;
                gvGradeDetails.DataSource = _gradeDetailsEntities;
                gvGradeDetails.DataKeyNames = _dataKey;
                gvGradeDetails.DataBind();

                //gvGradeDetails.Columns[7].Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Utilities.DisableControls(divInput);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";

                if (Session[SESSION_GRADEDETAILS] != null)
                {
                    _gradeDetailsEntities = (List<GradeDetailsEntity>)Session[SESSION_GRADEDETAILS];
                }
                else
                {
                    _gradeDetailsEntities = new List<GradeDetailsEntity>();
                }

                _gradeDetailsEntity = new GradeDetailsEntity();

                //_gradeDetailsEntity.Acacalid = Convert.ToInt32(cboAcaCalender.SelectedValue);
                //_gradeDetailsEntity.Programid = Convert.ToInt32(cboProgram.SelectedValue);
                _gradeDetailsEntity.Grade = txtGrade.Text.Trim();
                _gradeDetailsEntity.Gradepoint = Convert.ToDecimal(txtGradePoint.Text.Trim());
                _gradeDetailsEntity.Minmarks = Convert.ToInt32(txtMinMarks.Text.Trim());
                _gradeDetailsEntity.Maxmarks = Convert.ToInt32(txtMaxMarks.Text.Trim());
                _gradeDetailsEntity.Retakediscount = Convert.ToDecimal(txtRetakeDiscount.Text.Trim());

                _gradeDetailsEntities.Add(_gradeDetailsEntity);

                RemoveFromSession(SESSION_GRADEDETAILS);
                AddToSession(SESSION_GRADEDETAILS, _gradeDetailsEntities);
                
                FillGvGradeDetails(_gradeDetailsEntities);

                RemoveFromSession(SESSION_ACACALID);
                AddToSession(SESSION_ACACALID, Convert.ToInt32(cboAcaCalender.SelectedValue));
                RemoveFromSession(SESSION_PROGRAMID);
                AddToSession(SESSION_PROGRAMID, Convert.ToInt32(cboProgram.SelectedValue));
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Utilities.DisableControls(divSelection);
            Utilities.ClearControls(divInput);
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";
                int isExecute = 0;

                _gradeDetailsEntities = new List<GradeDetailsEntity>();
                _gradeDetailsEntities = (List<GradeDetailsEntity>)Session[SESSION_GRADEDETAILS] ;

                if(CheckDuplicate(Convert.ToInt32(cboAcaCalender.SelectedValue),Convert.ToInt32(cboProgram.SelectedValue)))
                {
                    lblMsg.Text = "Data already exist";
                    return;
                }

                //if ((bool)Session[SESSION_FLAG])
                //{
                    int creatorId = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;

                    isExecute = GradeDetails_BAO.Save(_gradeDetailsEntities, creatorId, Convert.ToInt32(cboAcaCalender.SelectedValue), Convert.ToInt32(cboProgram.SelectedValue));
                    lblMsg.Text = "Data save successfully";
                //}
            }
            catch (Exception ex)
            {
                CleareSession();
                throw ex;
            }

            CleareSession();
        }

        private bool CheckDuplicate(int acaCalId, int programId)
        {
            return GradeDetails_BAO.CheckDuplicate(acaCalId, programId);
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            try
            {
                Initialize();
                cboAcaCalender.SelectedIndex = cboAcaCalender.Items.Count - 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void gvGradeDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvGradeDetails.EditIndex = -1;
                List<GradeDetailsEntity> gradeDetailsEntity = (List<GradeDetailsEntity>)Session[SESSION_GRADEDETAILS];
                gvGradeDetails.DataSource = null;
                gvGradeDetails.DataSource = gradeDetailsEntity;
                gvGradeDetails.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void gvGradeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void gvGradeDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                _gradeDetailsEntities = (List<GradeDetailsEntity>)Session[SESSION_GRADEDETAILS];

                _gradeDetailsEntity = _gradeDetailsEntities[e.RowIndex];

                int execute = GradeDetails_BAO.Delete(_gradeDetailsEntity.Id);

                if (execute == 1)
                {
                    lblMsg.Text = "Data has been deleted.";
                    LoadGrid((int)GetFromSession(SESSION_ACACALID), (int)GetFromSession(SESSION_PROGRAMID));
                }
                else
                {
                    lblMsg.Text = "Data can not be delete.";
                }
            }
            catch (Exception ex)
            {

                lblMsg.Text = ex.Message;
            }
        }

        protected void gvGradeDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;

                gvGradeDetails.EditIndex = e.NewEditIndex;

                List<GradeDetailsEntity> gradeDetailsEntities = (List<GradeDetailsEntity>)Session[SESSION_GRADEDETAILS];

                gvGradeDetails.DataSource = null;
                gvGradeDetails.DataSource = gradeDetailsEntities;
                gvGradeDetails.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void gvGradeDetails_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                if (e.Exception == null)
                {
                    lblMsg.Text = "Successfully edited.";
                }
            }
            catch (Exception Ex)
            {
                lblMsg.Text = Ex.Message;
            }
        }

        protected void gvGradeDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int datakey = Convert.ToInt32(gvGradeDetails.DataKeys[e.RowIndex].Value);

                List<GradeDetailsEntity> entities = (List<GradeDetailsEntity>)Session[SESSION_GRADEDETAILS];
                entities.RemoveAt(e.RowIndex);
                GridViewRow row = gvGradeDetails.Rows[e.RowIndex];

                GradeDetailsEntity entity = new GradeDetailsEntity();
                entity.Id = datakey;//Convert.ToInt32(((TextBox)(row.Cells[7].Controls[0])).Text);
                entity.Grade = Convert.ToString(((TextBox)(row.Cells[2].Controls[0])).Text);
                entity.Gradepoint = Convert.ToDecimal(((TextBox)(row.Cells[3].Controls[0])).Text);
                entity.Minmarks = Convert.ToInt32(((TextBox)(row.Cells[4].Controls[0])).Text);
                entity.Maxmarks = Convert.ToInt32(((TextBox)(row.Cells[5].Controls[0])).Text);
                entity.Retakediscount = Convert.ToDecimal(((TextBox)(row.Cells[6].Controls[0])).Text);
                entity.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                entity.ModifiedDate = DateTime.Now;
                entity.Acacalid = (int)GetFromSession(SESSION_ACACALID);
                entity.Programid = (int)GetFromSession(SESSION_PROGRAMID);

                int execute = GradeDetails_BAO.Update(entity);


                entities.Add(entity);

                gvGradeDetails.EditIndex = -1;

                gvGradeDetails.DataSource = null;
                gvGradeDetails.DataSource = entities;
                gvGradeDetails.DataBind();

                RemoveFromSession(SESSION_GRADEDETAILS);
                AddToSession(SESSION_GRADEDETAILS, entities);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region METHOD
        private void Initialize()
        {
            lblMsg.Text = "";
            Utilities.ClearControls(divMaster);
            Utilities.EnableControls(divInput);
            Utilities.EnableControls(divSelection);
            CleareSession();
        }
        private void CleareSession()
        {
            Session.Remove(SESSION_GRADEDETAILS);
            Session.Remove(SESSION_GRADEDETAIL);
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
        private void LoadGrid(int acaCalId, int programId)
        {
            try
            {
                _gradeDetailsEntities = new List<GradeDetailsEntity>();
                _gradeDetailsEntities = GradeDetails_BAO.Load(acaCalId, programId);

                if (Session[SESSION_GRADEDETAILS] != null)
                {
                    Session.Remove(SESSION_GRADEDETAILS);
                }
                Session[SESSION_GRADEDETAILS] = _gradeDetailsEntities;

                gvGradeDetails.DataSource = null;
                gvGradeDetails.DataSource = _gradeDetailsEntities;
                gvGradeDetails.DataKeyNames = _dataKey;
                gvGradeDetails.DataBind();
                //gvGradeDetails.Columns[7]. = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void FillGvGradeDetails(List<GradeDetailsEntity> gradeDetailsEntities)
        {
            if (Session[SESSION_GRADEDETAILS] != null)
            {
                Session.Remove(SESSION_GRADEDETAILS);
            }
            Session[SESSION_GRADEDETAILS] = gradeDetailsEntities;

            gvGradeDetails.DataSource = null;
            gvGradeDetails.DataSource = gradeDetailsEntities;
            //gdvExamBreakup.DataKeyNames = _dataKey;
            gvGradeDetails.DataBind();
        }
        #endregion
    }
}
