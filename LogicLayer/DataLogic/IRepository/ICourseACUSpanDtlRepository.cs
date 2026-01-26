using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICourseACUSpanDtlRepository
    {
        int Insert(CourseACUSpanDtl courseACUSpanDtl);
        bool Update(CourseACUSpanDtl courseACUSpanDtl);
        bool Delete(int id);
        CourseACUSpanDtl GetById(int? id);
        List<CourseACUSpanDtl> GetAll();
    }
}
