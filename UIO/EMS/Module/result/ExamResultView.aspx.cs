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
using Microsoft.Reporting.WebForms;

namespace EMS.miu.result
{
    public partial class ExamResultView : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            if (!IsPostBack)
            {
                try
                {
                    ucProgram.LoadDropdownWithUserAccess(userObj.Id);
                    LoadCourse(0, 0);
                    //LoadAcaCalSection(0, 0, 0);
                    LoadExam(0);
                    lblMsg.Text = "";
                }
                catch { }
            }
        }
        
        private void ClearAll()
        {
            cbxAllSelect.Checked = false;
            lblExam.Visible = false;
            cblExamList.Visible = false;
            cbxAllSelect.Visible = false;
            btnLoad.Visible = false;
            ResultView.LocalReport.DataSources.Clear();
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAll();
            lblMsg.Text = string.Empty;
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            
        }

        protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAll();
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
                if (user.Person != null)
                {
                    Employee empObj = EmployeeManager.GetByPersonId(user.Person.PersonID);

                    if (empObj != null && empObj.EmployeeTypeId == 1)
                    {
                        acaCalSectionList = acaCalSectionList.Where(x => x.TeacherOneID == empObj.EmployeeID || x.TeacherThreeID == empObj.EmployeeID || x.TeacherTwoID == empObj.EmployeeID).ToList();
                    }
                }
                if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
                {
                    //acaCalSectionList = acaCalSectionList.OrderBy(x => x.FcSectionTitle).ToList();
                    foreach (AcademicCalenderSection acaCalSec in acaCalSectionList)
                    {
                        ddlCourse.Items.Add(new ListItem(acaCalSec.Course.FormalCode + ":" + acaCalSec.Course.Title + "(" + acaCalSec.SectionName + ")", acaCalSec.Course.CourseID + "_" + acaCalSec.Course.VersionID + "_" + acaCalSec.AcaCal_SectionID));
                    }
                }

                //List<OfferedCourse> offeredCourseList = OfferedCourseManager.GetAllByProgramIdAcaCalId(programId, acaCalId);
                //offeredCourseList = offeredCourseList.Where(x => x.IsActive == true).ToList();
                //if (offeredCourseList.Count > 0 && offeredCourseList != null)
                //{
                //    foreach (OfferedCourse offeredCourse in offeredCourseList)
                //        ddlCourse.Items.Add(new ListItem(offeredCourse.CourseCode + " " + offeredCourse.CourseTitle, offeredCourse.CourseID + "_" + offeredCourse.VersionID));
                //}
            }
            catch { }
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAll();
            string course = ddlCourse.SelectedValue;
            string[] courseVersion = course.Split('_');

            int courseId = Convert.ToInt32(courseVersion[0]);
            int versionId = Convert.ToInt32(courseVersion[1]);
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);
            int acaCalSection = Convert.ToInt32(courseVersion[2]);
            LoadExam(acaCalSection);
            
            //LoadAcaCalSection(courseId, versionId, acaCalId);
        }

        //protected void LoadAcaCalSection(int courseId, int versionId, int acaCalId)
        //{
        //    try
        //    {
        //        lblMsg.Text = string.Empty;
        //        ddlAcaCalSection.Items.Clear();
        //        ddlAcaCalSection.Items.Add(new ListItem("-Select Section-", "0"));
        //        ddlAcaCalSection.AppendDataBoundItems = true;

        //        List<AcademicCalenderSection> academicCalenderSectionList = AcademicCalenderSectionManager.GetByAcaCalCourseVersion(acaCalId, courseId, versionId);
        //        if (academicCalenderSectionList.Count > 0 && academicCalenderSectionList != null)
        //        {
        //            foreach (AcademicCalenderSection academicCalenderSection in academicCalenderSectionList)
        //                ddlAcaCalSection.Items.Add(new ListItem(academicCalenderSection.SectionName, Convert.ToString(academicCalenderSection.AcaCal_SectionID)));
        //        }
        //    }
        //    catch { }
        //    finally { }
        //}

        //protected void ddlAcaCalSection_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int acaCalSection = Convert.ToInt32(ddlAcaCalSection.SelectedValue);
        //    LoadExam(acaCalSection);
        //}

        protected void LoadExam(int acaCalSection)
        {
            try
            {
                lblMsg.Text = string.Empty;
                //ddlExam.Items.Clear();
                cblExamList.Items.Clear();
                //ddlExam.Items.Add(new ListItem("-Select Exam-", "0"));
                //ddlExam.AppendDataBoundItems = true;
                AcademicCalenderSection acacalSectionObj = AcademicCalenderSectionManager.GetById(acaCalSection);
                if (acacalSectionObj != null)
                {
                    List<ExamTemplateBasicItemDetails> examList = ExamTemplateBasicItemDetailsManager.GetAll().Where(d => d.ExamTemplateMasterId == acacalSectionObj.BasicExamTemplateId).ToList();
                    if (examList.Count > 0)
                    {
                        cbxAllSelect.Visible = true;
                        cblExamList.Visible = true;
                        btnLoad.Visible = true;
                        lblExam.Visible = true;
                        cblExamList.DataSource = examList;
                        cblExamList.DataValueField = "ExamTemplateBasicItemId";
                        cblExamList.DataTextField = "ExamTemplateBasicItemName";
                        cblExamList.DataBind();
                    }
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

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int sessionId = Convert.ToInt32(ucSession.selectedValue);
            string course = ddlCourse.SelectedValue;
            string[] courseVersion = course.Split('_');

            int courseId = Convert.ToInt32(courseVersion[0]);
            int versionId = Convert.ToInt32(courseVersion[1]);
            int acaCalSectionId = Convert.ToInt32(courseVersion[2]);
            //int examTemplateItemId = Convert.ToInt32(ddlExam.SelectedValue);
            AcademicCalenderSection acalSec = AcademicCalenderSectionManager.GetById(acaCalSectionId);
            string session = ucSession.selectedText;
            string program = ucProgram.selectedText;
            //string exam = ddlExam.SelectedItem.Text;
            string section = acalSec.SectionName;
            string courseName = acalSec.Course.FormalCode + " : " + acalSec.Course.Title;

            ReportParameter p1 = new ReportParameter("Session", session);
            ReportParameter p2 = new ReportParameter("Program", program);

            ReportParameter p4 = new ReportParameter("Course", courseName);
            ReportParameter p5 = new ReportParameter("Section", section);

            List<ExamMarkNewDTO> studentList = new List<ExamMarkNewDTO>();
            foreach (ListItem examItem in cblExamList.Items)
            {
                if (examItem.Selected)
                {
                    List<ExamMarkNewDTO> studentList2 = ExamMarkDetailsManager.GetExamMarkForReport(programId, sessionId, courseId, versionId, acaCalSectionId, Convert.ToInt32(examItem.Value));
                    if (studentList2.Count != 0)
                    {
                        foreach (ExamMarkNewDTO obj in studentList2)
                        {
                            studentList.Add(obj);
                        }
                    }
                }
            }

            try
            {
                if (studentList.Count != 0)
                {
                    ResultView.LocalReport.ReportPath = Server.MapPath("~/miu/result/ExamMarkView.rdlc");
                    ResultView.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p4, p5 });
                    ReportDataSource rds = new ReportDataSource("ExamMarkList", studentList);

                    ResultView.LocalReport.DataSources.Clear();
                    ResultView.LocalReport.DataSources.Add(rds);
                    lblMsg.Text = "";
                }
                else
                {
                    lblMsg.Text = "NO Data Found.";
                    return;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

        }
    }
}