using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    public class Voucher : Base
    {
        #region DBColumns
        /*         
	    [VoucherID]                 [int] 
	    [Prefix]                    [nvarchar](50) 
	    [SLNO]                      [bigint] 
	    [DrAccountHeadsID]          [int]  
	    [CrAccountHeadsID]          [int]  
	    [Amount]                    [numeric](18, 2)
	    [CreatedBy]                 [int] 
	    [CreatedDate]               [datetime] 
	    [ModifiedBy]                [int] 
	    [ModifiedDate]              [datetime] 
	    [Remarks]                   [nvarchar](500) 
        */
        #endregion


        #region Variables
        private string _prefix;
        private Int64 _slNo;
        private int _drAcHeadsId;
        private int _crAcHeadsId;
        private decimal _amount;
        private string _remarks;

        private string _referenceNo;
        private string _chequeNo;
        private string _chequeBankName;
        private DateTime _chequeDate;
        #endregion

        #region Constructor
        public Voucher()
        {
            _prefix = string.Empty;
            _slNo = 0;
            _drAcHeadsId = 0;
            _crAcHeadsId = 0;
            _amount = 0;
            _remarks = string.Empty;

            _referenceNo = string.Empty;
            _chequeNo = string.Empty;
            _chequeBankName = string.Empty;
            _chequeDate = DateTime.Now;
        }
        #endregion

        #region Constants

        #region Column Constants
        private const string VOUCHERID = "VoucherID";

        private const string PREFIX = "Prefix";
        private const string PREFIX_PA = "@Prefix";

        private const string SLNO = "SLNO";
        private const string SLNO_PA = "@SLNO";

        private const string DRACHEADSID = "DrAccountHeadsID";
        private const string DRACHEADSID_PA = "@DrAccountHeadsID";

        private const string CRACHEADSID = "CrAccountHeadsID";
        private const string CRACHEADSID_PA = "@CrAccountHeadsID";

        private const string AMOUNT = "Amount";
        private const string AMOUNT_PA = "@Amount";

        private const string REMARKS = "Remarks";
        private const string REMARKS_PA = "@Remarks";

        private const string REFERENCENO = "ReferenceNo";
        private const string REFERENCENO_PA = "@ReferenceNo";

        private const string CHEQUENO = "ChequeNo";
        private const string CHEQUENO_PA = "@ChequeNo";

        private const string CHEQUEBANKNAME = "ChequeBankName";
        private const string CHEQUEBANKNAME_PA = "@ChequeBankName";

        private const string CHEQUEDATE = "ChequeDate";
        private const string CHEQUEDATE_PA = "@ChequeDate";
        #endregion

        #region PKCOlumns
        private const string ALLCOLUMNS = "[" + VOUCHERID + "], "
                                        + "[" + PREFIX + "], "
                                        + "[" + SLNO + "], "
                                        + "[" + DRACHEADSID + "], "
                                        + "[" + CRACHEADSID + "], "
                                        + "[" + AMOUNT + "], "
                                        + "[" + REMARKS + "], ";
        #endregion

        #region NOPKCOLUMNS
        private const string NOPKCOLUMNS =
                                        "[" + PREFIX + "], "
                                        + "[" + SLNO + "], "
                                        + "[" + DRACHEADSID + "], "
                                        + "[" + CRACHEADSID + "], "
                                        + "[" + AMOUNT + "], "
                                        + "[" + REMARKS + "], "

                                        + "[" + REFERENCENO + "], "
                                        + "[" + CHEQUENO + "], "
                                        + "[" + CHEQUEBANKNAME + "], "
                                        + "[" + CHEQUEDATE + "], ";

                                        
        #endregion

        private const string TABLENAME = " [Voucher] ";

        #region SELECT
        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                    + BASECOLUMNS
                    + "FROM" + TABLENAME;
        #endregion

        #region INSERT
        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                     + NOPKCOLUMNS
                     + BASEMUSTCOLUMNS + ")"
                     + "VALUES ( "
                        + PREFIX_PA + ", "
                        + SLNO_PA + ", "
                        + DRACHEADSID_PA + ", "
                        + CRACHEADSID_PA + ", "
                        + AMOUNT_PA + ", "
                        + REMARKS_PA + ", "

                        + REFERENCENO_PA + ", "
                        + CHEQUENO_PA + ", "
                        + CHEQUEBANKNAME_PA + ", "
                        + CHEQUEDATE_PA + ", "

                        + CREATORID_PA + ", "
                        + CREATEDDATE_PA + ")";
        #endregion

        #region UPDATE
        private const string UPDATE = "UPDATE" + TABLENAME
                    + "SET [" + PREFIX + "] =" + PREFIX_PA + ", "
                    + "[" + SLNO + "] = " + SLNO_PA + ", "
                    + "[" + DRACHEADSID + "] = " + DRACHEADSID_PA + ", "
                    + "[" + CRACHEADSID + "] = " + CRACHEADSID_PA + ", "
                    + "[" + AMOUNT + "] = " + AMOUNT_PA + ", "
                    + "[" + REMARKS + "] = " + REMARKS_PA + ", "

                    + "[" + CREATORID + "] = " + CREATORID_PA + ", "
                    + "[" + CREATEDDATE + "] = " + CREATEDDATE_PA + ", "
                    + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ", "
                    + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;
        #endregion

        private const string DELETE = "DELETE FROM" + TABLENAME;

        #endregion

        #region Properties

        public string VouPrefix
        {
            get
            {
                return this._prefix;
            }
            set
            {
                this._prefix = value;
            }
        }
        private SqlParameter VouPrefixParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = PREFIX_PA;
                if (VouPrefix == string.Empty)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = VouPrefix;
                }
                return sqlParam;
            }
        }

        public Int64 SlNo
        {
            get { return this._slNo; }
            set { this._slNo = value; }
        }
        private SqlParameter SlNoParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = SLNO_PA;
                if (SlNo == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = SlNo;
                }
                return sqlParam;
            }
        }

        public int DrAcHeadsId
        {
            get { return this._drAcHeadsId; }
            set { this._drAcHeadsId = value; }
        }
        private SqlParameter DrAcHeadsIdParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = DRACHEADSID_PA;
                if (DrAcHeadsId == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = DrAcHeadsId;
                }
                return sqlParam;
            }
        }

        public int CrAcHeadsId
        {
            get { return this._crAcHeadsId; }
            set { this._crAcHeadsId = value; }
        }
        private SqlParameter CrAcHeadsIdParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = CRACHEADSID_PA;
                if (CrAcHeadsId == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = CrAcHeadsId;
                }
                return sqlParam;
            }
        }

        public decimal Amount
        {
            get { return this._amount; }
            set { this._amount = value; }
        }
        private SqlParameter AmountParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = AMOUNT_PA;
                if (Amount == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = Amount;
                }
                return sqlParam;
            }
        }

        public string Remarks
        {
            get { return this._remarks; }
            set { this._remarks = value; }
        }
        private SqlParameter RemarksParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = REMARKS_PA;
                if (Remarks == string.Empty)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = Remarks;
                }
                return sqlParam;
            }
        }

        public string ReferenceNo
        {
            get { return this._referenceNo; }
            set { this._referenceNo = value; }
        }
        private SqlParameter ReferenceNoParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = REFERENCENO_PA;
                if (ReferenceNo == string.Empty)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = ReferenceNo;
                }
                return sqlParam;
            }
        }

        public string ChequeNo
        {
            get { return this._chequeNo; }
            set { this._chequeNo = value; }
        }
        private SqlParameter ChequeNoParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = CHEQUENO_PA;
                if (ChequeNo == string.Empty)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = ChequeNo;
                }
                return sqlParam;
            }
        }

        public string ChequeBankName
        {
            get { return this._chequeBankName; }
            set { this._chequeBankName = value; }
        }
        private SqlParameter ChequeBankNameParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = CHEQUEBANKNAME_PA;
                if (ChequeNo == string.Empty)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = ChequeBankName;
                }
                return sqlParam;
            }
        }

        public DateTime ChequeDate
        {
            get { return _chequeDate; }
            set { _chequeDate = value; }
        }
        protected SqlParameter ChequeDateParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = CHEQUEDATE_PA;
                sqlParam.Value = ChequeDate;

                return sqlParam;
            }
        }
        #endregion

        #region Functions
        private static Voucher VoucherMapper(SQLNullHandler nullHandler)
        {
            Voucher voucher = new Voucher();

            voucher.Id = nullHandler.GetInt32(VOUCHERID);
            voucher.VouPrefix = nullHandler.GetString(PREFIX);
            voucher.SlNo = nullHandler.GetInt64(SLNO);
            voucher.DrAcHeadsId = nullHandler.GetInt32(DRACHEADSID);
            voucher.CrAcHeadsId = nullHandler.GetInt32(CRACHEADSID);
            voucher.Amount = nullHandler.GetDecimal(AMOUNT);
            voucher.Remarks = nullHandler.GetString(REMARKS);

            voucher.CreatorID = nullHandler.GetInt32(CREATORID);
            voucher.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            voucher.ModifierID = nullHandler.GetInt32(MODIFIERID);
            voucher.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);

            return voucher;
        }

        private static List<Voucher> mapVouchers(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Voucher> vouchers = null;

            while (theReader.Read())
            {
                if (vouchers == null)
                {
                    vouchers = new List<Voucher>();
                }
                Voucher student = VoucherMapper(nullHandler);
                vouchers.Add(student);
            }

            return vouchers;
        }

        private static Voucher mapVoucher(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            Voucher voucher = null;
            if (theReader.Read())
            {
                voucher = new Voucher();
                voucher = VoucherMapper(nullHandler);
            }

            return voucher;
        }

        #endregion


        public static Int64 GetMaxSlNo()
        {
            Int64 maxSlNo = 0;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT MAX(SLNO) FROM" + TABLENAME;

            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

            MSSqlConnectionHandler.CloseDbConnection();


            if (ob == null || ob == DBNull.Value)
            {
                maxSlNo = 1;
            }
            else
            {
                maxSlNo = Convert.ToInt64(ob) + 1;
            }

            return maxSlNo;
        }

        internal static int saveVoucherDrAccount(Voucher voucher, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (voucher.Id == 0)
                {
                    #region Insert

                    voucher.CrAcHeadsId = 0;

                    command = INSERT;
                    sqlParams = new SqlParameter[] { 
                                                 voucher.VouPrefixParam,  
                                                 voucher.SlNoParam, 
                                                 voucher.DrAcHeadsIdParam, 
                                                 voucher.CrAcHeadsIdParam,  
                                                 voucher.AmountParam,
                                                 voucher.RemarksParam,

                                                 voucher.ReferenceNoParam,
                                                 voucher.ChequeBankNameParam,
                                                 voucher.ChequeDateParam,
                                                 voucher.ChequeNoParam,
                                                 
                                                 voucher.CreatorIDParam,
                                                 voucher.CreatedDateParam
                                                 };
                    #endregion
                }
                else
                {
                    #region Update
                    //command = UPDATE
                    //+ " WHERE [" + NODEID + "] = " + ID_PA;
                    //sqlParams = new SqlParameter[] { node.NameParam,  
                    //                             node.IsLastLevelParam, 
                    //                             node.MinCreditParam, 
                    //                             node.MaxCreditParam,  
                    //                             node.MinCoursesParam,  
                    //                             node.MaxCoursesParam,
                    //                             node.IsActiveParam,
                    //                             node.IsVirtualParam,
                    //                             node.IsBundleParam,
                    //                             node.IsAssociatedParam,
                    //                             node.OperatorIDParam,
                    //                             node.CreatorIDParam,
                    //                             node.CreatedDateParam,
                    //                             node.ModifierIDParam,
                    //                             node.ModifiedDateParam,
                    //                             node.IDParam};
                    #endregion
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);
                return counter;

            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        internal static int saveVoucherCrAccount(Voucher voucher, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = string.Empty;
                SqlParameter[] sqlParams = null;



                if (voucher.Id == 0)
                {
                    #region Insert

                    voucher.DrAcHeadsId = 0;
                    //voucher.CrAcHeadCode = crAcHdCode;


                    command = INSERT;
                    sqlParams = new SqlParameter[] { 
                                                 voucher.VouPrefixParam,  
                                                 voucher.SlNoParam, 
                                                 voucher.DrAcHeadsIdParam, 
                                                 voucher.CrAcHeadsIdParam,  
                                                 voucher.AmountParam,
                                                 voucher.RemarksParam,

                                                 voucher.ReferenceNoParam,
                                                 voucher.ChequeBankNameParam,
                                                 voucher.ChequeDateParam,
                                                 voucher.ChequeNoParam,
                                                 
                                                 voucher.CreatorIDParam,
                                                 voucher.CreatedDateParam
                                                 //voucher.ModifierIDParam,
                                                 //voucher.ModifiedDateParam
                                                 };
                    #endregion
                }
                else
                {
                    #region Update
                    //command = UPDATE
                    //+ " WHERE [" + NODEID + "] = " + ID_PA;
                    //sqlParams = new SqlParameter[] { node.NameParam,  
                    //                             node.IsLastLevelParam, 
                    //                             node.MinCreditParam, 
                    //                             node.MaxCreditParam,  
                    //                             node.MinCoursesParam,  
                    //                             node.MaxCoursesParam,
                    //                             node.IsActiveParam,
                    //                             node.IsVirtualParam,
                    //                             node.IsBundleParam,
                    //                             node.IsAssociatedParam,
                    //                             node.OperatorIDParam,
                    //                             node.CreatorIDParam,
                    //                             node.CreatedDateParam,
                    //                             node.ModifierIDParam,
                    //                             node.ModifiedDateParam,
                    //                             node.IDParam};
                    #endregion
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);
                return counter;

            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        public static int Save(Voucher voucher)
        {
            int drAcId, crAcId, drEffectedRow, crEffectedRow;

            try
            {
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                drAcId = voucher.DrAcHeadsId;
                crAcId = voucher.CrAcHeadsId;

                voucher.SlNo = Voucher.GetMaxSlNo();
                crEffectedRow = Voucher.saveVoucherCrAccount(voucher, sqlConn, sqlTran);//Data save in Voucher table(CR amount)

                voucher.DrAcHeadsId = drAcId;
                drEffectedRow = Voucher.saveVoucherDrAccount(voucher, sqlConn, sqlTran);//Data save in Voucher table (DR amount)
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }

            MSSqlConnectionHandler.CommitTransaction();
            MSSqlConnectionHandler.CloseDbConnection();

            if (crEffectedRow > 0 && drEffectedRow > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
