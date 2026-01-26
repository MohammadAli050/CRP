using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxTabControl;
using BussinessObject;
using DataAccess;
using Common;

public partial class SyllabusMan_CourseCreator : BasePage
{
    #region Session

    private const string SESSION_EQUICOURSE = "EQUICOURSE";
    private const string SESSION_COURSES = "COURSES";
    private const string SESSION_COURSE = "COURSE";
    private const string SESSION_COURSE_ACU_SPAN_MASTER = "COURSEACUSPANMAS";
    private const string SESSION_COURSE_ACU_SPAN_DETAILS = "COURSEACUSPANDTLS";
    #endregion

    #region Variables
    private List<Course> _courses = null;

    private List<Course> _assocCourses = null;
    private Course _course = null;
    private string[] _dataKey = new string[2] { "Id", "VersionID" };
    private List<EquivalentCourse> _listEquiCourses = new List<EquivalentCourse>();

    private CourseACUSpanMas _courseACUSpanMas = null;
    private List<CourseACUSpanDtl> _courseACUSpanDtls = null;
    private List<AcademicCalender> _trimesterInfos = null;
    private List<TypeDefinition> _typeDef = null;
    private List<LogicLayer.BusinessObjects.CourseGroup> _courseGroup = null;
    List<Program> _programs = null;
    #endregion

    #region Functions

