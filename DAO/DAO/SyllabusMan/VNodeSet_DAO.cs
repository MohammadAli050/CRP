using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;

namespace DataAccess
{
    public class VNodeSet_DAO:Base_DAO
    {
        #region Constants
        private const string VNODE_SET_ID = "VNodeSetID";//1

        private const string VNODE_SET_MASTERID = "VNodeSetMasterID";
        private const string VNODE_SET_MASTERID_PA = "@VNodeSetMasterID";

        private const string NODE_ID = "NodeID";//2
        private const string NODE_ID_PA = "@NodeID";

        private const string SETNO = "SetNo";//3
        private const string SETNO_PA = "@SetNo";

        private const string OPERAND_NODE_ID = "OperandNodeID";//4
        private const string OPERAND_NODE_ID_PA = "@OperandNodeID";

        private const string OPERAND_COURSE_ID = "OperandCourseID";//5
        private const string OPERAND_COURSE_ID_PA = "@OperandCourseID";

        private const string OPERAND_VERSION_ID = "OperandVersionID";//6
        private const string OPERAND_VERSION_ID_PA = "@OperandVersionID";

        private const string NODE_COURSE_ID = "NodeCourseID";//5
        private const string NODE_COURSE_ID_PA = "@NodeCourseID";

        private const string OPERATOR_ID = "OperatorID";//7
        private const string OPERATOR_ID_PA = "@OperatorID";

        private const string WILDCARD = "WildCard";//8
        private const string WILDCARD_PA = "@WildCard";

        private const string IS_STUDNT_SPEC = "IsStudntSpec";//9
        private const string IS_STUDNT_SPEC_PA = "@IsStudntSpec";

        private const string ALLCOLUMNS = VNODE_SET_ID + ", "
                                        + VNODE_SET_MASTERID + ", "
                                        + NODE_ID + ", "
                                        + SETNO + ", "
                                        + OPERAND_NODE_ID + ", "
                                        + OPERAND_COURSE_ID + ", "
                                        + OPERAND_VERSION_ID + ", "
                                        + NODE_COURSE_ID + ", "
                                        + OPERATOR_ID + ", "
                                        + WILDCARD + ", "
                                        + IS_STUDNT_SPEC + ", ";

        private const string NOPKCOLUMNS = VNODE_SET_MASTERID + ", "
                                        + NODE_ID + ", "
                                        + SETNO + ", "
                                        + OPERAND_NODE_ID + ", "
                                        + OPERAND_COURSE_ID + ", "
                                        + OPERAND_VERSION_ID + ", "
                                        + NODE_COURSE_ID + ", "
                                        + OPERATOR_ID + ", "
                                        + WILDCARD + ", "
                                        + IS_STUDNT_SPEC + ", ";

        private const string TABLENAME = " [VNodeSet] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        private const string INSERT = "INSERT INTO" + TABLENAME
                            + "("
                            + NOPKCOLUMNS
                            + BASECOLUMNS
                            + ") "
                            + "VALUES "
                            + "("
                            + VNODE_SET_MASTERID_PA + ", "
                            + NODE_ID_PA + ", "
                            + SETNO_PA + ", "
                            + OPERAND_NODE_ID_PA + ", "
                            + OPERAND_COURSE_ID_PA + ", "
                            + OPERAND_VERSION_ID_PA + ", "
                            + NODE_COURSE_ID_PA + ", "
                            + OPERATOR_ID_PA + ", "
                            + WILDCARD_PA + ", "
                            + IS_STUDNT_SPEC_PA + ", "
                            + CREATORID_PA + ", "
                            + CREATEDDATE_PA + ", "
                            + MODIFIERID_PA + ", "
                            + MOIDFIEDDATE_PA
                            + ")";

        #region Old
        //private const string INSERTC = "INSERT INTO [VNodeSet] "
        //    + "([NodeID], "
        //    + "[SetNo], "
        //    + "[OperandCourseID], "
        //    + "[OperandVersionID], "
        //    + "[OperatorID]) "
        //    + "VALUES ";

        //private const string INSERTN = "INSERT INTO [VNodeSet] "
        //                    + "([NodeID], "
        //                    + "[SetNo], "
        //                    + "[OperandNodeID], "
        //                    + "[OperatorID]) "
        //                    + "VALUES ";

        //private const string INSERTCW = "INSERT INTO [VNodeSet] "
        //                    + "([NodeID], "
        //                    + "[SetNo], "
        //                    + "[OperandCourseID], "
        //                    + "[OperandVersionID], "
        //                    + "[OperatorID], "
        //                    + "[WildCard]) "
        //                    + "VALUES ";

        //private const string INSERTNW = "INSERT INTO [VNodeSet] "
        //            + "([NodeID], "
        //            + "[SetNo], "
        //            + "[OperandNodeID], "
        //            + "[OperatorID], "
        //            + "[WildCard]) "
        //            + "VALUES "; 
        #endregion

