using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;
using Common;

namespace BussinessObject
{
    [Serializable]
    public class AccountsHead: Base
    {
        #region DBColumns
        /*
        [AccountsID] [int] NOT NULL,
	    [Name] [nvarchar](50) ,
	    [Code] [nvarchar](50) ,
	    [ParentID] [int] NULL,
	    [CreatedBy] [int] NULL,
	    [CreatedDate] [datetime] NULL,
	    [ModifiedBy] [int] NULL,
	    [ModifiedDate] [datetime] NULL,
        Tag varchar (100)
	    [Remarks] [nvarchar](500)
         * 
         * IsLeaf bit
         */
        #endregion

        #region Variables
        //private int _accountsID;
        private string _name;
        //private string _code;
        private int _parentID;
        private string _tag;
        private string _remarks;
        private bool _isLeaf;
        private bool _sysMandatory;
        #endregion

        #region Public Variable
        public readonly string[] TAG_CBO = { CBO_NA, CBO_ADMISSION, CBO_STUDENT };
        #endregion

        #region Constants

        #region Tag Combo Constants
        private const string CBO_NA = "NA";
        private const string CBO_ADMISSION = "Admission";
        private const string CBO_STUDENT = "Student";
        #endregion

        #endregion

        #region Constructor
        public AccountsHead()
            : base()
        {
            _name = string.Empty;
            //_code = string.Empty;
            _parentID = 0;
            _tag = string.Empty;
            _remarks = string.Empty;
            _isLeaf = false;
            _sysMandatory = false;
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
                SqlParameter Param = new SqlParameter();

                Param.ParameterName = NAME_PA;
                Param.Value = Name;

                return Param;
            }
        }

        //public string Code
        //{
        //    get { return _code; }
        //    set { _code = value; }
        //}
        //private SqlParameter CodeParam
        //{
        //    get
        //    {
        //        SqlParameter param = new SqlParameter();
        //        param.ParameterName = CODE_PA;
        //        param.Value = Code;
        //        return param;
        //    }
        //}

