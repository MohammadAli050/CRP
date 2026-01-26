using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ForceOperationForCourse : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack)
        {
            ucProgram.LoadDropDownList();
            ddlAutoOpen.SelectedValue = "0";
            ddlPreRegistration.SelectedValue = "0";
            chkPriority.Checked = true;
            LoadCourse();
        }
        txtStudentName.Text = string.Empty;
    }

    //void LoadComboBox()
    //{
    //    LoadSemester();
    //    LoadBatch();
    //    LoadProgram();
    //    LoadCourse();
    //}

    //protected void LoadSemester()
    //{
    //    try
    //    {
    //        ddlSemester.Items.Clear();
    //        ddlSemester.Items.Add(new ListItem("Select", "0"));

    //        List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
    //        academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();


    //        ddlSemester.AppendDataBoundItems = true;

    //        if (academicCalenderList != null)
    //        {
    //            int count = academicCalenderList.Count;
    //            foreach (AcademicCalender academicCalender in academicCalenderList)
    //                ddlSemester.Items.Add(new ListItem("[" + academicCalender.Code + "] " + UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));

    //            AcademicCalender acaCal = academicCalenderList.Where(x => x.IsActiveRegistration == true).SingleOrDefault();
    //            if (acaCal != null)
    //                ddlSemester.SelectedValue = acaCal.AcademicCalenderID.ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //    finally { }
    //}

    //protected void LoadBatch()
    //{
    //    try
    //    {
    //        ddlBatch.Items.Clear();
    //        ddlBatch.Items.Add(new ListItem("Select", "0"));

    //        List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
    //        academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();


    //        ddlBatch.AppendDataBoundItems = true;

    //        if (academicCalenderList != null)
    //        {
    //            int count = academicCalenderList.Count;
    //            foreach (AcademicCalender academicCalender in academicCalenderList)
    //            {
    //                ddlBatch.Items.Add(new ListItem("[" + academicCalender.Code + "] " + UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //    finally { }
    //}

    //protected void LoadProgram()
    //{
    //    try
    //    {
    //        ddlProgram.Items.Clear();
    //        ddlProgram.Items.Add(new ListItem("Select", "0"));
    //        List<Program> programList = ProgramManager.GetAll();

    //        ddlProgram.AppendDataBoundItems = true;

    //        if (programList != null)
    //        {
    //            ddlProgram.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
    //            ddlProgram.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //    finally { }
    //}

    protected void LoadCourse()
    {
        try
        {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Select", "0"));

            List<Course> courseList = CourseManager.GetAll();
            if (courseList.Count > 0 && courseList != null)
            {
                foreach (Course course in courseList)
                {
                    string valueField = Convert.ToString(course.CourseID); //+"_" + course.VersionID;
                    string textField = "[" + course.FormalCode + "]-" + course.Title;
                    ddlCourse.Items.Add(new ListItem(textField, valueField));
                }
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
    }

    #endregion

    #region Event

    protected void btnPickStudentName_Click(object sender, EventArgs e)
    {

    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            string[] temp = new string[2];
            string[] courseName = new string[2];
            if (ddlCourse.SelectedValue != "0")
            {
                temp = ddlCourse.SelectedItem.Text.Split(']');
                courseName = temp[0].Split('[');

            }


            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int batchId = Convert.ToInt32(ucBatch.selectedValue);
            string newBatchId;
            if (batchId == 0)
            {
                newBatchId = "";
            }
            else 
            {
                newBatchId = Convert.ToString(batchId);
            }
            int semesterId = Convert.ToInt32(ucSession.selectedValue);
            int courseId = Convert.ToInt32(ddlCourse.SelectedValue);
            
            string studentRoll = txtStudentId.Text;
            int autoOpen = ddlAutoOpen.SelectedItem.Text == "X" ? 2 : Convert.ToInt32(ddlAutoOpen.SelectedValue) - 1;
            int preReg = ddlPreRegistration.SelectedItem.Text == "X" ? 2 : Convert.ToInt32(ddlPreRegistration.SelectedValue) - 1;
            int mandatory = ddlMandatory.SelectedItem.Text == "X" ? 2 : Convert.ToInt32(ddlMandatory.SelectedValue) - 1;

            List<ClassForceOperation> forceOperationList = ClassForceOperationManager.GetAllByParameters(programId, newBatchId, semesterId, courseId, studentRoll);

            if (forceOperationList.Count > 0 && forceOperationList != null)
                forceOperationList = forceOperationList.OrderBy(x => x.StudentID).ToList();

            if(forceOperationList.Count > 0 && forceOperationList != null)
                if (ddlAutoOpen.SelectedValue != "" && ddlAutoOpen.SelectedValue != "0")
                    forceOperationList = forceOperationList.Where(x => x.IsAutoOpen == Convert.ToBoolean(autoOpen)).ToList();

            if (forceOperationList.Count > 0 && forceOperationList != null)
                if (ddlPreRegistration.SelectedValue != "" && ddlPreRegistration.SelectedValue != "0")
                    forceOperationList = forceOperationList.Where(x => x.IsAutoAssign == Convert.ToBoolean(preReg)).ToList();

            if (forceOperationList.Count > 0 && forceOperationList != null)
                if (ddlMandatory.SelectedValue != "" && ddlMandatory.SelectedValue != "0")
                    forceOperationList = forceOperationList.Where(x => x.IsMandatory == Convert.ToBoolean(mandatory)).ToList();

            if (forceOperationList.Count > 0 && forceOperationList != null)
                if (txtCourseCode.Text != "")
                    forceOperationList = forceOperationList.Where(x => x.CourseCode.ToUpper().Contains(txtCourseCode.Text.ToUpper())).ToList();

            if (forceOperationList.Count > 0 && forceOperationList != null)
                if (txtCourseTitle.Text != "")
                    forceOperationList = forceOperationList.Where(x => x.CourseName.ToUpper().Contains(txtCourseTitle.Text.ToUpper())).ToList();

            if (forceOperationList.Count > 0 && forceOperationList != null)
                if (txtLow.Text != "")
                    forceOperationList = forceOperationList.Where(x => x.SequenceNo >= Convert.ToInt32(txtLow.Text)).ToList();

            if (forceOperationList.Count > 0 && forceOperationList != null)
                if (txtHigh.Text != "")
                    forceOperationList = forceOperationList.Where(x => x.SequenceNo <= Convert.ToInt32(txtHigh.Text)).ToList();

            if (txtStudentId.Text != null)
            {
                Student student = StudentManager.GetByRoll(txtStudentId.Text);
                if (student != null)
                    if (student.BasicInfo != null)
                        txtStudentName.Text = student.BasicInfo.FullName;
            }

            if (forceOperationList.Count > 0 && forceOperationList != null)
            {
                if (chkPriority.Checked)
                    forceOperationList = forceOperationList.OrderBy(x => x.Priority).ToList();
                gvWorkSheetGenerate.DataSource = forceOperationList;
            }
            else
                gvWorkSheetGenerate.DataSource = null;

            gvWorkSheetGenerate.DataBind();
        }
        catch(Exception Ex)
        {
            //lblMsg.Text = Ex.Message;
        }
    }

    protected void btnAutoOpen_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in gvWorkSheetGenerate.Rows)
            {
                CheckBox checkBox = (CheckBox)row.FindControl("chkIsAutoOpen");
                if (checkBox.Checked)
                {
                    HiddenField hiddenField = (HiddenField)row.FindControl("hdIsAutoOpen");
                    RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(Convert.ToInt32(hiddenField.Value));
                    registrationWorksheet.IsAutoOpen = true;
                    bool resultTrue = RegistrationWorksheetManager.Update(registrationWorksheet);
                }
                else
                {
                    HiddenField hiddenField = (HiddenField)row.FindControl("hdIsAutoOpen");
                    RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(Convert.ToInt32(hiddenField.Value));
                    registrationWorksheet.IsAutoOpen = false;
                    bool resultTrue = RegistrationWorksheetManager.Update(registrationWorksheet);
                }
            }
        }
        catch { }
    }

    protected void btnIsAutoAssign_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in gvWorkSheetGenerate.Rows)
            {
                CheckBox checkBox = (CheckBox)row.FindControl("chkIsAutoAssign");
                if (checkBox.Checked)
                {
                    HiddenField hiddenField = (HiddenField)row.FindControl("hdIsAutoAssign");
                    RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(Convert.ToInt32(hiddenField.Value));
                    registrationWorksheet.IsAutoAssign = true;
                    bool resultTrue = RegistrationWorksheetManager.Update(registrationWorksheet);
                }
                else
                {
                    HiddenField hiddenField = (HiddenField)row.FindControl("hdIsAutoAssign");
                    RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(Convert.ToInt32(hiddenField.Value));
                    registrationWorksheet.IsAutoAssign = false;
                    bool resultTrue = RegistrationWorksheetManager.Update(registrationWorksheet);
                }
            }
        }
        catch { }
    }

    protected void btnMandatory_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in gvWorkSheetGenerate.Rows)
            {
                CheckBox checkBox = (CheckBox)row.FindControl("chkIsMandatory");
                if (checkBox.Checked)
                {
                    HiddenField hiddenField = (HiddenField)row.FindControl("hdIsMandatory");
                    RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(Convert.ToInt32(hiddenField.Value));
                    registrationWorksheet.IsMandatory = true;
                    bool resultTrue = RegistrationWorksheetManager.Update(registrationWorksheet);
                }
                else
                {
                    HiddenField hiddenField = (HiddenField)row.FindControl("hdIsMandatory");
                    RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(Convert.ToInt32(hiddenField.Value));
                    registrationWorksheet.IsMandatory = false;
                    bool resultTrue = RegistrationWorksheetManager.Update(registrationWorksheet);
                }
            }
        }
        catch { }
    }

    protected void chkAutoOpenAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Checked)
            {
                chk.Text = "Unselect";
            }
            else
            {
                chk.Text = "Select";
            }

            foreach (GridViewRow row in gvWorkSheetGenerate.Rows)
            {

                CheckBox ckBox = (CheckBox)row.FindControl("chkIsAutoOpen");
                ckBox.Checked = chk.Checked;

            }
        }
        catch (Exception ex) { }
    }

    protected void chkIsAutoAssignAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Checked)
            {
                chk.Text = "Unselect";
            }
            else
            {
                chk.Text = "Select";
            }

            foreach (GridViewRow row in gvWorkSheetGenerate.Rows)
            {

                CheckBox ckBox = (CheckBox)row.FindControl("chkIsAutoAssign"); 
                ckBox.Checked = chk.Checked;

            }
        }
        catch (Exception ex) { }
    }

    protected void chkMandatoryAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Checked)
            {
                chk.Text = "Unselect";
            }
            else
            {
                chk.Text = "Select";
            }

            foreach (GridViewRow row in gvWorkSheetGenerate.Rows)
            {

                CheckBox ckBox = (CheckBox)row.FindControl("chkIsMandatory");
                ckBox.Checked = chk.Checked;

            }
        }
        catch (Exception ex) { }
    }

    #endregion
}