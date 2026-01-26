using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IOfferedCourseRepository
    {
        int Insert(OfferedCourse offeredCourse);
        int InsertList(List<OfferedCourse> offeredCourseList);
        bool Update(OfferedCourse offeredCourse);
        bool Delete(int id);
        OfferedCourse GetById(int? id);
        List<OfferedCourse> GetAll();
        bool DeleteByProgIdAndAcaCalId(int programId, int acaCalId);
        bool UpdateList(List<OfferedCourse> offeredCourseList);
        bool ActiveInactiveList(List<OfferedCourse> offeredCourseList);
        bool DeleteByProgramAndAcaCalAndTreeRoot(int programId, int acaCalId, int treeRoot);
        List<OfferedCourse> GetAllByProgramAcacalTreeroot(int programId, int acaCalId, int treeRoot);
        List<OfferedCourseDTO> GetAllDtoObjByProgramAcacalTreeroot(int programId, int acaCalId, int treeRoot);
        OfferedCourse GetBy(int ProgramID, int AcademicCalenderID, int TreeMasterID, int CourseID, int VersionID);
        List<WorkSheetCourseHistoryDTO> GetStudentByProgramCourseVersionSection(int acaCalId, int programId, int courseId, int versionId, int sectionId);
    }
}
