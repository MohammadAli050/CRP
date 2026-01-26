using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject.BasicSetup
{
    public class CampusBuilding : Base
    {
        private const string ALLCOLUMNS = "[BuildingId], "
                                          + "[BuildingName], "
                                          + "[CampusId], ";

        private const string TABLENAME = " [Building] ";

        private const string SELECT = "SELECT "
                           + ALLCOLUMNS
                           + BASECOLUMNS
                           + "FROM" + TABLENAME;



        private string _buildingName;
        private int _campusId;


        private const string BUILDINGNAME = "BuildingName";
        private const string BUILDINGNAME_PA = "@BuildingName";

        private const string CAMPUSID = "CampusId";
        private const string CAMPUSID_PA = "@CampusId";

        public string BuildingName
        {
            get { return _buildingName; }
            set { _buildingName = value; }
        }
        private SqlParameter BuildingNameParam
        {
            get
            {
                SqlParameter nameParam = new SqlParameter();
                nameParam.ParameterName = BUILDINGNAME_PA;

                nameParam.Value = BuildingName;

                return nameParam;
            }
        }

        public int CampusId
        {
            get { return _campusId; }
            set { _campusId = value; }
        }
        private SqlParameter CampusIdParam
        {
            get
            {
                SqlParameter nameParam = new SqlParameter();
                nameParam.ParameterName = CAMPUSID_PA;

                nameParam.Value = CampusId;

                return nameParam;
            }
        }

        public static List<CampusBuilding> GetCampusBuilding()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<CampusBuilding> campusBuilding = MapCampusBuilding(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return campusBuilding;
        }

        public static List<CampusBuilding> GetCampusBuildingByCampusId(int campusId)
        {
            string command = SELECT + "WHERE CampusId = " + campusId; ;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<CampusBuilding> campusBuilding = MapCampusBuilding(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return campusBuilding;
        }

        private static List<CampusBuilding> MapCampusBuilding(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<CampusBuilding> campusBuilding = null;

            while (theReader.Read())
            {
                if (campusBuilding == null)
                {
                    campusBuilding = new List<CampusBuilding>();
                }
                CampusBuilding campusBuildingObj = CampusBuildingMapper(nullHandler);
                campusBuilding.Add(campusBuildingObj);
            }

            return campusBuilding;
        }

        private static CampusBuilding CampusBuildingMapper(SQLNullHandler nullHandler)
        {
            CampusBuilding campusBuilding = new CampusBuilding();

            campusBuilding.Id = nullHandler.GetInt32("BuildingId");
            campusBuilding.BuildingName = nullHandler.GetString("BuildingName");
            campusBuilding.CampusId = nullHandler.GetInt32("CampusId");
            campusBuilding.CreatorID = nullHandler.GetInt32("CreatedBy");
            campusBuilding.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            campusBuilding.ModifierID = nullHandler.GetInt32("ModifiedBy");
            campusBuilding.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            return campusBuilding;
        }
    }
}