        public int ParentId
        {
            get { return _parentID; }
            set { _parentID = value; }
        }
        private SqlParameter ParentIdParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = PARENTID_PA;
                param.Value = ParentId;
                return param;
            }
        }

        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        private SqlParameter TagParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = TAG_PA;
                param.Value = Tag;
                return param;
            }
        }

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }
        private SqlParameter RemarksParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = REMARKS_PA;
                param.Value = Remarks;
                return param;
            }
        }

        public bool IsLeaf
        {
            get { return _isLeaf; }
            set { _isLeaf = value; }
        }
        private SqlParameter IsLeafParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = ISLEAF_PA;
                param.Value = IsLeaf;
                return param;
            }
        }

        public bool SysMandatory
        {
            get { return _sysMandatory; }
            set { _sysMandatory = value; }
        }
        private SqlParameter SysMandatoryParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = SYSMANDATORY_PA;
                param.Value = SysMandatory;
                return param;
            }
        }

        #endregion

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

        private const string ISLEAF = "IsLeaf";
        private const string ISLEAF_PA = "@IsLeaf";

        private const string SYSMANDATORY = "SysMandatory";
        private const string SYSMANDATORY_PA = "@SysMandatory";
        #endregion

        #region ALLCOLUMNS
        private const string ALLCOLUMNS = "[AccountsID], "
                                            + NAME + ", "
            //+ CODE + ", "
                                            + PARENTID + ", "
                                            + TAG + ", "
                                            + REMARKS + ", "
                                            + ISLEAF + ", "
                                            + SYSMANDATORY + ", ";
        #endregion

        #region NOPKCOLUMNS
        private const string NOPKCOLUMNS = NAME + ", "
            //+ CODE + ", "
                                            + PARENTID + ", "
                                            + TAG + ", "
                                            + REMARKS + ", "
                                            + ISLEAF + ", "
                                            + SYSMANDATORY + ", ";
        #endregion
      
        #region Select
        private const string SELECT = "SELECT "
                           + ALLCOLUMNS
                           + BASECOLUMNS
                           + "FROM" + TABLENAME;
        #endregion

        #region Insert
        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                             + ALLCOLUMNS
                             + BASEMUSTCOLUMNS + ")"
                             + "VALUES ( "
                             + ID_PA + ", "
                             + NAME_PA + ", "
                             //+ CODE_PA + ", "
                             + PARENTID_PA + ", "
                             + TAG_PA + ","
                             + REMARKS_PA + ", "
                             + ISLEAF_PA + ", "
                             + SYSMANDATORY_PA + ", "
                             + CREATORID_PA + ", "
                             + CREATEDDATE_PA + ")";
        #endregion

        #region Update
        private const string UPDATE = "UPDATE" + TABLENAME
                           + "SET [" + NAME + "] = " + NAME_PA + ", "
                            //+ "[" + CODE + "] = " + CODE_PA + ","
                           + "[" + PARENTID + "] = " + PARENTID_PA + ","
                           + "[" + TAG + "] = " + TAG_PA + ","
                           + "[" + REMARKS + "] = " + REMARKS_PA + ","
                           + "[" + ISLEAF + "] = " + ISLEAF_PA + ","
                           + "[" + SYSMANDATORY + "] = " + SYSMANDATORY_PA + ","

                           + "[CreatedBy] = " + CREATORID_PA + ","
                           + "[CreatedDate] = " + CREATEDDATE_PA + ","
                           + "[ModifiedBy] = " + MODIFIERID_PA + ","
                           + "[ModifiedDate] = " + MOIDFIEDDATE_PA;
        #endregion

        #region DELETE
        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region TABLE NAME
        private const string TABLENAME = " [AccountHeads] ";
        #endregion        
        
        #region Methods

        private static bool IsExistName(string name)
        {

            object ob = AccountsDAO.GetHeadCount(name);

            if (Convert.ToInt32(ob) == 0)
            {
                return false;
            }

            return true;
        }

        public static int Delete(int id)
        {
            int count = AccountsDAO.Delete(id);

            return 0;
        }       

        public static List<AccountsHead> GetRoots()
        {
            List<AccountsHead> acHead = null;

            string command = SELECT
                            + "WHERE ParentID = 0";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            acHead = MapCollection(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return acHead;
        }

        private static List<AccountsHead> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<AccountsHead> acHead = null;

            while (theReader.Read())
            {
                if (acHead == null)
                {
                    acHead = new List<AccountsHead>();
                }
                AccountsHead treeDetail = AccountsMapper(nullHandler);
                acHead.Add(treeDetail);
            }

            return acHead;
        }
        
        private static AccountsHead AccountsMapper(SQLNullHandler nullHandler)
        {
            AccountsHead accounts = new AccountsHead();

            accounts.Id = nullHandler.GetInt32( ACCOUNTSID);
            accounts.Name = nullHandler.GetString(NAME);
            //accounts.Code = nullHandler.GetString(CODE);
            accounts.ParentId = nullHandler.GetInt32(PARENTID);
            accounts.Tag = nullHandler.GetString(TAG);
            accounts.Remarks = nullHandler.GetString(REMARKS);
            accounts.IsLeaf = nullHandler.GetBoolean(ISLEAF);
            accounts.SysMandatory = nullHandler.GetBoolean(SYSMANDATORY);

            accounts.CreatorID = nullHandler.GetInt32("CreatedBy");
            accounts.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            accounts.ModifierID = nullHandler.GetInt32("ModifiedBy");
            accounts.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            return accounts;
        }

        public static int Save(AccountsHead acHead)
        {           
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (acHead.Id == 0)
                {
                    acHead.Id = AccountsHead.GetMaxAcHeadsID(sqlConn, sqlTran);
                    command = INSERT;
                    sqlParams = new SqlParameter[] { acHead.IDParam,
                                                     acHead.NameParam,
                                                     //acHead.CodeParam,
                                                     acHead.ParentIdParam,
                                                     acHead.TagParam,
                                                     acHead.RemarksParam, 
                                                     acHead.IsLeafParam,
                                                     acHead.SysMandatoryParam,
                                                    
                                                     acHead.CreatorIDParam, 
                                                     acHead.CreatedDateParam, 
                                                     };
                }
                else
                {
                    command = UPDATE
                            + " WHERE AccountsID = " + ID_PA;
                    sqlParams = new SqlParameter[] { acHead.NameParam,
                                                     //acHead.CodeParam,
                                                     acHead.ParentIdParam,
                                                     acHead.TagParam,
                                                     acHead.RemarksParam,
                                                     acHead.IsLeafParam,
                                                     acHead.SysMandatoryParam,
 
                                                     acHead.CreatorIDParam, 
                                                     acHead.CreatedDateParam, 
                                                     acHead.ModifierIDParam, 
                                                     acHead.ModifiedDateParam,
                                                     acHead.IDParam };
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

        public static List<AccountsHead> GetByParentID(int id)
        {
            List<AccountsHead> acHead = null;

            string command = SELECT
                            + "WHERE ParentID = " + id;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);
                       
            acHead = MapCollection(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return acHead;
        }

        public static AccountsHead Get(int id)
        {
            AccountsHead acHead = null;

            string command = SELECT
                            + "WHERE AccountsID = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(id, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            acHead = MapClass(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return acHead;
        }

        public static AccountsHead GetByname(string name)
        {
            AccountsHead acHead = null;

            string command = SELECT
                            + "WHERE Name = " + NAME_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(name, NAME_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            acHead = MapClass(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return acHead;
        }

        private static AccountsHead MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            AccountsHead acHead = null;
            if (theReader.Read())
            {
                acHead = new AccountsHead();
                acHead = AccountsMapper(nullHandler);
            }

            return acHead;
        }

        public static int GetRootCount()
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(*) FROM " + TABLENAME
                            + "WHERE [" + PARENTID + "] =  0 ";

            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

            MSSqlConnectionHandler.CloseDbConnection();

            return Convert.ToInt32(ob) + 1;
        }

        public static int GetChildCount(int parentId)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(*) FROM " + TABLENAME
                            + "WHERE [" + PARENTID + "] = " + parentId;

            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

            MSSqlConnectionHandler.CloseDbConnection();

            return Convert.ToInt32(ob) + 1;
        }

        internal static int saveStdAccount(AccountsHead accountHead, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = string.Empty;
                SqlParameter[] sqlParams = null;


                if (accountHead.Id == 0)
                {
                    #region Insert
                    accountHead.Id = AccountsHead.GetMaxAcHeadsID(sqlConn, sqlTran);
                    command = INSERT;
                    sqlParams = new SqlParameter[] { accountHead.IDParam,
                                                 accountHead.NameParam,  
                                                 //accountHead.CodeParam, 
                                                 accountHead.ParentIdParam, 
                                                 accountHead.TagParam,
                                                 accountHead.RemarksParam,
                                                 accountHead.IsLeafParam,
                                                 accountHead.SysMandatoryParam,
                                                 
                                                 accountHead.CreatorIDParam,
                                                 accountHead.CreatedDateParam,
                                                 accountHead.ModifierIDParam,
                                                 accountHead.ModifiedDateParam};
                    #endregion
                }
                else
                {
                    #region Update
                    //command = UPDATE
                    //+ " WHERE [" + NODEID + "] = " + ID_PA;
                    //sqlParams = new SqlParameter[] { //accountHead.IDParam,
                    //                             accountHead.NameParam,  
                    //                             accountHead.CodeParam, 
                    //                             accountHead.ParentIdParam, 
                    //                             accountHead.TagParam,
                    //                             accountHead.RemarksParam,
                                                 
                    //                             accountHead.CreatorIDParam,
                    //                             accountHead.CreatedDateParam,
                    //                             accountHead.ModifierIDParam,
                    //                             accountHead.ModifiedDateParam};
                    #endregion
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                if (counter <= 0)
                {
                    return 0;
                }
                else
                {
                    return accountHead.Id;
                }

            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        private static int GetMaxAcHeadsID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
             int newAcHeadID = 0;

             string command = "SELECT MAX(AccountsID) FROM" + TABLENAME;
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

            if (ob == null || ob == DBNull.Value)
            {
                newAcHeadID = 1;
            }
            else if (ob is Int32)
            {
                newAcHeadID = Convert.ToInt32(ob) + 1;
            }

            return newAcHeadID;
             
        }

        public static int GetMaxRootId()
        {
            int count = 0;
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT max(AccountsID) FROM  " + TABLENAME
                            + "WHERE [" + PARENTID + "] =  0";

            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

            MSSqlConnectionHandler.CloseDbConnection();

            if (ob == null || ob == DBNull.Value)
            {
                count = 0;
            }
            else if (ob is Int32)
            {
                count = Convert.ToInt32(ob);
            }

            return count;
        }       

        public static bool HasDuplicateName(AccountsHead acHead)
        {
            if (acHead == null)
            {
                return false;
            }
            else
            {
                if (acHead.Id == 0)
                {
                    if (AccountsHead.IsExistName(acHead.Name))
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
                    return false;
                }
            }
        }        

        public static bool HasDuplicateTag(AccountsHead acHead)
        {
            if (acHead == null)
            {
                return false;
            }
            else
            {
                if (acHead.Id == 0)
                {
                    if (AccountsHead.IsExistTag(acHead.Tag))
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
                    if (AccountsHead.IsExistTag(acHead.Tag, acHead.Id))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        private static bool IsExistTag(string tag, int id)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(*) FROM" + TABLENAME
                            + "WHERE [Tag] = " + TAG_PA + " and AccountsID <> " + ID_PA;

            SqlParameter codeParamTag = MSSqlConnectionHandler.MSSqlParamGenerator(tag, TAG_PA);
            SqlParameter codeParamId = MSSqlConnectionHandler.MSSqlParamGenerator(id, ID_PA);

            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { codeParamTag, codeParamId });

            MSSqlConnectionHandler.CloseDbConnection();

            if (Convert.ToInt32(ob) == 0)
            {
                return false;
            }
            return true;
        }

        private static bool IsExistTag(string tag)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(*) FROM" + TABLENAME
                            + "WHERE [Tag] = " + TAG_PA;

            SqlParameter codeParam = MSSqlConnectionHandler.MSSqlParamGenerator(tag, TAG_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { codeParam });

            MSSqlConnectionHandler.CloseDbConnection();

            if (Convert.ToInt32(ob) == 0)
            {
                return false;
            }
            return true;
        }

        public static int GetAcHeadsIdByTag(string tag)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT AccountsID FROM" + TABLENAME
                            + "WHERE [Tag] = " + TAG_PA;

            SqlParameter codeParam = MSSqlConnectionHandler.MSSqlParamGenerator(tag, TAG_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { codeParam });

            MSSqlConnectionHandler.CloseDbConnection();

            return Convert.ToInt32(ob);
        }

        public static int GetParentId(string tag)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT AccountsID FROM" + TABLENAME
                            + "WHERE [Tag] = " + TAG_PA;

            SqlParameter codeParam = MSSqlConnectionHandler.MSSqlParamGenerator(tag, TAG_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { codeParam });

            MSSqlConnectionHandler.CloseDbConnection();

            return Convert.ToInt32(ob);
        }

        #endregion

        public static List<AccountsHead> GetLeafAccounts()
        {
            List<AccountsHead> acHeads = null;

            string command = SELECT + "WHERE IsLeaf = 1";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            acHeads = MapCollection(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return acHeads;
        }
    }
}
