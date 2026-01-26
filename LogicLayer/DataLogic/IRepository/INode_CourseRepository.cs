using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface INode_CourseRepository
    {
        int Insert(Node_Course node_Course);
        bool Update(Node_Course node_Course);
        bool Delete(int id);
        Node_Course GetById(int? id);
        List<Node_Course> GetAll();
        List<NodeCoursesDTO> GetByNodeId(int nodeId);
    }
}
