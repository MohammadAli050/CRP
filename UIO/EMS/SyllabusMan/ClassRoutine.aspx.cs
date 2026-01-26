using System;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxTabControl;
using System.Drawing;
using BussinessObject;
using Common;
using System.Data;
using System.Data.SqlClient;

public partial class SyllabusMan_ClassRoutine : BasePage
{
    #region Session Names
    private const string SESSIONCLASSROUTINE = "CLASSROUTINE";
    #endregion

    #region Variables

    private List<AcademicCalender> _trimesterInfos = null;
    private List<AcademicCalenderSection> _routine = null;
    private List<Course> _courses = null;
    private List<RoomInfo> _rooms = null;
    private List<TimeSlotPlan> _timeSlot = null;
    private List<Teacher> _teachers = null;
    private List<Department> _depts = null;
    private string strID = string.Empty;
    List<TypeDefinition> _typeDefs = null;

    string selectedCourseVersionId = string.Empty;
    bool cboSearchCourse = false;
    int intCourseID = 0;
    int intVersionID = 0;
    string strErrorMsg = string.Empty;
    GridViewDataComboBoxColumn cbo = new GridViewDataComboBoxColumn();
    #endregion

    #region events

    //protected void Page_Init(object sender, EventArgs e)
    //{
    //    LoadRoutine();
    //}
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
                //DevExpress.Web.ASPxClasses.ASPxWebControl.RegisterBaseScript(this.Page);

                //FillGridCourseCombo();
                //FillRoomCombo((GridViewDataComboBoxColumn)gvRoutine.Columns[5], "RoomNumberAndCapacity");
                //FillRoomCombo((GridViewDataComboBoxColumn)gvRoutine.Columns[9], "RoomNumberAndCapacity");
                //FillDay((GridViewDataComboBoxColumn)gvRoutine.Columns[6]);
                //FillDay((GridViewDataComboBoxColumn)gvRoutine.Columns[10]);
                //FillTime((GridViewDataComboBoxColumn)gvRoutine.Columns[7]);
                //FillTime((GridViewDataComboBoxColumn)gvRoutine.Columns[11]);
                //FillTeacher((GridViewDataComboBoxColumn)gvRoutine.Columns[8]);
                //FillTeacher((GridViewDataComboBoxColumn)gvRoutine.Columns[12]);
                //FillDept((GridViewDataComboBoxColumn)gvRoutine.Columns[12]);
                //FillDept((GridViewDataComboBoxColumn)gvRoutine.Columns[12]);

                FillAcademicCalenderCombo();
                cboSearchCourse = true;
                FillGridCourseCombo(cboSearch);
            }

            btnDelete.Attributes.Add("onclick", "return confirm('Do you want to delete all records?');");
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
#if false
    devx grid view: grid row initializing for the occurence of insert and update event
