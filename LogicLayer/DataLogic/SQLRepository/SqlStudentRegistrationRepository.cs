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
    public partial class SqlStudentRegistrationRepository : IStudentRegistrationRepository
    {

        Database db = null;

        private string sqlInsert = "StudentRegistrationInsert";
        private string sqlUpdate = "StudentRegistrationUpdate";
        private string sqlDelete = "StudentRegistrationDeleteById";
        private string sqlGetById = "StudentRegistrationGetById";
        private string sqlGetAll = "StudentRegistrationGetAll";
        private string sqlGetByStudentId = "StudentRegistrationGetByStudentId";
        private string sqlGetAllByProgramBatchStudent = "StudentRegistrationGetAllByProgramBatchStudent";
               
        public int Insert(StudentRegistration studentregistration)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, studentregistration, isInsert);
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

        public bool Update(StudentRegistration studentregistration)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, studentregistration, isInsert);

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

        public StudentRegistration GetById(int? id)
        {
            StudentRegistration _studentregistration = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentRegistration> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentRegistration>(sqlGetById, rowMapper);
                _studentregistration = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentregistration;
            }

            return _studentregistration;
        }

        public StudentRegistration GetByRegistrationNo(string RegNo)
        {
            StudentRegistration _studentregistration = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentRegistration> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentRegistration>("StudentRegistrationGetByStudentRegNo", rowMapper);
                _studentregistration = accessor.Execute(RegNo).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentregistration;
            }

            return _studentregistration;
        }

        public StudentRegistration GetByLoginId(string loginId)
        {
            StudentRegistration _studentregistration = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentRegistration> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentRegistration>("StudentRegistrationGetByloginId", rowMapper);
                _studentregistration = accessor.Execute(loginId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentregistration;
            }

            return _studentregistration;
        }

        public List<StudentRegistration> GetAll()
        {
            List<StudentRegistration> studentregistrationList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentRegistration> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentRegistration>(sqlGetAll, mapper);
                IEnumerable<StudentRegistration> collection = accessor.Execute();

                studentregistrationList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentregistrationList;
            }

            return studentregistrationList;
        }

        public StudentRegistration GetByStudentId(int studentId)
        {
            StudentRegistration _studentregistration = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentRegistration> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentRegistration>(sqlGetByStudentId, rowMapper);
                _studentregistration = accessor.Execute(studentId).FirstOrDefault();

            }
            catch (Exception ex)
            {
                return _studentregistration;
            }

            return _studentregistration;
        }

        public List<StudentRegistration> GetAllByProgramBatchStudent(int programId, int batchId, string roll)
        {
            List<StudentRegistration> studentRegistrationList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentRegistration> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentRegistration>(sqlGetAllByProgramBatchStudent, mapper);
                IEnumerable<StudentRegistration> collection = accessor.Execute(programId, batchId, roll);

                studentRegistrationList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentRegistrationList;
            }

            return studentRegistrationList;
        }


       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StudentRegistration studentregistration, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, studentregistration.Id);
            }

            	
		db.AddInParameter(cmd,"StudentId",DbType.Int32,studentregistration.StudentId);
		db.AddInParameter(cmd,"RegistrationNo",DbType.String,studentregistration.RegistrationNo);
		db.AddInParameter(cmd,"SessionId",DbType.Int32,studentregistration.SessionId);
		db.AddInParameter(cmd,"Attribute1",DbType.String,studentregistration.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,studentregistration.Attribute2);
		db.AddInParameter(cmd,"Attribute3",DbType.String,studentregistration.Attribute3);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,studentregistration.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,studentregistration.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,studentregistration.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,studentregistration.ModifiedDate);
            
            return db;
        }

        private IRowMapper<StudentRegistration> GetMaper()
        {
            IRowMapper<StudentRegistration> mapper = MapBuilder<StudentRegistration>.MapAllProperties()

       	    .Map(m => m.Id).ToColumn("Id")
		    .Map(m => m.StudentId).ToColumn("StudentId")
		    .Map(m => m.RegistrationNo).ToColumn("RegistrationNo")
		    .Map(m => m.SessionId).ToColumn("SessionId")
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

