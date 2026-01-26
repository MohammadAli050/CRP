using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;

namespace DataAccess
{
    public class VNodeSetMaster_DAO:Base_DAO
    {
        #region Constants
        private const string VNODE_SET_MASTERID = "VNodeSetMasterID";//1

        private const string REQUIREDUNITS = "RequiredUnits";//2
        private const string REQUIREDUNITS_PA = "@RequiredUnits";

        private const string NODE_ID = "NodeID";//2
        private const string NODE_ID_PA = "@NodeID";

        private const string SETNO = "SetNo";//3
        private const string SETNO_PA = "@SetNo";

        private const string ALLCOLUMNS = VNODE_SET_MASTERID + ", "
                                        + NODE_ID + ", "
                                        + SETNO + ", "
                                        + REQUIREDUNITS + ", ";

        private const string NOPKCOLUMNS = NODE_ID + ", "
                                        + SETNO + ", "
                                        + REQUIREDUNITS + ", ";

        private const string TABLENAME = " [VNodeSetMaster] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        private const string INSERT = "INSERT INTO" + TABLENAME
                            + "("
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + ") "
                            + "VALUES "
                            + "("
                            + ID_PA + ", "
                            + NODE_ID_PA + ", "
                            + SETNO_PA + ", "
                            + REQUIREDUNITS_PA + ", "
                            + CREATORID_PA + ", "
                            + CREATEDDATE_PA + ", "
                            + MODIFIERID_PA + ", "
                            + MOIDFIEDDATE_PA
                            + ")";

        private const string UPDATE = "UPDATE" + TABLENAME
                        + "SET " + NODE_ID + " = " + NODE_ID_PA + ", "
                        + SETNO + " = " + SETNO_PA + ", "
                        + REQUIREDUNITS + " = " + REQUIREDUNITS_PA + ", "
                        + CREATORID + " = " + CREATORID_PA + ","//7
                        + CREATEDDATE + " = " + CREATEDDATE_PA + ","//8
                        + MODIFIERID + " = " + MODIFIERID_PA + ","//9
                        + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Methods
        private static VNodeSetMasterEntity vNodeMapper(SQLNullHandler nullHandler)
        {
            VNodeSetMasterEntity vNodeSet = new VNodeSetMasterEntity();

            vNodeSet.Id = nullHandler.GetInt32(VNODE_SET_MASTERID);
            vNodeSet.OwnerNodeID = nullHandler.GetInt32(NODE_ID);
            vNodeSet.SetNo = nullHandler.GetInt32(SETNO);
            vNodeSet.RequiredUnits = nullHandler.GetDecimal(REQUIREDUNITS);
            vNodeSet.CreatorID = nullHandler.GetInt32(CREATORID);
            vNodeSet.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            vNodeSet.ModifierID = nullHandler.GetInt32(MODIFIERID);
            vNodeSet.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);
            return vNodeSet;
        }
        private static List<VNodeSetMasterEntity> mapVNodeSets(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<VNodeSetMasterEntity> vNodeSets = null;

            while (theReader.Read())
            {
                if (vNodeSets == null)
                {
                    vNodeSets = new List<VNodeSetMasterEntity>();
                }
                VNodeSetMasterEntity vNodeSet = vNodeMapper(nullHandler);
                vNodeSets.Add(vNodeSet);
            }

            return vNodeSets;
        }
        private static VNodeSetMasterEntity mapVNodeSet(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            VNodeSetMasterEntity vNodeSet = null;
            if (theReader.Read())
            {
                vNodeSet = new VNodeSetMasterEntity();
                vNodeSet = vNodeMapper(nullHandler);
            }

            return vNodeSet;
        }

        internal static List<VNodeSetMasterEntity> GetByOwnerNode(int ownerNodeID)
        {
            List<VNodeSetMasterEntity> vNodeSets = null;

            string cmd = SELECT
                            + "WHERE " + NODE_ID + " = " + NODE_ID_PA;

            Common.DAOParameters dps = new Common.DAOParameters();
            dps.AddParameter("@NodeID", ownerNodeID);
            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

            SqlDataReader theReader = QueryHandler.ExecuteSelectQuery(cmd, ps);

            vNodeSets = mapVNodeSets(theReader);
            theReader.Close();
            return vNodeSets;
        }


        #endregion
    }
}
