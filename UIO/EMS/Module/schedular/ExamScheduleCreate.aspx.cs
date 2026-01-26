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

public partial class ExamScheduleCreate : BasePage
{

    BussinessObject.UIUMSUser userObj = null;
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        if (!IsPostBack)
        {
            LoadComboBox();
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

        LoadCalenderType();
        //LoadAcademicCalender();
        //LoadExamScheduleSet();
        LoadProgramCombo();
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

    protected void LoadExamScheduleSet(int acaCalId)
    {
        try
        {
            ddlExamScheduleSet.Items.Clear();
            ddlExamScheduleSet.Items.Add(new ListItem("Select", "0"));
            ddlExamScheduleSet.AppendDataBoundItems = true;

            List<ExamScheduleSet> examScheduleSetList = ExamScheduleSetManager.GetAllByAcaCalId(acaCalId);

            ddlExamScheduleSet.DataSource = examScheduleSetList;
            ddlExamScheduleSet.DataValueField = "Id";
            ddlExamScheduleSet.DataTextField = "SetName";
            ddlExamScheduleSet.DataBind();
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
                    if(tempExamScheduleDayList.Count > 0)
                        ddlDay.Items.Add(new ListItem("Day" + tempExamScheduleDayList[0].DayNo + " [" + tempExamScheduleDayList[0].DayDate.ToString("dd-MMM-yyyy") + "]", tempExamScheduleDayList[0].Id.ToString()));
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
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Select", "0_0"));
            ddlCourse.AppendDataBoundItems = true;

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

    protected void clearField()
    {
        try
        {
            ddlDay.SelectedValue = "0";
            ddlTimeSlot.SelectedValue = "0";
            ddlProgram.SelectedValue = "0";
            ddlCourse.Items.Clear();
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Select", "0_0"));
            cblSection.Items.Clear();
        }
        catch { }
        finally { }
    }

    protected void LoadingExamSchedule(int id)
    {
        try
        {
            ExamSchedule examSchedule = ExamScheduleManager.GetById(id);
            if (examSchedule != null)
            {
                ddlDay.SelectedValue = examSchedule.DayId.ToString();
                ddlTimeSlot.SelectedValue = examSchedule.TimeSlotId.ToString();
                ddlProgram.SelectedValue = examSchedule.ProgramId.ToString();
                btnCreate.CommandArgument = id.ToString();
                btnCreate.Text = "Update";
                btnLoad.Text = "Cancel";
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

    protected void LoadExamScheduleData(int acaCalId, int examSetId)
    {
        try
        {
            List<ExamSchedule> examScheduleList = ExamScheduleManager.GetAllByAcaCalExamSet(acaCalId, examSetId);
            if (examScheduleList.Count > 0 && examScheduleList != null)
            {
                List<Course> courseList = CourseManager.GetAll();
                Hashtable hashCourse = new Hashtable();
                foreach (Course course in courseList)
                    hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.FormalCode + ":" + course.Title);

                List<Program> programList = ProgramManager.GetAll();
                Hashtable hashProgram = new Hashtable();
                if (programList != null)
                    foreach (Program program in programList)
                        hashProgram.Add(program.ProgramID, program.ShortName);

                List<ExamScheduleDay> examScheduleDayList = ExamScheduleDayManager.GetAllByExamSet(examSetId);
                Hashtable hashExamScheduleDay = new Hashtable();
                if (examScheduleDayList != null)
                    foreach (ExamScheduleDay examScheduleDay in examScheduleDayList)
                        hashExamScheduleDay.Add(examScheduleDay.Id, "Day" + examScheduleDay.DayNo + " [" + examScheduleDay.DayDate.ToString("dd-MMM-yyyy") + "]");

                List<ExamScheduleTimeSlot> examScheduleTimeSlotList = ExamScheduleTimeSlotManager.GetAllByExamSet(examSetId);
                Hashtable hashExamScheduleTimeSlot = new Hashtable();
                if (examScheduleTimeSlotList != null)
                    foreach (ExamScheduleTimeSlot examScheduleTimeSlot in examScheduleTimeSlotList)
                        hashExamScheduleTimeSlot.Add(examScheduleTimeSlot.Id, "Slot" + examScheduleTimeSlot.TimeSlotNo + " [" + examScheduleTimeSlot.StartTime + "-" + examScheduleTimeSlot.EndTime + "]");

                foreach (ExamSchedule examSchedule in examScheduleList)
                {
                    examSchedule.SectionList = "";
                    int flag = 0;
                    List<ExamScheduleSection> sectionList = ExamScheduleSectionManager.GetAllByExamSchedule(examSchedule.Id);
                    if (sectionList.Count > 0 && sectionList != null)
                    {
                        foreach (ExamScheduleSection examScheduleSection in sectionList)
                        {
                            if (flag == 1) examSchedule.SectionList += "-";
                            flag = 1;
                            examSchedule.SectionList += examScheduleSection.Section;
                        }
                    }

                    string courseIndex = examSchedule.CourseId.ToString() + "_" + examSchedule.VersionId.ToString();
                    examSchedule.CourseInfo = hashCourse[courseIndex] == null ? "" : hashCourse[courseIndex].ToString();
                    examSchedule.ProgramName = hashProgram[examSchedule.ProgramId] == null ? "" : hashProgram[examSchedule.ProgramId].ToString();
                    examSchedule.Day = hashExamScheduleDay[examSchedule.DayId] == null ? "" : hashExamScheduleDay[examSchedule.DayId].ToString();
                    examSchedule.TimeSlot = hashExamScheduleTimeSlot[examSchedule.TimeSlotId] == null ? "" : hashExamScheduleTimeSlot[examSchedule.TimeSlotId].ToString();
                }

                if (ddlDay.SelectedValue != "0")
                    examScheduleList = examScheduleList.Where(x => x.DayId == Convert.ToInt32(ddlDay.SelectedValue)).ToList();
                if (ddlTimeSlot.SelectedValue != "0")
                    examScheduleList = examScheduleList.Where(x => x.TimeSlotId == Convert.ToInt32(ddlTimeSlot.SelectedValue)).ToList();
                if (ddlProgram.SelectedValue != "0")
                    examScheduleList = examScheduleList.Where(x => x.ProgramId == Convert.ToInt32(ddlProgram.SelectedValue)).ToList();
                if (ddlCourse.SelectedValue != "0_0")
                    examScheduleList = examScheduleList.Where(x => (x.CourseId + "_" + x.VersionId) == ddlDay.SelectedValue).ToList();

                gvExamScheduleList.DataSource = examScheduleList;
                gvExamScheduleList.DataBind();
            }
            else
            {
                gvExamScheduleList.DataSource = null;
                gvExamScheduleList.DataBind();
            }
        }
        catch { lblMsg.Text = "Error 2201"; }
        finally
        {
            gvExamScheduleList.Visible = true;
        }
    }

    protected void ExamScheduleUpdate(int userId)
    {
        try
        {
            int id = Convert.ToInt32(btnCreate.CommandArgument);
            ExamSchedule examSchedule = ExamScheduleManager.GetById(id);
            if (examSchedule != null)
            {
                examSchedule.DayId = Convert.ToInt32(ddlDay.SelectedValue);
                examSchedule.TimeSlotId = Convert.ToInt32(ddlTimeSlot.SelectedValue);
                examSchedule.ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
                string[] course = ddlCourse.SelectedValue.Split('_');
                examSchedule.CourseId = Convert.ToInt32(course[0]);
                examSchedule.VersionId = Convert.ToInt32(course[1]);
                examSchedule.ModifiedBy = userId;
                examSchedule.ModifiedDate = DateTime.Now;

                bool resultUpdate = ExamScheduleManager.Update(examSchedule);
                if (resultUpdate)
                {
                    List<ExamScheduleSection> examScheduleSectionList = ExamScheduleSectionManager.GetAllByExamSchedule(id);
                    bool resultDelete = ExamScheduleSectionManager.DeleteByExamSchedule(id);
                    if (resultDelete || (examScheduleSectionList.Count == 0 && !resultDelete ))
                    {
                        ExamScheduleSection examScheduleSection;
                        for (int i = 0; i < cblSection.Items.Count; i++)
                        {
                            if (cblSection.Items[i].Selected)
                            {
                                examScheduleSection = new ExamScheduleSection();
                                examScheduleSection.ExamScheduleId = id;
                                examScheduleSection.Section = cblSection.Items[i].Text;
                                examScheduleSection.CreatedBy = userId;
                                examScheduleSection.CreatedDate = DateTime.Now;

                                int resultInsertSec = ExamScheduleSectionManager.Insert(examScheduleSection);
                            }//end if
                        }//end for
                    }//end if
                    clearField();
                    lblMsg.Text = "Successfully Update";
                    btnCreate.Text = "Create";
                    btnLoad.Text = "Load";
                    btnLoad_Click(null, null);
                }//end if
            }//end if
        }//end try
        catch { }
        finally { }
    }

    protected void NewExamScheduleCreate(int userId)
    {
        try
        {
            ExamSchedule examSchedule = new ExamSchedule();
            examSchedule.AcaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            examSchedule.ExamSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);
            examSchedule.ProgramId = Convert.ToInt32(ddlProgram.SelectedValue);
            string[] course = ddlCourse.SelectedValue.Split('_');
            examSchedule.CourseId = Convert.ToInt32(course[0]);
            examSchedule.VersionId = Convert.ToInt32(course[1]);
            examSchedule.DayId = Convert.ToInt32(ddlDay.SelectedValue);
            examSchedule.TimeSlotId = Convert.ToInt32(ddlTimeSlot.SelectedValue);
            examSchedule.CreatedBy = userId;
            examSchedule.CreatedDate = DateTime.Now;

            ExamSchedule tempExamSchedule = ExamScheduleManager.GetByParameters(examSchedule.AcaCalId, examSchedule.ExamSetId, examSchedule.DayId, examSchedule.TimeSlotId, examSchedule.CourseId, examSchedule.VersionId);
            if (tempExamSchedule != null)
            {
                lblMsg.Text = "Already Inserted";
                return;
            }

            int resultInsert = ExamScheduleManager.Insert(examSchedule);

            if (resultInsert > 0)
            {
                ExamScheduleSection examScheduleSection;
                for (int i = 0; i < cblSection.Items.Count; i++)
                {
                    if (cblSection.Items[i].Selected)
                    {
                        examScheduleSection = new ExamScheduleSection();
                        examScheduleSection.ExamScheduleId = resultInsert;
                        examScheduleSection.Section = cblSection.Items[i].Text;
                        examScheduleSection.CreatedBy = userId;
                        examScheduleSection.CreatedDate = DateTime.Now;

                        int resultInsertSec = ExamScheduleSectionManager.Insert(examScheduleSection);
                    }
                }
                clearField();
                btnLoad_Click(null, null);
                btnLoad.Text = "Load";
                lblMsg.Text = "Successfully Insert";

                List<ConflictStudentDTO> conflictStudentDTOList = ExamScheduleManager.GetAllByAcaCalExamSetDaySlot(examSchedule.AcaCalId, examSchedule.ExamSetId, examSchedule.DayId, examSchedule.TimeSlotId);

                if (conflictStudentDTOList.Count > 0 && conflictStudentDTOList != null)
                {
                    string msg = "Conflicted Student : ";
                    int flag = 0;
                    foreach (ConflictStudentDTO e in conflictStudentDTOList)
                    {
                        if(flag == 0)
                            msg += e.Roll + " (" + e.Course + ")";
                        else
                            msg += ", " + e.Roll + " (" + e.Course + ")";

                        flag = 1;
                    }
                    lblMsg.Text = "Successfully Created and " + msg;
                }
            }
        }
        catch { }
        finally { btnLoad_Click(null, null); }
    }

    #endregion

    #region Event

    //protected void btnBackLink_Click(Object sender, EventArgs e)
    //{
    //    try { }
    //    catch { }
    //}

    protected void CalenderType_Changed(Object sender, EventArgs e)
    {
        try
        {
            int calenderTypeId = Convert.ToInt32(ddlCalenderType.SelectedValue);
            LoadAcademicCalender(calenderTypeId);
        }
        catch { }
    }

    protected void AcademicCalender_Changed(Object sender, EventArgs e)
    {
        try
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            LoadExamScheduleSet(acaCalId);
        }
        catch { }
    }

    protected void ExamScheduleSet_Changed(Object sender, EventArgs e)
    {
        try
        {
            gvExamScheduleList.Visible = false;

            int examScheduleSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);
            LoadExamScheduleDay(examScheduleSetId);
            LoadExamScheduleTimeSlot(examScheduleSetId);
        }
        catch { }
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
            cblSection.Items.Clear();

            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            string[] course = ddlCourse.SelectedValue.Split('_');
            int courseId = Convert.ToInt32(course[0]);
            int versionId = Convert.ToInt32(course[1]);

            List<AcademicCalenderSection> acaCalSecList = AcademicCalenderSectionManager.GetByAcaCalCourseVersion(acaCalId, courseId, versionId);
            if (acaCalSecList.Count > 0 && acaCalSecList != null)
                foreach (AcademicCalenderSection acaCalSec in acaCalSecList)
                    cblSection.Items.Add(new ListItem(acaCalSec.SectionName, acaCalSec.SectionName));
        }
        catch { }
        finally { }
    }

    protected void btnCreate_Click(Object sender, EventArgs e)
    {
        try
        {
            int createdBy = 99;
            HttpCookie aCookie = Request.Cookies[ConstantValue.Cookie_Authentication];
            string loginID = userObj.LogInID;
            User user = UserManager.GetByLogInId(loginID);
            if (user != null)
                createdBy = user.User_ID;

            if (btnCreate.Text == "Create")
            {
                NewExamScheduleCreate(createdBy);
            }
            else
            {
                ExamScheduleUpdate(createdBy);
            }
        }
        catch { }
    }

    protected void btnLoad_Click(Object sender, EventArgs e)
    {
        try
        {
            if (btnLoad.Text == "Load")
            {
                int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
                int examSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);

                LoadExamScheduleData(acaCalId, examSetId);
            }
            else
            {
                clearField();
                btnLoad.Text = "Load";
                btnCreate.Text = "Create";
            }
        }
        catch { }
        finally { }
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        try
        {
            clearField();
            btnLoad.Text = "Cancel";

            LinkButton linkButton = new LinkButton();
            linkButton = (LinkButton)sender;
            int id = Convert.ToInt32(linkButton.CommandArgument);

            LoadingExamSchedule(id);
        }
        catch { }
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                LinkButton linkButton = new LinkButton();
                linkButton = (LinkButton)sender;
                int id = Convert.ToInt32(linkButton.CommandArgument);

                List<ExamScheduleSection> examScheduleSectionList = ExamScheduleSectionManager.GetAllByExamSchedule(id);
                bool resultDeleteChild = ExamScheduleSectionManager.DeleteByExamSchedule(id);
                if (resultDeleteChild || (examScheduleSectionList.Count == 0 && !resultDeleteChild))
                {
                    bool resultDelete = ExamScheduleManager.Delete(id);
                    if (resultDelete)
                    {
                        lblMsg.Text = "Delete Successful";
                        btnLoad_Click(null, null);
                    }
                }
            }
            catch { }
        }
        catch { }
    }

    protected void btnConflictCheck_Click(Object sender, EventArgs e)
    {
        try
        {
            btnLoad_Click(null, null);

            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            int examSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);
            int dayId = Convert.ToInt32(ddlDay.SelectedValue);
            int timeSlotId = Convert.ToInt32(ddlTimeSlot.SelectedValue);
            if (acaCalId == 0 || examSetId == 0 || dayId == 0 || timeSlotId == 0)
            {
                lblMsg.Text = "Please select the dropdown value";
                return;
            }

            List<ConflictStudentDTO> conflictStudentDTOList = ExamScheduleManager.GetAllByAcaCalExamSetDaySlot(acaCalId, examSetId, dayId, timeSlotId);

            if (conflictStudentDTOList.Count > 0 && conflictStudentDTOList != null)
            {
                string msg = "Conflicted Student : ";
                int flag = 0;
                foreach (ConflictStudentDTO c in conflictStudentDTOList)
                {
                    if (flag == 0)
                        msg += c.Roll + " (" + c.Course + ")";
                    else
                        msg += ", " + c.Roll + " (" + c.Course + ")";

                    flag = 1;
                }
                lblMsg.Text = msg;
            }
            else
                lblMsg.Text = "No Conflicted Student";
        }
        catch { }
        finally { }
    }

    #endregion
}