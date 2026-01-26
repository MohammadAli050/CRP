using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    public class Menu : Base
    {
        #region Variables
        private string _name = string.Empty;
        private string _URL = string.Empty;
        private int _parentID = 0;
        private Menu _parent = null;
        private int _tier = 0;
        #endregion

        #region Constants
        private const string NAME = "@Name";
        private const string CURL = "@URL";
        private const string PARENTMENU = "@ParentMnu_ID";
        private const string TIER = "@Tier";

        private const string ALLCOLUMNS = "[Menu_ID], "
                                        + "[Name], "
                                        + "[URL], "
                                        + "[ParentMnu_ID], "
                                        + "[Tier], ";

        private const string NOPKCOLUMNS = "[Name], "
                                         + "[URL], "
                                         + "[ParentMnu_ID],"
                                         + "[Tier],";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM [UIUEMS_AD_Menu] ";

        private const string INSERT = "INSERT INTO [UIUEMS_AD_Menu] ("
                            + NOPKCOLUMNS
                            + BASECOLUMNS + ") "
                            + "VALUES ( "
                            + NAME + ", "//1
                            + CURL + ", "//2
                            + PARENTMENU + ", "//3
                            + TIER + ", "//4
                            + CREATORID + ", "//5
                            + CREATEDDATE + ", "//6
                            + MODIFIERID + ", "//7
                            + MOIDFIEDDATE + ")";//8

        private const string UPDATE = "UPDATE UIUEMS_AD_Menu "
                            + "SET Name = " + NAME + ", "
                            + "URL = " + CURL + ","
                            + "ParentMnu_ID = " + PARENTMENU + ","
                            + "Tier = " + TIER + ","
                            + "CreatedBy = " + CREATORID + ","
                            + "CreatedDate = " + CREATEDDATE + ","
                            + "ModifiedBy = " + MODIFIERID + ","
                            + "ModifiedDate = " + MOIDFIEDDATE;

        private const string DELETE = "DELETE FROM [UIUEMS_AD_Menu] ";
        #endregion

        #region Properties
        public string Name
        {
            get { return Name; }
            set { _name = value; }
        }
        private SqlParameter NameParam
        {
            get
            {
                SqlParameter nameParam = new SqlParameter();
                nameParam.ParameterName = "@Name";

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
                uRLParam.ParameterName = "@URL";

                uRLParam.Value = _URL;

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
                parentIDParam.ParameterName = "@ParentMnu_ID";

                parentIDParam.Value = _parentID;

                return parentIDParam;
            }
        }
        public Menu Parent
        {
            get { return _parent; }
            set { _parent = value; }
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
                tierParam.ParameterName = "@Tier";

                tierParam.Value = _tier;

                return tierParam;
            }
        }
        #endregion

        #region Methods
        private static Menu Mapper(SQLNullHandler nullHandler)
        {
            Menu menu = new Menu();

            menu.Id = nullHandler.GetInt32("Menu_ID");
            menu.Name = nullHandler.GetString("Name");
            menu.URL = nullHandler.GetString("URL");
            menu.ParentID = nullHandler.GetInt32("ParentMnu_ID");
            menu.Tier = nullHandler.GetInt32("Tier");
            menu.CreatorID = nullHandler.GetInt32("CreatedBy");
            menu.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            menu.ModifierID = nullHandler.GetInt32("ModifiedBy");
            menu.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");
            return menu;
        }
        private static Menu MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            Menu menu = null;
            if (theReader.Read())
            {
                menu = new Menu();
                menu = Mapper(nullHandler);
            }

            return menu;
        }
        private static List<Menu> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Menu> menus = null;

            while (theReader.Read())
            {
                if (menus == null)
                {
                    menus = new List<Menu>();
                }
                Menu treeDetail = Mapper(nullHandler);
                menus.Add(treeDetail);
            }

            return menus;
        }

        public static List<Menu> Get()
        {
            List<Menu> menus = null;

            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            menus = MapCollection(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return menus;
        }

        public static Menu Get(int mnuID)
        {
            Menu menu = null;

            string command = SELECT
                            + "WHERE Menu_ID = @ID";

            SqlParameter treeCalDetailIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(mnuID, "@ID");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { treeCalDetailIDParam });

            menu = MapClass(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return menu;
        }

        public static int Save(Menu menu)
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
                    command = INSERT;
                    sqlParams = new SqlParameter[] { menu.NameParam, //1
                                                     menu.URLParam, //2
                                                     menu.ParentIDParam, //3 
                                                     menu.TierParam, //4
                                                     menu.CreatorIDParam, //5 
                                                     menu.CreatedDateParam, //6
                                                     menu.ModifierIDParam, //7
                                                     menu.ModifiedDateParam };//8
                }
                else
                {
                    command = UPDATE
                            + " WHERE Menu_ID = @ID";
                    sqlParams = new SqlParameter[] { menu.NameParam, //1
                                                     menu.URLParam, //2
                                                     menu.ParentIDParam, //3
                                                     menu.TierParam, //4
                                                     menu.CreatorIDParam, //5
                                                     menu.CreatedDateParam, //6
                                                     menu.ModifierIDParam, //7
                                                     menu.ModifiedDateParam,//8
                                                     menu.IDParam };//9
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

        public static int Delete(int menuID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = DELETE
                                + "WHERE Menu_ID = @ID";

                SqlParameter mnuIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(menuID, "@ID");
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
