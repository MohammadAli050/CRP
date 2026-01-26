using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExamRoutine_ExamScheduleUpdate : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack)
        {
            LoadComboBox();
            if (this.Request.QueryString["ExamSchedule"] != null)
            {
                int id = int.Parse(this.Request.QueryString["ExamSchedule"]);
                btnUpdate.CommandArgument = id.ToString();
                LoadingExamSchedule(id);
            }
        }
    }

    protected void LoadComboBox()
    {
        ddlDay.Items.Clear();
        ddlDay.Items.Add(new ListItem("Select", "0"));
        ddlTimeSlot.Items.Clear();
        ddlTimeSlot.Items.Add(new ListItem("Select", "0"));
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Select", "0_0"));

        LoadProgramCombo();
    }

    protected void LoadingExamSchedule(int id)
    {
        try
        {
            ExamSchedule examSchedule = ExamScheduleManager.GetById(id);
            if (examSchedule != null)
            {
                LoadExamScheduleSet(examSchedule.ExamSetId);
                ddlDay.SelectedValue = examSchedule.DayId.ToString();
                ddlTimeSlot.SelectedValue = examSchedule.TimeSlotId.ToString();
                ddlProgram.SelectedValue = examSchedule.ProgramId.ToString();
                btnUpdate.CommandName = examSchedule.AcaCalId.ToString();
                LoadCourseCombo(examSchedule.AcaCalId, examSchedule.ProgramId);
                string course = examSchedule.CourseId + "_" + examSchedule.VersionId;
                ddlCourse.SelectedValue = course;
                LoadCourseSection(examSchedule.AcaCalId, examSchedule.CourseId, examSchedule.VersionId);

                List<ExamScheduleSection> examScheduleSectionList = ExamScheduleSectionManager.GetAllByExamSchedule(examSchedule.Id);
                if (examScheduleSectionList.Count > 0 && examScheduleSectionList != null)
                {
                    foreach (ExamScheduleSection examScheduleSection in examScheduleSectionList)
                    {
                        for (int i = 0; i < cblSection.Items.Count; i++)
                        {
                            if (cblSection.Items[i].Text == examScheduleSection.Section)
                            {
                                cblSection.Items[i].Selected = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
        catch { }
    }

    protected void LoadExamScheduleSet(int examScheduleSetId)
    {
        try
        {
            ddlExamScheduleSet.Items.Clear();
            ddlExamScheduleSet.Items.Add(new ListItem("Select", "0"));
            ddlExamScheduleSet.AppendDataBoundItems = true;

            ExamScheduleSet examScheduleSet = ExamScheduleSetManager.GetById(examScheduleSetId);
            ddlExamScheduleSet.Items.Add(new ListItem(examScheduleSet.SetName, examScheduleSet.Id.ToString()));

            ddlExamScheduleSet.SelectedValue = examScheduleSetId.ToString();
            ddlExamScheduleSet.Enabled = false;

            LoadExamScheduleDay(examScheduleSetId);
            LoadExamScheduleTimeSlot(examScheduleSetId);
        }
        catch { }
    }

    protected void LoadExamScheduleDay(int examScheduleSetId)
    {
        try
        {
            ddlDay.Items.Clear();
            ddlDay.Items.Add(new ListItem("Select", "0"));
            ddlDay.AppendDataBoundItems = true;

            List<ExamScheduleDay> examScheduleDayList = ExamScheduleDayManager.GetAllByExamSet(examScheduleSetId);

            ExamScheduleSet examScheduleSet = ExamScheduleSetManager.GetById(examScheduleSetId);
            if (examScheduleSet != null)
            {
                for (int i = 1; i <= examScheduleSet.TotalDay; i++)
                {
                    List<ExamScheduleDay> tempExamScheduleDayList = examScheduleDayList.Where(x => x.DayNo == i).ToList();
                    if (tempExamScheduleDayList.Count > 0)
                        ddlDay.Items.Add(new ListItem("Day" + tempExamScheduleDayList[0].DayNo + " [" + tempExamScheduleDayList[0].DayDate.ToString("dd-MMM-yyy") + "]", tempExamScheduleDayList[0].Id.ToString()));
                    else
                        ddlDay.Items.Add(new ListItem("Day" + i.ToString(), "0"));
                }
            }
        }
        catch { }
    }

    protected void LoadExamScheduleTimeSlot(int examScheduleSetId)
    {
        try
        {
            ddlTimeSlot.Items.Clear();
            ddlTimeSlot.Items.Add(new ListItem("Select", "0"));

            List<ExamScheduleTimeSlot> examScheduleTimeSlotList = ExamScheduleTimeSlotManager.GetAllByExamSet(examScheduleSetId);

            ExamScheduleSet examScheduleSet = ExamScheduleSetManager.GetById(examScheduleSetId);
            if (examScheduleSet != null)
            {
                for (int i = 1; i <= examScheduleSet.TotalTimeSlot; i++)
                {
                    List<ExamScheduleTimeSlot> tempExamScheduleTimeSlotList = examScheduleTimeSlotList.Where(x => x.TimeSlotNo == i).ToList();
                    if (tempExamScheduleTimeSlotList.Count > 0)
                        foreach (ExamScheduleTimeSlot e in tempExamScheduleTimeSlotList)
                            ddlTimeSlot.Items.Add(new ListItem("Slot" + e.TimeSlotNo + " [" + e.StartTime + "-" + e.EndTime, e.Id.ToString()));
                    else
                        ddlTimeSlot.Items.Add(new ListItem("Slot" + i, "0"));
                }
            }
        }
        catch { }
    }

    protected void LoadProgramCombo()
    {
        try
        {
            ddlProgram.Items.Clear();
            List<Program> programList = ProgramManager.GetAll();

            ddlProgram.Items.Add(new ListItem("Select", "0"));
            ddlProgram.AppendDataBoundItems = true;

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
            List<AcademicCalenderSection> academicCalenderSectionList = AcademicCalenderSectionManager.GetAllByAcaCalProgram(acaCalId, programId);
            if (academicCalenderSectionList.Count > 0 && academicCalenderSectionList != null)
            {
                List<Course> courseList = CourseManager.GetAllByProgram(programId);
                Hashtable hashCourse = new Hashtable();
                foreach (Course course in courseList)
                    hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.FormalCode + ":" + course.Title);

                Dictionary<string, string> dicCourse = new Dictionary<string, string>();
                foreach (AcademicCalenderSection acaCalSec in academicCalenderSectionList)
                {
                    string formalCodeTitle = acaCalSec.CourseID + "_" + acaCalSec.VersionID;
                    try
                    {
                        dicCourse.Add(acaCalSec.CourseID + "_" + acaCalSec.VersionID, hashCourse[formalCodeTitle].ToString());
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

    protected void LoadCourseSection(int acaCalId, int courseId, int versionId)
    {
        try
        {
            cblSection.Items.Clear();
            List<AcademicCalenderSection> acaCalSecList = AcademicCalenderSectionManager.GetByAcaCalCourseVersion(acaCalId, courseId, versionId);
            if (acaCalSecList.Count > 0 && acaCalSecList != null)
                foreach (AcademicCalenderSection acaCalSec in acaCalSecList)
                    cblSection.Items.Add(new ListItem(acaCalSec.SectionName, acaCalSec.SectionName));
        }
        catch { }
    }

    #endregion

    #region Event

    protected void btnBackLink_Click(Object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("ExamScheduleDayList.aspx");
        }
        catch { }
    }

    protected void btnUpdate_Click(Object sender, EventArgs e)
    {
        try
        {
            int modifiedBy = 99;
            HttpCookie aCookie = Request.Cookies[ConstantValue.Cookie_Authentication];
            string uid = aCookie["UserName"];
            User user = UserManager.GetByLogInId(uid);
            if (user != null)
                modifiedBy = user.User_ID;

            int id = Convert.ToInt32(btnUpdate.CommandArgument);
            ExamSchedule examSchedule = ExamScheduleManager.GetById(id);
            if (examSchedule != null)
            {
                examSchedule.DayId = Convert.ToInt32(ddlDay.SelectedValue);
                examSchedule.TimeSlotId = Convert.ToInt32(ddlTimeSlot.SelectedValue);
                examSchedule.ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
                string[] course = ddlCourse.SelectedValue.Split('_');
                examSchedule.CourseId = Convert.ToInt32(course[0]);
                examSchedule.VersionId = Convert.ToInt32(course[1]);
                examSchedule.ModifiedBy = modifiedBy;
                examSchedule.ModifiedDate = DateTime.Now;

                bool resultUpdate = ExamScheduleManager.Update(examSchedule);
                if (resultUpdate)
                {
                    bool resultDelete = ExamScheduleSectionManager.DeleteByExamSchedule(id);
                    if (resultDelete)
                    {
                        ExamScheduleSection examScheduleSection;
                        for (int i = 0; i < cblSection.Items.Count; i++)
                        {
                            if (cblSection.Items[i].Selected)
                            {
                                examScheduleSection = new ExamScheduleSection();
                                examScheduleSection.ExamScheduleId = id;
                                examScheduleSection.Section = cblSection.Items[i].Text;
                                examScheduleSection.CreatedBy = modifiedBy;
                                examScheduleSection.CreatedDate = DateTime.Now;

                                int resultInsertSec = ExamScheduleSectionManager.Insert(examScheduleSection);
                            }
                        }
                    }
                }
            }
        }
        catch { }
    }

    protected void Program_Changed(Object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(btnUpdate.CommandName);
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
            cblSection.Items.Clear();

            int acaCalId = Convert.ToInt32(btnUpdate.CommandName);
            string[] course = ddlCourse.SelectedValue.Split('_');
            int courseId = Convert.ToInt32(course[0]);
            int versionId = Convert.ToInt32(course[1]);

            LoadCourseSection(acaCalId, courseId, versionId);
        }
        catch { }
        finally { }
    }

    #endregion
}