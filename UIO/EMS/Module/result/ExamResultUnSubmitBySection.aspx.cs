using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;

namespace EMS.miu.result
{
    public partial class ExamResultUnSubmitBySection : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            if (!IsPostBack)
            {
                if (userObj != null)
                    ucProgram.LoadDropdownWithUserAccess(userObj.Id);
                LoadCourse(0, 0);
                LoadAcaCalSection(0, 0, 0);
                lblMsg.Text = "";
            }
        }
        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            pnlExamUnSubmit.Visible = false;
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
        {
            pnlExamUnSubmit.Visible = false;
            lblMsg.Text = string.Empty;
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int sessionId = Convert.ToInt32(ucSession.selectedValue);
            LoadCourse(programId, sessionId);
        }

        protected void LoadCourse(int programId, int acaCalId)
        {
            try
            {
                lblMsg.Text = string.Empty;
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("-Select Course-", "0"));
                ddlCourse.AppendDataBoundItems = true;

                BussinessObject.UIUMSUser userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
                List<AcademicCalenderSection> acaCalSectionList = AcademicCalenderSectionManager.GetAllByAcaCalProgram(acaCalId, programId);

                User user = UserManager.GetByLogInId(userObj.LogInID);
                Role userRole = RoleManager.GetById(user.RoleID);
                if (user.Person != null)
                {
                    Employee empObj = EmployeeManager.GetByPersonId(user.Person.PersonID);

                    if (empObj != null && empObj.EmployeeTypeId == 1 && userRole.RoleName != "Admin" && userRole.RoleName != "DepControMarkEntry")
                    {
                        acaCalSectionList = acaCalSectionList.Where(x => x.TeacherOneID == empObj.EmployeeID || x.TeacherThreeID == empObj.EmployeeID || x.TeacherTwoID == empObj.EmployeeID).ToList();
                    }
                }
                if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
                {
                    var courseList = acaCalSectionList.Select(m => new { m.CourseID, m.VersionID }).Distinct().ToList();
                    foreach (var c in courseList)
                    {
                        Course courseObj = CourseManager.GetByCourseIdVersionId(c.CourseID, c.VersionID);
                        ddlCourse.Items.Add(new ListItem(courseObj.FormalCode + " " + courseObj.Title, c.CourseID + "_" + c.VersionID));
                    }
                }
            }
            catch { }
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlExamUnSubmit.Visible = false;
            string course = ddlCourse.SelectedValue;
            string[] courseVersion = course.Split('_');

            int courseId = Convert.ToInt32(courseVersion[0]);
            int versionId = Convert.ToInt32(courseVersion[1]);
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);
            LoadAcaCalSection(courseId, versionId, acaCalId);
        }

        protected void LoadAcaCalSection(int courseId, int versionId, int acaCalId)
        {
            try
            {
                lblMsg.Text = string.Empty;
                ddlAcaCalSection.Items.Clear();
                ddlAcaCalSection.Items.Add(new ListItem("-Select Section-", "0"));
                ddlAcaCalSection.AppendDataBoundItems = true;

                List<AcademicCalenderSection> academicCalenderSectionList = AcademicCalenderSectionManager.GetByAcaCalCourseVersion(acaCalId, courseId, versionId);
                if (academicCalenderSectionList.Count > 0 && academicCalenderSectionList != null)
                {
                    foreach (AcademicCalenderSection academicCalenderSection in academicCalenderSectionList)
                        ddlAcaCalSection.Items.Add(new ListItem(academicCalenderSection.SectionName, Convert.ToString(academicCalenderSection.AcaCal_SectionID)));
                }
            }
            catch { }
            finally { }
        }
        protected void cbxSelectAll_Checked(object sender, EventArgs e)
        {
            foreach (ListItem item in cblExamList.Items)
            {
                item.Selected = cbxAllSelect.Checked;
            }
        }
        protected void LoadExam(int acaCalSection)
        {
            try
            {
                lblMsg.Text = string.Empty;
                cblExamList.Items.Clear();
                AcademicCalenderSection acacalSectionObj = AcademicCalenderSectionManager.GetById(acaCalSection);
                if (acacalSectionObj != null)
                {
                    List<ExamTemplateBasicItemDetails> examList = ExamTemplateBasicItemDetailsManager.GetAll().Where(d => d.ExamTemplateMasterId == acacalSectionObj.BasicExamTemplateId).ToList();
                    if (examList.Count > 0)
                    {
                        cbxAllSelect.Visible = true;
                        cblExamList.Visible = true;
                        //btnLoad.Visible = true;
                        lblExam.Visible = true;
                        cblExamList.DataSource = examList;
                        cblExamList.DataValueField = "ExamTemplateBasicItemId";
                        cblExamList.DataTextField = "ExamTemplateBasicItemName";
                        cblExamList.DataBind();
                        pnlExamUnSubmit.Visible = true;
                    }
                }
            }
            catch { }
        }
        protected void ddlAcaCalSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            int acaCalSectionId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);
            LoadExam(acaCalSectionId);
        }
        protected void btnConfirm_Clicked(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();
            int acaCalSection = Convert.ToInt32(ddlAcaCalSection.SelectedValue);
            ExamMarkPublishAcaCalSectionRelation examMarkPublishObj = ExamMarkPublishAcaCalSectionRelationManager.GetByAcacalSecId(acaCalSection);
            examMarkPublishObj.IsPublished = false;
            examMarkPublishObj.IsFinalSubmit = false;
            bool isupdated = ExamMarkPublishAcaCalSectionRelationManager.Update(examMarkPublishObj);
            if (isupdated)
                UnSubmitProcess();
        }
        private void UnSubmitProcess()
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int sessionId = Convert.ToInt32(ucSession.selectedValue);
            string course = ddlCourse.SelectedValue;
            string[] courseVersion = course.Split('_');
            int courseId = Convert.ToInt32(courseVersion[0]);
            int versionId = Convert.ToInt32(courseVersion[1]);
            int acaCalSection = Convert.ToInt32(ddlAcaCalSection.SelectedValue);
            int counter = 0;
            foreach (ListItem examItem in cblExamList.Items)
            {
                int examTemplateItemId = Convert.ToInt32(examItem.Value);
                if (examItem.Selected)
                {
                    List<ExamMarkNewDTO> studentList = ExamMarkDetailsManager.GetByExamMarkDtoByParameter(programId, sessionId, courseId, versionId, acaCalSection, examTemplateItemId);
                    foreach (ExamMarkNewDTO student in studentList)
                    {
                        bool isUpdated = false;
                        Student studentObj = StudentManager.GetByRoll(student.StudentRoll);
                        StudentCourseHistory schObj = StudentCourseHistoryManager.GetAllByAcaCalSectionId(acaCalSection).Where(x => x.StudentID == studentObj.StudentID).FirstOrDefault();
                        
                        if (schObj != null)
                        {
                            List<StudentCourseHistoryReplica> studentCourseHistoryReplicaList = StudentCourseHistoryReplicaManager.GetAllByCourseHistoryID(schObj.ID);
                            //is Manually grade change ?

                            if (studentCourseHistoryReplicaList != null && studentCourseHistoryReplicaList.Count > 0)
                            {

                            }
                            else
                            {
                                ExamMarkDetails examMarkDetailsObj = ExamMarkDetailsManager.GetByCourseHistoryIdExamTemplateItemId(schObj.ID, Convert.ToInt32(examItem.Value));
                                if (examMarkDetailsObj != null && examMarkDetailsObj.IsFinalSubmit != false)
                                {
                                    examMarkDetailsObj.IsFinalSubmit = false;
                                    isUpdated = ExamMarkDetailsManager.Update(examMarkDetailsObj);
                                    if (isUpdated)
                                        counter++;
                                }
                                //if (schObj.ObtainedGPA != null || schObj.ObtainedGrade != null || schObj.ObtainedTotalMarks != null || schObj.GradeId != null)
                                //{
                                //    schObj.ObtainedTotalMarks = null;
                                //    schObj.ObtainedGPA = null;
                                //    schObj.ObtainedGrade = null;
                                //    schObj.GradeId = null;
                                //    schObj.ModifiedBy = userObj.Id;
                                //    schObj.ModifiedDate = DateTime.Now;
                                //    schObj.CourseStatusID = 9;
                                //    bool result = StudentCourseHistoryManager.Update(schObj);

                                //}
                            }
                        }

                    }
                }
            }
            lblMsg.Text = counter + " item un-submitted!";
        }
        protected void btnUnSubmit_Clicked(object sender, EventArgs e)
        {
            try
            {
                int acaCalSection = Convert.ToInt32(ddlAcaCalSection.SelectedValue);
                ExamMarkPublishAcaCalSectionRelation examMarkPublishObj = new ExamMarkPublishAcaCalSectionRelation();
                examMarkPublishObj = ExamMarkPublishAcaCalSectionRelationManager.GetByAcacalSecId(acaCalSection);
                if (examMarkPublishObj != null && examMarkPublishObj.IsPublished == true)
                {
                    ModalPopupExtender1.Show();
                }
                else
                {
                    if (examMarkPublishObj != null)
                    {
                        examMarkPublishObj.IsFinalSubmit = false;
                        ExamMarkPublishAcaCalSectionRelationManager.Update(examMarkPublishObj);
                    }
                    UnSubmitProcess();
                }
            }
            catch { }
        }
    }
}