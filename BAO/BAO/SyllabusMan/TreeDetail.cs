using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;


namespace BussinessObject
{
    [Serializable]
    public class TreeDetail:Base
    {
        #region Variables
        private int _treeMasterID;
        private TreeMaster _treeMaster;
        private int _parentNodeID;
        private Node _parentNode;
        private int _childNodeID;
        private Node _childNode;
        #endregion

        #region Constructor
        public TreeDetail():base()
        {
            _treeMasterID = 0;
            _parentNodeID = 0;
            _parentNode = null;
            _childNodeID = 0;
            _childNode = null;
        } 
        #endregion

        #region Constants
        private const string TREE_DETAIL_ID = "TreeDetailID";

        private const string TREE_MASTER_ID = "TreeMasterID";
        private const string TREE_MASTER_ID_PA = "@TreeMasterID";
        private const string PARENT_NODE_ID = "ParentNodeID";
        private const string PARENT_NODE_ID_PA = "@ParentNodeID";
        private const string CHILD_NODE_ID = "ChildNodeID";
        private const string CHILD_NODE_ID_PA = "@ChildNodeID";

        private const string ALLCOLUMNS = TREE_DETAIL_ID + ", "
                                        + TREE_MASTER_ID + ", "
                                        + PARENT_NODE_ID + ", "
                                        + CHILD_NODE_ID + ", ";

        private const string NOPKCOLUMNS = TREE_MASTER_ID + ", "
                                        + PARENT_NODE_ID + ", "
                                        + CHILD_NODE_ID + ", ";

        private const string TABLENAME = " [TreeDetail] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        private const string INSERT = "INSERT INTO" + TABLENAME 
                            +"("
                            + NOPKCOLUMNS
                            + BASECOLUMNS
                            +") "
                            + "VALUES ";

        //private const string UPDATE = string.Empty;

        private const string DELETE = "DELETE FROM"+TABLENAME;
        #endregion

        #region Properties

        public int TreeMasterID
        {
            get { return _treeMasterID; }
            set { _treeMasterID = value; }
        }
        private SqlParameter TreeMasterIDParam
        {
            get
            {
                SqlParameter treeMasterIDParam = new SqlParameter();
                treeMasterIDParam.ParameterName = TREE_MASTER_ID_PA;

                treeMasterIDParam.Value = TreeMasterID;

                return treeMasterIDParam;
            }
        }
        public TreeMaster TreeMaster
        {
            get
            {
                if (_treeMaster == null)
                {
                    if (this.TreeMasterID > 0)
                    {
                        _treeMaster = TreeMaster.Get(this.TreeMasterID);
                    }
                }
                return _treeMaster;
            }
        }

        public int ParentNodeID
        {
            get { return _parentNodeID; }
            set { _parentNodeID = value; }
        }
        private SqlParameter ParentNodeIDParam
        {
            get
            {
                SqlParameter parentNodeIDParam = new SqlParameter();
                parentNodeIDParam.ParameterName = PARENT_NODE_ID_PA;

                if (ParentNodeID == 0)
                {
                    parentNodeIDParam.Value = DBNull.Value;
                }
                else
                {
                    parentNodeIDParam.Value = ParentNodeID;
                }

                return parentNodeIDParam;
            }
        }
        public Node ParentNode
        {
            get
            {
                if (_parentNode == null)
                {
                    if (this.ParentNodeID > 0)
                    {
                        _parentNode = Node.GetNode(this.ParentNodeID);
                    }
                }
                return _parentNode;
            }
        }

        public int ChildNodeID
        {
            get { return _childNodeID; }
            set { _childNodeID = value; }
        }
        private SqlParameter ChildNodeIDParam
        {
            get
            {
                SqlParameter childNodeIDParam = new SqlParameter();
                childNodeIDParam.ParameterName = CHILD_NODE_ID_PA;

                if (ChildNodeID == 0)
                {
                    childNodeIDParam.Value = DBNull.Value;
                }
                else
                {
                    childNodeIDParam.Value = ChildNodeID;
                }

                return childNodeIDParam;
            }
        }
        public Node ChildNode
        {
            get
            {
                if (_childNode == null)
                {
                    if (this.ChildNodeID > 0)
                    {
                        _childNode = Node.GetNode(this.ChildNodeID);
                    }
                }
                return _childNode;
            }
        }
        #endregion

        #region Functions
        private static TreeDetail treeDetailMapper(SQLNullHandler nullHandler)
        {
            TreeDetail treeDetail = new TreeDetail();

            treeDetail.Id = nullHandler.GetInt32("TreeDetailID");
            treeDetail.TreeMasterID = nullHandler.GetInt32("TreeMasterID");
            treeDetail.ParentNodeID = nullHandler.GetInt32("ParentNodeID");
            treeDetail.ChildNodeID = nullHandler.GetInt32("ChildNodeID");
            treeDetail.CreatorID = nullHandler.GetInt32("CreatedBy");
            treeDetail.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            treeDetail.ModifierID = nullHandler.GetInt32("ModifiedBy");
            treeDetail.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");
            return treeDetail;
        }
        private static List<TreeDetail> mapTreeDetails(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<TreeDetail> treeDetails = null;

            while (theReader.Read())
            {
                if (treeDetails == null)
                {
                    treeDetails = new List<TreeDetail>();
                }
                TreeDetail treeDetail = treeDetailMapper(nullHandler);
                treeDetails.Add(treeDetail);
            }

            return treeDetails;
        }
        private static TreeDetail mapTreeDetail(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            TreeDetail treeDetail = null;
            if (theReader.Read())
            {
                treeDetail = new TreeDetail();
                treeDetail = treeDetailMapper(nullHandler);
            }

            return treeDetail;
        }


