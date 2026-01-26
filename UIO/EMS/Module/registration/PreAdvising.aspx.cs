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
using System.Drawing;
using System.Data;


public partial class PreAdvising : BasePage
{
    BussinessObject.UIUMSUser userObj = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        lblMessage.Text = "";
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        if (!IsPostBack)
        {
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            CleareGrid();
            ShowMessage("");

            if (string.IsNullOrEmpty(txtStudent.Text.Trim()))
            {
                lblMessage.Text = "Insert student Roll";
                return;
            }

            Student student = StudentManager.GetByRoll(txtStudent.Text.Trim());
            if (student != null)
            {
                if (AccessAuthentication(userObj, student.Roll.Trim()))
                {
                    SessionManager.SaveObjToSession<Student>(student, ConstantValue.Session_Student);
                    FillStudentInfo(student);
                    LoadAutoOpenCourse(student.StudentID);

                    DeptRegSetUp drs = DeptRegSetUpManager.GetByProgramId(student.ProgramID);

                    if (student.CurrentSeccionResult.CGPA <= drs.AutoPreRegCGPA1 && student.CurrentSeccionResult.CGPA >= drs.AutoPreRegCGPA2)
                        lblPreAdvisingCrLimit.Text = drs.AutoPreRegCredit1.ToString("0.00");
                    if (student.CurrentSeccionResult.CGPA < drs.AutoPreRegCGPA2 && student.CurrentSeccionResult.CGPA >= drs.AutoPreRegCGPA3)
                        lblPreAdvisingCrLimit.Text = drs.AutoPreRegCredit2.ToString("0.00");
                    if (student.CurrentSeccionResult.CGPA < drs.AutoPreRegCGPA3 && student.CurrentSeccionResult.CGPA >= 0)
                        lblPreAdvisingCrLimit.Text = drs.AutoPreRegCredit3.ToString("0.00");
                }
                else
                {
                    lblMessage.Text = "Access Permission Denied. Please contact with Administrator.";
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void FillStudentInfo(Student student)
    {
        lblProgram.Text = student.Program.ShortName;
        lblBatch.Text = student.Batch.BatchNO.ToString();
        lblName.Text = student.BasicInfo.FullName;
    }

    private void CleareGrid()
    {
        gvCoursePreRegistration.DataSource = null;
        gvCoursePreRegistration.DataBind();
    }

    private void LoadAutoOpenCourse(int studentID)
    {
        List<RegistrationWorksheet> collection = null;
        collection = RegistrationWorksheetManager.GetAllOpenCourseByStudentID(studentID);

        if (collection != null)
        {
            gvCoursePreRegistration.DataSource = collection.OrderBy(c => c.Priority).ToList();
            gvCoursePreRegistration.DataBind();
        }
    }

    protected void btnPreRegistration_Click(object sender, EventArgs e)
    {
        try
        {
            ShowMessage("");

            LinkButton btn = (LinkButton)sender;
            int id = int.Parse(btn.CommandArgument.ToString());
            RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(id);
            Student student = StudentManager.GetById(registrationWorksheet.StudentID);

            if (userObj.RoleID == 9)
            {
                if (student.IsActive == false)
                {
                    lblMessage.Text = "Your status is In Active. Please contact with Department";
                    return;
                }
                else if (student.IsBlock == true)
                {
                    lblMessage.Text = "Your status is Blocked. Please contact with Department";
                    return;
                }                
            }

            if (registrationWorksheet.IsAutoAssign == false)
            {
                if (IsAlreadyTakeThisCourse(registrationWorksheet.StudentID, registrationWorksheet.CourseID, registrationWorksheet.VersionID))
                {
                    ShowMessage("Course has been taken already. Please select others...");
                    lblMessage.Focus();
                    return;
                }
            }

            
            int countTakenCourseInRW = RegistrationWorksheetManager.CountTakenCourseInRW(registrationWorksheet.ProgramID,
                                                                                         registrationWorksheet.CourseID,
                                                                                         registrationWorksheet.VersionID,
                                                                                         registrationWorksheet.TreeMasterID);

            //# Get limit from offered_corse By CourseID , VersionID and registration semester.
            AcademicCalender registrationCalender = AcademicCalenderManager.GetActiveRegistrationCalender();

              OfferedCourse offeredCourse = OfferedCourseManager.GetBy(registrationWorksheet.ProgramID,
                                                                        registrationCalender.AcademicCalenderID,
                                                                        registrationWorksheet.TreeMasterID,
                                                                        registrationWorksheet.CourseID,
                                                                        registrationWorksheet.VersionID);
              if (offeredCourse == null)
              {
                  ShowMessage("Courses are not offered yet. ");
                  return;
              }


            //_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-
            //_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-
            List<RegistrationWorksheet> studentWsList = RegistrationWorksheetManager.GetRegistrationSessionDataByStudentID(registrationWorksheet.StudentID);
           // DeptRegSetUp drs = DeptRegSetUpManager.GetByProgramId(registrationWorksheet.ProgramID);
            decimal drsLimit = 0;

            //if (student.CurrentSeccionResult == null)
            //{
            //    drsLimit = drs.AutoPreRegCredit3;
            //}
            //else
            //{
            //    if (student.CurrentSeccionResult.CGPA <= drs.AutoPreRegCGPA1 && student.CurrentSeccionResult.CGPA >= drs.AutoPreRegCGPA2)
            //        drsLimit = drs.AutoPreRegCredit1;
            //    if (student.CurrentSeccionResult.CGPA < drs.AutoPreRegCGPA2 && student.CurrentSeccionResult.CGPA >= drs.AutoPreRegCGPA3)
            //        drsLimit = drs.AutoPreRegCredit2;
            //    if (student.CurrentSeccionResult.CGPA < drs.AutoPreRegCGPA3 && student.CurrentSeccionResult.CGPA >= 0)
            //        drsLimit = drs.AutoPreRegCredit3;
            //}
            decimal countPreCourseCr = studentWsList.Where(sws => sws.IsAutoAssign == true).ToList().Sum(ws => ws.Credits); // need to update countPreCourseCr. need to count direct by SP
            //_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-
            //_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-


            if (countTakenCourseInRW <= offeredCourse.Limit)
            {

                if (registrationWorksheet.IsAutoAssign == true)
                {
                    registrationWorksheet.IsAutoAssign = false;
                }
                else
                {
                    registrationWorksheet.IsAutoAssign = true;
                }
                registrationWorksheet.ModifiedBy = registrationWorksheet.StudentID;
                registrationWorksheet.ModifiedDate = DateTime.Now;

                if (countPreCourseCr + registrationWorksheet.Credits > drsLimit + 1 && registrationWorksheet.IsAutoAssign == true)
                {
                    lblMessage.Text = "Pre registration Credit limit exceed.";
                    lblMessage.Focus();
                    return;
                }
                else
                {
                    bool result = RegistrationWorksheetManager.UpdateForAssignCourseNew(registrationWorksheet);
                    LoadAutoOpenCourse(registrationWorksheet.StudentID);
                }
            }
            else
            {
                lblMessage.Text = "Pre registration overloded.";
                lblMessage.Focus();
            }
        }
        catch (Exception)
        {
        }
    }

    private bool IsAlreadyTakeThisCourse(int studentId, int courseId, int versionId)
    {
        bool result = false;

        List<RegistrationWorksheet> collection = new List<RegistrationWorksheet>();
        collection = RegistrationWorksheetManager.GetAllOpenCourseByStudentID(studentId);

        int count = collection.Where(c => c.CourseID == courseId && c.VersionID == versionId && c.IsAutoAssign == true).ToList().Count();
        if (count > 0)
        {
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    private void ShowMessage(string msg)
    {
        lblMessage.Text = msg;
    }

    protected void lnkBtnOpenCourse_Click(object sender, EventArgs e)
    {
        if (SessionManager.GetObjFromSession<Student>(ConstantValue.Session_Student) != null)
        {
            string redirectURL = string.Format("{0}/Student/OpenAndAssignCourse.aspx?stdId={1}", AppPath.ApplicationPath, SessionManager.GetObjFromSession<Student>(ConstantValue.Session_Student).StudentID);

           // Response.Redirect(redirectURL, "_blank", "menubar=0,scrollbars=1,width=1000,height=800,top=10");
        }
        else
        {
            lblMessage.Text = "Please load student data first.";
        }
    }

    protected void lBtnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            ShowMessage("");
            LoadAutoOpenCourse(SessionManager.GetObjFromSession<Student>(ConstantValue.Session_Student).StudentID);
        }
        catch (Exception)
        {
        }
    }

}