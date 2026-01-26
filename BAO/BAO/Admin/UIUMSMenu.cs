using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class UIUMSMenu : Base
    {
        #region Variables
        private string _name = string.Empty;
        private string _URL = string.Empty;
        private int _parentID = 0;
        private UIUMSMenu _parent = null;
        private int _tier = 0;
        private List<UIUMSMenu> _childMenus = null;
        private bool _isSysAdminAccesible = false;

        private int _sequence = 0;
        #endregion

        #region Constants
        private const string NAME = "@Name";
        private const string CURL = "@URL";
        private const string PARENTMENU = "@ParentMnu_ID";
        private const string TIER = "@Tier";
        private const string SYSADMINACCESS = "@IsSysAdminAccesible";
        private const string SEQUENCE = "@Sequence";

        private const string ALLCOLUMNS = "[Menu_ID], "
                                        + "[Name], "
                                        + "[URL], "
                                        + "[ParentMnu_ID], "
                                        + "[Tier], "
                                        + "[IsSysAdminAccesible], "
                                        + "[Sequence], ";

        private const string NOPKCOLUMNS = "[Name], "
                                         + "[URL], "
                                         + "[ParentMnu_ID],"
                                         + "[Tier],"
                                         + "[IsSysAdminAccesible], "
                                         + "[Sequence], ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM [Menu] ";

        private const string INSERT = "INSERT INTO [Menu] ("
                            + ALLCOLUMNS
                            + BASEMUSTCOLUMNS + ") "
                            + "VALUES ( "
                            + ID_PA + ", "//1
                            + NAME + ", "//2
                            + CURL + ", "//3
                            + PARENTMENU + ", "//4
                            + TIER + ", "//5
                            + SYSADMINACCESS + ", "//6
                            + SEQUENCE + ", "//6
                            + CREATORID_PA + ", "//7
                            + CREATEDDATE_PA + ")";//8
        //+ MODIFIERID + ", "//9
        //+ MOIDFIEDDATE + ")";//10

        private const string UPDATE = "UPDATE Menu "
                            + "SET Name = " + NAME + ", "
                            + "URL = " + CURL + ","
                            + "ParentMnu_ID = " + PARENTMENU + ","
                            + "Tier = " + TIER + ","
                            + "IsSysAdminAccesible = " + SYSADMINACCESS + ","
                            + "Sequence = " + SEQUENCE + ","
                            + "CreatedBy = " + CREATORID_PA + ","
                            + "CreatedDate = " + CREATEDDATE_PA + ","
                            + "ModifiedBy = " + MODIFIERID_PA + ","
                            + "ModifiedDate = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM [Menu] ";
        #endregion

        #region constructor
        public UIUMSMenu()
            : base()
        {
            _name = string.Empty;
            _URL = string.Empty;
            _parentID = 0;
            _sequence = 0;
            _parent = null;
            _tier = 0;
            _childMenus = null;
            _isSysAdminAccesible = false;
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
                nameParam.ParameterName = NAME;

                nameParam.Value = Name;

                return nameParam;
            }
        }

        public string URL
        {
            get { return _URL; }
            set { _URL = value; }
        }
        private SqlParameter URLParam
        {
            get
            {
                SqlParameter uRLParam = new SqlParameter();
                uRLParam.ParameterName = CURL;

                if (_URL.Trim() == string.Empty)
                {
                    uRLParam.Value = DBNull.Value;
                }
                else
                {
                    uRLParam.Value = _URL;
                }


                return uRLParam;
            }
        }

        public int ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }
        private SqlParameter ParentIDParam
        {
            get
            {
                SqlParameter parentIDParam = new SqlParameter();
                parentIDParam.ParameterName = PARENTMENU;


                if (_parentID == 0)
                {
                    parentIDParam.Value = DBNull.Value;
                }
                else
                {
                    parentIDParam.Value = _parentID;
                }


                return parentIDParam;
            }
        }

        public UIUMSMenu Parent
        {
            get
            {
                if (_parent == null)
                {
                    if (_parentID > 0)
                    {
                        _parent = UIUMSMenu.Get(_parentID);
                    }
                }
                return _parent;
            }
            //set { _parent = value; }
        }

        public int Tier
        {
            get { return _tier; }
            set { _tier = value; }
        }
        private SqlParameter TierParam
        {
            get
            {
                SqlParameter tierParam = new SqlParameter();
                tierParam.ParameterName = TIER;

                tierParam.Value = _tier;

                return tierParam;
            }
        }

        public List<UIUMSMenu> ChildMenus
        {
            get
            {
                if (_childMenus == null)
                {
                    if (this.Id > 0)
                    {
                        _childMenus = UIUMSMenu.GetByParentID(this.Id);
                    }
                }
                return _childMenus;
            }
            set { _childMenus = value; }
        }

        public bool IsSysAdminAccesible
        {
            get { return _isSysAdminAccesible; }
            set { _isSysAdminAccesible = value; }
        }
        private SqlParameter IsSysAdminAccesibleParam
        {
            get
            {
                SqlParameter isSysAdminAccesibleParam = new SqlParameter();
                isSysAdminAccesibleParam.ParameterName = SYSADMINACCESS;

                isSysAdminAccesibleParam.Value = _isSysAdminAccesible;

                return isSysAdminAccesibleParam;
            }
        }

        public int Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }
        private SqlParameter SequenceParam
        {
            get
            {
                SqlParameter SequenceParam = new SqlParameter();
                SequenceParam.ParameterName = SEQUENCE;


                if (_sequence == 0)
                {
                    SequenceParam.Value = DBNull.Value;
                }
                else
                {
                    SequenceParam.Value = Sequence;
                }


                return SequenceParam;
            }
        }
        #endregion

        #region Methods
        private static UIUMSMenu Mapper(SQLNullHandler nullHandler)
        {
            UIUMSMenu menu = new UIUMSMenu();

            menu.Id = nullHandler.GetInt32("Menu_ID");
            menu.Name = nullHandler.GetString("Name");
            menu.URL = nullHandler.GetString("URL");
            menu.ParentID = nullHandler.GetInt32("ParentMnu_ID");
            menu.Tier = nullHandler.GetInt32("Tier");
            menu.IsSysAdminAccesible = nullHandler.GetBoolean("IsSysAdminAccesible");
            menu.Sequence = nullHandler.GetInt32("Sequence");
            menu.CreatorID = nullHandler.GetInt32("CreatedBy");
            menu.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            menu.ModifierID = nullHandler.GetInt32("ModifiedBy");
            menu.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");
            return menu;
        }
        private static UIUMSMenu MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            UIUMSMenu menu = null;
            if (theReader.Read())
            {
                menu = new UIUMSMenu();
                menu = Mapper(nullHandler);
            }

            return menu;
        }
        private static List<UIUMSMenu> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<UIUMSMenu> menus = null;

            while (theReader.Read())
            {
                if (menus == null)
                {
                    menus = new List<UIUMSMenu>();
                }
                UIUMSMenu treeDetail = Mapper(nullHandler);
                menus.Add(treeDetail);
            }

            return menus;
        }

        public static List<UIUMSMenu> Get()
        {
            List<UIUMSMenu> menus = null;

            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            menus = MapCollection(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return menus;
        }

        internal static int GetMaxMenuID(SqlConnection sqlConn)
        {
            int newCalendarMasterID = 0;

            string command = "SELECT MAX(Menu_ID) FROM [Menu]";
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
        internal static int GetMaxMenuID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newCalendarMasterID = 0;

            string command = "SELECT MAX(Menu_ID) FROM [Menu]";
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

        public static UIUMSMenu Get(int mnuID)
        {
            UIUMSMenu menu = null;

            string command = SELECT
                            + "WHERE Menu_ID = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(mnuID, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            menu = MapClass(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return menu;
        }
        public static List<UIUMSMenu> GetRoots()
        {
            List<UIUMSMenu> menus = null;

            string command = SELECT
                            + "WHERE ParentMnu_ID IS NULL";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            menus = MapCollection(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return menus;
        }
        public static List<UIUMSMenu> GetSysAdminMenus()
        {
            List<UIUMSMenu> menus = null;

            string command = SELECT
                            + "WHERE IsSysAdminAccesible = " + SYSADMINACCESS;

            SqlParameter sysAdminParam = new SqlParameter(SYSADMINACCESS, true);
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sysAdminParam });

            menus = MapCollection(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return menus;
        }
        public static List<UIUMSMenu> GetByParentID(int parentMenuID)
        {
            List<UIUMSMenu> menus = null;

            string command = SELECT
                            + "WHERE ParentMnu_ID = " + PARENTMENU;

            SqlParameter parentMenuIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(parentMenuID, PARENTMENU);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { parentMenuIDParam });

            menus = MapCollection(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return menus;
        }

        public static int Save(UIUMSMenu menu)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (menu.Id == 0)
                {
                    menu.Id = GetMaxMenuID(sqlConn, sqlTran);
                    command = INSERT;
                    sqlParams = new SqlParameter[] { menu.IDParam,//1
                                                     menu.NameParam, //2
                                                     menu.URLParam, //3
                                                     menu.ParentIDParam, //4 
                                                     menu.TierParam, //5
                                                     menu.IsSysAdminAccesibleParam,//6
                                                     menu.SequenceParam,//
                                                     menu.CreatorIDParam, //7
                                                     menu.CreatedDateParam, //8
                                                     menu.ModifierIDParam, //9
                                                     menu.ModifiedDateParam };//10
                }
                else
                {
                    command = UPDATE
                            + " WHERE Menu_ID = " + ID_PA;
                    sqlParams = new SqlParameter[] { menu.NameParam, //1
                                                     menu.URLParam, //2
                                                     menu.ParentIDParam, //3
                                                     menu.TierParam, //4
                                                     menu.IsSysAdminAccesibleParam,//5
                                                     menu.SequenceParam,//
                                                     menu.CreatorIDParam, //6
                                                     menu.CreatedDateParam, //7
                                                     menu.ModifierIDParam, //8
                                                     menu.ModifiedDateParam,//9
                                                     menu.IDParam };//10
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

        public static int Delete(int menuID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = DELETE
                                + "WHERE Menu_ID = " + ID_PA;

                SqlParameter mnuIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(menuID, ID_PA);
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { mnuIDParam });

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
