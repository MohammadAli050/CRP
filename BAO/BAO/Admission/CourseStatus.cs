using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class CourseStatus : Base
    {
        #region DB COlumns
        /*
         CourseStatusID	int	        Unchecked
         Code	        varchar(2)	Unchecked
         Description	varchar(50)	Unchecked
        */
        #endregion

        #region Variables
        private string _code;
        private string _description;
        private int _courseStatusID; 
        #endregion

        #region Constructors
        public CourseStatus()
        {
            _code = string.Empty;
            _description = string.Empty;
            _courseStatusID = 0;
        } 
        #endregion

        #region Constants
        #region Columns

        private const string COURSESTATUSID = "CourseStatusID";
        private const string COURSESTATUSID_PA = "@CourseStatusID";

        private const string CODE = "Code";
        private const string CODE_PA = "@Code";

        private const string DESCRIPTION = "Description";
        private const string DESCRIPTION_PA = "@Description";
        #endregion

        #region All-Columns
        private const string ALLCOLUMNS = COURSESTATUSID + ", "
                                + CODE + ", "
                                + DESCRIPTION + ", ";
        #endregion

        #region NoPK-Columns
        private const string NOPKCOLUMNS = CODE + ", "
                                + DESCRIPTION + ", ";
        #endregion

        private const string TABLENAME = " [CourseStatus] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        #region Insert
        private const string INSERT = "INSERT INTO" + TABLENAME
                     + "("
                     + ALLCOLUMNS
                     + ")"
                     + "VALUES ( "
                     + COURSESTATUSID_PA + ", "
                     + CODE_PA + ", "
                     + DESCRIPTION_PA + ")";
        #endregion

        #region Update
        private const string UPDATE = "UPDATE" + TABLENAME + "SET "
                             + CODE + " = " + CODE_PA + ", "
                             + DESCRIPTION + " = " + DESCRIPTION_PA;
        #endregion

        #endregion

        #region Properties
        public string Code
        {
            get
            {
                return this._code;
            }
            set
            {

                this._code = value;

            }
        }
        private SqlParameter CodeParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = CODE_PA;
                sqlParam.Value = _code;
                return sqlParam;
            }
        }

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }
        private SqlParameter DescriptionParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = DESCRIPTION_PA;
                sqlParam.Value = _description;
                return sqlParam;
            }
        }

        public int CourseStatusID
        {
            get { return _courseStatusID; }
            set { _courseStatusID = value; }
        }
        private SqlParameter OperatorIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = COURSESTATUSID_PA;
                sqlParam.Value = _courseStatusID;
                return sqlParam;
            }
        } 
        #endregion

        #region Methods
        private static CourseStatus Mapper(SQLNullHandler nullHandler)
        {
            CourseStatus obj = new CourseStatus();

            obj.CourseStatusID = nullHandler.GetInt32(COURSESTATUSID);
            obj.Code = nullHandler.GetString(CODE);
            obj.Description = nullHandler.GetString(DESCRIPTION);

            return obj;
        }
        private static CourseStatus MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            CourseStatus obj = null;
            if (theReader.Read())
            {
                obj = new CourseStatus();
                obj = Mapper(nullHandler);
            }

            return obj;
        }
        private static List<CourseStatus> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<CourseStatus> collection = null;

            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new List<CourseStatus>();
                }
                CourseStatus obj = Mapper(nullHandler);
                collection.Add(obj);
            }

            return collection;
        }

        public static List<CourseStatus> Gets()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<CourseStatus> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }

        public static CourseStatus Get(int courseStatusID)
        {
            string command = SELECT
                            + "WHERE " + COURSESTATUSID + " = " + COURSESTATUSID_PA;

            SqlParameter ownerNodeIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseStatusID, COURSESTATUSID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { ownerNodeIDParam });

            CourseStatus obj = MapClass(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }
        #endregion
    }
}
