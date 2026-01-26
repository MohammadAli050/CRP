using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;
using System.Text.RegularExpressions;

namespace BussinessObject
{
    public class User : Base
    {
        #region Variables
        private string _logInID;
        private string _lastLogInID;
        private string _password;
        private UserType _type;
        private int _userSourceTableID;
        private List<UserPermission> _permissions = null;
        #endregion

        #region Constants
        private const string LOGINID = "@LogInIDParam";
        private const string PASSWORD = "@PasswordParam";
        private const string USERSRCTBLID = "@UserSourceTableIDParam";
        private const string USERTYPE = "@UserTypeParam";

        private const string ALLCOLUMNS = "[User_ID], "
                                        + "[LogInID], "
                                        + "[Password], "
                                        + "[UserSourceTableID], "
                                        + "[UserType], ";

        private const string NOPKCOLUMNS = "[LogInID], "
                                        + "[Password], "
                                        + "[UserSourceTableID], "
                                        + "[UserType], ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM [UIUEMS_AD_User] ";

        private const string INSERT = "INSERT INTO [UIUEMS_AD_User] ("
                            + NOPKCOLUMNS
                            + BASECOLUMNS + ")"
                            + "VALUES ( "
                            + LOGINID + ", "
                            + PASSWORD + ", "
                            + USERSRCTBLID + ", "
                            + USERTYPE + ", "
                            + CREATORID + ", "
                            + CREATEDDATE + ", "
                            + MODIFIERID + ", "
                            + MOIDFIEDDATE + ")";

        private const string UPDATE = "UPDATE UIUEMS_AD_User "
                            + "SET LogInID = " + LOGINID + ", "
                            + "Password = " + PASSWORD + ","
                            + "UserSourceTableID = " + USERSRCTBLID + ","
                            + "UserType = " + USERTYPE + ","
                            + "CreatedBy = " + CREATORID + ","
                            + "CreatedDate = " + CREATEDDATE + ","
                            + "ModifiedBy = " + MODIFIERID + ","
                            + "ModifiedDate = " + MOIDFIEDDATE;

        private const string DELETE = "DELETE FROM [UIUEMS_AD_User] ";
        #endregion

        #region Constructor
        public User()
        {
            _logInID = string.Empty;
            _password = string.Empty;
            _type = UserType.None;
            _userSourceTableID = 0;
            _permissions = new List<UserPermission>();
        }
        #endregion

        #region Properties
        public string LogInID
        {
            get { return _logInID; }
            set { _logInID = value; }
        }
        private SqlParameter LogInIDParam
        {
            get
            {
                SqlParameter longInIDParam = new SqlParameter();
                longInIDParam.ParameterName = LOGINID;

                longInIDParam.Value = _logInID;

                return longInIDParam;
            }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        private SqlParameter PasswordParam
        {
            get
            {
                SqlParameter passwordParam = new SqlParameter();
                passwordParam.ParameterName = PASSWORD;

                passwordParam.Value = _password;

                return passwordParam;
            }
        }

        public UserType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private SqlParameter UserTypeParam
        {
            get
            {
                SqlParameter userTypeParam = new SqlParameter();
                userTypeParam.ParameterName = USERTYPE;

                userTypeParam.Value = (Int32)_type;

                return userTypeParam;
            }
        }

        public int UserSourceTableID
        {
            get { return _userSourceTableID; }
            set { _userSourceTableID = value; }
        }
        private SqlParameter UserSourceTableIDParam
        {
            get
            {
                SqlParameter userSourceTableIDParam = new SqlParameter();
                userSourceTableIDParam.ParameterName = USERSRCTBLID;

                userSourceTableIDParam.Value = _userSourceTableID;

                return userSourceTableIDParam;
            }
        }

        public List<UserPermission> Permissions
        {
            get
            {
                if (_permissions == null)
                {
                    if (this.Id > 0)
                    {
                        _permissions = UserPermission.GetByUserID(this.Id);
                    }
                }
                return _permissions;
            }
            set { _permissions = value; }
        }
        public string LastLongInID
        {
            get { return _lastLogInID; }
            set { _lastLogInID = value; }
        }

        #region Static Properties
        private static User _currentUser = null;
        public static User CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        #endregion
        #endregion

        #region Methods
        private static User Mapper(SQLNullHandler nullHandler)
        {
            User user = new User();

            user.Id = nullHandler.GetInt32("User_ID");
            user.LogInID = nullHandler.GetString("LogInID");
            user.LastLongInID = nullHandler.GetString("LogInID");
            user.Password = nullHandler.GetString("Password");
            user.UserSourceTableID = nullHandler.GetInt32("UserSourceTableID");
            user.Type = (UserType)nullHandler.GetInt32("UserType");
            user.CreatorID = nullHandler.GetInt32("CreatedBy");
            user.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            user.ModifierID = nullHandler.GetInt32("ModifiedBy");
            user.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");
            return user;
        }
        private static User MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            User user = null;
            if (theReader.Read())
            {
                user = new User();
                user = Mapper(nullHandler);
            }

            return user;
        }
        private static List<User> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<User> users = null;

