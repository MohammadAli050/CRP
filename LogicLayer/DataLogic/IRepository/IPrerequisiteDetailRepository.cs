using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IPrerequisiteDetailRepository
    {
        int Insert(PrerequisiteDetail prerequisiteDetail);
        bool Update(PrerequisiteDetail prerequisiteDetail);
        bool Delete(int id);
        PrerequisiteDetail GetById(int? id);
        List<PrerequisiteDetail> GetAll();

        List<PrerequisiteDetail> GetAllByProgramId(int programId);
    }
}
