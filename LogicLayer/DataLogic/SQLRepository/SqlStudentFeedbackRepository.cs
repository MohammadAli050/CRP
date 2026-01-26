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
    public partial class SqlStudentFeedbackRepository : IStudentFeedbackRepository
    {

        Database db = null;

        private string sqlInsert = "StudentFeedbackInsert";
        private string sqlUpdate = "StudentFeedbackUpdate";
        private string sqlDelete = "StudentFeedbackDelete";
        private string sqlGetById = "StudentFeedbackGetById";
        private string sqlGetAll = "StudentFeedbackGetAll";
               
        public int Insert(StudentFeedback studentfeedback)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, studentfeedback, isInsert);
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

        public bool Update(StudentFeedback studentfeedback)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, studentfeedback, isInsert);

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

        public StudentFeedback GetById(int? id)
        {
            StudentFeedback _studentfeedback = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentFeedback> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentFeedback>(sqlGetById, rowMapper);
                _studentfeedback = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentfeedback;
            }

            return _studentfeedback;
        }

        public List<StudentFeedback> GetAll()
        {
            List<StudentFeedback> studentfeedbackList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentFeedback> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentFeedback>(sqlGetAll, mapper);
                IEnumerable<StudentFeedback> collection = accessor.Execute();

                studentfeedbackList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentfeedbackList;
            }

            return studentfeedbackList;
        }

        public List<StudentFeedback> GetAllByStdentId(int StudentId)
        {
            List<StudentFeedback> studentfeedbackList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentFeedback> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentFeedback>("StudentFeedbackGetAllByStudentId", mapper);
                IEnumerable<StudentFeedback> collection = accessor.Execute(StudentId);

                studentfeedbackList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentfeedbackList;
            }

            return studentfeedbackList;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StudentFeedback studentfeedback, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, studentfeedback.Id);
            }

            	
		db.AddInParameter(cmd,"StudentId",DbType.Int32,studentfeedback.StudentId);
		db.AddInParameter(cmd,"Message",DbType.String,studentfeedback.Message);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,studentfeedback.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,studentfeedback.CreatedDate);
            
            return db;
        }

        private IRowMapper<StudentFeedback> GetMaper()
        {
            IRowMapper<StudentFeedback> mapper = MapBuilder<StudentFeedback>.MapAllProperties()

       	    .Map(m => m.Id).ToColumn("Id")
		    .Map(m => m.StudentId).ToColumn("StudentId")
		    .Map(m => m.Message).ToColumn("Message")
		    .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.FullName).ToColumn("FullName")
            .Map(m => m.RegistrationNo).ToColumn("RegistrationNo")
            .Map(m => m.Phone).ToColumn("Phone")
            
            .Build();

            return mapper;
        }
        #endregion

    }
}

