using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PreCourseAssign : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            base.CheckPage_Load();
            if (!IsPostBack)
            {
                LoadDropDown();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void LoadDropDown()
    {
        LoadBatch();
        LoadProgram();
        LoadCourse();
    }

    protected void LoadBatch()
    {
        try
        {
            ddlBatch.Items.Clear();
            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
            if (academicCalenderList.Count > 0)
            {
                academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();

                ddlBatch.Items.Add(new ListItem("-Select-", "0"));
                ddlBatch.AppendDataBoundItems = true;

                foreach (AcademicCalender academicCalender in academicCalenderList)
                    ddlBatch.Items.Add(new ListItem("[" + academicCalender.Code + "] " + academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.Code));

                academicCalenderList = academicCalenderList.Where(x => x.IsCurrent == true).ToList();
                ddlBatch.SelectedValue = academicCalenderList[0].Code;
            }
            else
            {
                lblMsg.Text = "Error: 101(Academic Calender not load)";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error: 1021";
        }
        finally { }
    }

    protected void LoadProgram()
    {
        try
        {
            ddlProgram.Items.Clear();
            List<Program> programList = ProgramManager.GetAll();

            ddlProgram.Items.Add(new ListItem("-Select-", "0"));
            ddlProgram.AppendDataBoundItems = true;

            if (programList != null)
            {
                ddlProgram.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                ddlProgram.DataTextField = "NameWithCode";
                ddlProgram.DataValueField = "Code";
                ddlProgram.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    protected void LoadCourse()
    {
        try
        {
            ddlPreMendatoryCourse.Items.Clear();
            //ddlPreMendatoryCourse.Items.Add(new ListItem("-Select-", "0"));
            ddlPreMendatoryCourse.Items.Add(new ListItem("Pre-English", "English"));
            ddlPreMendatoryCourse.Items.Add(new ListItem("Pre-Math", "Math"));

            ddlPreCourse.Items.Clear();
            ddlPreCourse.Items.Add(new ListItem("-Select-", "0_0"));
            ddlMainCourse.Items.Clear();
            ddlMainCourse.Items.Add(new ListItem("-Select-", "0_0"));

            List<Course> courseList = CourseManager.GetAll();

            if (courseList.Count > 0 && courseList != null)
            {
                //var acaCalSecList = dicAcaCalSec.Where(c => c.Key.ToUpper().Contains(searchKey.ToUpper())).OrderBy(x => x.Key).ToList();
                //courseList = courseList.Where(x => x.Title.ToUpper().Contains("math".ToUpper()) || x.Title.ToUpper().Contains("english".ToUpper())).ToList();
            }
            if (courseList.Count > 0 && courseList != null)
            {
                Dictionary<string, string> dicCourse = new Dictionary<string, string>();
                foreach (Course course in courseList)
                {
                    dicCourse.Add(course.CourseID + "_" + course.VersionID, course.Title + "[" + course.VersionCode + "]");
                }
                var allCourseList = dicCourse.Where(x => x.Value.ToUpper().Contains("math".ToUpper()) || x.Value.ToUpper().Contains("english".ToUpper())).OrderBy(x => x.Value).ToList();
                foreach (var temp in allCourseList)
                {
                    ddlPreCourse.Items.Add(new ListItem(temp.Value, temp.Key));
                    ddlMainCourse.Items.Add(new ListItem(temp.Value, temp.Key));
                }
            }
        }
        catch { }
    }

    protected bool CheckAllDropdown()
    {
        try
        {
            if (ddlProgram.SelectedValue == "0" || ddlPreCourse.SelectedValue == "0_0" || ddlMainCourse.SelectedValue == "0_0")
            {
                lblMsg.Text = "Please Select Dropdown Data";
                return false;
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    #endregion

    #region Event

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckAllDropdown())
            {
                string batchCode = ddlBatch.SelectedValue;
                string programCode = ddlProgram.SelectedValue;
                string preMandatoryCourse = ddlPreMendatoryCourse.SelectedValue;
                string[] preCourseCode = ddlPreCourse.SelectedValue.Split('_');
                string[] mainCourseCode = ddlMainCourse.SelectedValue.Split('_');
                int preCourseId = Convert.ToInt32(preCourseCode[0]);
                int preVersionId = Convert.ToInt32(preCourseCode[1]);
                int mainCourseId = Convert.ToInt32(mainCourseCode[0]);
                int mainVersionId = Convert.ToInt32(mainCourseCode[1]);
                //0 for Simple data retrieve
                List<StudentPreCourse> studentPreCourseList = StudentPreCourseManager.GetAllByParameter(0, batchCode, programCode, preMandatoryCourse, preCourseId, preVersionId, mainCourseId, mainVersionId);

                if (studentPreCourseList.Count > 0 && studentPreCourseList != null)
                    gvPreMandatoryCourse.DataSource = studentPreCourseList;
                gvPreMandatoryCourse.DataBind();
            }
        }
        catch { }
    }

    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckAllDropdown())
            {
                string batchCode = ddlBatch.SelectedValue;
                string programCode = ddlProgram.SelectedValue;
                string preMandatoryCourse = ddlPreMendatoryCourse.SelectedValue;
                string[] preCourseCode = ddlPreCourse.SelectedValue.Split('_');
                string[] mainCourseCode = ddlMainCourse.SelectedValue.Split('_');
                int preCourseId = Convert.ToInt32(preCourseCode[0]);
                int preVersionId = Convert.ToInt32(preCourseCode[1]);
                int mainCourseId = Convert.ToInt32(mainCourseCode[0]);
                int mainVersionId = Convert.ToInt32(mainCourseCode[1]);

                //1 for Data transfer admission solution to EMS
                List<StudentPreCourse> studentPreCourseList = StudentPreCourseManager.GetAllByParameter(1, batchCode, programCode, preMandatoryCourse, preCourseId, preVersionId, mainCourseId, mainVersionId);

                if (studentPreCourseList.Count > 0 && studentPreCourseList != null)
                    gvPreMandatoryCourse.DataSource = studentPreCourseList;

                gvPreMandatoryCourse.DataBind();
            }
        }
        catch { }
    }

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        foreach (GridViewRow row in gvPreMandatoryCourse.Rows)
    //        {
    //            CheckBox checkBox = (CheckBox)row.FindControl("chkPreCourse");
    //            HiddenField hiddenField = (HiddenField)row.FindControl("hdStudentPreCourseId");
    //            if (!checkBox.Checked)
    //            {
    //                bool resultDelete = StudentPreCourseManager.Delete(Convert.ToInt32(hiddenField.Value));
    //                if (resultDelete)
    //                    btnView_Click(null, null);
    //            }
    //        }
    //    }
    //    catch { }
    //}

    #endregion
}