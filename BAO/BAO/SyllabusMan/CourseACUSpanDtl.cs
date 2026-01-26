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
    public class CourseACUSpanDtl : Base
    {
        #region DBColumns
        //CourseACUSpanDtlID	int	    Unchecked
        //CourseACUSpanMasID	int	    Unchecked
        //CreditUnits	        money	Unchecked
        #endregion

        #region Variables
        int _courseACUSpanMasID;
        
        decimal _creditUnits;
        #endregion

        #region Constructor
        public CourseACUSpanDtl()
            : base()
        {
            _courseACUSpanMasID = 0;
            _creditUnits = 0;
        }
        #endregion


        #region Constants
        #region Column Constants
        private const string COURSEACUSPANDTLID = "CourseACUSpanDtlID";//0

        private const string COURSEACUSPANMASID = "CourseACUSpanMasID";//1
        private const string COURSEACUSPANMASID_PA = "@CourseACUSpanMasID";//1

        private const string CREDITUNITS = "CreditUnits";//2
        private const string CREDITUNITS_PA = "@CreditUnits";//2
        #endregion

        #region PKCOlumns
        private const string ALLCOLUMNS = "[" + COURSEACUSPANDTLID + "], "//0
                                        + "[" + COURSEACUSPANMASID + "], "//1
                                        + "[" + CREDITUNITS + "], ";//2
        #endregion

        #region NOPKCOLUMNS
        private const string NOPKCOLUMNS = "[" + COURSEACUSPANMASID + "], "//1
                                        + "[" + CREDITUNITS + "], ";//2
        #endregion

        private const string TABLENAME = " [CourseACUSpanDtl] ";

        #region SELECT
        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                    + BASECOLUMNS
                    + "FROM" + TABLENAME;
        #endregion

        #region INSERT
        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                     + ALLCOLUMNS
                     + BASECOLUMNS + ")"
                     + "VALUES ( "
                     + ID_PA + ", "//0
                     + COURSEACUSPANMASID_PA + ", "//1
                     + CREDITUNITS_PA + ", "//2
                     + CREATORID_PA + ", "//3
                     + CREATEDDATE_PA + ", "//4
                     + MODIFIERID_PA + ", "//5
                     + MOIDFIEDDATE_PA + ")";//7 
        #endregion

        #region UPDATE
        private const string UPDATE = "UPDATE" + TABLENAME
                    + "SET [" + COURSEACUSPANMASID + "] = " + COURSEACUSPANMASID_PA + ", "//1
                    + "[" + CREDITUNITS + "] = " + CREDITUNITS_PA + ", "//4
                    + "[" + CREATORID + "] = " + CREATORID_PA + ", "//5
                    + "[" + CREATEDDATE + "] = " + CREATEDDATE_PA + ", "//6
                    + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ", "//7
                    + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;//8
        #endregion

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion


        #region Properties
        public int CourseACUSpanMasID
        {
            get { return _courseACUSpanMasID; }
            set { _courseACUSpanMasID = value; }
        }
        private SqlParameter CourseACUSpanMasIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = COURSEACUSPANMASID_PA;
                sqlParam.Value = CourseACUSpanMasID;
                
                return sqlParam;
            }
        } 

        public decimal CreditUnits
        {
            get { return _creditUnits; }
            set { _creditUnits = value; }
        }
        private SqlParameter CreditUnitsParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = CREDITUNITS_PA;

                sqlParam.Value = CreditUnits;

                return sqlParam;
            }
        } 
        #endregion

        #region Methods
        private static CourseACUSpanDtl Mapper(SQLNullHandler nullHandler)
        {
            CourseACUSpanDtl obj = new CourseACUSpanDtl();

            obj.Id = nullHandler.GetInt32(COURSEACUSPANDTLID);//0
            obj.CourseACUSpanMasID = nullHandler.GetInt32(COURSEACUSPANMASID);//1
            obj.CreditUnits = nullHandler.GetDecimal(CREDITUNITS);//2
            obj.CreatorID = nullHandler.GetInt32(CREATORID);//5
            obj.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);//6
            obj.ModifierID = nullHandler.GetInt32(MODIFIERID);//7
            obj.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);//8

            return obj;
        }
        private static CourseACUSpanDtl MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            CourseACUSpanDtl obj = null;
            if (theReader.Read())
            {
                obj = new CourseACUSpanDtl();
                obj = Mapper(nullHandler);
            }

            return obj;
        }
        private static List<CourseACUSpanDtl> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<CourseACUSpanDtl> collection = null;

            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new List<CourseACUSpanDtl>();
                }
                CourseACUSpanDtl obj = Mapper(nullHandler);
                collection.Add(obj);
            }

            return collection;
        }

        public static List<CourseACUSpanDtl> GetsByMas(int courseACUSpanMasID)
        {
            string command = SELECT
                            + "WHERE [" + COURSEACUSPANMASID + "] = " + COURSEACUSPANMASID_PA;


            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseACUSpanMasID, COURSEACUSPANMASID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            List<CourseACUSpanDtl> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }
        internal static List<CourseACUSpanDtl> GetsByMas(int courseACUSpanMasID, SqlConnection sqlConn)
        {
            string command = SELECT
                            + "WHERE [" + COURSEACUSPANMASID + "] = " + COURSEACUSPANMASID_PA;


            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseACUSpanMasID, COURSEACUSPANMASID_PA);

  
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            List<CourseACUSpanDtl> collection = MapCollection(theReader);
            theReader.Close();


            return collection;
        }

        public static CourseACUSpanDtl Get(int courseACUSpanDtlID)
        {
            string command = SELECT
                            + "WHERE ["+COURSEACUSPANDTLID+"] = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseACUSpanDtlID, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            CourseACUSpanDtl obj = MapClass(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }

        internal static int GetMaxID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newID = 0;

            string command = "SELECT MAX(" + COURSEACUSPANDTLID + ") FROM " + TABLENAME;
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

            if (ob == null || ob == DBNull.Value)
            {
                newID = 1;
            }
            else if (ob is Int32)
            {
                newID = Convert.ToInt32(ob) + 1;
            }

            return newID;
        }


        public static int Save(CourseACUSpanDtl program)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (program.Id == 0)
                {
                    program.Id = CourseACUSpanDtl.GetMaxID(sqlConn, sqlTran);
                    command = INSERT;
                    sqlParams = new SqlParameter[] { program.IDParam,  
                                                     program.CourseACUSpanMasIDParam,
                                                     program.CreditUnitsParam,
                                                     program.CreatorIDParam, 
                                                     program.CreatedDateParam, 
                                                     program.ModifierIDParam, 
                                                     program.ModifiedDateParam };
                }
                else
                {


                    command = UPDATE
                            + " WHERE [" + COURSEACUSPANDTLID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { program.CourseACUSpanMasIDParam,
                                                     program.CreditUnitsParam,//6
                                                     program.CreatorIDParam, //7
                                                     program.CreatedDateParam, //8
                                                     program.ModifierIDParam, //9
                                                     program.ModifiedDateParam, //10
                                                     program.IDParam };
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
        internal static int Save(CourseACUSpanDtl program, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;
                //SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                //SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                    program.Id = CourseACUSpanDtl.GetMaxID(sqlConn, sqlTran);
                    command = INSERT;
                    sqlParams = new SqlParameter[] { program.IDParam,  
                                                     program.CourseACUSpanMasIDParam,
                                                     program.CreditUnitsParam,
                                                     program.CreatorIDParam, 
                                                     program.CreatedDateParam, 
                                                     program.ModifierIDParam, 
                                                     program.ModifiedDateParam };


                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                //MSSqlConnectionHandler.CommitTransaction();
                //MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        public static int Delete(int courseACUSpanDtlID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = DELETE
                                + "WHERE ["+COURSEACUSPANDTLID+"] = " + ID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseACUSpanDtlID, ID_PA);
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
        internal static int Delete(int courseACUSpanDtlID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = DELETE
                                + "WHERE [" + COURSEACUSPANDTLID + "] = " + ID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseACUSpanDtlID, ID_PA);
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { iDParam });

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        internal static int DeleteByMaster(int courseACUSpanMasID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = DELETE
                                + "WHERE [" + COURSEACUSPANMASID + "] = " + COURSEACUSPANMASID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseACUSpanMasID, COURSEACUSPANMASID_PA);
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { iDParam });

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
