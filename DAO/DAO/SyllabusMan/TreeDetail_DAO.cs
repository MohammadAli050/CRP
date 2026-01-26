using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;

namespace DataAccess
{
    public class TreeDetail_DAO : Base_DAO
    {
        #region Constants
        private const string TREE_DETAIL_ID = "TreeDetailID";

        private const string TREE_MASTER_ID = "TreeMasterID";
        private const string TREE_MASTER_ID_PA = "@TreeMasterID";
        private const string PARENT_NODE_ID = "ParentNodeID";
        private const string PARENT_NODE_ID_PA = "@ParentNodeID";
        private const string CHILD_NODE_ID = "ChildNodeID";
        private const string CHILD_NODE_ID_PA = "@ChildNodeID";

        private const string ALLCOLUMNS = TREE_DETAIL_ID + ", "
                                        + TREE_MASTER_ID + ", "
                                        + PARENT_NODE_ID + ", "
                                        + CHILD_NODE_ID + ", ";

        private const string NOPKCOLUMNS = TREE_MASTER_ID + ", "
                                        + PARENT_NODE_ID + ", "
                                        + CHILD_NODE_ID + ", ";

        private const string TABLENAME = " [TreeDetail] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        private const string INSERT = "INSERT INTO" + TABLENAME
                            + "("
                            + NOPKCOLUMNS
                            + BASECOLUMNS
                            + ") "
                            + "VALUES ";

        //private const string UPDATE = string.Empty;

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Functions
        private static TreeDetailEntity Mapper(SQLNullHandler nullHandler)
        {
            TreeDetailEntity treeDetail = new TreeDetailEntity();

            treeDetail.Id = nullHandler.GetInt32("TreeDetailID");
            treeDetail.TreeMasterID = nullHandler.GetInt32("TreeMasterID");
            treeDetail.ParentNodeID = nullHandler.GetInt32("ParentNodeID");
            treeDetail.ChildNodeID = nullHandler.GetInt32("ChildNodeID");
            treeDetail.CreatorID = nullHandler.GetInt32("CreatedBy");
            treeDetail.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            treeDetail.ModifierID = nullHandler.GetInt32("ModifiedBy");
            treeDetail.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");
            return treeDetail;
        }
        private static List<TreeDetailEntity> MapTreeDetails(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<TreeDetailEntity> treeDetails = null;

            while (theReader.Read())
            {
                if (treeDetails == null)
                {
                    treeDetails = new List<TreeDetailEntity>();
                }
                TreeDetailEntity td = Mapper(nullHandler);
                treeDetails.Add(td);
            }

            return treeDetails;
        }
        private static TreeDetailEntity MapTreeDetail(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            TreeDetailEntity treeDetail = null;
            if (theReader.Read())
            {
                treeDetail = new TreeDetailEntity();
                treeDetail = Mapper(nullHandler);
            }

            return treeDetail;
        }

        internal static List<TreeDetailEntity> GetByParentNode(int parentNodeID)
        {
            List<TreeDetailEntity> treeDetails = null;

            string command = SELECT
                            + "WHERE ParentNodeID = " + PARENT_NODE_ID_PA;

            Common.DAOParameters dps = new Common.DAOParameters();
            dps.AddParameter(PARENT_NODE_ID_PA, parentNodeID);

            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

            SqlDataReader theReader = QueryHandler.ExecuteSelectQuery(command, ps); //MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            treeDetails = MapTreeDetails(theReader);
            theReader.Close();
            return treeDetails;
        }
        #endregion
    }
}
