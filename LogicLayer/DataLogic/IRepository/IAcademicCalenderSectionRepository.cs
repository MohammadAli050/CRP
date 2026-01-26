using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IAcademicCalenderSectionRepository
    {
        int Insert(AcademicCalenderSection academicCalenderSection);
        bool Update(AcademicCalenderSection academicCalenderSection);
        bool Delete(int id);
        AcademicCalenderSection GetById(int? id);
        List<AcademicCalenderSection> GetAll();
        List<AcademicCalenderSection> GetAll(int studentId, int acaCalId);
        List<AcademicCalenderSection> GetAllByAcaCalId(int id);
        List<AcademicCalenderSection> GetAllByAcaCalIdStudentRoll(int id, string studentRoll);
        List<AcademicCalenderSection> GetAllByAcaCalIdState(int id, string state);
        List<AcademicCalenderSection> GetAllByRoomDayTime(int Room1, int Room2, int Day1, int Day2, int Time1, int Time2);
        AcademicCalenderSection GetByCourseVersionSecFac(int courseId, int versionId, string section, int facultyId);
        AcademicCalenderSection GetByAcaCalCourseVersionSection(int acaCalId, int courseId, int versionId, string sectionName);
        List<AcademicCalenderSection> GetByAcaCalCourseVersion(int acaCalId, int courseId, int versionId);
        List<AcademicCalenderSection> GetAllByRoomInRegSession(int roomId);
        List<AcademicCalenderSection> GetAllByTeacherInRegSession(int teacherId);
        List<AcademicCalenderSection> GetAllByAcaCalProgram(int acaCalId, int programId);
        List<AcademicCalenderSection> GetAllByAcaCalAndProgram(int programId, int sessionId);
        List<AcademicCalenderSectionWithCourse> GetAllCourseWithSectionByAcaCalAndProgramAndTeacher(int acaCalId, int programId, int teacherId);
        List<TeacherInfo> GetAllTeacherByAcaCalAndProgram(int acaCalId, int programId);
    }
}
