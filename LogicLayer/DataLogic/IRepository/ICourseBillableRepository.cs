using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICourseBillableRepository
    {
        int Insert(CourseBillable courseBillable);
        bool Update(CourseBillable courseBillable);
        bool Delete(int id);
        CourseBillable GetById(int? id);
        List<CourseBillable> GetAll();
    }
}
