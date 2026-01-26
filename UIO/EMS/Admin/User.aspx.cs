using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Common;
using BussinessObject;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using LogicLayer.BusinessLogic;


public partial class Admin_User : BasePage
{
    #region Variable Declaration
    List<UIUMSUser> _users = null;
    UIUMSUser _user = null;
    private string[] _dataKey = new string[1] { "Id" };
    private List<RegExpressionUsingProgAcaCal> _regExpressions;
    string strAccessIDPattern = string.Empty;
    string strStudentPattern = "([0-9]+)";
    private ArrayList arrPattern = new ArrayList();
    #endregion

    #region Constants

    #region Session Names
    private const string SESSIONUSER = "USER";
    private const string SESSIONNONSPECSTD = "NonSpecStd";
    private const string SESSIONUSERPERMISSIONS = "userpermissions";
    #endregion

    #endregion

    #region Functions
    /// <summary>
    /// initializing the page with default values
    /// </summary>
    private void ClearForm()
    {
        base.RemoveFromSession(SESSIONUSER);
        base.RemoveFromSession(SESSIONNONSPECSTD);
        base.RemoveFromSession("gvSelection");
        this.txtLogIn.Text = "";
        this.txtPassword.Text = "";
        this.ddlUsrSrc.SelectedIndex = 0;
        this.txtFindStd.Text = "";
        this.chkIsActiveUser.Checked = false;
        this.ddlProg.SelectedIndex = 0;
        this.ddlBatch.SelectedIndex = 0;
        clrAccessEndDate.Date = DateTime.Today;
        clrAccessStartDate.Date = DateTime.Today;
        clrRoleEndDate.Date = DateTime.Today;
        clrRoleStartDate.Date = DateTime.Today;
        gvSelectedProgram.DataSource = null;
        gvSelectedProgram.DataBind();
        gvSelection.DataSource = null;
        gvSelection.DataBind();
        this.txtLogIn.Focus();
        chkIsActiveUser.Checked = false;
        chkIsNonSpecStd.Checked = false;
        chkIsNonSpecStd_CheckedChanged(null, null);
        chkIsSpecStd.Checked = false;
        chkIsSpecStd_CheckedChanged(null, null);
    }

    /// <summary>
    /// user gridview is populating with all user
    /// </summary>
    private void FillList()
    {
        if (txtSearchParam.Text.Trim().Length > 0)
        {
            BussinessObject.UIUMSUser user = null;
            user = BussinessObject.UIUMSUser.GetByLogInID(txtSearchParam.Text.Trim(), false);

            if (user != null)
            {
                _users = new List<UIUMSUser>();
                _users.Add(user);
            }
        }
        else
        {
            _users = BussinessObject.UIUMSUser.Get(false);
        }

        if (Session["Users"] != null)
        {
            Session.Remove("Users");
        }
        Session.Add("Users", _users);

        if (_users != null)
        {
            _users = _users.Where(x => x.RoleID != 9).ToList();

            if (!string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                _users = _users.Where(n => n.UserName.Contains(txtName.Text.Trim())).ToList();
            }

            if (_users != null && _users.Count > 0)
            {
                gdvUsers.DataSource = _users;
                gdvUsers.DataKeyNames = _dataKey;

                gdvUsers.DataBind();

                //DisableButtons();

                if (_users.Count <= 0)
                {
                    gdvUsers.DataSource = null;

                    gdvUsers.DataBind();
                    Utilities.ShowMassage(lblErr, Color.Red, "No records found");
                }
            }
            else
            {
                gdvUsers.DataSource = null;

                gdvUsers.DataBind();
                Utilities.ShowMassage(lblErr, Color.Red, "No records found");
            }
        }
        else
        {
            gdvUsers.DataSource = null;

            gdvUsers.DataBind();
            Utilities.ShowMassage(lblErr, Color.Red, "No records found");
        }
    }

