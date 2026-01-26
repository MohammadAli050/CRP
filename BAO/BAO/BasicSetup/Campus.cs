using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject.BasicSetup
{
    public class Campus : Base
    {
        private const string ALLCOLUMNS = "[CampusId], "
                                          +"[CampusName], ";

        private const string TABLENAME = " [Campus] ";

        private const string SELECT = "SELECT "
                           + ALLCOLUMNS
                           + BASECOLUMNS
                           + "FROM" + TABLENAME;

        

        private string _campusName;


        private const string NAME = "CampusName";
        private const string NAME_PA = "@CampusName";

        public string CampusName
        {
            get { return _campusName; }
            set { _campusName = value; }
        }
        private SqlParameter NameParam
        {
            get
            {
                SqlParameter nameParam = new SqlParameter();
                nameParam.ParameterName = NAME_PA;

                nameParam.Value = CampusName;

                return nameParam;
            }
        }

        public static List<Campus> GetCampus()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<Campus> campus = MapCampus(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return campus;
        }

        private static List<Campus> MapCampus(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Campus> campus = null;

            while (theReader.Read())
            {
                if (campus == null)
                {
                    campus = new List<Campus>();
                }
                Campus campusObj = CampusMapper(nullHandler);
                campus.Add(campusObj);
            }

            return campus;
        }

        private static Campus CampusMapper(SQLNullHandler nullHandler)
        {
            Campus campus = new Campus();

            campus.Id = nullHandler.GetInt32("CampusId");
            campus.CampusName = nullHandler.GetString("CampusName");
            campus.CreatorID = nullHandler.GetInt32("CreatedBy");
            campus.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            campus.ModifierID = nullHandler.GetInt32("ModifiedBy");
            campus.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            return campus;
        }
    }
}
