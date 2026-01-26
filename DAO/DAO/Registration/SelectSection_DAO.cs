using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;

namespace DataAccess
{
    public class SelectSection_DAO
    {
        private const string ACADEMICCALENDERID_PA = "@AcademicCalenderID";
        private const string PROGRAMID_PA = "@ProgramID";
        private const string DEPTID_PA = "@DeptID";
        private const string COURSEID_PA = "@CourseID";
        private const string VERSIONID_PA = "@VersionID";

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

        public static List<SelectSectionEntity> GetSections(int acId, int deptId, int proId, int couId, int verId)
        {
            List<SelectSectionEntity> entities = new List<SelectSectionEntity>();

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

                SqlDataReader rd = QueryHandler.ExecuteSelectBatchQuery(cmd, ps);
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

        private static List<SelectSectionEntity> MapEntities(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            List<SelectSectionEntity> entities = new List<SelectSectionEntity>();
            while (theReader.Read())
            {
                SelectSectionEntity entity = new SelectSectionEntity();
                entity = Mapper(nullHandler);
                entities.Add(entity);
            }
            return entities;
        }

        private static SelectSectionEntity Mapper(SQLNullHandler nullHandler)
        {
            SelectSectionEntity entity = new SelectSectionEntity();

            entity.AcaCal_SectionID = nullHandler.GetInt32(ACACAL_SECTIONID);
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
    }
}
