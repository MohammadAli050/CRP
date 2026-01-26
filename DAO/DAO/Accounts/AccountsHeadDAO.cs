using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;
using Common;

   public class AccountsDAO :Base_DAO
    {

        #region Column
        private const string ACCOUNTSID = "AccountsID";
        //private const string ACCOUNTSID_PA = "@AccountsID";

        private const string NAME = "Name";
        private const string NAME_PA = "@Name";

        //private const string CODE = "Code";
        //private const string CODE_PA = "@Code";

        private const string PARENTID = "ParentID";
        private const string PARENTID_PA = "@ParentID";

        private const string TAG = "Tag";
        private const string TAG_PA = "@Tag";

        private const string REMARKS = "Remarks";
        private const string REMARKS_PA = "@Remarks";
        #endregion

        #region ALLCOLUMNS
        private const string ALLCOLUMNS = "[AccountsID], "
                                            + NAME + ", "
            //+ CODE + ", "
                                            + PARENTID + ", "
                                            + TAG + ", "
                                            + REMARKS + ", ";
        #endregion

        #region TABLE NAME
        private const string TABLENAME = " [AccountHeads] ";
        #endregion        

        #region DELETE
        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion 

        #region Methods

        public static object GetHeadCount(string name)
        {
            try
            {
                string command = "SELECT COUNT(*) FROM" + TABLENAME
                                           + "WHERE [Name] = " + NAME_PA;


                List<SqlParam> pLists = new List<SqlParam>();
                SqlParam pList = new SqlParam();

                pList.SqlParamName = NAME_PA;
                pList.SqlParamValue = name;
                pLists.Add(pList);

                //String[,] pList = { { name, NAME_PA } };

                object ob = QueryHandler.ExecuteSelect(command, pLists);

                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int Delete(int id)
        {
            try
            {
                int counter = 0;

                string command = DELETE + "WHERE AccountsID = " + ID_PA;

                //String[,] pList = { { id.ToString(), ID_PA } };

                List<SqlParam> pLists = new List<SqlParam>();
                SqlParam pList = new SqlParam();

                pList.SqlParamName = ID_PA;
                pList.SqlParamValue = id;
                pLists.Add(pList);

                counter = QueryHandler.ExecuteDelete(command, pLists);

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
