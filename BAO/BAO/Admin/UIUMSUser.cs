using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;
using System.Text.RegularExpressions;
using Common;


using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
namespace BussinessObject
{
    [Serializable]
    public class UIUMSUser : Base
    {
        #region Variables
        private string _logInID;
        private string _lastLogInID;
        private string _password;
        private int _roleID;
        private DateTime _roleExistStartDate;
        private DateTime _roleExistEndDate;

        private bool _isActive;
        private bool _isSysAdmin;

        private List<UserPermission> _userPermissions = null;
        private List<Role_Menu> _roleMenus = null;
        private List<UserMenu> _userMenus = null;

        private string _userName = "";
        #endregion

        #region Constants
        private const string LOGINID_PA = "@LogInIDParam";
        private const string PASSWORD_PA = "@PasswordParam";
        private const string ISACTIVE_PA = "@IsActive";


        private const string ROLEID_PA = "@RoleIDParam";
        private const string ROLEID = "RoleID";

        private const string STARTDATE_PA = "@RoleExistStartDate";
        private const string STARTDATE = "RoleExistStartDate";

        private const string ENDDATE_PA = "@RoleExistEndDate";
        private const string ENDDATE = "RoleExistEndDate";

        private const string ALLCOLUMNS = "[User_ID], "
                                        + "[LogInID], "
                                        + "[Password], "
                                        + "[RoleID], "
                                        + "[RoleExistStartDate], "
                                        + "[RoleExistEndDate], "
                                        + "[IsActive], ";

        private const string NOPKCOLUMNS = "[LogInID], "
                                         + "[Password], "
                                         + "[RoleID], "
                                         + "[RoleExistStartDate], "
                                         + "[RoleExistEndDate], "
                                         + "[IsActive], ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM [User] ";

        private const string INSERT = "INSERT INTO [User] ("
                            + ALLCOLUMNS
                            + BASEMUSTCOLUMNS + ")"
                            + "VALUES ( "
                            + ID_PA + ", "//0
                            + LOGINID_PA + ", "//1
                            + PASSWORD_PA + ", "//2
                            + ROLEID_PA + ", "
                            + STARTDATE_PA + ", "
                            + ENDDATE_PA + ", "
                            + ISACTIVE_PA + ", "//5
                            + CREATORID_PA + ", "//6
                            + CREATEDDATE_PA + ")";//7
        //+ MODIFIERID + ", "//8
        //+ MOIDFIEDDATE + ")";//9

        private const string UPDATE = "UPDATE [User] "
                            + "SET [LogInID] = " + LOGINID_PA + ", "
                            + "[Password] = " + PASSWORD_PA + ","
                            + "[RoleID] = " + ROLEID_PA + ","
                            + "[RoleExistStartDate] = " + STARTDATE_PA + ","
                            + "[RoleExistEndDate] = " + ENDDATE_PA + ","
                            + "[IsActive] = " + ISACTIVE_PA + ","
                            + "[CreatedBy] = " + CREATORID_PA + ","
                            + "[CreatedDate] = " + CREATEDDATE_PA + ","
                            + "[ModifiedBy] = " + MODIFIERID_PA + ","
                            + "[ModifiedDate] = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM [User] ";
        #endregion

        #region Constructor
        public UIUMSUser()
            : base()
        {
            _logInID = string.Empty;
            _password = string.Empty;
            _roleID = 0;
            _roleExistStartDate = DateTime.MinValue;
            _roleExistEndDate = DateTime.MinValue;
            _isActive = false;
            _isSysAdmin = false;
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
                longInIDParam.ParameterName = LOGINID_PA;

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
                passwordParam.ParameterName = PASSWORD_PA;

                passwordParam.Value = _password;

                return passwordParam;
            }
        }

        public List<Role_Menu> RoleMenus
        {
            get
            {
                if (_roleMenus == null)
                {
                    if (this.RoleID > 0)
                    {
                        _roleMenus = Role_Menu.GetMenusByRoleID(this.RoleID);
                    }
                }
                return _roleMenus;
            }
            set { }
        }

        public List<UserMenu> UserMenus
        {
            get
            {
                if (_userMenus == null)
                {
                    if (this.RoleID > 0)
                    {
                        _userMenus = UserMenuManager.GetAll(this.Id).Where(d => d.ValidTo.Value.Date >= DateTime.Now.Date).ToList();
                    }
                }
                return _userMenus;
            }
            set { }
        }

