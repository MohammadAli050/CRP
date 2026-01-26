using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLExamRoutineRepository : IExamRoutineRepository
    {
        Database db = null;

        private string sqlInsert = "ExamRoutineInsert";
        private string sqlUpdate = "ExamRoutineUpdate";
        private string sqlDelete = "ExamRoutineDeleteById";
        private string sqlGetById = "ExamRoutineGetById";
        private string sqlGetAll = "ExamRoutineGetAll";

        public int Insert(ExamRoutine examRoutine)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, examRoutine, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ExamRoutineID");

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

        public bool Update(ExamRoutine examRoutine)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, examRoutine, isInsert);

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

                db.AddInParameter(cmd, "ExamRoutineID", DbType.Int32, id);
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

        public ExamRoutine GetById(int id)
        {
            ExamRoutine _examRoutine = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamRoutine> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamRoutine>(sqlGetById, rowMapper);
                _examRoutine = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examRoutine;
            }

            return _examRoutine;
        }

        public List<ExamRoutine> GetAll()
        {
            List<ExamRoutine> examRoutineList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamRoutine> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamRoutine>(sqlGetAll, mapper);
                IEnumerable<ExamRoutine> collection = accessor.Execute();

                examRoutineList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examRoutineList;
            }

            return examRoutineList;
        }

        public List<rExamRoutine> GetExamRoutine(int acaCalId, int examScheduleSetId)
        {
            List<rExamRoutine> examRoutineList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rExamRoutine> mapper = GetExamRoutineMaper();

                var accessor = db.CreateSprocAccessor<rExamRoutine>("RptExamRoutine", mapper);
                IEnumerable<rExamRoutine> collection = accessor.Execute(acaCalId, examScheduleSetId);

                examRoutineList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examRoutineList;
            }

            return examRoutineList;
        }

        public List<InvigilationSchedule> GetInvigilationScheduleByAcaCalIdExamSetId(int acaCalId, int examScheduleSetId)
        {
            List<InvigilationSchedule> examRoutineList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<InvigilationSchedule> mapper = GetInvigilationScheduleMaper();

                var accessor = db.CreateSprocAccessor<InvigilationSchedule>("InvigilationScheduleByAcaCalIdExamSetID", mapper);
                IEnumerable<InvigilationSchedule> collection = accessor.Execute(acaCalId, examScheduleSetId);

                examRoutineList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examRoutineList;
            }

            return examRoutineList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamRoutine examRoutine, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ExamRoutineID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ExamRoutineID", DbType.Int32, examRoutine.ExamRoutineID);
            }

            db.AddInParameter(cmd, "AcaCal_SectionID", DbType.Int32, examRoutine.AcaCal_SectionID);
            db.AddInParameter(cmd, "RoomInfoID", DbType.Int32, examRoutine.RoomInfoID);
            db.AddInParameter(cmd, "TimeSlotPlanID", DbType.Int32, examRoutine.TimeSlotPlanID);
            db.AddInParameter(cmd, "ExamDate", DbType.DateTime, examRoutine.ExamDate);
            db.AddInParameter(cmd, "TeacherID1", DbType.Int32, examRoutine.TeacherID1);
            db.AddInParameter(cmd, "TeacherID2", DbType.Int32, examRoutine.TeacherID2);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, examRoutine.ProgramID);
            db.AddInParameter(cmd, "ExamTypeID", DbType.Int32, examRoutine.ExamTypeID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, examRoutine.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, examRoutine.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, examRoutine.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, examRoutine.ModifiedDate);
            return db;
        }

        private IRowMapper<ExamRoutine> GetMaper()
        {
            IRowMapper<ExamRoutine> mapper = MapBuilder<ExamRoutine>.MapAllProperties()
            .Map(m => m.ExamRoutineID).ToColumn("ExamRoutineID")
            .Map(m => m.AcaCal_SectionID).ToColumn("AcaCal_SectionID")
            .Map(m => m.RoomInfoID).ToColumn("RoomInfoID")
            .Map(m => m.TimeSlotPlanID).ToColumn("TimeSlotPlanID")
            .Map(m => m.ExamDate).ToColumn("ExamDate")
            .Map(m => m.TeacherID1).ToColumn("TeacherID1")
            .Map(m => m.TeacherID2).ToColumn("TeacherID2")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.ExamTypeID).ToColumn("ExamTypeID")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .DoNotMap(m => m.TeacherInfoOne)
            .DoNotMap(m => m.TeacherInfoTwo)
            .DoNotMap(m => m.TimeSlotPlanInfo)
            .DoNotMap(m => m.RoomInfo)
            .DoNotMap(m => m.CourseCode)
            .DoNotMap(m => m.CourseTitle)
            .DoNotMap(m => m.Section)

            .Build();
            //Mapper @sajib
            return mapper;
        }

        private IRowMapper<rExamRoutine> GetExamRoutineMaper()
        {
            IRowMapper<rExamRoutine> mapper = MapBuilder<rExamRoutine>.MapAllProperties()
            .Map(m => m.AcaCalId).ToColumn("AcaCalId")
            .Map(m => m.SetName).ToColumn("SetName")
            .Map(m => m.ShortName).ToColumn("ShortName")
            .Map(m => m.DayDate).ToColumn("DayDate")
            .Map(m => m.DayNo).ToColumn("DayNo")
            .Map(m => m.DayId).ToColumn("DayId")
            .Map(m => m.WName).ToColumn("WName")
            .Map(m => m.TimeSlotNo).ToColumn("TimeSlotNo")
            .Map(m => m.Time).ToColumn("Time")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.Title).ToColumn("Title")

            .Build();
            
            return mapper;
        }

        private IRowMapper<InvigilationSchedule> GetInvigilationScheduleMaper()
        {
            IRowMapper<InvigilationSchedule> mapper = MapBuilder<InvigilationSchedule>.MapAllProperties()
            .Map(m => m.Serial).ToColumn("Serial")
            .Map(m => m.Date).ToColumn("Date")
            .Map(m => m.DayName).ToColumn("DayName")
            .Map(m => m.EndTime).ToColumn("EndTime")
            .Map(m => m.RoomName).ToColumn("RoomName")
            .Map(m => m.Slot).ToColumn("Slot")
            .Map(m => m.SlotNo).ToColumn("SlotNo")
            .Map(m => m.StartTime).ToColumn("StartTime")

            .Build();

            return mapper;
        }

        #endregion
    }
}
