using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;

namespace EMS.miu.result
{
    public partial class ExamResultUnSubmit : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            if (!IsPostBack)
            {
                try
                {
                    ucProgram.LoadDropdownWithUserAccess(userObj.Id);
                    lblMsg.Text = "";
                }
                catch { }
            }
        }

        private void ClearAll()
        {
            cbxAllSelect.Checked = false;
            pnlExamUnSubmit.Visible = false;
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAll();
            lblStudentName.Visible = false;
            lblName.Visible = false;
            ddlCourse.Items.Clear();
            lblMsg.Text = string.Empty;
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));

        }
        protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAll();
            lblStudentName.Visible = false;
            lblName.Visible = false;
            ddlCourse.Items.Clear();
            lblMsg.Text = string.Empty;
        }
        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAll();
            if (ddlCourse.SelectedValue == "0")
            {
                pnlExamUnSubmit.Visible = false;
            }
            else
            {
                string course = ddlCourse.SelectedValue;
                string[] courseVersion = course.Split('_');

                int courseId = Convert.ToInt32(courseVersion[0]);
                int versionId = Convert.ToInt32(courseVersion[1]);
                int acaCalId = Convert.ToInt32(ucSession.selectedValue);
                int acaCalSection = Convert.ToInt32(courseVersion[2]);
                LoadExam(acaCalSection);
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
                        btnLoad.Visible = true;
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

        protected void cbxSelectAll_Checked(object sender, EventArgs e)
        {
            foreach (ListItem item in cblExamList.Items)
            {
                item.Selected = cbxAllSelect.Checked;
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            pnlExamUnSubmit.Visible = false;
            Student student = StudentManager.GetByRoll(txtStudentRoll.Text.Trim());
            if (student != null)
            {
                lblName.Visible = true;
                lblStudentName.Visible = true;
                lblStudentName.Text = student.BasicInfo.FullName;
            }
            ddlCourse.Items.Clear();
            lblMsg.Text = string.Empty;
            List<LogicLayer.BusinessObjects.AcademicCalenderSection> acaCalSectionList = AcademicCalenderSectionManager.GetAllByAcaCalIdStudentRoll(Convert.ToInt32(ucSession.selectedValue), txtStudentRoll.Text.Trim());
            ddlCourse.AppendDataBoundItems = true;
            ddlCourse.Items.Add(new ListItem("-Select-", "0"));
            if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
            {
                acaCalSectionList = acaCalSectionList.OrderBy(x => x.CourseID).ToList();
                foreach (LogicLayer.BusinessObjects.AcademicCalenderSection acaCalSec in acaCalSectionList)
                {
                    ddlCourse.Items.Add(new ListItem(acaCalSec.Course.Title + ":" + acaCalSec.Course.FormalCode + "(" + acaCalSec.SectionName + ")", acaCalSec.Course.CourseID + "_" + acaCalSec.Course.VersionID + "_" + acaCalSec.AcaCal_SectionID));
                }
            }
        }
        protected void btnUnSubmit_Clicked(object sender, EventArgs e)
        {
            try
            {
                string course = ddlCourse.SelectedValue;
                string[] courseVersion = course.Split('_');
                int acaCalSection = Convert.ToInt32(courseVersion[2]);

                Student studentObj = StudentManager.GetByRoll(txtStudentRoll.Text.Trim());
                StudentCourseHistory schObj = StudentCourseHistoryManager.GetAllByAcaCalSectionId(acaCalSection).Where(x => x.StudentID == studentObj.StudentID).FirstOrDefault();
                
                bool isUpdated = false;
                List<StudentCourseHistoryReplica> studentCourseHistoryReplicaList = StudentCourseHistoryReplicaManager.GetAllByCourseHistoryID(schObj.ID);
                            //is Manually grade change ?

                if (studentCourseHistoryReplicaList != null && studentCourseHistoryReplicaList.Count > 0)
                {

                }
                else
                {
                    foreach (ListItem examItem in cblExamList.Items)
                    {
                        if (examItem.Selected)
                        {
                            ExamMarkDetails examMarkDetailsObj = ExamMarkDetailsManager.GetByCourseHistoryIdExamTemplateItemId(schObj.ID, Convert.ToInt32(examItem.Value));
                            if (examMarkDetailsObj != null)
                            {
                                examMarkDetailsObj.IsFinalSubmit = false;
                                isUpdated = ExamMarkDetailsManager.Update(examMarkDetailsObj);
                            }
                        }
                    }
                }
                if (isUpdated)
                    lblMsg.Text = "Un-submit complete!";
            }
            catch { }
        }
    }
}