    /// <summary>
    /// user gridview is populating with specifiq user
    /// </summary>
    /// <param name="userID"></param>
    private void FillList(int userID)
    {
        _users = new List<UIUMSUser>();
        _users.Add(BussinessObject.UIUMSUser.Get(userID, false));

        if (Session["Users"] != null)
        {
            Session.Remove("Users");
        }
        Session.Add("Users", _users);

        if (_users != null)
        {
            gdvUsers.DataSource = _users;
            gdvUsers.DataKeyNames = _dataKey;

            gdvUsers.DataBind();

            //DisableButtons();

            if (_users.Count <= 0)
            {
                gdvUsers.DataSource = null;

                gdvUsers.DataBind();
                Utilities.ShowMassage(lblErr, Color.Red, "No records found");
            }
        }
        else
        {
            gdvUsers.DataSource = null;

            gdvUsers.DataBind();
            Utilities.ShowMassage(lblErr, Color.Red, "No records found");
        }
    }

    /// <summary>
    /// enabling master detail panels
    /// </summary>
    /// <param name="enbaleMaster">boolean value</param>
    /// <param name="enableDetail">boolean value</param>
    private void MasterDetailEnabler(bool enbaleMaster, bool enableDetail)
    {
        pnlMaster.Enabled = enbaleMaster;

        pnlDetails.Enabled = enableDetail;
        clrAccessEndDate.Enabled = enableDetail;
        clrAccessStartDate.Enabled = enableDetail;
        clrRoleEndDate.Enabled = enableDetail;
        clrRoleStartDate.Enabled = enableDetail;
    }

    /// <summary>
    /// enabling controls
    /// </summary>
    /// <param name="enable">boolean value</param>
    private void Enabler(bool enable)
    {
        //pnlDetails.Enabled = enable;
        clrRoleEndDate.Enabled = enable;
        clrRoleStartDate.Enabled = enable;
        clrAccessStartDate.Enabled = enable;
        clrAccessEndDate.Enabled = enable;
        clrRoleStartDate.Date = DateTime.Today;
        clrRoleEndDate.Date = DateTime.Today;
    }
    /// <summary>
    /// getting values for the controls from the result
    /// </summary>
    private void RefreshValue()
    {

        _user = (UIUMSUser)GetFromSession(SESSIONUSER);

        if (_user == null)
        {
            _user = new UIUMSUser();
        }

        this.txtLogIn.Text = _user.LogInID.ToString();
        this.ddlUsrSrc.SelectedValue = _user.RoleID.ToString();
        this.clrRoleStartDate.Date = _user.RoleExistStartDate;
        this.clrRoleEndDate.Date = _user.RoleExistEndDate;
        this.chkIsActiveUser.Checked = _user.IsActive;

        RefreshGridWithValue();

        this.txtLogIn.Focus();
        this.txtPassword.Text = _user.Password.ToString();// Utilities.Decrypt(_user.Password.ToString());
    }

