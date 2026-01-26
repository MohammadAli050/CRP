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
    public class DayLectureManager
    { 
        public static int Insert(DayLecture daylecture)
        {
            int id = RepositoryManager.DayLecture_Repository.Insert(daylecture); 
            return id;
        }

        public static bool Update(DayLecture daylecture)
        {
            bool isExecute = RepositoryManager.DayLecture_Repository.Update(daylecture); 
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.DayLecture_Repository.Delete(id); 
            return isExecute;
        }

        public static DayLecture GetById(int? id)
        { 
            DayLecture daylecture  = RepositoryManager.DayLecture_Repository.GetById(id);                
            return daylecture;
        }

        public static List<DayLecture> GetAll()
        {
            List<DayLecture> list  = RepositoryManager.DayLecture_Repository.GetAll();                
            return list;
        }

        public static DayLecture GetByProgramIdSessionIdCourseIdVersionIdlectureNo(int ProgramId,int SessionId,int CourseId,int VersionId,int LectureNo)
        {
            DayLecture daylecture = RepositoryManager.DayLecture_Repository.GetByProgramIdSessionIdCourseIdVersionIdlectureNo(ProgramId,SessionId,CourseId,VersionId,LectureNo);
            return daylecture;
        }
    }
}

