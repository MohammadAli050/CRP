using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_GradeSheetTransfer : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        PnlGradeSheet.Visible = false;
        pnlResultShow.Visible = false;
        btnTransfer.Visible = false;
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            LoadCamboBox();
            btnTransfer.Visible = false;
        }
    }

    void LoadCamboBox()
    {
        FillProgramComboBox();
        FillAcademicCalenderCombo();
        FillAcaCalSectionCombo();
    }

    void FillProgramComboBox()
    {
        try
        {
            //ddlProgram.Items.Clear();
            List<Program> programList = ProgramManager.GetAll();

            //ddlProgram.Items.Add(new ListItem("Select", "0"));
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

    void FillAcademicCalenderCombo()
    {
        try
        {
            ddlAcaCalBatch.Items.Clear();
            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll().OrderByDescending(x => x.AcademicCalenderID).ToList();

            ddlAcaCalBatch.Items.Add(new ListItem("Select", "0"));
            ddlAcaCalBatch.AppendDataBoundItems = true;

            if (academicCalenderList != null)
            {
                int count = academicCalenderList.Count;
                foreach (AcademicCalender academicCalender in academicCalenderList)
                {
                    ddlAcaCalBatch.Items.Add(new ListItem("[" + academicCalender.Code + "] " + academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                    count = academicCalender.AcademicCalenderID;
                }
            }

        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    void FillAcaCalSectionCombo()
    {
        ddlAcaCalSection.Items.Add(new ListItem("Select", "0"));
    }

    void FillAcaCalSectionCombo(int acaCalId, int programId, string searchKey)
    {
        List<AcademicCalenderSection> acaCalSectionList = AcademicCalenderSectionManager.GetAll();
        if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
        {
            ddlAcaCalSection.Items.Clear();
            ddlAcaCalSection.Items.Add(new ListItem("Select", "0"));

            if (acaCalId != 0 && programId != 0)
                acaCalSectionList = acaCalSectionList.Where(x => x.AcademicCalenderID == acaCalId && (x.ProgramID == programId)).ToList();
            else if (acaCalId == 0)
                acaCalSectionList = acaCalSectionList.Where(x => x.ProgramID == programId  ).ToList();
            else if (programId == 0)
                acaCalSectionList = acaCalSectionList.Where(x => x.AcademicCalenderID == acaCalId).ToList();

            if (acaCalSectionList.Count > 0)
            {
                List<Course> courseList = CourseManager.GetAll();
                Hashtable hashCourse = new Hashtable();
                foreach (Course course in courseList)
                    hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.Title + ":" + course.FormalCode);

                //acaCalSectionList = acaCalSectionList.OrderBy(x => x.CourseID).ThenBy(x => x.VersionID).ToList();
                Dictionary<string, string> dicAcaCalSec = new Dictionary<string, string>();
                foreach (AcademicCalenderSection acaCalSection in acaCalSectionList)
                {
                    string courseVersion = acaCalSection.CourseID.ToString() + "_" + acaCalSection.VersionID.ToString();
                    //ddlAcaCalSection.Items.Add(new ListItem(hashCourse[courseVersion] + "(" + acaCalSection.SectionName + ") ", acaCalSection.AcaCal_SectionID.ToString()));
                    try
                    {
                        dicAcaCalSec.Add(hashCourse[courseVersion] + "(" + acaCalSection.SectionName + ") ", acaCalSection.AcaCal_SectionID.ToString());
                    }
                    catch { }
                }
                var acaCalSecList = dicAcaCalSec.Where(c => c.Key.ToUpper().Contains(searchKey.ToUpper())).OrderBy(x => x.Key).ToList();
                foreach (var temp in acaCalSecList)
                    ddlAcaCalSection.Items.Add(new ListItem(temp.Key, temp.Value));
            }
        }
    }

    #endregion

    #region Event

    protected void ddlAcaCal_Change(Object sender, EventArgs e)
    {
        int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);

        FillAcaCalSectionCombo(acaCalId, programId, "");
    }

    protected void ddlProgram_Change(Object sender, EventArgs e)
    {
        int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);

        FillAcaCalSectionCombo(acaCalId, programId, "");
    }

    protected void btnLoadGradeSheet_Click(Object sender, EventArgs e)
    {
        if (ddlAcaCalBatch.SelectedValue == "0" || ddlProgram.SelectedValue == "0" || ddlAcaCalSection.SelectedValue == "0")
        {
            lblMsg.Text = "View Grade Sheet: Please Select <b>Dropdown Value First.</b>";
            return;
        }
        try
        {
            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
            Hashtable academicCalenderHash = new Hashtable();
            foreach (AcademicCalender academicCalender in academicCalenderList)
                academicCalenderHash.Add(academicCalender.AcademicCalenderID, academicCalender.Code);

            List<CourseStatus> CourseStatusList = CourseStatusManager.GetAll();
            Hashtable courseStatusHash = new Hashtable();
            foreach (CourseStatus courseStatus in CourseStatusList)
                courseStatusHash.Add(courseStatus.CourseStatusID, courseStatus.Code);

            int programId = Convert.ToInt32(ddlProgram.SelectedValue);
            int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
            int acaCalSectionId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);

            List<GradeSheet> gradeSheetList = GradeSheetManager.GetAllByAcaCalSectionId(acaCalSectionId);
            if (gradeSheetList.Count > 0 && gradeSheetList != null)
                gradeSheetList = gradeSheetList.Where(x => x.IsFinalSubmit == true && x.IsTransfer == false).ToList();

            if (gradeSheetList.Count > 0 && gradeSheetList != null)
            {
                btnTransfer.Visible = true;

                for (int i = 0; i < gradeSheetList.Count; i++)
                {
                    Student student = StudentManager.GetById(gradeSheetList[i].StudentID);
                    Person person = PersonManager.GetById(student.PersonID);

                    int currentGradeId = 999, previousGradeId = 999;
                    GradeDetails gradeDetailsCurrent = GradeDetailsManager.GetByGrade(gradeSheetList[i].ObtainGrade);
                    if (gradeDetailsCurrent != null)
                        currentGradeId = gradeDetailsCurrent.GradeId;


                    StudentCourseHistory stdCourseHistory = StudentCourseHistoryManager.GetBy(gradeSheetList[i].StudentID, gradeSheetList[i].CourseID, gradeSheetList[i].VersionID, true);
                    if (stdCourseHistory != null && stdCourseHistory.AcaCalID != acaCalId)
                    {
                        gradeSheetList[i].CourseHistoryId = stdCourseHistory.ID;
                        gradeSheetList[i].CourseHistoryGrade = stdCourseHistory.ObtainedGrade;
                        GradeDetails gradeDetailsPrevious = GradeDetailsManager.GetByGrade(stdCourseHistory.ObtainedGrade);
                        previousGradeId = gradeDetailsPrevious.GradeId;
                    }
                    else
                    {
                        gradeSheetList[i].CourseHistoryId = 0;
                        gradeSheetList[i].CourseHistoryGrade = "";
                    }



                    if (currentGradeId < previousGradeId)
                    {
                        gradeSheetList[i].IsCurrentGrade = true;
                        gradeSheetList[i].IsPreviousGrade = false;
                    }
                    else if (currentGradeId == 999 && previousGradeId == 999)
                    {
                        gradeSheetList[i].IsCurrentGrade = false;
                        gradeSheetList[i].IsPreviousGrade = false;
                    }
                    else
                    {
                        gradeSheetList[i].IsCurrentGrade = false;
                        gradeSheetList[i].IsPreviousGrade = true;
                    }
                    gradeSheetList[i].StudentRoll = student.Roll;
                    gradeSheetList[i].StudentName = person.FullName;

                    List<StudentCourseHistory> tempStudentCourseHistory = StudentCourseHistoryManager.GetAll(gradeSheetList[i].StudentID, gradeSheetList[i].CourseID, gradeSheetList[i].VersionID);
                    if (tempStudentCourseHistory.Count > 0 && tempStudentCourseHistory != null)
                    {
                        tempStudentCourseHistory = tempStudentCourseHistory.Where(x => x.AcaCalID != acaCalId).ToList();
                        if (tempStudentCourseHistory.Count > 0 && tempStudentCourseHistory != null)
                        {
                            tempStudentCourseHistory = tempStudentCourseHistory.OrderBy(x => x.AcaCalID).ToList();
                            string record = string.Empty;
                            foreach (StudentCourseHistory courseHistory in tempStudentCourseHistory)
                            {
                                if (courseHistory.ObtainedGrade != "" && courseHistory.ObtainedGrade != null)
                                {
                                    record += " " + courseHistory.ObtainedGrade + "[" + academicCalenderHash[courseHistory.AcaCalID] + "]";
                                }
                                else
                                {
                                    record += " " + courseStatusHash[courseHistory.CourseStatusID] + "[" + academicCalenderHash[courseHistory.AcaCalID] + "]";
                                }
                            }
                            gradeSheetList[i].PreviousRecord = record;
                        }
                    }
                }
            }
            else
            {
                lblMsg.Text = "<b>No Grade Submitted For this <i>" + ddlAcaCalSection.SelectedItem.Text + "</i> Course.</b>";
            }

            PnlGradeSheet.Visible = true;
            gvGradeSheet.DataSource = gradeSheetList;
            gvGradeSheet.DataBind();
        }
        catch { }
        finally { }
    }

    protected void btnShowGradeSheet_Click(object sender, EventArgs e)
    {
        if (ddlAcaCalBatch.SelectedValue == "0" || ddlProgram.SelectedValue == "0" || ddlAcaCalSection.SelectedValue == "0")
        {
            lblMsg.Text = "View Grade Sheet: Please Select <b>Dropdown Value First.</b>";
            return;
        }
        try
        {
            int acaCalSec = Convert.ToInt32(ddlAcaCalSection.SelectedValue);
            List<StudentCourseHistory> studentCourseHistoryList = StudentCourseHistoryManager.GetAllByAcaCalSectionId(acaCalSec);
            if (studentCourseHistoryList.Count > 0 && studentCourseHistoryList != null)
            {
                pnlResultShow.Visible = true;
                #region Hashing
                List<CourseStatus> courseStatusList = CourseStatusManager.GetAll();
                Hashtable courseStatusHash = new Hashtable();
                foreach (CourseStatus courseStatus in courseStatusList)
                    courseStatusHash.Add(courseStatus.CourseStatusID, courseStatus.Code);
                #endregion


                foreach (StudentCourseHistory studentCourseHistory in studentCourseHistoryList)
                {
                    Student student = StudentManager.GetById(studentCourseHistory.StudentID);
                    if (student != null)
                    {
                        Person person = PersonManager.GetById(student.PersonID);
                        if (person != null)
                        {
                            studentCourseHistory.StudentRoll = student.Roll;
                            studentCourseHistory.StudentName = person.FullName;
                        }
                    }

                    //if (studentCourseHistory.ObtainedGrade == "" || studentCourseHistory.ObtainedGrade == null)
                    //    if (courseStatusHash.ContainsKey(studentCourseHistory.CourseStatusID))
                    //        studentCourseHistory.CourseStatus = courseStatusHash[studentCourseHistory.CourseStatusID].ToString();
                }
                studentCourseHistoryList = studentCourseHistoryList.OrderBy(x => x.StudentRoll).ToList();

                gvResultShow.DataSource = studentCourseHistoryList;
                gvResultShow.DataBind();
            }
            else
            {
                gvResultShow.DataSource = null;
                gvResultShow.DataBind();
            }
        }
        catch { }
        finally { }
    }

    protected void btnTransfer_Click(Object sender, EventArgs e)
    {
        if (ddlAcaCalBatch.SelectedValue == "0" || ddlProgram.SelectedValue == "0" || ddlAcaCalSection.SelectedValue == "0")
        {
            lblMsg.Text = "View Grade Sheet: Please Select Dropdown Value First.";
            return;
        }

        int countTransfer = 0;
        int flag = 0;

        try
        {
            int acaCalSectionId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);
            AcademicCalenderSection acaCalSection = AcademicCalenderSectionManager.GetById(acaCalSectionId);
            Course course = CourseManager.GetByCourseIdVersionId(acaCalSection.CourseID, acaCalSection.VersionID);

            foreach (GridViewRow gridViewRow in gvGradeSheet.Rows)
            {
                HiddenField hfGradeSheetId = (HiddenField)gridViewRow.FindControl("hfGradeSheetId");
                HiddenField hfCourseHistoryId = (HiddenField)gridViewRow.FindControl("hfCourseHistoryId");
                CheckBox isCurrentGrade = (CheckBox)gridViewRow.FindControl("chkCurrentGrade");
                CheckBox isPreviousGrade = (CheckBox)gridViewRow.FindControl("chkPreviousGrade");

                GradeSheet gradeSheet = GradeSheetManager.GetById(Convert.ToInt32(hfGradeSheetId.Value));
                gradeSheet.IsTransfer = true;
                bool resultGradeSheetUpdate = false;
                //Find Some Value
                CourseStatus courseStatusPT = CourseStatusManager.GetByCode("Pt");
                CourseStatus courseStatusPN = CourseStatusManager.GetByCode("Pn");
                //Find Some Value
                StudentCourseHistory currentStudentCourseHistory = null;
                StudentCourseHistory previousStudentCourseHistory = null;
                currentStudentCourseHistory = StudentCourseHistoryManager.GetBy(gradeSheet.StudentID, gradeSheet.CourseID, gradeSheet.VersionID, gradeSheet.AcaCal_SectionID);
                if (hfCourseHistoryId.Value != "")
                    previousStudentCourseHistory = StudentCourseHistoryManager.GetById(Convert.ToInt32(hfCourseHistoryId.Value));

                if (isCurrentGrade.Checked)
                {
                    if (previousStudentCourseHistory != null && gradeSheet.ObtainGrade.ToUpper() != "W" && gradeSheet.ObtainGrade.ToUpper() != "I")
                    {
                        previousStudentCourseHistory.CourseStatusID = courseStatusPN.CourseStatusID;
                        previousStudentCourseHistory.IsConsiderGPA = false;
                        previousStudentCourseHistory.ModifiedBy = 100;
                        previousStudentCourseHistory.ModifiedDate = DateTime.Now;
                        bool resultPreviousCourseHistory = StudentCourseHistoryManager.Update(previousStudentCourseHistory);
                    }
                    GradeDetails gradeDetails = GradeDetailsManager.GetByGrade(gradeSheet.ObtainGrade);
                    if (gradeDetails != null)
                    {
                        currentStudentCourseHistory.ObtainedTotalMarks = gradeSheet.ObtainMarks;
                        currentStudentCourseHistory.ObtainedGPA = gradeDetails.GradePoint;
                        currentStudentCourseHistory.ObtainedGrade = gradeDetails.Grade;
                        currentStudentCourseHistory.GradeId = gradeDetails.GradeId;
                        currentStudentCourseHistory.CourseStatusID = courseStatusPT.CourseStatusID;
                        currentStudentCourseHistory.IsConsiderGPA = true;
                        currentStudentCourseHistory.CourseStatusDate = DateTime.Now;
                        currentStudentCourseHistory.CreatedBy = 99;
                        currentStudentCourseHistory.CreatedDate = DateTime.Now;
                        currentStudentCourseHistory.ModifiedBy = 100;
                        currentStudentCourseHistory.ModifiedDate = DateTime.Now;
                        bool resultCurrentStudentCourseHistory = StudentCourseHistoryManager.Update(currentStudentCourseHistory);
                        if (resultCurrentStudentCourseHistory)
                        {
                            resultGradeSheetUpdate = GradeSheetManager.Update(gradeSheet);
                            countTransfer++;
                        }
                    }
                    else
                    {
                        CourseStatus courseStatus = CourseStatusManager.GetByCode(gradeSheet.ObtainGrade);
                        if (courseStatus != null)
                        {
                            currentStudentCourseHistory.CourseStatusID = courseStatus.CourseStatusID;
                            currentStudentCourseHistory.IsConsiderGPA = false;
                            currentStudentCourseHistory.CourseStatusDate = DateTime.Now;
                            currentStudentCourseHistory.CreatedBy = 99;
                            currentStudentCourseHistory.CreatedDate = DateTime.Now;
                            currentStudentCourseHistory.ModifiedBy = 100;
                            currentStudentCourseHistory.ModifiedDate = DateTime.Now;
                            bool resultCurrentStudentCourseHistory = StudentCourseHistoryManager.Update(currentStudentCourseHistory);
                            if (resultCurrentStudentCourseHistory)
                            {
                                resultGradeSheetUpdate = GradeSheetManager.Update(gradeSheet);
                                countTransfer++;
                            }
                        }
                    }
                }
                else if (isPreviousGrade.Checked)
                {
                    GradeDetails gradeDetails = GradeDetailsManager.GetByGrade(gradeSheet.ObtainGrade);
                    if (gradeDetails != null)
                    {
                        currentStudentCourseHistory.ObtainedTotalMarks = gradeSheet.ObtainMarks;
                        currentStudentCourseHistory.ObtainedGPA = gradeDetails.GradePoint;
                        currentStudentCourseHistory.ObtainedGrade = gradeDetails.Grade;
                        currentStudentCourseHistory.GradeId = gradeDetails.GradeId;
                        currentStudentCourseHistory.CourseStatusID = courseStatusPN.CourseStatusID;
                        currentStudentCourseHistory.IsConsiderGPA = false;
                        currentStudentCourseHistory.CourseStatusDate = DateTime.Now;
                        currentStudentCourseHistory.CreatedBy = 99;
                        currentStudentCourseHistory.CreatedDate = DateTime.Now;
                        currentStudentCourseHistory.ModifiedBy = 100;
                        currentStudentCourseHistory.ModifiedDate = DateTime.Now;
                        bool resultCurrentStudentCourseHistory = StudentCourseHistoryManager.Update(currentStudentCourseHistory);
                        if (resultCurrentStudentCourseHistory)
                        {
                            resultGradeSheetUpdate = GradeSheetManager.Update(gradeSheet);
                            countTransfer++;
                        }
                    }
                    else
                    {
                        CourseStatus courseStatus = CourseStatusManager.GetByCode(gradeSheet.ObtainGrade);
                        if (courseStatus != null)
                        {
                            currentStudentCourseHistory.CourseStatusID = courseStatus.CourseStatusID;
                            currentStudentCourseHistory.IsConsiderGPA = false;
                            currentStudentCourseHistory.CourseStatusDate = DateTime.Now;
                            currentStudentCourseHistory.CreatedBy = 99;
                            currentStudentCourseHistory.CreatedDate = DateTime.Now;
                            currentStudentCourseHistory.ModifiedBy = 100;
                            currentStudentCourseHistory.ModifiedDate = DateTime.Now;
                            bool resultCurrentStudentCourseHistory = StudentCourseHistoryManager.Update(currentStudentCourseHistory);
                            if (resultCurrentStudentCourseHistory)
                            {
                                resultGradeSheetUpdate = GradeSheetManager.Update(gradeSheet);
                                countTransfer++;
                            }
                        }
                    }
                }
                else
                {
                    GradeDetails gradeDetails = GradeDetailsManager.GetByGrade(gradeSheet.ObtainGrade);
                    if (gradeDetails != null)
                    {
                        currentStudentCourseHistory.ObtainedTotalMarks = gradeSheet.ObtainMarks;
                        currentStudentCourseHistory.ObtainedGPA = gradeDetails.GradePoint;
                        currentStudentCourseHistory.ObtainedGrade = gradeDetails.Grade;
                        currentStudentCourseHistory.GradeId = gradeDetails.GradeId;
                        currentStudentCourseHistory.CourseStatusID = courseStatusPN.CourseStatusID;
                        currentStudentCourseHistory.IsConsiderGPA = false;
                        currentStudentCourseHistory.CourseStatusDate = DateTime.Now;
                        currentStudentCourseHistory.CreatedBy = 99;
                        currentStudentCourseHistory.CreatedDate = DateTime.Now;
                        currentStudentCourseHistory.ModifiedBy = 100;
                        currentStudentCourseHistory.ModifiedDate = DateTime.Now;
                        bool resultCurrentStudentCourseHistory = StudentCourseHistoryManager.Update(currentStudentCourseHistory);
                        if (resultCurrentStudentCourseHistory)
                        {
                            resultGradeSheetUpdate = GradeSheetManager.Update(gradeSheet);
                            countTransfer++;
                        }
                    }
                    else
                    {
                        CourseStatus courseStatus = CourseStatusManager.GetByCode(gradeSheet.ObtainGrade);
                        if (courseStatus != null)
                        {
                            currentStudentCourseHistory.CourseStatusID = courseStatus.CourseStatusID;
                            currentStudentCourseHistory.IsConsiderGPA = false;
                            currentStudentCourseHistory.CourseStatusDate = DateTime.Now;
                            currentStudentCourseHistory.CreatedBy = 99;
                            currentStudentCourseHistory.CreatedDate = DateTime.Now;
                            currentStudentCourseHistory.ModifiedBy = 100;
                            currentStudentCourseHistory.ModifiedDate = DateTime.Now;
                            bool resultCurrentStudentCourseHistory = StudentCourseHistoryManager.Update(currentStudentCourseHistory);
                            if (resultCurrentStudentCourseHistory)
                            {
                                resultGradeSheetUpdate = GradeSheetManager.Update(gradeSheet);
                                countTransfer++;
                            }
                        }
                    }
                }
            }
        }
        catch
        {
            flag = 1;
            lblMsg.Text = "Error: 1011";
        }
        finally
        {
            if (flag == 0 && countTransfer != 0)
            {
                lblMsg.Text = "<b>Data Transferred: </b>" + countTransfer;
                btnShowGradeSheet_Click(null, null);
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);
        string searchKey = txtSearch.Text;

        if (acaCalId == 0 || programId == 0)
        {
            lblMsg.Text = "Please Select Batch and Program.";
            return;
        }

        FillAcaCalSectionCombo(acaCalId, programId, searchKey);
    }

    protected void chkCurrentGrade_Change(object sender, EventArgs e)
    {
        CheckBox checkBoxCurrentGrade = (CheckBox)sender;
        GridViewRow gridViewRow = (GridViewRow)checkBoxCurrentGrade.NamingContainer;
        CheckBox checkBoxPreviousGrade = (CheckBox)(gridViewRow.FindControl("chkPreviousGrade"));
        Label label = (Label)(gridViewRow.FindControl("lblObtainGrade"));

        if (checkBoxCurrentGrade.Checked && label.Text != "W" && label.Text != "w" && label.Text != "I" && label.Text != "i")
            checkBoxPreviousGrade.Checked = false;
        else
            checkBoxCurrentGrade.Checked = false;
    }

    protected void chkPreviousGrade_Change(object sender, EventArgs e)
    {
        try
        {
            CheckBox checkBoxPreviousGrade = (CheckBox)sender;
            GridViewRow gridViewRow = (GridViewRow)checkBoxPreviousGrade.NamingContainer;
            CheckBox checkBoxCurrentGrade = (CheckBox)(gridViewRow.FindControl("chkCurrentGrade"));
            Label label = (Label)(gridViewRow.FindControl("lblCourseHistoryGrade"));

            if (checkBoxPreviousGrade.Checked && label.Text != "")
                checkBoxCurrentGrade.Checked = false;
            else
                checkBoxPreviousGrade.Checked = false;

            btnLoadGradeSheet_Click(null, null);
        }
        catch { }
    }

    #endregion
}