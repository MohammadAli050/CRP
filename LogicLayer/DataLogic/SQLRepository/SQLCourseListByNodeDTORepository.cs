using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LogicLayer.DataLogic.SQLRepository
{
    public class SQLCourseListByNodeDTORepository : ICourseListByNodeDTORepository
    {

        Database db = null;
        private string sqlAllCourseByNode = "AllCourseByNode";



        public List<CourseListByNodeDTO> GetAllByRootNodeId(int rootNodeId)
        {
            List<CourseListByNodeDTO> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseListByNodeDTO> mapper = MapBuilder<CourseListByNodeDTO>.MapAllProperties()
                .Map(m => m.NodeId).ToColumn("NodeID")
                .Map(m => m.NodeCourseId).ToColumn("Node_CourseID")
                .Map(m => m.CourseId).ToColumn("CourseID")
                .Map(m => m.VersionId).ToColumn("VersionID")
                .Build();


                var accessor = db.CreateSprocAccessor<CourseListByNodeDTO>(sqlAllCourseByNode, mapper);
                IEnumerable<CourseListByNodeDTO> collection = accessor.Execute(rootNodeId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }
    }
}
