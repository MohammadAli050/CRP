using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLAcademicCalenderExamSchedulerRepository : IAcademicCalenderExamSchedulerRepository
    {
        Database db = null;
       // Database mydb = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase("connection string here");
        

        private string sqlInsert = "AcademicCalenderExamSchedulerInsert";
        private string sqlUpdate = "AcademicCalenderExamSchedulerUpdate";
        private string sqlDelete = "AcademicCalenderExamSchedulerDeleteById";
        private string sqlGetById = "AcademicCalenderExamSchedulerGetById";
        private string sqlGetAll = "AcademicCalenderExamSchedulerGetAll";
        
        
        public int Insert(AcademicCalenderExamScheduler academicCalenderExamScheduler)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, academicCalenderExamScheduler, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "AcademicCalenderExamSchedulerID");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out id);
                }
            }
            catch (Exception ex)
            {
                id = 0;
            }

            return id;
        }

        public bool Update(AcademicCalenderExamScheduler academicCalenderExamScheduler)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, academicCalenderExamScheduler, isInsert);

                int rowsAffected = db.ExecuteNonQuery(cmd);

                if (rowsAffected > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public bool Delete(int id)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlDelete);

                db.AddInParameter(cmd, "AcademicCalenderExamSchedulerID", DbType.Int32, id);
                int rowsAffected = db.ExecuteNonQuery(cmd);

                if (rowsAffected > 0)
                {
                    result = true;
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public AcademicCalenderExamScheduler GetById(int? id)
        {
            AcademicCalenderExamScheduler _academicCalenderExamScheduler = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalenderExamScheduler> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalenderExamScheduler>(sqlGetById, rowMapper);
                _academicCalenderExamScheduler = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _academicCalenderExamScheduler;
            }

            return _academicCalenderExamScheduler;
        }

        public List<AcademicCalenderExamScheduler> GetAll()
        {
            List<AcademicCalenderExamScheduler> academicCalenderExamSchedulerList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalenderExamScheduler> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalenderExamScheduler>(sqlGetAll, mapper);
                IEnumerable<AcademicCalenderExamScheduler> collection = accessor.Execute();

                academicCalenderExamSchedulerList = collection.ToList();
            }

            catch (Exception ex)
            {
                return academicCalenderExamSchedulerList;
            }

            return academicCalenderExamSchedulerList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, AcademicCalenderExamScheduler academicCalenderExamScheduler, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "AcademicCalenderExamSchedulerID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "AcademicCalenderExamSchedulerID", DbType.Int32, academicCalenderExamScheduler.AcademicCalenderExamSchedulerID);
            }

            db.AddInParameter(cmd, "AcaCal_SectionID", DbType.Int32, academicCalenderExamScheduler.AcaCal_SectionID);
            db.AddInParameter(cmd, "RoomInfoOneID", DbType.Int32, academicCalenderExamScheduler.RoomInfoOneID);
            db.AddInParameter(cmd, "DayOne", DbType.Int32, academicCalenderExamScheduler.DayOne);
            db.AddInParameter(cmd, "TimeSlotPlanOneID", DbType.Int32, academicCalenderExamScheduler.TimeSlotPlanOneID);
            db.AddInParameter(cmd, "TeacherOneID", DbType.Int32, academicCalenderExamScheduler.TeacherOneID);
            db.AddInParameter(cmd, "TeacherTwoID", DbType.Int32, academicCalenderExamScheduler.TeacherTwoID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, academicCalenderExamScheduler.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, academicCalenderExamScheduler.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, academicCalenderExamScheduler.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, academicCalenderExamScheduler.ModifiedDate);
            db.AddInParameter(cmd, "TypeDefinitionID", DbType.Int32, academicCalenderExamScheduler.TypeDefinitionID);
            db.AddInParameter(cmd, "Occupied", DbType.Int32, academicCalenderExamScheduler.Occupied);
            db.AddInParameter(cmd, "Date", DbType.DateTime, academicCalenderExamScheduler.Date);
            
            return db;
        }

        private IRowMapper<AcademicCalenderExamScheduler> GetMaper()
        {
            IRowMapper<AcademicCalenderExamScheduler> mapper = MapBuilder<AcademicCalenderExamScheduler>.MapAllProperties()
            .Map(m => m.AcademicCalenderExamSchedulerID).ToColumn("AcademicCalenderExamSchedulerID")
            .Map(m => m.AcaCal_SectionID).ToColumn("AcaCal_SectionID")
            .Map(m => m.RoomInfoOneID).ToColumn("RoomInfoOneID")
            .Map(m => m.DayOne).ToColumn("DayOne")
            .Map(m => m.TimeSlotPlanOneID).ToColumn("TimeSlotPlanOneID")
            .Map(m => m.TeacherOneID).ToColumn("TeacherOneID")
            .Map(m => m.TeacherTwoID).ToColumn("TeacherTwoID")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Map(m => m.TypeDefinitionID).ToColumn("TypeDefinitionID")
            .Map(m => m.Occupied).ToColumn("Occupied")
            .Map(m => m.Date).ToColumn("Date")
            
            .Build();

            return mapper;
        }
        #endregion
    }
}
