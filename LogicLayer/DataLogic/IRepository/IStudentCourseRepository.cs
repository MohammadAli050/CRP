using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentCourseRepository
    {
        int Insert(StudentCourse student_Course);
        bool Update(StudentCourse student_Course);
        bool Delete(int id);
        StudentCourse GetById(int? id);
        List<StudentCourse> GetAll();
    }
}
