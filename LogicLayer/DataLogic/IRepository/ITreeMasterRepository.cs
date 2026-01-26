using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ITreeMasterRepository
    {
        int Insert(TreeMaster treeMaster);
        bool Update(TreeMaster treeMaster);
        bool Delete(int id);
        TreeMaster GetById(int? id);
        List<TreeMaster> GetAll();
        List<TreeMaster> GetAllByProgramID(int programID);
    }
}
