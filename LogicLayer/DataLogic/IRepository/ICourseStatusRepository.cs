using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICourseStatusRepository
    {
        int Insert(CourseStatus courseStatus);
        bool Update(CourseStatus courseStatus);
        bool Delete(int id);
        CourseStatus GetById(int? id);
        CourseStatus GetByCode(string code);
        List<CourseStatus> GetAll();
    }
}
