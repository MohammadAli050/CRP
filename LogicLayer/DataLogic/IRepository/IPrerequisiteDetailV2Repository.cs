using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.BusinessObjects.RO;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IPrerequisiteDetailV2Repository
    {
        int Insert(PrerequisiteDetailV2 prerequisitedetailv2);
        bool Update(PrerequisiteDetailV2 prerequisitedetailv2);
        bool Delete(int PreRequisiteDetailId);
        PrerequisiteDetailV2 GetById(int? PreRequisiteDetailId);
        List<PrerequisiteDetailV2> GetAll();
        List<PreRequisiteSetDTO> GetAllPreRequisiteSetAndCourses(int programId, int courseId, int versionId, string versionCode);
        List<PreRequisiteSetDTO> GetAllPreRequisiteDetailCourses(int preRequisiteMasterId);
        List<rPreRequisiteCourse> GetAllPreRequisiteCoursesProgramWise(int programId);
        
    }
}

