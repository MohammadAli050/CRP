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
    public partial class SqlStudentTranscriptInfoRepository : IStudentTranscriptInfoRepository
    {

        Database db = null;

        private string sqlInsert = "StudentTranscriptInfoInsert";
        private string sqlUpdate = "StudentTranscriptInfoUpdate";
        private string sqlDelete = "StudentTranscriptInfoDelete";
        private string sqlGetById = "StudentTranscriptInfoGetById";
        private string sqlGetAll = "StudentTranscriptInfoGetAll";

        public int Insert(StudentTranscriptInfo studenttranscriptinfo)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, studenttranscriptinfo, isInsert);
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

        public bool Update(StudentTranscriptInfo studenttranscriptinfo)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, studenttranscriptinfo, isInsert);

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

        public StudentTranscriptInfo GetById(int? id)
        {
            StudentTranscriptInfo _studenttranscriptinfo = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentTranscriptInfo> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentTranscriptInfo>(sqlGetById, rowMapper);
                _studenttranscriptinfo = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studenttranscriptinfo;
            }

            return _studenttranscriptinfo;
        }

        public StudentTranscriptInfo GetByStudentId(int? studentId)
        {
            StudentTranscriptInfo _studenttranscriptinfo = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentTranscriptInfo> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentTranscriptInfo>("StudentTranscriptInfoGetByStudentId", rowMapper);
                _studenttranscriptinfo = accessor.Execute(studentId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studenttranscriptinfo;
            }

            return _studenttranscriptinfo;
        }

        public List<StudentTranscriptInfo> GetByProgramIdBatchIdRoll(int ProgramId, int BatchId, string Roll)
        {
            List<StudentTranscriptInfo> list = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentTranscriptInfo> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentTranscriptInfo>("StudentTranscriptInfoGetByProgramIdBatchIdRoll", rowMapper);
                IEnumerable<StudentTranscriptInfo> collection = accessor.Execute(ProgramId, BatchId, Roll);

                list = collection.ToList();

            }
            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<StudentTranscriptInfo> GetAll()
        {
            List<StudentTranscriptInfo> studenttranscriptinfoList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentTranscriptInfo> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentTranscriptInfo>(sqlGetAll, mapper);
                IEnumerable<StudentTranscriptInfo> collection = accessor.Execute();

                studenttranscriptinfoList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studenttranscriptinfoList;
            }

            return studenttranscriptinfoList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StudentTranscriptInfo studenttranscriptinfo, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, studenttranscriptinfo.Id);
            }


            db.AddInParameter(cmd, "StudentId", DbType.Int32, studenttranscriptinfo.StudentId);
            db.AddInParameter(cmd, "PublicationDate", DbType.DateTime, studenttranscriptinfo.PublicationDate);
            db.AddInParameter(cmd, "PreparedDate", DbType.DateTime, studenttranscriptinfo.PreparedDate);
            db.AddInParameter(cmd, "Attribute1", DbType.Int32, studenttranscriptinfo.Attribute1);
            db.AddInParameter(cmd, "Attribute2", DbType.String, studenttranscriptinfo.Attribute2);
            db.AddInParameter(cmd, "ExaminationMonth", DbType.String, studenttranscriptinfo.ExaminationMonth);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, studenttranscriptinfo.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, studenttranscriptinfo.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, studenttranscriptinfo.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, studenttranscriptinfo.ModifiedDate);

            return db;
        }

        private IRowMapper<StudentTranscriptInfo> GetMaper()
        {
            IRowMapper<StudentTranscriptInfo> mapper = MapBuilder<StudentTranscriptInfo>.MapAllProperties()

           .Map(m => m.Id).ToColumn("Id")
        .Map(m => m.StudentId).ToColumn("StudentId")
        .Map(m => m.PublicationDate).ToColumn("PublicationDate")
        .Map(m => m.PreparedDate).ToColumn("PreparedDate")
        .Map(m => m.Attribute1).ToColumn("Attribute1")
        .Map(m => m.Attribute2).ToColumn("Attribute2")
        .Map(m => m.ExaminationMonth).ToColumn("ExaminationMonth")
        .Map(m => m.CreatedBy).ToColumn("CreatedBy")
        .Map(m => m.CreatedDate).ToColumn("CreatedDate")
        .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
        .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")

        .Map(m => m.Roll).ToColumn("Roll")
        .Map(m => m.FullName).ToColumn("FullName")

            .Build();

            return mapper;
        }
        #endregion

    }
}
