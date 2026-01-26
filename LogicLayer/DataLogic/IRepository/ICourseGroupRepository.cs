using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICourseGroupRepository
    {
        int Insert(CourseGroup coursegroup);
        bool Update(CourseGroup coursegroup);
        bool Delete(int CourseGroupId);
        CourseGroup GetById(int? CourseGroupId);
        List<CourseGroup> GetAll();
    }
}

