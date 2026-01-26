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

namespace EMS.Module.result
{
    public partial class UnRegistrationSingleStudent : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            if (!IsPostBack)
            {
            }
        }

        protected void btnLoadStudents_Click(object sender, EventArgs e)
        {
            LoadStudent();
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
                            User user = UserManager.GetByLogInId(userObj.LogInID);
                            Role userRole = RoleManager.GetById(user.RoleID);
                            ExamMarkPublishAcaCalSectionRelation publishObj = ExamMarkPublishAcaCalSectionRelationManager.GetByAcacalSecId((int)schObj.AcaCalSectionID);
                            if (publishObj != null && userRole != null)
                            {
                                if (userRole.RoleName != "Admin" && userRole.RoleName != "DepControMarkEntry" && publishObj.IsPublished == true)
                                {
                                    lblMsg.Text = "Can not be Un-registered after result publish";
                                    return;
                                }
                            }

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
                    if (studentCourseHistoryId > 0)
                    {
                        StudentCourseHistory schObj = StudentCourseHistoryManager.GetById(studentCourseHistoryId);
                        User user = UserManager.GetByLogInId(userObj.LogInID);
                        Role userRole = RoleManager.GetById(user.RoleID);

                        if (schObj != null && (schObj.GradeId != null))
                        {
                            lblMsg.Text = "Can't un-register. Grade Exists.";
                        }
                        else
                        {
                            bool isDeleted = StudentCourseHistoryManager.Delete(schObj.ID);
                            RegistrationWorksheet rw = RegistrationWorksheetManager.GetByStudentIdCourseIdVersionId(schObj.StudentID, schObj.CourseID, schObj.VersionID);
                            if (rw != null)
                            {
                                rw.IsRegistered = false;
                                RegistrationWorksheetManager.Update(rw);
                            }

                            if (isDeleted)
                            {
                                LoadStudent();
                                lblMsg.Text = "Un-registered Successfully";
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

        private void ClearAll()
        {
            GvStudentCourseHistory.Visible = false;
        }

        private void LoadStudent()
        {
            lblMsg.Text = "";

            string stdRoll = txtStudentId.Text.Trim();
            Student std = StudentManager.GetByRoll(stdRoll);

            if (std != null)
            {
                List<StudentCourseHistory> studentList = StudentCourseHistoryManager.GetAllByStudentId(std.StudentID);
                if (studentList != null && studentList.Count > 0)
                {
                    GvStudentCourseHistory.Visible = true;
                }

                GvStudentCourseHistory.DataSource = studentList.OrderByDescending(o => o.AcaCalID);
                GvStudentCourseHistory.DataBind();
            }
            else
            {
                lblMsg.Text = "No Data Found";
                GvStudentCourseHistory.DataSource = null;
                GvStudentCourseHistory.DataBind();
            }


        }
    }
}