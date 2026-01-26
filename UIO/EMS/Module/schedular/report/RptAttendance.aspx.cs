using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using System.Drawing;
public partial class Report_Attendance : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        pnlMessage.Visible = false;

        if (!IsPostBack && !IsCallback)
        {
        }
    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }

    protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(ucProgram.selectedValue);
        int acaCalId = Convert.ToInt32(ucSession.selectedValue);

        FillAcaCalSectionCombo(acaCalId, programId, "");
    }

    void FillAcaCalSectionCombo(int acaCalId, int programId, string searchKey)
    {
        try
        {
            BussinessObject.UIUMSUser userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            User user = UserManager.GetByLogInId(userObj.LogInID);
            Employee empObj = EmployeeManager.GetByPersonId(user.Person.PersonID);

            List<AcademicCalenderSection> acaCalSectionList = AcademicCalenderSectionManager.GetAll();
            if (empObj != null && empObj.EmployeeID!=2)
            {
                acaCalSectionList = acaCalSectionList.Where(x => x.TeacherOneID == empObj.EmployeeID || x.TeacherThreeID == empObj.EmployeeID || x.TeacherTwoID == empObj.EmployeeID).ToList();
            }
            if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
            {
                ddlAcaCalSection.Items.Clear();
                ddlAcaCalSection.Items.Add(new ListItem("Select", "0"));

                if (acaCalId != 0 && programId != 0)
                    acaCalSectionList = acaCalSectionList.Where(x => x.AcademicCalenderID == acaCalId && (x.ProgramID == programId)).ToList();
                else if (acaCalId == 0)
                    acaCalSectionList = acaCalSectionList.Where(x => x.ProgramID == programId).ToList();
                else if (programId == 0)
                    acaCalSectionList = acaCalSectionList.Where(x => x.AcademicCalenderID == acaCalId).ToList();

                if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
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
        catch { }
    }

    protected void GetAttendance_Click(Object sender, EventArgs e)
    {
        try
        {
            string programId = Convert.ToString(ucProgram.selectedText);
            int acaSecId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);
            string[] courseInfo = ddlAcaCalSection.SelectedItem.Text.Split(':', '(', ')');
            string semesterInfo = Convert.ToString(ucSession.selectedText);
            int colNo = Convert.ToInt32(ddlCol.SelectedItem.Value);

            if (acaSecId != 0)
            {
                List<rAttendance> list = ReportManager.GetAttendanceListByAcaCalID(acaSecId);
                List<rAttendanceClassTeacher> teacherList = ReportManager.GetAttendanceCourseTeacherByAcaCalID(acaSecId);

                ReportParameter p1 = new ReportParameter("Teacher1", teacherList[0].TeacherName + "(" + teacherList[0].Code + ")");
                ReportParameter p2 = new ReportParameter();
                ReportParameter p3 = new ReportParameter("CourseTitle", courseInfo[0]);
                ReportParameter p4 = new ReportParameter("Course", courseInfo[1]);
                ReportParameter p5 = new ReportParameter("Section", courseInfo[2]);
                ReportParameter p8 = new ReportParameter("Section2", courseInfo[3]);
                ReportParameter p6 = new ReportParameter("Trimester", semesterInfo);
                ReportParameter p7 = new ReportParameter("Program", programId);
                if (teacherList.Count > 1)
                    p2 = new ReportParameter("Teacher2", teacherList[1].TeacherName + " " + teacherList[1].Code);

                if (list.Count != 0)
                {
                    if (colNo == 0)
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/miu/schedular/report/RptAttendance.rdlc");
                    }
                    else if (colNo == 1)
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/miu/schedular/report/RptAttendance2.rdlc");
                    }
                    else
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/miu/schedular/report/RptAttendance3.rdlc");
                    }
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p2, p3, p4, p5, p6, p7, p8 });
                    ReportDataSource rds = new ReportDataSource("AttendanceDataSet", list);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    lblMessage.Text = "";
                }
                else
                {
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ShowMessage("No Data Found. Please Enter Valid Session, Program And Course");
                    return;
                }
            }
            else
            {
                ReportViewer1.LocalReport.DataSources.Clear();
                ShowMessage("Please Enter Session, Program And Course");
                return;
            }
        }
        catch (Exception ex)
        {

            lblMessage.Text = "Error Code: 01101";
        }

    }

    private void ShowMessage(string msg)
    {
        pnlMessage.Visible = true;

        lblMessage.Text = msg;
        lblMessage.ForeColor = Color.Red;

    }

}