#endif
    protected void gvRoutine_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "ChildCourse.FullCodeAndCourse")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            FillGridCourseCombo(combo);
            if (e.Value == null)
            {
                combo.SelectedIndex = 0;
                strID = combo.SelectedItem.Value.ToString();
            }
            if (!gvRoutine.IsNewRowEditing && strID == string.Empty)
            {
                string[] str = SplitValues(e.Value.ToString());
                Course cr = Course.GetCourses(str[0], str[1]);
                strID = cr.Id + "," + cr.VersionID;
            }
            combo.Callback += new CallbackEventHandlerBase(CourseCode_OnCallback);
            //combo.SelectedIndexChanged += new EventHandler(CourseCode_Selceted);
            //strID = combo.SelectedItem.Value.ToString();

            //if (e.KeyValue == DBNull.Value || e.KeyValue == null) return;
            //object val = gvRoutine.GetRowValuesByKeyValue(e.KeyValue, "ChildCourse.FullCodeAndCourse");            
            //if (val == DBNull.Value) return;
            //string country = (string)val;

            //ASPxComboBox combo = e.Editor as ASPxComboBox;
            //FillCityCombo(combo, country);

            //combo.Callback += new CallbackEventHandlerBase(cmbCity_OnCallback);

        }
        else if (e.Column.FieldName == "EquiCourse.FullCodeAndCourse")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            FillGridEquiCourseCombo(combo, strID);
            //object val = gvRoutine.GetRowValuesByKeyValue(e.KeyValue, "ChildCourse.FullCodeAndCourse");
            //if (val == DBNull.Value || val == null)
            //{
            //    FillGridEquiCourseCombo(combo, strID);
            //}
            //else
            //{
            //    FillGridEquiCourseCombo(combo, val.ToString());
            //}
            combo.Callback += new CallbackEventHandlerBase(EquiCourseCode_OnCallback);
        }
        else if (e.Column.Caption == "Credits")
        {
            ASPxTextBox txt = e.Editor as ASPxTextBox;
            string[] strcvid = SplitValues(selectedCourseVersionId);
            ShowCredits(txt, strcvid);
        }
        else if (e.Column.FieldName == "SectionName")
        {
            ASPxTextBox txt = e.Editor as ASPxTextBox;
            if (e.Value == null)
            {
                txt.Text = "A";
            }
        }
        else if (e.Column.FieldName == "Capacity")
        {
            ASPxSpinEdit spin = e.Editor as ASPxSpinEdit;
            if (e.Value == null)
            {
                spin.Text = "30";
            }
        }
        else if (e.Column.FieldName == "DayOne.Name")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            FillDay(combo);
            if (e.Value == null)
            {
                combo.SelectedIndex = 1;
            }
            combo.Callback += new CallbackEventHandlerBase(Day_OnCallback);
        }

        else if (e.Column.FieldName == "DayTwo.Name")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            FillDay(combo);
            combo.Callback += new CallbackEventHandlerBase(Day_OnCallback);
        }
        else if (e.Column.FieldName == "RoomInfoOne.RoomNumberAndCapacity")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            FillRoomCombo(combo);
            if (e.Value == null)
            {
                combo.SelectedIndex = 0;
            }
            combo.Callback += new CallbackEventHandlerBase(Room_OnCallback);
        }
        else if (e.Column.FieldName == "RoomInfoTwo.RoomNumberAndCapacity")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            FillRoomCombo(combo);//2
            combo.Callback += new CallbackEventHandlerBase(Room_OnCallback);
        }
        else if (e.Column.FieldName == "TimeSlotPlanOne.Time")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            FillTime(combo);
            if (e.Value == null)
            {
                combo.SelectedIndex = 0;
            }
            combo.Callback += new CallbackEventHandlerBase(Time_OnCallback);
        }
        else if (e.Column.FieldName == "TimeSlotPlanTwo.Time")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            FillTime(combo);
            combo.Callback += new CallbackEventHandlerBase(Time_OnCallback);
        }

        else if (e.Column.FieldName == "TeacherOneID.Code")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            FillTeacherCombo(combo);
            if (e.Value == null)
            {
                combo.SelectedIndex = 0;
            }
            combo.Callback += new CallbackEventHandlerBase(Teacher_OnCallback);
        }
        else if (e.Column.FieldName == "TeacherTwoID.Code")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            FillTeacherCombo(combo);
            combo.Callback += new CallbackEventHandlerBase(Teacher_OnCallback);
        }
        else if (e.Column.FieldName == "OwnerDepartment.Code")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            FillDeptCombo(combo);
            if (e.Value == null)
            {
                combo.SelectedIndex = 0;
            }
            else combo.Enabled = false;

            combo.Callback += new CallbackEventHandlerBase(Dept_OnCallback);
        }
        else if (e.Column.FieldName == "ShareDptOne.Code")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            FillDeptCombo(combo);
            combo.Callback += new CallbackEventHandlerBase(Dept_OnCallback);
        }
        else if (e.Column.FieldName == "ShareDptTwo.Code")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            FillDeptCombo(combo);
            combo.Callback += new CallbackEventHandlerBase(Dept_OnCallback);
        }
        else if (e.Column.FieldName == "SectionTypeDefinition.Definition")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            FillSectionCombo(combo);
            combo.Callback += new CallbackEventHandlerBase(Section_OnCallback);
        }
    }

    private void EquiCourseCode_OnCallback(object source, CallbackEventArgsBase e)
    {
        FillGridEquiCourseCombo(source as ASPxComboBox, e.Parameter);
    }

    //private void CourseCode_Selceted(object source, EventArgs e)
    //{
    //    FillGridEquiCourseCombo(source);
    //}

    private void Section_OnCallback(object source, CallbackEventArgsBase e)
    {
        FillSectionCombo(source as ASPxComboBox);
    }
    private void CourseCode_OnCallback(object source, CallbackEventArgsBase e)
    {
        FillGridCourseCombo(source as ASPxComboBox);
    }
    private void Day_OnCallback(object source, CallbackEventArgsBase e)
    {
        FillDay(source as ASPxComboBox);
    }
    private void Room_OnCallback(object source, CallbackEventArgsBase e)
    {
        FillRoomCombo(source as ASPxComboBox);
    }
    private void Time_OnCallback(object source, CallbackEventArgsBase e)
    {
        FillTime(source as ASPxComboBox);
    }
    private void Teacher_OnCallback(object source, CallbackEventArgsBase e)
    {
        FillTeacherCombo(source as ASPxComboBox);
    }
    private void Dept_OnCallback(object source, CallbackEventArgsBase e)
    {
        FillDeptCombo(source as ASPxComboBox);
    }
    protected void cboAcaCalender_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDeptCombo();
            cboDept_SelectedIndexChanged(null, null);
            lblMsg.Text = string.Empty;
            _routine = null;
            gvRoutine.DataSource = _routine;
            gvRoutine.DataBind();
            LoadRoutine();
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }

    protected void gvRoutine_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            Nullable<int> id = Convert.ToInt32(e.Keys["Id"]);
            AcademicCalenderSection acaCalSec = RefreshObject(e.NewValues, true, id);

            AcademicCalenderSection.Save(acaCalSec);

            e.Cancel = true;
            gvRoutine.CancelEdit();
            List<AcademicCalenderSection> acaSec = new List<AcademicCalenderSection>();
            acaSec = AcademicCalenderSection.GetCourseRoutine(Convert.ToInt32(cboAcaCalender.SelectedItem.Value.ToString()), Convert.ToInt32(cboDept.SelectedItem.Value.ToString()), Convert.ToInt32(cboProgram.SelectedItem.Value.ToString()), intCourseID, intVersionID);

            gvRoutine.DataSource = acaSec;
            gvRoutine.DataBind();
            if (Session[SESSIONCLASSROUTINE] != null)
            {
                Session.Remove(SESSIONCLASSROUTINE);
            }
            Session[SESSIONCLASSROUTINE] = acaSec;

        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally { }
    }
    protected void gvRoutine_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            AcademicCalenderSection AcaCalSection = RefreshObject(e.NewValues, false, null);
            if (AcaCalSection == null)
            {
                return;
            }
            AcademicCalenderSection.Save(AcaCalSection);
            e.Cancel = true;
            gvRoutine.CancelEdit();
            List<AcademicCalenderSection> _classroutine = new List<AcademicCalenderSection>();
            _classroutine = AcademicCalenderSection.GetCourseRoutine(Convert.ToInt32(cboAcaCalender.SelectedItem.Value.ToString()), Convert.ToInt32(cboDept.SelectedItem.Value.ToString()), Convert.ToInt32(cboProgram.SelectedItem.Value.ToString()), intCourseID, intVersionID);
            gvRoutine.DataSource = _classroutine;
            gvRoutine.DataBind();
            if (Session[SESSIONCLASSROUTINE] != null)
            {
                Session.Remove(SESSIONCLASSROUTINE);
            }
            Session.Add(SESSIONCLASSROUTINE, _classroutine);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    protected void gvRoutine_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        try
        {
            Nullable<int> id = Convert.ToInt32(e.Keys["Id"]);
            List<AcademicCalenderSection> routine = (List<AcademicCalenderSection>)Session[SESSIONCLASSROUTINE];
            var vs = from acs in routine where acs.Id == id.Value select acs;

            AcademicCalenderSection acaCalSec = (AcademicCalenderSection)vs.ElementAt(0);
            AcademicCalenderSection.Delete(acaCalSec.Id, -1);
            e.Cancel = true;
            gvRoutine.CancelEdit();
            List<AcademicCalenderSection> acaSec = new List<AcademicCalenderSection>();
            acaSec = AcademicCalenderSection.GetByACACAlenderID(Convert.ToInt32(cboAcaCalender.SelectedItem.Value.ToString()));
            if (acaSec != null)
            {
                gvRoutine.DataSource = acaSec;
                if (Session[SESSIONCLASSROUTINE] != null)
                {
                    Session.Remove(SESSIONCLASSROUTINE);
                }
                Session.Add(SESSIONCLASSROUTINE, acaSec);
            }
            else
            {
                gvRoutine.DataSource = new List<AcademicCalenderSection>();
                if (Session[SESSIONCLASSROUTINE] != null)
                {
                    Session.Remove(SESSIONCLASSROUTINE);
                }
            }
            gvRoutine.DataBind();
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    protected void gvRoutine_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void gvRoutine_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        try
        {
            //if (gvRoutine.GetSelectedFieldValues("Id") == null)
            //{
            //    gvRoutine.DataSource = new List<AcademicCalenderSection>();
            //    gvRoutine.DataBind();
            //}
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void gvRoutine_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (cboDept.SelectedItem.Value.ToString() == "0" || cboProgram.SelectedItem.Value.ToString() == "0")
        {
            e.RowError = Message.DEPENDENTINSERTNOTIFICATION;
            return;
        }
        for (int i = 1; i <= 8; i++)
        {
            if (i != 2)
            {
                GridViewColumn gvc = gvRoutine.Columns[i];
                GridViewDataColumn dataCol = gvc as GridViewDataColumn;
                if (e.NewValues[dataCol.FieldName] == null || e.NewValues[dataCol.FieldName].ToString() == "")
                {
                    e.Errors[dataCol] = Message.NULLFIELDNOTIFICATION;
                }
            }
        }
        AcademicCalenderSection acaCalSection = RefreshObject(e.NewValues, false, null);
        if (AcademicCalenderSection.HasDuplicateCode(acaCalSection, Convert.ToInt32(cboAcaCalender.SelectedItem.Value.ToString()), Convert.ToInt32(cboDept.SelectedItem.Value.ToString()), Convert.ToInt32(cboProgram.SelectedItem.Value.ToString()), acaCalSection.ChildCourseID, acaCalSection.ChildVersionID, acaCalSection.SectionName,acaCalSection.Capacity,acaCalSection.RoomInfoOneID,acaCalSection.RoomInfoTwoID,acaCalSection.DayOneValue,acaCalSection.DayTwoValue, acaCalSection.TimeSlotPlanOneID,acaCalSection.TimeSlotPlanTwoID,acaCalSection.TeacherIDOne,acaCalSection.TeacherIDTwo,acaCalSection.TypeDefinationID))
        {
            e.RowError = Message.DUPLICATEMESSAGE;
        }
    }
    protected void gvRoutine_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        strID = e.Parameters;
        LoadRoutine();
    }

    private void GridViewCommandColumnButtonState(GridViewCommandColumn col, bool boolButtonState)
    {
        col.EditButton.Visible = boolButtonState;
        col.NewButton.Visible = boolButtonState;
        col.DeleteButton.Visible = boolButtonState;
    }
    protected void cboProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewCommandColumn col = (GridViewCommandColumn)gvRoutine.Columns[0];
        if (cboProgram.SelectedItem.Value.ToString() == "0")
        {
            col.Visible = false;
            //GridViewCommandColumnButtonState((GridViewCommandColumn)gvRoutine.Columns[0], false);
        }
        else
        {
            col.Visible = true;
            //GridViewCommandColumnButtonState((GridViewCommandColumn)gvRoutine.Columns[0], true);
        }
        LoadRoutine();
    }
    protected void cboDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboDept.SelectedItem.Value.ToString() == "0")
        {
            FillProgramCombo(string.Empty);
            cboProgram.ReadOnly = true;
        }
        else
        {
            FillProgramCombo(cboDept.SelectedItem.Value.ToString());
            cboProgram.ReadOnly = false;
        }
        LoadRoutine();
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;

            if (gvRoutine.DataSource == null)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, Message.DELETENOTIFICATION);
                return;
            }
            AcademicCalenderSection.Delete(-1, Convert.ToInt32(cboAcaCalender.SelectedItem.Value.ToString()));
            LoadRoutine();
            Utilities.ShowMassage(lblMsg, Color.Blue, lblHeader.Text + " - " + Message.SUCCESSFULLYDELETED);
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (cboSearch.SelectedItem.Value.ToString() != "0")
        {
            string[] str = SplitValues(cboSearch.SelectedItem.Value.ToString());
            intCourseID = Convert.ToInt32(str[0]);
            intVersionID = Convert.ToInt32(str[1]);
        }
        LoadRoutine();
    }

    #endregion

    #region Functions
    private void FillDeptCombo()
    {
        try
        {
            cboDept.Items.Clear();
            List<Department> _depts = Department.GetDepts();
            if (_depts == null)
            {
                return;
            }
            cboDept.Items.Add("All", 0);
            foreach (Department dept in _depts)
            {
                ListEditItem item = new ListEditItem();
                item.Value = dept.Id.ToString();
                item.Text = dept.DetailedName;
                cboDept.Items.Add(item);
            }
            cboDept.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    private void FillProgramCombo(string str)
    {
        try
        {
            cboProgram.Items.Clear();
            List<Program> _programs = Program.GetProgramsByDeptID(str);
            if (_programs == null)
            {
                pnlCalender.Enabled = false;
                pnlSearch.Enabled = false;
                return;
            }
            pnlCalender.Enabled = true;
            pnlSearch.Enabled = true;
            cboProgram.Items.Add("All", 0);
            foreach (Program prog in _programs)
            {
                ListEditItem item = new ListEditItem();
                item.Value = prog.Id.ToString();
                item.Text = prog.ShortName;
                cboProgram.Items.Add(item);
            }
            cboProgram.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    private void ShowCredits(ASPxTextBox txt, String[] str)
    {
        Course _course = Course.GetCourse((Convert.ToInt32(str[0])), Convert.ToInt32(str[1]));
        if (_course == null)
        {
            return;
        }
        txt.Text = _course.Credits.ToString();
    }
    private void FillDay(ASPxComboBox combo)//1
    {
        combo.Items.Clear();
        combo.TextField = "Name";
        combo.ValueField = "Value";
        foreach (FriendlyWeekDays cs in FriendlyWeekDays.GetEnum())
        {
            ListEditItem item = new ListEditItem();
            item.Value = cs.Value.ToString();
            item.Text = cs.Name;
            combo.Items.Add(item);
        }
    }
    private void FillRoomCombo(ASPxComboBox combo)
    {
        try
        {
            _rooms = RoomInfo.GetRoomsInfo();
            if (_rooms == null)
            {
                return;
            }
            combo.Items.Clear();

            combo.TextField = "RoomNumberAndCapacity";
            combo.ValueField = "Id";

            foreach (RoomInfo cs in _rooms)
            {
                ListEditItem item = new ListEditItem();
                item.Value = cs.Id.ToString();
                item.Text = cs.RoomNumberAndCapacity;
                combo.Items.Add(item);
            }
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    private void FillGridEquiCourseCombo(ASPxComboBox combo, string strID)
    {
        try
        {
            string[] str = SplitValues(strID);
            _courses = null;
            if (str.Length == 2)
            {
                _courses = EquivalentCourse.GetEquiCourse(Convert.ToInt32(str[0]), Convert.ToInt32(str[1]));
            }
            //else if (str.Length == 3)
            //{
            //    Course cr = Course.GetCourses(str[0], string.Empty);
            //    _courses = EquivalentCourse.GetEquiCourse(cr.Id, cr.VersionID);
            //}
            //else if (str.Length == 4)
            //{
            //    Course cr = Course.GetCourses(str[0], str[1]);
            //    _courses = EquivalentCourse.GetEquiCourse(cr.Id, cr.VersionID);
            //} 
            combo.Items.Clear();
            combo.Text = string.Empty;

            if (_courses == null)
            {
                return;
            }
            combo.TextField = "FullCodeAndCourse";
            combo.ValueField = "Id";

            foreach (Course cs in _courses)
            {
                ListEditItem item = new ListEditItem();
                item.Value = cs.Id.ToString() + "," + cs.VersionID.ToString();
                item.Text = cs.FullCodeAndCourse;
                combo.Items.Add(item);
            }
            //combo.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
    }
    //private void FillGridEquiCourseCombo(object combo)
    //{
    //    try
    //    {
    //        //strID = ((ASPxComboBox)combo).Value.ToString();
    //        //string[] str = SplitValues(strID);
    //        //if (str.Length == 2)
    //        //{
    //        //    _courses = EquivalentCourse.GetEquiCourse(Convert.ToInt32(str[0]), Convert.ToInt32(str[1]));
    //        //}
    //        //else if (str.Length == 3)
    //        //{
    //        //    Course cr = Course.GetCourses(str[0], string.Empty);
    //        //    _courses = EquivalentCourse.GetEquiCourse(cr.Id, cr.VersionID);
    //        //}
    //        //else if (str.Length == 4)
    //        //{
    //        //    Course cr = Course.GetCourses(str[0], str[1]);
    //        //    _courses = EquivalentCourse.GetEquiCourse(cr.Id, cr.VersionID);
    //        //}


    //        //gvRoutine.Columns[2].

    //        //combo.Items.Clear();

    //        //if (_courses == null)
    //        //{
    //        //    return;
    //        //}
    //        //combo.TextField = "FullCodeAndCourse";
    //        //combo.ValueField = "Id";

    //        //foreach (Course cs in _courses)
    //        //{
    //        //    ListEditItem item = new ListEditItem();
    //        //    item.Value = cs.Id.ToString() + "," + cs.VersionID.ToString();
    //        //    item.Text = cs.FullCodeAndCourse;
    //        //    combo.Items.Add(item);
    //        //}
    //        //combo.SelectedIndex = 0;
    //    }
    //    catch (Exception ex)
    //    {
    //        Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
    //    }
    //}
    private void FillGridCourseCombo(ASPxComboBox combo)
    {
        try
        {
            _courses = Course.GetActiveCourses();//Course.GetActiveMotherCourses();
            if (_courses == null)
            {
                return;
            }
            combo.Items.Clear();
            if (cboSearchCourse)
            {
                combo.Items.Add("All", 0);
                cboSearchCourse = false;
                combo.SelectedIndex = 0;
            }
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
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    private void FillTime(ASPxComboBox combo)
    {
        try
        {
            _timeSlot = TimeSlotPlan.GetTimeSlotPlans();
            if (_timeSlot == null)
            {
                return;
            }
            combo.Items.Clear();

            combo.TextField = "Time";
            combo.ValueField = "Id";

            foreach (TimeSlotPlan cs in _timeSlot)
            {
                ListEditItem item = new ListEditItem();
                item.Value = cs.Id.ToString();
                item.Text = cs.Time;
                combo.Items.Add(item);
            }
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    private void FillTeacherCombo(ASPxComboBox combo)
    {
        try
        {
            _teachers = Teacher.Gets();
            if (_teachers == null)
            {
                return;
            }
            combo.Items.Clear();

            combo.TextField = "Code";
            combo.ValueField = "Id";

            foreach (Teacher cs in _teachers)
            {
                ListEditItem item = new ListEditItem();
                item.Value = cs.Id.ToString();
                item.Text = cs.Code;
                combo.Items.Add(item);
            }
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    private void FillDeptCombo(ASPxComboBox combo)
    {
        try
        {
            _depts = Department.GetDepts();
            if (_depts == null)
            {
                return;
            }
            combo.Items.Clear();

            combo.TextField = "Code";
            combo.ValueField = "Id";

            foreach (Department cs in _depts)
            {
                ListEditItem item = new ListEditItem();
                item.Value = cs.Id.ToString();
                item.Text = cs.Code;
                combo.Items.Add(item);
            }
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    private void FillTime(GridViewDataComboBoxColumn cbo)
    {
        try
        {
            _timeSlot = TimeSlotPlan.GetTimeSlotPlans();
            if (_timeSlot == null)
            {
                return;
            }
            //filling course code combo
            cbo.PropertiesComboBox.TextField = "Time";
            cbo.PropertiesComboBox.ValueField = "Id";

            foreach (TimeSlotPlan cs in _timeSlot)
            {
                ListEditItem item = new ListEditItem();
                item.Value = cs.Id.ToString();
                item.Text = cs.Time;
                cbo.PropertiesComboBox.Items.Add(item);
            }

            //.DataSource = _courses;
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    private void FillDay(GridViewDataComboBoxColumn cbo)
    {
        cbo.PropertiesComboBox.TextField = "Name";
        cbo.PropertiesComboBox.ValueField = "Value";

        foreach (FriendlyWeekDays cs in FriendlyWeekDays.GetEnum())
        {
            ListEditItem item = new ListEditItem();
            item.Value = cs.Value.ToString();
            item.Text = cs.Name;
            cbo.PropertiesComboBox.Items.Add(item);
        }
    }
    private void FillRoomCombo(GridViewDataComboBoxColumn cbo, string str)
    {
        try
        {
            _rooms = RoomInfo.GetRoomsInfo();
            if (_rooms == null)
            {
                return;
            }
            cbo.PropertiesComboBox.TextField = str;
            cbo.PropertiesComboBox.ValueField = "Id";
            cbo.PropertiesComboBox.DataSource = _rooms;
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    private void FillTeacher(GridViewDataComboBoxColumn cbo)
    {
        try
        {
            _teachers = Teacher.Gets();
            if (_teachers == null)
            {
                return;
            }
            cbo.PropertiesComboBox.TextField = "Code";
            cbo.PropertiesComboBox.ValueField = "Id";
            cbo.PropertiesComboBox.DataSource = _teachers;
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    private void FillDept(GridViewDataComboBoxColumn cbo)
    {
        try
        {
            _depts = Department.GetDepts();
            if (_depts == null)
            {
                return;
            }
            cbo.PropertiesComboBox.TextField = "Code";
            cbo.PropertiesComboBox.ValueField = "Id";
            cbo.PropertiesComboBox.DataSource = _depts;
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
            _courses = Course.GetActiveCourses();//Course.GetActiveMotherCourses();
            if (_courses == null)
            {
                return;
            }
            //filling course code combo
            cbo = (GridViewDataComboBoxColumn)gvRoutine.Columns[3];
            cbo.PropertiesComboBox.TextField = "FullCodeAndCourse";
            cbo.PropertiesComboBox.ValueField = "Id";

            foreach (Course cs in _courses)
            {
                ListEditItem item = new ListEditItem();
                item.Value = cs.Id.ToString() + "," + cs.VersionID.ToString();
                item.Text = cs.FullCodeAndCourse;
                cbo.PropertiesComboBox.Items.Add(item);
            }

            //.DataSource = _courses;
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
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
            
            cboAcaCalender_SelectedIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }

    private void FillSectionCombo(ASPxComboBox combo)
    {
        try
        {
            _typeDefs = TypeDefinition.GetTypes("Section");

            if (_typeDefs == null)
            {
                return;
            }
            combo.Items.Clear();

            combo.TextField = "Definition";
            combo.ValueField = "TypeDefinitionID";

            foreach (TypeDefinition typDef in _typeDefs)
            {
                ListEditItem item = new ListEditItem();
                item.Value = typDef.Id.ToString();
                item.Text = typDef.Definition;
                combo.Items.Add(item);
            }

            combo.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally { }
    }
    
    private void LoadRoutine()
    {
        try
        {
            if (cboAcaCalender.SelectedItem == null || cboDept.SelectedItem == null || cboProgram.SelectedItem == null)
            {
                return;
            }
            else
            {
                _routine = AcademicCalenderSection.GetCourseRoutine(Convert.ToInt32(cboAcaCalender.SelectedItem.Value.ToString()), Convert.ToInt32(cboDept.SelectedItem.Value.ToString()), Convert.ToInt32(cboProgram.SelectedItem.Value.ToString()), intCourseID, intVersionID);
                if (_routine == null)
                {
                    gvRoutine.DataSource = new List<AcademicCalenderSection>();
                    gvRoutine.DataBind();
                    return;
                }
                //removing prev session result and adding current datasource
                if (Session[SESSIONCLASSROUTINE] != null)
                {
                    Session.Remove(SESSIONCLASSROUTINE);
                }
                Session.Add(SESSIONCLASSROUTINE, _routine);
                gvRoutine.DataSource = _routine;
                //gvRoutine.KeyFieldName = "Id";
                gvRoutine.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        finally
        {
            intCourseID = 0;
            intVersionID = 0;
        }
    }
    private AcademicCalenderSection RefreshObject(OrderedDictionary newValues, bool IsEdit, Nullable<int> id)
    {
        AcademicCalenderSection acaCalSec = null;
        try
        {
            if (IsEdit)
            {
                List<AcademicCalenderSection> routine = (List<AcademicCalenderSection>)Session[SESSIONCLASSROUTINE];
                var vs = from acs in routine where acs.Id == id.Value select acs;
                acaCalSec = (AcademicCalenderSection)vs.ElementAt(0);
                acaCalSec.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                acaCalSec.ModifiedDate = DateTime.Now;
            }
            else
            {
                acaCalSec = new AcademicCalenderSection();
                acaCalSec.CreatorID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                acaCalSec.CreatedDate = DateTime.Now;
            }

            acaCalSec.AcademicCalenderID = Convert.ToInt32(cboAcaCalender.SelectedItem.Value.ToString());

            string[] strCourseIds = SplitValues(newValues["ChildCourse.FullCodeAndCourse"].ToString());
            acaCalSec.ChildCourseID = Convert.ToInt32(strCourseIds[0]);
            acaCalSec.ChildVersionID = Convert.ToInt32(strCourseIds[1]);

            acaCalSec.SectionName = newValues["SectionName"].ToString();
            acaCalSec.Capacity = Convert.ToInt32(newValues["Capacity"].ToString());
            acaCalSec.RoomInfoOneID = Convert.ToInt32(newValues["RoomInfoOne.RoomNumberAndCapacity"].ToString());
            acaCalSec.RoomInfoTwoID = (newValues["RoomInfoTwo.RoomNumberAndCapacity"] != null) ? Convert.ToInt32(newValues["RoomInfoTwo.RoomNumberAndCapacity"].ToString()) : 0;
            acaCalSec.DayOneValue = Convert.ToInt32(newValues["DayOne.Name"].ToString());
            acaCalSec.DayTwoValue = (newValues["DayTwo.Name"] != null) ? Convert.ToInt32(newValues["DayTwo.Name"].ToString()) : 0;
            acaCalSec.TimeSlotPlanOneID = Convert.ToInt32(newValues["TimeSlotPlanOne.Time"].ToString());
            acaCalSec.TimeSlotPlanTwoID = (newValues["TimeSlotPlanTwo.Time"] != null) ? Convert.ToInt32(newValues["TimeSlotPlanTwo.Time"].ToString()) : 0;
            acaCalSec.TeacherIDOne = Convert.ToInt32(newValues["TeacherOneID.Code"].ToString());
            acaCalSec.TeacherIDTwo = (newValues["TeacherTwoID.Code"] != null) ? Convert.ToInt32(newValues["TeacherTwoID.Code"].ToString()) : 0;
            acaCalSec.DeptID = Convert.ToInt32(cboDept.SelectedItem.Value.ToString());
            acaCalSec.ProgramID = Convert.ToInt32(cboProgram.SelectedItem.Value.ToString());
            acaCalSec.ShareDptIDOne = (newValues["ShareDptOne.Code"] != null) ? Convert.ToInt32(newValues["ShareDptOne.Code"].ToString()) : 0;
            acaCalSec.ShareDptIDTwo = (newValues["ShareDptTwo.Code"] != null) ? Convert.ToInt32(newValues["ShareDptTwo.Code"].ToString()) : 0;
            acaCalSec.TypeDefinationID = (newValues["SectionTypeDefinition.Definition"] != null) ? Convert.ToInt32(newValues["SectionTypeDefinition.Definition"].ToString()) : 0;
        }
        catch (Exception ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, ex.Message);
        }
        return acaCalSec;
    }
    private string[] SplitValues(string str)
    {
        return str.Split(new char[] { ',', '-' });
    }


    #endregion

}
