using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    [Table(Name = "SkillType")]
    public class SkillType
    {

        #region DB Columns
        /*
        SkillTypeID	    int	            Unchecked
        TypeDescription	varchar(200)	Unchecked
        */
        #endregion

        #region Variables
        private int _skillTypeID;
        private string _typeDescription; 
        #endregion

        #region Constructors
        public SkillType()
        {
            _typeDescription = string.Empty;
            _skillTypeID = 0;
        } 
        #endregion

        #region Constants
        #region Columns

        private const string SKILLTYPEID = "SkillTypeID";
        private const string SKILLTYPEID_PA = "@SkillTypeID";

        private const string TYPEDESCRIPTION = "TypeDescription";
        private const string TYPEDESCRIPTION_PA = "@TypeDescription";
        #endregion

        #region All-Columns
        private const string ALLCOLUMNS = SKILLTYPEID + ", "
                                + TYPEDESCRIPTION;
        #endregion

        #region NoPK-Columns
        private const string NOPKCOLUMNS = TYPEDESCRIPTION;
        #endregion

        private const string TABLENAME = " [SkillType] ";

        #region Select
        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                    + "FROM" + TABLENAME; 
        #endregion

        #region Insert
        private const string INSERT = "INSERT INTO" + TABLENAME
                     + "("
                     + NOPKCOLUMNS
                     + ")"
                     + "VALUES ( "
                     + TYPEDESCRIPTION_PA + ")";
        #endregion

        #region Update
        private const string UPDATE = "UPDATE" + TABLENAME + "SET "
                             + TYPEDESCRIPTION + " = " + TYPEDESCRIPTION_PA;
        #endregion

        #endregion

        #region Properties
        [Column(Name = "SkillTypeID")]
        public int SkillTypeID
        {
            get
            {
                return this._skillTypeID;
            }
            set
            {
                this._skillTypeID = value;
            }
        }

        private SqlParameter SkillTypeIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = SKILLTYPEID_PA;
                sqlParam.Value = _skillTypeID;
                return sqlParam;
            }
        }

        [Column(Name = "TypeDescription")]
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
        private static SkillType Mapper(SQLNullHandler nullHandler)
        {
            SkillType obj = new SkillType();

            obj.SkillTypeID = nullHandler.GetInt32(SKILLTYPEID);
            obj.TypeDescription = nullHandler.GetString(TYPEDESCRIPTION);

            return obj;
        }
        private static SkillType MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            SkillType obj = null;
            if (theReader.Read())
            {
                obj = new SkillType();
                obj = Mapper(nullHandler);
            }

            return obj;
        }
        private static List<SkillType> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<SkillType> collection = null;

            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new List<SkillType>();
                }
                SkillType obj = Mapper(nullHandler);
                collection.Add(obj);
            }

            return collection;
        }

        public static List<SkillType> Gets()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<SkillType> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }
        public static List<SkillType> GetsLinqWay()
        {
            //string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            var db = new DataContext(sqlConn);

            Table<SkillType> SkillTypes = db.GetTable<SkillType>();

            
            //SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<SkillType> collection = SkillTypes.ToList<SkillType>();
            //theReader.Close();

            //MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }

        public static SkillType Get(int skillTypeID)
        {
            string command = SELECT
                            + "WHERE " + SKILLTYPEID + " = " + SKILLTYPEID_PA;

            SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(skillTypeID, SKILLTYPEID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });

            SkillType obj = MapClass(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }
        public static SkillType GetLinqWay(int skillTypeID)
        {
            SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(skillTypeID, SKILLTYPEID_PA);
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            var db = new DataContext(sqlConn);

            IEnumerable<SkillType> SkillTypes = db.ExecuteQuery<SkillType>(SELECT+ "WHERE " + SKILLTYPEID + " = " + SKILLTYPEID_PA,new SqlParameter[] { sqlParam });

            return SkillTypes.Single<SkillType>();
        }

        public static int Save(SkillType obj)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (obj.SkillTypeID == 0)
                {
                    #region Insert
                    command = INSERT;
                    sqlParams = new SqlParameter[] { obj.TypeDescriptionParam };//+ MOIDFIEDDATE_PA + ")";//15 
                    #endregion
                }
                else
                {

                    #region Update
                    command = UPDATE
                    + " WHERE [" + SKILLTYPEID + "] = " + SKILLTYPEID_PA;
                    sqlParams = new SqlParameter[] { obj.TypeDescriptionParam,   //+ MOIDFIEDDATE_PA + ")";//15 
                                                     obj.SkillTypeIDParam };
                    #endregion
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteActionLTM(command, sqlConn, sqlParams);

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
