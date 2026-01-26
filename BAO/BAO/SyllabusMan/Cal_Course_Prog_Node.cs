using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class Cal_Course_Prog_Node : Base
    {
        #region Varibales
        private int _treeCalendarDetailID;
        private TreeCalendarDetail _treeCalenderDetail;
        //private int _treeMasterID;
        //private TreeMaster _treeMaster;
        //private int _calendarMasterID;
        //private CalendarUnitMaster _calendarMaster;
        private int _programID;
        private Program _program;
        private int _nodeID;
        private Node _node;
        private int _courseID;
        private int _versionID;
        private Course _course;
        private int _nodeCourseID;
        private int _priority;
        private NodeCourse _nodeCourse;

        private string _nodeLinkName;
        private decimal _credits;
        private bool _isMajorRelated;
        private bool _isMinorRelated;
        #endregion

        #region Constants
        private const string SELECT = "SELECT "
                            + "[CalCorProgNodeID], "
                            + "[TreeCalendarDetailID], "
                            + "[OfferedByProgramID], "
                            + "[CourseID], "
                            + "[VersionID], "
                            + "[Node_CourseID], "
                            + "[NodeID], "
                            + "[NodeLinkName], "
                            + "[Priority], "
                            + "[Credits], "
                            + "IsMajorRelated, "
                            + "IsMinorRelated, "
                            + BASECOLUMNS
                            + "FROM [CalCourseProgNode] ";

        private const string INSERT = "INSERT INTO [CalCourseProgNode] ("
            //+ "[CalCorProgNodeID], "
                            + "[TreeCalendarDetailID], "
                            + "[OfferedByProgramID], "
                            + "[CourseID], "
                            + "[VersionID], "
                            + "[Node_CourseID], "
                            + "[NodeID], "
                            + "[NodeLinkName], "
                            + "[Priority], "
                            + "[Credits], "
                            + "IsMajorRelated, "
                            + "IsMinorRelated, "
                            + BASECOLUMNS
                            + ")"
                            + "VALUES ( "
            //+ "@CalCorProgNodeID, "
                            + "@TreeCalendarDetailID, "
                            + "@OfferedByProgramID, "
                            + "@CourseID, "
                            + "@VersionID, "
                            + "@Node_CourseID, "
                            + "@NodeID, "
                            + "@NodeLinkName, "
                            + "@Priority, "
                            + "@Credits, "
                            + "@IsMajorRelated, "
                            + "@IsMinorRelated, "
                            + CREATORID_PA + ", "
                            + CREATEDDATE_PA + ", "
                            + MODIFIERID_PA + ", "
                            + MOIDFIEDDATE_PA
                            + ")";

        private const string UPDATE = "UPDATE [CalCourseProgNode] "
                            + "SET TreeCalendarDetailID = @TreeCalendarDetailID, "
                            + "OfferedByProgramID = @OfferedByProgramID, "
                            + "CourseID = @CourseID, "
                            + "VersionID = @VersionID, "
                            + "Node_CourseID = @Node_CourseID, "
                            + "NodeID = @NodeID, "
                            + "NodeLinkName = @NodeLinkName, "
                            + "Priority = @Priority, "
                            + "Credits = @Credits, "
                            + "IsMajorRelated = @IsMajorRelated, "
                            + "IsMinorRelated = @IsMinorRelated, "
                            + CREATORID + " = " + CREATORID_PA + ", "
                            + CREATEDDATE + " = " + CREATEDDATE_PA + ", "
                            + MODIFIERID + " = " + MODIFIERID_PA + ", "
                            + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM [CalCourseProgNode] ";
        #endregion

        #region Constructor
        public Cal_Course_Prog_Node()
            : base()
        {
            _treeCalendarDetailID = 0;
            _treeCalenderDetail = null;
            //_treeMasterID = 0;
            //_treeMaster = null;
            //_calendarMasterID = 0;
            //_calendarMaster = null;
            _programID = 0;
            _program = null;
            _nodeID = 0;
            _node = null;
            _courseID = 0;
            _versionID = 0;
            _course = null;
            _nodeCourseID = 0;
            _nodeCourse = null;
            _nodeLinkName = string.Empty;
            _priority = 0;
            _credits = 0M;
            _isMajorRelated = false;
            _isMinorRelated = false;
        }
        #endregion

        #region Properties

        public int TreeCalendarDetailID
        {
            get { return _treeCalendarDetailID; }
            set { _treeCalendarDetailID = value; }
        }
        private SqlParameter TreeCalendarDetailIDParam
        {
            get
            {
                SqlParameter treeCalendarDetailIDParam = new SqlParameter();
                treeCalendarDetailIDParam.ParameterName = "@TreeCalendarDetailID";

                treeCalendarDetailIDParam.Value = _treeCalendarDetailID;

                return treeCalendarDetailIDParam;
            }
        }
        public TreeCalendarDetail TreeCalenderDetail
        {
            get { return _treeCalenderDetail; }
            private set { _treeCalenderDetail = value; }
        }

        #region old
        //public int TreeMasterID
        //{
        //    get { return _treeMasterID; }
        //    set { _treeMasterID = value; }
        //}
        //private SqlParameter TreeMasterIDParam
        //{
        //    get
        //    {
        //        SqlParameter treeMasterIDParam = new SqlParameter();
        //        treeMasterIDParam.ParameterName = "@TreeMasterID";

        //        treeMasterIDParam.Value = _treeMasterID;

        //        return treeMasterIDParam;
        //    }
        //}
        //public TreeMaster TreeMaster
        //{
        //    get { return _treeMaster; }
        //    private set { _treeMaster = value; }
        //}

        //public int CalendarMasterID
        //{
        //    get { return _calendarMasterID; }
        //    set { _calendarMasterID = value; }
        //}
        //private SqlParameter CalendarMasterIDParam
        //{
        //    get
        //    {
        //        SqlParameter calendarMasterIDParam = new SqlParameter();
        //        calendarMasterIDParam.ParameterName = "@CalendarMasterID";

        //        calendarMasterIDParam.Value = _calendarMasterID;

        //        return calendarMasterIDParam;
        //    }
        //}
        //public CalendarUnitMaster CalendarMaster
        //{
        //    get { return _calendarMaster; }
        //    private set { _calendarMaster = value; }
        //} 
        #endregion

        public int ProgramID
        {
            get { return _programID; }
            set { _programID = value; }
        }
        private SqlParameter ProgramIDParam
        {
            get
            {
                SqlParameter programIDParam = new SqlParameter();
                programIDParam.ParameterName = "@OfferedByProgramID";

                programIDParam.Value = _programID;

                return programIDParam;
            }
        }
        public Program Program
        {
            get
            {
                if (_program == null)
                {
                    if (this.ProgramID > 0)
                    {
                        _program = Program.GetProgram(this.ProgramID);
                    }
                }
                return _program;
            }
            private set { _program = value; }
        }

        public int NodeID
        {
            get { return _nodeID; }
            set { _nodeID = value; }
        }
        private SqlParameter NodeIDParam
        {
            get
            {
                SqlParameter nodeIDParam = new SqlParameter();
                nodeIDParam.ParameterName = "@NodeID";
                if (_nodeID == 0)
                {
                    nodeIDParam.Value = DBNull.Value;
                }
                else
                {
                    nodeIDParam.Value = _nodeID;
                }

                return nodeIDParam;
            }
        }
        public Node Node
        {
            get
            {
                if (_node == null)
                {
                    if (this._nodeID > 0)
                    {
                        _node = Node.GetNode(this.NodeID);
                    }
                }
                return _node;
            }
            private set { _node = value; }
        }

        public int CourseID
        {
            get { return _courseID; }
            set { _courseID = value; }
        }
        private SqlParameter CourseIDParam
        {
            get
            {
                SqlParameter codeIDParam = new SqlParameter();
                codeIDParam.ParameterName = "@CourseID";
                if (_courseID == 0)
                {
                    codeIDParam.Value = DBNull.Value;
                }
                else
                {
                    codeIDParam.Value = _courseID;
                }

                return codeIDParam;
            }
        }
        public int VersionID
        {
            get { return _versionID; }
            set { _versionID = value; }
        }
        private SqlParameter VersionIDParam
        {
            get
            {
                SqlParameter versionIDParam = new SqlParameter();
                versionIDParam.ParameterName = "@VersionID";
                if (_versionID == 0)
                {
                    versionIDParam.Value = DBNull.Value;
                }
                else
                {
                    versionIDParam.Value = _versionID;
                }
                return versionIDParam;
            }
        }
        public Course Course
        {
            get
            {
                if (_course == null)
                {
                    if ((_courseID > 0) && (_versionID > 0))
                    {
                        _course = Course.GetCourse(_courseID, _versionID);
                    }
                }
                return _course;
            }
            private set { _course = value; }
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
                nodeCourseIDParam.ParameterName = "@Node_CourseID";
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

        public string NodeLinkName
        {
            get { return _nodeLinkName; }
            set { _nodeLinkName = value; }
        }
        private SqlParameter NodeLinkNameParam
        {
            get
            {
                SqlParameter nodeLinkNameParam = new SqlParameter();
                nodeLinkNameParam.ParameterName = "@NodeLinkName";
                if (_nodeLinkName == string.Empty)
                {
                    nodeLinkNameParam.Value = DBNull.Value;
                }
                else
                {
                    nodeLinkNameParam.Value = _nodeLinkName;
                }

                return nodeLinkNameParam;
            }
        }

        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }
        private SqlParameter PriorityParam
        {
            get
            {
                SqlParameter priorityParam = new SqlParameter();
                priorityParam.ParameterName = "@Priority";
                if (Priority > 0)
                {
                    priorityParam.Value = Priority;
                }
                else
                {
                    priorityParam.Value = DBNull.Value;
                }
                priorityParam.Value = Priority;
                return priorityParam;
            }
        }

        public decimal Credits
        {
            get { return _credits; }
            set { _credits = value; }
        }
        private SqlParameter CreditsParam
        {
            get
            {
                SqlParameter creditsParam = new SqlParameter();
                creditsParam.ParameterName = "@Credits";
                if (Credits > 0)
                {
                    creditsParam.Value = Credits;
                }
                else
                {
                    creditsParam.Value = DBNull.Value;
                }
                return creditsParam;
            }
        }

        public bool IsMajorRelated
        {
            get { return _isMajorRelated; }
            set { _isMajorRelated = value; }
        }
        private SqlParameter IsMajorRelatedParam
        {
            get
            {
                SqlParameter isMajorRelatedParam = new SqlParameter();
                isMajorRelatedParam.ParameterName = "@IsMajorRelated";
                if (Credits > 0)
                {
                    isMajorRelatedParam.Value = IsMajorRelated;
                }
                else
                {
                    isMajorRelatedParam.Value = DBNull.Value;
                }
                return isMajorRelatedParam;
            }
        }

        public bool IsMinorRelated
        {
            get { return _isMinorRelated; }
            set { _isMinorRelated = value; }
        }
        private SqlParameter IsMinorRelatedParam
        {
            get
            {
                SqlParameter isMinorRelatedParam = new SqlParameter();
                isMinorRelatedParam.ParameterName = "@IsMinorRelated";
                if (Credits > 0)
                {
                    isMinorRelatedParam.Value = IsMinorRelated;
                }
                else
                {
                    isMinorRelatedParam.Value = DBNull.Value;
                }
                return isMinorRelatedParam;
            }
        }

        #endregion

        #region Methods
        private static Cal_Course_Prog_Node calendarDistributionMapper(SQLNullHandler nullHandler)
        {
            Cal_Course_Prog_Node calendarDistribution = new Cal_Course_Prog_Node();

            calendarDistribution.Id = nullHandler.GetInt32("CalCorProgNodeID");
            calendarDistribution.TreeCalendarDetailID = nullHandler.GetInt32("TreeCalendarDetailID");
            calendarDistribution.ProgramID = nullHandler.GetInt32("OfferedByProgramID");
            calendarDistribution.NodeID = nullHandler.GetInt32("NodeID");
            calendarDistribution.CourseID = nullHandler.GetInt32("CourseID");
            calendarDistribution.VersionID = nullHandler.GetInt32("VersionID");
            calendarDistribution.NodeCourseID = nullHandler.GetInt32("Node_CourseID");
            calendarDistribution.NodeLinkName = nullHandler.GetString("NodeLinkName");
            calendarDistribution.Priority = nullHandler.GetInt32("Priority");
            calendarDistribution.Credits = nullHandler.GetDecimal("Credits");
            calendarDistribution.IsMajorRelated = nullHandler.GetBoolean("IsMajorRelated");
            calendarDistribution.IsMinorRelated = nullHandler.GetBoolean("IsMinorRelated");
            calendarDistribution.CreatorID = nullHandler.GetInt32("CreatedBy");
            calendarDistribution.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            calendarDistribution.ModifierID = nullHandler.GetInt32("ModifiedBy");
            calendarDistribution.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");
            return calendarDistribution;
        }
        private static List<Cal_Course_Prog_Node> mapCalendarDistributions(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Cal_Course_Prog_Node> calendarDistributions = null;

            while (theReader.Read())
            {
                if (calendarDistributions == null)
                {
                    calendarDistributions = new List<Cal_Course_Prog_Node>();
                }
                Cal_Course_Prog_Node treeDetail = calendarDistributionMapper(nullHandler);
                calendarDistributions.Add(treeDetail);
            }

            return calendarDistributions;
        }
        private static Cal_Course_Prog_Node mapCalendarDistribution(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            Cal_Course_Prog_Node calendarDistribution = null;
            if (theReader.Read())
            {
                calendarDistribution = new Cal_Course_Prog_Node();
                calendarDistribution = calendarDistributionMapper(nullHandler);
            }

            return calendarDistribution;
        }

        internal static int GetMaxCalDistID(SqlConnection sqlConn)
        {
            int newCalDistID = 0;

            string command = "SELECT MAX(CalCorProgNodeID) FROM [CalCourseProgNode]";
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

            if (ob == null || ob == DBNull.Value)
            {
                newCalDistID = 1;
            }
            else if (ob is Int32)
            {
                newCalDistID = Convert.ToInt32(ob) + 1;
            }

            return newCalDistID;
        }
        internal static int GetMaxCalDistID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newCalDistID = 0;

            string command = "SELECT MAX(CalCorProgNodeID) FROM [CalCourseProgNode]";
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

            if (ob == null || ob == DBNull.Value)
            {
                newCalDistID = 1;
            }
            else if (ob is Int32)
            {
                newCalDistID = Convert.ToInt32(ob) + 1;
            }

            return newCalDistID;
        }

        public static Cal_Course_Prog_Node Get(int calendarDistributionID)
        {
            Cal_Course_Prog_Node calendarDistribution = null;

            string command = SELECT
                            + "WHERE CalCorProgNodeID = @CalCorProgNodeID";

            SqlParameter calendarDistributionIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(calendarDistributionID, "@CalCorProgNodeID");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { calendarDistributionIDparam });

            calendarDistribution = mapCalendarDistribution(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return calendarDistribution;
        }

        public static List<Cal_Course_Prog_Node> Get()
        {
            List<Cal_Course_Prog_Node> calendarDistributions = null;

            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            calendarDistributions = mapCalendarDistributions(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return calendarDistributions;
        }
        public static List<Cal_Course_Prog_Node> GetByTreeCalDet(int treeCalendarDetailID)
        {
            List<Cal_Course_Prog_Node> calendarDistributions = null;

            string command = SELECT
                            + "WHERE TreeCalendarDetailID = @TreeCalendarDetailID";

            SqlParameter treeCalendarDetailIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(treeCalendarDetailID, "@TreeCalendarDetailID");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { treeCalendarDetailIDparam });

            calendarDistributions = mapCalendarDistributions(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return calendarDistributions;
        }
        internal static List<Cal_Course_Prog_Node> GetByTreeCalDet(int treeCalendarDetailID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            List<Cal_Course_Prog_Node> calendarDistributions = null;

            string command = SELECT
                            + "WHERE TreeCalendarDetailID = @TreeCalendarDetailID";

            SqlParameter treeCalendarDetailIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(treeCalendarDetailID, "@TreeCalendarDetailID");

            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteBatchQuerry(command, sqlConn, sqlTran, new SqlParameter[] { treeCalendarDetailIDparam });

            calendarDistributions = mapCalendarDistributions(theReader);
            theReader.Close();

            return calendarDistributions;
        }

        public static int Save(Cal_Course_Prog_Node calendarDistribution)
        {
            int counter = 0;
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

            SqlParameter[] sqlParams = null;

            string command = string.Empty;

            if (calendarDistribution.Id == 0)
            {
                command = INSERT;
                sqlParams = new SqlParameter[] { calendarDistribution.TreeCalendarDetailIDParam, 
                                                 calendarDistribution.ProgramIDParam, 
                                                 calendarDistribution.CourseIDParam, 
                                                 calendarDistribution.VersionIDParam, 
                                                 calendarDistribution.NodeCourseIDParam,
                                                 calendarDistribution.NodeIDParam, 
                                                 calendarDistribution.NodeLinkNameParam,
                                                 calendarDistribution.PriorityParam,
                                                 calendarDistribution.CreditsParam,
                                                 calendarDistribution.IsMajorRelatedParam,
                                                   calendarDistribution.IsMinorRelatedParam,
                                                     calendarDistribution.CreatorIDParam,
                                                     calendarDistribution.CreatedDateParam,
                                                     calendarDistribution.ModifierIDParam,
                                                     calendarDistribution.ModifiedDateParam };
            }
            else
            {
                command = UPDATE
                        + " WHERE CalCorProgNodeID = " + ID_PA;
                sqlParams = new SqlParameter[] { calendarDistribution.TreeCalendarDetailIDParam, 
                                                 calendarDistribution.ProgramIDParam, 
                                                 calendarDistribution.CourseIDParam, 
                                                 calendarDistribution.VersionIDParam, 
                                                 calendarDistribution.NodeCourseIDParam, 
                                                 calendarDistribution.NodeIDParam, 
                                                 calendarDistribution.NodeLinkNameParam,
                                                 calendarDistribution.PriorityParam,
                                                 calendarDistribution.CreditsParam,
                                                 calendarDistribution.IsMajorRelatedParam,
                                                   calendarDistribution.IsMinorRelatedParam,
                                                     calendarDistribution.CreatorIDParam,
                                                     calendarDistribution.CreatedDateParam,
                                                     calendarDistribution.ModifierIDParam,
                                                     calendarDistribution.ModifiedDateParam, 
                                                 calendarDistribution.IDParam };
            }
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

            MSSqlConnectionHandler.CommitTransaction();
            MSSqlConnectionHandler.CloseDbConnection();
            return counter;
        }
        internal static int Save(Cal_Course_Prog_Node calendarDistribution, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            SqlParameter[] sqlParams = null;

            string command = string.Empty;

            if (calendarDistribution.Id == 0)
            {
                command = INSERT;
                sqlParams = new SqlParameter[] { 
                                                calendarDistribution.TreeCalendarDetailIDParam, 
                                                calendarDistribution.ProgramIDParam, 
                                                calendarDistribution.CourseIDParam, 
                                                calendarDistribution.VersionIDParam, 
                                                calendarDistribution.NodeCourseIDParam,
                                                calendarDistribution.NodeIDParam, 
                                                calendarDistribution.NodeLinkNameParam,
                                                calendarDistribution.PriorityParam,
                                                calendarDistribution.CreditsParam,
                                                calendarDistribution.IsMajorRelatedParam,
                                                calendarDistribution.IsMinorRelatedParam,
                                                calendarDistribution.CreatorIDParam,
                                                calendarDistribution.CreatedDateParam,
                                                calendarDistribution.ModifierIDParam,
                                                calendarDistribution.ModifiedDateParam };
            }
            else
            {
                command = UPDATE
                        + " WHERE CalCorProgNodeID = " + ID_PA;
                sqlParams = new SqlParameter[] { 
                                                calendarDistribution.TreeCalendarDetailIDParam, 
                                                calendarDistribution.ProgramIDParam, 
                                                calendarDistribution.CourseIDParam, 
                                                calendarDistribution.VersionIDParam, 
                                                calendarDistribution.NodeCourseIDParam,
                                                calendarDistribution.NodeIDParam, 
                                                calendarDistribution.NodeLinkNameParam,
                                                calendarDistribution.PriorityParam,
                                                calendarDistribution.CreditsParam,
                                                calendarDistribution.IsMajorRelatedParam,
                                                calendarDistribution.IsMinorRelatedParam,
                                                calendarDistribution.CreatorIDParam,
                                                calendarDistribution.CreatedDateParam,
                                                calendarDistribution.ModifierIDParam,
                                                calendarDistribution.ModifiedDateParam , 
                                                calendarDistribution.IDParam };
            }
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);
            return counter;
        }

        public static int Delete(int calendarDistributionID)
        {
            int counter = 0;
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = DELETE
                            + "WHERE CalCorProgNodeID = @CalCorProgNodeID";

            #region Parameters
            SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(calendarDistributionID, "@CalCorProgNodeID");
            #endregion

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteAction(command, sqlConn, new SqlParameter[] { sqlParam });

            MSSqlConnectionHandler.CloseDbConnection();
            return counter;
        }
        internal static int Delete(int calendarDistributionID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = DELETE
                            + "WHERE CalCorProgNodeID = @CalCorProgNodeID";

            #region Parameters
            SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(calendarDistributionID, "@CalCorProgNodeID");
            #endregion

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { sqlParam });

            return counter;
        }

        public static int DeleteByTreeCalDet(int treeCalendarDetailID)
        {
            int counter = 0;
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = DELETE
                + "WHERE TreeCalendarDetailID = @TreeCalendarDetailID";
            //+ " WHERE CalCourseProgNode.TreeCalendarDetailID IN (SELECT TreeCalendarDetail.TreeCalendarDetailID FROM TreeCalendarDetail WHERE TreeCalendarDetail.TreeCalendarMasterID = @TreeCalendarDetailID )";

            #region Parameters
            SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(treeCalendarDetailID, "@TreeCalendarDetailID");
            #endregion

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteAction(command, sqlConn, new SqlParameter[] { sqlParam });

            MSSqlConnectionHandler.CloseDbConnection();
            return counter;
        }
        internal static int DeleteByTreeCalDet(int treeCalendarDetailID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = DELETE
                + "WHERE TreeCalendarDetailID = @TreeCalendarDetailID";
            //+ " WHERE CalCourseProgNode.TreeCalendarDetailID IN (SELECT TreeCalendarDetail.TreeCalendarDetailID FROM TreeCalendarDetail WHERE TreeCalendarDetail.TreeCalendarMasterID = @TreeCalendarDetailID )";

            #region Parameters
            SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(treeCalendarDetailID, "@TreeCalendarDetailID");
            #endregion

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { sqlParam });

            return counter;
        }

        public static int DeleteByTreeCalMas(int treeCalendarMasterID)
        {
            int counter = 0;
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = DELETE
                        + " WHERE CalCourseProgNode.TreeCalendarDetailID IN (SELECT TreeCalendarDetail.TreeCalendarDetailID FROM TreeCalendarDetail WHERE TreeCalendarDetail.TreeCalendarMasterID = @TreeCalendarMasterID )";

            #region Parameters
            SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(treeCalendarMasterID, "@TreeCalendarMasterID");
            #endregion

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteAction(command, sqlConn, new SqlParameter[] { sqlParam });

            MSSqlConnectionHandler.CloseDbConnection();
            return counter;
        }
        internal static int DeleteByTreeCalMas(int treeCalendarDetailID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = DELETE
                        + " WHERE CalCourseProgNode.TreeCalendarDetailID IN (SELECT TreeCalendarDetail.TreeCalendarDetailID FROM TreeCalendarDetail WHERE TreeCalendarDetail.TreeCalendarMasterID = @TreeCalendarMasterID )";

            #region Parameters
            SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(treeCalendarDetailID, "@TreeCalendarMasterID");
            #endregion

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { sqlParam });

            return counter;
        }
        #endregion       
    }
}
