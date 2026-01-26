using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.registration.report
{
    public partial class RptRegistrationStatusReport : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();

            if (!IsPostBack)
            {
                lblCount.Text = "0";
                int programId = 0;// Convert.ToInt32(ucProgram.selectedValue);
                LoadCourse(programId);
                LoadTeacher();
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            LoadCourse(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void LoadCourse(int programId)
        {
            try
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Select", "0"));

                List<Course> courseList = CourseManager.GetAllByProgram(programId);
                if (courseList.Count > 0 && courseList != null)
                {
                    foreach (Course course in courseList)
                    {
                        string valueField = Convert.ToString(course.CourseID); //+"_" + course.VersionID;
                        string textField = "[" + course.FormalCode + "]-" + course.Title;
                        ddlCourse.Items.Add(new ListItem(textField, valueField));
                    }
                }
            }
            catch (Exception ex)
            {
                //lblMsg.Text = ex.Message;
            }
        }

        public void LoadTeacher()
        {
            try
            {
                ddlTeacher.Items.Clear();
                ddlTeacher.Items.Add(new ListItem("Select", "0"));

                List<Teacher> teacherList = TeacherManager.GetAll();
                if (teacherList.Count > 0 && teacherList != null)
                {
                    foreach (Teacher teacher in teacherList)
                    {
                        string valueField = Convert.ToString(teacher.TeacherId); //+"_" + course.VersionID;
                        string textField = Convert.ToString(teacher.TeacherName);
                        ddlTeacher.Items.Add(new ListItem(textField, valueField));
                    }
                }
            }
            catch (Exception ex)
            {
                //lblMsg.Text = ex.Message;
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int sessionId = Convert.ToInt32(ucSession.selectedValue);
                int courseId = Convert.ToInt32(ddlCourse.SelectedValue);
                int teacherId = Convert.ToInt32(ddlTeacher.SelectedValue);

                if (programId > 0)
                {
                    List<rptRegistrationStatus> list = RegistrationStatusReportManager.GetByCourseIdOrTeacherId(programId, sessionId, courseId, teacherId);
                    if (list != null)
                    {
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportDataSource rds = new ReportDataSource("DataSet", list);
                        ReportViewer1.LocalReport.DataSources.Add(rds);
                        ReportViewer1.LocalReport.Refresh();
                        
                    }
                }
                else
                {                   
                    return;
                }
            }
            catch (Exception exp)
            {
            }
        }
    }
}