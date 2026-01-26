using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;
using Common;

namespace BussinessObject
{
    [Serializable]
    public class PreReqDetail : Base
    {
        #region DBColumns

        /*
         [PrerequisiteID] [int] IDENTITY(1,1) NOT NULL,
	    [PrerequisiteMasterID] [int] NULL,
	    [NodeCourseID] [int] NULL,
	    [PreReqNodeCourseID] [int] NULL,
	    [OperatorID] [int] NULL,
	    [OperatorIDMinOccurance] [int] NULL,
	    [ReqCredits] [numeric](18, 2) NULL,
	    [NodeID] [int] NULL,
	    [PreReqNodeID] [int] NULL,
	    [CreatedBy] [int] NOT NULL,
	    [CreatedDate] [datetime] NOT NULL,
	    [ModifiedBy] [int] NULL,
	    [ModifiedDate] [datetime] NULL,
        */

        #endregion

        #region Variables

        private int _prereqMasterID;
        private int _nodeCourseID;
        private int _preReqNodeCourseID;
        private int _operatorID;
        private int _operatorIDMinOccurance;
        private decimal _reqCredits;
        private int _nodeID;
        private int _preReqNodeID;
        private NodeCourse _nodeCourse = null;
        private Node _node = null;
        private Operator _operator = null;

        #endregion

        #region Constants

        #region Columns

        private const string PREREQDETAILID = "PrerequisiteID";

        private const string PREREQMASID = "PrerequisiteMasterID";
        private const string PREREQMASID_PA = "@PrerequisiteMasterID";

        private const string NODE_COURSE_ID = "NodeCourseID";
        private const string NODE_COURSE_ID_PA = "@NodeCourseID";

        private const string PREREQ_NODECOURSE_ID = "PreReqNodeCourseID";
        private const string PREREQ_NODECOURSE_ID_PA = "@PreReqNodeCourseID";

        private const string OPERATORID = "OperatorID";
        private const string OPERATORID_PA = "@OperatorID";

        private const string OPERATORID_MINOCCURANCE = "OperatorIDMinOccurance";
        private const string OPERATORID_MINOCCURANCE_PA = "@OperatorIDMinOccurance";

        private const string REQCREDITS = "ReqCredits";
        private const string REQCREDITS_PA = "@ReqCredits";

        private const string NODEID = "NodeID";
        private const string NODEID_PA = "@NodeID";

        private const string PREREQNODEID = "PreReqNodeID";
        private const string PREREQNODEID_PA = "@PreReqNodeID";

        #endregion

        #region PKColumns

        private const string ALLCOLUMNS = "[" + PREREQDETAILID + "], "
                                        + "[" + PREREQMASID + "], "
                                        + "[" + NODE_COURSE_ID + "], "
                                        + "[" + PREREQ_NODECOURSE_ID + "], "
                                        + "[" + OPERATORID + "], "
                                        + "[" + OPERATORID_MINOCCURANCE + "], "
                                        + "[" + REQCREDITS + "], "
                                        + "[" + NODEID + "], "
                                        + "[" + PREREQNODEID + "], ";
        #endregion

        #region NOPKColumns

        private const string NOPKCOLUMNS = "[" + PREREQMASID + "], "
                                        + "[" + NODE_COURSE_ID + "], "
                                        + "[" + PREREQ_NODECOURSE_ID + "], "
                                        + "[" + OPERATORID + "], "
                                        + "[" + OPERATORID_MINOCCURANCE + "], "
                                        + "[" + REQCREDITS + "], "
                                        + "[" + NODEID + "], "
                                        + "[" + PREREQNODEID + "], ";
        #endregion

        #region TableName
        private const string TABLENAME = " [PrerequisiteDetail] ";
        #endregion

        #region Select Query

        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                    + BASECOLUMNS
                    + "FROM" + TABLENAME;

        #endregion

        #region Insert Query

        private const string INSERT = "INSERT INTO" + TABLENAME + "(" + NOPKCOLUMNS
                                                                     + BASECOLUMNS + ")"
                                                                     + "VALUES ( "
                                                                     + PREREQMASID_PA + ", "
                                                                     + NODE_COURSE_ID_PA + ", "
                                                                     + PREREQ_NODECOURSE_ID_PA + ", "
                                                                     + OPERATORID_PA + ", "
                                                                     + OPERATORID_MINOCCURANCE_PA + ", "
                                                                     + REQCREDITS_PA + ", "
                                                                     + NODEID_PA + ", "
                                                                     + PREREQNODEID_PA + ", "
                                                                     + CREATORID_PA + ", "
                                                                     + CREATEDDATE_PA + ", "
                                                                     + MODIFIERID_PA + ", "
                                                                     + MOIDFIEDDATE_PA + ")";

