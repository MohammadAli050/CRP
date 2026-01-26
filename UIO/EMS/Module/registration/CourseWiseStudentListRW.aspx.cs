using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CourseWiseStudentListRW : BasePage
{

    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            base.CheckPage_Load();
            if (!IsPostBack)
            {
                LoadComboBox();
            }
        }
        catch { }
        finally { }
    }

    protected void LoadComboBox()
    {
        try
        {
            LoadCalenderType();
            LoadProgramCombo();

            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Select", "0_0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("All", "0"));
        }
        catch { }
        finally { }
    }

    protected void LoadCalenderType()
    {
        try
        {
            ddlCalenderType.Items.Clear();
            //ddlCalenderType.Items.Add(new ListItem("Select", "0"));
            //ddlCalenderType.AppendDataBoundItems = true;

            List<CalenderUnitMaster> calenderUnitMasterList = CalenderUnitMasterManager.GetAll();

            if (calenderUnitMasterList.Count > 0 && calenderUnitMasterList != null)
            {
                ddlCalenderType.DataValueField = "CalenderUnitMasterID";
                ddlCalenderType.DataTextField = "Name";
                ddlCalenderType.DataSource = calenderUnitMasterList;
                ddlCalenderType.DataBind();
            }
        }
        catch { }
        finally
        {
            int calenderTypeId = Convert.ToInt32(ddlCalenderType.SelectedValue);
            LoadAcademicCalender(calenderTypeId);
        }
    }

    protected void LoadAcademicCalender(int calenderTypeId)
    {
        try
        {
            ddlAcademicCalender.Items.Clear();
            ddlAcademicCalender.Items.Add(new ListItem("Select", "0"));
            ddlAcademicCalender.AppendDataBoundItems = true;

            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll(calenderTypeId);

            if (academicCalenderList.Count > 0 && academicCalenderList != null)
            {
                foreach (AcademicCalender academicCalender in academicCalenderList)
                    ddlAcademicCalender.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));

                academicCalenderList = academicCalenderList.Where(x => x.IsActiveRegistration == true).ToList();
                ddlAcademicCalender.SelectedValue = academicCalenderList[0].AcademicCalenderID.ToString();

                AcademicCalender_Changed(null, null);
            }
        }
        catch { }
    }

    protected void LoadProgramCombo()
    {
        try
        {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Add(new ListItem("Select", "0"));
            ddlProgram.AppendDataBoundItems = true;

            List<Program> programList = ProgramManager.GetAll();

            if (programList != null)
            {
                ddlProgram.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                ddlProgram.DataValueField = "ProgramID";
                ddlProgram.DataTextField = "ShortName";
                ddlProgram.DataBind();
            }
        }
        catch (Exception ex) { }
        finally { }
    }

    protected void LoadCourseCombo(int acaCalId, int programId)
    {
        try
        {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Select", "0_0"));
            ddlCourse.AppendDataBoundItems = true;

            List<OfferedCourse> offeredCourseList = OfferedCourseManager.GetAllByProgramIdAcaCalId(programId, acaCalId);
            if (offeredCourseList.Count > 0 && offeredCourseList != null)
            {
                List<Course> courseList = CourseManager.GetAllByProgram(programId);
                Hashtable hashCourse = new Hashtable();
                foreach (Course course in courseList)
                    hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.FormalCode + ":" + course.Title);

                Dictionary<string, string> dicCourse = new Dictionary<string, string>();
                foreach (OfferedCourse offeredCourse in offeredCourseList)
                {
                    string formalCodeTitle = offeredCourse.CourseID + "_" + offeredCourse.VersionID;
                    try
                    {
                        dicCourse.Add(offeredCourse.CourseID + "_" + offeredCourse.VersionID, hashCourse[formalCodeTitle].ToString());
                    }
                    catch { }
                }
                var allCourseList = dicCourse.OrderBy(x => x.Value).ToList();
                foreach (var temp in allCourseList)
                    ddlCourse.Items.Add(new ListItem(temp.Value, temp.Key));
            }
        }
        catch { }
        finally { }
    }

    protected void LoadCourseSectionCombo(int acaCalId, int courseId, int versionId)
    {
        try
        {
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("All", "0"));
            ddlSection.AppendDataBoundItems = true;

            List<AcademicCalenderSection> acaCalSecList = AcademicCalenderSectionManager.GetByAcaCalCourseVersion(acaCalId, courseId, versionId);

            if (acaCalSecList != null)
            {
                ddlSection.DataSource = acaCalSecList.OrderBy(d => d.SectionName).ToList();
                ddlSection.DataValueField = "AcaCal_SectionID";
                ddlSection.DataTextField = "SectionName";
                ddlSection.DataBind();
            }
        }
        catch (Exception ex) { }
        finally { }
    }

    #endregion

    #region Event

    protected void CalenderType_Changed(Object sender, EventArgs e)
    {
        try
        {
            LoadAcademicCalender(Convert.ToInt32(ddlCalenderType.SelectedValue));
        }
        catch{}
        finally{}
    }
    
    protected void AcademicCalender_Changed(Object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);

            LoadCourseCombo(acaCalId, programId);
        }
        catch { }
        finally { }
    }

    protected void Program_Changed(Object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);

            LoadCourseCombo(acaCalId, programId);
        }
        catch { }
        finally { }
    }

    protected void Course_Changed(Object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            string[] courseIndex = ddlCourse.SelectedValue.Split('_');
            int courseId = Convert.ToInt32(courseIndex[0]);
            int versionId = Convert.ToInt32(courseIndex[1]);

            LoadCourseSectionCombo(acaCalId, courseId, versionId);
        }
        catch { }
        finally { }
    }

    protected void btnLoad_Click(Object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            int programId = Convert.ToInt32(ddlProgram.SelectedValue);
            string[] courseIndex = ddlCourse.SelectedValue.Split('_');
            int courseId = Convert.ToInt32(courseIndex[0]);
            int versionId = Convert.ToInt32(courseIndex[1]);
            int sectionId = Convert.ToInt32(ddlSection.SelectedValue);

            List<WorkSheetCourseHistoryDTO> studentList = OfferedCourseManager.GetStudentByProgramCourseVersionSection(acaCalId, programId, courseId, versionId, sectionId);

            if (studentList.Count > 0 && studentList != null)
            {
                gvStudentList.DataSource = studentList;
                gvStudentList.DataBind();
            }
            else
            {
                gvStudentList.DataSource = null;
                gvStudentList.DataBind();
            }
        }
        catch { }
        finally { }
    }

    #endregion
}