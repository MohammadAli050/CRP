using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IEquiCourseDetailsRepository
    {
        int Insert(EquiCourseDetails equicoursedetails);
        bool Update(EquiCourseDetails equicoursedetails);
        bool Delete(int EquiCourseDetailId);
        EquiCourseDetails GetById(int? EquiCourseDetailId);
        List<EquiCourseDetails> GetAll();
        List<EquivalentCourseDTO> GetAllEquivalentCourse();
        List<EquivalentCourseDTO> GetAllEquivalentCourseForRpt();
        List<EquivalentCourseDTO> GetAllEquivalentCourseByMasterId(int equivalentCoursemasterId);
        List<EquivalentCourseDTO> GetAllEquivalentCourseByParameters(int programId, int courseId, int versionId, string vesionCode);
    }
}

