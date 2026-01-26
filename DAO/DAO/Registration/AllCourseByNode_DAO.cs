using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;

namespace DataAccess
{
   public class AllCourseByNode_DAO
    {
       private const string NODEID_PA = "@NodeId";

       private const string COURSEID = "CourseID";
       private const string VERSIONID = "VersionID";
       private const string NODE_COURSEID = "Node_CourseID";
       private const string FORMALCODE = "FormalCode";
       private const string VERSIONCODE = "VersionCode";
       private const string COURSETITLE = "CourseTitle";



        public static List<AllCourseByNodeEntity> GetAllDataByNodeId(int nodeId)
       {
           List<AllCourseByNodeEntity> entities = new List<AllCourseByNodeEntity>();

            try
            {
                entities = null;
               // string cmd = " sp_AllCourseByNode ";
                string cmd = " DECLARE	@return_value int EXEC	@return_value = [sp_AllCourseByNode] @NodeId = " + NODEID_PA + " SELECT	'Return Value' = @return_value ";
                DAOParameters dps = new DAOParameters();
                dps.AddParameter(NODEID_PA, nodeId);
                List<SqlParameter> ps = Methods.GetSQLParameters(dps);

                SqlDataReader rd = QueryHandler.ExecuteSelectBatchQuery(cmd, ps);
                entities = MapEntities(rd);
                rd.Close();
            }
            catch (Exception ex)
            {
                //FixMe
                throw ex;
            }
            return entities;
        }

        private static List<AllCourseByNodeEntity> MapEntities(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            List<AllCourseByNodeEntity> entities = new List<AllCourseByNodeEntity>();
            while (theReader.Read())
            {
                AllCourseByNodeEntity entity = new AllCourseByNodeEntity();
                entity = Mapper(nullHandler);
                entities.Add(entity);
            }
            return entities;
        }

        private static AllCourseByNodeEntity Mapper(SQLNullHandler nullHandler)
        {
            AllCourseByNodeEntity entity = new AllCourseByNodeEntity();

            entity.CourseID = nullHandler.GetInt32(COURSEID);
            entity.VersionID = nullHandler.GetInt32(VERSIONID);
            entity.Node_CourseID = nullHandler.GetInt32(NODE_COURSEID);
            entity.FormalCode = nullHandler.GetString(FORMALCODE);
            entity.VersionCode = nullHandler.GetString(VERSIONCODE);
            entity.CourseTitle = nullHandler.GetString(COURSETITLE);

            return entity;
        }
    }
}
