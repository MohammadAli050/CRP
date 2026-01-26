using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;


namespace BussinessObject
{
    [Serializable]
    public class TreeCalendarMaster : Base
    {
        #region Variables
        private int _treeMasterID;
        private TreeMaster _treeMaster;
        private int _calendarMasterID;
        private CalendarUnitMaster _calendarMaster;
        private List<TreeCalendarDetail> _treeCalendarDetails;
        private string _name = string.Empty;
        #endregion

        #region Constants
        private const string SELECT = "SELECT "
                            + "[TreeCalendarMasterID], "
                            + "[TreeMasterID], "
                            + "[CalendarMasterID], "
                            + "[Name], "
                            + BASECOLUMNS
                            + "FROM [TreeCalendarMaster] ";

        private const string INSERT = "INSERT INTO [TreeCalendarMaster] ("
                            + "[TreeCalendarMasterID], "
                            + "[TreeMasterID], "
                            + "[CalendarMasterID],"
                            + "[Name], "
                            + BASECOLUMNS
                            + ") "
                            + "VALUES ( "
                            + ID_PA + ", "
                            + "@TreeMasterID, "
                            + "@CalendarMasterID, "
                            + "@Name, "
                            + CREATORID_PA + ", "
                            + CREATEDDATE_PA + ", "
                            + MODIFIERID_PA + ", "
                            + MOIDFIEDDATE_PA
                            + ")";

        private const string UPDATE = "UPDATE TreeCalendarMaster "
                            + "SET TreeMasterID = @TreeMasterID, "
                            + "CalendarMasterID = @CalendarMasterID"
                            + "Name = @Name, "
                            + CREATORID + " = " + CREATORID_PA + ", "
                            + CREATEDDATE + " = " + CREATEDDATE_PA + ", "
                            + MODIFIERID + " = " + MODIFIERID_PA + ", "
                            + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM [TreeCalendarMaster] ";
        #endregion

        #region Constructor
        public TreeCalendarMaster():base()
        {
            _treeMasterID = 0;
            _treeMaster = null;
            _calendarMasterID = 0;
            _calendarMaster = null;
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
                nameParam.ParameterName = "@Name";

                nameParam.Value = _name;

                return nameParam;
            }
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

        public List<TreeCalendarDetail> TreeCalendarDetails
        {
            get
            {
                if (_treeCalendarDetails == null)
                {
                    if (this.Id > 0)
                    {
                        _treeCalendarDetails = TreeCalendarDetail.GetByTreeCalMaster(this.Id);
                    }
                }
                return _treeCalendarDetails;
            }
            set { _treeCalendarDetails = value; }
        } 
        #endregion

        #region Functions
        private static TreeCalendarMaster treeCalDetailMapper(SQLNullHandler nullHandler)
        {
            TreeCalendarMaster treeDetail = new TreeCalendarMaster();

            treeDetail.Id = nullHandler.GetInt32("TreeCalendarMasterID");
            treeDetail.TreeMasterID = nullHandler.GetInt32("TreeMasterID");
            treeDetail.CalendarMasterID = nullHandler.GetInt32("CalendarMasterID");
            treeDetail.Name = nullHandler.GetString("Name");
            treeDetail.CreatorID = nullHandler.GetInt32("CreatedBy");
            treeDetail.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            treeDetail.ModifierID = nullHandler.GetInt32("ModifiedBy");
            treeDetail.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");
            return treeDetail;
        }
        private static List<TreeCalendarMaster> mapTreeCalDetails(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<TreeCalendarMaster> treeCalDetails = null;

            while (theReader.Read())
            {
                if (treeCalDetails == null)
                {
                    treeCalDetails = new List<TreeCalendarMaster>();
                }
                TreeCalendarMaster treeDetail = treeCalDetailMapper(nullHandler);
                treeCalDetails.Add(treeDetail);
            }

            return treeCalDetails;
        }
        private static TreeCalendarMaster mapTreeCalDetail(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            TreeCalendarMaster treeCalDetail = null;
            if (theReader.Read())
            {
                treeCalDetail = new TreeCalendarMaster();
                treeCalDetail = treeCalDetailMapper(nullHandler);
            }

            return treeCalDetail;
        }

