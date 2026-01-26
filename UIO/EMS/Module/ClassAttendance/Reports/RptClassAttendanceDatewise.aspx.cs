using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.ClassAttendance.Reports
{
    public partial class RptClassAttendanceDatewise : BasePage
    {
        int userId = 0;
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
            User userObj = UserManager.GetByLogInId(loginID);
            if (userObj != null)
                userId = userObj.User_ID;

            if (!IsPostBack)
            {
                SetUserInfoInSession();
                ucProgram.LoadDropdownWithUserAccess(userId);
            }

        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            Program prog = ProgramManager.GetById(Convert.ToInt32(ucProgram.selectedValue));
            AcademicCalender acal = AcademicCalenderManager.GetIsCurrent(prog.CalenderUnitMasterID);
            string startDate = acal.StartDate.ToString("dd/MM/yyyy");
            txtAttendanceFromDate.Text = startDate;
            string endDate = acal.EndDate.ToString("dd/MM/yyyy");
            txtAttendanceToDate.Text = endDate;
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
                Role userRole = RoleManager.GetById(user.RoleID);
                List<AcademicCalenderSection> acaCalSectionList = AcademicCalenderSectionManager.GetAll();
                if (user.Person != null)
                {
                    Employee empObj = EmployeeManager.GetByPersonId(user.Person.PersonID);

                    if (empObj != null && userRole.RoleName != "Admin" && userRole.RoleName != "Coordinator")
                    {
                        acaCalSectionList = acaCalSectionList.Where(x => x.TeacherOneID == empObj.EmployeeID || x.TeacherThreeID == empObj.EmployeeID || x.TeacherTwoID == empObj.EmployeeID).ToList();
                    }
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
        protected void SetUserInfoInSession()
        {
            try
            {
                int employeeId = 0;
                //HttpCookie aCookie = Request.Cookies[ConstantValue.Cookie_Authentication];
                //string uid = aCookie["UserName"];
                //string pwd = aCookie["UserPassword"];

                string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();

                User user = UserManager.GetByLogInId(loginID);
                if (user != null)
                {
                    Role role = RoleManager.GetById(user.RoleID);
                    if (role != null)
                    {
                        Session["Role"] = role.RoleName;
                    }
                    if (user.Person != null)
                    {
                        if (user.Person.Employee != null)
                            employeeId = user.Person.Employee.EmployeeID;
                    }
                }
            }
            catch { }
        }


        protected void btnLoad_Click(object sender, EventArgs e)
        {
            ClassAttendance.LocalReport.DataSources.Clear();
            ClassAttendance.LocalReport.Refresh();
            if (ddlAcaCalSection.SelectedItem.Value == "0")
            {
                return;
            }
            int acaCalSectionId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);
            DateTime attendanceFromDate = DateTime.ParseExact(txtAttendanceFromDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);
            DateTime attendanceToDate = DateTime.ParseExact(txtAttendanceToDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);

            try
            {
                List<rClassAttendance> list = ClassAttendanceManager.GetAttendanceReportDatewiseByAcaCalSection(acaCalSectionId, attendanceFromDate, attendanceToDate);

                if (list != null && list.Count > 0)
                {
                    list = list.OrderBy(x => x.Roll).ToList();
                    AcademicCalenderSection acs = AcademicCalenderSectionManager.GetById(acaCalSectionId);


                    string fileName = "Class_Attendance_" + acs.FcSectionTitle;

                    ClassAttendance.LocalReport.ReportPath = Server.MapPath("~/miu/ClassAttendance/Reports/RptAttendanceDatewise.rdlc");
                    ReportDataSource rds = new ReportDataSource("AttendanceDataSet", list);

                    ReportParameter[] parameters = new ReportParameter[7];
                    parameters[0] = new ReportParameter("CourseSectionName", acs.SectionName);
                    parameters[1] = new ReportParameter("SessionName", AcademicCalenderManager.GetById(acs.AcademicCalenderID).FullCode);
                    parameters[2] = new ReportParameter("TeacherName", LoadTeacherNames(acs));
                    parameters[3] = new ReportParameter("CourseName", acs.Course.Title);
                    parameters[4] = new ReportParameter("CourseCode", acs.Course.FormalCode);
                    parameters[5] = new ReportParameter("Program", acs.Program.ShortName);
                    parameters[6] = new ReportParameter("PrintDate", DateTime.Now.ToString("dd/MM/yyyy"));
                    ClassAttendance.LocalReport.DataSources.Clear();
                    ClassAttendance.LocalReport.SetParameters(parameters);
                    ClassAttendance.LocalReport.DataSources.Add(rds);

                    ClassAttendance.LocalReport.DisplayName = fileName;

                    ShowMessage("", Color.Red);
                }
                else
                {
                    ShowMessage("No Data Found", Color.Red);
                }


            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, Color.Red);
            }

        }

        private string LoadTeacherNames(AcademicCalenderSection acs)
        {
            string teacherNames = "";
            Employee emp = EmployeeManager.GetById(acs.TeacherOneID);
            if (emp != null)
                teacherNames += " " + emp.EmployeeName;
            emp = EmployeeManager.GetById(acs.TeacherTwoID);
            if (emp != null)
                teacherNames += " " + emp.EmployeeName;
            emp = EmployeeManager.GetById(acs.TeacherThreeID);
            if (emp != null)
                teacherNames += " " + emp.EmployeeName;

            return teacherNames;

        }


        //protected void LoadAcaCalSection(int acaCalId)
        //{
        //    try
        //    {
        //        ddlAcaCalSection.Items.Clear();
        //        ddlAcaCalSection.Items.Add(new ListItem("Select", "0"));
        //        ddlProgram.AppendDataBoundItems = true;

        //        int employeeId = 0;
        //        //HttpCookie aCookie = Request.Cookies[ConstantValue.Cookie_Authentication];
        //        //string uid = aCookie["UserName"];
        //        //string pwd = aCookie["UserPassword"];

        //        string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();

        //        User user = UserManager.GetByLogInId(loginID);
        //        if (user != null)
        //        {
        //            if (user.Person != null)
        //            {
        //                if (user.Person.Employee != null)
        //                    employeeId = user.Person.Employee.EmployeeID;
        //            }
        //        }

        //        List<AcademicCalenderSection> acaCalSectionList = AcademicCalenderSectionManager.GetAllByAcaCalId(acaCalId);
        //        if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
        //            acaCalSectionList = acaCalSectionList.Where(x => x.ProgramID == Convert.ToInt32(ddlProgram.SelectedValue)).ToList();
        //        if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
        //        {
        //            //if (acaCalId != 0)
        //            //    acaCalSectionList = acaCalSectionList.Where(x => x.AcademicCalenderID == acaCalId && x.AcaCal_SectionID == 1171).ToList();

        //            if (Session["Role"].ToString().Contains("Faculty") || Session["Role"].ToString().Contains("Coordinator"))
        //            {
        //                if (employeeId != 0)
        //                    acaCalSectionList = acaCalSectionList.Where(x => x.TeacherOneID == employeeId || x.TeacherTwoID == employeeId).ToList();
        //                else
        //                    acaCalSectionList = null;
        //            }
        //            else if (!Session["Role"].ToString().Contains("Admin") && !Session["Role"].ToString().Contains("Exam") && !Session["Role"].ToString().Contains("Controller"))
        //            {
        //                acaCalSectionList = null;
        //            }

        //            if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
        //            {
        //                List<Course> courseList = CourseManager.GetAll();
        //                Hashtable hashCourse = new Hashtable();
        //                foreach (Course course in courseList)
        //                    hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.FormalCode + ":" + course.Title);

        //                Dictionary<string, string> dicAcaCalSec = new Dictionary<string, string>();
        //                foreach (AcademicCalenderSection acaCalSection in acaCalSectionList)
        //                {
        //                    string courseVersion = acaCalSection.CourseID.ToString() + "_" + acaCalSection.VersionID.ToString();
        //                    try { dicAcaCalSec.Add(hashCourse[courseVersion] + "(" + acaCalSection.SectionName + ") ", acaCalSection.AcaCal_SectionID.ToString()); }
        //                    catch { }
        //                }
        //                //var acaCalSecList = dicAcaCalSec.Where(c => c.Key.ToUpper().Contains(searchKey.ToUpper())).OrderBy(x => x.Key).ToList();
        //                //var acaCalSecList = dicAcaCalSec.OrderBy(x => x.Key).ToList();
        //                var acaCalSecList = dicAcaCalSec.Where(c => c.Key.ToUpper().Contains("")).OrderBy(x => x.Key).ToList();
        //                foreach (var temp in acaCalSecList)
        //                    ddlAcaCalSection.Items.Add(new ListItem(temp.Key, temp.Value));
        //            }
        //        }

        //    }
        //    catch { }
        //    finally { }
        //}

        private void ShowMessage(String Message, Color color)
        {
            lblMsg.Text = Message;
            lblMsg.ForeColor = color;

        }

    }
}