using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class Node : Base
    {

        #region Variables
        private string _name;
        private bool _isLastLevel;
        private decimal _minCredit;
        private decimal _maxCredit;
        private int _maxCourses;
        private int _minCourses;
        private bool _isVirtual;
        private bool _isBundle;
        private bool _isAssociated;
        private int _operatorID;
        private bool _isActive;
        private Operator _operator;
        private List<VNodeSet> _vNodeSets = null;
        private List<VNodeSetMaster> _vNodeSetMases = null;
        private List<NodeCourse> _node_Courses = null;
        private List<PreRequisiteMaster> _preReqMasters = null;
        private bool _hasPreriquisite = false;
        private bool _isMajor = false;
        #endregion

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

        private const string ISACTIVE = "IsActive";
        private const string ISACTIVE_PA = "@IsActive";

        private const string ISMAJOR = "IsMajor";
        private const string ISMAJOR_PA = "@IsMajor";
        #endregion

        #region Allcolumns
        private const string ALLCOLUMNS = "[" + NODEID + "], "
                                + "[" + NAME + "], "
                                + "[" + ISLASTLEVEL + "], "
                                + "[" + MINCREDIT + "], "
                                + "[" + MAXCREDIT + "], "
                                + "[" + MINCOURSES + "], "
                                + "[" + MAXCOURSES + "], "
                                + "[" + ISACTIVE + "], "
                                + "[" + ISVIRTUAL + "], "
                                + "[" + ISBUNDLE + "], "
                                + "[" + ISASSOCIATED + "], "
                                + "[" + OPERATORID + "], "
                                + "[" + ISMAJOR + "], ";
        #endregion

        #region No Pk Cloumns
        private const string NOPKCOLUMNS = "[" + NAME + "], "
                                + "[" + ISLASTLEVEL + "], "
                                + "[" + MINCREDIT + "], "
                                + "[" + MAXCREDIT + "], "
                                + "[" + MINCOURSES + "], "
                                + "[" + MAXCOURSES + "], "
                                + "[" + ISACTIVE + "], "
                                + "[" + ISVIRTUAL + "], "
                                + "[" + ISBUNDLE + "], "
                                + "[" + ISASSOCIATED + "], "
                                + "[" + OPERATORID + "], "
                                + "[" + ISMAJOR + "], ";
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
                    + ID_PA + ", "
                    + NAME_PA + ", "
                    + ISLASTLEVEL_PA + ", "
                    + MINCREDIT_PA + ", "
                    + MAXCREDIT_PA + ", "
                    + MINCOURSES_PA + ", "
                    + MAXCOURSES_PA + ", "
                    + ISACTIVE_PA + ", "
                    + ISVIRTUAL_PA + ", "
                    + ISBUNDLE_PA + ", "
                    + ISASSOCIATED_PA + ", "
                    + OPERATORID_PA + ", "
                     + ISMAJOR_PA + ", "
                    + CREATORID_PA + ", "
                    + CREATEDDATE_PA + ", "
                    + MODIFIERID_PA + ", "
                    + MOIDFIEDDATE_PA +  ")";
        #endregion

        #region Update
        private const string UPDATE = "UPDATE" + TABLENAME
        + "SET " + NAME + " = " + NAME_PA + ", "
        + ISLASTLEVEL + " = " + ISLASTLEVEL_PA + ", "
        + MINCREDIT + " = " + MINCREDIT_PA + ", "
        + MAXCREDIT + " = " + MAXCREDIT_PA + ", "
        + MINCOURSES + " = " + MINCOURSES_PA + ", "
        + MAXCOURSES + " = " + MAXCOURSES_PA + ", "
        + ISACTIVE + " = " + ISACTIVE_PA + ", "
        + ISVIRTUAL + " = " + ISVIRTUAL_PA + ", "
        + ISBUNDLE + " = " + ISBUNDLE_PA + ", "
        + ISASSOCIATED + " = " + ISASSOCIATED_PA + ", "
        + OPERATORID + " = " + OPERATORID_PA + ", "
        + CREATORID + " = " + CREATORID_PA + ", "
        + CREATEDDATE + " = " + CREATEDDATE_PA + ", "
        + MODIFIERID + " = " + MODIFIERID_PA + ", "
        + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA + ", "
         + ISMAJOR + " = " + ISMAJOR_PA;
        #endregion

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Constructor
        public Node()
            : base()
        {
            _name = string.Empty;
            _isVirtual = false;
            _isBundle = false;
            _isLastLevel = false;
            _isAssociated = false;
            _minCredit = 0;
            _maxCredit = 0;
            _maxCourses = 0;
            _minCourses = 0;
            _operatorID = 0;
            _isActive = true;

            _vNodeSets = null;
            _vNodeSetMases = null;
            _node_Courses = null;
            _preReqMasters = null;
            _isMajor = false;
        }
        #endregion

        #region Properties
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private SqlParameter NameParam
        {
            get
            {
                SqlParameter nameParam = new SqlParameter();
                nameParam.ParameterName = NAME_PA;
                nameParam.Value = Name;
                return nameParam;
            }
        }

        public bool IsLastLevel
        {
            get { return _isLastLevel; }
            set { _isLastLevel = value; }
        }
        private SqlParameter IsLastLevelParam
        {
            get
            {
                SqlParameter isLastLevelParam = new SqlParameter();
                isLastLevelParam.ParameterName = ISLASTLEVEL_PA;
                isLastLevelParam.Value = IsLastLevel;
                return isLastLevelParam;
            }
        }

        public decimal MinCredit
        {
            get { return _minCredit; }
            set { _minCredit = value; }
        }
        private SqlParameter MinCreditParam
        {
            get
            {
                SqlParameter isLastLevelParam = new SqlParameter();
                isLastLevelParam.ParameterName = MINCREDIT_PA;
                if (MinCredit > 0)
                {
                    isLastLevelParam.Value = MinCredit;
                }
                else
                {
                    isLastLevelParam.Value = DBNull.Value;
                }
                return isLastLevelParam;
            }
        }

        public decimal MaxCredit
        {
            get { return _maxCredit; }
            set { _maxCredit = value; }
        }
        private SqlParameter MaxCreditParam
        {
            get
            {
                SqlParameter isLastLevelParam = new SqlParameter();
                isLastLevelParam.ParameterName = MAXCREDIT_PA;
                if (MaxCredit > 0)
                {
                    isLastLevelParam.Value = MaxCredit;
                }
                else
                {
                    isLastLevelParam.Value = DBNull.Value;
                }
                return isLastLevelParam;
            }
        }

        public int MaxCourses
        {
            get { return _maxCourses; }
            set { _maxCourses = value; }
        }
        private SqlParameter MaxCoursesParam
        {
            get
            {
                SqlParameter isLastLevelParam = new SqlParameter();
                isLastLevelParam.ParameterName = MAXCOURSES_PA;
                if (MaxCourses > 0)
                {
                    isLastLevelParam.Value = MaxCourses;
                }
                else
                {
                    isLastLevelParam.Value = DBNull.Value;
                }
                return isLastLevelParam;
            }
        }

        public int MinCourses
        {
            get { return _minCourses; }
            set { _minCourses = value; }
        }
        private SqlParameter MinCoursesParam
        {
            get
            {
                SqlParameter isLastLevelParam = new SqlParameter();
                isLastLevelParam.ParameterName = MINCOURSES_PA;
                if (MinCourses > 0)
                {
                    isLastLevelParam.Value = MinCourses;
                }
                else
                {
                    isLastLevelParam.Value = DBNull.Value;
                }
                return isLastLevelParam;
            }
        }

        public bool IsVirtual
        {
            get { return _isVirtual; }
            set { _isVirtual = value; }
        }
        private SqlParameter IsVirtualParam
        {
            get
            {
                SqlParameter isVirtualParam = new SqlParameter();
                isVirtualParam.ParameterName = ISVIRTUAL_PA;
                isVirtualParam.Value = IsVirtual;
                return isVirtualParam;
            }
        }

        public bool IsBundle
        {
            get { return _isBundle; }
            set { _isBundle = value; }
        }
        private SqlParameter IsBundleParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ISBUNDLE_PA;
                sqlParam.Value = IsBundle;
                return sqlParam;
            }
        }

        public bool IsAssociated
        {
            get { return _isAssociated; }
            set { _isAssociated = value; }
        }
        private SqlParameter IsAssociatedParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ISASSOCIATED_PA;
                sqlParam.Value = IsAssociated;
                return sqlParam;
            }
        }

        public int OperatorID
        {
            get { return _operatorID; }
            set { _operatorID = value; }
        }
        private SqlParameter OperatorIDParam
        {
            get
            {
                SqlParameter operatorIDParam = new SqlParameter();
                operatorIDParam.ParameterName = OPERATORID_PA;
                if (OperatorID == 0)
                {
                    operatorIDParam.Value = DBNull.Value;
                }
                else
                {
                    operatorIDParam.Value = OperatorID;
                }
                return operatorIDParam;
            }
        }
        public Operator Operator
        {
            get
            {
                if (_operator == null)
                {
                    if (this.OperatorID > 0)
                    {
                        _operator = Operator.GetOperator(this.OperatorID);
                    }
                }
                return _operator;
            }
        }

        #region Is Active
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        private SqlParameter IsActiveParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ISACTIVE_PA;

                sqlParam.Value = IsActive;
                return sqlParam;
            }
        }
        #endregion

        public List<VNodeSet> VNodeSets
        {
            get
            {
                if (_vNodeSets == null)
                {
                    if (this.Id > 0)
                    {
                        _vNodeSets = VNodeSet.GetByOwnerNode(this.Id);
                    }
                }
                return _vNodeSets;
            }
        }
        public List<VNodeSetMaster> VNodeSetMasters
        {
            get
            {
                if (_vNodeSetMases == null)
                {
                    if (this.Id > 0)
                    {
                        _vNodeSetMases = VNodeSetMaster.GetByOwnerNode(this.Id);
                    }
                }
                return _vNodeSetMases;
                //return VNodeSetMaster.GetVNodeSetMasters(this.VNodeSets);
            }
        }
        public List<NodeCourse> Node_Courses
        {
            get
            {
                if (_node_Courses == null)
                {
                    if (this.Id > 0)
                    {
                        _node_Courses = NodeCourse.GetByParentNode(this.Id);
                    }
                }
                return _node_Courses;
            }
        }

        public List<PreRequisiteMaster> PreReqMasters
        {
            get
            {
                if (_preReqMasters == null)
                {
                    _preReqMasters = PreRequisiteMaster.GetMastersByNode(this.Id);
                }
                return _preReqMasters;
            }
            set { _preReqMasters = value; }
        }

        public bool HasPreriquisite
        {
            get
            {
                if (_preReqMasters != null)
                {
                    if (_preReqMasters.Count > 0)
                    {
                        _hasPreriquisite = true;
                    }
                }
                return _hasPreriquisite;
            }
            set { _hasPreriquisite = value; }
        }




        public bool IsMajor
        {
            get { return _isMajor; }
            set { _isMajor = value; }
        }
        private SqlParameter IsMajorParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ISMAJOR_PA;
                sqlParam.Value = IsMajor;
                return sqlParam;
            }
        }


        #endregion

        #region Functions
        private static Node nodeMapper(SQLNullHandler nullHandler)
        {
            Node node = new Node();
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
            node.IsMajor = nullHandler.GetBoolean(ISMAJOR);
            return node;
        }
        private static List<Node> mapNodes(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Node> nodes = null;
            while (theReader.Read())
            {
                if (nodes == null)
                {
                    nodes = new List<Node>();
                }
                Node node = nodeMapper(nullHandler);
                nodes.Add(node);
            }

            return nodes;
        }
        private static Node mapNode(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            Node node = null;
            if (theReader.Read())
            {
                node = nodeMapper(nullHandler);
            }

            return node;
        }

        internal static int GetMaxNodeID(SqlConnection sqlConn)
        {
            int newNodeID = 0;

            string command = "SELECT MAX(NodeID) FROM" + TABLENAME;
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

            if (ob == null || ob == DBNull.Value)
            {
                newNodeID = 1;
            }
            else if (ob is Int32)
            {
                newNodeID = Convert.ToInt32(ob) + 1;
            }

            return newNodeID;
        }
        internal static int GetMaxNodeID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newNodeID = 0;

            string command = "SELECT MAX(NodeID) FROM" + TABLENAME;
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

            if (ob == null || ob == DBNull.Value)
            {
                newNodeID = 1;
            }
            else if (ob is Int32)
            {
                newNodeID = Convert.ToInt32(ob) + 1;
            }

            return newNodeID;
        }

        public static List<Node> GetLastLevelNodes()
        {
            string command = SELECT
                            + "WHERE IsLastLevel = 1";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<Node> nodes = mapNodes(theReader);

            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return nodes;
        }
        public static List<Node> GetNodes()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<Node> nodes = mapNodes(theReader);

            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return nodes;
        }
        public static List<Node> GetNonLastLevelNodes()
        {
            string command = SELECT
                            + "WHERE IsLastLevel <> 1";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<Node> nodes = mapNodes(theReader);

            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return nodes;
        }
        public static List<Node> GetNodes(string param)
        {
            string command = SELECT
                            + "WHERE [Name] Like '%" + param + "%'";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<Node> nodes = mapNodes(theReader);

            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return nodes;
        }

        public static List<Node> GetNodesbyRoot(int treeMasID)
        {
            List<Node> nodes = new List<Node>();
            TreeMaster treeMaster = TreeMaster.Get(treeMasID);
            List<TreeDetail> treeDetail = TreeDetail.GetByMasterID(treeMaster.Id);
            if (treeDetail != null)
            {
                foreach (TreeDetail td in treeDetail)
                {
                    Node node = GetNode(td.ChildNodeID);
                    if (node != null)
                    {
                        nodes.Add(node);
                    }
                }
            }

            return nodes;
        }


        public static Node GetNode(int nodeID)
        {
            Node node = new Node();

            string command = SELECT
                            + "WHERE NodeID = @NodeID";// +nodeID.ToString().Trim();

            SqlParameter sqlParam = new SqlParameter("@NodeID", nodeID);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });

            node = mapNode(theReader);

            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return node;
        }
        internal static Node GetNode(int nodeID, SqlConnection sqlConn)
        {
            Node node = new Node();

            string command = SELECT
                            + "WHERE NodeID = @NodeID";// +nodeID.ToString().Trim();

            SqlParameter sqlParam = new SqlParameter("@NodeID", nodeID);

            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });

            node = mapNode(theReader);

            theReader.Close();
            return node;
        }

        public static int SaveNode(Node node)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                #region Sequence
                //+ ID_PA + ", "//0
                //+ NAME_PA + ", "//1
                //+ ISLASTLEVEL_PA + ", "//2
                //+ MINCREDIT_PA + ", "//3
                //+ MAXCREDIT_PA + ", "//4
                //+ MINCOURSES_PA + ", "//5
                //+ MAXCOURSES_PA + ", "//6
                //+ ISACTIVE_PA + ", "//7
                //+ ISVIRTUAL_PA + ", "//8
                //+ ISBUNDLE_PA + ", "//9
                //+ OPERATORID_PA + ", "//10 
                #endregion

                if (node.Id == 0)
                {
                    #region Insert
                    node.Id = Node.GetMaxNodeID(sqlConn, sqlTran);
                    command = INSERT;
                    sqlParams = new SqlParameter[] { node.IDParam,
                                                 node.NameParam,  
                                                 node.IsLastLevelParam, 
                                                 node.MinCreditParam, 
                                                 node.MaxCreditParam,  
                                                 node.MinCoursesParam,  
                                                 node.MaxCoursesParam,
                                                 node.IsActiveParam,
                                                 node.IsVirtualParam,
                                                 node.IsBundleParam,
                                                 node.IsAssociatedParam,
                                                 node.OperatorIDParam,
                                                 node.CreatorIDParam,
                                                 node.CreatedDateParam,
                                                 node.ModifierIDParam,
                                                 node.ModifiedDateParam,
                                                 node.IsMajorParam};
                    #endregion
                }
                else
                {
                    #region Update
                    command = UPDATE
                    + " WHERE [" + NODEID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { node.NameParam,  
                                                 node.IsLastLevelParam, 
                                                 node.MinCreditParam, 
                                                 node.MaxCreditParam,  
                                                 node.MinCoursesParam,  
                                                 node.MaxCoursesParam,
                                                 node.IsActiveParam,
                                                 node.IsVirtualParam,
                                                 node.IsBundleParam,
                                                 node.IsAssociatedParam,                                                 
                                                 node.OperatorIDParam,
                                                 node.CreatorIDParam,
                                                 node.CreatedDateParam,
                                                 node.ModifierIDParam,
                                                 node.ModifiedDateParam,
                                                 node.IDParam,
                                                 node.IsMajorParam};
                    #endregion
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                PreRequisiteMaster.DeleteByNode(node.Id, sqlConn, sqlTran);

                if (node.HasPreriquisite)
                {
                    foreach (PreRequisiteMaster item in node.PreReqMasters)
                    {
                        item.Node_ID = node.Id;
                        PreRequisiteMaster.Save(item, sqlConn, sqlTran);
                    }
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
        internal static int SaveNode(Node node, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = string.Empty;
                SqlParameter[] sqlParams = null;


                if (node.Id == 0)
                {
                    #region Insert
                    node.Id = Node.GetMaxNodeID(sqlConn, sqlTran);
                    command = INSERT;
                    sqlParams = new SqlParameter[] { node.IDParam,
                                                 node.NameParam,  
                                                 node.IsLastLevelParam, 
                                                 node.MinCreditParam, 
                                                 node.MaxCreditParam,  
                                                 node.MinCoursesParam,  
                                                 node.MaxCoursesParam,
                                                 node.IsActiveParam,
                                                 node.IsVirtualParam,
                                                 node.IsBundleParam,
                                                 node.IsAssociatedParam,
                                                 node.OperatorIDParam,
                                                 node.IsMajorParam,
                                                 node.CreatorIDParam,
                                                 node.CreatedDateParam,
                                                 node.ModifierIDParam,
                                                 node.ModifiedDateParam
                                                };
                    #endregion
                }
                else
                {
                    #region Update
                    command = UPDATE
                    + " WHERE [" + NODEID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { node.NameParam,  
                                                 node.IsLastLevelParam, 
                                                 node.MinCreditParam, 
                                                 node.MaxCreditParam,  
                                                 node.MinCoursesParam,  
                                                 node.MaxCoursesParam,
                                                 node.IsActiveParam,
                                                 node.IsVirtualParam,
                                                 node.IsBundleParam,
                                                 node.IsAssociatedParam,
                                                 node.OperatorIDParam,
                                                 node.CreatorIDParam,
                                                 node.CreatedDateParam,
                                                 node.ModifierIDParam,
                                                 node.ModifiedDateParam,
                                                 node.IDParam,
                                                 node.IsMajorParam};
                    #endregion
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                PreRequisiteMaster.DeleteByNode(node.Id, sqlConn, sqlTran);

                if (node.HasPreriquisite)
                {
                    foreach (PreRequisiteMaster item in node.PreReqMasters)
                    {
                        item.Node_ID = node.Id;
                        PreRequisiteMaster.Save(item, sqlConn, sqlTran);
                    }
                }


                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        internal static int UpdateLastLvlFlag(int nodeID, bool isLastLevel, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = string.Empty;
                command = "UPDATE " + TABLENAME
                        + "SET IsLastLevel = @IsLastLevel"// + (isLastLevel ? "1" : "0")
                        + " WHERE NodeID = @NodeID";// +nodeID;

                SqlParameter lstLvlParam = new SqlParameter("@IsLastLevel", isLastLevel);
                SqlParameter nodeIDParam = new SqlParameter("@NodeID", nodeID);

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { lstLvlParam, nodeIDParam });

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        internal static int UpdateVirtualFlag(int nodeID, bool isVirtual, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = string.Empty;
                command = "UPDATE " + TABLENAME
                        + "SET IsVirtual = @IsVirtual" //+ (isVirtual ? "1" : "0")
                        + " WHERE NodeID = @NodeID";// +nodeID;

                SqlParameter isVirtualParam = new SqlParameter("@IsVirtual", isVirtual);
                SqlParameter nodeIDParam = new SqlParameter("@NodeID", nodeID);

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { isVirtualParam, nodeIDParam });

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        public static int DeleteNode(int nodeID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                VNodeSetMaster.DeleteVNodeByOwnerNode(nodeID, sqlConn, sqlTran);
                VNodeSet.DeleteVNodeByOperandNodeID(nodeID, sqlConn, sqlTran);
                //VNodeSet.DeleteVNodeByOwnerNode(nodeID, sqlConn, sqlTran);
                PreReqDetail.DeleteByPreReqNodeID(nodeID, sqlConn, sqlTran);

                PreRequisiteMaster.DeleteByNode(nodeID, sqlConn, sqlTran);
                NodeCourse.DeleteNode_CourseByParentNode(nodeID, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE NodeID = @NodeID";// +nodeID.ToString().Trim();

                SqlParameter sqlParam = new SqlParameter("@NodeID", nodeID);
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { sqlParam });

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
        internal static int DeleteNode(int nodeID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                VNodeSet.DeleteVNodeByOwnerNode(nodeID, sqlConn, sqlTran);
                VNodeSet.DeleteVNodeByOperandNodeID(nodeID, sqlConn, sqlTran);
                VNodeSetMaster.DeleteVNodeByOwnerNode(nodeID, sqlConn, sqlTran);
                PreRequisiteMaster.DeleteByNode(nodeID, sqlConn, sqlTran);
                PreReqDetail.DeleteByPreReqNodeID(nodeID, sqlConn, sqlTran);
                NodeCourse.DeleteNode_CourseByParentNode(nodeID, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE NodeID = @NodeID";// +nodeID.ToString().Trim();

                SqlParameter sqlParam = new SqlParameter("@NodeID", nodeID);
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { sqlParam });

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        public static Node GetLastLevelNode(int nodeID)
        {
            Node node = new Node();

            string command = SELECT
                            + "WHERE NodeID = @NodeID AND IsLastLevel = 1";// +nodeID.ToString().Trim();

            SqlParameter sqlParam = new SqlParameter("@NodeID", nodeID);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });

            node = mapNode(theReader);

            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return node;
        }
        #endregion
    }
}
