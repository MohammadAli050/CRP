using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class ForwardCoursesManager
    {
        public static int Insert(ForwardCourses forwardcourses)
        {
            int id = RepositoryManager.ForwardCourses_Repository.Insert(forwardcourses);
            return id;
        }

        public static bool Update(ForwardCourses forwardcourses)
        {
            bool isExecute = RepositoryManager.ForwardCourses_Repository.Update(forwardcourses);
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ForwardCourses_Repository.Delete(id);
            return isExecute;
        }        

        public static bool DeleteByStudentIdCourseIdVersionIdAcaCalId(int StudentId, int CourseId, int VersionId, int AcaCalId)
        {
            bool isExecute = RepositoryManager.ForwardCourses_Repository.DeleteByStudentIdCourseIdVersionIdAcaCalId(StudentId, CourseId, VersionId, AcaCalId);
            return isExecute;
        }

        public static ForwardCourses GetByCourseHistoryId(int CourseHistoryId)
        {
            ForwardCourses forwardcourses = RepositoryManager.ForwardCourses_Repository.GetByCourseHistoryId(CourseHistoryId);
            return forwardcourses;
        }

        public static ForwardCourses GetByRegistrationWorkSheetId(int RegWrokId)
        {
            ForwardCourses forwardcourses = RepositoryManager.ForwardCourses_Repository.GetByRegistrationWorkSheetId(RegWrokId);
            return forwardcourses;
        }

        public static ForwardCourses GetById(int? id)
        {
            ForwardCourses forwardcourses = RepositoryManager.ForwardCourses_Repository.GetById(id);
            return forwardcourses;
        }

        public static List<ForwardCourses> GetAll()
        {

            List<ForwardCourses> list = RepositoryManager.ForwardCourses_Repository.GetAll();

            return list;
        }

        public static List<ForwardCourses> GetAllByStudentIdAcaCalId(int StudentId, int AcaCalId)
        {

            List<ForwardCourses> list = RepositoryManager.ForwardCourses_Repository.GetAllByStudentIdAcaCalId(StudentId, AcaCalId);
            return list;
        }
    }
}