        #endregion

        #region Delete

        private const string DELETE = "DELETE FROM" + TABLENAME;

        #endregion

        #region Update Query

        private const string UPDATE = "UPDATE" + TABLENAME
                    + "SET [" + PREREQMASID + "] = " + PREREQMASID_PA + ", "
                    + "[" + NODE_COURSE_ID + "] = " + NODE_COURSE_ID_PA + ", "
                    + "[" + PREREQ_NODECOURSE_ID + "] = " + PREREQ_NODECOURSE_ID_PA + ", "
                    + "[" + OPERATORID + "] = " + OPERATORID_PA + ", "
                    + "[" + OPERATORID_MINOCCURANCE + "] = " + OPERATORID_MINOCCURANCE_PA + ", "
                    + "[" + REQCREDITS + "] = " + REQCREDITS_PA + ", "
                    + "[" + NODEID + "] = " + NODEID_PA + ","
                    + "[" + PREREQNODEID + "] = " + PREREQNODEID_PA + ", "
                    + "[" + CREATORID + "] = " + CREATORID_PA + ", "
                    + "[" + CREATEDDATE + "] = " + CREATEDDATE_PA + ", "
                    + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ", "
                    + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;

        #endregion

        #endregion

        #region Properties
        public NodeCourse NodeCourse
        {
            get
            {
                if (_nodeCourse == null)
                {
                    _nodeCourse = NodeCourse.GetNodeCourse(PreReqNodeCourseID);
                }
                return _nodeCourse;
            }
        }
        public Node Node
        {
            get
            {
                if (_node == null)
                {
                    _node = Node.GetNode(PreReqNodeID);
                }
                return _node;
            }
        }
        public Operator Operator
        {
            get
            {
                if (_operator == null)
                {
                    _operator = Operator.GetOperator(_operatorID);
                }
                return _operator;
            }
        } 

        public int PrereqMasterID
        {
            get { return _prereqMasterID; }
            set { _prereqMasterID = value; }
        }
        private SqlParameter PrereqMasterIDParam
        {
            get
            {
                SqlParameter sqlPrereqMasterIDParam = new SqlParameter();

                sqlPrereqMasterIDParam.ParameterName = PREREQMASID_PA;

                if (PrereqMasterID == 0)
                {
                    sqlPrereqMasterIDParam.Value = DBNull.Value;
                }
                else
                {
                    sqlPrereqMasterIDParam.Value = PrereqMasterID;
                }

                return sqlPrereqMasterIDParam;
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
                SqlParameter sqlNodeCourseIDParam = new SqlParameter();

                sqlNodeCourseIDParam.ParameterName = NODE_COURSE_ID_PA;
                if (NodeCourseID == 0)
                {
                    sqlNodeCourseIDParam.Value = DBNull.Value;
                }
                else
                {
                    sqlNodeCourseIDParam.Value = NodeCourseID;
                }

                return sqlNodeCourseIDParam;
            }
        }


        public int PreReqNodeCourseID
        {
            get { return _preReqNodeCourseID; }
            set { _preReqNodeCourseID = value; }
        }
        private SqlParameter PreReqNodeCourseIDParam
        {
            get
            {
                SqlParameter sqlPreReqNodeCourseIDParam = new SqlParameter();

                sqlPreReqNodeCourseIDParam.ParameterName = PREREQ_NODECOURSE_ID_PA;
                if (PreReqNodeCourseID == 0)
                {
                    sqlPreReqNodeCourseIDParam.Value = DBNull.Value;
                }
                else
                {
                    sqlPreReqNodeCourseIDParam.Value = PreReqNodeCourseID;
                }

                return sqlPreReqNodeCourseIDParam;
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
                SqlParameter sqlOperatorIDParam = new SqlParameter();

                sqlOperatorIDParam.ParameterName = OPERATORID_PA;
                if (OperatorID == 0)
                {
                    sqlOperatorIDParam.Value = DBNull.Value;
                }
                else
                {
                    sqlOperatorIDParam.Value = OperatorID;
                }

                return sqlOperatorIDParam;
            }
        }