        internal static int GetMaxTreeCalDetailID(SqlConnection sqlConn)
        {
            int newTreeCalDetailID = 0;

            string command = "SELECT MAX(TreeCalendarMasterID) FROM [TreeCalendarMaster]";
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

            string command = "SELECT MAX(TreeCalendarMasterID) FROM [TreeCalendarMaster]";
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

        public static List<TreeCalendarMaster> Get()
        {
            List<TreeCalendarMaster> treeCalendarDetails = null;

            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            treeCalendarDetails = mapTreeCalDetails(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return treeCalendarDetails;
        }

        public static TreeCalendarMaster Get(int treeCalendarMasterID)
        {
            TreeCalendarMaster treeCalendarDetail = null;

            string command = SELECT
                            + "WHERE TreeCalendarMasterID = @TreeCalendarMasterID";

            SqlParameter treeCalDetailIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(treeCalendarMasterID, "@TreeCalendarMasterID");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { treeCalDetailIDParam });

            treeCalendarDetail = mapTreeCalDetail(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return treeCalendarDetail;
        }
        internal static TreeCalendarMaster Get(int treeCalendarMasterID, SqlConnection sqlConn)
        {
            TreeCalendarMaster treeCalendarDetail = null;

            string command = SELECT
                            + "WHERE TreeCalendarMasterID = @TreeCalendarMasterID";

            SqlParameter treeCalendarDetailIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(treeCalendarMasterID, "@TreeCalendarMasterID");
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { treeCalendarDetailIDParam });

            treeCalendarDetail = mapTreeCalDetail(theReader);
            theReader.Close();
            //SqlConnectionHandler.CloseDbConnection();
            return treeCalendarDetail;
        }

        public static List<TreeCalendarMaster> GetByTreeMaster(int tranMasterID)
        {
            List<TreeCalendarMaster> treeDetails = null;

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
        
        internal static List<TreeCalendarMaster> GetByTreeMaster(int tranMasterID, SqlConnection sqlConn)
        {
            List<TreeCalendarMaster> treeDetails = null;

            string command = SELECT
                            + "WHERE TreeMasterID = @TreeMasterID";

            SqlParameter treeMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(tranMasterID, "@TreeMasterID");

            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { treeMasterIDParam });

            treeDetails = mapTreeCalDetails(theReader);
            theReader.Close();

            return treeDetails;
        }
        internal static TreeCalendarMaster GetByTreeMasterID(int treeMasterID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            TreeCalendarMaster treeDetail = null;

            string command = SELECT
                            + "WHERE TreeMasterID = @TreeMasterID";

            SqlParameter treeMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(treeMasterID, "@TreeMasterID");

            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteBatchQuerry(command, sqlConn, sqlTran, new SqlParameter[] { treeMasterIDParam });

            treeDetail = mapTreeCalDetail(theReader);
            theReader.Close();

            return treeDetail;
        }

        public static List<TreeCalendarMaster> GetByCalMaster(int calMasterID)
        {
            List<TreeCalendarMaster> treeDetails = null;

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
        internal static List<TreeCalendarMaster> GetByCalMaster(int calMasterID, SqlConnection sqlConn)
        {
            List<TreeCalendarMaster> treeDetails = null;

            string command = SELECT
                            + "WHERE CalendarMasterID = @CalendarMasterID";

            SqlParameter calMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(calMasterID, "@CalendarMasterID");

            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { calMasterIDParam });

            treeDetails = mapTreeCalDetails(theReader);
            theReader.Close();

            return treeDetails;
        }

        public static int Save(TreeCalendarMaster treeCalendarMaster)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (treeCalendarMaster.Id == 0)
                {
                    treeCalendarMaster.Id = TreeCalendarMaster.GetMaxTreeCalDetailID(sqlConn, sqlTran);
                    command = INSERT;
                    sqlParams = new SqlParameter[] { treeCalendarMaster.IDParam, 
                                                     treeCalendarMaster.TreeMasterIDParam, 
                                                     treeCalendarMaster.CalendarMasterIDParam, 
                                                     treeCalendarMaster.NameParam,
                                                     treeCalendarMaster.CreatorIDParam,
                                                     treeCalendarMaster.CreatedDateParam,
                                                     treeCalendarMaster.ModifierIDParam,
                                                     treeCalendarMaster.ModifiedDateParam };
                }
                else
                {
                    command = UPDATE
                            + " WHERE TreeCalendarMasterID = " + ID_PA;
                    sqlParams = new SqlParameter[] { treeCalendarMaster.TreeMasterIDParam, 
                                                     treeCalendarMaster.CalendarMasterIDParam, 
                                                     treeCalendarMaster.NameParam,
                                                     treeCalendarMaster.CreatorIDParam,
                                                     treeCalendarMaster.CreatedDateParam,
                                                     treeCalendarMaster.ModifierIDParam,
                                                     treeCalendarMaster.ModifiedDateParam, 
                                                     treeCalendarMaster.IDParam };
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                TreeCalendarDetail.DeleteByTreeCalMaster(treeCalendarMaster.Id, sqlConn, sqlTran);
                if (treeCalendarMaster.TreeCalendarDetails != null)
                {
                    foreach (TreeCalendarDetail treeCalDetail in treeCalendarMaster.TreeCalendarDetails)
                    {
                        treeCalDetail.TreeCalendarMasterID = treeCalendarMaster.Id;
                        TreeCalendarDetail.Save(treeCalDetail, sqlConn, sqlTran);
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
        internal static int Save(TreeCalendarMaster treeCalendarMaster, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = string.Empty;

            SqlParameter[] sqlParams = null;

            if (treeCalendarMaster.Id == 0)
            {
                treeCalendarMaster.Id = TreeCalendarMaster.GetMaxTreeCalDetailID(sqlConn, sqlTran);
                command = INSERT;
                sqlParams = new SqlParameter[] { treeCalendarMaster.IDParam, 
                                                     treeCalendarMaster.TreeMasterIDParam, 
                                                     treeCalendarMaster.CalendarMasterIDParam, 
                                                     treeCalendarMaster.NameParam,
                                                     treeCalendarMaster.CreatorIDParam,
                                                     treeCalendarMaster.CreatedDateParam,
                                                     treeCalendarMaster.ModifierIDParam,
                                                     treeCalendarMaster.ModifiedDateParam };
            }
            else
            {
                command = UPDATE
                        + " WHERE TreeCalendarMasterID = " + ID_PA;
                sqlParams = new SqlParameter[] { treeCalendarMaster.TreeMasterIDParam, 
                                                     treeCalendarMaster.CalendarMasterIDParam, 
                                                     treeCalendarMaster.NameParam,
                                                     treeCalendarMaster.CreatorIDParam,
                                                     treeCalendarMaster.CreatedDateParam,
                                                     treeCalendarMaster.ModifierIDParam,
                                                     treeCalendarMaster.ModifiedDateParam, 
                                                     treeCalendarMaster.IDParam };
            }
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

            TreeCalendarDetail.DeleteByTreeCalMaster(treeCalendarMaster.Id, sqlConn, sqlTran);
            if (treeCalendarMaster.TreeCalendarDetails != null)
            {
                foreach (TreeCalendarDetail treeCalDetail in treeCalendarMaster.TreeCalendarDetails)
                {
                    treeCalDetail.TreeCalendarMasterID = treeCalendarMaster.Id;
                    TreeCalendarDetail.Save(treeCalDetail, sqlConn, sqlTran);
                }
            }

            return counter;
        }

        public static int Delete(int treeCalendarMasterID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                TreeCalendarDetail.DeleteByTreeCalMaster(treeCalendarMasterID, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE TreeCalendarMasterID = @TreeCalendarMasterID";

                SqlParameter treeCalDetailIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(treeCalendarMasterID, "@TreeCalendarMasterID");
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { treeCalDetailIDParam });

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
        public static int DeleteByCalMaster(int calendarMasterID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                TreeCalendarDetail.DeleteByCalMaster(calendarMasterID, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE CalendarMasterID = @CalendarMasterID";

                SqlParameter calendarMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(calendarMasterID, "@CalendarMasterID");
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { calendarMasterIDParam });

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
        public static int DeleteByTreeMaster(int treeMasterID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                TreeCalendarDetail.DeleteByTreeMaster(treeMasterID, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE TreeMasterID = @TreeMasterID";

                SqlParameter treeMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(treeMasterID, "@TreeMasterID");
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { treeMasterIDParam });

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
        #endregion
    }
}
