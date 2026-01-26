using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects.RO;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICourseRepository
    {
        List<Course> GetAllByProgram(int ProgramID);
        List<Course> GetAll();
        List<Course> GetAllOfferedCourse();
        Course GetByCourseIdVersionId(int CourseID, int VersionID);

        List<NodeCourses> GetAllNodeCoursesByNodeId(int nodeId);
        List<CourseListByTeacherDTO> GetCourseByTeacherId(int teacherId);

        List<RptFlatCourse> GetAllFlatCourseByProgram(int programId);

        List<Course> GetAllByProgramAndSessionFromStudentCourseHistoryTable(int programId, int sessionId);

        List<Course> GetAllCourseByProgramAndSessionBatchFromStudentCourseHistoryTable(int programId, int sessionId, int BatchId);
        List<Course> GetAllCourseByProgramAndSessionBatchExamCenterFromStudentCourseHistoryTable(int programId, int sessionId, int BatchId,int ExamCenterId);

        
        List<rTreeDistribution> GetTreeDistributionByProgram(int programId, string treeCalendarMasterId);

        List<rCourseRegistrationForm> GetCourseRegistrationForm(int programId, int acaCalId, string roll);

        List<rOfferedCourse> GetOfferedCourse(int programId, int acaCalId);

        List<rCourseWiseStudentList> GetCourseWiseStudentList(int programId, int acaCalId);

        List<Course> GetOfferedCourseByProgramSession(int programId, int sessionId);

        List<rTopSheet> LoadTopSheet(int examScheduleSetId, int dayId, int timeSlotId);

        List<rExamSection> LoadExamSection(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId, string courseId, int teacherId);

        List<rTeacherList> LoadTeacherList(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId, string courseId);

        List<Course> GetAllByAcaCalIdProgramId(int acaCalId, int programId);
        List<Course> GetAllByAcaCalIdProgramIdFromCourseHistory(int acaCalId, int programId);
        List<Course> GetAllByAcaCalIdStudentRoll(int acaCalId, string studentRoll);

        List<Course> GetAllByFormalCode(string formalcode);

        List<Course> GetAllByVersionCode(string versioncode);

        List<Course> GetAllRetakeOrRegularOpenCourseByStudentIdCourseTypeId(int StudentId, int TypeId);

        int Insert(Course course);

        bool Update(Course course);

        bool Delete(int courseId, int versionId);

        List<Course> GetAllCourseByProgramBatchYearFromCourseHistory(int programId, int batchId, int yearId);
    }
}
