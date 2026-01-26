using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class CalenderUnitType : Base
    {
        #region DB Columns
        //CalenderUnitTypeID
        //CalenderUnitMasterID
        //TypeName 
	    #endregion

        #region Constants

        private const string CALENDER_UNIT_TYPE_ID = "[CalenderUnitTypeID]";

        private const string CALENDER_UNIT_MASTER_ID = "[CalenderUnitMasterID]";
        private const string CALENDER_UNIT_MASTER_ID_PA = "@CalenderUnitMasterID";

        private const string TYPE_NAME = "[TypeName]";
        private const string TYPE_NAME_PA = "@TypeName";

        private const string ALLCOLUMNS = CALENDER_UNIT_TYPE_ID + ", "
                                        + CALENDER_UNIT_MASTER_ID + ", "
                                        + TYPE_NAME + ", ";

        private const string NOPKCOLUMNS = CALENDER_UNIT_MASTER_ID + ", "
                                        + TYPE_NAME + ", ";

        private const string TABLENAME = " [CalenderUnitType] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                             + NOPKCOLUMNS
                             + BASECOLUMNS + ")"
                             + "VALUES ( "
                             + CALENDER_UNIT_MASTER_ID_PA + ", "
                             + TYPE_NAME_PA + ", "
                             + CREATORID_PA + ", "
                             + CREATEDDATE_PA + ", "
                             + MODIFIERID_PA + ", "
                             + MOIDFIEDDATE_PA + ")";

        private const string UPDATE = "UPDATE" + TABLENAME
                            + "SET " + CALENDER_UNIT_MASTER_ID + " = " + CALENDER_UNIT_MASTER_ID_PA + ", "
                            + TYPE_NAME + " = " + TYPE_NAME_PA + ","
                            + CREATORID + " = " + CREATORID_PA + ","
                            + CREATEDDATE + " = " + CREATEDDATE_PA + ","
                            + MODIFIERID + " = " + MODIFIERID_PA + ","
                            + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Constructor
        public CalenderUnitType()
            : base()
        {
            _calenderUnitMasterID = 0;
            _calendarMaster = null;
            _typeName = string.Empty;
        } 
        #endregion

        #region Variables
        private int _calenderUnitMasterID;
        private CalendarUnitMaster _calendarMaster;
        private string _typeName;
        private string _lastTypeName; 
        #endregion

        #region Properties
        public int CalenderUnitMasterID
        {
            get { return _calenderUnitMasterID; }
            set { _calenderUnitMasterID = value; }
        }
        private SqlParameter CalenderUnitMasterIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = CALENDER_UNIT_MASTER_ID_PA;

                sqlParam.Value = CalenderUnitMasterID;

                return sqlParam;
            }
        }
        public CalendarUnitMaster CalendarMaster
        {
            get
            {
                if (_calendarMaster == null)
                {
                    if (this.CalenderUnitMasterID > 0)
                    {
                        _calendarMaster = CalendarUnitMaster.GetCalendarMaster(this.CalenderUnitMasterID);
                    }
                }
                return _calendarMaster;
            }
            private set { _calendarMaster = value; }
        }

        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }
        private SqlParameter TypeNameParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = TYPE_NAME_PA;

                sqlParam.Value = TypeName;

                return sqlParam;
            }
        }

        public string LastTypeName
        {
            get { return _lastTypeName; }
            set { _lastTypeName = value; }
        }
        #endregion

        #region Methods
        private static CalenderUnitType CalUTMapper(SQLNullHandler nullHandler)
        {
            CalenderUnitType clUT = new CalenderUnitType();

            clUT.Id = nullHandler.GetInt32("CalenderUnitTypeID");
            clUT.CalenderUnitMasterID = nullHandler.GetInt32("CalenderUnitMasterID");
            clUT.TypeName = nullHandler.GetString("TypeName");
            clUT.CreatorID = nullHandler.GetInt32("CreatedBy");
            clUT.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            clUT.ModifierID = nullHandler.GetInt32("ModifiedBy");
            clUT.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            return clUT;
        }
        private static List<CalenderUnitType> MapCalUTs(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<CalenderUnitType> clUTs = null;

            while (theReader.Read())
            {
                if (clUTs == null)
                {
                    clUTs = new List<CalenderUnitType>();
                }
                CalenderUnitType program = CalUTMapper(nullHandler);
                clUTs.Add(program);
            }

            return clUTs;
        }
        private static CalenderUnitType MapCalUT(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            CalenderUnitType clUT = null;
            if (theReader.Read())
            {
                clUT = new CalenderUnitType();
                clUT = CalUTMapper(nullHandler);
            }

            return clUT;
        }

        public static List<CalenderUnitType> GetCalUTypes()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<CalenderUnitType> clUTs = MapCalUTs(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return clUTs;
        }
        public static List<CalenderUnitType> GetCalUTypes(string parameter)
        {
            string command = SELECT
                            + "WHERE " + TYPE_NAME + " Like '%" + parameter + "%'";
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<CalenderUnitType> clUTs = MapCalUTs(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return clUTs;
        }
        public static CalenderUnitType GetCalUType(int schoolID)
        {
            string command = SELECT
                            + "WHERE " + CALENDER_UNIT_TYPE_ID + " = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(schoolID, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            CalenderUnitType clUT = MapCalUT(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return clUT;
        }

        public static CalenderUnitType GetCalUType(string name)
        {
            string command = SELECT
                            + "WHERE "+TYPE_NAME+" = " + TYPE_NAME_PA;

            SqlParameter codeParam = MSSqlConnectionHandler.MSSqlParamGenerator(name, TYPE_NAME_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { codeParam });

            CalenderUnitType school = MapCalUT(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return school;
        }

        public static bool IsExist(string name)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(*) FROM" + TABLENAME
                            + "WHERE " + TYPE_NAME + " = " + TYPE_NAME_PA;
            SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(name, TYPE_NAME_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { sqlParam });

            MSSqlConnectionHandler.CloseDbConnection();

            return (Convert.ToInt32(ob) > 0);
        }

        internal static bool IsExist(string name, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            string command = "SELECT COUNT(*) FROM" + TABLENAME
                            + "WHERE " + TYPE_NAME + " = " + TYPE_NAME_PA;
            SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(name, TYPE_NAME_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran, new SqlParameter[] { sqlParam });

            return (Convert.ToInt32(ob) > 0);
        }

        public static bool HasDuplicateCode(CalenderUnitType clUT)
        {
            if (clUT == null)
            {
                return Department.IsExist(clUT.TypeName);
            }
            else
            {
                if (clUT.Id == 0)
                {
                    if (Department.IsExist(clUT.TypeName))
                    {
                        return Department.IsExist(clUT.TypeName);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (clUT.TypeName != clUT.LastTypeName)
                    {
                        return Department.IsExist(clUT.TypeName);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        internal static bool HasDuplicateCode(CalenderUnitType clUT, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            if (clUT == null)
            {
                return Department.IsExist(clUT.TypeName, sqlConn, sqlTran);
            }
            else
            {
                if (clUT.Id == 0)
                {
                    if (Department.IsExist(clUT.TypeName, sqlConn, sqlTran))
                    {
                        return Department.IsExist(clUT.TypeName, sqlConn, sqlTran);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (clUT.TypeName != clUT.LastTypeName)
                    {
                        return Department.IsExist(clUT.TypeName, sqlConn, sqlTran);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static int Save(CalenderUnitType clUT)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (HasDuplicateCode(clUT, sqlConn, sqlTran))
                {
                    throw new Exception("Duplicate Calender Unit Type Name Not Allowed.");
                }

                if (clUT.Id == 0)
                {
                    command = INSERT;
                    sqlParams = new SqlParameter[] { clUT.CalenderUnitMasterIDParam,  
                                                     clUT.TypeNameParam,
                                                     clUT.CreatorIDParam, 
                                                     clUT.CreatedDateParam, 
                                                     clUT.ModifierIDParam, 
                                                     clUT.ModifiedDateParam };
                }
                else
                {

                    command = UPDATE
                            + " WHERE " + CALENDER_UNIT_TYPE_ID + " = " + ID_PA;
                    sqlParams = new SqlParameter[] { clUT.CalenderUnitMasterIDParam,  
                                                     clUT.TypeNameParam, 
                                                     clUT.CreatorIDParam,
                                                     clUT.CreatedDateParam, 
                                                     clUT.ModifierIDParam, 
                                                     clUT.ModifiedDateParam, 
                                                     clUT.IDParam };
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
        public static int Delete(int clUTID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = DELETE
                                + "WHERE " + CALENDER_UNIT_TYPE_ID + " = " + ID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(clUTID, ID_PA);
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { iDParam });

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
