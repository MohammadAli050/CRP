using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IRegistrationWorksheetRepository
    {
       int Insert(RegistrationWorksheet registrationWorksheet);
        bool Update(RegistrationWorksheet registrationWorksheet);
        bool Delete(int id);
        RegistrationWorksheet GetById(int id);
        List<RegistrationWorksheet> GetAll();
        List<RegistrationWorksheet> GetAllOpenCourseByStudentID(int studentID);
        List<RegistrationWorksheet> GetByStudentID(int studentID);
        bool RegisterCourse(int id, int createdBy, int courseStatusID);
        List<RegistrationWorksheet> GetAllByStdProgCal(int studentId, int programId, int batchId);
        List<RegistrationWorksheet> GetAllByProgCal(int programId, int batchId);
        List<RegistrationWorksheet> GetAllByAcaProgCourse(int acaCalId, int programId, string courseList);
        List<RegistrationWorksheet> GetReqByProgCal(int programId, int batchId); //Custon RegistrationWorksheet Class for eliminating data overhead
        List<RegistrationWorksheet> GetPreRegByProgCal(int programId, int batchId);
        List<RegistrationWorksheet> GetPreAdByProgCal(int programId, int batchId);
        List<RegistrationWorksheet> GetPreRegByCourse(int courseId, int versionId);
        List<RegistrationWorksheet> GetReqByCourse(int courseId, int versionId);
        #region Prepare Registration WorkSheet
        bool RegistrationWorksheetGeneratePerStudent(RegistrationWorksheetParam rwParam, int semesterNumber);

        bool RegistrationWorksheetAutoOpen(RegistrationWorksheetParam registrationWorksheetAutoOpen);

        bool RegistrationWorksheetAutoPreRegistration(RegistrationWorksheetParam registrationWorksheetAutoPreReg);

        bool RegistrationWorksheetAutoMandatory(RegistrationWorksheetParam registrationWorksheetAutoMandatory); 
        #endregion

        bool UpdateForAssignCourseNew(RegistrationWorksheet registrationWorksheet);

        bool UpdateForAssignCourseRetake(RegistrationWorksheet registrationWorksheet);

        List<RegistrationWorksheet> GetAllAutoAssignCourseByStudentID(int studentId);

        bool CourseRegistrationForStudent(int studentId);

        bool RegistrationWorksheetGeneratePerStudentBBA(RegistrationWorksheetParam rwParam);

        List<RegistrationWorksheet> GetRegistrationSessionDataByStudentID(int studentId);

        List<RegistrationWorksheet> GetByProgramId(int programId);

        List<RegistrationWorksheet> GetAll(int acaCalId, int programId);

        int CountTakenCourseInRW(int ProgramID, int CourseID, int VersionID, int TreeMasterID);

        int CountOpenCourseInRW(int ProgramID, int CourseID, int VersionID, int TreeMasterID);

        int CountMandatoryCourseInRW(int ProgramID, int CourseID, int VersionID, int TreeMasterID);

        List<RegistrationWorksheet> GetAllOpenCourseWhichSectionIsMatchInStudentBatchByStudentID(int studentId);

        bool UpdateForSectionTake(RegistrationWorksheet registrationWorksheet);

        bool UpdateForSectionRemove(RegistrationWorksheet registrationWorksheet);

        List<RegistrationWorksheet> GetAllStudentByProgramSession(int programId, int sessionId);

        List<RegistrationWorksheet> GetAllStudentByProgramSessionCourse(int programId, int sessionId, int batchId, int courseId, int versionId);

        List<StudentBulkRegistration> GetAllStudentWithAllCourseSectionByProgramSessionBatch(int programId, int sessionId, int batchId);

        List<RegistrationWorksheet> GetAllCourseByProgramSessionBatchStudentId(int StudentId, int sessionId);

        List<RegistrationWorksheet> GetAllForwardCoursesByStudentIDAcaCalId(int studentId, int AcaCalId);

        RegistrationWorksheet GetByStudentIdCourseIdVersionId(int studentId, int courseId, int versionId);

        bool IsForwardOrRegistrationDoneForStudent(int StudentId, int AcaCalId, int IsRetake);
    }
}