    /// <summary>
    /// getting gridview values for access permissions
    /// </summary>
    private void RefreshGridWithValue()
    {
        if (_user.UserPermissions == null)
        {
            return;
        }
        List<UserPermission> permissions = _user.UserPermissions;
        List<RegExpressionUsingProgAcaCal> regs = new List<RegExpressionUsingProgAcaCal>();
        List<StudentEntity> stdsPattern = new List<StudentEntity>();

        foreach (UserPermission p in permissions)
        {
            string strPattern = p.AccessIDPattern;
            if (!p.AccessIDPattern.Contains('('))
            {
                StudentEntity std = new StudentEntity();
                std.Roll = p.AccessIDPattern.Trim();
                std.CreatedDate = p.StartDate;
                std.ModifiedDate = p.EndDate;
                stdsPattern.Add(std);
            }
            else
            {
                RegExpressionUsingProgAcaCal re = new RegExpressionUsingProgAcaCal();
                string[] strArr;
                strPattern = strPattern.Remove(p.AccessIDPattern.Length - strStudentPattern.Length);
                if (strPattern.StartsWith(strStudentPattern))
                {
                    if (strPattern.Length == strStudentPattern.Length)
                    {
                        re.ProgId = "0";
                        re.ProgCode = "All";
                    }
                    else
                    {
                        strArr = strPattern.Split('(', ')');

                        if (strArr[5] == "[0-9]+")
                        {
                            re.ProgId = "0";
                            re.ProgCode = "All";
                        }
                        else
                        {
                            re.ProgId = strArr[5];
                            re.ProgCode = ProgramManager.GetById(Convert.ToInt32(re.ProgId)).Code;
                        }
                    }
                }
                else
                {
                    strArr = strPattern.Split('(', ')');
                    re.ProgId = strArr[1];
                    re.ProgCode = ProgramManager.GetById(Convert.ToInt32(re.ProgId)).Code;
                }

                if (strPattern.EndsWith(strStudentPattern))
                {
                    re.BatchId = "0";
                    re.BatchNo = "All";
                }
                else
                {
                    strArr = strPattern.Split('(', ')');

                    if (strArr[3] == "[0-9]+")
                    {
                        re.ProgId = "0";
                        re.ProgCode = "All";
                    }
                    else
                    {
                        re.BatchId = strArr[3];
                        re.BatchNo = BatchManager.GetById(Convert.ToInt32(re.BatchId)).BatchNO.ToString();
                    }
                }

                re.AccessValidFrom = p.StartDate;
                re.AccessValidTo = p.EndDate;

                regs.Add(re);
            }
        }
        gvSelectedProgram.DataSource = null;
        gvSelection.DataSource = null;
        if (stdsPattern.Count > 0)
        {
            gvSelection.DataSource = stdsPattern;
            chkIsSpecStd.Checked = true;
            pnlSpecStd.Visible = true;
            if (IsSessionVariableExists("gvSelection"))
            {
                RemoveFromSession("gvSelection");
            }
            AddToSession("gvSelection", stdsPattern);
        }
        if (regs.Count > 0)
        {
            gvSelectedProgram.DataSource = regs;
            chkIsNonSpecStd.Checked = true;
            pnlNonSpecStd.Visible = true;
            if (IsSessionVariableExists(SESSIONNONSPECSTD))
            {
                RemoveFromSession(SESSIONNONSPECSTD);
            }
            AddToSession(SESSIONNONSPECSTD, regs);
        }
        gvSelectedProgram.DataBind();
        gvSelection.DataBind();
        CheckStdGridViewAfterBinding();
    }

    /// <summary>
    /// reading control values
    /// </summary>
    /// <returns></returns>
    private UIUMSUser RefreshObject()
    {
        UIUMSUser user = null;
        if (!IsSessionVariableExists(SESSIONUSER))
        {
            user = new UIUMSUser();
        }
        else
        {
            user = (UIUMSUser)GetFromSession(SESSIONUSER);
        }

        user.LogInID = txtLogIn.Text.Trim();
        // user.Password = Utilities.Encrypt(txtPassword.Text.Trim());

        user.IsActive = chkIsActiveUser.Checked;
        user.RoleID = Int32.Parse(ddlUsrSrc.SelectedItem.Value);
        user.RoleExistStartDate = clrRoleStartDate.Date;
        user.RoleExistEndDate = clrRoleEndDate.Date;
        user.UserPermissions = new List<UserPermission>();

        if (chkIsNonSpecStd.Checked && Session[SESSIONNONSPECSTD] != null)
        {
            user.UserPermissions = MakeAccessIDPattern(new List<UserPermission>());
        }
        if (chkIsSpecStd.Checked)
        {
            if (user.UserPermissions.Count > 0)
            {
                user.UserPermissions = MakeStudentAccessIDPattern(user.UserPermissions);
            }
            else
            {
                user.UserPermissions = MakeStudentAccessIDPattern(new List<UserPermission>());
            }
        }

        return user;
    }

    private bool MatchwithRegularExpression(string roll)
    {
        //if (IsSessionVariableExists(SESSIONUSER))
        //{
        //    UIUMSUser user = (UIUMSUser)GetFromSession(SESSIONUSER);
        //    if (user != null && user.UserPermissions != null)
        //    {
        //        foreach (UserPermission up in user.UserPermissions)
        //        { 

        //        }
        //    }
        //}
        return false;
    }