        public static List<TreeDetail> GetByParentNode(int parentNodeID)
        {
            List<TreeDetail> treeDetails = null;

            string command = SELECT
                            + "WHERE ParentNodeID = " + parentNodeID.ToString().Trim();

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            treeDetails = mapTreeDetails(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return treeDetails;
        }
        internal static List<TreeDetail> GetByParentNode(int parentNodeID, SqlConnection sqlConn)
        {
            List<TreeDetail> treeDetails = null;

            string command = SELECT
                            + "WHERE ParentNodeID = " + parentNodeID.ToString().Trim();

            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            treeDetails = mapTreeDetails(theReader);
            theReader.Close();

            return treeDetails;
        }

        public static List<TreeDetail> GetByParentNode(int parentNodeID, int trnaMasterID)
        {
            List<TreeDetail> treeDetails = null;

            string command = SELECT
                            + "WHERE ParentNodeID = " + parentNodeID.ToString().Trim() + " AND TreeMasterID = " + trnaMasterID.ToString(); ;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            treeDetails = mapTreeDetails(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return treeDetails;
        }
        internal static List<TreeDetail> GetByParentNode(int parentNodeID, int trnaMasterID, SqlConnection sqlConn)
        {
            List<TreeDetail> treeDetails = null;

            string command = SELECT
                            + "WHERE ParentNodeID = " + parentNodeID.ToString().Trim() + " AND TreeMasterID = " + trnaMasterID.ToString(); 

            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            treeDetails = mapTreeDetails(theReader);
            theReader.Close();

            return treeDetails;
        }

        public static string GetChildNodeIDByTreeMasterID(int trnaMasterID)
        {
            DataTable tempTable = null;

            string command = "SELECT "
                            + "[ChildNodeID] "
                            + "FROM" + TABLENAME
                            + "WHERE TreeMasterID = " + trnaMasterID.ToString(); 

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            tempTable = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, "ChildNodeIDs", sqlConn);

            MSSqlConnectionHandler.CloseDbConnection();

            string childNodeIDs = string.Empty;
            foreach (DataRow item in tempTable.Rows)
            {
                if (childNodeIDs.Trim().Length == 0)
                {
                    childNodeIDs = item["ChildNodeID"].ToString();
                }
                else
                {
                    childNodeIDs += "," + item["ChildNodeID"].ToString();
                }
            }
            return childNodeIDs;
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

        public static int SaveTreeDetail(TreeDetail treeDetail)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (treeDetail.TreeMasterID == 0)
                {
                    command = INSERT + "("
                             + TREE_MASTER_ID_PA + ", "
                             + PARENT_NODE_ID_PA + ", "
                             + CHILD_NODE_ID_PA + ", "
                             + CREATORID_PA + ", "
                             + CREATEDDATE_PA + ", "
                             + MODIFIERID_PA + ", "
                             + MOIDFIEDDATE_PA
                             + ")";

                    sqlParams = new SqlParameter[] { 
                    treeDetail.TreeMasterIDParam, 
                    treeDetail.ParentNodeIDParam, 
                    treeDetail.ChildNodeIDParam, 
                    treeDetail.CreatorIDParam,
                    treeDetail.CreatedDateParam, 
                    treeDetail.ModifierIDParam, 
                    treeDetail.ModifiedDateParam };
                }
                else
                {
                    command = "UPDATE" + TABLENAME
                            + "SET TreeMasterID = " + TREE_MASTER_ID_PA + ", "
                            + "ParentNodeID = " + PARENT_NODE_ID_PA + ", "
                            + "ChildNodeID = " + CHILD_NODE_ID_PA + ", "
                            + "[CreatedBy] = " + CREATORID_PA + ","//7
                            + "[CreatedDate] = " + CREATEDDATE_PA + ","//8
                            + "[ModifiedBy] = " + MODIFIERID_PA + ","//9
                            + "[ModifiedDate] = " + MOIDFIEDDATE_PA
                            + " WHERE TreeDetailID = " + ID_PA;

                    sqlParams = new SqlParameter[] { 
                    treeDetail.TreeMasterIDParam, 
                    treeDetail.ParentNodeIDParam, 
                    treeDetail.ChildNodeIDParam, 
                    treeDetail.CreatorIDParam,
                    treeDetail.CreatedDateParam, 
                    treeDetail.ModifierIDParam, 
                    treeDetail.ModifiedDateParam,
                    treeDetail.IDParam};
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteAction(command, sqlConn, sqlParams);

                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        public static int SaveTreeDetailWithChildNode(Node childNode, TreeDetail treeDetail)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                Node.SaveNode(childNode, sqlConn, sqlTran);
                treeDetail.ChildNodeID = childNode.Id;

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (treeDetail.Id == 0)
                {
                    treeDetail.ChildNodeID = childNode.Id;
                    command = INSERT + "("
                             + TREE_MASTER_ID_PA + ", "
                             + PARENT_NODE_ID_PA + ", "
                             + CHILD_NODE_ID_PA + ", "
                             + CREATORID_PA + ", "
                             + CREATEDDATE_PA + ", "
                             + MODIFIERID_PA + ", "
                             + MOIDFIEDDATE_PA
                             + ")";

                    sqlParams = new SqlParameter[] { 
                    treeDetail.TreeMasterIDParam, 
                    treeDetail.ParentNodeIDParam, 
                    treeDetail.ChildNodeIDParam, 
                    treeDetail.CreatorIDParam,
                    treeDetail.CreatedDateParam, 
                    treeDetail.ModifierIDParam, 
                    treeDetail.ModifiedDateParam };
                }
                else
                {
                    command = "UPDATE" + TABLENAME
                            + "SET TreeMasterID = " + TREE_MASTER_ID_PA + ", "
                            + "ParentNodeID = " + PARENT_NODE_ID_PA + ", "
                            + "ChildNodeID = " + CHILD_NODE_ID_PA + ", "
                            + "[CreatedBy] = " + CREATORID_PA + ","//7
                            + "[CreatedDate] = " + CREATEDDATE_PA + ","//8
                            + "[ModifiedBy] = " + MODIFIERID_PA + ","//9
                            + "[ModifiedDate] = " + MOIDFIEDDATE_PA
                            + " WHERE TreeDetailID = " + ID_PA;

                    sqlParams = new SqlParameter[] { 
                    treeDetail.TreeMasterIDParam, 
                    treeDetail.ParentNodeIDParam, 
                    treeDetail.ChildNodeIDParam, 
                    treeDetail.CreatorIDParam,
                    treeDetail.CreatedDateParam, 
                    treeDetail.ModifierIDParam, 
                    treeDetail.ModifiedDateParam,
                    treeDetail.IDParam};
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

        public static int DeleteTreeDetail(int treeDetailID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

                string command = DELETE
                                + "WHERE TreeDetailID =" + treeDetailID.ToString();
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteAction(command, sqlConn);

                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        public static int DeleteTreeDetail(int treeMasterID, int childNodeID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = DELETE
                                + "WHERE TreeMasterID =" + treeMasterID.ToString() + " AND ChildNodeID =" + childNodeID.ToString();
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

                Node.DeleteNode(childNodeID, sqlConn, sqlTran);

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
        internal static int DeleteTreeDetailByMaster(int treeMasterID,SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = DELETE
                                + "WHERE TreeMasterID =" + treeMasterID.ToString();
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        internal static int DeleteTreeDetailByMaster(int treeMasterID, SqlConnection sqlConn, SqlTransaction sqlTran, string childNodeIDs)
        {
            try
            {
                int counter = 0;

                string[] chilNodeIDArr = childNodeIDs.Split(new char[] { ',' });

                string command = DELETE
                                + "WHERE TreeMasterID =" + treeMasterID.ToString();
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

                foreach (string childNodeID in chilNodeIDArr)
                {
                    Node.DeleteNode(Int32.Parse(childNodeID), sqlConn, sqlTran);
                }

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        public static bool IsExist(int parentNodeID, int trnaMasterID)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(TreeDetailID) FROM" + TABLENAME
                            + "WHERE ParentNodeID = " + parentNodeID.ToString().Trim() + " AND TreeMasterID = " + trnaMasterID.ToString();
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

            MSSqlConnectionHandler.CloseDbConnection();

            return (Convert.ToInt32(ob) > 0);
        }
        public static List<TreeDetail> GetByMasterID(int trnaMasterID)
        {
            List<TreeDetail> treeDetails = null;

            string command = SELECT
                            + "WHERE TreeMasterID = " + trnaMasterID; 

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            treeDetails = mapTreeDetails(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return treeDetails;
        }
        public static List<TreeDetail> GetRelationalNodes(int parentNodeID, int treeMasID)
        {
            List<TreeDetail> treeDetails = null;
            string command = string.Empty;
            if (treeMasID != 0)
            {
                command = "with tab as ( select * from " + TABLENAME + " where ParentNodeID = " + parentNodeID +
                                " and TreeMasterID = " + treeMasID +
                                " union all select v.* from " + TABLENAME + " v INNER JOIN tab t ON v.ParentNodeID = t.ChildNodeID ) select * from tab order by 3";
            }
            else
            {
                command = "with tab as ( select * from " + TABLENAME + " where ParentNodeID = " + parentNodeID +
                                " union all select v.* from " + TABLENAME + " v INNER JOIN tab t ON v.ParentNodeID = t.ChildNodeID ) select * from tab order by 3";
            }

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            treeDetails = mapTreeDetails(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return treeDetails;

        }
        #endregion
    }
}
