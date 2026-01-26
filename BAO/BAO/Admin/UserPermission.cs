using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class UserPermission : Base
    {

        #region Variables
        private int _userID;
        private string _accessIDPattern;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _personID;
        private Person _person = null;

        #endregion

        #region Constants

        private const string USERID_PA = "@User_ID";
        private const string USERID = "User_ID";

        private const string IDPATTERN = "AccessIDPattern";
        private const string IDPATTERN_PA = "@AccessIDPattern";

        private const string STARTDATE = "AccessStartDate";
        private const string STARTDATE_PA = "@AccessStartDate";

        private const string ENDDATE = "AccessEndDate";
        private const string ENDDATE_PA = "@AccessEndDate";

        private const string PERSONID = "PersonID";
        private const string PERSONID_PA = "@PersonID";

        private const string ALLCOLUMNS = "[UsrPermsn_ID], "
                                        + "[User_ID], "
                                        + "[AccessIDPattern], "
                                        + "[AccessStartDate], "
                                        + "[AccessEndDate], "
                                        ;

        private const string NOPKCOLUMNS = "[User_ID], "
                                        + "[AccessIDPattern], "
                                        + "[AccessStartDate], "
                                        + "[AccessEndDate], "
                                        ;

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM [UsrPermsn] ";

        private const string INSERT = "INSERT INTO [UsrPermsn] ("
                            + ALLCOLUMNS
                            + BASEMUSTCOLUMNS + ") "
                            + "VALUES ( "
                            + ID_PA + ","
                            + USERID_PA + ","
                            + IDPATTERN_PA + ","
                            + STARTDATE_PA + ","
                            + ENDDATE_PA + ","
                           // + PERSONID_PA + ","
                            + CREATORID_PA + ", "
                            + CREATEDDATE_PA + ")";

        private const string UPDATE = "UPDATE UsrPermsn "
                            + "SET User_ID = " + USERID_PA + ", "
                            + "AccessIDPattern = " + IDPATTERN_PA + ", "
                            + "AccessStartDate = " + STARTDATE_PA + ", "
                            + "AccessEndDate = " + ENDDATE_PA + ", "
                           // + "PersonID = " + PERSONID_PA + ", "
                            + "CreatedBy = " + CREATORID_PA + ", "
                            + "CreatedDate = " + CREATEDDATE_PA + ", "
                            + "ModifiedBy = " + MODIFIERID_PA + ", "
                            + "ModifiedDate = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM [UsrPermsn] ";
        #endregion

        #region Properties
        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }
        private SqlParameter UserIDParam
        {
            get
            {
                SqlParameter userIDParam = new SqlParameter();
                userIDParam.ParameterName = USERID_PA;

                userIDParam.Value = UserID;

                return userIDParam;
            }
        }

        public string AccessIDPattern
        {
            get { return _accessIDPattern; }
            set { _accessIDPattern = value; }
        }
        private SqlParameter AccessIDPatternParam
        {
            get
            {
                SqlParameter accessIDPatternParam = new SqlParameter();
                accessIDPatternParam.ParameterName = IDPATTERN_PA;

                accessIDPatternParam.Value = AccessIDPattern;

                return accessIDPatternParam;
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        private SqlParameter StartDateParam
        {
            get
            {
                SqlParameter startDateParam = new SqlParameter();
                startDateParam.ParameterName = STARTDATE_PA;

                startDateParam.Value = StartDate;

                return startDateParam;
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        private SqlParameter EndDateParam
        {
            get
            {
                SqlParameter endDateParam = new SqlParameter();
                endDateParam.ParameterName = ENDDATE_PA;

                endDateParam.Value = EndDate;

                return endDateParam;
            }
        }

        public int PersonID
        {
            get { return _personID; }
            set { _personID = value; }
        }
        private SqlParameter PersonIDParam
        {
            get
            {
                SqlParameter personIDParam = new SqlParameter();
                personIDParam.ParameterName = PERSONID_PA;
                personIDParam.Value = PersonID;

                return personIDParam;
            }
        }

        public Person Person
        {
            get
            {
                if (_person == null)
                {
                    if (_personID > 0)
                    {
                        _person = BussinessObject.Person.GetPersonById(_personID);
                    }
                }
                return _person;                
            }
        }

        #endregion

        #region Constructor
        public UserPermission()
            : base()
        {
            _userID = 0;
            _accessIDPattern = string.Empty;
            _startDate = DateTime.MinValue;
            _endDate = DateTime.MinValue;
            _personID = 0;
        }
        #endregion

        #region Methods
        private static UserPermission Mapper(SQLNullHandler nullHandler)
        {
            UserPermission permission = new UserPermission();

            permission.Id = nullHandler.GetInt32("UsrPermsn_ID");
            permission.UserID = nullHandler.GetInt32(USERID);
            permission.AccessIDPattern = nullHandler.GetString(IDPATTERN);
            permission.StartDate = nullHandler.GetDateTime(STARTDATE);
            permission.EndDate = nullHandler.GetDateTime(ENDDATE);
           // permission.PersonID = nullHandler.GetInt32(PERSONID);
            permission.CreatorID = nullHandler.GetInt32("CreatedBy");
            permission.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            permission.ModifierID = nullHandler.GetInt32("ModifiedBy");
            permission.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");
            return permission;
        }
        private static UserPermission MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            UserPermission permission = null;
            if (theReader.Read())
            {
                permission = new UserPermission();
                permission = Mapper(nullHandler);
            }

            return permission;
        }
        private static List<UserPermission> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<UserPermission> permissions = null;

            while (theReader.Read())
            {
                if (permissions == null)
                {
                    permissions = new List<UserPermission>();
                }
                UserPermission permission = Mapper(nullHandler);
                permissions.Add(permission);
            }

            return permissions;
        }

        internal static int GetMaxUserPermitID(SqlConnection sqlConn)
        {
            int newCalendarMasterID = 0;

            string command = "SELECT MAX(UsrPermsn_ID) FROM [UsrPermsn]";
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
        internal static int GetMaxUserPermitID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newCalendarMasterID = 0;

            string command = "SELECT MAX(UsrPermsn_ID) FROM [UsrPermsn]";
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

        public static List<UserPermission> Get()
        {
            List<UserPermission> permissions = null;

            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            permissions = MapCollection(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return permissions;
        }
        public static UserPermission Get(int permissionID)
        {
            UserPermission permission = null;

            string command = SELECT
                            + "WHERE UsrPermsn_ID = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(permissionID, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            permission = MapClass(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return permission;
        }
        public static List<UserPermission> GetByUserID(int userID)
        {
            List<UserPermission> permissions = null;

            string command = SELECT
                + "WHERE User_ID = " + USERID_PA;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlParameter userIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(userID, USERID_PA);
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { userIDparam });

            permissions = MapCollection(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return permissions;
        }
        internal static List<UserPermission> GetByUserID(int userID, SqlConnection sqlConn)
        {
            List<UserPermission> permissions = null;
            string command = SELECT
                + "WHERE User_ID = " + USERID_PA;

            SqlParameter userIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(userID, USERID_PA);
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { userIDparam });

            permissions = MapCollection(theReader);
            theReader.Close();

            return permissions;
        }

        public static int Save(UserPermission permission)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (permission.Id == 0)
                {
                    command = INSERT;
                    sqlParams = new SqlParameter[] { permission.IDParam,
                                                     permission.UserIDParam, 
                                                     permission.AccessIDPatternParam,
                                                     permission.StartDateParam,
                                                     permission.EndDateParam,
                                                     permission.CreatorIDParam, 
                                                     permission.CreatedDateParam };
                }
                else
                {
                    command = UPDATE
                            + " WHERE UsrPermsn_ID = " + ID_PA;
                    sqlParams = new SqlParameter[] { permission.UserIDParam, 
                                                     permission.AccessIDPatternParam, 
                                                     permission.AccessIDPatternParam,
                                                     permission.StartDateParam,
                                                     permission.EndDateParam, 
                                                     permission.CreatorIDParam, 
                                                     permission.CreatedDateParam, 
                                                     permission.ModifierIDParam, 
                                                     permission.ModifiedDateParam, 
                                                     permission.IDParam };
                }
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
        internal static int Save(UserPermission permission, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            try
            {
                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (permission.Id == 0)
                {
                    permission.Id = GetMaxUserPermitID(sqlConn, sqlTran);
                    command = INSERT;
                    sqlParams = new SqlParameter[] { permission.IDParam,
                                                     permission.UserIDParam, 
                                                     permission.AccessIDPatternParam,
                                                     permission.StartDateParam,
                                                     permission.EndDateParam,
                                                     permission.CreatorIDParam, 
                                                     permission.CreatedDateParam };
                }
                else
                {
                    command = UPDATE
                            + " WHERE UsrPermsn_ID = " + ID_PA;
                    sqlParams = new SqlParameter[] { permission.UserIDParam, 
                                                     permission.AccessIDPatternParam, 
                                                     permission.StartDateParam,
                                                     permission.EndDateParam, 
                                                     permission.CreatorIDParam, 
                                                     permission.CreatedDateParam, 
                                                     permission.ModifierIDParam, 
                                                     permission.ModifiedDateParam, 
                                                     permission.IDParam };
                }

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return counter;
        }

        public static int Delete(int permissionID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = DELETE
                                + "WHERE UsrPermsn_ID = " + ID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(permissionID, ID_PA);
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
        internal static int Delete(int permissionID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = DELETE
                            + "WHERE UsrPermsn_ID = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(permissionID, ID_PA);
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { iDParam });

            return counter;
        }
        internal static int DeleteByUserID(int userID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = DELETE
                            + "WHERE User_ID = " + userID;

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

            return counter;
        }

        public static bool HasPermission(int userID)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(UsrPermsn_ID) FROM [UsrPermsn] "
                            + "WHERE User_ID = " + userID;

            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

            MSSqlConnectionHandler.CloseDbConnection();

            return (Convert.ToInt32(ob) > 0);
        }
        #endregion
    }
}
