using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
   public interface ICourseWavTransfrMasterRepository
    {
       int Insert(CourseWavTransfrMaster courseWavTransfrMaster);
       bool Update(CourseWavTransfrMaster courseWavTransfrMaster);
        bool Delete(int id);
        CourseWavTransfrMaster GetById(int? id);
        List<CourseWavTransfrMaster> GetAll();
    }
}
