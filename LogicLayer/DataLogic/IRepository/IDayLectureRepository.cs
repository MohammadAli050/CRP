using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IDayLectureRepository
    {
        int Insert(DayLecture daylecture);
        bool Update(DayLecture daylecture);
        bool Delete(int Id);
        DayLecture GetById(int? Id);
        DayLecture GetByProgramIdSessionIdCourseIdVersionIdlectureNo(int ProgramId,int SessionId,int CourseId,int VersionId,int LectureNo);
        List<DayLecture> GetAll();
    }
}

