using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class CourseListByNodeDTOManager
    {
        public static List<CourseListByNodeDTO> GetAllByRootNodeId(int rootNodeId)
        {
            return RepositoryManager.CourseListByNodeDTO_Repository.GetAllByRootNodeId(rootNodeId);
        }
    }
}