        public List<UserPermission> UserPermissions
        {
            get
            {
                if (_userPermissions == null)
                {
                    if (this.Id > 0)
                    {
                        _userPermissions = UserPermission.GetByUserID(this.Id);
                    }
                }
                return _userPermissions;
            }
            set
            {
                _userPermissions = value;
            }
        }

        public string LastLongInID
        {
            get { return _lastLogInID; }
            set { _lastLogInID = value; }
        }

        public int RoleID
        {
            get { return _roleID; }
            set { _roleID = value; }
        }
        private SqlParameter RoleIDParam
        {
            get
            {
                SqlParameter roleIDParam = new SqlParameter();
                roleIDParam.ParameterName = ROLEID_PA;
                roleIDParam.Value = RoleID;
                return roleIDParam;
            }
        }

        public DateTime RoleExistStartDate
        {
            get { return _roleExistStartDate; }
            set { _roleExistStartDate = value; }
        }
        private SqlParameter RoleExistStartDateParam
        {
            get
            {
                SqlParameter roleExistStartDateParam = new SqlParameter();
                roleExistStartDateParam.ParameterName = STARTDATE_PA;
                roleExistStartDateParam.Value = RoleExistStartDate;
                return roleExistStartDateParam;
            }
        }

        public DateTime RoleExistEndDate
        {
            get { return _roleExistEndDate; }
            set { _roleExistEndDate = value; }
        }
        private SqlParameter RoleExistEndDateParam
        {
            get
            {
                SqlParameter roleExistEndDateParam = new SqlParameter();
                roleExistEndDateParam.ParameterName = ENDDATE_PA;
                roleExistEndDateParam.Value = RoleExistEndDate;
                return roleExistEndDateParam;
            }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        private SqlParameter IsActiveParam
        {
            get
            {
                SqlParameter isActiveParam = new SqlParameter();
                isActiveParam.ParameterName = ISACTIVE_PA;

                isActiveParam.Value = _isActive;

                return isActiveParam;
            }
        }

        public bool IsSysAdmin
        {
            get
            {
                if (this.Id == -1)
                {
                    _isSysAdmin = true;
                }
                return _isSysAdmin;
            }
            set { _isSysAdmin = value; }
        }

        public string UserName
        {
            get
            {
                if (string.IsNullOrEmpty(_userName))
                {
                    if (this.Id > 0)
                    {
                        LogicLayer.BusinessObjects.Person person = LogicLayer.BusinessLogic.PersonManager.GetByUserId(Id);

                        if (person == null)
                        {
                            _userName = "";
                        }
                        else
                        {
                            _userName = person.FullName;
                        }
                    }
                }
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        #region Static Properties
        //private static UIUMSUser _currentUser = null;
        //public static UIUMSUser CurrentUser
        //{
        //    get { return _currentUser; }
        //    set { _currentUser = value; }
        //}
        #endregion

        #endregion

        #region Methods
        private static UIUMSUser Mapper(SQLNullHandler nullHandler)
        {
            UIUMSUser user = new UIUMSUser();

            user.Id = nullHandler.GetInt32("User_ID");
            user.LogInID = nullHandler.GetString("LogInID");
            user.LastLongInID = nullHandler.GetString("LogInID");
            user.Password = nullHandler.GetString("Password");
            user.RoleID = nullHandler.GetInt32(ROLEID);
            user.RoleExistStartDate = nullHandler.GetDateTime(STARTDATE);
            user.RoleExistEndDate = nullHandler.GetDateTime(ENDDATE);
            user.IsActive = nullHandler.GetBoolean("IsActive");
            user.CreatorID = nullHandler.GetInt32("CreatedBy");
            user.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            user.ModifierID = nullHandler.GetInt32("ModifiedBy");
            user.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");
            return user;
        }
        private static UIUMSUser MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            UIUMSUser user = null;
            if (theReader.Read())
            {
                user = new UIUMSUser();
                user = Mapper(nullHandler);
            }

            return user;
        }
        private static List<UIUMSUser> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<UIUMSUser> users = null;

            while (theReader.Read())
            {
                if (users == null)
                {
                    users = new List<UIUMSUser>();
                }
                UIUMSUser user = Mapper(nullHandler);
                users.Add(user);
            }

            return users;
        }

        internal static int GetMaxUserID(SqlConnection sqlConn)
        {
            int newCalendarMasterID = 0;

            string command = "SELECT MAX(User_ID) FROM [User]";
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

            if (ob == null || ob == DBNull.Value)
            {
                newCalendarMasterID = 1;
            }
            else if (ob is Int32)
            {
                int oldID = Convert.ToInt32(ob);
                if (oldID == -1)
                {
                    newCalendarMasterID = oldID + 2;
                }
                else
                {
                    newCalendarMasterID = oldID + 1;
                }
            }

            return newCalendarMasterID;
        }
        internal static int GetMaxUserID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newCalendarMasterID = 0;

            string command = "SELECT MAX(User_ID) FROM [User]";
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

            if (ob == null || ob == DBNull.Value)
            {
                newCalendarMasterID = 1;
            }
            else if (ob is Int32)
            {
                int oldID = Convert.ToInt32(ob);
                if (oldID == -1)
                {
                    newCalendarMasterID = oldID + 2;
                }
                else
                {
                    newCalendarMasterID = oldID + 1;
                }
            }

            return newCalendarMasterID;
        }

