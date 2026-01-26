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
    public partial class SQLCourseStatusRepository : ICourseStatusRepository
    {
        Database db = null;

        private string sqlInsert = "CourseStatusInsert";
        private string sqlUpdate = "CourseStatusUpdate";
        private string sqlDelete = "CourseStatusDeleteById";
        private string sqlGetById = "CourseStatusGetById";
        private string sqlGetByCode = "CourseStatusGetByCode";
        private string sqlGetAll = "CourseStatusGetAll";
        
        
        public int Insert(CourseStatus courseStatus)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, courseStatus, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "CourseStatusID");

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

        public bool Update(CourseStatus courseStatus)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, courseStatus, isInsert);

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

                db.AddInParameter(cmd, "CourseStatusID", DbType.Int32, id);
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

        public CourseStatus GetById(int? id)
        {
            CourseStatus _courseStatus = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseStatus> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseStatus>(sqlGetById, rowMapper);
                _courseStatus = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _courseStatus;
            }

            return _courseStatus;
        }

        public CourseStatus GetByCode(string code)
        {
            CourseStatus _courseStatus = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseStatus> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseStatus>(sqlGetByCode, rowMapper);
                _courseStatus = accessor.Execute(code).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _courseStatus;
            }

            return _courseStatus;
        }

        public List<CourseStatus> GetAll()
        {
            List<CourseStatus> courseStatusList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseStatus> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseStatus>(sqlGetAll, mapper);
                IEnumerable<CourseStatus> collection = accessor.Execute();

                courseStatusList = collection.ToList();
            }

            catch (Exception ex)
            {
                return courseStatusList;
            }

            return courseStatusList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, CourseStatus courseStatus, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "CourseStatusID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "CourseStatusID", DbType.Int32, courseStatus.CourseStatusID);
            }

            db.AddInParameter(cmd, "Code", DbType.String, courseStatus.Code);
            db.AddInParameter(cmd, "Description", DbType.String, courseStatus.Description);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, courseStatus.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, courseStatus.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, courseStatus.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, courseStatus.ModifiedDate);
            
            return db;
        }

        private IRowMapper<CourseStatus> GetMaper()
        {
            IRowMapper<CourseStatus> mapper = MapBuilder<CourseStatus>.MapAllProperties()
            .Map(m => m.CourseStatusID).ToColumn("CourseStatusID")
            .Map(m => m.Code).ToColumn("Code")
            .Map(m => m.Description).ToColumn("Description")
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
