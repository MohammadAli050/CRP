using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessLogic;
using CommonUtility;

public partial class AutoMandatoryCourse : BasePage
{
    List<Student> _studentListWithCGPA = null;

    #region Event
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
        _scriptMan.AsyncPostBackTimeout = 36000;

        if (!IsPostBack)
        {
            lblCount.Text = "0";
            LoadDropDown();
        }

        //if (!string.IsNullOrEmpty(SessionManager.GetObjFromSession<string>(SessionName.Common_Session_Student_Roll)))
        //{
        //    if (string.IsNullOrEmpty(txtStudent.Text))
        //        txtStudent.Text = SessionManager.GetObjFromSession<string>(SessionName.Common_Session_Student_Roll);
        //}
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            lblCount.Text = "0";
            _studentListWithCGPA = new List<Student>();
            List<Student> studentList = null;
            gvStudentList.PageIndex = 0;

            int programId = Convert.ToInt32(ddlProgram.SelectedItem.Value);
            int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedItem.Value);
            int CGPASessionId = Convert.ToInt32(ddlCGPASession.SelectedItem.Value);

            // List<Student> studentList = StudentManager.GetAllFromRegWorksheetByProgramAndBatch(programId, acaCalId);

            if (!string.IsNullOrEmpty(txtStudent.Text.Trim()))
            {
                SessionManager.SaveObjToSession<string>(txtStudent.Text.Trim(), ConstantValue.Session_Student_Roll); 

                studentList = StudentManager.GetFromRegWorksheetByStudentRoll(txtStudent.Text.Trim());

                if (programId != 0)
                {
                    studentList = studentList.Where(s => s.ProgramID == programId).ToList();
                }
                if (acaCalId != 0)
                {
                    studentList = studentList.Where(s => s.BatchId == acaCalId).ToList();
                }
            }
            else
            {
                studentList = StudentManager.GetAllFromRegWorksheetByProgramAndBatch(programId, acaCalId);
            }

            if (studentList != null)
                studentList = studentList.Where(s => s.IsActive == true).ToList();

            #region Filter
            int isBlock = Convert.ToInt32(ddlBlock.SelectedValue);
            int isMajor = Convert.ToInt32(ddlMajor.SelectedValue);

            if (isBlock == 0) // 0 = not Block students
            {
                if (studentList != null)
                    studentList = studentList.Where(s => s.IsBlock == false).ToList();
            }
            else if (isBlock == 1) // 1 = Block Student
            {
                if (studentList != null)
                    studentList = studentList.Where(s => s.IsBlock == true).ToList();
            }

            if (isMajor == 1) // 0 = Major node declared students
            {
                if (studentList != null)
                    studentList = studentList.Where(s => !string.IsNullOrEmpty(s.Major1NodeID.ToString())).ToList();
            }
            else if (isMajor == 2) // 1 = Major node not declared Student
            {
                if (studentList != null)
                    studentList = studentList.Where(s => string.IsNullOrEmpty(s.Major1NodeID.ToString())).ToList();
            }
            #endregion

            if (studentList != null && studentList.Count > 0)
            {
                studentList = studentList.OrderBy(s => s.Roll).ToList();

                foreach (Student item in studentList)
                {
                    DeptRegSetUp drs = DeptRegSetUpManager.GetByProgramId(item.ProgramID);

                    StudentACUDetail obj = null;
                    if (chkCurrentCgpa.Checked)
                    {
                        obj = StudentACUDetailManager.GetCurrentCgpaByStudentId(item.StudentID);
                    }
                    else
                    {
                        obj = StudentACUDetailManager.GetByStudentIdAndBatchId(item.StudentID, CGPASessionId);
                    }

                    if (obj != null)
                    {
                        item.CGPA = obj.CGPA;                       

                        decimal AutoMandatoryCr = 0;
                        if (drs != null)
                        {
                            if (obj.CGPA <= drs.ManCGPA1 && obj.CGPA >= drs.ManCGPA2)
                                AutoMandatoryCr = drs.ManCredit1;
                            if (obj.CGPA < drs.ManCGPA2 && obj.CGPA >= drs.ManCGPA3)
                                AutoMandatoryCr = drs.ManCredit2;
                            if (obj.CGPA < drs.ManCGPA3 && obj.CGPA >= 0)
                                AutoMandatoryCr = drs.LocalCredit3;
                        }
                        item.AutoMandaotryCr = AutoMandatoryCr;
                    }
                    else
                    {
                        item.CGPA = 0;
                        item.AutoMandaotryCr = drs.LocalCredit3;
                    }

                    _studentListWithCGPA.Add(item);
                }

                if (_studentListWithCGPA != null && _studentListWithCGPA.Count() != 0)
                {
                    gvStudentList.DataSource = _studentListWithCGPA;
                    gvStudentList.DataBind();

                    SessionManager.SaveListToSession<Student>(_studentListWithCGPA, "studentListWithCGPA");

                    lblCount.Text = _studentListWithCGPA.Count().ToString();
                }
            }
            else
            {
                gvStudentList.DataSource = null;
                gvStudentList.DataBind();
                lblCount.Text = "0";
            }
        }
        catch (Exception ex)
        {

        }
    }
       
    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(SessionManager.GetObjFromSession<string>(ConstantValue.Session_Student_Roll)))
        {
            if (string.IsNullOrEmpty(txtStudent.Text))
                txtStudent.Text = SessionManager.GetObjFromSession<string>(ConstantValue.Session_Student_Roll);
        }
    }

    protected void btnAutoMandatory_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in gvStudentList.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("ChkActive");
                if (ckBox.Checked)
                {
                    RegistrationWorksheetParam rwParam = new RegistrationWorksheetParam();

                    HiddenField hdId = (HiddenField)row.FindControl("hdnId");
                    int id = Convert.ToInt32(hdId.Value);

                    Student student = StudentManager.GetById(id);

                    rwParam.StudentId = student.StudentID;
                    rwParam.AcademicCalenderID = student.BatchId;// Convert.ToInt32(ddlAcaCalBatch.SelectedItem.Value);
                    Label OpenCr = (Label)row.FindControl("lblAutoMandaotryCr");
                    rwParam.CrOpenLimit = Convert.ToDecimal(OpenCr.Text);
                    rwParam.DepartmentID = student.Program.DeptID;
                    rwParam.ProgramID = Convert.ToInt32(student.ProgramID);
                    rwParam.TreeCalendarMasterID = Convert.ToInt32(student.TreeCalendarMasterID);
                    rwParam.TreeMasterID = Convert.ToInt32(student.TreeMasterID);

                    bool result = RegistrationWorksheetManager.RegistrationWorksheetAutoMandatory(rwParam);

                    if (result)
                        LoadGridFromSession();
                }
            }
        }
        catch (Exception)
        {
        }
    }

    protected void gvStudentList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvStudentList.PageIndex = e.NewPageIndex;

            LoadGridFromSession();
        }
        catch (Exception ex)
        {

        }
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Checked)
            {
                chk.Text = "Unselect All";
            }
            else
            {
                chk.Text = "Select All";
            }

            foreach (GridViewRow row in gvStudentList.Rows)
            {
                CheckBox ckBox = (CheckBox)row.FindControl("ChkActive");
                ckBox.Checked = chk.Checked;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void chkCurrentCgpa_CheckedChanged(object sender, EventArgs e)
    {

        if (chkCurrentCgpa.Checked)
        {
            ddlCGPASession.Enabled = false;
            lblCGPASession.Enabled = false;
        }
        else
        {
            ddlCGPASession.Enabled = true;
            lblCGPASession.Enabled = true;
        }
    }
    #endregion

    #region Method


    private void LoadGridFromSession()
    {
        _studentListWithCGPA = null;
        _studentListWithCGPA = SessionManager.GetListFromSession<Student>("studentListWithCGPA");

        if (_studentListWithCGPA != null)
        {
            gvStudentList.DataSource = _studentListWithCGPA;
            gvStudentList.DataBind();

            lblCount.Text = _studentListWithCGPA.Count().ToString();
        }
    }
    private void LoadDropDown()
    {
        //follow the order of combo loding
        FillAcademicCalenderCombo();
        FillProgramCombo();
        //FillLinkCalender();
    }
    private void FillAcademicCalenderCombo()
    {
        try
        {
            ddlAcaCalBatch.Items.Clear();
            ddlAcaCalBatch.Items.Add(new ListItem("Select", "0"));

            ddlCGPASession.Items.Clear();
            ddlCGPASession.Items.Add(new ListItem("Select", "0"));

            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
            academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();


            ddlAcaCalBatch.AppendDataBoundItems = true;
            ddlCGPASession.AppendDataBoundItems = true;

            if (academicCalenderList != null)
            {
                ddlAcaCalBatch.DataSource = academicCalenderList.OrderByDescending(d => d.Code).ToList();
                ddlAcaCalBatch.DataBind();
                ddlCGPASession.DataSource = academicCalenderList.OrderByDescending(d => d.Code).ToList();
                ddlCGPASession.DataBind();
                //int count = academicCalenderList.Count;
                //foreach (AcademicCalender academicCalender in academicCalenderList)
                //{
                //    ddlAcaCalBatch.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                //    ddlCGPASession.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));

                //}

            }

        }
        catch (Exception ex)
        {
        }
        finally { }
    }
    private void FillProgramCombo()
    {
        try
        {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Add(new ListItem("Select", "0"));
            List<Program> programList = ProgramManager.GetAll();

            ddlProgram.AppendDataBoundItems = true;

            if (programList != null)
            {
                ddlProgram.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                ddlProgram.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    #endregion

    protected void gvStudentList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}