using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICoursePredictMasterRepository
    {
        int Insert(CoursePredictMaster coursepredictmaster);
        bool Update(CoursePredictMaster coursepredictmaster);
        bool Delete(int Id);
        CoursePredictMaster GetById(int Id);
        List<CoursePredictMaster> GetAll();
        bool InsertByAcaCalProgram(int acaCalId, int programId);
        List<CoursePredictMaster> GetAll(int acaCalId, int programId);
    }
}

