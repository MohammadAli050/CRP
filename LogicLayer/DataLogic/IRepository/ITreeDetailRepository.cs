using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ITreeDetailRepository
    {
        int Insert(TreeDetail treeDetail);
        bool Update(TreeDetail treeDetail);
        bool Delete(int id);
        TreeDetail GetById(int? id);
        List<TreeDetail> GetAll();
        List<TreeDetail> GetByTreeMasterIdParentNodeId(int treeMasterId, int parentNodeId);
    }
}
