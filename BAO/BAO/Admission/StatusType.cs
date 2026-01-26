using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class StatusType
    {
        #region DBColumns
        /*
        StatusTypeID	int	            Unchecked
        TypeDescription	varchar(200)	Unchecked
        */
        #endregion

        #region Variables
        private int _statusTypeID;
        private string _typeDescription; 
        #endregion

        #region Constructors
        public StatusType()
        {
            _typeDescription = string.Empty;
            _statusTypeID = 0;
        } 
        #endregion

        #region Constants
        #region Columns

        private const string STATUSTYPEID = "StatusTypeID";
        private const string STATUSTYPEID_PA = "@StatusTypeID";

        private const string TYPEDESCRIPTION = "TypeDescription";
        private const string TYPEDESCRIPTION_PA = "@TypeDescription";
        #endregion

        #region All-Columns
        private const string ALLCOLUMNS = STATUSTYPEID + ", "
                                + TYPEDESCRIPTION;
        #endregion

        #region NoPK-Columns
        private const string NOPKCOLUMNS = TYPEDESCRIPTION;
        #endregion

        private const string TABLENAME = " [StatusType] ";

        #region Select
        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                    + "FROM" + TABLENAME; 
        #endregion

        #region Insert
        private const string INSERT = "INSERT INTO" + TABLENAME
                     + "("
                     + ALLCOLUMNS
                     + ")"
                     + "VALUES ( "
                     + STATUSTYPEID_PA + ", "
                     + TYPEDESCRIPTION_PA + ")";
        #endregion

        #region Update
        private const string UPDATE = "UPDATE" + TABLENAME + "SET "
                             + STATUSTYPEID + " = " + STATUSTYPEID_PA + ", "
                             + TYPEDESCRIPTION + " = " + TYPEDESCRIPTION_PA;
        #endregion

        #endregion

        #region Properties
        public int StatusTypeID
        {
            get
            {
                return this._statusTypeID;
            }
            set
            {
                this._statusTypeID = value;
            }
        }
        private SqlParameter StatusTypeIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = STATUSTYPEID_PA;
                sqlParam.Value = _statusTypeID;
                return sqlParam;
            }
        }

        public string TypeDescription
        {
            get
            {
                return this._typeDescription;
            }
            set
            {
                this._typeDescription = value;
            }
        }
        private SqlParameter TypeDescriptionParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = TYPEDESCRIPTION_PA;
                sqlParam.Value = _typeDescription;
                return sqlParam;
            }
        } 
        #endregion

        #region Methods
        private static StatusType Mapper(SQLNullHandler nullHandler)
        {
            StatusType obj = new StatusType();

            obj.StatusTypeID = nullHandler.GetInt32(STATUSTYPEID);
            obj.TypeDescription = nullHandler.GetString(TYPEDESCRIPTION);

            return obj;
        }
        private static StatusType MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            StatusType obj = null;
            if (theReader.Read())
            {
                obj = new StatusType();
                obj = Mapper(nullHandler);
            }

            return obj;
        }
        private static List<StatusType> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<StatusType> collection = null;

            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new List<StatusType>();
                }
                StatusType obj = Mapper(nullHandler);
                collection.Add(obj);
            }

            return collection;
        }

        public static List<StatusType> Gets()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<StatusType> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }

        public static StatusType Get(int statusTypeID)
        {
            string command = SELECT
                            + "WHERE " + STATUSTYPEID + " = " + STATUSTYPEID_PA;

            SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(statusTypeID, STATUSTYPEID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });

            StatusType obj = MapClass(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }
        #endregion
    }
}
