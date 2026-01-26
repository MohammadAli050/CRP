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
using System.Drawing;
using System.Collections.Generic;

namespace EMS
{
    public partial class SiblingSetup : BasePage
    {
        SiblingSetupEntity _siblingSetupEntity = null;
        List<SiblingSetupEntity> _siblingSetupEntities = null;
        private bool _flag_New = false;
        private string[] _dataKey = new string[1] { "Id" };


        #region SESSION
        private const string SESSION_APPLICANT1 = "Applicant1";
        private const string SESSION_APPLICANT2 = "Applicant2";
        private const string SESSION_FLAG_NEW = "Flag_New";
        private const string SESSION_GROUPID = "GroupId";
        private const string SESSION_ROLL = "Roll";
        #endregion

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Page.Request.ServerVariables["http_user_agent"].ToLower().Contains("safari"))
            {
                Page.ClientTarget = "uplevel";
            }
        }
        private void Initialize()
        {
            lblMsg.Text = "";
            Session.Remove(SESSION_APPLICANT1);
            Session.Remove(SESSION_APPLICANT2);

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.CheckPage_Load();
                if (!IsPostBack && !IsCallback)
                {
                    btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete?');");
                    Initialize();
                    Utilities.DisableControls(this.pnlInput);
                    ButtonEnableDisable(false);
                    ClearSession();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void ButtonEnableDisable(bool enable)
        {
            btnAdd.Enabled = enable;
            btnDelete.Enabled = enable;
        }

        //public static void DisableControls(Control ctrl)
        //{
        //    foreach (Control cntrl in ctrl.Controls)
        //    {
        //        if (cntrl.Controls != null)
        //        {
        //            DisableControls(cntrl);
        //        }

        //        if (cntrl is TextBox)
        //        {
        //            TextBox tb = (TextBox)cntrl;
        //            tb.Enabled=false;
        //        }
        //        if (cntrl is DropDownList)
        //        {
        //            DropDownList cbo = (DropDownList)cntrl;
        //            cbo.Enabled = false;
        //        }
        //        if (cntrl is CheckBox)
        //        {
        //            CheckBox chk = (CheckBox)cntrl;
        //            chk.Enabled = false;
        //        }
        //        if (cntrl is RadioButton)
        //        {
        //            RadioButton rad = (RadioButton)cntrl;
        //            rad.Enabled = false;
        //        }
        //        if (cntrl is Button)
        //        {
        //            Button btn = (Button)cntrl;
        //            btn.Enabled = false;
        //        }                
        //    }
        //}

        //public static void EnableControls(Control ctrl)
        //{
        //    foreach (Control cntrl in ctrl.Controls)
        //    {
        //        if (cntrl.Controls != null)
        //        {
        //            EnableControls(cntrl);
        //        }

        //        if (cntrl is TextBox)
        //        {
        //            TextBox tb = (TextBox)cntrl;
        //            tb.Enabled = true;
        //        }
        //        if (cntrl is DropDownList)
        //        {
        //            DropDownList cbo = (DropDownList)cntrl;
        //            cbo.Enabled = true;
        //        }
        //        if (cntrl is CheckBox)
        //        {
        //            CheckBox chk = (CheckBox)cntrl;
        //            chk.Enabled = true;
        //        }
        //        if (cntrl is RadioButton)
        //        {
        //            RadioButton rad = (RadioButton)cntrl;
        //            rad.Enabled = true;
        //        }
        //        if (cntrl is Button)
        //        {
        //            Button btn = (Button)cntrl;
        //            btn.Enabled = true;
        //        }
        //    }
        //}

        protected void btnShowName1_Click(object sender, EventArgs e)
        {
            Student Applicant1 = Student.GetStudentByRoll(txtRoll1Applicant.Text.Trim());

            if (Applicant1 == null)
            {
                //CleareControl();
                Utilities.ShowMassage(lblMsg, Color.Red, "No students found");
            }
            else
            {
                txtName1Applicant.Text = Applicant1.StdName;
                lblMsg.Text = "";

                if (Session[SESSION_APPLICANT1] != null)
                {
                    Session.Remove(SESSION_APPLICANT1);
                }
                Session[SESSION_APPLICANT1] = Applicant1;
            }
        }

        protected void btnShowName2_Click(object sender, EventArgs e)
        {
            Student Applicant2 = Student.GetStudentByRoll(txtRoll2Applicant.Text.Trim());

            if (Applicant2 == null)
            {
                //CleareControl();
                Utilities.ShowMassage(lblMsg, Color.Red, "No students found");
            }
            else
            {
                txtName2Applicant.Text = Applicant2.StdName;
                lblMsg.Text = "";

                if (Session[SESSION_APPLICANT2] != null)
                {
                    Session.Remove(SESSION_APPLICANT2);
                }
                Session[SESSION_APPLICANT2] = Applicant2;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int effectedRow = 0;

            try
            {
                if ((bool)Session[SESSION_FLAG_NEW])
                {
                    // Create new group, add two entity
                    _siblingSetupEntities = RefreshObjects();

                    foreach (SiblingSetupEntity item in _siblingSetupEntities)
                    {
                        if (CheckDuplicate(item.ApplicantId))
                        {
                            CustomException ce = new CustomException("Duplicate value is not allowed to save.");
                            throw ce;
                        }
                    }

                    int groupId = SiblingSetup_BAO.Save(_siblingSetupEntities);
                    effectedRow = groupId;//for message showing
                    LoadGridByGroupId(groupId);
                    Utilities.ClearControls(this.pnlInput);

                    if (effectedRow > 0)
                    {
                        //CleareControl();
                        Utilities.ShowMassage(lblMsg, Color.Blue, "Data Saved Succesfully");
                        ButtonEnableDisable(true);
                    }
                    else if (effectedRow == 0)
                    {
                        Utilities.ShowMassage(lblMsg, Color.Blue, "Data can not be Saved");
                    }
                }
                else
                {
                    //update existing group, add one entity
                    _siblingSetupEntity = RefreshObject();

                    if (CheckDuplicate(_siblingSetupEntity.ApplicantId))
                    {
                        CustomException ce = new CustomException("Duplicate value is not allowed to save.");
                        throw ce;
                    }

                    effectedRow = SiblingSetup_BAO.Save(_siblingSetupEntity);
                    LoadGridByGroupId(_siblingSetupEntity.GroupId);
                    Utilities.ClearControls(this.pnlInput);

                    if (effectedRow > 0)
                    {
                        //CleareControl();
                        Utilities.ShowMassage(lblMsg, Color.Blue, "Data Saved Succesfully");
                        ButtonEnableDisable(true);
                    }
                    else if (effectedRow == 0)
                    {
                        Utilities.ShowMassage(lblMsg, Color.Blue, "Data can not be Saved");
                    }
                }
            }
            catch (CustomException Ce)
            {
                lblMsg.Text = Ce.Message;
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "Data can not be Saved. Error : " + ex);
            }

        }

        private bool CheckDuplicate(int applicantId)
        {
            return SiblingSetup_BAO.CheckDuplicate(applicantId);
        }

        private void CleareControl()
        {
            throw new NotImplementedException();
        }

        private SiblingSetupEntity RefreshObject()
        {
            SiblingSetupEntity entity = new SiblingSetupEntity();

            entity.ApplicantId = ((Student)Session[SESSION_APPLICANT2]).Id;
            entity.GroupId = ((int)Session[SESSION_GROUPID]);

            entity.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
            entity.CreatedDate = DateTime.Now;

            return entity;
        }

        private List<SiblingSetupEntity> RefreshObjects()
        {
            List<SiblingSetupEntity> entities = new List<SiblingSetupEntity>();
            SiblingSetupEntity entity = new SiblingSetupEntity();

            entity.ApplicantId = ((Student)Session[SESSION_APPLICANT1]).Id;
            entity.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
            entity.CreatedDate = DateTime.Now;
            entities.Add(entity);

            entity = new SiblingSetupEntity();

            entity.ApplicantId = ((Student)Session[SESSION_APPLICANT2]).Id;
            entity.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
            entity.CreatedDate = DateTime.Now;
            entities.Add(entity);

            return entities;
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            Utilities.ClearControls(this.pnlInput);
            Utilities.ClearControls(this.pnlControl);
            Utilities.EnableControls(this.pnlControl);
            Utilities.DisableControls(this.pnlInput);
            ButtonEnableDisable(false);
            CleareGridView();
            ClearSession();
        }

        private void ClearSession()
        {
            Session.Remove(SESSION_APPLICANT1);
            Session.Remove(SESSION_APPLICANT2);
            Session.Remove(SESSION_FLAG_NEW);
            Session.Remove(SESSION_GROUPID);
            Session.Remove(SESSION_ROLL);
        }

        private void CleareGridView()
        {
            gvDetails.DataSource = null;
            gvDetails.DataBind();
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            try
            {
                LoadGrid();
                ButtonEnableDisable(true);
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "Error : " + ex.Message);
            }
        }

        private void LoadGrid()
        {
            //_siblingSetupEntities = new List<SiblingSetupEntity>();
            //_siblingSetupEntities = SiblingSetup_BAO.GetAllInGroupBy(txtApplicantRoll.Text.Trim());

            DataTable dt = new DataTable();

            dt = SiblingSetup_BAO.GetAllInGroupBy(txtApplicantRoll.Text.Trim());

            gvDetails.DataSource = null;
            gvDetails.DataSource = dt;
            gvDetails.DataBind();
        }

        private void LoadGridByGroupId(int groupId)
        {
            //_siblingSetupEntities = new List<SiblingSetupEntity>();

            //_siblingSetupEntities = SiblingSetup_BAO.GetAllInGroupBy(txtApplicantRoll.Text.Trim());

            DataTable dt = new DataTable();

            dt = SiblingSetup_BAO.GetAllInGroupBy(groupId);

            gvDetails.DataSource = null;
            gvDetails.DataSource = dt;
            // gvDetails.DataKeyNames = _dataKey;
            gvDetails.DataBind();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                _flag_New = true;
                if (Session[SESSION_FLAG_NEW] != null)
                {
                    Session.Remove(SESSION_FLAG_NEW);
                }
                Session[SESSION_FLAG_NEW] = _flag_New;

                Utilities.EnableControls(this.pnlInput);
                Utilities.DisableControls(this.pnlControl);
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "Error : " + ex.Message);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;

                if (gvDetails.SelectedRow == null)
                {
                    Utilities.ShowMassage(lblMsg, Color.Red, "Before Add an item, you must select the Item.");
                    return;
                }

                _flag_New = false;
                if (Session[SESSION_FLAG_NEW] != null)
                {
                    Session.Remove(SESSION_FLAG_NEW);
                }
                Session[SESSION_FLAG_NEW] = _flag_New;

                Utilities.EnableControls(this.pnlInput);
                Utilities.DisableControls(this.pnlControl);

                string gId = ((System.Web.UI.WebControls.Label)(gvDetails.Rows[gvDetails.SelectedIndex].Cells[0].FindControl("lblGroupID"))).Text;
                string roll = ((System.Web.UI.WebControls.Label)(gvDetails.Rows[gvDetails.SelectedIndex].Cells[0].FindControl("lblRoll"))).Text;
                string name = ((System.Web.UI.WebControls.Label)(gvDetails.Rows[gvDetails.SelectedIndex].Cells[0].FindControl("lblName"))).Text;

                if (Session[SESSION_GROUPID] != null)
                {
                    Session.Remove(SESSION_GROUPID);
                }
                Session[SESSION_GROUPID] = Convert.ToInt32(gId);

                txtRoll1Applicant.Text = roll;
                txtName1Applicant.Text = name;
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            int effectedRow = 0;
            try
            {
                if (gvDetails.SelectedRow == null)
                {
                    Utilities.ShowMassage(lblMsg, Color.Red, "Before deleting an item, you must select the Item.");
                    return;
                }

                string id = ((System.Web.UI.WebControls.Label)(gvDetails.Rows[gvDetails.SelectedIndex].Cells[0].FindControl("lblSiblingSetupId"))).Text;
                string gId = ((System.Web.UI.WebControls.Label)(gvDetails.Rows[gvDetails.SelectedIndex].Cells[0].FindControl("lblGroupID"))).Text;

                effectedRow = SiblingSetup_BAO.Delete(Convert.ToInt32(id));
                LoadGridByGroupId(Convert.ToInt32(gId));
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "Error : " + ex.Message);
            }

            if (effectedRow > 0)
            {
                Utilities.ShowMassage(lblMsg, Color.Green, "Successfully Deleted");
            }
            else
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "Unable to delete.");
            }
        }
    }
}
