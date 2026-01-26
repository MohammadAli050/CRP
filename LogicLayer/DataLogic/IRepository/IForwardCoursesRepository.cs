using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IForwardCoursesRepository
    {
        int Insert(ForwardCourses forwardcourses);
        bool Update(ForwardCourses forwardcourses);
        bool Delete(int Id);
        bool DeleteByStudentIdCourseIdVersionIdAcaCalId(int StudentId, int CourseId, int VersionId, int AcaCalId);
        ForwardCourses GetById(int? Id);
        List<ForwardCourses> GetAll();
        List<ForwardCourses>  GetAllByStudentIdAcaCalId(int StudentId, int AcaCalId);
        ForwardCourses GetByCourseHistoryId(int CourseHistoryId);
        ForwardCourses GetByRegistrationWorkSheetId(int RegWrokId);
    }
}

