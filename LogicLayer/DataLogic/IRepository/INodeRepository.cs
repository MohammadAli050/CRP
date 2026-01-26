using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface INodeRepository
    {
        int Insert(Node node);
        bool Update(Node node);
        bool Delete(int id);
        Node GetById(int? id);
        List<Node> GetAll();
        List<Node> GetAllMajorNodeByBatchId(int batchId);
        List<Node> GetNodeByTreeMasterId(int treeMasterId);
    }
}
