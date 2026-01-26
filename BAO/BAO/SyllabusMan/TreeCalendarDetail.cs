using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class TreeCalendarDetail : Base
    {
        #region Variables
        private int _treeCalendarMasterID;


        private TreeCalendarMaster _treeCalendarMaster;


        private int _treeMasterID;
        private TreeMaster _treeMaster;
        private int _calendarMasterID;
        private CalendarUnitMaster _calendarMaster;
        private int _calendarUnitDistributionID;
        private CalenderUnitDistribution _calendarDetail;
        private List<Cal_Course_Prog_Node> _cal_Course_Prog_Nodes;
        #endregion

        #region Constants
        private const string SELECT = "SELECT "
                            + "[TreeCalendarDetailID], "
                            + "[TreeCalendarMasterID], "
                            + "[TreeMasterID], "
                            + "[CalendarMasterID], "
                            + "[CalendarUnitDistributionID], "
                            + BASECOLUMNS
                            + "FROM [TreeCalendarDetail] ";

        private const string INSERT = "INSERT INTO [TreeCalendarDetail] ("
                            + "[TreeCalendarDetailID], "
                            + "[TreeCalendarMasterID], "
                            + "[TreeMasterID], "
                            + "[CalendarMasterID], "
                            + "[CalendarUnitDistributionID], "
                            + BASECOLUMNS
                            + ") "
                            + "VALUES ( "
                            + ID_PA+", "
                            + "@TreeCalendarMasterID, "
                            + "@TreeMasterID, "
                            + "@CalendarMasterID, "
                            + "@CalendarUnitDistributionID, "
                            + CREATORID_PA + ", "
                            + CREATEDDATE_PA + ", "
                            + MODIFIERID_PA + ", "
                            + MOIDFIEDDATE_PA
                            + ")";

        private const string UPDATE = "UPDATE TreeCalendarDetail "
                            + "SET TreeCalendarMasterID = @TreeCalendarMasterID, "
                            + "TreeMasterID = @TreeMasterID, "
                            + "CalendarMasterID = @CalendarMasterID, "
                            + "CalendarUnitDistributionID = @CalendarUnitDistributionID, "
                            + CREATORID + " = " + CREATORID_PA + ", "
                            + CREATEDDATE + " = " + CREATEDDATE_PA + ", "
                            + MODIFIERID + " = " + MODIFIERID_PA + ", "
                            + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM [TreeCalendarDetail] ";
        #endregion

        #region Constructor
        public TreeCalendarDetail():base()
        {
            _treeMasterID = 0;
            _treeMaster = null;
            _calendarMasterID = 0;
            _calendarMaster = null;
            _cal_Course_Prog_Nodes = null;
        } 
        #endregion

        #region Properties
        public int TreeCalendarMasterID
        {
            get { return _treeCalendarMasterID; }
            set { _treeCalendarMasterID = value; }
        }
        private SqlParameter TreeCalendarMasterIDParam
        {
            get
            {
                SqlParameter treeCalendarMasterIDParam = new SqlParameter();
                treeCalendarMasterIDParam.ParameterName = "@TreeCalendarMasterID";

                treeCalendarMasterIDParam.Value = _treeCalendarMasterID;

                return treeCalendarMasterIDParam;
            }
        }
        public TreeCalendarMaster TreeCalendarMaster
        {
            get 
            {
                if (_treeCalendarMaster == null)
                {
                    if (this.TreeCalendarMasterID > 0)
                    {
                        _treeCalendarMaster = TreeCalendarMaster.Get(this.TreeCalendarMasterID);
                    }
                }
                return _treeCalendarMaster; 
            }
            set { _treeCalendarMaster = value; }
        }

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
                treeMasterIDParam.ParameterName = "@TreeMasterID";

                treeMasterIDParam.Value = _treeMasterID;

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
            private set { _treeMaster = value; }
        }

        public int CalendarMasterID
        {
            get { return _calendarMasterID; }
            set { _calendarMasterID = value; }
        }
        private SqlParameter CalendarMasterIDParam
        {
            get
            {
                SqlParameter calendarMasterIDParam = new SqlParameter();
                calendarMasterIDParam.ParameterName = "@CalendarMasterID";

                calendarMasterIDParam.Value = _calendarMasterID;

                return calendarMasterIDParam;
            }
        }
        public CalendarUnitMaster CalendarMaster
        {
            get 
            {
                if (_calendarMaster == null)
                {
                    if (this.CalendarMasterID > 0)
                    {
                        _calendarMaster = CalendarUnitMaster.GetCalendarMaster(this.CalendarMasterID);
                    }
                }
                return _calendarMaster; 
            }
            private set { _calendarMaster = value; }
        }

        public int CalendarUnitDistributionID
        {
            get { return _calendarUnitDistributionID; }
            set { _calendarUnitDistributionID = value; }
        }
        private SqlParameter CalendarUnitDistributionIDParam
        {
            get
            {
                SqlParameter calendarUnitDistributionIDParam = new SqlParameter();
                calendarUnitDistributionIDParam.ParameterName = "@CalendarUnitDistributionID";

                calendarUnitDistributionIDParam.Value = _calendarUnitDistributionID;

                return calendarUnitDistributionIDParam;
            }
        }
        public CalenderUnitDistribution CalendarDetail
        {
            get 
            {
                if (_calendarDetail == null)
                {
                    if (this.CalendarUnitDistributionID > 0)
                    {
                        _calendarDetail = CalenderUnitDistribution.GetCalendarDetail(this.CalendarUnitDistributionID);
                    }
                }
                return _calendarDetail; 
            }
            private set { _calendarDetail = value; }
        }

        public List<Cal_Course_Prog_Node> Cal_Course_Prog_Nodes
        {
            get
            {
                if (_cal_Course_Prog_Nodes == null)
                {
                    if (this.Id > 0)
                    {
                        _cal_Course_Prog_Nodes = Cal_Course_Prog_Node.GetByTreeCalDet(this.Id);
                    }
                }
                return _cal_Course_Prog_Nodes;
            }
        }
        #endregion

        #region Functions
        private static TreeCalendarDetail treeCalDetailMapper(SQLNullHandler nullHandler)
        {
            TreeCalendarDetail treeDetail = new TreeCalendarDetail();

            treeDetail.Id = nullHandler.GetInt32("TreeCalendarDetailID");
            treeDetail.TreeCalendarMasterID = nullHandler.GetInt32("TreeCalendarMasterID");
            treeDetail.TreeMasterID = nullHandler.GetInt32("TreeMasterID");
            treeDetail.CalendarMasterID = nullHandler.GetInt32("CalendarMasterID");
            treeDetail.CalendarUnitDistributionID = nullHandler.GetInt32("CalendarUnitDistributionID");
            treeDetail.CreatorID = nullHandler.GetInt32("CreatedBy");
            treeDetail.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            treeDetail.ModifierID = nullHandler.GetInt32("ModifiedBy");
            treeDetail.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");
            return treeDetail;
        }
        private static List<TreeCalendarDetail> mapTreeCalDetails(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<TreeCalendarDetail> treeCalDetails = null;

            while (theReader.Read())
            {
                if (treeCalDetails == null)
                {
                    treeCalDetails = new List<TreeCalendarDetail>();
                }
                TreeCalendarDetail treeDetail = treeCalDetailMapper(nullHandler);
                treeCalDetails.Add(treeDetail);
            }

            return treeCalDetails;
        }
        private static TreeCalendarDetail mapTreeCalDetail(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            TreeCalendarDetail treeCalDetail = null;
            if (theReader.Read())
            {
                treeCalDetail = new TreeCalendarDetail();
                treeCalDetail = treeCalDetailMapper(nullHandler);
            }

            return treeCalDetail;
        }

        internal static int GetMaxTreeCalDetailID(SqlConnection sqlConn)
        {
            int newTreeCalDetailID = 0;

            string command = "SELECT MAX(TreeCalendarDetailID) FROM [TreeCalendarDetail]";
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

            if (ob == null || ob == DBNull.Value)
            {
                newTreeCalDetailID = 1;
            }
            else if (ob is Int32)
            {
                newTreeCalDetailID = Convert.ToInt32(ob) + 1;
            }

            return newTreeCalDetailID;
        }
        internal static int GetMaxTreeCalDetailID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newTreeCalDetailID = 0;

            string command = "SELECT MAX(TreeCalendarDetailID) FROM [TreeCalendarDetail]";
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

            if (ob == null || ob == DBNull.Value)
            {
                newTreeCalDetailID = 1;
            }
            else if (ob is Int32)
            {
                newTreeCalDetailID = Convert.ToInt32(ob) + 1;
            }

            return newTreeCalDetailID;
        }

        public static List<TreeCalendarDetail> Get()
        {
            List<TreeCalendarDetail> treeCalendarDetails = null;

            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            treeCalendarDetails = mapTreeCalDetails(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return treeCalendarDetails;
        }

        public static TreeCalendarDetail Get(int treeCalendarDetailID)
        {
            TreeCalendarDetail treeCalendarDetail = null;

            string command = SELECT
                            + "WHERE TreeCalendarDetailID = @TreeCalendarDetailID";

            SqlParameter treeCalDetailIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(treeCalendarDetailID, "@TreeCalendarDetailID");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { treeCalDetailIDParam });

            treeCalendarDetail = mapTreeCalDetail(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return treeCalendarDetail;
        }
        internal static TreeCalendarDetail Get(int treeCalendarDetailID, SqlConnection sqlConn)
        {
            TreeCalendarDetail treeCalendarDetail = null;

            string command = SELECT
                            + "WHERE TreeCalendarDetailID = @TreeCalendarDetailID";

            SqlParameter treeCalendarDetailIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(treeCalendarDetailID, "@TreeCalendarDetailID");
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { treeCalendarDetailIDParam });

            treeCalendarDetail = mapTreeCalDetail(theReader);
            theReader.Close();
            //SqlConnectionHandler.CloseDbConnection();
            return treeCalendarDetail;
        }

        public static List<TreeCalendarDetail> GetByTreeCalMaster(int treeCalMasterID)
        {
            List<TreeCalendarDetail> treeDetails = null;

            string command = SELECT
                            + "WHERE TreeCalendarMasterID = @TreeCalendarMasterID";

            SqlParameter treeCalMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(treeCalMasterID, "@TreeCalendarMasterID");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { treeCalMasterIDParam });

            treeDetails = mapTreeCalDetails(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return treeDetails;
        }
        internal static List<TreeCalendarDetail> GetByTreeCalMaster(int tranCalMasterID, SqlConnection sqlConn)
        {
            List<TreeCalendarDetail> treeDetails = null;

            string command = SELECT
                            + "WHERE TreeCalendarMasterID = @TreeCalendarMasterID";

            SqlParameter treeCalMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(tranCalMasterID, "@TreeCalendarMasterID");

            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { treeCalMasterIDParam });

            treeDetails = mapTreeCalDetails(theReader);
            theReader.Close();

            return treeDetails;
        }
        internal static List<TreeCalendarDetail> GetByTreeCalMaster(int tranCalMasterID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            List<TreeCalendarDetail> treeDetails = null;

            string command = SELECT
                            + "WHERE TreeCalendarMasterID = @TreeCalendarMasterID";

            SqlParameter treeCalMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(tranCalMasterID, "@TreeCalendarMasterID");

            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteBatchQuerry(command, sqlConn, sqlTran, new SqlParameter[] { treeCalMasterIDParam });

            treeDetails = mapTreeCalDetails(theReader);
            theReader.Close();

            return treeDetails;
        }

        public static List<TreeCalendarDetail> GetByTreeMaster(int tranMasterID)
        {
            List<TreeCalendarDetail> treeDetails = null;

            string command = SELECT
                            + "WHERE TreeMasterID = @TreeMasterID";

            SqlParameter treeMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(tranMasterID, "@TreeMasterID");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { treeMasterIDParam });

            treeDetails = mapTreeCalDetails(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return treeDetails;
        }
        internal static List<TreeCalendarDetail> GetByTreeMaster(int tranMasterID, SqlConnection sqlConn)
        {
            List<TreeCalendarDetail> treeDetails = null;

            string command = SELECT
                            + "WHERE TreeMasterID = @TreeMasterID";

            SqlParameter treeMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(tranMasterID, "@TreeMasterID");

            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { treeMasterIDParam });

            treeDetails = mapTreeCalDetails(theReader);
            theReader.Close();

            return treeDetails;
        }

        public static List<TreeCalendarDetail> GetByCalMaster(int calMasterID)
        {
            List<TreeCalendarDetail> treeDetails = null;

            string command = SELECT
                            + "WHERE CalendarMasterID = @CalendarMasterID";

            SqlParameter calMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(calMasterID, "@CalendarMasterID");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { calMasterIDParam });

            treeDetails = mapTreeCalDetails(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return treeDetails;
        }
        internal static List<TreeCalendarDetail> GetByCalMaster(int calMasterID, SqlConnection sqlConn)
        {
            List<TreeCalendarDetail> treeDetails = null;

            string command = SELECT
                            + "WHERE CalendarMasterID = @CalendarMasterID";

            SqlParameter calMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(calMasterID, "@CalendarMasterID");

            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { calMasterIDParam });

            treeDetails = mapTreeCalDetails(theReader);
            theReader.Close();

            return treeDetails;
        }

        public static int Save(TreeCalendarDetail treeCalendarDetail)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (treeCalendarDetail.Id == 0)
                {
                    treeCalendarDetail.Id = TreeCalendarDetail.GetMaxTreeCalDetailID(sqlConn, sqlTran);
                    command = INSERT;
                    sqlParams = new SqlParameter[] { treeCalendarDetail.IDParam, 
                        treeCalendarDetail.TreeCalendarMasterIDParam, 
                        treeCalendarDetail.TreeMasterIDParam, 
                        treeCalendarDetail.CalendarMasterIDParam, 
                        treeCalendarDetail.CalendarUnitDistributionIDParam,
                         treeCalendarDetail.CreatorIDParam,
                         treeCalendarDetail.CreatedDateParam,
                         treeCalendarDetail.ModifierIDParam,
                         treeCalendarDetail.ModifiedDateParam  };
                }
                else
                {
                    command = UPDATE
                            + " WHERE TreeCalendarDetailID = " + ID_PA;
                    sqlParams = new SqlParameter[] { treeCalendarDetail.TreeCalendarMasterIDParam, 
                        treeCalendarDetail.TreeMasterIDParam, 
                        treeCalendarDetail.CalendarMasterIDParam, 
                        treeCalendarDetail.CalendarUnitDistributionIDParam,
                         treeCalendarDetail.CreatorIDParam,
                         treeCalendarDetail.CreatedDateParam,
                         treeCalendarDetail.ModifierIDParam,
                         treeCalendarDetail.ModifiedDateParam,  
                        treeCalendarDetail.IDParam };
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
        internal static int Save(TreeCalendarDetail treeCalendarDetail, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = string.Empty;

            SqlParameter[] sqlParams = null;

            if (treeCalendarDetail.Id == 0)
            {
                treeCalendarDetail.Id = TreeCalendarDetail.GetMaxTreeCalDetailID(sqlConn, sqlTran);
                command = INSERT;
                sqlParams = new SqlParameter[] { treeCalendarDetail.IDParam, 
                        treeCalendarDetail.TreeCalendarMasterIDParam, 
                        treeCalendarDetail.TreeMasterIDParam, 
                        treeCalendarDetail.CalendarMasterIDParam, 
                        treeCalendarDetail.CalendarUnitDistributionIDParam,
                         treeCalendarDetail.CreatorIDParam,
                         treeCalendarDetail.CreatedDateParam,
                         treeCalendarDetail.ModifierIDParam,
                         treeCalendarDetail.ModifiedDateParam  };
            }
            else
            {
                command = UPDATE
                        + " WHERE TreeCalendarDetailID = " + ID_PA;
                sqlParams = new SqlParameter[] { treeCalendarDetail.TreeCalendarMasterIDParam, 
                        treeCalendarDetail.TreeMasterIDParam, 
                        treeCalendarDetail.CalendarMasterIDParam, 
                        treeCalendarDetail.CalendarUnitDistributionIDParam,
                         treeCalendarDetail.CreatorIDParam,
                         treeCalendarDetail.CreatedDateParam,
                         treeCalendarDetail.ModifierIDParam,
                         treeCalendarDetail.ModifiedDateParam,  
                        treeCalendarDetail.IDParam };
            }
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

            return counter;
        }

        public static int Delete(int treeCalendarDetailID)
        {
            int counter = 0;
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

            Cal_Course_Prog_Node.DeleteByTreeCalDet(treeCalendarDetailID, sqlConn, sqlTran);
            string command = DELETE
                            + "WHERE TreeCalendarDetailID = @TreeCalendarDetailID";

            SqlParameter treeCalDetailIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(treeCalendarDetailID, "@TreeCalendarDetailID");
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { treeCalDetailIDParam });

            MSSqlConnectionHandler.CommitTransaction();
            MSSqlConnectionHandler.CloseDbConnection();
            return counter;
        }
        internal static int Delete(int treeCalendarDetailID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            Cal_Course_Prog_Node.DeleteByTreeCalDet(treeCalendarDetailID, sqlConn, sqlTran);
            string command = DELETE
                            + "WHERE TreeCalendarDetailID = @TreeCalendarDetailID";

            SqlParameter treeCalDetailIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(treeCalendarDetailID, "@TreeCalendarDetailID");
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { treeCalDetailIDParam });

            return counter;
        }

        public static int DeleteByCalMaster(int calendarMasterID)
        {
            int counter = 0;
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

            string command = DELETE
                            + "WHERE CalendarMasterID = @CalendarMasterID";

            SqlParameter calendarMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(calendarMasterID, "@CalendarMasterID");
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { calendarMasterIDParam });

            MSSqlConnectionHandler.CommitTransaction();
            MSSqlConnectionHandler.CloseDbConnection();

            return counter;
        }
        internal static int DeleteByCalMaster(int calendarMasterID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = DELETE
                            + "WHERE CalendarMasterID = @CalendarMasterID";

            SqlParameter calendarMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(calendarMasterID, "@CalendarMasterID");
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { calendarMasterIDParam });

            return counter;
        }

        public static int DeleteByTreeMaster(int treeMasterID)
        {
            int counter = 0;
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

            string command = DELETE
                            + "WHERE TreeMasterID = @TreeMasterID";

            SqlParameter treeMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(treeMasterID, "@TreeMasterID");
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { treeMasterIDParam });

            MSSqlConnectionHandler.CommitTransaction();
            MSSqlConnectionHandler.CloseDbConnection();

            return counter;
        }
        internal static int DeleteByTreeMaster(int treeMasterID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = DELETE
                            + "WHERE TreeMasterID = @TreeMasterID";

            SqlParameter treeMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(treeMasterID, "@TreeMasterID");
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { treeMasterIDParam });

            return counter;
        }

        public static int DeleteByTreeCalMaster(int treeCalMasterID)
        {
            int counter = 0;
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

            Cal_Course_Prog_Node.DeleteByTreeCalMas(treeCalMasterID, sqlConn, sqlTran);
            string command = DELETE
                            + "WHERE TreeCalendarMasterID = @TreeCalendarMasterID";

            SqlParameter treeCalMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(treeCalMasterID, "@TreeCalendarMasterID");
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { treeCalMasterIDParam });

            MSSqlConnectionHandler.CommitTransaction();
            MSSqlConnectionHandler.CloseDbConnection();

            return counter;
        }
        internal static int DeleteByTreeCalMaster(int treeCalMasterID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            Cal_Course_Prog_Node.DeleteByTreeCalMas(treeCalMasterID, sqlConn, sqlTran);
            string command = DELETE
                            + "WHERE TreeCalendarMasterID = @TreeCalendarMasterID";

            SqlParameter treeCalMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(treeCalMasterID, "@TreeCalendarMasterID");
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { treeCalMasterIDParam });

            return counter;
        }
        #endregion
    }
}