    private void FillAcademicCalenderCombo()
    {
        try
        {
            _trimesterInfos = AcademicCalender.Gets();
            if (_trimesterInfos == null)
            {
                return;
            }
            cboStarACU.Items.Clear();

            cboStarACU.Items.Add("None", 0);

            cboStarACU.TextField = "_name";
            cboStarACU.ValueField = "Id";

            foreach (AcademicCalender ac in _trimesterInfos)
            {
                ListEditItem lei = new ListEditItem();
                lei.Value = ac.Id.ToString();
                lei.Text = ac.Name;
                cboStarACU.Items.Add(lei);
            }
            cboStarACU.SelectedIndex = 0;
            //cboAcaCalender_SelectedIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    private void FillCourseType()
    {
        try
        {
            _typeDef = TypeDefinition.GetTypes("Course");

            cboCourseType.Items.Clear();
            cboCourseType.Items.Add("None", 0);

            if (_typeDef == null)
            {
                cboCourseType.SelectedIndex = 0;
                return;
            }           

            cboCourseType.TextField = "Definition";
            cboCourseType.ValueField = "Id";

            foreach (TypeDefinition ac in _typeDef)
            {
                ListEditItem lei = new ListEditItem();
                lei.Value = ac.Id.ToString();
                lei.Text = ac.Definition;
                cboCourseType.Items.Add(lei);
            }
            cboCourseType.SelectedIndex = 0;
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
            cboPrograms.Items.Clear();
            _programs = Program.GetPrograms();

            if (_programs == null)
            {
                return;
            }
            cboPrograms.Items.Clear();

            cboPrograms.Items.Add("None", 0);

            cboPrograms.TextField = "ShortName";
            cboPrograms.ValueField = "Id";

            foreach (Program prog in _programs)
            {
                ListEditItem item = new ListEditItem();
                item.Value = prog.Id.ToString();
                item.Text = prog.ShortName;
                cboPrograms.Items.Add(item);
            }
            cboPrograms.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    private void FillGridCourseCombo()
    {
        try
        {
            _assocCourses = Course.GetCourses();//Course.GetActiveCourses();//Course.GetActiveMotherCourses();
            if (_assocCourses == null)
            {
                return;
            }
            cboAssociateCourse.Items.Clear();
            cboAssociateCourse.Items.Add("None", 0);

            cboAssociateCourse.TextField = "FullCodeAndCourse";
            cboAssociateCourse.ValueField = "Id";

            foreach (Course cs in _assocCourses)
            {
                if (IsSessionVariableExists(SESSION_COURSE))
                {
                    _course = (Course)GetFromSession(SESSION_COURSE);
                    if (_course.FormalCode == cs.FormalCode && _course.VersionCode == cs.VersionCode)
                    {
                        continue;
                    }
                }

                ListEditItem item = new ListEditItem();
                item.Value = cs.Id.ToString() + "," + cs.VersionID.ToString();
                item.Text = cs.FullCodeAndCourse;
                cboAssociateCourse.Items.Add(item);
            }
            cboAssociateCourse.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    private void FillCourseGroup()
    {
        try
        {
            _courseGroup = LogicLayer.BusinessLogic.CourseGroupManager.GetAll();
            if (_courseGroup == null)
            {
                return;
            }
            cboCourseGroup.Items.Clear();
            cboCourseGroup.Items.Add("None", 0);

            cboCourseGroup.TextField = "GroupName";
            cboCourseGroup.ValueField = "CourseGroupId";

            foreach (LogicLayer.BusinessObjects.CourseGroup cg in _courseGroup)
            {
                ListEditItem lei = new ListEditItem();
                lei.Value = cg.CourseGroupId.ToString();
                lei.Text = cg.GroupName;
                cboCourseGroup.Items.Add(lei);
            }
            cboCourseGroup.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }

    private void ClearForm()
    {
        this.txtCode.Text = "";
        this.txtVcode.Text = "";
        this.txtName.Text = "";
        this.txtCredit.Text = "";
        this.txtContents.Text = string.Empty;

        this.spnEditMaxACU.Value = 0;
        this.spnEditMinACU.Value = 0;

        gvwCourses.SelectedIndex = -1;

        chkHasEqui.Checked = false;
        chkCreditCourse.Checked = true;
        chkIsActive.Checked = true;
        chkSecMan.Checked = true;
        chkHasMultiSCUSpan.Checked = false;

        gdvEquiCourses.CancelEdit();
        gdvEquiCourses.DataSource = null;
        gdvEquiCourses.DataSource = new List<Course>();
        gdvEquiCourses.DataBind();
        gdvEquiCourses.Enabled = false;

        gdvAllowablelUnits.CancelEdit();
        gdvAllowablelUnits.DataSource = null;
        gdvAllowablelUnits.DataSource = new List<CourseACUSpanDtl>();
        gdvAllowablelUnits.DataBind();
        gdvAllowablelUnits.Enabled = false;

        cboAssociateCourse.SelectedIndex = 0;
        cboPrograms.SelectedIndex = 0;
        cboStarACU.SelectedIndex = 0;
        cboCourseType.SelectedIndex = 0;
        cboCourseGroup.SelectedIndex = 0;
        VersionEnbleDisable(false);
        pnlACUS.Enabled = false;
    }
    private void FillList()
    {
        if (txtSrch.Text.Trim().Length > 0)
        {
            _courses = Course.GetCourses(txtSrch.Text.Trim());
        }
        else
        {
            _courses = Course.GetCourses();
        }

        if (Session[SESSION_COURSES] != null)
        {
            Session.Remove(SESSION_COURSES);
        }
        Session.Add(SESSION_COURSES, _courses);

        if (_courses != null)
        {
            lblCount.Text = "Count: " + _courses.Count;

            gvwCourses.DataSource = null;
            gvwCourses.DataKeyNames = _dataKey;
            gvwCourses.DataSource = _courses;

            gvwCourses.DataBind();
            gvwCourses.DataKeyNames = _dataKey;

            DisableButtons();

            if (_courses.Count <= 0)
            {
                gvwCourses.DataSource = null;

                gvwCourses.DataBind();
                lblMsg.Text = string.Empty;
                lblMsg.Text = "No records found";
            }
        }
        else
        {
            gvwCourses.DataSource = null;

            gvwCourses.DataBind();
            lblMsg.Text = string.Empty;
            lblMsg.Text = "No records found";
        }
    }
    private void FillList(int courseID, int versionID)
    {
        _courses = new List<Course>();
        _courses.Add(Course.GetCourse(courseID, versionID));

        if (Session[SESSION_COURSES] != null)
        {
            Session.Remove(SESSION_COURSES);
        }
        Session.Add(SESSION_COURSES, _courses);

        if (_courses != null)
        {
            gvwCourses.DataSource = _courses;
            gvwCourses.DataKeyNames = _dataKey;

            gvwCourses.DataBind();

            DisableButtons();

            if (_courses.Count <= 0)
            {
                lblMsg.Text = string.Empty;
                lblMsg.Text = "No records found";
            }
        }
        else
        {
            lblMsg.Text = string.Empty;
            lblMsg.Text = "No records found";
        }
    }
    private DataSet FillListImport()
    {
        DataSet dsCourses = new DataSet();
        dsCourses.ReadXml(@"C:/Courses.xml", XmlReadMode.ReadSchema);

        gvwCourses.DataSource = dsCourses.Tables[0];
        //gvwCourses.DataKeyNames = _dataKey;
        gvwCourses.DataBind();

        //DisableButtons();
        return dsCourses;
    }
    private void DisableButtons()
    {
        if (gvwCourses.Rows.Count <= 0)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            //btnExport.Enabled = false;
            //pnlMIS.Enabled = false;
        }
        else
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            //btnExport.Enabled = true;
            //pnlMIS.Enabled = true;
        }
    }
    private void DisableButtons(bool enable)
    {
        btnEdit.Enabled = enable;
        btnDelete.Enabled = enable;
        //pnlMIS.Enabled = enable;
    }
    private void CollectionEnableDisable(bool enable)
    {
        pnlCourses.Enabled = enable;
        gvwCourses.Enabled = enable;
    }
    private void CourseEnableDisable(bool enable)
    {
        //pcTabControl.Enabled = enable;
        //pcTabControl.TabPages[0].Enabled = enable;
        //pcTabControl.TabPages[1].Enabled = enable;
        //pcTabControl.TabPages[2].Enabled = enable;
        //pcTabControl.TabPages[3].Enabled = enable;
        pnlGI.Enabled = enable;
        pnlMACU.Enabled = enable;
        pnlEQUI.Enabled = enable;
        pnlCont.Enabled = enable;
        pnlCourseCtl.Enabled = enable;

    }
    private void VersionEnbleDisable(bool enable)
    {
        rfvVCode.Enabled = enable;
        lblVCode.Visible = enable;
        txtCode.ReadOnly = enable;

        if (!txtCode.ReadOnly)
        {
            txtCode.Text = string.Empty;
        }
    }

    private void RefreshValue()
    {
        _course = new Course();
        _course = (Course)Session[SESSION_COURSE];
        this.txtCode.Text = _course.FormalCode.ToString();
        this.txtVcode.Text = _course.VersionCode.ToString();
        this.txtName.Text = _course.Title.ToString();
        this.txtCredit.Text = _course.Credits.ToString();
        this.txtContents.Text = _course.CourseContent.Trim();

        this.chkCreditCourse.Checked = _course.IsCredit;
        this.chkSecMan.Checked = _course.IsSectionMandatory;
        this.chkIsActive.Checked = _course.IsActive;

        if (_course.TypeDefinitionID != 0)
        {
            this.cboCourseType.Value = _course.TypeDefinitionID;
            this.cboCourseType.Text = BussinessObject.TypeDefinition.GetTypeDef(_course.TypeDefinitionID).Definition;
        }

        if (_course.CourseGroupId != 0)
        {
            this.cboCourseGroup.Value = _course.CourseGroupId;
            this.cboCourseGroup.Text = LogicLayer.BusinessLogic.CourseGroupManager.GetById(_course.CourseGroupId).GroupName;
        }

        if (_course.AssocCourseID != 0 && _course.AssocVersionID != 0)
        {
            this.cboAssociateCourse.Value = _course.AssocCourseID.ToString() + "," + _course.AssocVersionID.ToString();
        }

        if (_course.OwnerProgID > 0)
        {
            this.cboPrograms.Value = _course.OwnerProgID;
            this.cboPrograms.Text = _course.OwnerProgram.ShortName;
        }

        if (_course.StartACUID > 0)
        {
            this.cboStarACU.Value = _course.StartACUID;
            this.cboStarACU.Text = _course.StartACU.Name;
        }

        #region Multi ACU Spans
        if (_course.HasMultipleACUSpan)
        {
            this.chkHasMultiSCUSpan.Checked = true;
            pnlACUS.Enabled = true;
            if (_course.CourseACUSpanInfo != null)
            {
                _courseACUSpanMas = _course.CourseACUSpanInfo;
                RemoveFromSession(SESSION_COURSE_ACU_SPAN_MASTER);
                AddToSession(SESSION_COURSE_ACU_SPAN_MASTER, _courseACUSpanMas);

                RemoveFromSession(SESSION_COURSE_ACU_SPAN_DETAILS);
                _courseACUSpanDtls = _courseACUSpanMas.CourseACUSpanDetails;
                AddToSession(SESSION_COURSE_ACU_SPAN_DETAILS, _courseACUSpanDtls);

                this.spnEditMaxACU.Value = _courseACUSpanMas.MaxACUNo;
                this.spnEditMinACU.Value = _courseACUSpanMas.MinACUNo;

                BindACUSpanGrid();
            }
        }
        else
        {
            this.chkHasMultiSCUSpan.Checked = false;
            pnlACUS.Enabled = false;
            this.spnEditMaxACU.Value = 0;
            this.spnEditMinACU.Value = 0;


            RemoveFromSession(SESSION_COURSE_ACU_SPAN_MASTER);
            RemoveFromSession(SESSION_COURSE_ACU_SPAN_DETAILS);

            BindACUSpanGrid();
        }
        #endregion

        #region Fetch equivalent courses for the selected course

        //Convert.ToInt32(odict[0]), Convert.ToInt32(odict[1]));
        ////gdvEquiCourses.DataSource = (eqCourse == null) ? new List<Course>() : eqCourse;
        ////gdvEquiCourses.DataBind();

        ////if (eqCourse == null)
        ////{
        ////    gdvEquiCourses.Enabled = false;
        ////}

        //if (Session[SESSION_EQUICOURSE] == null)
        //{
        //    Session.Add(SESSION_EQUICOURSE, EquivalentCourse.GetEquiCourses(Convert.ToInt32(odict[0]), Convert.ToInt32(odict[1])));
        //}

        if (_course.HasEquivalents)
        {
            this.chkHasEqui.Checked = true;
            gdvEquiCourses.Enabled = true;
            _listEquiCourses = _course.Equivalents;
            RemoveFromSession(SESSION_EQUICOURSE);
            AddToSession(SESSION_EQUICOURSE, _listEquiCourses);


            List<Course> eqCourse = EquivalentCourse.GetCoursesInfo(_listEquiCourses);
            if (eqCourse == null)
            {
                gdvEquiCourses.DataSource = new List<Course>();
                gdvEquiCourses.DataBind();
                gdvEquiCourses.Enabled = false;
            }
            else
            {
                gdvEquiCourses.DataSource = eqCourse;
                gdvEquiCourses.DataBind();
            }
        }
        else
        {

            RemoveFromSession(SESSION_EQUICOURSE);
            this.chkHasEqui.Checked = false;
            gdvEquiCourses.DataSource = new List<Course>();
            gdvEquiCourses.DataBind();
            gdvEquiCourses.Enabled = false;
        }
        #endregion

        this.txtCode.Focus();
    }
    private Course RefreshObject()
    {
        Course course = null;
        if (Session[SESSION_COURSE] == null)
        {
            course = new Course();
        }
        else
        {
            course = (Course)Session[SESSION_COURSE];
        }

        course.FormalCode = txtCode.Text.Trim();
        course.VersionCode = txtVcode.Text.Trim();
        course.Title = txtName.Text.Trim();
        course.CourseContent = txtContents.Text.Trim();

        course.IsActive = chkIsActive.Checked;
        course.IsCredit = chkCreditCourse.Checked;
        course.IsSectionMandatory = chkSecMan.Checked;
        if (cboCourseType.SelectedItem != null)
            course.TypeDefinitionID = Int32.Parse(cboCourseType.SelectedItem.Value.ToString());

        if (cboCourseGroup.SelectedItem != null)
            course.CourseGroupId = Int32.Parse(cboCourseGroup.SelectedItem.Value.ToString());

        if (cboAssociateCourse.SelectedIndex > 0)
        {
            string[] str = Common.Methods.SplitValues(cboAssociateCourse.Value.ToString());
            course.AssocCourseID = Convert.ToInt32(str[0]);
            course.AssocVersionID = Convert.ToInt32(str[1]);
        }
        else
        {
            course.AssocCourseID = 0;
            course.AssocVersionID = 0;
        }

        if (cboPrograms.SelectedIndex > 0)
        {
            course.OwnerProgID = Convert.ToInt32(cboPrograms.Value.ToString());
        }
        else
        {
            course.OwnerProgID = 0;
        }

        if (cboStarACU.SelectedIndex > 0)
        {
            course.StartACUID = Convert.ToInt32(cboStarACU.Value.ToString());
        }
        else
        {
            course.StartACUID = 0;
        }

        decimal credits = 0;
        if (Decimal.TryParse(txtCredit.Text.Trim(), out credits))
        {
            course.Credits = credits;
        }

        if (chkHasMultiSCUSpan.Checked)
        {
            if (IsSessionVariableExists(SESSION_COURSE_ACU_SPAN_MASTER))
            {
                _courseACUSpanMas = (CourseACUSpanMas)Session[SESSION_COURSE_ACU_SPAN_MASTER];
            }
            else
            {
                _courseACUSpanMas = new CourseACUSpanMas();
            }

            course.CourseACUSpanInfo = _courseACUSpanMas;
            course.CourseACUSpanInfo.MaxACUNo = Int32.Parse(spnEditMaxACU.Value.ToString());
            course.CourseACUSpanInfo.MinACUNo = Int32.Parse(spnEditMinACU.Value.ToString());


            if (course.CourseACUSpanInfo.Id == 0)
            {
                course.CourseACUSpanInfo.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                course.CourseACUSpanInfo.CreatedDate = DateTime.Now;
            }
            else
            {
                course.CourseACUSpanInfo.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                course.CourseACUSpanInfo.ModifiedDate = DateTime.Now;
            }

            if (IsSessionVariableExists(SESSION_COURSE_ACU_SPAN_DETAILS))
            {
                course.CourseACUSpanInfo.CourseACUSpanDetails = (List<CourseACUSpanDtl>)GetFromSession(SESSION_COURSE_ACU_SPAN_DETAILS);
            }
        }
        else
        {
            course.CourseACUSpanInfo = null;
        }

        if (course.CourseACUSpanInfo != null && chkHasMultiSCUSpan.Checked)
        {
            course.HasMultipleACUSpan = true;
        }
        else
        {
            course.HasMultipleACUSpan = false;
        }

        if (IsSessionVariableExists(SESSION_EQUICOURSE))
        {
            course.Equivalents = (List<EquivalentCourse>)Session[SESSION_EQUICOURSE];
        }

        if (course.Equivalents != null && course.Equivalents.Count > 0 && chkHasEqui.Checked)
        {
            course.HasEquivalents = true;
        }
        else
        {
            course.HasEquivalents = false;
            course.Equivalents = null;
        }
        return course;
    }

    private EquivalentCourse RefreshEquiCourseObject(OrderedDictionary newValues)
    {
        EquivalentCourse equiCourse = new EquivalentCourse();

        equiCourse.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
        equiCourse.CreatedDate = DateTime.Now;

        string[] strIDs = Common.Methods.SplitValues(newValues["FullCodeAndCourse"].ToString());
        equiCourse.EquiCourseID = Convert.ToInt32(strIDs[0]);
        equiCourse.EquiVersionID = Convert.ToInt32(strIDs[1]);

        return equiCourse;
    }
    private void FillGridCourseCombo(ASPxComboBox combo)
    {
        try
        {
            _courses = Course.GetCourses();//Course.GetActiveMotherCourses();
            if (_courses == null)
            {
                return;
            }
            combo.Items.Clear();
            combo.TextField = "FullCodeAndCourse";
            combo.ValueField = "Id";

            foreach (Course cs in _courses)
            {
                ListEditItem item = new ListEditItem();
                item.Value = cs.Id.ToString() + "," + cs.VersionID.ToString();
                item.Text = cs.FullCodeAndCourse;
                combo.Items.Add(item);
            }
        }
        catch (Exception ex)
        {
            throw ex;
            //Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    private void BindGrid()
    {
        gdvEquiCourses.DataSource = null;
        if (_listEquiCourses == null)
        {
            chkHasEqui.Checked = false;
            chkHasEqui_CheckedChanged(null, null);
            gdvEquiCourses.Enabled = false;
            gdvEquiCourses.DataSource = new List<Course>();
        }
        else if (_listEquiCourses.Count == 0)
        {
            chkHasEqui.Checked = false;
            chkHasEqui_CheckedChanged(null, null);
            gdvEquiCourses.Enabled = false;
            gdvEquiCourses.DataSource = new List<Course>();
        }
        else
        {
            gdvEquiCourses.DataSource = EquivalentCourse.GetCoursesInfo(_listEquiCourses);
        }
        gdvEquiCourses.DataBind();

        if (Session[SESSION_EQUICOURSE] != null)
        {
            Session.Remove(SESSION_EQUICOURSE);
        }
        Session.Add(SESSION_EQUICOURSE, _listEquiCourses);
    }

    private CourseACUSpanDtl RefreshCourseACUSpanDtlObject(OrderedDictionary newValues)
    {
        CourseACUSpanDtl courseACUSpanDtl = new CourseACUSpanDtl();

        courseACUSpanDtl.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
        courseACUSpanDtl.CreatedDate = DateTime.Now;

        courseACUSpanDtl.CreditUnits = Convert.ToInt32(newValues["CreditUnits"]);

        return courseACUSpanDtl;
    }
    private void BindACUSpanGrid()
    {
        gdvAllowablelUnits.DataSource = null;
        if (_courseACUSpanDtls == null)
        {
            //chkHasMultiSCUSpan.Checked = false;
            //chkHasMultiSCUSpan_CheckedChanged(null, null);
            //gdvAllowablelUnits.Enabled = false;
            gdvAllowablelUnits.DataSource = new List<CourseACUSpanDtl>();

            if (Session[SESSION_COURSE_ACU_SPAN_DETAILS] != null)
            {
                Session.Remove(SESSION_COURSE_ACU_SPAN_DETAILS);
            }
        }
        else if (_courseACUSpanDtls.Count == 0)
        {
            //chkHasMultiSCUSpan.Checked = false;
            //chkHasMultiSCUSpan_CheckedChanged(null, null);
            //gdvAllowablelUnits.Enabled = false;
            gdvAllowablelUnits.DataSource = new List<CourseACUSpanDtl>();

            if (Session[SESSION_COURSE_ACU_SPAN_DETAILS] != null)
            {
                Session.Remove(SESSION_COURSE_ACU_SPAN_DETAILS);
            }
            Session.Add(SESSION_COURSE_ACU_SPAN_DETAILS, _courseACUSpanDtls);
        }
        else
        {
            gdvAllowablelUnits.DataSource = _courseACUSpanDtls;

            if (Session[SESSION_COURSE_ACU_SPAN_DETAILS] != null)
            {
                Session.Remove(SESSION_COURSE_ACU_SPAN_DETAILS);
            }
            Session.Add(SESSION_COURSE_ACU_SPAN_DETAILS, _courseACUSpanDtls);
        }
        gdvAllowablelUnits.DataBind();
    }

    private void ClearSession()
    {
        RemoveFromSession(SESSION_COURSES);
        RemoveFromSession(SESSION_COURSE);
        RemoveFromSession(SESSION_EQUICOURSE);
        RemoveFromSession(SESSION_COURSE_ACU_SPAN_MASTER);
        RemoveFromSession(SESSION_COURSE_ACU_SPAN_DETAILS);
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
            if (!IsPostBack && !IsCallback)
            {
                FillAcademicCalenderCombo();
                FillGridCourseCombo();
                FillProgramCombo();
                FillCourseType();
                FillCourseGroup();
                DisableButtons();
                gdvEquiCourses.Enabled = false;
                lblCourseID.Text = string.Empty;
                lblVersionID.Text = string.Empty;

                ClearSession();

                btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete the selected course?');");
                pcTabControl.ActiveTabIndex = 0;

                txtSrch.Focus();



                gdvEquiCourses.DataSource = new List<Course>(); // List<AcademicCalenderSection>();//making sure that AspxGridView's keyfieldname is set. 
                gdvEquiCourses.DataBind();
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }

    protected void butFind_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            FillList();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }
    protected void btnVersion_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (gvwCourses.SelectedRow == null)
            {
                lblMsg.Text = "Before trying to add a version, you must select the coure for which to create a version.";
                return;
            }

            RemoveFromSession(SESSION_EQUICOURSE);
            RemoveFromSession(SESSION_COURSE_ACU_SPAN_MASTER);
            RemoveFromSession(SESSION_COURSE_ACU_SPAN_DETAILS);

            CollectionEnableDisable(false);
            CourseEnableDisable(true);

            _course = new Course();

            DataKey dtkey = gvwCourses.SelectedDataKey;// SelectedPersistedDataKey;

            IOrderedDictionary odict = dtkey.Values;
            lblCourseID.Text = odict[0].ToString();
            lblVersionID.Text = odict[1].ToString();
            _course = Course.GetCourse(Convert.ToInt32(odict[0]), Convert.ToInt32(odict[1]));

            txtCode.Text = _course.FormalCode;

            VersionEnbleDisable(true);

            this.txtVcode.Focus();
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }
    protected void butSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            string oldCode = string.Empty;

            if (Session[SESSION_COURSE] != null)
            {
                oldCode = ((Course)Session[SESSION_COURSE]).FormalCode;
            }

            _course = null;

            _course = RefreshObject();

            if (Course.HasDuplicateCode(_course, oldCode))
            {
                throw new Exception("Duplicate codes are not allowed.");
            }

            bool isNewCourse = true;
            if (_course.Id > 0)
            {
                isNewCourse = false;
                _course.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _course.ModifiedDate = DateTime.Now;
            }
            else
            {
                _course.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                _course.CreatedDate = DateTime.Now;
            }

            Course.SaveCourse(_course);

            //Course.SaveCourse(_course, (Session[SESSIONEQUICOURSE] != null) ? (List<EquivalentCourse>)Session[SESSIONEQUICOURSE] : _listEquiCourses);

            FillList(_course.Id, _course.VersionID);

            if (isNewCourse)
            {
                lblMsg.Text = "Course information successfully saved";
                ClearForm();
                this.txtCode.Focus();
            }
            else
            {
                lblMsg.Text = "Course information successfully updated";
                ClearForm();
                CollectionEnableDisable(true);
                CourseEnableDisable(false);
                DisableButtons();
                txtSrch.Focus();
            }

            if (Session[SESSION_COURSE] != null)
            {
                Session.Remove(SESSION_COURSE);
            }
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 2627)
            {
                lblMsg.Text = "Duplicate codes are not allowed";
            }
            else
            {
                lblMsg.Text = SqlEx.Message;
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (Session[SESSION_COURSE] != null)
            {
                Session.Remove(SESSION_COURSE);
            }
            RemoveFromSession(SESSION_EQUICOURSE);
            RemoveFromSession(SESSION_COURSE_ACU_SPAN_MASTER);
            RemoveFromSession(SESSION_COURSE_ACU_SPAN_DETAILS);
            CollectionEnableDisable(false);
            CourseEnableDisable(true);

            this.txtCode.Focus();
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
            lblMsg.Text = string.Empty;
            if (gvwCourses.SelectedRow == null)
            {
                lblMsg.Text = "Before trying to edit an item, you must select the desired Item.";
                return;
            }

            #region Fetch selected course's attributes for edit
            _course = new Course();


            //DataKey dtkey = gvwCourses.SelectedPersistedDataKey;
            DataKey dtkey = gvwCourses.SelectedDataKey;


            IOrderedDictionary odict = dtkey.Values;
            lblCourseID.Text = odict[0].ToString();
            lblVersionID.Text = odict[1].ToString();
            _course = Course.GetCourse(Convert.ToInt32(odict[0]), Convert.ToInt32(odict[1]));

            if (Session[SESSION_COURSE] != null)
            {
                Session.Remove(SESSION_COURSE);
            }
            Session.Add(SESSION_COURSE, _course);

            FillGridCourseCombo();
            RefreshValue();
            #endregion

            this.txtName.Focus();

            CollectionEnableDisable(false);
            CourseEnableDisable(true);
            //DisableCourse(true);
            DisableButtons(false);
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (gvwCourses.SelectedRow == null)
            {
                lblMsg.Text = "Before deleting an item, you must select the Item.";
                return;
            }

            int index = gvwCourses.SelectedIndex;

            DataKey dtkey = gvwCourses.SelectedDataKey;

            IOrderedDictionary odict = dtkey.Values;

            Course.DeleteCourse(Convert.ToInt32(odict[0]), Convert.ToInt32(odict[1]));
            FillList();
            gvwCourses.SelectedIndex = -1;
            gvwCourses.SelectedIndex = index;

            lblMsg.Text = "Course information successfully deleted";
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 547)
            {
                lblMsg.Text = "This course has been referenced in other tables, please delete those references first.";
            }
            else
            {
                lblMsg.Text = SqlEx.Message;
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        ClearForm();
        CollectionEnableDisable(true);
        CourseEnableDisable(false);
        DisableButtons();
        txtSrch.Focus();
    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (Session[SESSION_COURSES] != null)
            {
                List<Course> courses = (List<Course>)Session[SESSION_COURSES];

                DataSet dsCourses = Course.ExportCourses(courses);

                dsCourses.WriteXml(@"C:/Courses.xml", XmlWriteMode.WriteSchema);

                lblMsg.Text = @"File created at C:/Course.xml";
            }

        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            DataSet dsCourses = FillListImport();

            Course.ImportCourses(dsCourses);

            lblMsg.Text = @"File imported";
        }
        catch (SqlException SqlEx)
        {
            if (SqlEx.Number == 2627)
            {
                lblMsg.Text = "Duplicate codes are not allowed";
            }
            else
            {
                lblMsg.Text = SqlEx.Message;
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = Ex.Message;
        }
    }

    protected void gvwCourses_Sorting(object sender, GridViewSortEventArgs e)
    {
        //gvwCourses.Sort(e.SortExpression, SortDirection.Ascending);
    }
    protected void gvwCourses_Sorted(object sender, EventArgs e)
    {

    }

    protected void gdvEquiCourses_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "FullCodeAndCourse")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            FillGridCourseCombo(combo);
            if (e.Value == null)
            {
                combo.SelectedIndex = 0;
                //strID = combo.SelectedItem.Value.ToString();
            }
            combo.Callback += new CallbackEventHandlerBase(CourseCode_OnCallback);
        }
    }
    private void CourseCode_OnCallback(object source, CallbackEventArgsBase e)
    {
        FillGridCourseCombo(source as ASPxComboBox);
    }
    protected void gdvEquiCourses_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            e.Cancel = true;
            gdvEquiCourses.CancelEdit();
            EquivalentCourse refEquicourse = RefreshEquiCourseObject(e.NewValues);
            if (Session[SESSION_EQUICOURSE] != null)
            {
                _listEquiCourses = (List<EquivalentCourse>)Session[SESSION_EQUICOURSE];
            }
            if (refEquicourse != null)
            {
                _listEquiCourses.Add(refEquicourse);
            }
            BindGrid();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    protected void gdvEquiCourses_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            EquivalentCourse refEquicourse = RefreshEquiCourseObject(e.NewValues);
            if (refEquicourse == null)
            {
                return;
            }
            e.Cancel = true;
            gdvEquiCourses.CancelEdit();
            if (Session[SESSION_EQUICOURSE] != null)
            {
                _listEquiCourses = (List<EquivalentCourse>)Session[SESSION_EQUICOURSE];
            }
            _listEquiCourses.Add(refEquicourse);
            BindGrid();
        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally { }
    }
    protected void gdvEquiCourses_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        try
        {
            _listEquiCourses = (List<EquivalentCourse>)Session[SESSION_EQUICOURSE];
            _courses = EquivalentCourse.GetCoursesInfo(_listEquiCourses);
            if (_courses == null)
            {
                return;
            }
            e.Cancel = true;
            gdvEquiCourses.CancelEdit();
            string[] str = Common.Methods.SplitValues(e.Values["FullCodeAndCourse"].ToString());
            foreach (Course c in _courses)
            {
                if (c.FormalCode.ToString() == str[0] && c.VersionCode.ToString() == str[1])
                {
                    int intIndex = 0;
                    bool isFound = false;
                    foreach (EquivalentCourse obj in _listEquiCourses)
                    {
                        if (obj.EquiCourseID == c.Id && obj.EquiVersionID == c.VersionID)
                        {
                            isFound = true;
                            break;
                        }

                        intIndex++;
                    }
                    if (isFound)
                    {
                        _listEquiCourses.RemoveAt(intIndex);
                        BindGrid();
                        if (_listEquiCourses == null)
                        {
                            chkHasEqui.Checked = false;
                            chkHasEqui_CheckedChanged(null, null);
                        }
                        else if (_listEquiCourses.Count == 0)
                        {
                            chkHasEqui.Checked = false;
                            chkHasEqui_CheckedChanged(null, null);
                        }
                    }
                    break;
                }
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
    protected void gdvEquiCourses_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void chkHasEqui_CheckedChanged(object sender, EventArgs e)
    {
        if (chkHasEqui.Checked)
        {
            gdvEquiCourses.Enabled = true;
            gdvEquiCourses.DataSource = new List<Course>();
            gdvEquiCourses.DataBind();
        }
        else
        {
            _listEquiCourses = (List<EquivalentCourse>)Session[SESSION_EQUICOURSE];

            if (_listEquiCourses != null)
            {
                _listEquiCourses.Clear();
            }
            else
            {
                _listEquiCourses = new List<EquivalentCourse>();
            }
            gdvEquiCourses.Enabled = false;
            gdvEquiCourses.DataSource = new List<Course>();
            gdvEquiCourses.DataBind();
        }
    }
    protected void gdvEquiCourses_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        string[] strIDs = Common.Methods.SplitValues(e.NewValues["FullCodeAndCourse"].ToString());
        if (Session[SESSION_EQUICOURSE] != null)
        {
            _listEquiCourses = (List<EquivalentCourse>)Session[SESSION_EQUICOURSE];
            if (_listEquiCourses.Count != 0)
            {
                foreach (EquivalentCourse ec in _listEquiCourses)
                {
                    if (ec.EquiCourseID.ToString() == strIDs[0] && ec.EquiVersionID.ToString() == strIDs[1])
                    {
                        e.RowError = Common.Message.DUPLICATEMESSAGE;
                    }
                }
            }
        }
    }

    protected void gdvAllowablelUnits_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        try
        {
            if (e.Column.FieldName == "CreditUnits")
            {
                ASPxSpinEdit spin = e.Editor as ASPxSpinEdit;
                if (e.Value == null)
                {
                    spin.Value = 3;
                }
            }
        }
        catch (Exception Ex)
        {

            throw Ex;
        }
    }
    protected void gdvAllowablelUnits_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            e.Cancel = true;
            gdvAllowablelUnits.CancelEdit();
            CourseACUSpanDtl courseACUSpanDtl = RefreshCourseACUSpanDtlObject(e.NewValues);
            if (Session[SESSION_COURSE_ACU_SPAN_DETAILS] != null)
            {
                _courseACUSpanDtls = (List<CourseACUSpanDtl>)Session[SESSION_COURSE_ACU_SPAN_DETAILS];
            }

            if (_courseACUSpanDtls == null)
            {
                _courseACUSpanDtls = new List<CourseACUSpanDtl>();
            }

            if (courseACUSpanDtl != null)
            {
                _courseACUSpanDtls.Add(courseACUSpanDtl);
            }
            BindACUSpanGrid();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    protected void gdvAllowablelUnits_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {

            throw Ex;
        }
    }
    protected void gdvAllowablelUnits_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            CourseACUSpanDtl courseACUSpanDtl = RefreshCourseACUSpanDtlObject(e.NewValues);
            if (courseACUSpanDtl == null)
            {
                return;
            }
            e.Cancel = true;
            gdvAllowablelUnits.CancelEdit();
            if (Session[SESSION_COURSE_ACU_SPAN_DETAILS] != null)
            {
                _courseACUSpanDtls = (List<CourseACUSpanDtl>)Session[SESSION_COURSE_ACU_SPAN_DETAILS];
            }
            if (courseACUSpanDtl != null)
            {
                _courseACUSpanDtls.Add(courseACUSpanDtl);
            }
            BindACUSpanGrid();
        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally { }
    }
    protected void gdvAllowablelUnits_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {

            throw Ex;
        }
    }
    protected void gdvAllowablelUnits_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        try
        {
            _courseACUSpanDtls = (List<CourseACUSpanDtl>)Session[SESSION_COURSE_ACU_SPAN_DETAILS];
            e.Cancel = true;
            gdvAllowablelUnits.CancelEdit();

            bool isFound = false;
            int intIndex = 0;
            foreach (CourseACUSpanDtl obj in _courseACUSpanDtls)
            {
                if (obj.CreditUnits == Convert.ToDecimal(e.Values["CreditUnits"]))
                {
                    isFound = true;
                    break;
                }

                intIndex++;
            }
            if (isFound)
            {
                _courseACUSpanDtls.RemoveAt(intIndex);
                BindACUSpanGrid();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
    protected void gdvAllowablelUnits_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {

            throw Ex;
        }
    }
    protected void chkHasMultiSCUSpan_CheckedChanged(object sender, EventArgs e)
    {
        if (chkHasMultiSCUSpan.Checked)
        {
            pnlACUS.Enabled = true;
            gdvAllowablelUnits.Enabled = true;

        }
        else
        {
            _courseACUSpanDtls = (List<CourseACUSpanDtl>)Session[SESSION_COURSE_ACU_SPAN_DETAILS];

            if (_courseACUSpanDtls != null)
            {
                _courseACUSpanDtls.Clear();
            }
            else
            {
                _courseACUSpanDtls = new List<CourseACUSpanDtl>();
            }
            spnEditMaxACU.Value = 0;
            spnEditMinACU.Value = 0;
            BindACUSpanGrid();
            pnlACUS.Enabled = false;
        }
    }

    protected void gdvAllowablelUnits_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (Session[SESSION_COURSE_ACU_SPAN_DETAILS] != null)
        {
            _courseACUSpanDtls = (List<CourseACUSpanDtl>)Session[SESSION_COURSE_ACU_SPAN_DETAILS];
            if (_courseACUSpanDtls.Count != 0)
            {
                decimal units = Convert.ToDecimal(e.NewValues["CreditUnits"]);
                foreach (CourseACUSpanDtl ec in _courseACUSpanDtls)
                {
                    if (Math.Round(units, 2) == Math.Round(ec.CreditUnits, 2))
                    {
                        e.RowError = Common.Message.DUPLICATEMESSAGE;
                    }
                }
            }
        }
    }
    protected void gdvAllowablelUnits_DataBinding(object sender, EventArgs e)
    {

    }

    #endregion
}