        public static List<UIUMSUser> Get(bool includePermissions)
        {
            List<UIUMSUser> users = null;

            string command = SELECT + " where User_ID != -2";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            users = MapCollection(theReader);
            theReader.Close();

            if (includePermissions)
            {
                //foreach (UIUMSUser user in users)
                //{
                //    user.Permissions = UserPermission.GetByUserID(user.Id, sqlConn);
                //}
            }

            MSSqlConnectionHandler.CloseDbConnection();

            return users;
        }
        public static UIUMSUser Get(int userID, bool includePermissions)
        {
            UIUMSUser user = null;

            string command = SELECT
                            + "WHERE User_ID = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(userID, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            user = MapClass(theReader);
            theReader.Close();

            //if (includePermissions)
            //{
            //    user.UserPermissions = UserPermission.GetByUserID(user.Id, sqlConn);
            //}
            MSSqlConnectionHandler.CloseDbConnection();
            return user;
        }
        public static UIUMSUser GetByLogInID(string logInID, bool includePermissions)
        {
            UIUMSUser user = null;

            string command = SELECT + "WHERE LogInID COLLATE Latin1_General_CS_AS = '" + logInID + "'";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            user = MapClass(theReader);
            theReader.Close();

            //if (includePermissions)
            //{
            //    user.Permissions = UserPermission.GetByUserID(user.Id, sqlConn);
            //}

            MSSqlConnectionHandler.CloseDbConnection();
            return user;
        }
        public static int Save(UIUMSUser user)
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
                    sqlParams = new SqlParameter[] { user.IDParam,
                                                     user.LogInIDParam, 
                                                     user.PasswordParam,
                                                     user.RoleIDParam,
                                                     user.RoleExistStartDateParam,
                                                     user.RoleExistEndDateParam,
                                                     user.IsActiveParam, 
                                                     user.CreatorIDParam, 
                                                     user.CreatedDateParam };
                }
                else
                {
                    command = UPDATE
                            + " WHERE User_ID = " + ID_PA;
                    sqlParams = new SqlParameter[] { user.LogInIDParam, 
                                                     user.PasswordParam,
                                                     user.RoleIDParam,
                                                     user.RoleExistStartDateParam,
                                                     user.RoleExistEndDateParam,
                                                     user.IsActiveParam,
                                                     user.CreatorIDParam, 
                                                     user.CreatedDateParam, 
                                                     user.ModifierIDParam, 
                                                     user.ModifiedDateParam, 
                                                     user.IDParam };
                }

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                UserPermission.DeleteByUserID(user.Id, sqlConn, sqlTran);
                if (user.UserPermissions != null)
                {
                    foreach (UserPermission permission in user.UserPermissions)
                    {
                        permission.Id = 0;
                        permission.UserID = user.Id;
                        permission.CreatorID = user.CreatorID;
                        permission.CreatedDate = user.CreatedDate;

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
        public static int SaveSysAdmin(UIUMSUser user)
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
                    user.Id = -2;
                    command = INSERT;
                    sqlParams = new SqlParameter[] { user.IDParam,//0
                                                     user.LogInIDParam, //1
                                                     user.PasswordParam,//2
                                                     user.RoleIDParam,
                                                     user.RoleExistStartDateParam,
                                                     user.RoleExistEndDateParam,
                                                     user.IsActiveParam, //5
                                                     user.CreatorIDParam, //6
                                                     user.CreatedDateParam };//9


                    counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                    //UserPermission.DeleteByUserID(user.Id, sqlConn, sqlTran);
                    //if (user.UserPermissions != null)
                    //{
                    //    foreach (UserPermission permission in user.UserPermissions)
                    //    {
                    //        permission.Id = 0;
                    //        permission.UserID = user.Id;                            
                    //        UserPermission.Save(permission, sqlConn, sqlTran);
                    //    }
                    //}
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
                                + "WHERE User_ID = " + ID_PA;

                SqlParameter userIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(userID, ID_PA);
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
        public static bool HasDuplicateLogInID(UIUMSUser user)
        {
            if (user == null)
            {
                return UIUMSUser.IsExist(user.LogInID);
            }
            else
            {
                if (user.Id == 0)
                {
                    if (UIUMSUser.IsExist(user.LogInID))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (user.LogInID != user.LastLongInID)
                    {
                        return UIUMSUser.IsExist(user.LogInID);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public static bool IsExist(string logInID)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(*) FROM [User] "
                            + "WHERE [User].[LogInID] COLLATE Latin1_General_CS_AS = " + LOGINID_PA;
            SqlParameter logInIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(logInID, LOGINID_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { logInIDParam });

            MSSqlConnectionHandler.CloseDbConnection();

            return (Convert.ToInt32(ob) > 0);
        }
        public static bool Authenticate(int userID, int menuID)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(*) FROM Menu,User,UsrPermsn "
                           + "WHERE Menu.Menu_ID = UsrPermsn.Menu_ID AND "
                           + "User.User_ID = UsrPermsn.User_ID AND "
                           + "Menu.Menu_ID = @MenuID AND User.User_ID = @UserID";

            SqlParameter userIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(userID, "@UserID");
            SqlParameter menuIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(menuID, "@MenuID");
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { menuIDParam, userIDParam });

            MSSqlConnectionHandler.CloseDbConnection();

            return (Convert.ToInt32(ob) > 0);
        }
        public static bool Login(string login, string password)
        {
            bool bIsValid = false;
            if (login.Length == 0)
            {
                bIsValid = false;
                throw new Exception("Empty Login ID not allowed");
            }

            if (!UIUMSUser.IsExist(login))
            {
                bIsValid = false;
                throw new Exception("Invalid Login-ID, Enter valid Login-ID");
            }

            UIUMSUser currentUser = UIUMSUser.GetByLogInID(login, true);
            if (currentUser == null)
            {
                bIsValid = false;
                currentUser = null;
                throw new Exception("Invalid Login-ID, Enter valid Login-ID");
            }
            if (!currentUser.IsActive)
            {
                bIsValid = false;
                currentUser = null;
                throw new Exception("Entered user is inactive");
            }

            if (password.Length == 0)
            {
                bIsValid = false;
                currentUser = null;
                throw new Exception("Empty Password not  allowed");
            }

            bIsValid = true;
            // if (_currentUser.Password != Utilities.Encrypt(password))
            if (currentUser.Password != password)
            {
                bIsValid = false;
                currentUser = null;
                throw new Exception("Invalid password, try again...");
            }

            return bIsValid;
        }

        public static int ChangePasswordSave(UIUMSUser user)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();
                SqlParameter[] sqlParams = null;

                string command = UPDATE
                                + " WHERE User_ID = " + ID_PA;
                sqlParams = new SqlParameter[] { user.LogInIDParam, 
                                                     user.PasswordParam,
                                                     user.RoleIDParam,
                                                     user.RoleExistStartDateParam,
                                                     user.RoleExistEndDateParam,
                                                     user.IsActiveParam,
                                                     user.CreatorIDParam, 
                                                     user.CreatedDateParam, 
                                                     user.ModifierIDParam, 
                                                     user.ModifiedDateParam, 
                                                     user.IDParam};


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
        #endregion

        public static List<UIUMSUser> GetUserIdAndLoginId()
        {
            List<UIUMSUser> users = null;

            string command = SELECT + " order by LogInID ";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            users = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return users;
        }
    }
}
