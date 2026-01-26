using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;

namespace DataAccess
{
    public class Section_DAO
    {
        private const string ACADEMICCALENDERID_PA = "@AcademicCalenderID";
        private const string PROGRAMID_PA = "@ProgramID";
        private const string DEPTID_PA = "@DeptID";
        private const string COURSEID_PA = "@CourseID";
        private const string VERSIONID_PA = "@VersionID";
        private const string ACACAL_SECTIONID_PA = "@AcaCal_SectionID";
        private const string ID_PA = "@ID";

        private const string ACACAL_SECTIONID = "AcaCal_SectionID";
        private const string SECTIONNAME = "SectionName";
        private const string TIMESLOT_1 = "TimeSlot_1";
        private const string DAYONE = "DayOne";
        private const string TIMESLOT_2 = "TimeSlot_2";
        private const string DAYTWO = "DayTwo";
        private const string FACULTY_1 = "Faculty_1";
        private const string FACULTY_2 = "Faculty_2";
        private const string ROOMNO_1 = "RoomNo_1";
        private const string ROOMNO_2 = "RoomNo_2";
        private const string CAPACITY = "Capacity";
        private const string OCCUPIED = "Occupied";

        public static List<SectionEntity> GetSections(int acId, int deptId, int proId, int couId, int verId)
        {
            List<SectionEntity> entities = new List<SectionEntity>();

            try
            {
                entities = null;                
                string cmd = @" DECLARE	@return_value int 
                            EXEC	@return_value = sp_SelectSection 
                            @AcademicCalenderID = @AcademicCalenderID,
                            @ProgramID = @ProgramID,
                            @DeptID = @DeptID,
                            @CourseID = @CourseID,
                            @VersionID = @VersionID
                            SELECT	'Return Value' = @return_value ";

                DAOParameters dps = new DAOParameters();
                dps.AddParameter(ACADEMICCALENDERID_PA, acId);
                dps.AddParameter(PROGRAMID_PA, proId);
                dps.AddParameter(DEPTID_PA, deptId);
                dps.AddParameter(COURSEID_PA, couId);
                dps.AddParameter(VERSIONID_PA, verId);

                List<SqlParameter> ps = Methods.GetSQLParameters(dps);

                SqlDataReader rd = QueryHandler.ExecuteSelectQuery(cmd, ps);
                entities = MapEntities(rd);
                rd.Close();
            }
            catch (Exception ex)
            {
                //FixMe
                throw ex;
            }
            return entities;
        }

        private static List<SectionEntity> MapEntities(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            List<SectionEntity> entities = new List<SectionEntity>();
            while (theReader.Read())
            {
                SectionEntity entity = new SectionEntity();
                entity = Mapper(nullHandler);
                entities.Add(entity);
            }
            return entities;
        }

        private static SectionEntity Mapper(SQLNullHandler nullHandler)
        {
            SectionEntity entity = new SectionEntity();

            entity.Id = nullHandler.GetInt32(ACACAL_SECTIONID);
            entity.SectionName = nullHandler.GetString(SECTIONNAME);
            entity.TimeSlot_1 = nullHandler.GetString(TIMESLOT_1);
            entity.DayOne = nullHandler.GetString(DAYONE);
            entity.TimeSlot_2 = nullHandler.GetString(TIMESLOT_2);
            entity.DayTwo = nullHandler.GetString(DAYTWO);
            entity.Faculty_1 = nullHandler.GetString(FACULTY_1);
            entity.Faculty_2 = nullHandler.GetString(FACULTY_2);
            entity.RoomNo_1 = nullHandler.GetString(ROOMNO_1);
            entity.RoomNo_2 = nullHandler.GetString(ROOMNO_2);
            entity.Capacity = nullHandler.GetInt32(CAPACITY);
            entity.Occupied = nullHandler.GetInt32(OCCUPIED);
            

            return entity;
        }

        internal static int IncreaseOccupied(int acaCalSecId, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                DAOParameters dParam = new DAOParameters();

                string command = @"update AcademicCalenderSection set Occupied =(select (isnull(Occupied,0)+1)as 'Occupied' from AcademicCalenderSection where AcaCal_SectionID = " + ACACAL_SECTIONID_PA + ") where AcaCal_SectionID = " + ACACAL_SECTIONID_PA + "";

                dParam.AddParameter(ACACAL_SECTIONID_PA, acaCalSecId);

                List<SqlParameter> sqlParams = Methods.GetSQLParameters(dParam);

                int i = QueryHandler.ExecuteSaveBatchAction(command, sqlParams, sqlConn, sqlTran);

                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static int DecreaseIfOccupied(int sccpnId, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
           try
            {
                DAOParameters dParam = new DAOParameters();

                string command = @"update AcademicCalenderSection set Occupied =(select (isnull(Occupied,0)-1)as 'Occupied' from AcademicCalenderSection where AcaCal_SectionID = (select AcaCal_SectionID from StudentCalCourseProgNode where ID = " + ID_PA + "))where AcaCal_SectionID =  (select AcaCal_SectionID from StudentCalCourseProgNode where ID = " + ID_PA + ")";

                dParam.AddParameter(ID_PA, sccpnId);

                List<SqlParameter> sqlParams = Methods.GetSQLParameters(dParam);

                int i = QueryHandler.ExecuteSaveBatchAction(command, sqlParams, sqlConn, sqlTran);

                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }

        public static int ChkVacant(int sectionId)
        {
            try
            {
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = @"select Capacity - Occupied as 'vacant'  from AcademicCalenderSection  where AcaCal_SectionID = " + sectionId;

                int i = QueryHandler.ExecuteSaveBatchsScalar(command, sqlConn, sqlTran);

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection(); 

                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
    }
}
