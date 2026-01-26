using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;

namespace DataAccess
{
    public class Node_DAO : Base_DAO
    {
        #region Constants

        #region ColNames
        private const string NODEID = "NodeID";

        private const string NAME = "Name";
        private const string NAME_PA = "@Name";

        private const string ISLASTLEVEL = "IsLastLevel";
        private const string ISLASTLEVEL_PA = "@IsLastLevel";

        private const string MINCREDIT = "MinCredit";
        private const string MINCREDIT_PA = "@MinCredit";

        private const string MAXCREDIT = "MaxCredit";
        private const string MAXCREDIT_PA = "@MaxCredit";

        private const string MINCOURSES = "MinCourses";
        private const string MINCOURSES_PA = "@MinCourses";

        private const string MAXCOURSES = "MaxCourses";
        private const string MAXCOURSES_PA = "@MaxCourses";

        private const string ISVIRTUAL = "IsVirtual";
        private const string ISVIRTUAL_PA = "@IsVirtual";

        private const string ISBUNDLE = "IsBundle";
        private const string ISBUNDLE_PA = "@IsBundle";

        private const string ISASSOCIATED = "IsAssociated";
        private const string ISASSOCIATED_PA = "@IsAssociated";

        private const string OPERATORID = "OperatorID";
        private const string OPERATORID_PA = "@OperatorID";

        private const string ISACTIVE = "IsActive";//15
        private const string ISACTIVE_PA = "@IsActive";
        #endregion

        #region Allcolumns
        private const string ALLCOLUMNS = "[" + NODEID + "], "//0
                                + "[" + NAME + "], "//1
                                + "[" + ISLASTLEVEL + "], "//2
                                + "[" + MINCREDIT + "], "//3
                                + "[" + MAXCREDIT + "], "//4
                                + "[" + MINCOURSES + "], "//5
                                + "[" + MAXCOURSES + "], "//6
                                + "[" + ISACTIVE + "], "//7
                                + "[" + ISVIRTUAL + "], "//8
                                + "[" + ISBUNDLE + "], "//9
                                + "[" + ISASSOCIATED + "], "//10
                                + "[" + OPERATORID + "], ";//11
        #endregion

        #region No Pk Cloumns
        private const string NOPKCOLUMNS = "[" + NAME + "], "//1
                                + "[" + ISLASTLEVEL + "], "//2
                                + "[" + MINCREDIT + "], "//3
                                + "[" + MAXCREDIT + "], "//4
                                + "[" + MINCOURSES + "], "//5
                                + "[" + MAXCOURSES + "], "//6
                                + "[" + ISACTIVE + "], "//7
                                + "[" + ISVIRTUAL + "], "//8
                                + "[" + ISBUNDLE + "], "//9
                                + "[" + ISASSOCIATED + "], "//10
                                + "[" + OPERATORID + "], ";//11
        #endregion

        private const string TABLENAME = " [Node] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        #region Insert
        private const string INSERT = "INSERT INTO" + TABLENAME
                    + "("
                    + ALLCOLUMNS
                    + BASECOLUMNS
                    + ") "
                    + "VALUES ( "
                    + ID_PA + ", "//0
                    + NAME_PA + ", "//1
                    + ISLASTLEVEL_PA + ", "//2
                    + MINCREDIT_PA + ", "//3
                    + MAXCREDIT_PA + ", "//4
                    + MINCOURSES_PA + ", "//5
                    + MAXCOURSES_PA + ", "//6
                    + ISACTIVE_PA + ", "//7
                    + ISVIRTUAL_PA + ", "//8
                    + ISBUNDLE_PA + ", "//9
                    + ISASSOCIATED_PA + ", "//10
                    + OPERATORID_PA + ", "//10
                    + CREATORID_PA + ", "
                    + CREATEDDATE_PA + ", "
                    + MODIFIERID_PA + ", "
                    + MOIDFIEDDATE_PA + ")";
        #endregion

