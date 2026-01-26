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
    public partial class SqlStudentInstitutionRepository : IStudentInstitutionRepository
    {

        Database db = null;

        private string sqlInsert = "StudentInstitutionInsert";
        private string sqlUpdate = "StudentInstitutionUpdate";
        private string sqlDelete = "StudentInstitutionDelete";
        private string sqlGetById = "StudentInstitutionGetById";
        private string sqlGetAll = "StudentInstitutionGetAll";

        public int Insert(StudentInstitution studentinstitution)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, studentinstitution, isInsert);
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

        public bool Update(StudentInstitution studentinstitution)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, studentinstitution, isInsert);

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

        public StudentInstitution GetById(int? id)
        {
            StudentInstitution _studentinstitution = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentInstitution> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentInstitution>(sqlGetById, rowMapper);
                _studentinstitution = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentinstitution;
            }

            return _studentinstitution;
        }

        public StudentInstitution GetByStudentId(int StudentId)
        {
            StudentInstitution _studentinstitution = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentInstitution> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentInstitution>("StudentInstitutionGetByStudentId", rowMapper);
                _studentinstitution = accessor.Execute(StudentId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentinstitution;
            }

            return _studentinstitution;
        }


        public List<StudentInstitution> GetAll()
        {
            List<StudentInstitution> studentinstitutionList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentInstitution> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentInstitution>(sqlGetAll, mapper);
                IEnumerable<StudentInstitution> collection = accessor.Execute();

                studentinstitutionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentinstitutionList;
            }

            return studentinstitutionList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StudentInstitution studentinstitution, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, studentinstitution.Id);
            }


            db.AddInParameter(cmd, "StudentId", DbType.Int32, studentinstitution.StudentId);
            db.AddInParameter(cmd, "InstitutionId", DbType.Int32, studentinstitution.InstitutionId);
            db.AddInParameter(cmd, "ExemCenterId", DbType.Int32, studentinstitution.ExemCenterId);
            db.AddInParameter(cmd, "Attribute1", DbType.String, studentinstitution.Attribute1);
            db.AddInParameter(cmd, "Attribute2", DbType.String, studentinstitution.Attribute2);
            db.AddInParameter(cmd, "Attribute3", DbType.String, studentinstitution.Attribute3);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, studentinstitution.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, studentinstitution.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, studentinstitution.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, studentinstitution.ModifiedDate);

            return db;
        }

        private IRowMapper<StudentInstitution> GetMaper()
        {
            IRowMapper<StudentInstitution> mapper = MapBuilder<StudentInstitution>.MapAllProperties()

           .Map(m => m.Id).ToColumn("Id")
        .Map(m => m.StudentId).ToColumn("StudentId")
        .Map(m => m.InstitutionId).ToColumn("InstitutionId")
        .Map(m => m.ExemCenterId).ToColumn("ExemCenterId")
        .Map(m => m.Attribute1).ToColumn("Attribute1")
        .Map(m => m.Attribute2).ToColumn("Attribute2")
        .Map(m => m.Attribute3).ToColumn("Attribute3")
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

