using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IClassAttendanceRepository
    {
        int Insert(ClassAttendance _attendance);
        bool Update(ClassAttendance _attendance);
        bool Delete(int id);
        ClassAttendance GetById(int id);
        List<ClassAttendance> GetAllByAcaCalSectionDate(int acaCalSectionId, DateTime attendanceDate);
        List<ClassAttendance> GetAttendanceByAcaCalSectionDate(int acaCalSectionId, DateTime attendanceDate);
        List<ClassAttendance> GetAllByStudentIdCourseIdAcaCalId(int studentId, int courseId, int acaCalId);
        ClassAttendance GetByDateStudentIDAcaCalSecIDate(int studentId, int acaCalSecId, DateTime attendanceDate);
        List<rClassAttendance> GetAttendanceReportByAcaCalSection(int acaCalSectionId, DateTime attendanceFromDate, DateTime attendanceToDate);
        List<rClassAttendance> GetAttendanceReportDatewiseByAcaCalSection(int acaCalSectionId, DateTime attendanceFromDate, DateTime attendanceToDate);
        List<rDateTime> GetAllDate();
    }
}
