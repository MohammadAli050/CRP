using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IVNodeSetMasterRepository
    {
        int Insert(VNodeSetMaster vNodeSetMaster);
        bool Update(VNodeSetMaster vNodeSetMaster);
        bool Delete(int id);
        VNodeSetMaster GetById(int? id);
        List<VNodeSetMaster> GetAll();
        List<VNodeSetMaster> GetByNodeId(int nodeId);
    }
}
