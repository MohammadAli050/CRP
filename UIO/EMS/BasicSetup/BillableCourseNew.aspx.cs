using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using BussinessObject;
using System.Drawing;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

namespace EMS
{
    public partial class BasicSetup_BillableCourseNew : BasePage
    {
        #region Variables

        private List<Program> _programs = null;
        private List<AcademicCalender> _trimesterInfos = null;
        private List<CourseEntity> _courses = null;
        private Program _program = null;
        private CourseEntity _course = null;
        private BillableCourseEntity _billableCourse = null;
        private List<BillableCourseEntity> _billableCourses = null;
        private ViewNotSetBillableCoursesEntity _notSetBillableCourse = null;
        private List<ViewNotSetBillableCoursesEntity> _notSetBillableCourses = null;
        private bool _isFindClicked = false;
        private bool _isCourseClicked = false;
        private string[] _dataKey = new string[1] { "Id" };

        #endregion

        #region Session Consts
        private const string SESSION_PROGRAM = "Program";
        private const string SESSION_COURSE = "Course";
        private const string SESSIONTABLE = "DATA_TABLE";
        private const string SESSIONCURRENTTRIMESTER = "CurrentTrimester";
        private const string SESSION_BILLABLE_COURSE = "BillableCourse";
        private const string SESSION_IS_COURSE_CLICKED = "isCourseClicked";
        private const string SESSION_IS_FIND_CLICKED = "isFindClicked";

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

