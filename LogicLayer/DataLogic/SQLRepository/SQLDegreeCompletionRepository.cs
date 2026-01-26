using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LogicLayer.DataLogic.SQLRepository
{
    public class SQLDegreeCompletionRepository : IDegreeCompletionRepository
    {
        Database db = null;

        private string sqlInsert = "DegreeCompletionInsert";
        private string sqlUpdate = "DegreeCompletionUpdate";
        private string sqlDelete = "DegreeCompletionDelete";
        private string sqlGetAll = "DegreeCompletionGetAll";
        private string sqlGetById = "DegreeCompletionGetById";
        private string sqlGetByStduentId = "DegreeCompletionGetByStudentId";


        public int Insert(DegreeCompletion degreeCompletion)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, degreeCompletion, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "DegreeCompletionId");

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

        public bool Update(DegreeCompletion degreeCompletion)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, degreeCompletion, isInsert);

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

                db.AddInParameter(cmd, "DegreeCompletionId", DbType.Int32, id);
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

        public DegreeCompletion GetById(int id)
        {
            DegreeCompletion _degreeCompletion = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<DegreeCompletion> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<DegreeCompletion>(sqlGetById, rowMapper);
                _degreeCompletion = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _degreeCompletion;
            }

            return _degreeCompletion;
        }

        public List<DegreeCompletion> GetAll()
        {
            List<DegreeCompletion> degreeCompletionList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<DegreeCompletion> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<DegreeCompletion>(sqlGetAll, mapper);
                IEnumerable<DegreeCompletion> collection = accessor.Execute();

                degreeCompletionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return degreeCompletionList;
            }

            return degreeCompletionList;
        }

        public DegreeCompletion GetByStudentId(int id)
        {
            DegreeCompletion _degreeCompletion = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<DegreeCompletion> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<DegreeCompletion>(sqlGetByStduentId, rowMapper);
                _degreeCompletion = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _degreeCompletion;
            }

            return _degreeCompletion;
        }

        #region rowMapper

        private Database addParam(Database db, DbCommand cmd, DegreeCompletion degreeCompletion, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "DegreeCompletionId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "DegreeCompletionId", DbType.Int32, degreeCompletion.DegreeCompletionId);
            }

            db.AddInParameter(cmd, "StudentId", DbType.Int32, degreeCompletion.StudentId);
            db.AddInParameter(cmd, "DegreeTranscriptNumber", DbType.String, degreeCompletion.DegreeTranscriptNumber);
            db.AddInParameter(cmd, "TNGenerateDate", DbType.DateTime, degreeCompletion.DegreeTranscriptGenerateDate);
            db.AddInParameter(cmd, "DegreeCertificateNumber", DbType.String, degreeCompletion.DegreeCertificateNumber);
            db.AddInParameter(cmd, "DCGenerateDate", DbType.DateTime, degreeCompletion.DegreeCertificateGenerateDate);
            db.AddInParameter(cmd, "IsDegreeComplete", DbType.Boolean, degreeCompletion.IsDegreeComplete);
            db.AddInParameter(cmd, "Attribute1", DbType.String, degreeCompletion.Attribute1);
            db.AddInParameter(cmd, "Attribute2", DbType.String, degreeCompletion.Attribute2);
            db.AddInParameter(cmd, "Attribute3", DbType.String, degreeCompletion.Attribute3);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, degreeCompletion.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, degreeCompletion.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, degreeCompletion.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, degreeCompletion.ModifiedDate);

            return db;
        }

        private IRowMapper<DegreeCompletion> GetMaper()
        {
            IRowMapper<DegreeCompletion> mapper = MapBuilder<DegreeCompletion>.MapAllProperties()

            .Map(m => m.DegreeCompletionId).ToColumn("DegreeCompletionId")
            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.DegreeTranscriptNumber).ToColumn("DegreeTranscriptNumber")
            .Map(m => m.DegreeTranscriptGenerateDate).ToColumn("TNGenerateDate")
            .Map(m => m.DegreeCertificateNumber).ToColumn("DegreeCertificateNumber")
            .Map(m => m.DegreeCertificateGenerateDate).ToColumn("DCGenerateDate")
            .Map(m => m.IsDegreeComplete).ToColumn("IsDegreeComplete")
            .Map(m => m.Attribute1).ToColumn("Attribute1")
            .Map(m => m.Attribute2).ToColumn("Attribute2")
            .Map(m => m.Attribute3).ToColumn("Attribute3")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .DoNotMap(m => m.PublicationDate)
            .DoNotMap(m => m.StudentTranscriptInfoId)
            .DoNotMap(m => m.ExaminationMonth)

            .Build();

            return mapper;
        }

        #endregion
    }
}
