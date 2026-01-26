using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class VNodeSetMaster : Base
    {
        #region DBcloumns
        //VNodeSetMasterID	int	        Unchecked
        //SetNo	            int	        Unchecked
        //NodeID	        int	        Unchecked
        //RequiredUnits	    money	    Checked
        //CreatedBy	        int	        Unchecked
        //CreatedDate	    datetime	Unchecked
        //ModifiedBy	    int	        Checked
        //ModifiedDate	    datetime	Checked
        #endregion

        #region Variables
        private decimal _requiredUnits;
        private int _ownerNodeID;
        private Node _ownerNode;

        private int _setNo;
        private List<VNodeSet> _vNodeSets;
        #endregion

        #region Constructor
        public VNodeSetMaster()
            : base()
        {
            _requiredUnits = 0;
            _ownerNodeID = 0;
            _ownerNode = null;

            _setNo = 0;
            _vNodeSets = null;
        } 
        #endregion

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
                            + ID_PA +", "
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

        #region Properties

        public int OwnerNodeID
        {
            get { return _ownerNodeID; }
            set { _ownerNodeID = value; }
        }
        private SqlParameter OwnerNodeIDParam
        {
            get
            {
                SqlParameter ownerNodeIDParam = new SqlParameter();
                ownerNodeIDParam.ParameterName = NODE_ID;

                ownerNodeIDParam.Value = OwnerNodeID;

                return ownerNodeIDParam;
            }
        }
        public Node OwnerNode
        {
            get
            {
                if (_ownerNode == null)
                {
                    if (this.OwnerNodeID > 0)
                    {
                        _ownerNode = Node.GetNode(this.OwnerNodeID);
                    }
                }
                return _ownerNode;
            }
        }

        public int SetNo
        {
            get { return _setNo; }
            set { _setNo = value; }
        }
        private SqlParameter SetNoParam
        {
            get
            {
                SqlParameter setNoParam = new SqlParameter();
                setNoParam.ParameterName = SETNO_PA;

                setNoParam.Value = SetNo;

                return setNoParam;
            }
        }

        public string SetName
        {
            get
            {
                return "Set " + _setNo.ToString();
            }
        }

        public decimal RequiredUnits
        {
            get { return _requiredUnits; }
            set { _requiredUnits = value; }
        }
        private SqlParameter RequiredUnitsParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = REQUIREDUNITS_PA;
                if (RequiredUnits<=0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = RequiredUnits; 
                }

                return sqlParam;
            }
        }

        public List<VNodeSet> VNodeSets
        {
            get
            {
                if (_vNodeSets == null)
                {
                    if (this.OwnerNodeID > 0)
                    {
                        _vNodeSets = VNodeSet.GetByOwnerNode(this.OwnerNodeID, this.SetNo);
                    }
                }
                return _vNodeSets;
            }
            set
            {
                _vNodeSets = value;
            }
        }
        #endregion

        #region Methods
        private static VNodeSetMaster vNodeMapper(SQLNullHandler nullHandler)
        {
            VNodeSetMaster vNodeSet = new VNodeSetMaster();

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
        private static List<VNodeSetMaster> mapVNodeSets(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<VNodeSetMaster> vNodeSets = null;

            while (theReader.Read())
            {
                if (vNodeSets == null)
                {
                    vNodeSets = new List<VNodeSetMaster>();
                }
                VNodeSetMaster vNodeSet = vNodeMapper(nullHandler);
                vNodeSets.Add(vNodeSet);
            }

            return vNodeSets;
        }
        private static VNodeSetMaster mapVNodeSet(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            VNodeSetMaster vNodeSet = null;
            if (theReader.Read())
            {
                vNodeSet = new VNodeSetMaster();
                vNodeSet = vNodeMapper(nullHandler);
            }

            return vNodeSet;
        }

        private static bool CheckSet(VNodeSet vNodeSet, int setNo)
        {
            if (vNodeSet.SetNo == setNo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static List<VNodeSetMaster> GetVNodeSetMasters(List<VNodeSet> vNodeSets)
        {
            List<VNodeSetMaster> vNodeSetMasters = null;
            Hashtable setnos = null;

            foreach (VNodeSet vNodeSet in vNodeSets)
            {
                if (vNodeSetMasters == null)
                {
                    vNodeSetMasters = new List<VNodeSetMaster>();
                }
                if (setnos == null)
                {
                    setnos = new Hashtable();
                }

                if (setnos.ContainsKey(vNodeSet.SetNo))
                {
                    continue;
                }
                else
                {
                    setnos.Add(vNodeSet.SetNo, vNodeSet);
                    VNodeSetMaster vNodeSetMaster = new VNodeSetMaster();
                    vNodeSetMaster.SetNo = vNodeSet.SetNo;
                    vNodeSetMaster.OwnerNodeID = vNodeSet.OwnerNodeID;


                    vNodeSetMaster.VNodeSets = new List<VNodeSet>();

                    foreach (VNodeSet vNodeSetInner in vNodeSets)
                    {
                        if (vNodeSet.SetNo == vNodeSetInner.SetNo)
                        {
                            vNodeSetMaster.VNodeSets.Add(vNodeSetInner);
                        }
                    }

                    vNodeSetMasters.Add(vNodeSetMaster);
                }
            }


            return vNodeSetMasters;
        }

        internal static int GetMaxID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newID = 0;

            string command = "SELECT MAX("+ VNODE_SET_MASTERID +") FROM" + TABLENAME;
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

            if (ob == null || ob == DBNull.Value)
            {
                newID = 1;
            }
            else if (ob is Int32)
            {
                newID = Convert.ToInt32(ob) + 1;
            }

            return newID;
        }
        internal static bool IsExist(int setNo, int ownerNodeID,SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            string command = "SELECT COUNT(" + VNODE_SET_MASTERID + ") FROM" + TABLENAME
                            + "WHERE [" + SETNO + "] = " + SETNO_PA + " AND [" + NODE_ID + "] = " + NODE_ID_PA;
            SqlParameter codeParam = MSSqlConnectionHandler.MSSqlParamGenerator(setNo, SETNO_PA);
            SqlParameter nodeParam = MSSqlConnectionHandler.MSSqlParamGenerator(ownerNodeID, NODE_ID_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran, new SqlParameter[] { codeParam,nodeParam });

            return (Convert.ToInt32(ob) > 0);
        }

        public static VNodeSetMaster Get(int vNodeSetMasID)
        {
            VNodeSetMaster vNodeSet = null;

            string command = SELECT
                            + "WHERE " + VNODE_SET_MASTERID + " = " + ID_PA;

            SqlParameter vNodeSetIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(vNodeSetMasID, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { vNodeSetIDparam });

            vNodeSet = mapVNodeSet(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return vNodeSet;
        }
        public static VNodeSetMaster GetByOwnerNodeAndSet(int ownerNodeID, int setNo)
        {
            VNodeSetMaster vNodeSets = null;

            string command = SELECT
                            + "WHERE " + NODE_ID + " = " + NODE_ID_PA + " AND " + SETNO + " = " + SETNO_PA;

            #region SqlParameters
            SqlParameter ownerNodeIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(ownerNodeID, "@NodeID");
            SqlParameter setNoPram = MSSqlConnectionHandler.MSSqlParamGenerator(setNo, "@SetNo");
            #endregion

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { ownerNodeIDParam, setNoPram });
            vNodeSets = mapVNodeSet(theReader);


            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return vNodeSets;
        }

        public static List<VNodeSetMaster> GetByOwnerNode(int ownerNodeID)
        {
            List<VNodeSetMaster> vNodeSets = null;

            string command = SELECT
                            + "WHERE " + NODE_ID + " = " + NODE_ID_PA;

            #region Parameters
            SqlParameter ownerNodeIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(ownerNodeID, NODE_ID_PA);
            #endregion

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { ownerNodeIDparam });

            vNodeSets = mapVNodeSets(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return vNodeSets;
        }

        public static int Insert(VNodeSetMaster vNodeSetMaster)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                if (VNodeSetMaster.IsExist(vNodeSetMaster.SetNo, vNodeSetMaster.OwnerNodeID, sqlConn, sqlTran))
                {
                    throw new Exception("Another set with the same SetNo exits");
                }

                SqlParameter[] sqlParams = null;
                Node.UpdateVirtualFlag(vNodeSetMaster.OwnerNodeID, true, sqlConn, sqlTran);

                string command = string.Empty;

                //if (vNodeSet.Id == 0)
                //{
                command = INSERT;


                vNodeSetMaster.Id = VNodeSetMaster.GetMaxID(sqlConn, sqlTran);
                sqlParams = new SqlParameter[] { vNodeSetMaster.IDParam,
                                                 vNodeSetMaster.OwnerNodeIDParam, 
                                                 vNodeSetMaster.SetNoParam,
                                                 vNodeSetMaster.RequiredUnitsParam,
                                                 vNodeSetMaster.CreatorIDParam,
                                                 vNodeSetMaster.CreatedDateParam,
                                                 vNodeSetMaster.ModifierIDParam,
                                                 vNodeSetMaster.ModifiedDateParam};
                //}
                //else
                //{
                //    command = UPDATE
                //            + " WHERE " + VNODE_SET_MASTERID + " = " + ID_PA;
                //    sqlParams = new SqlParameter[] { vNodeSet.OwnerNodeIDParam, 
                //                                 vNodeSet.SetNoParam,
                //                                 vNodeSet.CreatorIDParam,
                //                                 vNodeSet.CreatedDateParam,
                //                                 vNodeSet.ModifierIDParam,
                //                                 vNodeSet.ModifiedDateParam, 
                //                                 vNodeSet.IDParam };
                //}
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                VNodeSet.DeleteVNode(vNodeSetMaster.OwnerNodeID, vNodeSetMaster.SetNo, sqlConn, sqlTran);

                foreach (VNodeSet item in vNodeSetMaster.VNodeSets)
                {
                    item.VNodeSetMasID = vNodeSetMaster.Id;
                    VNodeSet.SaveVNodeSet(item, sqlConn, sqlTran);
                }

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        public static int DeleteVNodeSetMas(int ownerNodeID, int setNo)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                VNodeSet.DeleteVNode(ownerNodeID, setNo, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE " + NODE_ID + " = " + NODE_ID_PA + " AND " + SETNO + " = " + SETNO_PA;

                SqlParameter ownerNodeIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(ownerNodeID, NODE_ID_PA);
                SqlParameter setNoPram = MSSqlConnectionHandler.MSSqlParamGenerator(setNo, SETNO_PA);

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { ownerNodeIDParam, setNoPram });

                //Node.UpdateVirtualFlag(ownerNodeID, false, sqlConn, sqlTran);

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        public static int DeleteVNodeSetMasWithDeflag(int ownerNodeID, int setNo)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                VNodeSet.DeleteVNode(ownerNodeID, setNo, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE " + NODE_ID + " = " + NODE_ID_PA + " AND " + SETNO + " = " + SETNO_PA;

                SqlParameter ownerNodeIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(ownerNodeID, NODE_ID_PA);
                SqlParameter setNoPram = MSSqlConnectionHandler.MSSqlParamGenerator(setNo, SETNO_PA);

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { ownerNodeIDParam, setNoPram });

                Node.UpdateVirtualFlag(ownerNodeID, false, sqlConn, sqlTran);

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        internal static int DeleteVNode(int vNodeSetMasID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                VNodeSet.DeleteVNodeByVNodeMas(vNodeSetMasID, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE " + VNODE_SET_MASTERID + " = " + ID_PA;

                #region Parameters
                SqlParameter vNodeSetIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(vNodeSetMasID, ID_PA);
                #endregion

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { vNodeSetIDparam });

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        internal static int DeleteVNodeByOwnerNode(int ownerNodeID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = DELETE
                                + "WHERE NodeID = @NodeID";

                #region Parameters
                SqlParameter ownerNodeIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(ownerNodeID, "@NodeID");
                #endregion

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { ownerNodeIDparam });

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        #endregion
    }
}
