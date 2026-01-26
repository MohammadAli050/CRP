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
    public partial class SQLClassRoutineRepository : IClassRoutineRepository
    {
        Database db = null;

        private string sqlInsert = "ClassRoutineInsert";
        private string sqlUpdate = "ClassRoutineUpdate";
        private string sqlDelete = "ClassRoutineDeleteById";
        private string sqlGetById = "ClassRoutineGetById";
        private string sqlGetAll = "ClassRoutineGetAll";
        private string sqlClassRoutine = "RptClassRoutineByProgram";
        
        
        
        public int Insert(ClassRoutine classRoutine)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, classRoutine, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ClassRoutineID");

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

        public bool Update(ClassRoutine classRoutine)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, classRoutine, isInsert);

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

                db.AddInParameter(cmd, "ClassRoutineID", DbType.Int32, id);
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

        public ClassRoutine GetById(int? id)
        {
            ClassRoutine _classRoutine = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ClassRoutine> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ClassRoutine>(sqlGetById, rowMapper);
                _classRoutine = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _classRoutine;
            }

            return _classRoutine;
        }

        public List<ClassRoutine> GetAll()
        {
            List<ClassRoutine> classRoutineList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ClassRoutine> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ClassRoutine>(sqlGetAll, mapper);
                IEnumerable<ClassRoutine> collection = accessor.Execute();

                classRoutineList = collection.ToList();
            }

            catch (Exception ex)
            {
                return classRoutineList;
            }

            return classRoutineList;
        }

        public List<rClassRoutineByProgram> GetClassRoutineByProgramAndAcaCalId(int programID, int acaCalID)
        {
            List<rClassRoutineByProgram> classRoutineList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rClassRoutineByProgram> mapper = GetClassRoutineMaper();

                var accessor = db.CreateSprocAccessor<rClassRoutineByProgram>(sqlClassRoutine, mapper);
                IEnumerable<rClassRoutineByProgram> collection = accessor.Execute(programID, acaCalID);

                classRoutineList = collection.ToList();
            }

            catch (Exception ex)
            {
                return classRoutineList;
            }

            return classRoutineList;
        }

        public List<rClassScheduleForFaculty> GetClassScheduleForFaculty(int facultyId, int programId, int sessionId)
        {
            List<rClassScheduleForFaculty> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rClassScheduleForFaculty> mapper = GetClassScheduleForFaculty();

                var accessor = db.CreateSprocAccessor<rClassScheduleForFaculty>("RptClassScheduleForFaculty", mapper);
                IEnumerable<rClassScheduleForFaculty> collection = accessor.Execute(facultyId, programId, sessionId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ClassRoutine classRoutine, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ClassRoutineID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ClassRoutineID", DbType.Int32, classRoutine.ClassRoutineID);
            }

            db.AddInParameter(cmd, "AcaCal_CourseID", DbType.Int32, classRoutine.AcaCal_CourseID);
            db.AddInParameter(cmd, "Section", DbType.String, classRoutine.Section);
            db.AddInParameter(cmd, "Capacity", DbType.String, classRoutine.Capacity);
            db.AddInParameter(cmd, "RoomInfoID", DbType.Int32, classRoutine.RoomInfoID);
            db.AddInParameter(cmd, "TimeSlotPlanID", DbType.Int32, classRoutine.TimeSlotPlanID);
            db.AddInParameter(cmd, "Day", DbType.String, classRoutine.Day);
            db.AddInParameter(cmd, "TeacherID", DbType.Int32, classRoutine.TeacherID);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, classRoutine.ProgramID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, classRoutine.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, classRoutine.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, classRoutine.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, classRoutine.ModifiedDate);
           
            return db;
        }

        private IRowMapper<ClassRoutine> GetMaper()
        {
            IRowMapper<ClassRoutine> mapper = MapBuilder<ClassRoutine>.MapAllProperties()
            .Map(m => m.ClassRoutineID).ToColumn("ClassRoutineID")
            .Map(m => m.AcaCal_CourseID).ToColumn("AcaCal_CourseID")
            .Map(m => m.Section).ToColumn("Section")
            .Map(m => m.Capacity).ToColumn("Capacity")
            .Map(m => m.RoomInfoID).ToColumn("RoomInfoID")
            .Map(m => m.TimeSlotPlanID).ToColumn("TimeSlotPlanID")
            .Map(m => m.Day).ToColumn("Day")
            .Map(m => m.TeacherID).ToColumn("TeacherID")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            
            .Build();

            return mapper;
        }

        private IRowMapper<rClassRoutineByProgram> GetClassRoutineMaper()
        {
            IRowMapper<rClassRoutineByProgram> mapper = MapBuilder<rClassRoutineByProgram>.MapAllProperties()
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.DetailedName).ToColumn("DetailedName")
            .Map(m => m.ShortName).ToColumn("ShortName")
            .Map(m => m.AcademicCalenderID).ToColumn("AcademicCalenderID")
            .Map(m => m.TypeName).ToColumn("TypeName")
            .Map(m => m.Year).ToColumn("Year")
            .Map(m => m.StartDate).ToColumn("StartDate")
            .Map(m => m.WName).ToColumn("WName")
            .Map(m => m.RoomName).ToColumn("RoomName")
            .Map(m => m.TimeSlot).ToColumn("TimeSlot")
            .Map(m => m.Time).ToColumn("Time")
            .Map(m => m.Day).ToColumn("Day")  
            .Map(m => m.SectionName).ToColumn("SectionName")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.Room).ToColumn("Room")
            .Map(m => m.Code).ToColumn("Code")
            

            .Build();

            return mapper;
        }

        private IRowMapper<rClassScheduleForFaculty> GetClassScheduleForFaculty()
        {
            IRowMapper<rClassScheduleForFaculty> mapper = MapBuilder<rClassScheduleForFaculty>.MapAllProperties()
            .Map(m => m.ValueID).ToColumn("ValueID")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.Title).ToColumn("Title")
            .Map(m => m.SectionName).ToColumn("SectionName")
            .Map(m => m.WName).ToColumn("WName")
            .Map(m => m.TimeSlot).ToColumn("TimeSlot")
            .Map(m => m.RoomName).ToColumn("RoomName")
            .Map(m => m.Faculty).ToColumn("Faculty")
            .Map(m => m.FullName).ToColumn("FullName")

            .Build();

            return mapper;
        }
        #endregion
    }
}