        #region Update
        private const string UPDATE = "UPDATE" + TABLENAME
        + "SET " + NAME + " = " + NAME_PA + ", "//1
        + ISLASTLEVEL + " = " + ISLASTLEVEL_PA + ", "//2
        + MINCREDIT + " = " + MINCREDIT_PA + ", "//3
        + MAXCREDIT + " = " + MAXCREDIT_PA + ", "//4
        + MINCOURSES + " = " + MINCOURSES_PA + ", "//5
        + MAXCOURSES + " = " + MAXCOURSES_PA + ", "//6
        + ISACTIVE + " = " + ISACTIVE_PA + ", "//7
        + ISVIRTUAL + " = " + ISVIRTUAL_PA + ", "//8
        + ISBUNDLE + " = " + ISBUNDLE_PA + ", "//9
        + ISASSOCIATED + " = " + ISASSOCIATED_PA + ", "//9
        + OPERATORID + " = " + OPERATORID_PA + ", "//10
        + CREATORID + " = " + CREATORID_PA + ","//
        + CREATEDDATE + " = " + CREATEDDATE_PA + ","//
        + MODIFIERID + " = " + MODIFIERID_PA + ","//
        + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;
        #endregion

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Functions
        private static NodeEntity Mapper(SQLNullHandler nullHandler)
        {
            NodeEntity node = new NodeEntity();
            node.Id = nullHandler.GetInt32(NODEID);
            node.Name = nullHandler.GetString(NAME);
            node.IsLastLevel = nullHandler.GetBoolean(ISLASTLEVEL);
            node.MaxCourses = nullHandler.GetInt32(MAXCOURSES);
            node.MinCourses = nullHandler.GetInt32(MINCOURSES);
            node.MaxCredit = nullHandler.GetDecimal(MAXCREDIT);
            node.MinCredit = nullHandler.GetDecimal(MINCREDIT);
            node.IsVirtual = nullHandler.GetBoolean(ISVIRTUAL);
            node.IsBundle = nullHandler.GetBoolean(ISBUNDLE);
            node.IsAssociated = nullHandler.GetBoolean(ISASSOCIATED);
            node.OperatorID = nullHandler.GetInt32(OPERATORID);
            node.IsActive = nullHandler.GetBoolean(ISACTIVE);
            node.CreatorID = nullHandler.GetInt32(CREATORID);
            node.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            node.ModifierID = nullHandler.GetInt32(MODIFIERID);
            node.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);
            return node;
        }
        private static List<NodeEntity> Maps(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<NodeEntity> nodes = null;
            while (theReader.Read())
            {
                if (nodes == null)
                {
                    nodes = new List<NodeEntity>();
                }
                NodeEntity node = Mapper(nullHandler);
                nodes.Add(node);
            }

            return nodes;
        }
        private static NodeEntity Map(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            NodeEntity node = null;
            if (theReader.Read())
            {
                node = Mapper(nullHandler);
            }

            return node;
        }

        internal static NodeEntity GetNode(int nodeID)
        {
            try
            {
                string cmd = SELECT + " where NodeID = @NodeID";

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter("@NodeID", nodeID);
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                SqlDataReader theReader = QueryHandler.ExecuteSelectQuery(cmd, ps);
                NodeEntity node = Map(theReader);
                theReader.Close();

                return node;
            }
            catch (Exception ex)
            {
                //FixMe
                throw ex;
            }
        }
        /// <summary>
        /// get children from TreeDetail's parent node
        /// </summary>
        /// <param name="parentNodeID"></param>
        /// <returns></returns>
        internal static List<NodeEntity> GetChildrenByParentNode(int parentNodeID)
        {
            List<NodeEntity> nodes = null;

            string command = "DECLARE	@return_value int EXEC	@return_value = [dbo].[sp_GetChildNodesByParent] @nodeID = @ParentNodeID SELECT 'Return Value' = @return_value"; 

            Common.DAOParameters dps = new Common.DAOParameters();
            dps.AddParameter("@ParentNodeID", parentNodeID);

            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

            SqlDataReader theReader = QueryHandler.ExecuteSelectQuery(command, ps); //MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            nodes = Maps(theReader);
            theReader.Close();
            return nodes;
        }
        
        
        #endregion
    }
}