        public int OperatorIDMinOccurance
        {
            get { return _operatorIDMinOccurance; }
            set { _operatorIDMinOccurance = value; }
        }
        private SqlParameter OperatorIDMinOccuranceParam
        {
            get
            {
                SqlParameter sqlOpIDMinOccurParam = new SqlParameter();

                sqlOpIDMinOccurParam.ParameterName = OPERATORID_MINOCCURANCE_PA;
                if (OperatorIDMinOccurance == 0)
                {
                    sqlOpIDMinOccurParam.Value = DBNull.Value;
                }
                else
                {
                    sqlOpIDMinOccurParam.Value = OperatorIDMinOccurance;
                }

                return sqlOpIDMinOccurParam;
            }
        }

        public decimal ReqCredits
        {
            get { return _reqCredits; }
            set { _reqCredits = value; }
        }
        private SqlParameter ReqCreditsParam
        {
            get
            {
                SqlParameter sqlReqCreditsParam = new SqlParameter();

                sqlReqCreditsParam.ParameterName = REQCREDITS_PA;
                if (ReqCredits == 0)
                {
                    sqlReqCreditsParam.Value = DBNull.Value;
                }
                else
                {
                    sqlReqCreditsParam.Value = ReqCredits;
                }

                return sqlReqCreditsParam;
            }
        }

        public int Node_ID
        {
            get { return _nodeID; }
            set { _nodeID = value; }
        }
        private SqlParameter Node_IDParam
        {
            get
            {
                SqlParameter sqlNode_IDParam = new SqlParameter();

                sqlNode_IDParam.ParameterName = NODEID_PA;
                if (Node_ID == 0)
                {
                    sqlNode_IDParam.Value = DBNull.Value;
                }
                else
                {
                    sqlNode_IDParam.Value = Node_ID;
                }

                return sqlNode_IDParam;
            }
        }

        public int PreReqNodeID
        {
            get { return _preReqNodeID; }
            set { _preReqNodeID = value; }
        }
        private SqlParameter PreReqNodeIDParam
        {
            get
            {
                SqlParameter sqlPreReqNodeIDParam = new SqlParameter();

                sqlPreReqNodeIDParam.ParameterName = PREREQNODEID_PA;
                if (PreReqNodeID == 0)
                {
                    sqlPreReqNodeIDParam.Value = DBNull.Value;
                }
                else
                {
                    sqlPreReqNodeIDParam.Value = PreReqNodeID;
                }

                return sqlPreReqNodeIDParam;
            }
        }

        #endregion

        #region Methods

        private static PreReqDetail Mapper(SQLNullHandler nullHandler)
        {
            PreReqDetail obj = new PreReqDetail();

            obj.Id = nullHandler.GetInt32(PREREQDETAILID);
            obj.PrereqMasterID = nullHandler.GetInt32(PREREQMASID);
            obj.NodeCourseID = nullHandler.GetInt32(NODE_COURSE_ID);
            obj.PreReqNodeCourseID = nullHandler.GetInt32(PREREQ_NODECOURSE_ID);
            obj.OperatorID = nullHandler.GetInt32(OPERATORID);
            obj.OperatorIDMinOccurance = nullHandler.GetInt32(OPERATORID_MINOCCURANCE);
            obj.ReqCredits = nullHandler.GetDecimal(REQCREDITS);
            obj.Node_ID = nullHandler.GetInt32(NODEID);
            obj.PreReqNodeID = nullHandler.GetInt32(PREREQNODEID);
            obj.CreatorID = nullHandler.GetInt32(CREATORID);//12
            obj.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);//13
            obj.ModifierID = nullHandler.GetInt32(MODIFIERID);//14
            obj.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);//15

