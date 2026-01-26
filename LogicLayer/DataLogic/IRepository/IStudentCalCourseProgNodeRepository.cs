using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentCalCourseProgNodeRepository
    {
        int Insert(StudentCalCourseProgNode studentCalCourseProgNode);
        bool Update(StudentCalCourseProgNode studentCalCourseProgNode);
        bool Delete(int id);
        StudentCalCourseProgNode GetById(int? id);
        List<StudentCalCourseProgNode> GetAll();
    }
}
