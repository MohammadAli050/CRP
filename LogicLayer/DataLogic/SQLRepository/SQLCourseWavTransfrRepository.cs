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
    partial class SQLCourseWavTransfrRepository : ICourseWavTransfrRepository
    {
        Database db = null;

        private string sqlInsert = "CourseWavTransfrInsert";
        private string sqlUpdate = "CourseWavTransfrUpdate";
        private string sqlDelete = "CourseWavTransfrDeleteById";
        private string sqlGetById ="CourseWavTransfrGetById";
        private string sqlGetAll = "CourseWavTransfrGetAll";
        private string sqlGetUniqueAll = "CourseWavTransfrGetUniqueAll";
        private string sqlGetByStudentId = "CourseWavTransfrGetByStudentId";

        public int Insert(CourseWavTransfr courseWavTransfr)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, courseWavTransfr, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "CourseWavTransfrID");

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

        public bool Update(CourseWavTransfr courseWavTransfr)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, courseWavTransfr, isInsert);

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

                db.AddInParameter(cmd, "CourseWavTransfrID", DbType.Int32, id);
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

        public CourseWavTransfr GetById(int? id)
        {
            CourseWavTransfr _courseWavTransfr = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseWavTransfr> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseWavTransfr>(sqlGetById, rowMapper);
                _courseWavTransfr = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _courseWavTransfr;
            }

            return _courseWavTransfr;
        }

        public List<CourseWavTransfr> GetAll()
        {
            List<CourseWavTransfr> CourseWavTransfrList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseWavTransfr> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseWavTransfr>(sqlGetAll, mapper);
                IEnumerable<CourseWavTransfr> collection = accessor.Execute();

                CourseWavTransfrList = collection.ToList();
            }

            catch (Exception ex)
            {
                return CourseWavTransfrList;
            }

            return CourseWavTransfrList;
        }

        public List<CourseWavTransfr> GetByStudentId(int studentId)
        {
            List<CourseWavTransfr> courseList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseWavTransfr> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseWavTransfr>(sqlGetByStudentId, mapper);
                IEnumerable<CourseWavTransfr> collection = accessor.Execute(studentId).ToList();

                courseList = collection.ToList();
            }

            catch (Exception ex)
            {
                return courseList;
            }

            return courseList;
        }

        public List<CourseWavTransfr> GetUniqueAll()
        {
            List<CourseWavTransfr> CourseWavTransfrList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseWavTransfr> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseWavTransfr>(sqlGetUniqueAll, mapper);
                IEnumerable<CourseWavTransfr> collection = accessor.Execute();

                CourseWavTransfrList = collection.ToList();
            }

            catch (Exception ex)
            {
                return CourseWavTransfrList;
            }

            return CourseWavTransfrList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, CourseWavTransfr courseWavTransfr, bool isInsert)
        {
            /*
            if (isInsert)
            {
                db.AddOutParameter(cmd, "CourseWavTransfrID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "CourseWavTransfrID", DbType.Int32, courseWavTransfr.CourseWavTransfrID);
            }*/

            db.AddInParameter(cmd, "CourseWavTransfrID", DbType.Int32, courseWavTransfr.CourseWavTransfrID);
            db.AddInParameter(cmd, "StudentID", DbType.Int32, courseWavTransfr.StudentID);
            db.AddInParameter(cmd, "UniversityName", DbType.String, courseWavTransfr.UniversityName);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, courseWavTransfr.FromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, courseWavTransfr.ToDate);
            db.AddInParameter(cmd, "DivisionType", DbType.Int32, courseWavTransfr.DivisionType);
            db.AddInParameter(cmd, "CourseStatusID", DbType.Int32, courseWavTransfr.CourseStatusID);
            db.AddInParameter(cmd, "Remarks", DbType.String, courseWavTransfr.Remarks);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, courseWavTransfr.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, courseWavTransfr.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, courseWavTransfr.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, courseWavTransfr.ModifiedDate);

            return db;
        }

        private IRowMapper<CourseWavTransfr> GetMaper()
        {
            IRowMapper<CourseWavTransfr> mapper = MapBuilder<CourseWavTransfr>.MapAllProperties()
            .Map(m => m.CourseWavTransfrID).ToColumn("CourseWavTransfrID")
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.UniversityName).ToColumn("UniversityName")
            .Map(m => m.FromDate).ToColumn("FromDate")
             .Map(m => m.ToDate).ToColumn("ToDate")
            .Map(m => m.DivisionType).ToColumn("DivisionType")
            .Map(m => m.CourseStatusID).ToColumn("CourseStatusID")
            .Map(m => m.Remarks).ToColumn("Remarks")
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