            return obj;
        }
        private static PreReqDetail MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            PreReqDetail obj = null;
            if (theReader.Read())
            {
                obj = new PreReqDetail();
                obj = Mapper(nullHandler);
            }

            return obj;
        }
        private static List<PreReqDetail> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            List<PreReqDetail> collection = null;
            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new List<PreReqDetail>();
                }
                PreReqDetail obj = Mapper(nullHandler);
                collection.Add(obj);
            }
            return collection;
        }
        public static List<PreReqDetail> Gets()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<PreReqDetail> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }
        public static List<PreReqDetail> GetDetailsByMasterID(int iD)
        {
            string command = SELECT
                            + "WHERE [" + PREREQMASID + "] = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(iD, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            List<PreReqDetail> obj = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }

        public static int Save(PreReqDetail obj)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (obj.Id == 0)
                {
                    #region Insert
                    command = INSERT;
                    sqlParams = new SqlParameter[] { //obj.IDParam,
                                                     obj.PrereqMasterIDParam,  
                                                     obj.NodeCourseIDParam,
                                                     obj.PreReqNodeCourseIDParam,
                                                     obj.OperatorIDParam,
                                                     obj.OperatorIDMinOccuranceParam,
                                                     obj.ReqCreditsParam,
                                                     obj.Node_IDParam,
                                                     obj.PreReqNodeCourseIDParam,
                                                     obj.CreatorIDParam, 
                                                     obj.CreatedDateParam, 
                                                     obj.ModifierIDParam, 
                                                     obj.ModifiedDateParam };
                    #endregion
                }
                else
                {

                    #region Update
                    command = UPDATE
                    + " WHERE [" + PREREQDETAILID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { obj.PrereqMasterIDParam,  
                                                     obj.NodeCourseIDParam,
                                                     obj.PreReqNodeCourseIDParam,
                                                     obj.OperatorIDParam,
                                                     obj.OperatorIDMinOccuranceParam,
                                                     obj.ReqCreditsParam,
                                                     obj.Node_IDParam,
                                                     obj.PreReqNodeCourseIDParam,
                                                     obj.CreatorIDParam, 
                                                     obj.CreatedDateParam, 
                                                     obj.ModifierIDParam, 
                                                     obj.ModifiedDateParam, 
                                                     obj.IDParam };
                    #endregion
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
        internal static int Save(PreReqDetail obj, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;
                string command = string.Empty;
                SqlParameter[] sqlParams = null;
                
                #region Insert
                command = INSERT;
                sqlParams = new SqlParameter[] { //obj.IDParam,
                                                 obj.PrereqMasterIDParam,  
                                                 obj.NodeCourseIDParam,
                                                 obj.PreReqNodeCourseIDParam,
                                                 obj.OperatorIDParam,
                                                 obj.OperatorIDMinOccuranceParam,
                                                 obj.ReqCreditsParam,
                                                 obj.Node_IDParam,
                                                 obj.PreReqNodeIDParam,
                                                 obj.CreatorIDParam, 
                                                 obj.CreatedDateParam, 
                                                 obj.ModifierIDParam, 
                                                 obj.ModifiedDateParam };
                    #endregion
                
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        public static int Delete(int intMasterID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = DELETE + " where PrerequisiteMasterID = " + intMasterID;

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

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
        internal static int Delete(int intMasterID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = DELETE + " WHERE PrerequisiteMasterID = " + intMasterID;

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        internal static int DeleteByPreReqNodeID(int preReqNodeID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = DELETE + " WHERE [" + PREREQNODEID + "] = " + preReqNodeID;

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        internal static int DeleteByPreReqNodeCourseID(int preReqNodeCourseID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = DELETE + " WHERE [" + PREREQ_NODECOURSE_ID + "] = " + preReqNodeCourseID;

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        internal static int DeleteByNodeCourseID(int node_CourseID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = DELETE
                            + "WHERE " + PREREQDETAILID + " IN ("
                            + "SELECT dbo.PrerequisiteDetail.PrerequisiteID "
                            + "FROM dbo.PrerequisiteDetail LEFT OUTER JOIN "
                            + "dbo.PrerequisiteMaster ON "
                            + "dbo.PrerequisiteDetail.PrerequisiteMasterID = dbo.PrerequisiteMaster.PrerequisiteMasterID LEFT OUTER JOIN "
                            + "dbo.NodeCourse ON dbo.PrerequisiteMaster.NodeCourseID = dbo.NodeCourse.Node_CourseID WHERE (dbo.NodeCourse.Node_CourseID =  @NodeCourseID))";

            #region Parameters
            SqlParameter ownerNodeIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(node_CourseID, "@NodeCourseID");
            #endregion

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { ownerNodeIDparam });

            return counter;
        }
        internal static int DeleteByNodeID(int nodeID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = DELETE
                            + "WHERE " + PREREQDETAILID + " IN ("
                            + "SELECT dbo.PrerequisiteDetail.PrerequisiteID "
                            + "FROM dbo.PrerequisiteDetail LEFT OUTER JOIN "
                            + "dbo.PrerequisiteMaster ON "
                            + "dbo.PrerequisiteDetail.PrerequisiteMasterID = dbo.PrerequisiteMaster.PrerequisiteMasterID LEFT OUTER JOIN "
                            + "dbo.Node ON dbo.PrerequisiteMaster.NodeID = dbo.Node.NodeID WHERE (dbo.Node.NodeID =  @NodeID))";

            #region Parameters
            SqlParameter ownerNodeIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(nodeID, "@NodeID");
            #endregion

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { ownerNodeIDparam });

            return counter;
        }

        internal static int DeleteByParentNodeIDCourseIDONodeCourse(int parentNodeID, int childCourseID, int childVersionID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = DELETE
                            + "WHERE " + PREREQDETAILID + " IN ("
                            + "SELECT dbo.PrerequisiteDetail.PrerequisiteID "
                            + "FROM dbo.PrerequisiteDetail LEFT OUTER JOIN "
                            + "dbo.PrerequisiteMaster ON "
                            + "dbo.PrerequisiteDetail.PrerequisiteMasterID = dbo.PrerequisiteMaster.PrerequisiteMasterID LEFT OUTER JOIN "
                            + "dbo.NodeCourse ON dbo.PrerequisiteMaster.NodeCourseID = dbo.NodeCourse.Node_CourseID "
                            + "WHERE dbo.NodeCourse.NodeID = " + parentNodeID.ToString() + " AND dbo.NodeCourse.CourseID = " + childCourseID.ToString() + " AND dbo.NodeCourse.VersionID = " + childVersionID.ToString() + ")"; 

            #region Parameters

            #endregion

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

            return counter;
        }
        internal static int DeleteByParentNodeIDONodeCourse(int parentNodeID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = DELETE
                            + "WHERE " + PREREQDETAILID + " IN ("
                            + "SELECT dbo.PrerequisiteDetail.PrerequisiteID "
                            + "FROM dbo.PrerequisiteDetail LEFT OUTER JOIN "
                            + "dbo.PrerequisiteMaster ON "
                            + "dbo.PrerequisiteDetail.PrerequisiteMasterID = dbo.PrerequisiteMaster.PrerequisiteMasterID LEFT OUTER JOIN "
                            + "dbo.NodeCourse ON dbo.PrerequisiteMaster.NodeCourseID = dbo.NodeCourse.Node_CourseID "
                            + "WHERE dbo.NodeCourse.NodeID = " + parentNodeID.ToString() +")";

            #region Parameters

            #endregion

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

            return counter;
        }

        internal static int DeleteByParentNodeIDCourseIDOPreNodeCourse(int parentNodeID, int childCourseID, int childVersionID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = DELETE
                            + "WHERE " + PREREQDETAILID + " IN ("
                            + "SELECT dbo.PrerequisiteDetail.PrerequisiteID "
                            + "FROM dbo.NodeCourse RIGHT OUTER JOIN "
                            + "dbo.PrerequisiteDetail ON "
                            + "dbo.NodeCourse.Node_CourseID = dbo.PrerequisiteDetail.PreReqNodeCourseID "
                            + "WHERE dbo.NodeCourse.NodeID = " + parentNodeID.ToString() + " AND dbo.NodeCourse.CourseID = " + childCourseID.ToString() + " AND dbo.NodeCourse.VersionID = " + childVersionID.ToString() + ")";

            #region Parameters

            #endregion

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

            return counter;
        }
        internal static int DeleteByParentNodeIDOPreNodeCourse(int parentNodeID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = DELETE
                            + "WHERE " + PREREQDETAILID + " IN ("
                            + "SELECT dbo.PrerequisiteDetail.PrerequisiteID "
                            + "FROM dbo.NodeCourse RIGHT OUTER JOIN "
                            + "dbo.PrerequisiteDetail ON "
                            + "dbo.NodeCourse.Node_CourseID = dbo.PrerequisiteDetail.PreReqNodeCourseID "
                            + "WHERE dbo.NodeCourse.NodeID = " + parentNodeID.ToString() + ")";

            #region Parameters

            #endregion

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

            return counter;
        }
        #endregion
    }
}