        private const string UPDATE = "UPDATE" + TABLENAME
                        + "SET " + VNODE_SET_MASTERID + " = " + VNODE_SET_MASTERID_PA + ", "
                        + NODE_ID + " = " + NODE_ID_PA + ", "
                        + SETNO + " = " + SETNO_PA + ", "
                        + OPERAND_NODE_ID + " = " + OPERAND_NODE_ID_PA + ", "
                        + OPERAND_COURSE_ID + " = " + OPERAND_COURSE_ID_PA + ", "
                        + OPERAND_VERSION_ID + " = " + OPERAND_VERSION_ID_PA + ", "
                        + NODE_COURSE_ID + " = " + NODE_COURSE_ID_PA + ", "
                        + OPERATOR_ID + " = " + OPERATOR_ID_PA + ", "
                        + WILDCARD + " = " + WILDCARD_PA + ", "
                        + IS_STUDNT_SPEC + " = " + IS_STUDNT_SPEC_PA + ", "
                        + CREATORID + " = " + CREATORID_PA + ","//7
                        + CREATEDDATE + " = " + CREATEDDATE_PA + ","//8
                        + MODIFIERID + " = " + MODIFIERID_PA + ","//9
                        + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Functions
        private static VNodeSetEntity vNodeMapper(SQLNullHandler nullHandler)
        {
            VNodeSetEntity vNodeSet = new VNodeSetEntity();

            vNodeSet.Id = nullHandler.GetInt32(VNODE_SET_ID);
            vNodeSet.VNodeSetMasID = nullHandler.GetInt32(VNODE_SET_MASTERID);
            vNodeSet.OwnerNodeID = nullHandler.GetInt32(NODE_ID);
            vNodeSet.SetNo = nullHandler.GetInt32(SETNO);
            vNodeSet.OperatorID = nullHandler.GetInt32(OPERATOR_ID);
            vNodeSet.OperandNodeID = nullHandler.GetInt32(OPERAND_NODE_ID);
            vNodeSet.OperandCourseID = nullHandler.GetInt32(OPERAND_COURSE_ID);
            vNodeSet.OperandVersionID = nullHandler.GetInt32(OPERAND_VERSION_ID);
            vNodeSet.NodeCourseID = nullHandler.GetInt32(NODE_COURSE_ID);
            vNodeSet.WildCard = nullHandler.GetString(WILDCARD);
            vNodeSet.IsStudntSpec = nullHandler.GetBoolean(IS_STUDNT_SPEC);
            vNodeSet.CreatorID = nullHandler.GetInt32(CREATORID);
            vNodeSet.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            vNodeSet.ModifierID = nullHandler.GetInt32(MODIFIERID);
            vNodeSet.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);
            return vNodeSet;
        }
        private static List<VNodeSetEntity> mapVNodeSets(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<VNodeSetEntity> vNodeSets = null;

            while (theReader.Read())
            {
                if (vNodeSets == null)
                {
                    vNodeSets = new List<VNodeSetEntity>();
                }
                VNodeSetEntity vNodeSet = vNodeMapper(nullHandler);
                vNodeSets.Add(vNodeSet);
            }

            return vNodeSets;
        }
        private static VNodeSetEntity mapVNodeSet(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            VNodeSetEntity vNodeSet = null;
            if (theReader.Read())
            {
                vNodeSet = new VNodeSetEntity();
                vNodeSet = vNodeMapper(nullHandler);
            }

            return vNodeSet;
        }
        internal static List<VNodeSetEntity> GetByOwnerNode(int ownerNodeID)
        {
            List<VNodeSetEntity> vNodeSets = null;

            string cmd = SELECT
                            + "WHERE NodeID = @NodeID";

            Common.DAOParameters dps = new Common.DAOParameters();
            dps.AddParameter("@NodeID", ownerNodeID);
            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

            SqlDataReader theReader = QueryHandler.ExecuteSelectQuery(cmd, ps);
            
            vNodeSets = mapVNodeSets(theReader);
            theReader.Close();

            return vNodeSets;
        }
        internal static List<VNodeSetEntity> GetByMasterID(int masID)
        {
            List<VNodeSetEntity> vNodeSets = null;

            string cmd = SELECT
                            + "WHERE " + VNODE_SET_MASTERID + " = " + VNODE_SET_MASTERID_PA;

            Common.DAOParameters dps = new Common.DAOParameters();
            dps.AddParameter(VNODE_SET_MASTERID_PA, masID);
            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

            SqlDataReader theReader = QueryHandler.ExecuteSelectQuery(cmd, ps);

            vNodeSets = mapVNodeSets(theReader);
            theReader.Close();

            return vNodeSets;
        }
        internal static VNodeSetEntity Get(int vNodeSetID)
        {
            VNodeSetEntity vNodeSet = null;

            string cmd = SELECT
                            + "WHERE VNodeSetID = @VNodeSetID";

            Common.DAOParameters dps = new Common.DAOParameters();
            dps.AddParameter("@VNodeSetID", vNodeSetID);
            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

            SqlDataReader theReader = QueryHandler.ExecuteSelectQuery(cmd, ps);
            vNodeSet = mapVNodeSet(theReader);
            theReader.Close();

            return vNodeSet;
        }

        #endregion
    }
}
