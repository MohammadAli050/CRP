using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic
{
    public interface IStudentPreCourseRepository
    {
        int Insert(StudentPreCourse studentprecourse);
        bool Update(StudentPreCourse studentprecourse);
        bool Delete(int id);
        StudentPreCourse GetById(int id);
        List<StudentPreCourse> GetAll();
        List<StudentPreCourse> GetAllByParameter(int action, string batchCode, string programCode, string preMandatoryCourse, int preCourseId, int preVersionId, int mainCourseId, int mainVersionId);
    }
}
