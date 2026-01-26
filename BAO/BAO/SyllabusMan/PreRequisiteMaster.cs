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
    public class PreRequisiteMaster : Base
    {
        #region DBCOlumns
        /*
        PrerequisiteMasterID	int	            Unchecked
        Name	                varchar(50)	    Checked
        ProgramID	            int	            Checked
        NodeID	                int	            Checked
        NodeCourseID	        int	            Checked
        ReqCredits	            numeric(18, 2)	Checked
        CreatedBy	            int	            Unchecked
        CreatedDate	            datetime	    Unchecked
        ModifiedBy	            int	            Checked
        ModifiedDate	        datetime	    Checked
         */

        #endregion

        #region Variables
        private decimal _reqCredits;
        private int _nodeCourseID;
        private int _nodeID;
        private string _Name;
        private int _ProgramID;
        private List<PreReqDetail> _preReqDetails = null;
        private List<PreReqDetail> _preReqDetailCourses = null;
        private List<PreReqDetail> _preReqDetailNodes = null;
        #endregion

        #region Constants

        #region column Constants

        private const string PREREQMASTERID = "PrerequisiteMasterID";
        private const string PREREQMASTERID_PA = "@PrerequisiteMasterID";

        private const string NODE_COURSE_ID = "NodeCourseID";
        private const string NODE_COURSE_ID_PA = "@NodeCourseID";

        private const string REQCREDITS = "ReqCredits";
        private const string REQCREDITS_PA = "@ReqCredits";

        private const string NODEID = "NodeID";
        private const string NODEID_PA = "@NodeID";

        private const string NAME = "Name";
        private const string NAME_PA = "@Name";

        private const string PROGRAMID = "ProgramID";
        private const string PROGRAMID_PA = "@ProgramID";

        #endregion

        #region PKCoolumns

        private const string ALLCOLUMNS = "[" + PREREQMASTERID + "], "
                                        + "[" + NAME + "], "
                                        + "[" + PROGRAMID + "], "
                                        + "[" + NODEID + "], "
                                        + "[" + NODE_COURSE_ID + "], "
                                        + "[" + REQCREDITS + "], ";
        #endregion

        #region NOPKCoolumns

        private const string NOPKCOLUMNS = "[" + NAME + "], "
                                        + "[" + PROGRAMID + "], "
                                        + "[" + NODEID + "], "
                                        + "[" + NODE_COURSE_ID + "], "
                                        + "[" + REQCREDITS + "], ";
        #endregion

        #region TableName
        private const string TABLENAME = " [PrerequisiteMaster] ";
        #endregion

        #region Select Query
        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                    + BASECOLUMNS
                    + "FROM" + TABLENAME;

        #endregion

        #region Insert Query

        private const string INSERT = "INSERT INTO" + TABLENAME + "(" + ALLCOLUMNS
                                                                     + BASECOLUMNS + ")"
                                                                     + "VALUES ( "
                                                                     + ID_PA + ", "
                                                                     + NAME_PA + ", "
                                                                     + PROGRAMID_PA + ", "
                                                                     + NODEID_PA + ", "
                                                                     + NODE_COURSE_ID_PA + ", "
                                                                     + REQCREDITS_PA + ", "
                                                                     + CREATORID_PA + ", "
                                                                     + CREATEDDATE_PA + ", "
                                                                     + MODIFIERID_PA + ", "
                                                                     + MOIDFIEDDATE_PA + ")";

        #endregion

        #region DELETE

        private const string DELETE = "DELETE FROM" + TABLENAME;

        #endregion

        #region UPDATE
        private const string UPDATE = "UPDATE" + TABLENAME
                    + "SET [" + NAME + "] = " + NAME_PA + ", "
                    + "[" + PROGRAMID + "] = " + PROGRAMID_PA + ", "
                    + "[" + NODEID + "] = " + NODEID_PA + ", "
                    + "[" + NODE_COURSE_ID + "] = " + NODE_COURSE_ID_PA + ", "
                    + "[" + REQCREDITS + "] = " + REQCREDITS_PA + ", "
                    + "[" + CREATORID + "] = " + CREATORID_PA + ", "
                    + "[" + CREATEDDATE + "] = " + CREATEDDATE_PA + ", "
                    + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ", "
                    + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;
        #endregion


        #endregion

        #region Properties

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private SqlParameter NameParam
        {
            get
            {
                SqlParameter sqlNameParam = new SqlParameter();

                sqlNameParam.ParameterName = NAME_PA;
                sqlNameParam.Value = Name;

                return sqlNameParam;
            }
        }

        public int ProgramID
        {
            get { return _ProgramID; }
            set { _ProgramID = value; }
        }
        private SqlParameter ProgramIDParam
        {
            get
            {
                SqlParameter sqlProgramIDParamParam = new SqlParameter();

                sqlProgramIDParamParam.ParameterName = PROGRAMID_PA;
                sqlProgramIDParamParam.Value = ProgramID;

                return sqlProgramIDParamParam;
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

                sqlReqCreditsParam.ParameterName = REQCREDITS;
                sqlReqCreditsParam.Value = ReqCredits;

                return sqlReqCreditsParam;
            }
        }

        public List<PreReqDetail> PreReqDetails
        {
            get
            {
                if (_preReqDetails == null)
                {
                    if (this.Id > 0)
                    {
                        _preReqDetails = PreReqDetail.GetDetailsByMasterID(this.Id);

                    }
                }
                return _preReqDetails;
            }
            set { _preReqDetails = value; }
        }

        public List<PreReqDetail> PreReqDetailCourses
        {
            get
            {
                if (_preReqDetailCourses == null)
                {
                    if (_preReqDetails != null)
                    {
                        foreach (PreReqDetail item in _preReqDetails)
                        {
                            if (item.PreReqNodeCourseID > 0)
                            {
                                if (_preReqDetailCourses == null)
                                {
                                    _preReqDetailCourses = new List<PreReqDetail>();
                                }
                                _preReqDetailCourses.Add(item);
                            }
                        }
                    }
                }
                return _preReqDetailCourses;
            }
            set { _preReqDetailCourses = value; }
        }

        public List<PreReqDetail> PreReqDetailNodes
        {
            get
            {
                if (_preReqDetailNodes == null)
                {
                    if (_preReqDetails != null)
                    {
                        foreach (PreReqDetail item in _preReqDetails)
                        {
                            if (item.PreReqNodeID > 0)
                            {
                                if (_preReqDetailNodes == null)
                                {
                                    _preReqDetailNodes = new List<PreReqDetail>();
                                }
                                _preReqDetailNodes.Add(item);
                            }
                        }
                    }
                }
                return _preReqDetailNodes;
            }
            set { _preReqDetailNodes = value; }
        } 

        #endregion

        #region Methods
        private static PreRequisiteMaster Mapper(SQLNullHandler nullHandler)
        {
            PreRequisiteMaster obj = new PreRequisiteMaster();

            obj.Id = nullHandler.GetInt32(PREREQMASTERID);
            obj.Name = nullHandler.GetString(NAME);
            obj.ProgramID = nullHandler.GetInt32(PROGRAMID);
            obj.Node_ID = nullHandler.GetInt32(NODEID);
            obj.NodeCourseID = nullHandler.GetInt32(NODE_COURSE_ID);
            obj.ReqCredits = nullHandler.GetDecimal(REQCREDITS);
            obj.CreatorID = nullHandler.GetInt32(CREATORID);//12
            obj.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);//13
            obj.ModifierID = nullHandler.GetInt32(MODIFIERID);//14
            obj.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);//15

            return obj;
        }
        private static PreRequisiteMaster MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            PreRequisiteMaster obj = null;
            if (theReader.Read())
            {
                obj = new PreRequisiteMaster();
                obj = Mapper(nullHandler);
            }

            return obj;
        }
        private static List<PreRequisiteMaster> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<PreRequisiteMaster> collection = null;

            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new List<PreRequisiteMaster>();
                }
                PreRequisiteMaster obj = Mapper(nullHandler);
                collection.Add(obj);
            }

            return collection;
        }

        internal static int GetMaxID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newID = 0;

            string command = "SELECT MAX(" + PREREQMASTERID + ") FROM " + TABLENAME;
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

        public static List<PreRequisiteMaster> Gets()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<PreRequisiteMaster> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }
        public static List<PreRequisiteMaster> GetsByNodeCourse(int nodeCourseID)
        {
            string command = SELECT
                            + "WHERE [" + NODE_COURSE_ID + "] = " + NODE_COURSE_ID_PA;

            SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(nodeCourseID, NODE_COURSE_ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });

            List<PreRequisiteMaster> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }
        internal static List<PreRequisiteMaster> GetsByNodeCourse(int nodeCourseID,SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            string command = SELECT
                            + "WHERE [" + NODE_COURSE_ID + "] = " + NODE_COURSE_ID_PA;

            SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(nodeCourseID, NODE_COURSE_ID_PA);

            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteBatchQuerry(command, sqlConn, sqlTran, new SqlParameter[] { sqlParam });

            List<PreRequisiteMaster> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }

        public static PreRequisiteMaster Get(int iD)
        {
            string command = SELECT
                            + "WHERE [" + PREREQMASTERID + "] = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(iD, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            PreRequisiteMaster obj = MapClass(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }        
        public static List<PreRequisiteMaster> GetMasters(string parameters)
        {
            List<PreRequisiteMaster> masters = null;

            string command = SELECT
                            + "WHERE [Name] Like '%" + parameters + "%'";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            masters = MapCollection(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return masters;
        }
        public static List<PreRequisiteMaster> GetMastersByNode_courseID(int NodeCOurseID)
        {
            List<PreRequisiteMaster> masters = null;

            string command = SELECT
                            + "WHERE [" + NODE_COURSE_ID + "] = " + NODE_COURSE_ID_PA;

            SqlParameter sqlParam = new SqlParameter(NODE_COURSE_ID_PA, NodeCOurseID);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });

            masters = MapCollection(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return masters;
        }
        public static List<PreRequisiteMaster> GetMastersByNode(int NodeID)
        {
            List<PreRequisiteMaster> masters = null;

            string command = SELECT
                            + "WHERE [" + NODEID + "] = " + NODEID_PA;

            SqlParameter sqlParam = new SqlParameter(NODEID_PA, NodeID);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });

            masters = MapCollection(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return masters;
        }

        public static int Save(PreRequisiteMaster obj)
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
                    obj.Id = PreRequisiteMaster.GetMaxID(sqlConn, sqlTran);
                    command = INSERT;
                    sqlParams = new SqlParameter[] { obj.IDParam,
                                                     obj.NameParam,  
                                                     obj.ProgramIDParam,
                                                     obj.Node_IDParam,
                                                     obj.NodeCourseIDParam,  
                                                     obj.ReqCreditsParam,
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
                    + " WHERE [" + PREREQMASTERID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { obj.NameParam,  
                                                     obj.ProgramIDParam,
                                                     obj.Node_IDParam,
                                                     obj.NodeCourseIDParam,  
                                                     obj.ReqCreditsParam,
                                                     obj.CreatorIDParam, 
                                                     obj.CreatedDateParam, 
                                                     obj.ModifierIDParam, 
                                                     obj.ModifiedDateParam, 
                                                     obj.IDParam };
                    #endregion
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);
                PreReqDetail.Delete(obj.Id);
                if (obj.PreReqDetailCourses != null)
                {
                    foreach (PreReqDetail dt in obj.PreReqDetailCourses)
                    { 
                        dt.Id = 0;
                        PreReqDetail.Save(dt, sqlConn, sqlTran);
                    }
                }
                if (obj.PreReqDetailNodes != null)
                {
                    foreach (PreReqDetail dt in obj.PreReqDetailNodes)
                    {
                        dt.Id = 0;
                        PreReqDetail.Save(dt, sqlConn, sqlTran);
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
        internal static int Save(PreRequisiteMaster obj, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;
                //SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                //SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                //if (obj.Id == 0)
                //{
                #region Insert
                obj.Id = PreRequisiteMaster.GetMaxID(sqlConn, sqlTran);
                command = INSERT;
                sqlParams = new SqlParameter[] { obj.IDParam,
                                                     obj.NameParam,  
                                                     obj.ProgramIDParam,
                                                     obj.Node_IDParam,
                                                     obj.NodeCourseIDParam,  
                                                     obj.ReqCreditsParam,
                                                     obj.CreatorIDParam, 
                                                     obj.CreatedDateParam, 
                                                     obj.ModifierIDParam, 
                                                     obj.ModifiedDateParam };
                #endregion
                //}
                //else
                //{

                //    #region Update
                //    command = UPDATE
                //    + " WHERE [" + PREREQMASTERID + "] = " + ID_PA;
                //    sqlParams = new SqlParameter[] { obj.NameParam,  
                //                                     obj.ProgramIDParam,
                //                                     obj.Node_IDParam,
                //                                     obj.NodeCourseIDParam,  
                //                                     obj.ReqCreditsParam,
                //                                     obj.CreatorIDParam, 
                //                                     obj.CreatedDateParam, 
                //                                     obj.ModifierIDParam, 
                //                                     obj.ModifiedDateParam, 
                //                                     obj.IDParam };
                //    #endregion
                //}
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);
                PreReqDetail.Delete(obj.Id,sqlConn,sqlTran);

                if (obj.PreReqDetailCourses != null)
                {
                    foreach (PreReqDetail dt in obj.PreReqDetailCourses)
                    {
                        dt.Id = 0;
                        dt.PrereqMasterID = obj.Id;
                        if (obj.Node_ID > 0)
                        {
                            dt.Node_ID = obj.Node_ID;
                        }
                        else if (obj.NodeCourseID > 0)
                        {
                            dt.NodeCourseID = obj.NodeCourseID;
                        }
                        PreReqDetail.Save(dt, sqlConn, sqlTran);
                    }
                }
                if (obj.PreReqDetailNodes != null)
                {
                    foreach (PreReqDetail dt in obj.PreReqDetailNodes)
                    {
                        dt.Id = 0;
                        dt.PrereqMasterID = obj.Id;
                        if (obj.Node_ID > 0)
                        {
                            dt.Node_ID = obj.Node_ID;
                        }
                        else if (obj.NodeCourseID > 0)
                        {
                            dt.NodeCourseID = obj.NodeCourseID;
                        }
                        PreReqDetail.Save(dt, sqlConn, sqlTran);
                    }
                }

                //MSSqlConnectionHandler.CommitTransaction();
                //MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }


        public static int Delete(PreRequisiteMaster obj)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                PreReqDetail.Delete(obj.Id);

                string command = DELETE + " where PrerequisiteMasterID = " + obj.Id;

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
        internal static int Delete(PreRequisiteMaster obj, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                PreReqDetail.Delete(obj.Id);

                string command = DELETE + " where PrerequisiteMasterID = " + obj.Id;

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        internal static int DeleteByNodeCourse(int nodeCourseID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                PreReqDetail.DeleteByNodeCourseID(nodeCourseID,sqlConn,sqlTran);

                string command = DELETE + " WHERE [" + NODE_COURSE_ID + "] = " + NODE_COURSE_ID_PA;

                #region Parameters
                SqlParameter ownerNodeIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(nodeCourseID, NODE_COURSE_ID_PA);
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
        internal static int DeleteByNode(int nodeID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                PreReqDetail.DeleteByNodeID(nodeID, sqlConn, sqlTran);

                string command = DELETE + " WHERE [" + NODEID + "] = " + NODEID_PA;

                #region Parameters
                SqlParameter ownerNodeIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(nodeID, NODEID_PA);
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

        internal static int DeleteByParentNodeIDCourseIDONodeCourse(int parentNodeID, int childCourseID, int childVersionID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            PreReqDetail.DeleteByParentNodeIDCourseIDONodeCourse(parentNodeID, childCourseID, childVersionID, sqlConn, sqlTran);

            string command = DELETE
                            + "WHERE " + PREREQMASTERID + " IN ("
                            + "SELECT dbo.PrerequisiteMaster.PrerequisiteMasterID "
                            + "FROM dbo.PrerequisiteMaster LEFT OUTER JOIN "
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

            PreReqDetail.DeleteByParentNodeIDONodeCourse(parentNodeID, sqlConn, sqlTran);

            string command = DELETE
                            + "WHERE " + PREREQMASTERID + " IN ("
                            + "SELECT dbo.PrerequisiteMaster.PrerequisiteMasterID "
                            + "FROM dbo.PrerequisiteMaster LEFT OUTER JOIN "
                            + "dbo.NodeCourse ON dbo.PrerequisiteMaster.NodeCourseID = dbo.NodeCourse.Node_CourseID "
                            + "WHERE dbo.NodeCourse.NodeID = " + parentNodeID.ToString() + ")";

            #region Parameters

            #endregion

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

            return counter;
        }
        #endregion

    }
}
