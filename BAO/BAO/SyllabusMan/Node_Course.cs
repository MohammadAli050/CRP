using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class NodeCourse :Base
    {
        #region DBColumns
        //Node_CourseID	        int	            Unchecked
        //NodeID	            int	            Checked
        //CourseID	            int	            Checked
        //VersionID	            int	            Checked
        //Priority	            int	            Unchecked
        //IsActive	            bit	            Unchecked
        //PassingGPA	        numeric(18, 2)	Unchecked
        //CreatedBy	            int	            Unchecked
        //CreatedDate	        datetime	    Unchecked
        //ModifiedBy	        int	            Checked
        //ModifiedDate	        datetime	    Checked
        #endregion

        #region Variables
        private int _parentNodeID;
        private Node _parentNode = null;
        private int _childCourseID;
        private int _childVersionID;
        private Course _childCourse = null;
        private Course _childActiveCourse = null;
        private int _priority;
        private bool _isActive;
        private decimal _passingGPA;
        private List<PreRequisiteMaster> _preReqMasters = null;
        private bool _hasPreriquisite = false;
        #endregion

        #region Constants
        #region Columns
        private const string NODE_COURSEID = "Node_CourseID";

        private const string NODEID = "NodeID";
        private const string NODEID_PA = "@NodeID";

        private const string COURSEID = "CourseID";
        private const string COURSEID_PA = "@CourseID";

        private const string VERSIONID = "VersionID";
        private const string VERSIONID_PA = "@VersionID";

        private const string PRIORITY = "Priority";
        private const string PRIORITY_PA = "@Priority";

        private const string PASSINGGPA = "PassingGPA";
        private const string PASSINGGPA_PA = "@PassingGPA";

        private const string ISACTIVE = "IsActive";//15
        private const string ISACTIVE_PA = "@IsActive"; 
        #endregion

        #region Allcolumns
        private const string ALLCOLUMNS = NODE_COURSEID + ", "
                                + NODEID + ", "
                                + COURSEID + ", "
                                + VERSIONID + ", "
                                + PRIORITY + ", "
                                + PASSINGGPA + ", "
                                + ISACTIVE + ", "; 
        #endregion

        #region NoPkColumns
        private const string NOPKCOLUMNS = NODEID + ", "
                                + COURSEID + ", "
                                + VERSIONID + ", "
                                + PRIORITY + ", "
                                + PASSINGGPA + ", "
                                + ISACTIVE + ", "; 
        #endregion

        private const string TABLENAME = " [NodeCourse] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        #region Insert
        private const string INSERT = "INSERT INTO" + TABLENAME
                     + "("
                     + ALLCOLUMNS
                     + BASECOLUMNS
                     + ")"
                     + "VALUES ( "
                     + ID_PA + ", "
                     + NODEID_PA + ", "
                     + COURSEID_PA + ", "
                     + VERSIONID_PA + ", "
                     + PRIORITY_PA + ", "
                     + PASSINGGPA_PA + ", "
                     + ISACTIVE_PA + ", "
                     + CREATORID_PA + ", "
                     + CREATEDDATE_PA + ", "
                     + MODIFIERID_PA + ", "
                     + MOIDFIEDDATE_PA + ")"; 
        #endregion

        #region Update
        private const string UPDATE = "UPDATE" + TABLENAME
        + "SET " + NODEID + " = " + NODEID_PA + ", "
        + COURSEID + " = " + COURSEID_PA + ", "
        + VERSIONID + " = " + VERSIONID_PA + ", "
        + PRIORITY + " = " + PRIORITY_PA + ", "
        + PASSINGGPA + " = " + PASSINGGPA_PA + ", "
        + ISACTIVE + " = " + ISACTIVE_PA + ", "
        + CREATORID + " = " + CREATORID_PA + ","//7
        + CREATEDDATE + " = " + CREATEDDATE_PA + ","//8
        + MODIFIERID + " = " + MODIFIERID_PA + ","//9
        + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA; 
        #endregion

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Constructor
        public NodeCourse()
            : base()
        {
            _parentNodeID = 0;
            _parentNode = null;
            _childCourseID = 0;
            _childVersionID = 0;
            _childCourse = null;
            _priority = 0;
            _passingGPA = 0;
            _isActive = true;
        }
        #endregion

        #region Properties

        #region Parent Node
        public int ParentNodeID
        {
            get { return _parentNodeID; }
            set { _parentNodeID = value; }
        }
        private SqlParameter ParentNodeIDParam
        {
            get
            {
                SqlParameter ownerNodeIDParam = new SqlParameter();
                ownerNodeIDParam.ParameterName = NODEID_PA;

                ownerNodeIDParam.Value = ParentNodeID;

                return ownerNodeIDParam;
            }
        }
        public Node ParentNode
        {
            get
            {
                if (_parentNode == null)
                {
                    if (_parentNodeID > 0)
                    {
                        _parentNode = Node.GetNode(_parentNodeID);
                    }
                }
                return _parentNode;
            }
        }
        #endregion

        #region Child Course
        public int ChildCourseID
        {
            get { return _childCourseID; }
            set { _childCourseID = value; }
        }
        private SqlParameter ChildCourseIDParam
        {
            get
            {
                SqlParameter oparandCourseIDParam = new SqlParameter();
                oparandCourseIDParam.ParameterName = COURSEID_PA;
                if (ChildCourseID == 0)
                {
                    oparandCourseIDParam.Value = DBNull.Value;
                }
                else
                {
                    oparandCourseIDParam.Value = ChildCourseID;
                }
                return oparandCourseIDParam;
            }
        }
        public int ChildVersionID
        {
            get { return _childVersionID; }
            set { _childVersionID = value; }
        }
        private SqlParameter ChildVersionIDParam
        {
            get
            {
                SqlParameter oparandVersionIDParam = new SqlParameter();
                oparandVersionIDParam.ParameterName = VERSIONID_PA;
                if (ChildVersionID == 0)
                {
                    oparandVersionIDParam.Value = DBNull.Value;
                }
                else
                {
                    oparandVersionIDParam.Value = ChildVersionID;
                }
                return oparandVersionIDParam;
            }
        }
        public Course ChildCourse
        {
            get
            {
                if (_childCourse == null)
                {
                    if ((_childCourseID > 0) && (_childVersionID > 0))
                    {
                        _childCourse = Course.GetCourse(_childCourseID, _childVersionID);
                    }
                }
                return _childCourse;
            }
        }
        public Course ChildActiveCourse
        {
            get
            {
                if (_childActiveCourse == null)
                {
                    if ((_childCourseID > 0) && (_childVersionID > 0))
                    {
                        _childActiveCourse = Course.GetActiveCourse(_childCourseID, _childVersionID);
                    }
                }
                return _childActiveCourse;
            }
        }
        #endregion

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
                priorityParam.ParameterName = PRIORITY_PA;
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

        public decimal PassingGPA
        {
            get { return _passingGPA; }
            set { _passingGPA = value; }
        }
        private SqlParameter PassingGPAParam
        {
            get
            {
                SqlParameter passingGPAParam = new SqlParameter();
                passingGPAParam.ParameterName = PASSINGGPA_PA;
                if (PassingGPA > 0)
                {
                    passingGPAParam.Value = PassingGPA;
                }
                else
                {
                    passingGPAParam.Value = DBNull.Value;
                }
                return passingGPAParam;
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

        public List<PreRequisiteMaster> PreReqMasters
        {
            get 
            {
                if (_preReqMasters == null)
                {
                    _preReqMasters = PreRequisiteMaster.GetsByNodeCourse(this.Id);
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
        #endregion

        #region Functions
        private static NodeCourse node_CourseMapper(SQLNullHandler nullHandler)
        {
            NodeCourse node_Course = new NodeCourse();

            node_Course.Id = nullHandler.GetInt32(NODE_COURSEID);
            node_Course.ParentNodeID = nullHandler.GetInt32(NODEID);
            node_Course.ChildCourseID = nullHandler.GetInt32(COURSEID);
            node_Course.ChildVersionID = nullHandler.GetInt32(VERSIONID);
            node_Course.Priority = nullHandler.GetInt32(PRIORITY);
            node_Course.IsActive = nullHandler.GetBoolean(ISACTIVE);
            node_Course.CreatorID = nullHandler.GetInt32(CREATORID);
            node_Course.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            node_Course.ModifierID = nullHandler.GetInt32(MODIFIERID);
            node_Course.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);
            return node_Course;
        }
        private static List<NodeCourse> mapNode_Courses(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            
            List<NodeCourse> node_Courses = null;
            
            while (theReader.Read())
            {
                if (node_Courses == null)
                {
                    node_Courses = new List<NodeCourse>();
                }
                NodeCourse node_Course = node_CourseMapper(nullHandler);
                node_Courses.Add(node_Course);
            }

            return node_Courses;
        }
        private static NodeCourse mapNode_Course(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            NodeCourse node_Course = null;
            if (theReader.Read())
            {
                node_Course = new NodeCourse();
                node_Course = node_CourseMapper(nullHandler);
            }

            return node_Course;
        }

        #region Old
        //public static Node_Course getNRST(int node_CourseID)
        //{
        //    string command = SELECT
        //                    + "WHERE Tpl_CourseID = " + node_CourseID.ToString().Trim();

        //    SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
        //    SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

        //    Node_Course node_course = mapNode_Course(theReader);

        //    theReader.Close();


        //    MSSqlConnectionHandler.CloseDbConnection();

        //    return node_course;
        //}
        #endregion

        internal static int GetMaxNode_CourseID(SqlConnection sqlConn)
        {
            int newCourseID = 0;

            string command = "SELECT MAX(Node_CourseID) FROM" + TABLENAME;
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

            if (ob == null || ob == DBNull.Value)
            {
                newCourseID = 1;
            }
            else if (ob is Int32)
            {
                newCourseID = Convert.ToInt32(ob) + 1;
            }

            return newCourseID;
        }
        internal static int GetMaxNode_CourseID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newCourseID = 0;

            string command = "SELECT MAX(Node_CourseID) FROM" + TABLENAME;
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

            if (ob == null || ob == DBNull.Value)
            {
                newCourseID = 1;
            }
            else if (ob is Int32)
            {
                newCourseID = Convert.ToInt32(ob) + 1;
            }

            return newCourseID;
        }

        public static List<NodeCourse> GetByParentNode(int nodeID)
        {
            List<NodeCourse> node_Courses = new List<NodeCourse>();

            string command = SELECT
                            + "WHERE NodeID = " + nodeID.ToString().Trim();

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            node_Courses = mapNode_Courses(theReader);

            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return node_Courses;
        }
        internal static List<NodeCourse> GetByParentNode(int nodeID, SqlConnection sqlConn)
        {
            List<NodeCourse> template_Courses = new List<NodeCourse>();

            string command = SELECT
                            + "WHERE NodeID = " + nodeID.ToString().Trim();

            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            template_Courses = mapNode_Courses(theReader);

            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return template_Courses;
        }

        public static NodeCourse GetByParentNode(int parentNodeID, int childCourseID, int childVersionID)
        {
            NodeCourse node_Course = new NodeCourse();

            string command = SELECT
                            + "WHERE NodeID = " + parentNodeID.ToString() + " AND CourseID = " + childCourseID.ToString() + " AND VersionID = " + childVersionID.ToString();

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            node_Course = mapNode_Course(theReader);

            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return node_Course;
        }

        public static int SaveNode_Courses(List<NodeCourse> node_Courses)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                foreach (NodeCourse node_Course in node_Courses)
                {
                    string command = string.Empty;
                    SqlParameter[] sqlParams = null;

                    if (node_Course.Id == 0)
                    {
                        node_Course.Id = NodeCourse.GetMaxNode_CourseID(sqlConn,sqlTran);
                        command = INSERT;

                        sqlParams = new SqlParameter[] { node_Course.IDParam,  
                                                 node_Course.ParentNodeIDParam, 
                                                 node_Course.ChildCourseIDParam, 
                                                 node_Course.ChildVersionIDParam, 
                                                 node_Course.PriorityParam, 
                                                 node_Course.PassingGPAParam,
                                                 node_Course.IsActiveParam,
                                                 node_Course.CreatorIDParam,
                                                 node_Course.CreatedDateParam,
                                                 node_Course.ModifierIDParam,
                                                 node_Course.ModifiedDateParam};
                    }
                    else
                    {
                        command = UPDATE
                                + " WHERE [" + NODE_COURSEID + "] = " + ID_PA;

                        sqlParams = new SqlParameter[] { node_Course.ParentNodeIDParam, 
                                                 node_Course.ChildCourseIDParam, 
                                                 node_Course.ChildVersionIDParam, 
                                                 node_Course.PriorityParam, 
                                                 node_Course.PassingGPAParam,
                                                 node_Course.IsActiveParam,
                                                 node_Course.CreatorIDParam,
                                                 node_Course.CreatedDateParam,
                                                 node_Course.ModifierIDParam,
                                                 node_Course.ModifiedDateParam,
                                                 node_Course.IDParam};
                    }
                    counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);
                    PreRequisiteMaster.DeleteByNodeCourse(node_Course.Id, sqlConn, sqlTran);

                    if (node_Course.HasPreriquisite)
                    {
                        foreach (PreRequisiteMaster item in node_Course.PreReqMasters)
                        {
                            item.NodeCourseID = node_Course.Id;
                            PreRequisiteMaster.Save(item, sqlConn, sqlTran);
                        }
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
        public static int SaveNode_Course(NodeCourse node_Course)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                Node.UpdateLastLvlFlag(node_Course.ParentNodeID, true, sqlConn, sqlTran);

                string command = string.Empty;
                SqlParameter[] sqlParams = null;


                if (node_Course.Id == 0)
                {
                    node_Course.Id = NodeCourse.GetMaxNode_CourseID(sqlConn,sqlTran);
                    command = INSERT;

                    sqlParams = new SqlParameter[] { node_Course.IDParam,  
                                                 node_Course.ParentNodeIDParam, 
                                                 node_Course.ChildCourseIDParam, 
                                                 node_Course.ChildVersionIDParam, 
                                                 node_Course.PriorityParam, 
                                                 node_Course.PassingGPAParam,
                                                 node_Course.IsActiveParam,
                                                 node_Course.CreatorIDParam,
                                                 node_Course.CreatedDateParam,
                                                 node_Course.ModifierIDParam,
                                                 node_Course.ModifiedDateParam};
                }
                else
                {
                    command = UPDATE
                            + " WHERE [" + NODE_COURSEID + "] = " + ID_PA;

                    sqlParams = new SqlParameter[] { node_Course.ParentNodeIDParam, 
                                                 node_Course.ChildCourseIDParam, 
                                                 node_Course.ChildVersionIDParam, 
                                                 node_Course.PriorityParam, 
                                                 node_Course.PassingGPAParam,
                                                 node_Course.IsActiveParam,
                                                 node_Course.CreatorIDParam,
                                                 node_Course.CreatedDateParam,
                                                 node_Course.ModifierIDParam,
                                                 node_Course.ModifiedDateParam,
                                                 node_Course.IDParam};
                }

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                PreRequisiteMaster.DeleteByNodeCourse(node_Course.Id, sqlConn, sqlTran);

                if (node_Course.HasPreriquisite)
                {
                    foreach (PreRequisiteMaster item in node_Course.PreReqMasters)
                    {
                        item.NodeCourseID = node_Course.Id;
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

        public static int DeleteNode_Course(int node_CourseID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                PreReqDetail.DeleteByPreReqNodeCourseID(node_CourseID, sqlConn, sqlTran);
                PreRequisiteMaster.DeleteByNodeCourse(node_CourseID, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE Node_CourseID =" + NODE_COURSEID;

                SqlParameter sqlParam = new SqlParameter(NODE_COURSEID,node_CourseID);
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
        public static int DeleteNode_Course(int parentNodeID, int childCourseID, int childVersionID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                PreReqDetail.DeleteByParentNodeIDCourseIDOPreNodeCourse(parentNodeID, childCourseID, childVersionID, sqlConn, sqlTran);
                PreRequisiteMaster.DeleteByParentNodeIDCourseIDONodeCourse(parentNodeID, childCourseID, childVersionID, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE NodeID = " + parentNodeID.ToString() + " AND CourseID = " + childCourseID.ToString() + " AND VersionID = " + childVersionID.ToString();
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
        public static int DeleteNode_CourseWithParentDeflag(int parentNodeID, int childCourseID, int childVersionID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                PreReqDetail.DeleteByParentNodeIDCourseIDOPreNodeCourse(parentNodeID, childCourseID, childVersionID, sqlConn, sqlTran);
                PreRequisiteMaster.DeleteByParentNodeIDCourseIDONodeCourse(parentNodeID, childCourseID, childVersionID, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE NodeID = " + parentNodeID.ToString() + " AND CourseID = " + childCourseID.ToString() + " AND VersionID = " + childVersionID.ToString();
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

                Node.UpdateLastLvlFlag(parentNodeID, false, sqlConn, sqlTran);

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
        internal static int DeleteNode_Course(int node_CourseID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;
                PreReqDetail.DeleteByPreReqNodeCourseID(node_CourseID, sqlConn, sqlTran);
                PreRequisiteMaster.DeleteByNodeCourse(node_CourseID, sqlConn, sqlTran);
                string command = DELETE
                                + "WHERE Node_CourseID = " + node_CourseID;
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        internal static int DeleteNode_CourseByParentNode(int parentNodeID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                PreReqDetail.DeleteByParentNodeIDOPreNodeCourse(parentNodeID, sqlConn, sqlTran);
                PreRequisiteMaster.DeleteByParentNodeIDONodeCourse(parentNodeID, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE NodeID =" + parentNodeID.ToString();
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        
        public static List<NodeCourse> GetNodeCoursesByRoot(int intRoot)
        {
            List<NodeCourse> nodeCourseCollection = new List<NodeCourse>();
            TreeMaster treeMaster = TreeMaster.Get(intRoot);
            List<TreeDetail> treeDetail = TreeDetail.GetByMasterID(treeMaster.Id);
            if (treeDetail != null)
            {
                if (treeDetail.Count > 0)
                {
                    foreach (TreeDetail td in treeDetail)
                    {
                        List<NodeCourse> nc = GetByParentNode(td.ChildNodeID);
                        if (nc != null)
                        {
                            nodeCourseCollection.AddRange(nc);
                        }
                    }
                } 
            }

            return nodeCourseCollection;
        }
        public static NodeCourse GetNodeCourse(int intNodeCourseID)
        {
            NodeCourse node_Course = new NodeCourse();

            string command = SELECT
                            + "WHERE Node_CourseID = " + intNodeCourseID.ToString();

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            node_Course = mapNode_Course(theReader);

            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return node_Course;
        }
        /// <summary>
        /// get all node_courses
        /// </summary>
        /// <returns></returns>
        public static List<NodeCourse> GetNodeCourses()
        {
            List<NodeCourse> node_Courses = new List<NodeCourse>();

            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            node_Courses = mapNode_Courses(theReader);

            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return node_Courses;
        }        
        public static List<NodeCourse> GetNodeCoursesByCourseVersionID(int courseID, int versionID)
        {
            List<NodeCourse> node_Courses = new List<NodeCourse>();

            string command = SELECT + " WHERE CourseID = " + courseID + " and VersionID = " + versionID;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            node_Courses = mapNode_Courses(theReader);

            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return node_Courses;
        }
        public static List<NodeCourse> GetNodeCoursesByFormalCourseCode(string formalCourseCode)
        {
            List<NodeCourse> node_Courses = new List<NodeCourse>();
            List<Course> courses = Course.GetCoursesByCode(formalCourseCode);
            if (courses != null)
            {
                foreach (Course cs in courses)
                {
                    List<NodeCourse> ncourses = NodeCourse.GetNodeCoursesByCourseVersionID(cs.Id, cs.VersionID);
                    if (ncourses != null)
                    {
                        foreach (NodeCourse nc in ncourses)
                        {
                            node_Courses.Add(nc);
                        }
                    }
                }
            }                        

            return node_Courses;
        }

        #endregion
    }
}
