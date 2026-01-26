using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class CalenderUnitDistribution :Base
    {
        #region Variables
        private int _calendarMasterID;
        private string _name;
        private CalendarUnitMaster _calendarMaster; 
        #endregion

        #region Constants
        private const string SELECT = "SELECT "
                            + "[CalenderUnitDistributionID], "
                            + "[CalenderUnitMasterID], "
                            + "[Name], "
                            + BASECOLUMNS
                            + "FROM [CalenderUnitDistribution] ";

        private const string INSERT = "INSERT INTO [CalenderUnitDistribution] "
                            + "([CalenderUnitDistributionID], "
                            + "[CalenderUnitMasterID], "
                            + "[Name], "
                            + BASECOLUMNS
                            + ") "
                            + "VALUES "
                            + "("
                            + ID_PA + ", "
                            + "@CalendarMasterID, "
                            + "@Name, "
                            + CREATORID_PA + ", "
                            + CREATEDDATE_PA + ", "
                            + MODIFIERID_PA + ", "
                            + MOIDFIEDDATE_PA
                            + ")";


        private const string UPDATE = "UPDATE CalenderUnitDistribution "
                            + "SET CalenderUnitMasterID = @CalendarMasterID, "
                            + "Name = @Name, "
                            + CREATORID + " = " + CREATORID_PA + ", "
                            + CREATEDDATE + " = " + CREATEDDATE_PA + ", "
                            + MODIFIERID + " = " + MODIFIERID_PA + ", "
                            + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM [CalenderUnitDistribution] ";
        #endregion

        #region Constructor
        public CalenderUnitDistribution():base()
        {
            _calendarMasterID = 0;
            _name = string.Empty;
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
        #endregion

        #region Fuctions
        private static CalenderUnitDistribution clendarDetailMapper(SQLNullHandler nullHandler)
        {
            CalenderUnitDistribution calendarDetail = new CalenderUnitDistribution();

            calendarDetail.Id = nullHandler.GetInt32("CalenderUnitDistributionID");
            calendarDetail.CalendarMasterID = nullHandler.GetInt32("CalenderUnitMasterID");
            calendarDetail.Name = nullHandler.GetString("Name");
            calendarDetail.CreatorID = nullHandler.GetInt32("CreatedBy");
            calendarDetail.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            calendarDetail.ModifierID = nullHandler.GetInt32("ModifiedBy");
            calendarDetail.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");
            return calendarDetail;
        }
        private static List<CalenderUnitDistribution> mapCalendarDetails(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<CalenderUnitDistribution> calendarDetails = null;

            while (theReader.Read())
            {
                if (calendarDetails == null)
                {
                    calendarDetails = new List<CalenderUnitDistribution>();
                }
                CalenderUnitDistribution calendarDetail = clendarDetailMapper(nullHandler);
                calendarDetails.Add(calendarDetail);
            }

            return calendarDetails;
        }
        private static CalenderUnitDistribution mapCalendarDetail(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            CalenderUnitDistribution calendarDetail = null;
            if (theReader.Read())
            {
                calendarDetail = new CalenderUnitDistribution();
                calendarDetail = clendarDetailMapper(nullHandler);
            }

            return calendarDetail;
        }

        internal static int GetMaxCalendarDetailID(SqlConnection sqlConn)
        {
            int newCalendarDetailID = 0;

            string command = "SELECT MAX(CalenderUnitDistributionID) FROM [CalenderUnitDistribution]";
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

            if (ob == null || ob == DBNull.Value)
            {
                newCalendarDetailID = 1;
            }
            else if (ob is Int32)
            {
                newCalendarDetailID = Convert.ToInt32(ob) + 1;
            }

            return newCalendarDetailID;
        }
        internal static int GetMaxCalendarDetailID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newCalendarDetailID = 0;

            string command = "SELECT MAX(CalenderUnitDistributionID) FROM [CalenderUnitDistribution]";
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

            if (ob == null || ob == DBNull.Value)
            {
                newCalendarDetailID = 1;
            }
            else if (ob is Int32)
            {
                newCalendarDetailID = Convert.ToInt32(ob) + 1;
            }

            return newCalendarDetailID;
        }

        public static List<CalenderUnitDistribution> GetCalendarDetails(string parameters)
        {
            List<CalenderUnitDistribution> calendarDetails = null;

            string command = SELECT
                            + "WHERE [Name] Like '%" + parameters + "%'";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            calendarDetails = mapCalendarDetails(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return calendarDetails;
        }
        public static List<CalenderUnitDistribution> GetCalendarDetailsByName(string name)
        {
            List<CalenderUnitDistribution> calendarDetails = null;

            string command = SELECT
                            + "WHERE Name = @Name";

            SqlParameter nameParam = MSSqlConnectionHandler.MSSqlParamGenerator(name, "@Name");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { nameParam });

            calendarDetails = mapCalendarDetails(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return calendarDetails;
        }
        public static List<CalenderUnitDistribution> GetCalendarDetails()
        {
            List<CalenderUnitDistribution> calendarDetails = null;

            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            calendarDetails = mapCalendarDetails(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return calendarDetails;
        }

        public static List<CalenderUnitDistribution> GetCalendarDetailByMasterID(int calendarMasterID)
        {
            List<CalenderUnitDistribution> calendarDetails = null;

            string command = SELECT
                            + "WHERE CalenderUnitMasterID = @CalendarMasterID";

            SqlParameter calendarMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(calendarMasterID, "@CalendarMasterID");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { calendarMasterIDParam });

            calendarDetails = mapCalendarDetails(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return calendarDetails;
        }
        internal static List<CalenderUnitDistribution> GetCalendarDetailByMasterID(int calendarMasterID, SqlConnection sqlConn)
        {
            List<CalenderUnitDistribution> calendarDetails = null;

            string command = SELECT
                            + "WHERE CalenderUnitMasterID = @CalendarMasterID";

            SqlParameter calendarMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(calendarMasterID, "@CalendarMasterID");
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { calendarMasterIDParam });

            calendarDetails = mapCalendarDetails(theReader);
            theReader.Close();
            //SqlConnectionHandler.CloseDbConnection();
            return calendarDetails;
        }

        public static CalenderUnitDistribution GetCalendarDetail(int calendarDetailID)
        {
            CalenderUnitDistribution calendarDetail = null;

            string command = SELECT
                            + "WHERE CalenderUnitDistributionID = @CalendarDetailID";

            SqlParameter calendarDetailIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(calendarDetailID, "@CalendarDetailID");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { calendarDetailIDParam });

            calendarDetail = mapCalendarDetail(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return calendarDetail;
        }
        internal static CalenderUnitDistribution GetCalendarDetail(int calendarDetailID, SqlConnection sqlConn)
        {
            CalenderUnitDistribution calendarDetail = null;

            string command = SELECT
                            + "WHERE CalenderUnitDistributionID = @CalendarDetailID";

            SqlParameter calendarDetailIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(calendarDetailID, "@CalendarDetailID");
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { calendarDetailIDParam });

            calendarDetail = mapCalendarDetail(theReader);
            theReader.Close();
            //SqlConnectionHandler.CloseDbConnection();
            return calendarDetail;
        }

        public static int Save(CalenderUnitDistribution calendarDetail)
        {
            int counter = 0;
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = string.Empty;
            SqlParameter[] sqlParams = null;

            if (calendarDetail.Id == 0)
            {
                calendarDetail.Id = CalenderUnitDistribution.GetMaxCalendarDetailID(sqlConn);
                command = INSERT;
                sqlParams = new SqlParameter[] { calendarDetail.IDParam,
                                                 calendarDetail.CalendarMasterIDParam, 
                                                 calendarDetail.NameParam, 
                                                 calendarDetail.CreatorIDParam,
                                                 calendarDetail.CreatedDateParam,
                                                 calendarDetail.ModifierIDParam,
                                                 calendarDetail.ModifiedDateParam};
            }
            else
            {
                command = UPDATE
                        + " WHERE CalenderUnitDistributionID = " + ID_PA;
                sqlParams = new SqlParameter[] { calendarDetail.CalendarMasterIDParam, 
                                                 calendarDetail.NameParam,
                                                 calendarDetail.CreatorIDParam,
                                                 calendarDetail.CreatedDateParam,
                                                 calendarDetail.ModifierIDParam,
                                                 calendarDetail.ModifiedDateParam, 
                                                 calendarDetail.IDParam };
            }
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteAction(command, sqlConn, sqlParams);

            MSSqlConnectionHandler.CloseDbConnection();
            return counter;
        }
        internal static int Save(CalenderUnitDistribution calendarDetail, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = string.Empty;

            SqlParameter[] sqlParams = null;

            if (calendarDetail.Id == 0)
            {
                calendarDetail.Id = CalenderUnitDistribution.GetMaxCalendarDetailID(sqlConn, sqlTran);
                command = INSERT;
                sqlParams = new SqlParameter[] { calendarDetail.IDParam,
                                                 calendarDetail.CalendarMasterIDParam, 
                                                 calendarDetail.NameParam, 
                                                 calendarDetail.CreatorIDParam,
                                                 calendarDetail.CreatedDateParam,
                                                 calendarDetail.ModifierIDParam,
                                                 calendarDetail.ModifiedDateParam};
            }
            else
            {
                command = UPDATE
                        + " WHERE CalenderUnitDistributionID = " + ID_PA;
                sqlParams = new SqlParameter[] { calendarDetail.CalendarMasterIDParam, 
                                                 calendarDetail.NameParam,
                                                 calendarDetail.CreatorIDParam,
                                                 calendarDetail.CreatedDateParam,
                                                 calendarDetail.ModifierIDParam,
                                                 calendarDetail.ModifiedDateParam, 
                                                 calendarDetail.IDParam };
            }
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

            return counter;
        }

        public static int Delete(int calendarDetailID)
        {
            int counter = 0;
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

            string command = DELETE
                            + "WHERE CalenderUnitDistributionID = @CalendarDetailID";
            SqlParameter calendarDetailIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(calendarDetailID, "@CalendarDetailID");
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { calendarDetailIDParam });

            MSSqlConnectionHandler.CommitTransaction();
            MSSqlConnectionHandler.CloseDbConnection();
            return counter;
        }
        internal static int Delete(int calendarDetailID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = DELETE
                            + "WHERE CalenderUnitDistributionID = @CalendarDetailID";
            SqlParameter calendarDetailIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(calendarDetailID, "@CalendarDetailID");
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { calendarDetailIDParam });

            return counter;
        }
        internal static int DeleteByMaster(int calendarMasterID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = DELETE
                            + "WHERE CalenderUnitMasterID = @CalendarMasterID";
            SqlParameter calendarDetailIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(calendarMasterID, "@CalendarMasterID");
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { calendarDetailIDParam });

            return counter;
        }
        #endregion
    }
}
