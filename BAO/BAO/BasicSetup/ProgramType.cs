using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class ProgramType
    {
        #region Variables
        private int _programTypeID;
        private string _typeDescription;
        #endregion

        #region Constants
        private const string SELECT = "SELECT "
                            + "[ProgramTypeID], "
                            + "[TypeDescription] "
                            + "FROM [ProgramType] ";

        private const string DELETE = "DELETE FROM [ProgramType] ";
        #endregion

        #region Constructor
        public ProgramType()
        {
            _typeDescription = string.Empty;
        }
        #endregion

        #region Properties
        public int ProgramTypeID
        {
            get { return _programTypeID; }
            set { _programTypeID = value; }
        }

        public string TypeDescription
        {
            get { return _typeDescription; }
            set { _typeDescription = value; }
        }
        #endregion

        #region Methods
        private static ProgramType ProgramTypeMapper(SQLNullHandler nullHandler)
        {
            ProgramType opertor = new ProgramType();

            opertor.ProgramTypeID = nullHandler.GetInt32("ProgramTypeID");
            opertor.TypeDescription = nullHandler.GetString("TypeDescription");

            return opertor;
        }
        private static List<ProgramType> MapProgramTypes(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<ProgramType> operators = null;

            while (theReader.Read())
            {
                if (operators == null)
                {
                    operators = new List<ProgramType>();
                }
                ProgramType opertor = ProgramTypeMapper(nullHandler);
                operators.Add(opertor);
            }

            return operators;
        }
        private static ProgramType MapProgramType(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            ProgramType opertor = null;
            if (theReader.Read())
            {
                opertor = new ProgramType();
                opertor = ProgramTypeMapper(nullHandler);
            }

            return opertor;
        }

        public static List<ProgramType> GetProgramTypes()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<ProgramType> operators = MapProgramTypes(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return operators;
        }

        public static ProgramType GetProgramType(int programTypeID)
        {
            string command = SELECT
                            + "WHERE ProgramTypeID = @ProgramTypeID";

            SqlParameter ownerNodeIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(programTypeID, "@ProgramTypeID");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { ownerNodeIDParam });

            ProgramType opertor = MapProgramType(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return opertor;
        }
        #endregion
    }
}
