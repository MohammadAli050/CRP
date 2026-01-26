using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.registration
{
    public partial class UnRegistration : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            if (!IsPostBack)
            {
                ucProgram.LoadDropdownWithUserAccess(userObj.Id);
            }
        }
        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAll();
            LoadCourse(0, 0);
            lblMsg.Text = string.Empty;
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }
        protected void OnCourseSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAll();
        }
        protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAll();
            ddlCourse.Items.Clear();
            lblMsg.Text = string.Empty;
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int sessionId = Convert.ToInt32(ucSession.selectedValue);
            LoadCourse(programId, sessionId);
        }
        protected void LoadCourse(int programId, int acaCalId)
        {
            lblMsg.Text = "";
            try
            {
                List<Course> courseList = CourseManager.GetAllByAcaCalIdProgramIdFromCourseHistory(acaCalId, programId);
                if (courseList.Count > 0)
                {
                    ddlCourse.AppendDataBoundItems = true;
                    ddlCourse.DataTextField = "CourseFullInfo";
                    ddlCourse.DataValueField = "CourseID";
                    ddlCourse.DataSource = courseList;
                    ddlCourse.DataBind();
                }
                else
                {
                    ddlCourse.Items.Clear();
                }
            }
            catch { }
        }
        private void ClearAll()
        {
            GvStudentCourseHistory.Visible = false;
        }
        protected void btnLoadStudents_Click(object sender, EventArgs e)
        {
            LoadStudent();
        }
        private void LoadStudent()
        {
            lblMsg.Text = "";
            List<StudentCourseHistoryDTO> studentList = StudentCourseHistoryManager.GetAllByAcaCalIdCourseId(Convert.ToInt32(ucSession.selectedValue), Convert.ToInt32(ddlCourse.SelectedValue));
            if (studentList != null)
            {
                GvStudentCourseHistory.Visible = true;
                GvStudentCourseHistory.DataSource = studentList;
                GvStudentCourseHistory.DataBind();
            }
            else lblMsg.Text = "No Data Found";
        }
        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)sender;
                foreach (GridViewRow row in GvStudentCourseHistory.Rows)
                {
                    CheckBox ckBox = (CheckBox)row.FindControl("chkUnRegister");
                    ckBox.Checked = chk.Checked;
                }

            }
            catch (Exception ex)
            {
            }
        }
        protected void btnUnRegisterAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in GvStudentCourseHistory.Rows)
                {
                    CheckBox ckBox = (CheckBox)row.FindControl("chkUnRegister");
                    if (ckBox.Checked == true)
                    {
                        Label courseHistoryId = (Label)row.FindControl("lblStudentCourseHistoryId");
                        int studentCourseHistoryId = Convert.ToInt32(courseHistoryId.Text);
                        if (studentCourseHistoryId > 0)
                        {
                            StudentCourseHistory schObj = StudentCourseHistoryManager.GetById(studentCourseHistoryId);
                            List<LogicLayer.BusinessObjects.ClassAttendance> classAttList = ClassAttendanceManager.GetAllByStudentIdCourseIdAcaCalId(schObj.StudentID, schObj.CourseID, (int)schObj.AcaCalID);
                            List<ExamMarkDetails> examMarkList = ExamMarkDetailsManager.GetExamMarkByCourseHistoryId(schObj.ID);
                            if (classAttList.Count > 0 && examMarkList.Count > 0)
                            {
                                lblMsg.Text = "Can't un-register. Attendance or Exam Mark exists.";
                            }
                            else
                            {
                                bool isDeleted = StudentCourseHistoryManager.Delete(schObj.ID);
                                if (isDeleted)
                                {
                                    LoadStudent();
                                    lblMsg.Text = "Un-registered Successfully";
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        protected void GvUnReg_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "UnReg")
                {
                    int studentCourseHistoryId = Convert.ToInt32(e.CommandArgument);
                    if(studentCourseHistoryId > 0)
                    {
                        StudentCourseHistory schObj = StudentCourseHistoryManager.GetById(studentCourseHistoryId);
                        List<LogicLayer.BusinessObjects.ClassAttendance> classAttList = ClassAttendanceManager.GetAllByStudentIdCourseIdAcaCalId(schObj.StudentID, schObj.CourseID, (int)schObj.AcaCalID);
                        List<ExamMarkDetails> examMarkList = ExamMarkDetailsManager.GetExamMarkByCourseHistoryId(schObj.ID);
                        if (classAttList.Count > 0 && examMarkList.Count > 0)
                        {
                            lblMsg.Text = "Can't un-register. Attendance and Exam Mark exists.";
                        }
                        else if(examMarkList.Count > 0)
                        {
                            lblMsg.Text = "Can't un-register. Exam Mark exists.";
                        }
                        else if (classAttList.Count > 0)
                        {
                            lblMsg.Text = "Can't un-register. Attendance exists.";
                        }
                        else
                        {
                            bool isDeleted = StudentCourseHistoryManager.Delete(schObj.ID);
                            RegistrationWorksheet rw = RegistrationWorksheetManager.GetByStudentIdCourseIdVersionId(schObj.StudentID, schObj.CourseID, schObj.VersionID);
                            rw.IsRegistered = false;
                            RegistrationWorksheetManager.Update(rw);
                            if (isDeleted)
                            {
                                LoadStudent();
                                lblMsg.Text = "Un-registered Successfully";
                            }
                        }
                    }
                }
                else if (e.CommandName == "DelMarks")
                {
                    int studentCourseHistoryId = Convert.ToInt32(e.CommandArgument);
                    if (studentCourseHistoryId > 0)
                    {
                        StudentCourseHistory schObj = StudentCourseHistoryManager.GetById(studentCourseHistoryId);
                        List<ExamMarkDetails> examMarkList = ExamMarkDetailsManager.GetExamMarkByCourseHistoryId(schObj.ID);
                        if (examMarkList.Count > 0)
                        {
                            foreach (ExamMarkDetails examMarkDetailsObj in examMarkList)
                            {
                                bool isDeleted = false;
                                isDeleted = ExamMarkDetailsManager.Delete(examMarkDetailsObj.ExamMarkDetailId);
                                if (isDeleted)
                                {
                                    lblMsg.Text = "Exam Mark deleted.";
                                    try
                                    {
                                        LogGeneralManager.Insert(
                                                         DateTime.Now,
                                                         "",
                                                         ucSession.selectedText.ToString(),
                                                         userObj.LogInID,
                                                         "",
                                                         "",
                                                         "Course Un-Registration",
                                                         userObj.LogInID + " deleted exam mark of " + examMarkDetailsObj.ConvertedMark+ " for Course: " + schObj.CourseTitle + ", Semester: " + ucSession.selectedText.ToString(),
                                                         userObj.LogInID + " deleted exam Marks",
                                                          ((int)CommonEnum.PageName.UnRegistration).ToString(),
                                                         CommonEnum.PageName.UnRegistration.ToString(),
                                                         _pageUrl,
                                                         schObj.Roll);
                                    }
                                    catch { }
                                }
                            }
                            
                        }
                    }
                }
                else if (e.CommandName == "DelAttendance")
                {
                    int studentCourseHistoryId = Convert.ToInt32(e.CommandArgument);
                    if (studentCourseHistoryId > 0)
                    {
                        StudentCourseHistory schObj = StudentCourseHistoryManager.GetById(studentCourseHistoryId);
                        List<LogicLayer.BusinessObjects.ClassAttendance> classAttList = ClassAttendanceManager.GetAllByStudentIdCourseIdAcaCalId(schObj.StudentID, schObj.CourseID, (int)schObj.AcaCalID);
                        if (classAttList.Count > 0)
                        {
                            foreach (LogicLayer.BusinessObjects.ClassAttendance classAttendanceObj in classAttList)
                            {
                                bool isDeleted = false;
                                isDeleted = ClassAttendanceManager.Delete(classAttendanceObj.ID);
                                if (isDeleted)
                                {
                                    lblMsg.Text = "Class Attendance deleted.";
                                    try
                                    {
                                        LogGeneralManager.Insert(
                                                         DateTime.Now,
                                                         "",
                                                         ucSession.selectedText.ToString(),
                                                         userObj.LogInID,
                                                         "",
                                                         "",
                                                         "Course Un-Registration",
                                                         userObj.LogInID + " deleted attendance of " + classAttendanceObj.AttendanceDate + " for AcademicCalendarSection ID: " + classAttendanceObj.AcaCalSectionID + ", StatusID: "+classAttendanceObj.StatusID+ ", Semester: " + ucSession.selectedText.ToString(),
                                                         userObj.LogInID + " deleted class attendance",
                                                          ((int)CommonEnum.PageName.UnRegistration).ToString(),
                                                         CommonEnum.PageName.UnRegistration.ToString(),
                                                         _pageUrl,
                                                         schObj.Roll);
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
    }
}