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
    public class Role : Base
    {
        #region DB variables

        //[dbo].[Role](
        //[ID] [int] IDENTITY(1,1) NOT NULL,
        //[RoleName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
        //[SessionTime] [int] NULL,
        //[CreatedBy] [int] NOT NULL,
        //[CreatedDate] [datetime] NOT NULL,
        //[ModifiedBy] [int] NULL,
        //[ModifiedDate] [datetime] NULL

        #endregion

        #region Variables

        private string _roleName;
        private int _sessionTime;
        private List<Role_Menu> _roleMenus;

        #endregion

        #region Constants

        private const string ID = "ID";

        private const string ROLENAME_PA = "@RoleNameParam";
        private const string ROLENAME = "RoleName";

        private const string SESSIONTIME_PA = "@SessionTimeParam";
        private const string SESSIONTIME = "SessionTime";

        private const string TABLENAME = "[Role]";

        private const string ALLCOLUMNS = "[ID], "
                                        + "[RoleName], "
                                        + "[SessionTime], ";

        private const string NOPKCOLUMNS = "[RoleName], "
                                        + "[SessionTime], ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM " + TABLENAME;

        private const string INSERT = "INSERT INTO " + TABLENAME + " ("
                            + ALLCOLUMNS
                            + BASEMUSTCOLUMNS + ")"
                            + "VALUES ( "
                            + ID_PA + ", "//0
                            + ROLENAME_PA + ", "//1
                            + SESSIONTIME_PA + ", "//2
                            + CREATORID_PA + ", "//6
                            + CREATEDDATE_PA + ")";//7
        //+ MODIFIERID + ", "//8
        //+ MOIDFIEDDATE + ")";//9

        private const string UPDATE = "UPDATE " + TABLENAME
                            + " SET [RoleName] = " + ROLENAME_PA + ", "
                            + "[SessionTime] = " + SESSIONTIME_PA + ","
                            + "[CreatedBy] = " + CREATORID_PA + ","
                            + "[CreatedDate] = " + CREATEDDATE_PA + ","
                            + "[ModifiedBy] = " + MODIFIERID_PA + ","
                            + "[ModifiedDate] = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM " + TABLENAME;

        #endregion

        #region Constructor
        public Role()
            : base()
        {
            _roleName = string.Empty;
            _sessionTime = 0;
        }
        #endregion

        #region Properties

        public string RoleName
        {
            get { return _roleName; }
            set { _roleName = value; }
        }
        private SqlParameter RoleNameParam
        {
            get
            {
                SqlParameter roleNameParam = new SqlParameter();
                roleNameParam.ParameterName = ROLENAME_PA;
                roleNameParam.Value = RoleName;
                return roleNameParam;
            }
        }

        public int SessionTime
        {
            get { return _sessionTime; }
            set { _sessionTime = value; }
        }
        private SqlParameter SessionTimeParam
        {
            get
            {
                SqlParameter sessionTimeParam = new SqlParameter();
                sessionTimeParam.ParameterName = SESSIONTIME_PA;

                if (SessionTime > 0)
                {
                    sessionTimeParam.Value = SessionTime;
                }
                else
                {
                    sessionTimeParam.Value = DBNull.Value;
                }

                return sessionTimeParam;
            }
        }

        public List<Role_Menu> RoleMenus
        {
            get
            {
                if (_roleMenus == null)
                {
                    if (this.Id > 0)
                    {
                        _roleMenus = Role_Menu.GetMenusByRoleID(this.Id);
                    }
                }
                return _roleMenus;
            }
            set { _roleMenus = new List<Role_Menu>(); }
        }


        #endregion

        #region Methods

        private static Role RoleMapper(SQLNullHandler nullHandler)
        {
            Role role = new Role();

            role.Id = nullHandler.GetInt32(ID);
            role.RoleName = nullHandler.GetString(ROLENAME);
            role.SessionTime = nullHandler.GetInt32(SESSIONTIME);
            role.CreatorID = nullHandler.GetInt32(CREATORID);
            role.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            role.ModifierID = nullHandler.GetInt32(MODIFIERID);
            role.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);

            return role;
        }
        private static List<Role> MapRoles(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Role> roles = null;
            while (theReader.Read())
            {
                if (roles == null)
                {
                    roles = new List<Role>();
                }
                Role role = RoleMapper(nullHandler);
                roles.Add(role);
            }

            return roles;
        }
        private static Role MapRole(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            Role role = null;
            if (theReader.Read())
            {
                role = RoleMapper(nullHandler);
            }

            return role;
        }

        internal static int GetMaxRoleID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newRoleID = 0;

            string command = "SELECT MAX(ID) FROM " + TABLENAME;
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

            if (ob == null || ob == DBNull.Value)
            {
                newRoleID = 1;
            }
            else if (ob is Int32)
            {
                int oldID = Convert.ToInt32(ob);
                if (oldID == -1)
                {
                    newRoleID = oldID + 2;
                }
                else
                {
                    newRoleID = oldID + 1;
                }
            }

            return newRoleID;
        }
        /// <summary>
        /// get specific rows
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public static Role Get(int roleID)
        {
            Role role = null;
            try
            {
                string command = SELECT
                                    + " WHERE " + ID + " = " + ID_PA;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(roleID, ID_PA);

                SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

                role = MapRole(theReader);
                theReader.Close();

                role.RoleMenus = Role_Menu.GetByRoleID(role.Id, sqlConn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            MSSqlConnectionHandler.CloseDbConnection();
            return role;
        }
        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns></returns>
        public static List<Role> Get()
        {
            List<Role> roles = null;
            try
            {
                string command = SELECT + " where ID != 0 order by RoleName asc";

                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

                roles = MapRoles(theReader);
                theReader.Close();

                if (roles != null)
                {
                    foreach (Role role in roles)
                    {
                        role.RoleMenus = Role_Menu.GetByRoleID(role.Id, sqlConn);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            MSSqlConnectionHandler.CloseDbConnection();
            return roles;
        }
        public static List<Role> GetByRoleName(string roleName)
        {
            List<Role> roles = null;
            try
            {
                string command = SELECT
                                + " WHERE " + ROLENAME + " like '%" + roleName + "%' order by RoleName asc";

                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

                roles = MapRoles(theReader);
                theReader.Close();
                if (roles != null)
                {
                    foreach (Role role in roles)
                    {
                        role.RoleMenus = Role_Menu.GetByRoleID(role.Id, sqlConn);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            MSSqlConnectionHandler.CloseDbConnection();
            return roles;
        }

        public static int Save(Role role)
        {
            int counter = 0;
            try
            {
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (role.Id == 0)
                {
                    command = INSERT;
                    role.Id = GetMaxRoleID(sqlConn, sqlTran);
                    sqlParams = new SqlParameter[] { role.IDParam,
                                                     role.RoleNameParam, 
                                                     role.SessionTimeParam,
                                                     role.CreatorIDParam, 
                                                     role.CreatedDateParam };
                }
                else
                {
                    command = UPDATE
                            + " WHERE " + ID + " = " + ID_PA;
                    sqlParams = new SqlParameter[] { role.RoleNameParam, 
                                                     role.SessionTimeParam,
                                                     role.CreatorIDParam, 
                                                     role.CreatedDateParam, 
                                                     role.ModifierIDParam, 
                                                     role.ModifiedDateParam, 
                                                     role.IDParam };
                }

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                Role_Menu.DeleteByRoleID(role.Id, sqlConn, sqlTran);
                if (role.RoleMenus != null)
                {
                    foreach (Role_Menu roleMenu in role.RoleMenus)
                    {
                        roleMenu.Id = 0;
                        roleMenu.RoleID = role.Id;
                        roleMenu.CreatorID = role.CreatorID;
                        roleMenu.CreatedDate = role.CreatedDate;

                        Role_Menu.Save(roleMenu, sqlConn, sqlTran);
                    }
                }

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
            return counter;
        }
        public static int Delete(int roleID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                Role_Menu.DeleteByRoleID(roleID, sqlConn, sqlTran);

                string command = DELETE
                                + " WHERE ID = " + roleID;

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

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
