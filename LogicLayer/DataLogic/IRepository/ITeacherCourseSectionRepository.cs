using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ITeacherCourseSectionRepository
    {
        int Insert(TeacherCourseSection teachercoursesection);
        bool Update(TeacherCourseSection teachercoursesection);
        bool Delete(int Id);
        TeacherCourseSection GetById(int? Id);
        List<TeacherCourseSection> GetAll();
    }
}

