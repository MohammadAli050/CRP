using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    public class Permission : Base
    {
        #region Variables
        private int _userID;
        private int _menuID;
        private User _user;
        private Menu _menu;
        #endregion

        #region Constants
        private const string MENUID = "@Menu_ID";
        private const string USERID = "@User_ID";

        private const string ALLCOLUMNS = "[UsrPermsn_ID], "
                                        + "[Menu_ID], "
                                        + "[User_ID], ";

        private const string NOPKCOLUMNS = "[Menu_ID], "
                                         + "[User_ID], ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM [AD_UsrPermsn] ";

        private const string INSERT = "INSERT INTO [AD_UsrPermsn] ("
                            + NOPKCOLUMNS
                            + BASECOLUMNS + ") "
                            + "VALUES ( "
                            + MENUID + ", "
                            + USERID + ", "
                            + CREATORID + ", "
                            + CREATEDDATE + ", "
                            + MODIFIERID + ", "
                            + MOIDFIEDDATE + ")";

        private const string UPDATE = "UPDATE AD_UsrPermsn "
                            + "SET Menu_ID = " + MENUID + ", "
                            + "User_ID = " + USERID + ", "
                            + "CreatedBy = " + CREATORID + ", "
                            + "CreatedDate = " + CREATEDDATE + ", "
                            + "ModifiedBy = " + MODIFIERID + ", "
                            + "ModifiedDate = " + MOIDFIEDDATE;

        private const string DELETE = "DELETE FROM [AD_UsrPermsn] ";
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
                userIDParam.ParameterName = USERID;

                userIDParam.Value = _userID;

                return userIDParam;
            }
        }
        public User User
        {
            get { return _user; }
        }

        public int MenuID
        {
            get { return _menuID; }
            set { _menuID = value; }
        }
        private SqlParameter MenuIDParam
        {
            get
            {
                SqlParameter menuIDParam = new SqlParameter();
                menuIDParam.ParameterName = MENUID;

                menuIDParam.Value = _menuID;

                return menuIDParam;
            }
        }
        public Menu Menu
        {
            get { return _menu; }
        } 
        #endregion

        #region Constructor
        public Permission()
        {
            _userID = 0;
            _menuID = 0;
            _user = new User();
            _menu = new Menu();
        } 
        #endregion

        #region Methods
        private static Permission Mapper(SQLNullHandler nullHandler)
        {
            Permission permission = new Permission();

            permission.Id = nullHandler.GetInt32("UsrPermsn_ID");
            permission.UserID = nullHandler.GetInt32("User_ID");
            permission.MenuID = nullHandler.GetInt32("Menu_ID");
            permission.CreatorID = nullHandler.GetInt32("CreatedBy");
            permission.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            permission.ModifierID = nullHandler.GetInt32("ModifiedBy");
            permission.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");
            return permission;
        }
        private static Permission MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            Permission permission = null;
            if (theReader.Read())
            {
                permission = new Permission();
                permission = Mapper(nullHandler);
            }

            return permission;
        }
        private static List<Permission> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Permission> permissions = null;

            while (theReader.Read())
            {
                if (permissions == null)
                {
                    permissions = new List<Permission>();
                }
                Permission permission = Mapper(nullHandler);
                permissions.Add(permission);
            }

            return permissions;
        }

        public static List<Permission> Get()
        {
            List<Permission> permissions = null;

            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            permissions = MapCollection(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return permissions;
        }
        public static Permission Get(int permissionID)
        {
            Permission permission = null;

            string command = SELECT
                            + "WHERE UsrPermsn_ID = @ID";

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(permissionID, "@ID");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            permission = MapClass(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return permission;
        }
        public static List<Permission> GetByUserID(int userID)
        {
            List<Permission> permissions = null;

            string command = SELECT
                + "WHERE User_ID = " + USERID;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            permissions = MapCollection(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return permissions;
        }
        internal static List<Permission> GetByUserID(int userID, SqlConnection sqlConn)
        {
            List<Permission> permissions = null;
            string command = SELECT
                + "WHERE User_ID = " + USERID;

            SqlParameter userIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(userID, USERID);
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn,new SqlParameter[]{userIDparam});

            permissions = MapCollection(theReader);
            theReader.Close();

            return permissions;
        }

        public static int Save(Permission permission)
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
                    sqlParams = new SqlParameter[] { permission.MenuIDParam, 
                                                     permission.UserIDParam, 
                                                     permission.CreatorIDParam, 
                                                     permission.CreatedDateParam, 
                                                     permission.ModifierIDParam, 
                                                     permission.ModifiedDateParam };
                }
                else
                {
                    command = UPDATE
                            + " WHERE UsrPermsn_ID = " + ID;
                    sqlParams = new SqlParameter[] { permission.MenuIDParam, 
                                                     permission.UserIDParam, 
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
        internal static int Save(Permission permission, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = string.Empty;
            SqlParameter[] sqlParams = null;

            if (permission.Id == 0)
            {
                command = INSERT;
                sqlParams = new SqlParameter[] { permission.MenuIDParam, 
                                                 permission.UserIDParam, 
                                                 permission.CreatorIDParam, 
                                                 permission.CreatedDateParam, 
                                                 permission.ModifierIDParam, 
                                                 permission.ModifiedDateParam };
            }
            else
            {
                command = UPDATE
                        + " WHERE UsrPermsn_ID = " + ID;
                sqlParams = new SqlParameter[] { permission.MenuIDParam, 
                                                 permission.UserIDParam, 
                                                 permission.CreatorIDParam, 
                                                 permission.CreatedDateParam, 
                                                 permission.ModifierIDParam, 
                                                 permission.ModifiedDateParam, 
                                                 permission.IDParam };
            }

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);
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
                                + "WHERE UsrPermsn_ID = " + ID;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(permissionID, ID);
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
                            + "WHERE UsrPermsn_ID = " + ID;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(permissionID, ID);
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { iDParam });

            return counter;
        }
        internal static int DeleteByUserID(int userID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = DELETE
                            + "WHERE User_ID = " + USERID;

            SqlParameter userIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(userID, USERID);
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { userIDParam });

            return counter;
        }
        #endregion
    }
}
