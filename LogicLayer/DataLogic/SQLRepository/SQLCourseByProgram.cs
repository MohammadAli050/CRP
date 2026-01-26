using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System;
using System.Collections.Generic;
using LogicLayer.BusinessObjects.RO;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLCourseByProgram : ICourseListByProgram
    {
        Database db = null;


        public List<rCourseListByProgram> GetAllByProgram(int programId)
        {
            List<rCourseListByProgram> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<rCourseListByProgram> mapper = AllCourseMapper();

                var accessor = db.CreateSprocAccessor<rCourseListByProgram>("RptAllCourseByProgram", mapper);
                IEnumerable<rCourseListByProgram> collection = accessor.Execute(programId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rCourseListByProgram> AllCourseMapper()
        {
            IRowMapper<rCourseListByProgram> mapper = MapBuilder<rCourseListByProgram>.MapAllProperties()

            .Map(m => m.ParentNodeID).ToColumn("ParentNodeID")
            .Map(m => m.ChildNodeID).ToColumn("ChildNodeID")
            .Map(m => m.NodeID).ToColumn("NodeID")
            .Map(m => m.Name).ToColumn("Name")
            .Map(m => m.Node_CourseID).ToColumn("Node_CourseID")
            .Map(m => m.CourseID).ToColumn("CourseID")
            .Map(m => m.VersionID).ToColumn("VersionID")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.VersionCode).ToColumn("VersionCode")
            .Map(m => m.Title).ToColumn("Title")

            .Build();

            return mapper;
        }


    }
}
