using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class VNodeSet :Base
    {
        #region Variables
        private int _vNodeSetMasID;
        private VNodeSetMaster _vNodeSetMaster;

        private int _ownerNodeID;
        private Node _ownerNode;

        private int _setNo;
        private int _operatorID;
        private Operator _operator;

        private int _operandNodeID;
        private Node _operandNode;

        private int _operandCourseID;
        private int _operandVersionID;
        private Course _operandCourse;


        private int _nodeCourseID;
        private NodeCourse _nodeCourse;

        private string _wildCard;
        private bool _isStudntSpec;
        #endregion

        #region Constructor
        public VNodeSet()
            : base()
        {
            _vNodeSetMasID = 0;
            _vNodeSetMaster = null;

            _ownerNodeID = 0;
            _ownerNode = null;

            _setNo = 0;
            _operatorID = 0;

            _operandNodeID = 0;
            _operandNode = null;

            _operandCourseID = 0;
            _operandVersionID = 0;
            _operandCourse = null;

            _nodeCourseID = 0;
            _nodeCourse = null;

            _wildCard = string.Empty;
            _isStudntSpec = false;
        }
        #endregion

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

        #region Properties
        public int VNodeSetMasID
        {
            get { return _vNodeSetMasID; }
            set { _vNodeSetMasID = value; }
        }
        private SqlParameter VNodeSetMasIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = VNODE_SET_MASTERID_PA;

                sqlParam.Value = VNodeSetMasID;

                return sqlParam;
            }
        }
        public VNodeSetMaster SetMaster
        {
            get
            {
                if (_vNodeSetMaster == null)
                {
                    if (this.VNodeSetMasID > 0)
                    {
                        _vNodeSetMaster = VNodeSetMaster.Get(this.VNodeSetMasID);
                    }
                }
                return _vNodeSetMaster;
            }
        }

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
                operatorIDParam.ParameterName = OPERATOR_ID_PA;

                operatorIDParam.Value = OperatorID;

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

        public int OperandNodeID
        {
            get { return _operandNodeID; }
            set { _operandNodeID = value; }
        }
        public Node OperandNode
        {
            get
            {
                if (_operandNode == null)
                {
                    if (this.OperandNodeID > 0)
                    {
                        _operandNode = Node.GetNode(this.OperandNodeID);
                    }
                }
                return _operandNode;
            }
        }
        private SqlParameter OperandNodeIDParam
        {
            get
            {
                SqlParameter oparandNodeIDParam = new SqlParameter();
                oparandNodeIDParam.ParameterName = OPERAND_NODE_ID_PA;
                if (_operandNodeID == 0)
                {
                    oparandNodeIDParam.Value = DBNull.Value;
                }
                else
                {
                    oparandNodeIDParam.Value = _operandNodeID;
                }
                return oparandNodeIDParam;
            }
        }

        public int OperandCourseID
        {
            get { return _operandCourseID; }
            set { _operandCourseID = value; }
        }
        private SqlParameter OperandCourseIDParam
        {
            get
            {
                SqlParameter oparandCourseIDParam = new SqlParameter();
                oparandCourseIDParam.ParameterName = OPERAND_COURSE_ID_PA;
                if (_operandCourseID == 0)
                {
                    oparandCourseIDParam.Value = DBNull.Value;
                }
                else
                {
                    oparandCourseIDParam.Value = _operandCourseID;
                }
                return oparandCourseIDParam;
            }
        }
        public int OperandVersionID
        {
            get { return _operandVersionID; }
            set { _operandVersionID = value; }
        }
        private SqlParameter OperandVersionIDParam
        {
            get
            {
                SqlParameter oparandVersionIDParam = new SqlParameter();
                oparandVersionIDParam.ParameterName = OPERAND_VERSION_ID_PA;
                if (_operandVersionID == 0)
                {
                    oparandVersionIDParam.Value = DBNull.Value;
                }
                else
                {
                    oparandVersionIDParam.Value = _operandVersionID;
                }
                return oparandVersionIDParam;
            }
        }
        public Course OperandCourse
        {
            get
            {
                if (_operandCourse == null)
                {
                    if ((_operandCourseID > 0) && (_operandVersionID > 0))
                    {
                        _operandCourse = Course.GetCourse(_operandCourseID, _operandVersionID);
                    }
                }
                return _operandCourse;
            }
        }

        public int NodeCourseID
        {
            get { return _nodeCourseID; }
            set { _nodeCourseID = value; }
        }
        private SqlParameter NodeCourseIDParam
        {
            get
            {
                SqlParameter nodeCourseIDParam = new SqlParameter();
                nodeCourseIDParam.ParameterName = NODE_COURSE_ID_PA;
                if (_nodeCourseID == 0)
                {
                    nodeCourseIDParam.Value = DBNull.Value;
                }
                else
                {
                    nodeCourseIDParam.Value = _nodeCourseID;
                }

                return nodeCourseIDParam;
            }
        }
        public NodeCourse NodeCourse
        {
            get
            {
                if (_nodeCourse == null)
                {
                    if (this._nodeCourseID > 0)
                    {
                        _nodeCourse = NodeCourse.GetNodeCourse(this.NodeCourseID);
                    }
                }
                return _nodeCourse;
            }
            private set { _nodeCourse = value; }
        }

        public string WildCard
        {
            get { return _wildCard; }
            set { _wildCard = value; }
        }
        private SqlParameter WildCardParam
        {
            get
            {
                SqlParameter wildCardParam = new SqlParameter();
                wildCardParam.ParameterName = WILDCARD_PA;
                if (_wildCard.Trim() == string.Empty)
                {
                    wildCardParam.Value = DBNull.Value;
                }
                else
                {
                    wildCardParam.Value = _wildCard;
                }
                return wildCardParam;
            }
        }

        public bool IsStudntSpec
        {
            get { return _isStudntSpec; }
            set { _isStudntSpec = value; }
        }
        private SqlParameter IsStudntSpecParam
        {
            get
            {
                SqlParameter isStudntSpecParam = new SqlParameter();
                isStudntSpecParam.ParameterName = IS_STUDNT_SPEC_PA;

                isStudntSpecParam.Value = _isStudntSpec;

                return isStudntSpecParam;
            }
        }
        #endregion

        #region Functions
        private static VNodeSet vNodeMapper(SQLNullHandler nullHandler)
        {
            VNodeSet vNodeSet = new VNodeSet();

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
        private static List<VNodeSet> mapVNodeSets(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<VNodeSet> vNodeSets = null;

            while (theReader.Read())
            {
                if (vNodeSets == null)
                {
                    vNodeSets = new List<VNodeSet>();
                }
                VNodeSet vNodeSet = vNodeMapper(nullHandler);
                vNodeSets.Add(vNodeSet);
            }

            return vNodeSets;
        }
        private static VNodeSet mapVNodeSet(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            VNodeSet vNodeSet = null;
            if (theReader.Read())
            {
                vNodeSet = new VNodeSet();
                vNodeSet = vNodeMapper(nullHandler);
            }

            return vNodeSet;
        }

        public static VNodeSet Get(int vNodeSetID)
        {
            VNodeSet vNodeSet = null;

            string command = SELECT
                            + "WHERE VNodeSetID = @VNodeSetID";

            SqlParameter vNodeSetIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(vNodeSetID, "@VNodeSetID");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { vNodeSetIDparam });

            vNodeSet = mapVNodeSet(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return vNodeSet;
        }

        public static List<VNodeSet> GetByOwnerNode(int ownerNodeID)
        {
            List<VNodeSet> vNodeSets = null;

            string command = SELECT
                            + "WHERE NodeID = @NodeID";

            #region Parameters
            SqlParameter ownerNodeIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(ownerNodeID, "@NodeID");
            #endregion

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { ownerNodeIDparam });

            vNodeSets = mapVNodeSets(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return vNodeSets;
        }
        internal static List<VNodeSet> GetByOwnerNode(int ownerNodeID, SqlConnection sqlConn)
        {
            List<VNodeSet> vNodeSets = null;

            string command = SELECT
                            + "WHERE NodeID = @NodeID";

            #region Parameters
            SqlParameter ownerNodeIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(ownerNodeID, "@NodeID");
            #endregion

            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { ownerNodeIDparam });

            vNodeSets = mapVNodeSets(theReader);
            theReader.Close();

            return vNodeSets;
        }

        public static List<VNodeSet> GetByOwnerNode(int ownerNodeID, int setNo)
        {
            List<VNodeSet> vNodeSets = null;

            string command = SELECT
                            + "WHERE NodeID = @NodeID AND SetNo = @SetNo";

            #region SqlParameters
            SqlParameter ownerNodeIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(ownerNodeID, "@NodeID");
            SqlParameter setNoPram = MSSqlConnectionHandler.MSSqlParamGenerator(setNo, "@SetNo"); 
            #endregion

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { ownerNodeIDParam, setNoPram });
            vNodeSets = mapVNodeSets(theReader);


            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return vNodeSets;
        }
        internal static List<VNodeSet> GetByOwnerNode(int ownerNodeID, int setNo, SqlConnection sqlConn)
        {
            List<VNodeSet> vNodeSets = null;

            string command = SELECT
                            + "WHERE NodeID = @NodeID AND SetNo = @SetNo";

            #region SqlParameters
            SqlParameter ownerNodeIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(ownerNodeID, "@NodeID");

            SqlParameter setNoPram = MSSqlConnectionHandler.MSSqlParamGenerator(setNo, "@SetNo"); 
            #endregion

            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { ownerNodeIDParam, setNoPram });

            vNodeSets = mapVNodeSets(theReader);
            theReader.Close();

            return vNodeSets;
        }

        internal static List<int> GetUniqueSetNos(int ownerNodeID)
        {
            List<int> vNodeSetNos = null;

            string command = "SELECT DISTINCT SetNo FROM VNodeSet "
                            + "WHERE NodeID = @NodeID";

            #region SQLParams
            SqlParameter ownerNodeIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(ownerNodeID, "@NodeID"); 
            #endregion

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn,new SqlParameter[]{ownerNodeIDParam});

            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            while (theReader.Read())
            {
                if (vNodeSetNos == null)
                {
                    vNodeSetNos = new List<int>();
                }
                vNodeSetNos.Add(nullHandler.GetInt32("SetNo"));
            }
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return vNodeSetNos;
        }

        #region Old
        //public static List<TemplateHierarchy> getByParentTemplateNRST(int parentTemplateID)
        //{
        //    List<TemplateHierarchy> templateHierarchys = new List<TemplateHierarchy>();

        //    string command = "SELECT "
        //                    + "TplHierarchyID, "
        //                    + "ParentTplID, "
        //                    + "ChildTplID "
        //                    + "FROM dbo.TplHierarchy "
        //                    + "WHERE ParentTplID = " + parentTemplateID.ToString().Trim();

        //    SqlConnection sqlConn = SqlConnectionHandler.GetConnection();
        //    SqlDataReader theReader = SqlConnectionHandler.MSSqlExecuteQuerryCommand(command, sqlConn);

        //    templateHierarchys = mapTemplateHeirarchy(theReader);
        //    theReader.Close();

        //    foreach (TemplateHierarchy templateHierarchy in templateHierarchys)
        //    {
        //        if (templateHierarchy.ParentTplID != 0)
        //        {
        //            templateHierarchy._ParentTemplate = Template.getTemplate(templateHierarchy.ParentTplID, sqlConn);
        //        }
        //        if (templateHierarchy.ChildTplID != 0)
        //        {
        //            templateHierarchy._ChildTemplate = Template.getTemplate(templateHierarchy.ChildTplID, sqlConn);
        //        }
        //    }

        //    SqlConnectionHandler.CloseDbConnection();

        //    return templateHierarchys;
        //}
        //internal static List<TemplateHierarchy> getByParentTemplateNRST(int parentTemplateID, SqlConnection sqlConn)
        //{
        //    List<TemplateHierarchy> templateHierarchys = new List<TemplateHierarchy>();

        //    string command = "SELECT "
        //                    + "TplHierarchyID, "
        //                    + "ParentTplID, "
        //                    + "ChildTplID "
        //                    + "FROM dbo.TplHierarchy "
        //                    + "WHERE ParentTplID = " + parentTemplateID.ToString().Trim();

        //    SqlDataReader theReader = SqlConnectionHandler.MSSqlExecuteQuerryCommand(command, sqlConn);

        //    templateHierarchys = mapTemplateHeirarchy(theReader);
        //    theReader.Close();

        //    foreach (TemplateHierarchy templateHierarchy in templateHierarchys)
        //    {
        //        if (templateHierarchy.ParentTplID != 0)
        //        {
        //            templateHierarchy._ParentTemplate = Template.getTemplate(templateHierarchy.ParentTplID, sqlConn);
        //        }
        //        if (templateHierarchy.ChildTplID != 0)
        //        {
        //            templateHierarchy._ChildTemplate = Template.getTemplate(templateHierarchy.ChildTplID, sqlConn);
        //        }
        //    }

        //    return templateHierarchys;
        //} 
        #endregion

        public static int SaveVNodeSet(VNodeSet vNodeSet)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                SqlParameter[] sqlParams = null;
                Node.UpdateVirtualFlag(vNodeSet.OwnerNodeID, true, sqlConn, sqlTran);

                string command = string.Empty;

                if (vNodeSet.Id == 0)
                {
                    command = INSERT;
                    sqlParams = new SqlParameter[] { vNodeSet.VNodeSetMasIDParam,
                                                 vNodeSet.OwnerNodeIDParam, 
                                                 vNodeSet.SetNoParam, 
                                                 vNodeSet.OperandNodeIDParam, 
                                                 vNodeSet.OperandCourseIDParam, 
                                                 vNodeSet.OperandVersionIDParam, 
                                                 vNodeSet.NodeCourseIDParam,
                                                 vNodeSet.OperatorIDParam, 
                                                 vNodeSet.WildCardParam,
                                                 vNodeSet.IsStudntSpecParam, 
                                                 vNodeSet.CreatorIDParam,
                                                 vNodeSet.CreatedDateParam,
                                                 vNodeSet.ModifierIDParam,
                                                 vNodeSet.ModifiedDateParam};
                    #region Old
                    //if (vNodeSet.OperandNodeID == 0 && vNodeSet.WildCard.Trim().Length == 0)
                    //{
                    //    #region No Operand Node and No wild card
                    //    command = INSERTC + "("
                    //     + vNodeSet.OwnerNodeID.ToString() + ", "
                    //     + vNodeSet.SetNo.ToString() + ", "
                    //     + vNodeSet.OperandCourseID.ToString() + ", "
                    //     + vNodeSet.OperandVersionID.ToString() + ", "
                    //     + vNodeSet.OperatorID.ToString() + ")";
                    //    #endregion
                    //}
                    //else if (vNodeSet.OperandNodeID == 0 && vNodeSet.WildCard.Trim().Length > 0)
                    //{
                    //    #region No Operand Node
                    //    command = INSERTCW + "("
                    //     + vNodeSet.OwnerNodeID.ToString() + ", "
                    //     + vNodeSet.SetNo.ToString() + ", "
                    //     + vNodeSet.OperandCourseID.ToString() + ", "
                    //     + vNodeSet.OperandVersionID.ToString() + ", "
                    //     + vNodeSet.OperatorID.ToString() + ", "
                    //     + "'" + vNodeSet.WildCard.ToString() + "'" + ")";
                    //    #endregion
                    //}
                    //else if (vNodeSet.OperandCourseID == 0 && vNodeSet.OperandVersionID == 0 && vNodeSet.WildCard.Trim().Length == 0)
                    //{
                    //    #region No Operand Course and No wild card
                    //    command = INSERTN + "("
                    //     + vNodeSet.OwnerNodeID.ToString() + ", "
                    //     + vNodeSet.SetNo.ToString() + ", "
                    //     + vNodeSet.OperandNodeID.ToString() + ", "
                    //     + vNodeSet.OperatorID.ToString() + ")";
                    //    #endregion
                    //}
                    //else if (vNodeSet.OperandCourseID == 0 && vNodeSet.OperandVersionID == 0 && vNodeSet.WildCard.Trim().Length > 0)
                    //{
                    //    #region No Operand Course
                    //    command = INSERTNW + "("
                    //     + vNodeSet.OwnerNodeID.ToString() + ", "
                    //     + vNodeSet.SetNo.ToString() + ", "
                    //     + vNodeSet.OperandNodeID.ToString() + ", "
                    //     + vNodeSet.OperatorID.ToString() + ", "
                    //     + "'" + vNodeSet.WildCard.ToString() + "'" + ")";
                    //    #endregion
                    //} 
                    #endregion
                }
                else
                {
                    command = UPDATE
                            + " WHERE VNodeSetID = " + ID_PA;
                    sqlParams = new SqlParameter[] { vNodeSet.VNodeSetMasIDParam,
                                                 vNodeSet.OwnerNodeIDParam, 
                                                 vNodeSet.SetNoParam, 
                                                 vNodeSet.OperandNodeIDParam, 
                                                 vNodeSet.OperandCourseIDParam, 
                                                 vNodeSet.OperandVersionIDParam, 
                                                 vNodeSet.NodeCourseIDParam,
                                                 vNodeSet.OperatorIDParam, 
                                                 vNodeSet.WildCardParam, 
                                                 vNodeSet.IsStudntSpecParam, 
                                                 vNodeSet.CreatorIDParam,
                                                 vNodeSet.CreatedDateParam,
                                                 vNodeSet.ModifierIDParam,
                                                 vNodeSet.ModifiedDateParam, 
                                                 vNodeSet.IDParam };
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

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
        internal static int SaveVNodeSet(VNodeSet vNodeSet, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                SqlParameter[] sqlParams = null;
                Node.UpdateVirtualFlag(vNodeSet.OwnerNodeID, true, sqlConn, sqlTran);

                string command = string.Empty;

                if (vNodeSet.Id == 0)
                {
                    command = INSERT;
                    sqlParams = new SqlParameter[] { vNodeSet.VNodeSetMasIDParam,
                                                 vNodeSet.OwnerNodeIDParam, 
                                                 vNodeSet.SetNoParam, 
                                                 vNodeSet.OperandNodeIDParam, 
                                                 vNodeSet.OperandCourseIDParam, 
                                                 vNodeSet.OperandVersionIDParam, 
                                                 vNodeSet.NodeCourseIDParam,
                                                 vNodeSet.OperatorIDParam, 
                                                 vNodeSet.WildCardParam,
                                                 vNodeSet.IsStudntSpecParam, 
                                                 vNodeSet.CreatorIDParam,
                                                 vNodeSet.CreatedDateParam,
                                                 vNodeSet.ModifierIDParam,
                                                 vNodeSet.ModifiedDateParam};
                }
                else
                {
                    command = UPDATE
                            + " WHERE VNodeSetID = " + ID_PA;
                    sqlParams = new SqlParameter[] { vNodeSet.VNodeSetMasIDParam,
                                                 vNodeSet.OwnerNodeIDParam, 
                                                 vNodeSet.SetNoParam, 
                                                 vNodeSet.OperandNodeIDParam, 
                                                 vNodeSet.OperandCourseIDParam, 
                                                 vNodeSet.OperandVersionIDParam, 
                                                 vNodeSet.NodeCourseIDParam,
                                                 vNodeSet.OperatorIDParam, 
                                                 vNodeSet.WildCardParam,
                                                 vNodeSet.IsStudntSpecParam, 
                                                 vNodeSet.CreatorIDParam,
                                                 vNodeSet.CreatedDateParam,
                                                 vNodeSet.ModifierIDParam,
                                                 vNodeSet.ModifiedDateParam, 
                                                 vNodeSet.IDParam };
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        public static int DeleteVNode(int vNodeSetID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

                string command = DELETE
                                + "WHERE VNodeSetID = @VNodeSetID";

                #region Parameters
                SqlParameter vNodeSetIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(vNodeSetID, "@VNodeSetID");
                #endregion

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteAction(command, sqlConn, new SqlParameter[] { vNodeSetIDparam });

                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        public static int DeleteVNode(int ownerNodeID, int setNo)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

                string command = DELETE
                                + "WHERE NodeID = @NodeID AND SetNo = @SetNo";

                #region Parameters
                SqlParameter ownerNodeIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(ownerNodeID, "@NodeID");
                SqlParameter setNoPram = MSSqlConnectionHandler.MSSqlParamGenerator(setNo, "@SetNo");
                #endregion

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteAction(command, sqlConn, new SqlParameter[] { ownerNodeIDParam, setNoPram });

                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        public static int DeleteVNodeWithDeflag(int ownerNodeID, int setNo)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = DELETE
                                + "WHERE NodeID = @NodeID AND SetNo = @SetNo";

                SqlParameter ownerNodeIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(ownerNodeID, "@NodeID");
                SqlParameter setNoPram = MSSqlConnectionHandler.MSSqlParamGenerator(setNo, "@SetNo");

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

        internal static int DeleteVNode(int vNodeSetID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = DELETE
                                + "WHERE VNodeSetID = @VNodeSetID";

                #region Parameters
                SqlParameter vNodeSetIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(vNodeSetID, "@VNodeSetID");
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
        internal static int DeleteVNodeByVNodeMas(int vNodeSetMasID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = DELETE
                                + "WHERE " + VNODE_SET_MASTERID + " = " + VNODE_SET_MASTERID_PA;

                #region Parameters
                SqlParameter vNodeSetIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(vNodeSetMasID, VNODE_SET_MASTERID_PA);
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
        internal static int DeleteVNodeByMasOwnerNode(int ownerNodeID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = DELETE
                                + "WHERE " + VNODE_SET_ID + " IN ("
                                + "SELECT dbo.VNodeSet.VNodeSetID "
                                + "FROM dbo.VNodeSet LEFT OUTER JOIN "
                                + "dbo.VNodeSetMaster ON "
                                + "dbo.VNodeSet.VNodeSetMasterID = dbo.VNodeSetMaster.VNodeSetMasterID LEFT OUTER JOIN "
                                + "dbo.Node ON dbo.VNodeSetMaster.NodeID = dbo.Node.NodeID WHERE (dbo.Node.NodeID = " + NODE_ID_PA + "))";

                #region Parameters
                SqlParameter ownerNodeIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(ownerNodeID, NODE_ID_PA);
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
        internal static int DeleteVNode(int ownerNodeID, int setNo, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = DELETE
                                + "WHERE NodeID = @NodeID AND SetNo = @SetNo";

                #region Parameters
                SqlParameter ownerNodeIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(ownerNodeID, "@NodeID");
                SqlParameter setNoPram = MSSqlConnectionHandler.MSSqlParamGenerator(setNo, "@SetNo");
                #endregion

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { ownerNodeIDParam, setNoPram });

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        internal static int DeleteVNodeByOperandNodeID(int operandNodeID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = DELETE
                                + "WHERE OperandNodeID = @OperandNodeID";

                #region Parameters
                SqlParameter vNodeSetIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(operandNodeID, "@OperandNodeID");
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
        internal static int DeleteVNodeWithDeflag(int ownerNodeID, int setNo, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = DELETE
                                + "WHERE NodeID = @NodeID AND SetNo = @SetNo";

                #region Parameters
                SqlParameter ownerNodeIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(ownerNodeID, "@NodeID");
                SqlParameter setNoPram = MSSqlConnectionHandler.MSSqlParamGenerator(setNo, "@SetNo");
                #endregion

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { ownerNodeIDParam, setNoPram });

                Node.UpdateVirtualFlag(ownerNodeID, false, sqlConn, sqlTran);
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
