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
    public partial class SqlDayLectureRepository : IDayLectureRepository
    {

        Database db = null;

        private string sqlInsert = "DayLectureInsert";
        private string sqlUpdate = "DayLectureUpdate";
        private string sqlDelete = "DayLectureDelete";
        private string sqlGetById = "DayLectureGetById";
        private string sqlGetAll = "DayLectureGetAll";
               
        public int Insert(DayLecture daylecture)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, daylecture, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "Id");

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

        public bool Update(DayLecture daylecture)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, daylecture, isInsert);

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

                db.AddInParameter(cmd, "Id", DbType.Int32, id);
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

        public DayLecture GetById(int? id)
        {
            DayLecture _daylecture = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<DayLecture> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<DayLecture>(sqlGetById, rowMapper);
                _daylecture = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _daylecture;
            }

            return _daylecture;
        }

        public DayLecture GetByProgramIdSessionIdCourseIdVersionIdlectureNo(int ProgramId, int SessionId, int CourseId, int VersionId, int LectureNo)
        {
            DayLecture _daylecture = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<DayLecture> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<DayLecture>("DayLectureGetByProgramIdSessionIdCourseIdVersionIdlectureNo", rowMapper);
                _daylecture = accessor.Execute(ProgramId,SessionId,CourseId,VersionId,LectureNo).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _daylecture;
            }

            return _daylecture;
        }

        public List<DayLecture> GetAll()
        {
            List<DayLecture> daylectureList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<DayLecture> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<DayLecture>(sqlGetAll, mapper);
                IEnumerable<DayLecture> collection = accessor.Execute();

                daylectureList = collection.ToList();
            }

            catch (Exception ex)
            {
                return daylectureList;
            }

            return daylectureList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, DayLecture daylecture, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, daylecture.Id);
            }

            	
		db.AddInParameter(cmd,"ProgramId",DbType.Int32,daylecture.ProgramId);
		db.AddInParameter(cmd,"SessionId",DbType.Int32,daylecture.SessionId);
		db.AddInParameter(cmd,"CourseId",DbType.Int32,daylecture.CourseId);
		db.AddInParameter(cmd,"VersionId",DbType.Int32,daylecture.VersionId);
		db.AddInParameter(cmd,"LectureNo",DbType.Int32,daylecture.LectureNo);
		db.AddInParameter(cmd,"Topic",DbType.String,daylecture.Topic);
		db.AddInParameter(cmd,"Remarks1",DbType.String,daylecture.Remarks1);
		db.AddInParameter(cmd,"Remarks2",DbType.String,daylecture.Remarks2);
		db.AddInParameter(cmd,"Remarks3",DbType.String,daylecture.Remarks3);
		db.AddInParameter(cmd,"Remarks4",DbType.String,daylecture.Remarks4);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,daylecture.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,daylecture.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,daylecture.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,daylecture.ModifiedDate);
            
            return db;
        }

        private IRowMapper<DayLecture> GetMaper()
        {
            IRowMapper<DayLecture> mapper = MapBuilder<DayLecture>.MapAllProperties()

       	   .Map(m => m.Id).ToColumn("Id")
		.Map(m => m.ProgramId).ToColumn("ProgramId")
		.Map(m => m.SessionId).ToColumn("SessionId")
		.Map(m => m.CourseId).ToColumn("CourseId")
		.Map(m => m.VersionId).ToColumn("VersionId")
		.Map(m => m.LectureNo).ToColumn("LectureNo")
		.Map(m => m.Topic).ToColumn("Topic")
		.Map(m => m.Remarks1).ToColumn("Remarks1")
		.Map(m => m.Remarks2).ToColumn("Remarks2")
		.Map(m => m.Remarks3).ToColumn("Remarks3")
		.Map(m => m.Remarks4).ToColumn("Remarks4")
		.Map(m => m.CreatedBy).ToColumn("CreatedBy")
		.Map(m => m.CreatedDate).ToColumn("CreatedDate")
		.Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
		.Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            
            .Build();

            return mapper;
        }
        #endregion

    }
}

