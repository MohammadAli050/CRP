using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.DataLogic.DAFactory;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;

namespace LogicLayer.BusinessLogic
{
    public class ClassAttendanceManager
    {
        public static int Insert(ClassAttendance _Attendance)
        {
            int id = RepositoryManager.ClassAttendance_Repository.Insert(_Attendance);
            return id;
        }

        public static bool Update(ClassAttendance _Attendance)
        {
            bool isExecute = RepositoryManager.ClassAttendance_Repository.Update(_Attendance);
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ClassAttendance_Repository.Delete(id);
            return isExecute;
        }

        public static ClassAttendance GetByID(int id)
        {
            ClassAttendance _attendance = RepositoryManager.ClassAttendance_Repository.GetById(id);
            return _attendance;
        }

        public static List<ClassAttendance> GetAllByAcaCalSectionDate(int acaCalSectionId,DateTime attendanceDate)
        {
            List<ClassAttendance> _list= RepositoryManager.ClassAttendance_Repository.GetAllByAcaCalSectionDate(acaCalSectionId, attendanceDate);
            return _list;
        }

        public static List<ClassAttendance> GetAttendanceByAcaCalSectionDate(int acaCalSectionId, DateTime attendanceDate)
        {
            List<ClassAttendance> _list = RepositoryManager.ClassAttendance_Repository.GetAttendanceByAcaCalSectionDate(acaCalSectionId, attendanceDate);
            return _list;
        }

        public static List<ClassAttendance> GetAllByStudentIdCourseIdAcaCalId(int studentId, int courseId, int acaCalId)
        {
            List<ClassAttendance> _list = RepositoryManager.ClassAttendance_Repository.GetAllByStudentIdCourseIdAcaCalId(studentId, courseId, acaCalId);
            return _list;
        }

        public static List<rClassAttendance> GetAttendanceReportByAcaCalSection(int acaCalSectionId, DateTime attendanceFromDate, DateTime attendanceToDate)
        {
            List<rClassAttendance> list = RepositoryManager.ClassAttendance_Repository.GetAttendanceReportByAcaCalSection(acaCalSectionId, attendanceFromDate, attendanceToDate);
            return list;
        }

        public static List<rClassAttendance> GetAttendanceReportDatewiseByAcaCalSection(int acaCalSectionId, DateTime attendanceFromDate, DateTime attendanceToDate)
        {
            List<rClassAttendance> list = RepositoryManager.ClassAttendance_Repository.GetAttendanceReportDatewiseByAcaCalSection(acaCalSectionId, attendanceFromDate, attendanceToDate);
            return list;
        }

        public static ClassAttendance GetByDateStudentIDAcaCalSecIDate(int studentId, int acaCalSecId,DateTime attendanceDate)
        {
            ClassAttendance _classAttendance = RepositoryManager.ClassAttendance_Repository.GetByDateStudentIDAcaCalSecIDate(studentId,acaCalSecId, attendanceDate);

            return _classAttendance;
        }

        public static List<rDateTime> GetAllDate()
        {
            List<rDateTime> list = RepositoryManager.ClassAttendance_Repository.GetAllDate();
            return list;
        }
    }
}

