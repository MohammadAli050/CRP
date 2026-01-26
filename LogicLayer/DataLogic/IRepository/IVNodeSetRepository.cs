using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IVNodeSetRepository
    {
        int Insert(VNodeSet vNodeSet);
        bool Update(VNodeSet vNodeSet);
        bool Delete(int id);
        VNodeSet GetById(int? id);
        List<VNodeSet> GetAll();
        List<VNodeSet> GetbyNodeId(int nodeId);
        List<VNodeSet> GetbyVNodeSetMasterId(int vNodeSetMasterId);
    }
}
