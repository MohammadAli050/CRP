using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class CalendarUnitMaster : Base
    {
        #region Variables
        private string _name;
        private List<CalenderUnitDistribution> _calendarDetails; 
        #endregion

        #region Constants
        private const string CALENDER_UNIT_MASTER_ID = "CalenderUnitMasterID";

        private const string NAME = "Name";
        private const string NAME_PA = "@Name";

        private const string ALLCOLUMNS = CALENDER_UNIT_MASTER_ID + ", "
                                        + NAME + ", ";

        private const string NOPKCOLUMNS = NAME + ", ";

        private const string TABLENAME = " [CalenderUnitMaster] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        private const string INSERT = "INSERT INTO" + TABLENAME
                            + "("
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + ") "
                            + "VALUES "
                            + "("
                            + ID_PA + ", "
                            + NAME_PA + ", "
                            + CREATORID_PA + ", "
                            + CREATEDDATE_PA + ", "
                            + MODIFIERID_PA + ", "
                            + MOIDFIEDDATE_PA
                            + ")";


        private const string UPDATE = "UPDATE" + TABLENAME
                            + "SET " + NAME + " = " + NAME_PA + ", "
                            + CREATORID + " = " + CREATORID_PA + ", "//7
                            + CREATEDDATE + " = " + CREATEDDATE_PA + ", "//8
                            + MODIFIERID + " = " + MODIFIERID_PA + ", "//9
                            + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Constaructor
        public CalendarUnitMaster():base()
        {
            _name = string.Empty;
            _calendarDetails = null;
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

        public List<CalenderUnitDistribution> CalendarDetails
        {
            get
            {
                if (_calendarDetails == null)
                {
                    if (this.Id > 0)
                    {
                        _calendarDetails = CalenderUnitDistribution.GetCalendarDetailByMasterID(this.Id);
                    }
                }
                return _calendarDetails;
            }
            set { _calendarDetails = value; }
        } 
        #endregion

        #region Fuctions
        private static CalendarUnitMaster clendarMasterMapper(SQLNullHandler nullHandler)
        {
            CalendarUnitMaster calendarMaster = new CalendarUnitMaster();

            calendarMaster.Id = nullHandler.GetInt32(CALENDER_UNIT_MASTER_ID);
            calendarMaster.Name = nullHandler.GetString(NAME);
            calendarMaster.CreatorID = nullHandler.GetInt32(CREATORID);
            calendarMaster.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            calendarMaster.ModifierID = nullHandler.GetInt32(MODIFIERID);
            calendarMaster.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);
            return calendarMaster;
        }
        private static List<CalendarUnitMaster> mapCalendarMasters(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<CalendarUnitMaster> calendarMasters = null;

            while (theReader.Read())
            {
                if (calendarMasters == null)
                {
                    calendarMasters = new List<CalendarUnitMaster>();
                }
                CalendarUnitMaster calendarMaster = clendarMasterMapper(nullHandler);
                calendarMasters.Add(calendarMaster);
            }

            return calendarMasters;
        }
        private static CalendarUnitMaster mapCalendarMaster(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            CalendarUnitMaster calendarMaster = null;
            if (theReader.Read())
            {
                calendarMaster = new CalendarUnitMaster();
                calendarMaster = clendarMasterMapper(nullHandler);
            }

            return calendarMaster;
        }

        internal static int GetMaxCalendarMasterID(SqlConnection sqlConn)
        {
            int newCalendarMasterID = 0;

            string command = "SELECT MAX(" + CALENDER_UNIT_MASTER_ID + ") FROM" + TABLENAME;
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

            if (ob == null || ob == DBNull.Value)
            {
                newCalendarMasterID = 1;
            }
            else if (ob is Int32)
            {
                newCalendarMasterID = Convert.ToInt32(ob) + 1;
            }

            return newCalendarMasterID;
        }
        internal static int GetMaxCalendarMasterID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newCalendarMasterID = 0;

            string command = "SELECT MAX(" + CALENDER_UNIT_MASTER_ID + ") FROM" + TABLENAME;
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

            if (ob == null || ob == DBNull.Value)
            {
                newCalendarMasterID = 1;
            }
            else if (ob is Int32)
            {
                newCalendarMasterID = Convert.ToInt32(ob) + 1;
            }

            return newCalendarMasterID;
        }

        public static bool HasDuplicateName(CalendarUnitMaster calendarUnitMaster, string oldname)
        {
            if (calendarUnitMaster == null)
            {
                return CalendarUnitMaster.IsExist(calendarUnitMaster.Name);
            }
            else
            {
                if (calendarUnitMaster.Id == 0)
                {
                    return CalendarUnitMaster.IsExist(calendarUnitMaster.Name);
                }
                else
                {
                    if (calendarUnitMaster.Name != oldname)
                    {
                        return CalendarUnitMaster.IsExist(calendarUnitMaster.Name);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static List<CalendarUnitMaster> GetCalendarMasters(string parameters)
        {
            List<CalendarUnitMaster> calendarMasters = null;

            string command = SELECT
                            + "WHERE [Name] Like '%" + parameters + "%'";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            calendarMasters = mapCalendarMasters(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return calendarMasters;
        }
        public static List<CalendarUnitMaster> GetCalendarMastersByName(string name)
        {
            List<CalendarUnitMaster> calendarMasters = null;

            string command = SELECT
                            + "WHERE Name = @Name";

            SqlParameter nameParam = MSSqlConnectionHandler.MSSqlParamGenerator(name, "@Name");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { nameParam });

            calendarMasters = mapCalendarMasters(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return calendarMasters;
        }
        public static List<CalendarUnitMaster> GetCalendarMasters()
        {
            List<CalendarUnitMaster> calendarMasters = null;

            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            calendarMasters = mapCalendarMasters(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return calendarMasters;
        }

        public static CalendarUnitMaster GetCalendarMaster(int calendarMasterID)
        {
            CalendarUnitMaster calendarMaster = null;

            string command = SELECT
                            + "WHERE " + CALENDER_UNIT_MASTER_ID + " = @CalendarMasterID";

            SqlParameter calendarMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(calendarMasterID, "@CalendarMasterID");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { calendarMasterIDParam });

            calendarMaster = mapCalendarMaster(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return calendarMaster;
        }
        internal static CalendarUnitMaster GetCalendarMaster(int calendarMasterID, SqlConnection sqlConn)
        {
            CalendarUnitMaster calendarMaster = null;

            string command = SELECT
                            + "WHERE " + CALENDER_UNIT_MASTER_ID + " = @CalendarMasterID";

            SqlParameter calendarMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(calendarMasterID, "@CalendarMasterID");
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { calendarMasterIDParam });

            calendarMaster = mapCalendarMaster(theReader);
            theReader.Close();
            //SqlConnectionHandler.CloseDbConnection();
            return calendarMaster;
        }

        public static int Save(CalendarUnitMaster calendarMaster)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (calendarMaster.Id == 0)
                {
                    calendarMaster.Id = CalendarUnitMaster.GetMaxCalendarMasterID(sqlConn,sqlTran);
                    command = INSERT;
                    sqlParams = new SqlParameter[] { calendarMaster.IDParam,
                                                     calendarMaster.NameParam,
                                                     calendarMaster.CreatorIDParam,
                                                     calendarMaster.CreatedDateParam,
                                                     calendarMaster.ModifierIDParam,
                                                     calendarMaster.ModifiedDateParam};
                }
                else
                {
                    command = UPDATE
                            + " WHERE " + CALENDER_UNIT_MASTER_ID + " = " + ID_PA;
                    sqlParams = new SqlParameter[] { calendarMaster.NameParam, 
                                                     calendarMaster.CreatorIDParam,
                                                     calendarMaster.CreatedDateParam,
                                                     calendarMaster.ModifierIDParam,
                                                     calendarMaster.ModifiedDateParam,
                                                     calendarMaster.IDParam };
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                CalenderUnitDistribution.DeleteByMaster(calendarMaster.Id, sqlConn, sqlTran);
                if (calendarMaster.CalendarDetails != null)
                {
                    foreach (CalenderUnitDistribution calendarDetail in calendarMaster.CalendarDetails)
                    {
                        calendarDetail.Id = 0;
                        calendarDetail.CalendarMasterID = calendarMaster.Id;
                        if (calendarMaster.ModifiedDate != DateTime.MinValue)
                        {
                            calendarDetail.CreatorID = calendarMaster.ModifierID;
                            calendarDetail.CreatedDate = calendarMaster.ModifiedDate;
                        }
                        else
                        {
                            calendarDetail.CreatorID = calendarMaster.CreatorID;
                            calendarDetail.CreatedDate = calendarMaster.CreatedDate;
                        }
                        CalenderUnitDistribution.Save(calendarDetail, sqlConn, sqlTran);
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
        internal static int Save(CalendarUnitMaster calendarMaster, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = string.Empty;

            SqlParameter[] sqlParams = null;

            if (calendarMaster.Id == 0)
            {
                calendarMaster.Id = CalendarUnitMaster.GetMaxCalendarMasterID(sqlConn, sqlTran);
                command = INSERT;
                sqlParams = new SqlParameter[] { calendarMaster.IDParam,
                                                 calendarMaster.NameParam,
                                                 calendarMaster.CreatorIDParam,
                                                 calendarMaster.CreatedDateParam,
                                                 calendarMaster.ModifierIDParam,
                                                 calendarMaster.ModifiedDateParam};
            }
            else
            {
                command = UPDATE
                        + " WHERE " + CALENDER_UNIT_MASTER_ID + " = " + ID_PA;
                sqlParams = new SqlParameter[] { calendarMaster.NameParam, 
                                                 calendarMaster.CreatorIDParam,
                                                 calendarMaster.CreatedDateParam,
                                                 calendarMaster.ModifierIDParam,
                                                 calendarMaster.ModifiedDateParam,
                                                 calendarMaster.IDParam };
            }
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

            return counter;
        }

        public static int Delete(int calendarMasterID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                CalenderUnitDistribution.DeleteByMaster(calendarMasterID, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE " + CALENDER_UNIT_MASTER_ID + " = @CalendarMasterID";

                SqlParameter calendarMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(calendarMasterID, "@CalendarMasterID");
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { calendarMasterIDParam });

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                if (MSSqlConnectionHandler.IsATransactionActive())
                {
                    MSSqlConnectionHandler.RollBackAndClose(); 
                }
                throw ex;
            }
        }
        internal static int Delete(int calendarMasterID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = DELETE
                            + "WHERE " + CALENDER_UNIT_MASTER_ID + " = @CalendarMasterID";

            SqlParameter calendarMasterIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(calendarMasterID, "@CalendarMasterID");
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { calendarMasterIDParam });

            return counter;
        }

        public static bool IsExist(string name)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(" + CALENDER_UNIT_MASTER_ID + ") FROM" + TABLENAME
                            + "WHERE Name = @Name";
            SqlParameter nameParam = MSSqlConnectionHandler.MSSqlParamGenerator(name, "@Name");
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { nameParam });

            MSSqlConnectionHandler.CloseDbConnection();

            return (Convert.ToInt32(ob) > 0);
        }
        #endregion
    }
}
