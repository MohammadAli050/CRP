using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;
using System.Text.RegularExpressions;
using Common;
using System.Web;

namespace BussinessObject
{
    [Serializable]
    public class Role_Menu : Base
    {
        #region Variables

        private int _roleID;
        private int _menuID;
        private UIUMSMenu _menu;

        #endregion

        #region Constants

        private const string ID = "ID";

        private const string ROLEID = "RoleID";
        private const string ROLEID_PA = "@RoleIDParam";

        private const string MENUID = "MenuID";
        private const string MENUID_PA = "@MenuIDParam";

        private const string TABLENAME = "[RoleMenu]";

        private const string ALLCOLUMNS = " [ID], "
                                          + "[RoleID], "
                                          + "[MenuID], ";

        private const string NOPKCOLUMNS = "[RoleID], "
                                          + "[MenuID], ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM " + TABLENAME;

        private const string INSERT = "INSERT INTO " + TABLENAME + " ("
                            + NOPKCOLUMNS
                            + BASEMUSTCOLUMNS + ")"
                            + "VALUES ( "
                            + ROLEID_PA + ", "//1
                            + MENUID_PA + ", "//2
                            + CREATORID_PA + ", "//6
                            + CREATEDDATE_PA + ")";//7
        //+ MODIFIERID + ", "//8
        //+ MOIDFIEDDATE + ")";//9

        private const string UPDATE = "UPDATE " + TABLENAME
                            + " SET [RoleID] = " + ROLEID_PA + ", "
                            + "[MenuID] = " + MENUID_PA + ","
                            + "[CreatedBy] = " + CREATORID_PA + ","
                            + "[CreatedDate] = " + CREATEDDATE_PA + ","
                            + "[ModifiedBy] = " + MODIFIERID_PA + ","
                            + "[ModifiedDate] = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM " + TABLENAME;

        #endregion

        #region Constructor
        public Role_Menu()
            : base()
        {
            _roleID = 0;
            _menuID = 0;
        }
        #endregion

        #region Properties

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
                menuIDParam.ParameterName = MENUID_PA;
                menuIDParam.Value = MenuID;
                return menuIDParam;
            }
        }

        public UIUMSMenu Menu
        {
            get {
                    if (_menu == null)
                    {
                        if (_menuID > 0)
                        {
                            _menu = UIUMSMenu.Get(_menuID);
                        }
                    }
                    return _menu;
            }
        }

        #endregion

        #region Methods

        private static Role_Menu RoleMenuMapper(SQLNullHandler nullHandler)
        {
            Role_Menu roleMenu = new Role_Menu();

            roleMenu.Id = nullHandler.GetInt32(ID);
            roleMenu.RoleID = nullHandler.GetInt32(ROLEID);
            roleMenu.MenuID = nullHandler.GetInt32(MENUID);
            roleMenu.CreatorID = nullHandler.GetInt32(CREATORID);
            roleMenu.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            roleMenu.ModifierID = nullHandler.GetInt32(MODIFIERID);
            roleMenu.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);

            return roleMenu;
        }
        private static List<Role_Menu> mapRoleMenus(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Role_Menu> roleMenus = null;
            while (theReader.Read())
            {
                if (roleMenus == null)
                {
                    roleMenus = new List<Role_Menu>();
                }
                Role_Menu roleMenu = RoleMenuMapper(nullHandler);
                roleMenus.Add(roleMenu);
            }

            return roleMenus;
        }
        private static Role_Menu mapRoleMenu(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            Role_Menu roleMenu = null;
            if (theReader.Read())
            {
                roleMenu = RoleMenuMapper(nullHandler);
            }

            return roleMenu;
        }
        
        internal static int Save(Role_Menu roleMenu, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            try
            {
                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (roleMenu.Id == 0)
                {
                    command = INSERT;
                    sqlParams = new SqlParameter[] { roleMenu.RoleIDParam, 
                                                 roleMenu.MenuIDParam, 
                                                 roleMenu.CreatorIDParam, 
                                                 roleMenu.CreatedDateParam };
                }
                else
                {
                    command = UPDATE
                            + " WHERE " + ID + " = " + ID_PA;
                    sqlParams = new SqlParameter[] { roleMenu.RoleIDParam, 
                                                 roleMenu.MenuIDParam, 
                                                 roleMenu.CreatorIDParam, 
                                                 roleMenu.CreatedDateParam, 
                                                 roleMenu.ModifierIDParam, 
                                                 roleMenu.ModifiedDateParam, 
                                                 roleMenu.IDParam };
                }

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);
            }
            catch(Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
            return counter;
        }
        internal static List<Role_Menu> GetByRoleID(int roleID, SqlConnection sqlConn)
        {
            List<Role_Menu> roleMenu = null;
            string command = SELECT
                + "WHERE " + ROLEID + " = " + ROLEID_PA;

            SqlParameter roleIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(roleID, ROLEID_PA);
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { roleIDparam });

            roleMenu = mapRoleMenus(theReader);
            theReader.Close();

            return roleMenu;
        }
        public static bool HasPermission(int roleID, int menuID)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(ID) FROM " + TABLENAME
                            + " WHERE RoleID = " + roleID + " AND MenuID = " + menuID;
            
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

            MSSqlConnectionHandler.CloseDbConnection();
            return (Convert.ToInt32(ob) > 0);
        }
        internal static int DeleteByRoleID(int roleID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;
            string command = DELETE
                            + " WHERE RoleID = " + roleID;

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

            return counter;
        }
        public static List<Role_Menu> GetMenusByRoleID(int roleID)
        {
            List<Role_Menu> roleMenu = null;
            string command;
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader;
            if (roleID > 0)
            {
                command = SELECT
                + "WHERE " + ROLEID + " = " + ROLEID_PA;
                SqlParameter roleIDparam = MSSqlConnectionHandler.MSSqlParamGenerator(roleID, ROLEID_PA);
                theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { roleIDparam });            
            }
            else
            {
                command = SELECT;
                theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);
            }

            roleMenu = mapRoleMenus(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return roleMenu;
        }
        #endregion
    }
}
