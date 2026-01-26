using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LogicLayer.DataLogic
{
    public partial class SQLStudentPreCourseRepository : IStudentPreCourseRepository
    {
        Database db = null;

        private string sqlInsert = "StudentPreCourseInsert";
        private string sqlUpdate = "StudentPreCourseUpdate";
        private string sqlDelete = "StudentPreCourseDeleteById";
        private string sqlGetById = "StudentPreCourseGetById";
        private string sqlGetAll = "StudentPreCourseGetAll";
        private string sqlGetAllByParameter = "StudentPreCourseGetAllByParameter";
        
        public int Insert(StudentPreCourse studentprecourse)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);               
                
                db = addParam(db, cmd, studentprecourse, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "StudentPreCourseId");

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

        public bool Update(StudentPreCourse studentprecourse)
        {
            bool result = false;
            bool isInsert = false;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);


                db = addParam(db, cmd, studentprecourse, isInsert);
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

                db.AddInParameter(cmd, "StudentPreCourseId", DbType.Int32, id);

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

        public StudentPreCourse GetById(int id)
        {
            StudentPreCourse _studentprecourse = new StudentPreCourse();
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                
                IRowMapper<StudentPreCourse> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentPreCourse>(sqlGetById, rowMapper);
                _studentprecourse = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentprecourse;
            }

            return _studentprecourse;
        }

        public List<StudentPreCourse> GetAll()
        {
            List<StudentPreCourse> studentprecourseList = new List<StudentPreCourse>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentPreCourse> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentPreCourse>(sqlGetAll, mapper);
                IEnumerable<StudentPreCourse> collection = accessor.Execute();

                studentprecourseList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentprecourseList;
            }

            return studentprecourseList;
        }

        public List<StudentPreCourse> GetAllByParameter(int action, string batchCode, string programCode, string preMandatoryCourse, int preCourseId, int preVersionId, int mainCourseId, int mainVersionId)
        {
            List<StudentPreCourse> studentPreCourseList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentPreCourse> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentPreCourse>(sqlGetAllByParameter, mapper);
                IEnumerable<StudentPreCourse> collection = accessor.Execute(action, batchCode, programCode, preMandatoryCourse, preCourseId, preVersionId, mainCourseId, mainVersionId);

                studentPreCourseList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentPreCourseList;
            }

            return studentPreCourseList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StudentPreCourse studentprecourse, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "StudentPreCourseId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "StudentPreCourseId", DbType.Int32, Int32.MaxValue);
            }

            db.AddInParameter(cmd, "StudentId", DbType.Int32, studentprecourse.StudentId);
            db.AddInParameter(cmd, "PreCourseId", DbType.Int32, studentprecourse.PreCourseId);
            db.AddInParameter(cmd, "PreVersionId", DbType.Int32, studentprecourse.PreVersionId);
            db.AddInParameter(cmd, "PreNodeCourseId", DbType.Int32, studentprecourse.PreNodeCourseId);
            db.AddInParameter(cmd, "MainCourseId", DbType.Int32, studentprecourse.MainCourseId);
            db.AddInParameter(cmd, "MainVersionId", DbType.Int32, studentprecourse.MainVersionId);
            db.AddInParameter(cmd, "ManiNodeCourseId", DbType.Int32, studentprecourse.ManiNodeCourseId);
            db.AddInParameter(cmd, "IsBundle", DbType.Boolean, studentprecourse.IsBundle);
            db.AddInParameter(cmd, "Remarks", DbType.String, studentprecourse.Remarks);
            db.AddInParameter(cmd, "IsBool", DbType.Boolean, studentprecourse.IsBool);
            db.AddInParameter(cmd, "Number", DbType.Int32, studentprecourse.Number);
            db.AddInParameter(cmd, "Attribute1", DbType.String, studentprecourse.Attribute1);
            db.AddInParameter(cmd, "Attribute2", DbType.String, studentprecourse.Attribute2);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, studentprecourse.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, studentprecourse.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, studentprecourse.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, studentprecourse.ModifiedDate);

            return db;
        }

        private IRowMapper<StudentPreCourse> GetMaper()
        {
            IRowMapper<StudentPreCourse> mapper = MapBuilder<StudentPreCourse>.MapAllProperties()
            .Map(m => m.StudentPreCourseId).ToColumn("StudentPreCourseId")
            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.PreCourseId).ToColumn("PreCourseId")
            .Map(m => m.PreVersionId).ToColumn("PreVersionId")
            .Map(m => m.PreNodeCourseId).ToColumn("PreNodeCourseId")
            .Map(m => m.MainCourseId).ToColumn("MainCourseId")
            .Map(m => m.MainVersionId).ToColumn("MainVersionId")
            .Map(m => m.ManiNodeCourseId).ToColumn("ManiNodeCourseId")
            .Map(m => m.IsBundle).ToColumn("IsBundle")
            .Map(m => m.Remarks).ToColumn("Remarks")
            .Map(m => m.IsBool).ToColumn("IsBool")
            .Map(m => m.Number).ToColumn("Number")
            .Map(m => m.Attribute1).ToColumn("Attribute1")
            .Map(m => m.Attribute2).ToColumn("Attribute2")
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


