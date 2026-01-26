using LogicLayer.BusinessLogic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.Module.Dashboard
{
    public partial class SemesterDashboard : BasePage
    {
        int userId = 0;

        BussinessObject.UIUMSUser UserObj = null;
        UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();
        static UCAMDAL.UCAMEntities statiucamContext = new UCAMDAL.UCAMEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UserObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

                base.CheckPage_Load();
                if (!IsPostBack)
                {

                    LoadCalendarTypes();
                    LoadAcademicCalendars();
                    LoadStudentGrid(0, 0);
                }
            }
            catch (Exception Ex)
            {
            }
        }

        private void LoadCalendarTypes()
        {
            try
            {
                var CalenderUnitMasterList = ucamContext.CalenderUnitMasters.ToList();

                ddlCalendarType.Items.Clear();
                ddlCalendarType.AppendDataBoundItems = true;
                ddlCalendarType.Items.Add(new ListItem("All", "0"));

                if (CalenderUnitMasterList != null && CalenderUnitMasterList.Count > 0)
                {
                    ddlCalendarType.DataTextField = "Name";
                    ddlCalendarType.DataValueField = "CalenderUnitMasterID";
                    ddlCalendarType.DataSource = CalenderUnitMasterList.OrderBy(x => x.CalenderUnitMasterID).ToList();
                    ddlCalendarType.DataBind();
                }

            }
            catch (Exception ex)
            {
            }
        }

        protected void ddlCalendarType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAcademicCalendars();
        }
        private void LoadAcademicCalendars()
        {
            try
            {

                List<LogicLayer.BusinessObjects.AcademicCalender> academicCalenderList = new List<LogicLayer.BusinessObjects.AcademicCalender>();
                int CalenderUnitMasterId = 0;
                try
                {
                    CalenderUnitMasterId = Convert.ToInt32(ddlCalendarType.SelectedValue);
                }
                catch (Exception ex)
                {
                }

                if (CalenderUnitMasterId > 0)
                    academicCalenderList = AcademicCalenderManager.GetAll(CalenderUnitMasterId);
                else
                    academicCalenderList = AcademicCalenderManager.GetAll();

                ddlAcademicCalendar.Items.Clear();
                ddlAcademicCalendar.Items.Add(new ListItem("All", "0"));
                ddlAcademicCalendar.AppendDataBoundItems = true;

                if (academicCalenderList != null && academicCalenderList.Any())
                {
                    academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();
                    foreach (LogicLayer.BusinessObjects.AcademicCalender academicCalender in academicCalenderList)
                        ddlAcademicCalendar.Items.Add(new ListItem(academicCalender.FullCodeWithType, academicCalender.AcademicCalenderID.ToString()));
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void LoadStudentGrid(int calendarTypeId, int academicCalendarId)
        {
            try
            {
                /// get data using linq query

                gvStudents.DataSource = null;
                gvStudents.DataBind();

                var students = (from s in ucamContext.Students
                                join p in ucamContext.People on s.PersonID equals p.PersonID
                                join pr in ucamContext.Programs on s.ProgramID equals pr.ProgramID
                                join b in ucamContext.Batches on s.BatchId equals b.BatchId
                                join sch in ucamContext.StudentCourseHistories on s.StudentID equals sch.StudentID into schGroupJoin
                                from sch in schGroupJoin.DefaultIfEmpty()


                                    //join acu in
                                    //    (from a in ucamContext.StudentACUDetails
                                    //     where a.CGPA != null && a.CGPA > 0
                                    //     group a by a.StudentID into g
                                    //     select new
                                    //     {
                                    //         StudentID = g.Key,
                                    //         CGPA = g.Max(x => x.CGPA)
                                    //     })
                                    //on s.StudentID equals acu.StudentID into acuJoin
                                    //from acu in acuJoin.DefaultIfEmpty()
                                where (s.IsDeleted == false || s.IsDeleted == null) && s.IsActive == true
                                && (sch.AcaCalID == academicCalendarId || academicCalendarId == 0)
                                orderby s.StudentID
                                select new
                                {
                                    s.StudentID,
                                    s.Roll,
                                    p.FullName,
                                    ProgramName = pr.ShortName,
                                    BatchName = b.BatchNO,
                                    s.IsActive
                                }).Distinct().ToList();

                gvStudents.DataSource = students;
                gvStudents.DataBind();



            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

        protected void gvStudents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStudents.PageIndex = e.NewPageIndex;
            int calendarTypeId = Convert.ToInt32(ddlCalendarType.SelectedValue);
            int academicCalendarId = Convert.ToInt32(ddlAcademicCalendar.SelectedValue);
            LoadStudentGrid(calendarTypeId, academicCalendarId);

            GetDashboardData(calendarTypeId.ToString(), academicCalendarId.ToString());
        }

        [WebMethod]
        public static string GetDashboardData(string calendarTypeId, string academicCalendarId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["Connection String"].ConnectionString;

            var dashboardData = new
            {
                TotalStudents = GetTotalStudents(connStr, calendarTypeId, academicCalendarId),
                TotalCourses = GetTotalCourses(connStr, calendarTypeId, academicCalendarId),
                TotalPrograms = GetTotalPrograms(connStr, calendarTypeId, academicCalendarId),
                TotalInst = GetTotalInst(connStr, calendarTypeId, academicCalendarId),
                ProgramData = GetProgramData(connStr, calendarTypeId, academicCalendarId),
                CGPAData = GetCGPADistribution(connStr, calendarTypeId, academicCalendarId),
                CourseData = GetCourseData(connStr, calendarTypeId, academicCalendarId)
            };

            return JsonConvert.SerializeObject(dashboardData);
        }

        private static int GetTotalStudents(string connStr, string calendarTypeId, string academicCalendarId)
        {
            try
            {
                int studentCount = 0;

                studentCount = statiucamContext.StudentCourseHistories.Where(x => x.AcaCalID.ToString() == academicCalendarId || academicCalendarId == "0")
                    .Select(x => x.StudentID).Distinct().Count();

                return studentCount;

            }
            catch
            {
                return 0;
            }
        }

        private static int GetTotalCourses(string connStr, string calendarTypeId, string academicCalendarId)
        {
            try
            {
                int courseCount = 0;

                courseCount = statiucamContext.StudentCourseHistories.Where(x => x.AcaCalID.ToString() == academicCalendarId || academicCalendarId == "0")
                    .Select(x => x.CourseID).Distinct().Count();

                return courseCount;
            }
            catch
            {
                return 0;
            }
        }

        private static int GetTotalPrograms(string connStr, string calendarTypeId, string academicCalendarId)
        {
            try
            {
                int totalPrograms = 0;

                totalPrograms = (from s in statiucamContext.Students
                                 join p in statiucamContext.People on s.PersonID equals p.PersonID
                                 join pr in statiucamContext.Programs on s.ProgramID equals pr.ProgramID
                                 join sch in statiucamContext.StudentCourseHistories on s.StudentID equals sch.StudentID into schGroupJoin
                                 from sch in schGroupJoin.DefaultIfEmpty()
                                 where (s.IsDeleted == false || s.IsDeleted == null) && s.IsActive == true
                                 && (sch.AcaCalID.ToString() == academicCalendarId || academicCalendarId == "0")
                                 select new
                                 {
                                     pr.ProgramID
                                 }).Distinct().ToList().Count();

                return totalPrograms;
            }
            catch
            {
                return 0;
            }
        }

        private static int GetTotalInst(string connStr, string calendarTypeId, string academicCalendarId)
        {
            try
            {
                int totalInst = 0;

                totalInst = (from s in statiucamContext.Students
                             join p in statiucamContext.People on s.PersonID equals p.PersonID
                             join pr in statiucamContext.Programs on s.ProgramID equals pr.ProgramID
                             join sch in statiucamContext.StudentCourseHistories on s.StudentID equals sch.StudentID into schGroupJoin
                             from sch in schGroupJoin.DefaultIfEmpty()
                             where (s.IsDeleted == false || s.IsDeleted == null) && s.IsActive == true
                             && (sch.AcaCalID.ToString() == academicCalendarId || academicCalendarId == "0")
                             select new
                             {
                                 pr.InstituteId
                             }).Distinct().Count();

                return totalInst;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private static List<object> GetProgramData(string connStr, string calendarTypeId, string academicCalendarId)
        {
            var programList = new List<object>();

            try
            {

                var students = (from s in statiucamContext.Students
                                join pr in statiucamContext.Programs on s.ProgramID equals pr.ProgramID
                                join sch in statiucamContext.StudentCourseHistories on s.StudentID equals sch.StudentID into schGroupJoin
                                from sch in schGroupJoin.DefaultIfEmpty()
                                where (s.IsDeleted == false || s.IsDeleted == null) && s.IsActive == true
                                && (sch.AcaCalID.ToString() == academicCalendarId || academicCalendarId == "0")
                                select new
                                {
                                    pr.ProgramID,
                                    pr.ShortName,
                                    s.StudentID
                                }).Distinct().ToList();

                var programData = students.GroupBy(x => new { x.ProgramID, x.ShortName })
                    .Select(g => new
                    {
                        ProgramName = g.Key.ShortName,
                        StudentCount = g.Count()
                    }).OrderByDescending(x => x.StudentCount).ToList();
                foreach (var item in programData)
                {
                    programList.Add(new
                    {
                        ProgramName = item.ProgramName,
                        StudentCount = item.StudentCount
                    });
                }
            }
            catch (Exception ex) { }

            return programList;
        }

        private static List<object> GetCGPADistribution(string connStr, string calendarTypeId, string academicCalendarId)
        {
            var cgpaList = new List<object>();

            try
            {

                var gradedetails = GradeDetailsManager.GetAll().Where(x => x.GradeId <= 11).ToList().OrderBy(x => x.GradeId);

                var acudetails = statiucamContext.StudentACUDetails
                    .Where(x => x.GPA != null && x.GPA > 0
                    && (x.StdAcademicCalenderID.ToString() == academicCalendarId || academicCalendarId == "0")
                    ).ToList();

                //Range = reader["Range"].ToString(),
                //            Count = Convert.ToInt32(reader["Count"])

                for (int i = 0; i < gradedetails.Count() - 2; i++)
                {
                    cgpaList.Add(new
                    {
                        Range = gradedetails.ElementAt(i + 1).GradePoint + "-" + gradedetails.ElementAt(i).GradePoint,
                        Count = acudetails.Where(x => x.GPA >= gradedetails.ElementAt(i + 1).GradePoint && x.GPA < gradedetails.ElementAt(i).GradePoint).Select(x => x.StudentID).Distinct().Count()
                    });
                }


            }
            catch (Exception ex) { }

            return cgpaList;
        }

        private static List<object> GetCourseData(string connStr, string calendarTypeId, string academicCalendarId)
        {
            var courseList = new List<object>();

            try
            {
                var students = (from s in statiucamContext.Students
                                join p in statiucamContext.People on s.PersonID equals p.PersonID
                                join pr in statiucamContext.Programs on s.ProgramID equals pr.ProgramID
                                join sch in statiucamContext.StudentCourseHistories on s.StudentID equals sch.StudentID into schGroupJoin
                                from sch in schGroupJoin.DefaultIfEmpty()
                                join c in statiucamContext.Courses on sch.CourseID equals c.CourseID
                                where (s.IsDeleted == false || s.IsDeleted == null) && s.IsActive == true
                                && (sch.AcaCalID.ToString() == academicCalendarId || academicCalendarId == "0")

                                select new
                                {
                                    c.CourseID,
                                    c.FormalCode,
                                    c.Title,
                                    pr.ShortName,
                                    s.StudentID,
                                    c.Credits,
                                    c.IsActive
                                }).ToList();

                courseList = (from course in students
                              group course by new { course.CourseID, course.FormalCode, course.Title, course.ShortName, course.Credits, course.IsActive } into courseGroup
                              select new
                              {
                                  CourseCode = courseGroup.Key.FormalCode,
                                  CourseTitle = courseGroup.Key.Title,
                                  ProgramName = courseGroup.Key.ShortName,
                                  EnrolledStudents = courseGroup.Select(x => x.StudentID).Distinct().Count(),
                                  Credits = courseGroup.Key.Credits,
                                  IsActive = courseGroup.Key.IsActive
                              }).OrderByDescending(x => x.EnrolledStudents).Take(20)
                            .Select(c => new
                            {
                                c.CourseCode,
                                c.CourseTitle,
                                c.ProgramName,
                                c.EnrolledStudents,
                                Credits = Convert.ToDecimal(c.Credits).ToString("F1"),
                                c.IsActive
                            }).ToList<object>();

            }
            catch (Exception ex) { }

            return courseList;
        }

        private static string BuildFilterQuery(string calendarTypeId, string academicCalendarId)
        {
            string filterQuery = "";

            if (calendarTypeId != "0")
            {
                filterQuery += @" AND EXISTS (
                    SELECT 1 FROM AcademicCalender ac
                    WHERE ac.CalenderUnitTypeID IN (
                        SELECT CalenderUnitTypeID FROM CalenderUnitType 
                        WHERE CalenderUnitMasterID = @CalendarTypeId
                    )
                    AND b.AcaCalId = ac.AcademicCalenderID
                )";
            }

            if (academicCalendarId != "0")
            {
                filterQuery += " AND b.AcaCalId = @AcademicCalendarId";
            }

            return filterQuery;
        }

        private static void AddFilterParameters(SqlCommand cmd, string calendarTypeId, string academicCalendarId)
        {
            if (calendarTypeId != "0")
            {
                cmd.Parameters.AddWithValue("@CalendarTypeId", calendarTypeId);
            }

            if (academicCalendarId != "0")
            {
                cmd.Parameters.AddWithValue("@AcademicCalendarId", academicCalendarId);
            }
        }
        protected void showAlert(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
        }

        protected void ddlAcademicCalendar_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStudentGrid(0, Convert.ToInt32(ddlAcademicCalendar.SelectedValue));
        }
    }
}