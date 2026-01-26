using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.student.report
{
    public partial class RptStudentExamRoutine : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            pnlMessage.Visible = false;

            if (!IsPostBack)
            {
                if (userObj.RoleID == 9)
                {
                    User user = UserManager.GetById(userObj.Id);
                    Student student = StudentManager.GetBypersonID(user.Person.PersonID);

                    txtRoll.Text = student.Roll;
                    txtRoll.ReadOnly = true;
                }

                FillAcademicCalenderCombo();
            }
        }

        private void FillAcademicCalenderCombo()
        {
            string roll = txtRoll.Text.Trim();
            try
            {
                ddlSession.Items.Clear();
                List<rAcaCalSessionListByRoll> list = AcademicCalenderManager.GetAcaCalSessionListByRoll(roll);

                ddlSession.Items.Add(new ListItem("-Select-", "0"));
                ddlSession.AppendDataBoundItems = true;

                if (list != null)
                {
                    list = list.OrderByDescending(x => x.Year).ToList();
                    int count = list.Count;
                    foreach (rAcaCalSessionListByRoll academicCalender in list)
                    {
                        ddlSession.Items.Add(new ListItem(academicCalender.TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                        count = academicCalender.AcademicCalenderID;
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        protected void buttonView_Click(object sender, EventArgs e)
        {
            string roll = txtRoll.Text.Trim();
            int acaCalId = Convert.ToInt32(ddlSession.SelectedValue);

            List<rStudentExamRoutine> list = ExamManager.GetStudentExamRoutine(roll, acaCalId);
            List<rOfferedCourseExamRoutine> OfferedCourseExamRoutine = ExamManager.GetOfferedCourseExamRoutineForStudent(roll, acaCalId);

            string session = ddlSession.SelectedItem.Text;

            ReportParameter p1 = new ReportParameter("Session", session);

            if (OfferedCourseExamRoutine.Count != 0)
            {
                StudentExamRoutine.LocalReport.ReportPath = Server.MapPath("~/miu/student/report/RptStudentExamRoutine.rdlc");
                this.StudentExamRoutine.LocalReport.SetParameters(new ReportParameter[] { p1 });
                ReportDataSource rds = new ReportDataSource("StudentExamRoutine", list);
                ReportDataSource rds2 = new ReportDataSource("OfferedCourseExamRoutineForStudent", OfferedCourseExamRoutine);

                StudentExamRoutine.LocalReport.DataSources.Clear();
                StudentExamRoutine.LocalReport.DataSources.Add(rds);
                StudentExamRoutine.LocalReport.DataSources.Add(rds2);
            }
            else
            {
                ShowMessage("NO Data Found.");
            }

        }

        private void ShowMessage(string msg)
        {
            pnlMessage.Visible = true;

            lblMessage.Text = msg;
            lblMessage.ForeColor = Color.Red;
        }
    }
}