using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
   public interface IEquiCourseRepository
    {
       int Insert(EquiCourse equiCourse);
       bool Update(EquiCourse equiCourse);
        bool Delete(int id);
        EquiCourse GetById(int? id);
        List<EquiCourse> GetAll();
        List<rEquivalentCourse> GetAllEquivalentCourseByProgramId(int programId);
    }
}