    /// <summary>
    /// reading student specific gridview values for access permissions
    /// </summary>
    /// <param name="permissions">holds current permissions. can be null.</param>
    /// <returns></returns>
    private List<UserPermission> MakeStudentAccessIDPattern(List<UserPermission> permissions)
    {
        for (int i = 0; i < gvSelection.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)gvSelection.Rows[i].Cells[0].FindControl("chkLeft");
            if (chk.Checked)
            {
                UserPermission up = new UserPermission();
                up.AccessIDPattern = gvSelection.Rows[i].Cells[1].Text;
                up.StartDate = Convert.ToDateTime(gvSelection.Rows[i].Cells[2].Text);
                up.EndDate = Convert.ToDateTime(gvSelection.Rows[i].Cells[3].Text);

                permissions.Add(up);
            }
        }
        return permissions;
    }
    /// <summary>
    /// reading nonstudent specific gridview values for access permissions
    /// </summary>
    /// <param name="permissions">holds current permissions. can be null.</param>
    /// <returns></returns>
    private List<UserPermission> MakeAccessIDPattern(List<UserPermission> permissions)
    {
        List<RegExpressionUsingProgAcaCal> regExpress = (List<RegExpressionUsingProgAcaCal>)GetFromSession(SESSIONNONSPECSTD);
        if (regExpress == null)
        {
            return null;
        }
        // note: sorted the list(regExpress)
        var list = from element in regExpress
                   orderby element.ProgId, element.BatchId
                   select element;

        foreach (RegExpressionUsingProgAcaCal reg in list)
        {
            UserPermission perm = new UserPermission();
            if (reg.ProgId == "0" && reg.BatchId == "0")
            {
                perm.AccessIDPattern = "([0-9]+)([0-9]+)([0-9]+)" + strStudentPattern;
                perm.StartDate = reg.AccessValidFrom;
                perm.EndDate = reg.AccessValidTo;
            }
            else if (reg.ProgId == "0" && reg.BatchId != "0")
            {
                perm.AccessIDPattern = "([0-9]+)(" + reg.BatchId + ")([0-9]+)" + strStudentPattern;
                perm.StartDate = reg.AccessValidFrom;
                perm.EndDate = reg.AccessValidTo;
            }
            else if (reg.ProgId != "0" && reg.BatchId == "0")
            {
                perm.AccessIDPattern = "([0-9]+)([0-9]+)(" + reg.ProgId + ")" + strStudentPattern;
                perm.StartDate = reg.AccessValidFrom;
                perm.EndDate = reg.AccessValidTo;
            }
            else
            {
                perm.AccessIDPattern = "([0-9]+)(" + reg.BatchId + ")(" + reg.ProgId + ")" + strStudentPattern;
                perm.StartDate = reg.AccessValidFrom;
                perm.EndDate = reg.AccessValidTo;
            }
            permissions.Add(perm);
        }
        return permissions;
    }

    /// <summary>
    /// filling drop down list with roles
    /// </summary>
    private void FillRoleCombo()
    {
        List<Role> roles = null;
        roles = Role.Get();
        if (roles == null)
        {
            return;
        }
        foreach (Role role in roles)
        {
            ListItem item = new ListItem();
            item.Value = role.Id.ToString();
            item.Text = role.RoleName;
            ddlUsrSrc.Items.Add(item);
        }
        ddlUsrSrc.SelectedIndex = 0;
    }
    /// <summary>
    /// filling drop down list with programs
    /// </summary>
    private void FillProgramCombo()
    {
        List<Program> progs = null;
        progs = Program.GetPrograms();
        if (progs == null)
        {
            return;
        }
        ListItem item = new ListItem();
        item.Value = "0";
        item.Text = "All";
        ddlProg.Items.Add(item);
        foreach (Program prog in progs)
        {
            item = new ListItem();
            item.Value = prog.Id.ToString();
            item.Text = prog.Code;
            ddlProg.Items.Add(item);
        }
        ddlProg.SelectedIndex = -1;
    }
    /// <summary>
    /// filling drop down list with AcademicCalenders
    /// </summary>
    private void FillBatch()
    {
        ddlBatch.Items.Clear();

        ListItem item = new ListItem();
        item.Value = "0";
        item.Text = "All";
        ddlBatch.Items.Add(item);

        List<LogicLayer.BusinessObjects.Batch> batchList = null;
        batchList = BatchManager.GetAll();
        if (batchList == null)
        {
            return;
        }
        if (ddlProg.SelectedValue != "0")
        {
            batchList = batchList.Where(b => b.ProgramId == Convert.ToInt32(ddlProg.SelectedValue)).ToList();
        }

        foreach (LogicLayer.BusinessObjects.Batch batch in batchList)
        {
            item = new ListItem();
            item.Value = batch.BatchId.ToString();
            item.Text = batch.BatchNO.ToString().PadLeft(2, '0');
            ddlBatch.Items.Add(item);
        }
        ddlBatch.SelectedIndex = -1;
    }
    private RegExpressionUsingProgAcaCal GetRegExpress()
    {
        RegExpressionUsingProgAcaCal regExpress = new RegExpressionUsingProgAcaCal();
        regExpress.BatchId = ddlBatch.SelectedValue;
        regExpress.BatchNo = ddlBatch.SelectedItem.Text;
        regExpress.ProgId = ddlProg.SelectedValue;
        regExpress.ProgCode = ddlProg.SelectedItem.Text;
        regExpress.AccessValidFrom = clrAccessStartDate.Date;
        regExpress.AccessValidTo = clrAccessEndDate.Date;
        return regExpress;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private bool Validation()
    {
        bool boolReturn = true;
        int progCode = 32000;
        if (ddlProg.SelectedValue == "0")
        {
            progCode = 0;
        }
        int acaCalCode = 32000;
        if (ddlBatch.SelectedValue == "0")
        {
            acaCalCode = 0;
        }
        foreach (RegExpressionUsingProgAcaCal reg in _regExpressions)
        {
            if ((reg.ProgId == ddlProg.SelectedValue && Int32.Parse(reg.BatchId) > acaCalCode)
                || (Int32.Parse(reg.ProgId) > progCode && reg.BatchId == ddlBatch.SelectedValue))
            {
                boolReturn = false;
                break;
            }
        }
        return boolReturn;
    }
    /// <summary>
    /// checking check box to true in the gridview by the results
    /// </summary>
    private void CheckStdGridViewAfterBinding()
    {
        if (IsSessionVariableExists("gvSelection"))
        {
            List<StudentEntity> stds = (List<StudentEntity>)GetFromSession("gvSelection");
            if (stds != null)
            {
                foreach (StudentEntity std in stds)
                {
                    for (int i = 0; i < gvSelection.Rows.Count; i++)
                    {
                        if (gvSelection.Rows[i].Cells[1].Text.Trim() == std.Roll.Trim())
                        {
                            CheckBox chk = (CheckBox)gvSelection.Rows[i].Cells[0].FindControl("chkLeft");
                            chk.Checked = true;
                            gvSelection.Rows[i].Cells[2].Text = std.CreatedDate.ToString();
                            gvSelection.Rows[i].Cells[3].Text = std.ModifiedDate.ToString();
                            break;
                        }
                    }
                }
            }
        }
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

    protected void Page_PreRenderComplete(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Enabler(false);
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            base.CheckPage_Load();
            txtPassword.ReadOnly = false;

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
                FillRoleCombo();
                FillProgramCombo();

                FillBatchComboForAll();

                MasterDetailEnabler(true, false);

                pnlNonSpecStd.Visible = false;
                pnlSpecStd.Visible = false;

                clrRoleStartDate.Date = DateTime.Today;
                clrRoleEndDate.Date = DateTime.Today;
                clrAccessEndDate.Date = DateTime.Today;
                clrAccessStartDate.Date = DateTime.Today;
            }
            btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete?');");
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblErr, Color.Red, Ex.Message);
        }
    }

    private void FillBatchComboForAll()
    {
        ddlBatch.Items.Clear();

        ListItem item = new ListItem();
        item.Value = "0";
        item.Text = "All";
        ddlBatch.Items.Add(item);
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        try
        {
            FillList();
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblErr, Color.Red, Ex.Message);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            txtPassword.ReadOnly = false;

            if (IsSessionVariableExists(SESSIONUSER))
            {
                RemoveFromSession(SESSIONUSER);
            }
            RemoveFromSession(SESSIONNONSPECSTD);

            MasterDetailEnabler(false, true);
            Enabler(true);
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblErr, Color.Red, Ex.Message);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            if (gdvUsers.SelectedRow == null)
            {
                Utilities.ShowMassage(lblErr, Color.Red, "Before trying to edit an item, you must select the desired Item.");
                return;
            }
            _user = new UIUMSUser();

            DataKey dtkey = gdvUsers.SelectedDataKey;

            IOrderedDictionary odict = dtkey.Values;
            _user = UIUMSUser.Get(Convert.ToInt32(odict[0]), true);



            if (IsSessionVariableExists(SESSIONUSER))
            {
                RemoveFromSession(SESSIONUSER);
            }
            AddToSession(SESSIONUSER, _user);
            MasterDetailEnabler(false, true);
            RefreshValue();

            txtPassword.Text = "******";
            txtPassword.ReadOnly = true;
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblErr, Color.Red, Ex.Message);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (gdvUsers.SelectedRow == null)
            {
                Utilities.ShowMassage(lblErr, Color.Red, "Before trying to delete an item, you must select the desired Item.");
                return;
            }
            _user = new UIUMSUser();

            DataKey dtkey = gdvUsers.SelectedPersistedDataKey;

            //if (_user.IsSysAdmin)
            //{
            //    Utilities.ShowMassage(lblErr, Color.Red, "Sys admin cannot be deleted");
            //    return;
            //}

            IOrderedDictionary odict = dtkey.Values;
            UIUMSUser.Delete(Convert.ToInt32(odict[0]));

            Utilities.ShowMassage(lblErr, Color.Blue, "User information successfully deleted");
            FillList();
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 547)
            {
                Utilities.ShowMassage(lblErr, Color.Red, "This user has been referenced in other tables, please delete those references first.");
            }
            else
            {
                Utilities.ShowMassage(lblErr, Color.Red, SqlEx.Message);
            }
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblErr, Color.Red, Ex.Message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string oldCode = string.Empty;

            _user = null;
            _user = RefreshObject();

            bool isNewUser = true;
            if (_user.Id == 0)
            {
                _user.Password = txtPassword.Text.Trim();
                _user.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _user.CreatedDate = DateTime.Now;
            }
            else
            {
                isNewUser = false;
                // _user.Password = txtPassword.Text.Trim();
                _user.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _user.ModifiedDate = DateTime.Now;
            }

            if (UIUMSUser.HasDuplicateLogInID(_user))
            {
                throw new Exception("Duplicate logInIDs are not allowed.");
            }

            UIUMSUser.Save(_user);

            FillList(_user.Id);

            if (isNewUser)
            {
                Utilities.ShowMassage(lblErr, Color.Blue, "User information successfully saved");
                this.txtLogIn.Focus();
            }
            else
            {
                Utilities.ShowMassage(lblErr, Color.Blue, "User information successfully updated");
                MasterDetailEnabler(true, false);
            }
            ClearForm();
            if (IsSessionVariableExists(SESSIONNONSPECSTD))
            {
                RemoveFromSession(SESSIONNONSPECSTD);
            }
            if (IsSessionVariableExists("gvSelection"))
            {
                RemoveFromSession("gvSelection");
            }
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 2627)
            {
                Utilities.ShowMassage(lblErr, Color.Red, "Duplicate LogInIDs are not allowed");
            }
            else
            {
                Utilities.ShowMassage(lblErr, Color.Red, SqlEx.Message);
            }
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblErr, Color.Red, Ex.Message);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearForm();
            MasterDetailEnabler(true, false);
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblErr, Color.Red, Ex.Message);
        }
    }

    protected void chkIsSpecStd_CheckedChanged(object sender, EventArgs e)
    {
        if (chkIsSpecStd.Checked)
        {
            pnlSpecStd.Visible = true;
        }
        else
        {
            pnlSpecStd.Visible = false;
        }
    }

    #region btnFindStd_Click ---- Sajib Ahmed
    protected void btnFindStd_Click(object sender, EventArgs e)
    {
        #region Tanvir vai Code
        //    List<Student> stds = null;
        //    gvSelection.DataSource = null;
        //    // GetActiveStudentsByProgAndAdminCalCode Not found. So generate prototype.
        //    stds = Student.GetActiveStudentsByProgAndAdminCalCode(ddlProg.SelectedValue, ddlAcaCal.SelectedValue, txtFindStd.Text.Trim());
        //    if (stds != null)
        //    {
        //        gvSelection.DataSource = stds;
        //        gvSelection.DataBind();
        //    }
        //    CheckStdGridViewAfterBinding();
        #endregion
        List<LogicLayer.BusinessObjects.Student> stds = LogicLayer.BusinessLogic.StudentManager.GetAll();
        string studentId = txtFindStd.Text.Trim();
        if (stds != null && stds.Count > 0)
        {
            stds = stds.Where(x => x.Roll.Contains(studentId)).ToList();
            if (stds != null && stds.Count > 0)
            {
                stds = stds.OrderBy(x => x.Roll).ToList();

                gvSelection.DataSource = stds;
                gvSelection.DataBind();
            }
        }
        CheckStdGridViewAfterBinding();
    }
    #endregion

    protected void btnAddGridView_Click(object sender, EventArgs e)
    {
        _regExpressions = new List<RegExpressionUsingProgAcaCal>();
        if (Session[SESSIONNONSPECSTD] != null)
        {
            _regExpressions = (List<RegExpressionUsingProgAcaCal>)Session[SESSIONNONSPECSTD];
        }
        //if (_regExpressions.Count > 0)
        //{
        //    if (!Validation())
        //    {
        //        Utilities.ShowMassage(lblErr, Color.Blue, "You are trying to add parent item. So please delete the child items before.");
        //        return;
        //    }
        //}
        _regExpressions.Add(GetRegExpress());
        gvSelectedProgram.DataSource = null;

        gvSelectedProgram.DataSource = _regExpressions;
        gvSelectedProgram.DataBind();

        if (Session[SESSIONNONSPECSTD] != null)
        {
            Session.Remove(SESSIONNONSPECSTD);
        }
        Session.Add(SESSIONNONSPECSTD, _regExpressions);
    }
    protected void chkLeft_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ch = (CheckBox)sender;
        GridViewRow row = (GridViewRow)ch.NamingContainer;
        ch.Checked = ((CheckBox)sender).Checked;

        if (ch.Checked)
        {
            row.Cells[2].Text = clrAccessStartDate.Date.ToString();
            row.Cells[3].Text = clrAccessEndDate.Date.ToString();
        }
        else
        {
            row.Cells[2].Text = string.Empty;
            row.Cells[3].Text = string.Empty;
        }
    }
    protected void chkIsNonSpecStd_CheckedChanged(object sender, EventArgs e)
    {
        if (chkIsNonSpecStd.Checked)
        {
            pnlNonSpecStd.Visible = true;
        }
        else
        {
            pnlNonSpecStd.Visible = false;
        }
    }
    protected void gvSelectedProgram_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (IsSessionVariableExists(SESSIONNONSPECSTD))
        {
            RemoveFromSession(SESSIONNONSPECSTD);
        }
        gvSelectedProgram.EditIndex = e.NewEditIndex;

    }
    protected void gvSelectedProgram_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<RegExpressionUsingProgAcaCal> regs = (List<RegExpressionUsingProgAcaCal>)Session[SESSIONNONSPECSTD];
        regs.RemoveAt(e.RowIndex);
        RemoveFromSession(SESSIONNONSPECSTD);
        AddToSession(SESSIONNONSPECSTD, regs);

        gvSelectedProgram.DataSource = null;
        gvSelectedProgram.DataSource = regs;
        gvSelectedProgram.DataBind();
    }
    #endregion

    protected void gvSelection_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlProg_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillBatch();
    }
}
