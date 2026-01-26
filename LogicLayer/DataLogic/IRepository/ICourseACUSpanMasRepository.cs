using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
   public interface ICourseACUSpanMasRepository
    {
       int Insert(CourseACUSpanMas courseACUSpanMas);
       bool Update(CourseACUSpanMas courseACUSpanMas);
        bool Delete(int id);
        CourseACUSpanMas GetById(int? id);
        List<CourseACUSpanMas> GetAll();
    }
}
