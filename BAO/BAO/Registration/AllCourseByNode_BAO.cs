using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
    public class AllCourseByNode_BAO
    {        
        public static List<AllCourseByNodeEntity> GetAllDataByNodeId(int nodeId)
        {
            return AllCourseByNode_DAO.GetAllDataByNodeId(nodeId);
        }
    }
}