            while (theReader.Read())
            {
                if (users == null)
                {
                    users = new List<User>();
                }
                User user = Mapper(nullHandler);
                users.Add(user);
            }

            return users;
        }

        internal static int GetMaxUserID(SqlConnection sqlConn)
        {
            int newCalendarMasterID = 0;

            string command = "SELECT MAX(User_ID) FROM [UIUEMS_AD_User]";
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
        internal static int GetMaxUserID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newCalendarMasterID = 0;

            string command = "SELECT MAX(User_ID) FROM [UIUEMS_AD_User]";
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

        public static List<User> Get(bool includePermissions)
        {
            List<User> users = null;

            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            users = MapCollection(theReader);

            if (includePermissions)
            {
                foreach (User user in users)
                {
                    user.Permissions = UserPermission.GetByUserID(user.Id, sqlConn);
                }
            }

            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return users;
        }
        public static User Get(int userID, bool includePermissions)
        {
            User user = null;

            string command = SELECT
                            + "WHERE User_ID = " + ID;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(userID, ID);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            user = MapClass(theReader);

            if (includePermissions)
            {
                user.Permissions = UserPermission.GetByUserID(user.Id, sqlConn);
            }

            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return user;
        }
        public static User GetByLogInID(string logInID, bool includePermissions)
        {
            User user = null;

            string command = SELECT
                            + "WHERE LogInID = " + ID;

            SqlParameter longIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(logInID, ID);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { longIDParam });

            user = MapClass(theReader);

            if (includePermissions)
            {
                user.Permissions = UserPermission.GetByUserID(user.Id, sqlConn);
            }

            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return user;
        }
        public static int Save(User user)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (user.Id == 0)
                {
                    user.Id = GetMaxUserID(sqlConn, sqlTran);
                    command = INSERT;
                    sqlParams = new SqlParameter[] { user.LogInIDParam, 
                                                     user.PasswordParam,
                                                     user.UserSourceTableIDParam, 
                                                     user.UserTypeParam, 
                                                     user.CreatorIDParam, 
                                                     user.CreatedDateParam, 
                                                     user.ModifierIDParam, 
                                                     user.ModifiedDateParam };
                }
                else
                {
                    command = UPDATE
                            + " WHERE User_ID = " + ID;
                    sqlParams = new SqlParameter[] { user.LogInIDParam, 
                                                     user.PasswordParam,
                                                     user.UserSourceTableIDParam, 
                                                     user.UserTypeParam,
                                                     user.CreatorIDParam, 
                                                     user.CreatedDateParam, 
                                                     user.ModifierIDParam, 
                                                     user.ModifiedDateParam, 
                                                     user.IDParam };
                }

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                UserPermission.DeleteByUserID(user.Id, sqlConn, sqlTran);
                if (user.Permissions != null)
                {
                    foreach (UserPermission permission in user.Permissions)
                    {
                        permission.Id = 0;
                        permission.UserID = user.Id;
                        UserPermission.Save(permission, sqlConn, sqlTran);
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
        public static int Delete(int userID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                UserPermission.DeleteByUserID(userID, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE User_ID = " + ID;

                SqlParameter userIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(userID, ID);
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { userIDParam });

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

        public static bool IsStrongPassward(string passward)
        {
            bool strong = false;
            // Create a new Regex object.
            Regex r = new Regex(@"^\w*(?=\w*\d)(?=\w*[a-z])(?=\w*[A-Z])\w*$");
            // Find a single match in the string.
            Match m = r.Match(passward.Trim());
            if (!m.Success)
            {
                strong = false;
                //throw new ServiceException("Invalid password. A valid password must contain at least one Capital letter,one Small letter and one number");
            }
            else
            {
                strong = true;
            }

            return strong;
        }
        //public static bool IsExist(string logInID)
        //{
        //    bool 
        //}
        #endregion
    }
}
