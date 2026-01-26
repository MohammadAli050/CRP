using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ConflickCheck : BasePage
{
    #region Event

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            FillAcademicCalenderCombo();
            FillProgramCombo();
            ddlCourse.Items.Add(new ListItem("--Select--", "0"));
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        lbStudentList.Items.Clear();
        lbConflictCourse.Items.Clear();
        lbStudentCourse.Items.Clear();
        chbCourseList.Items.Clear();

        if (ddlAcaCalBatch.SelectedValue == "0" || ddlProgram.SelectedValue == "0")
        {
            lblMsg.Text = "Please Select <b>Semester</b> OR <b>Program</b>";
            return;
        }

        FillCourseCombo();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlCourse.SelectedValue != "0")
        {
            List<Course> courseList = CourseManager.GetAll();
            Hashtable hashCourse = new Hashtable();
            foreach (Course course in courseList)
                hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.FormalCode + ":" + course.Title);

            string courseVersion = ddlCourse.SelectedValue;
            string[] courseVersionId = courseVersion.Split('_');
            int courseId = Convert.ToInt32(courseVersionId[0]);
            int versionId = Convert.ToInt32(courseVersionId[1]);

            int flag = 0;
            for (int i = 0; i < chbCourseList.Items.Count; i++)
            {
                if (chbCourseList.Items[i].Value == courseVersion)
                {
                    flag = 1;
                    lblMsg.Text = "Already added into the LIST.";
                    break;
                }
            }
            if (flag == 0)
            {
                ListItem item = new ListItem(hashCourse[courseVersion].ToString(), courseId + "_" + versionId);
                item.Selected = true;
                chbCourseList.Items.Add(item);
            }
        }
    }

    protected void chbCourseList_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chbCourseList.Items.Count; i++)
        {
            if (!chbCourseList.Items[i].Selected)
            {
                chbCourseList.Items.Remove(chbCourseList.Items[i]);
            }
        }
    }

    protected void btnMatchCheck_Click(object sender, EventArgs e)
    {
        lbStudentList.Items.Clear();
        lbConflictCourse.Items.Clear();
        lbStudentCourse.Items.Clear();

        int[,] courseIdVersionId = new int[100, 2];

        for (int i = 0; i < chbCourseList.Items.Count; i++)
        {
            string[] courseVersion = (chbCourseList.Items[i].Value).Split('_');
            courseIdVersionId[i, 0] = Convert.ToInt32(courseVersion[0]);
            courseIdVersionId[i, 1] = Convert.ToInt32(courseVersion[1]);
        }
        string courseVersioCheck = "(";
        int flag = 0;
        for (int i = 0; i < chbCourseList.Items.Count; i++)
        {
            for (int j = i + 1; j < chbCourseList.Items.Count; j++)
            {
                if (flag == 1)
                    courseVersioCheck += "or ";
                flag = 1;
                //courseVersioCheck += courseIdVersionId[i][0].ToString() + "_" + courseIdVersionId[i][1].ToString();

                courseVersioCheck += "(a.CourseID = " + courseIdVersionId[i, 0].ToString() + " and a.VersionID = " + courseIdVersionId[i, 1].ToString() +
                                   " and b.CourseID = " + courseIdVersionId[j, 0].ToString() + " and b.VersionID = " + courseIdVersionId[j, 1].ToString() + ") ";
            }
        }
        courseVersioCheck += ")";
        int acaCalId = Convert.ToInt16(ddlAcaCalBatch.SelectedValue);
        int programId = Convert.ToInt16(ddlProgram.SelectedValue);
        List<RegistrationWorksheet> regWorkSheetList = RegistrationWorksheetManager.GetAllByAcaProgCourse(acaCalId, programId, courseVersioCheck);
        regWorkSheetList = regWorkSheetList.Distinct().ToList();

        Hashtable hashStudent = new Hashtable();
        if (regWorkSheetList.Count > 0)
        {
            List<Student> studentList = StudentManager.GetAll();
            if (studentList.Count > 0)
            {
                studentList = studentList.Where(x => x.ProgramID == programId).ToList();
                if (studentList.Count > 0)
                {
                    foreach (Student student in studentList)
                    {
                        hashStudent.Add(student.StudentID, student.Roll);
                    }
                }
            }
            foreach (RegistrationWorksheet regWorkSheet in regWorkSheetList)
            {
                //lbStudentList.Items.Add(new ListItem(hashStudent[regWorkSheet.StudentID].ToString(), regWorkSheet.StudentID.ToString()));
                ListItem item = new ListItem(hashStudent[regWorkSheet.StudentID].ToString(), regWorkSheet.StudentID.ToString());
                if (!lbStudentList.Items.Contains(item))
                {
                    lbStudentList.Items.Add(item);
                }
            }
        }
    }

    protected void lbStudentList_Change(object sender, EventArgs e)
    {
        lbConflictCourse.Items.Clear();
        lbStudentCourse.Items.Clear();

        int studentId = 0;
        for (int i = 0; i < lbStudentList.Items.Count; i++)
        {
            if (lbStudentList.Items[i].Selected)
            {
                studentId = Convert.ToInt16(lbStudentList.Items[i].Value);
                break;
            }
        }

        List<Course> courseList = CourseManager.GetAll();
        Hashtable hashCourse = new Hashtable();
        foreach (Course course in courseList)
            hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.FormalCode);

        int acaCalId = Convert.ToInt32(ddlAcaCalBatch.SelectedValue);
        int programId = Convert.ToInt32(ddlProgram.SelectedValue);
        List<RegistrationWorksheet> regWorkSheetList = RegistrationWorksheetManager.GetAllAutoAssignCourseByStudentID(studentId);
        if (regWorkSheetList.Count > 0)
        {
            regWorkSheetList = regWorkSheetList.Where(x => x.OriginalCalID == acaCalId && x.ProgramID == programId).ToList();
            if (regWorkSheetList.Count > 0)
            {
                foreach (RegistrationWorksheet regWorkSheet in regWorkSheetList)
                {
                    string formalCodeTitle = regWorkSheet.CourseID + "_" + regWorkSheet.VersionID;
                    ddlCourse.Items.Add(new ListItem(hashCourse[formalCodeTitle].ToString(), regWorkSheet.CourseID + "_" + regWorkSheet.VersionID));

                    lbStudentCourse.Items.Add(new ListItem(hashCourse[formalCodeTitle].ToString(), formalCodeTitle));
                }

                int courseId = 0, versionId = 0;
                for (int i = 0; i < chbCourseList.Items.Count; i++)
                {
                    string[] courseVersion = (chbCourseList.Items[i].Value).Split('_');
                    courseId = Convert.ToInt32(courseVersion[0]);
                    versionId = Convert.ToInt32(courseVersion[1]);

                    if (regWorkSheetList.Where(x => x.CourseID == courseId && x.VersionID == versionId).ToList().Count > 0)
                    {
                        lbConflictCourse.Items.Add(new ListItem(hashCourse[chbCourseList.Items[i].Value].ToString(), chbCourseList.Items[i].Value.ToString()));
                    }
                }
            }
        }
    }

    #endregion

    #region Method

    private void FillAcademicCalenderCombo()
    {
        try
        {
            ddlAcaCalBatch.Items.Clear();
            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
            if (academicCalenderList.Count > 0)
            {
                academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();

                ddlAcaCalBatch.Items.Add(new ListItem("--Select--", "0"));
                ddlAcaCalBatch.AppendDataBoundItems = true;

                foreach (AcademicCalender academicCalender in academicCalenderList)
                {
                    ddlAcaCalBatch.Items.Add(new ListItem("[" + academicCalender.Code + "] " + academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                }
            }
            else
            {
                lblMsg.Text = "Error: 101(Academic Calender not load)";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error: 101.1";
        }
        finally { }
    }

    private void FillProgramCombo()
    {
        try
        {
            ddlProgram.Items.Clear();
            List<Program> programList = ProgramManager.GetAll();

            ddlProgram.Items.Add(new ListItem("--All Program--", "0"));
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

    private void FillCourseCombo()
    {
        try
        {
            //List<RegistrationWorksheet> regWorkSheetList = RegistrationWorksheetManager.GetAll(Convert.ToInt32(ddlAcaCalBatch.SelectedValue), Convert.ToInt32(ddlProgram.SelectedValue));
            List<OfferedCourse> offeredCourseList = OfferedCourseManager.GetAllByProgramIdAcaCalId(Convert.ToInt32(ddlProgram.SelectedValue), Convert.ToInt32(ddlAcaCalBatch.SelectedValue));
            if (offeredCourseList.Count > 0)
            {

                if (offeredCourseList.Count > 0)
                {
                    //regWorkSheetList = regWorkSheetList.SelectMany(x => x.c).Select(x => x.VersionID).OrderBy(x => x.CourseID).ThenBy(x => x.VersionID).Distinct().ToList();
                    offeredCourseList = offeredCourseList.OrderBy(x => x.CourseTitle).ToList();

                    List<Course> courseList = CourseManager.GetAll();
                    Hashtable hashCourse = new Hashtable();
                    foreach (Course course in courseList)
                        hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.FormalCode + ":" + course.Title);

                    ddlCourse.Items.Clear();
                    ddlCourse.Items.Add(new ListItem("--Select--", "0"));
                    if (offeredCourseList.Count > 0)
                    {
                        foreach (OfferedCourse offeredCourse in offeredCourseList)
                        {
                            string formalCodeTitle = offeredCourse.CourseID + "_" + offeredCourse.VersionID;
                            ListItem listItem = new ListItem(hashCourse[formalCodeTitle].ToString(), offeredCourse.CourseID + "_" + offeredCourse.VersionID);

                            if(!ddlCourse.Items.Contains(listItem))
                                ddlCourse.Items.Add(listItem);
                            //string formalCodeTitle = offeredCourse.CourseID + "_" + offeredCourse.VersionID;
                            //ddlCourse.Items.Add(new ListItem(hashCourse[formalCodeTitle].ToString(), offeredCourse.CourseID + "_" + offeredCourse.VersionID));
                        }
                    }
                }
            }
            else
                lblMsg.Text = "Error 101: Empty List";
        }
        catch { lblMsg.Text = "Error 102: Exception"; }
        finally { }
    }

    #endregion
}