                if (!IsPostBack)
                {
                    BindControls();
                    DisbleRightContainerButtons();
                    //FillSchCombo();
                    //DisableButtons();
                    //txtSrch.Focus();
                }
                //btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete?');");
            }
            catch (Exception exception)
            {
                Utilities.ShowMassage(lblErrorMessage, Color.Red, exception.Message);
            }
        }

        protected void OnDDLProgramChanged(object sender, EventArgs e)
        {
            try
            {
                List<RbProgramEntity> programEntities = BillableCourse_BAO.GetAllPrograms();
                if (programEntities == null)
                {
                    lblErrorMessage.Text = "Could not find any program!";
                    return;
                }

                string programCode = programEntities[DDLProgram.SelectedIndex].Code;
            }
            catch (Exception exception)
            {
                string msg = exception.ToString();
                lblErrorMessage.Text = msg;
            }

        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                lblErrorMessage.Text = string.Empty;
                lblErrorMessage.Visible = false;
                _isCourseClicked = false;
                if (Session[SESSION_IS_COURSE_CLICKED] != null)
                {
                    Session.Remove(SESSION_IS_COURSE_CLICKED);
                }
                Session.Add(SESSION_IS_COURSE_CLICKED, _isCourseClicked);

                _isFindClicked = true;
                if (Session[SESSION_IS_FIND_CLICKED] != null)
                {
                    Session.Remove(SESSION_IS_FIND_CLICKED);
                }
                Session.Add(SESSION_IS_FIND_CLICKED, _isFindClicked);

                FillBillaleCourseGrid(txtSearch.Text);
            }
            catch (Exception exception)
            {
                string strError = exception.Message;
                Utilities.ShowMassage(lblErrorMessage,Color.Red,strError);
            }

        }

        protected void btnNotSetBillableCourses_Click(object sender, EventArgs e)
        {
            _isCourseClicked = true;
            if (Session[SESSION_IS_COURSE_CLICKED] != null)
            {
                Session.Remove(SESSION_IS_COURSE_CLICKED);
            }
            Session.Add(SESSION_IS_COURSE_CLICKED, _isCourseClicked);

            _isFindClicked = false;
            if (Session[SESSION_IS_FIND_CLICKED] != null)
            {
                Session.Remove(SESSION_IS_FIND_CLICKED);
            }
            Session.Add(SESSION_IS_FIND_CLICKED, _isFindClicked);

            FillGridWithNotSetBillableCourses();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewCourses.SelectedRow == null)
                {
                    string errorMessage = "In order to edit an Item, u must select the item";
                    lblErrorMessage.Visible = true;
                    Utilities.ShowMassage(lblErrorMessage, errorMessage);
                    return;
                }
                else
                {
                    ClearErrorMessage();
                }
                //Get the course id and version id from the selected row
                Label lblInnerCourseId = new Label();
                lblInnerCourseId = (Label)gridViewCourses.SelectedRow.Cells[1].FindControl("lblCourseID");

                Label lblInnerVersionID = new Label();
                lblInnerVersionID = (Label)gridViewCourses.SelectedRow.Cells[0].FindControl("lblVersionID");

                _course = new CourseEntity();
                //int courseID = Convert.ToInt32(gridViewCourses.SelectedValue);

                int courseID = Convert.ToInt32(lblInnerCourseId.Text);
                int versionId = Convert.ToInt32(lblInnerVersionID.Text);

                _course = new CourseEntity();
                _course = Course_BAO.GetCourseByIdVersionID(courseID, versionId);
                if (Session[SESSION_COURSE] != null)
                {
                    Session.Remove(SESSION_COURSE);
                }
                Session.Add(SESSION_COURSE, _course);

                _isFindClicked = (bool) Session[SESSION_IS_FIND_CLICKED];
                if (_isFindClicked)
                {
                    _billableCourse = new BillableCourseEntity();
                    _billableCourse = BillableCourse_BAO.GetCourseByCourseIdVersionID(courseID, versionId);
                    if (Session[SESSION_BILLABLE_COURSE] != null)
                    {
                        Session.Remove(SESSION_BILLABLE_COURSE);
                    }
                    Session.Add(SESSION_BILLABLE_COURSE, _billableCourse);
                    //To DO
                }

                //Remove any existing Course object in the sesson

                DisableCollection(false);
                DisableButtons(false);
                btnSave.Enabled = true;
                EnableRightContainerButtons();
                RefreshValue();
                contentBodyRight.Disabled = false;
                
            }
            catch (Exception exception)
            {
                lblErrorMessage.Visible = true;
                Utilities.ShowMassage(lblErrorMessage, exception.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMessage();
                _billableCourse = new BillableCourseEntity();
                if (Session[SESSION_COURSE] != null)
                {
                    _course = (CourseEntity)Session[SESSION_COURSE];
                    _billableCourse.CourseID = _course.Id;
                    _billableCourse.VersionID = _course.VersionID;
                    _billableCourse.IsCreditCourse = _course.IsCredit;
                    _billableCourse.AcaCalID = Convert.ToInt32(ddlAcaCalender.SelectedValue);
                    _billableCourse.ProgramID = Convert.ToInt32(DDLProgram.SelectedValue);
                    _billableCourse.BillStartFromRetakeNo = Convert.ToInt32(txtRetakeNumber.Text);

                    if (Variables.SaveMode == SaveMode.Add)
                    {
                        _billableCourse.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                        _billableCourse.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        _billableCourse.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                        _billableCourse.ModifiedDate = DateTime.Now;
                    }

                }
                int counter = BillableCourse_BAO.Save(_billableCourse);
                if(counter==1)
                {
                    lblErrorMessage.Visible = true;
                    string message = counter + " Row(s) succesfully updated";
                    Utilities.ShowMassage(lblErrorMessage, System.Drawing.Color.Blue, message);
                }
                //contentBodyRight.Disabled = true;
                btnSave.Enabled = false;
                //btnCancel.Enabled = false;

            }
            catch (Exception exception)
            {
                lblErrorMessage.Visible = true;
                Utilities.ShowMassage(lblErrorMessage, exception.Message);
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                lblErrorMessage.Text = string.Empty;
                ClearForm();
                DisableCollection(true);
                DisableButtons();
                DisbleRightContainerButtons();
                contentBodyRight.Disabled = true;
                txtSearch.Focus();
            }
            catch (Exception exception)
            {
                Utilities.ShowMassage(lblErrorMessage, exception.Message);
            }
        }

        
        #endregion

        #region Methods
        private void ClearErrorMessage()
        {
            lblErrorMessage.Text = string.Empty;
            lblErrorMessage.Visible = false;
        }

        private void ClearForm()
        {
            try
            {
                txtCourseCode.Text = string.Empty;
                txtCourseName.Text = string.Empty;
                txtCourseVersion.Text = string.Empty;
                txtRetakeNumber.Text = string.Empty;
            }
            catch (Exception exception)
            {
                Utilities.ShowMassage(lblErrorMessage, exception.Message);
            }
        }

        private void DisableButtons(bool enable)
        {
            btnEdit.Enabled = enable;
            btnDelete.Enabled = enable;
        }

        private void DisableButtons()
        {
            if (gridViewCourses.Rows.Count <= 0)
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
            else
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void DisbleRightContainerButtons()
        {
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void EnableRightContainerButtons()
        {
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void DisableCollection(bool enable)
        {
            gridViewCourses.Enabled = enable;
        }

        private void BindControls()
        {
            try
            {
                FillProgramDropDownList();
                FillAcademicCalenderDropDownList();
                //FillCourseGrid(txtSearch.Text);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void FillBillaleCourseGrid(string searchText)
        {
            try
            {
                //_courses = new List<CourseEntity>();
                //_courses = Course_BAO.GetAllCourses(searchText);
                _billableCourses = new List<BillableCourseEntity>();
                _billableCourses = BillableCourse_BAO.GetAllBillableCourses(searchText);
                gridViewCourses.DataSource = null;
                gridViewCourses.DataSource = _billableCourses;
                gridViewCourses.DataKeyNames = _dataKey;
                gridViewCourses.DataBind();
            }
            catch (Exception exception)
            {
                lblErrorMessage.Text = string.Empty;
                lblErrorMessage.Text = exception.Message;
            }
        }

        private void FillGridWithNotSetBillableCourses()
        {
            _notSetBillableCourses = new List<ViewNotSetBillableCoursesEntity>();
            _notSetBillableCourses = BillableCourse_BAO.GetAllNotSetBillableCourses();
            gridViewCourses.DataSource = null;
            gridViewCourses.DataSource = _notSetBillableCourses;
            //gridViewCourses.DataKeyNames = _dataKey;
            gridViewCourses.DataBind();
            _billableCourses = null;
            _billableCourse = null;
        }

        private void FillProgramDropDownList()
        {
            try
            {
                List<RbProgramEntity> programEntities = FormSaleSubmit_BAO.GetAllPrograms();
                //.......................
                if (programEntities != null && programEntities.Count > 0)
                {
                    if (Session[SESSION_PROGRAM] != null)
                    {
                        Session.Remove(SESSION_PROGRAM);
                    }
                    Session[SESSION_PROGRAM] = programEntities;
                }
                //.......................
                if (programEntities == null)
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "No Program Found";
                    return;
                }

                DDLProgram.DataSource = programEntities;
                DDLProgram.DataTextField = "Shortname";
                DDLProgram.DataValueField = "Id";
                //DDLProgram.DataValueField = "Code";
                DDLProgram.DataBind();
            }
            catch (Exception exception)
            {
                lblErrorMessage.Text = string.Empty;
                lblErrorMessage.Text = exception.Message;
            }
        }

        private void FillAcademicCalenderDropDownList()
        {
            try
            {
                string currentTrimester = "";
                _trimesterInfos = AcademicCalender.Gets();

                if (_trimesterInfos == null)
                {
                    return;
                }

                foreach (AcademicCalender ac in _trimesterInfos)
                {
                    if (ac.IsCurrent)
                    {
                        currentTrimester = ac.Id.ToString();
                    }

                    ListItem lei = new ListItem();
                    lei.Value = ac.Id.ToString();
                    lei.Text = ac.CalenderUnitType.TypeName.ToString() + " " + ac.Year.ToString();
                    ddlAcaCalender.Items.Add(lei);
                }

                if (Session[SESSIONCURRENTTRIMESTER] != null)
                {
                    Session.Remove(SESSIONCURRENTTRIMESTER);
                }
                Session[SESSIONCURRENTTRIMESTER] = currentTrimester;

                ddlAcaCalender.SelectedValue = currentTrimester;
            }
            catch (Exception ex)
            {
                Utilities.ShowMassage(lblErrorMessage, ex.Message);
            }
            finally { }
        }

        private void RefreshValue()
        {
            try
            {
                _isCourseClicked = (bool)Session[SESSION_IS_COURSE_CLICKED];
                if (_isCourseClicked)
                {
                    _course = new CourseEntity();
                    _course = (CourseEntity)Session[SESSION_COURSE];
                    txtCourseCode.Text = _course.FormalCode;
                    txtCourseName.Text = _course.Title;
                    txtCourseVersion.Text = _course.VersionID.ToString();
                    txtRetakeNumber.Text = "0";
                    Variables.SaveMode = SaveMode.Add;

                }
                _isFindClicked = (bool)Session[SESSION_IS_FIND_CLICKED];
                if (_isFindClicked)
                {
                    _course = new CourseEntity();
                    _course = (CourseEntity)Session[SESSION_COURSE];
                    txtCourseCode.Text = _course.FormalCode;
                    txtCourseName.Text = _course.Title;

                    _billableCourse = new BillableCourseEntity();
                    _billableCourse = (BillableCourseEntity)Session[SESSION_BILLABLE_COURSE];

                    txtCourseVersion.Text = _billableCourse.VersionID.ToString();
                    txtRetakeNumber.Text = _billableCourse.BillStartFromRetakeNo.ToString();

                    Variables.SaveMode = SaveMode.Update;
                }

            }
            catch (Exception exception)
            {
                lblErrorMessage.Visible = true;
                Utilities.ShowMassage(lblErrorMessage, exception.Message);
            }
        }
        #endregion

    }
}
