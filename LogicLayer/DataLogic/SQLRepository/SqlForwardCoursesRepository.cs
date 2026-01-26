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
    public partial class SqlForwardCoursesRepository : IForwardCoursesRepository
    {

        Database db = null;

        private string sqlInsert = "ForwardCoursesInsert";
        private string sqlUpdate = "ForwardCoursesUpdate";
        private string sqlDelete = "ForwardCoursesDelete";
        private string sqlGetById = "ForwardCoursesGetById";
        private string sqlGetAll = "ForwardCoursesGetAll";

        public int Insert(ForwardCourses forwardcourses)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, forwardcourses, isInsert);
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

        public bool Update(ForwardCourses forwardcourses)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, forwardcourses, isInsert);

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

        public bool DeleteByStudentIdCourseIdVersionIdAcaCalId(int StudentId, int CourseId, int VersionId, int AcaCalId)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("ForwardCoursesDeleteByStudentIdCourseIdVersionIdAcaCalId");

                db.AddInParameter(cmd, "StudentId", DbType.Int32, StudentId);
                db.AddInParameter(cmd, "CourseId", DbType.Int32, CourseId);
                db.AddInParameter(cmd, "VersionId", DbType.Int32, VersionId);
                db.AddInParameter(cmd, "AcaCalId", DbType.Int32, AcaCalId);
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

        public ForwardCourses GetById(int? id)
        {
            ForwardCourses _forwardcourses = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ForwardCourses> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ForwardCourses>(sqlGetById, rowMapper);
                _forwardcourses = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _forwardcourses;
            }

            return _forwardcourses;
        }

        public List<ForwardCourses> GetAll()
        {
            List<ForwardCourses> forwardcoursesList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ForwardCourses> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ForwardCourses>(sqlGetAll, mapper);
                IEnumerable<ForwardCourses> collection = accessor.Execute();

                forwardcoursesList = collection.ToList();
            }

            catch (Exception ex)
            {
                return forwardcoursesList;
            }

            return forwardcoursesList;
        }

        public List<ForwardCourses> GetAllByStudentIdAcaCalId(int StudentId, int AcaCalId)
        {
            List<ForwardCourses> forwardcoursesList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ForwardCourses> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ForwardCourses>("ForwardCoursesGetByStudentIdAcaCalId", mapper);
                IEnumerable<ForwardCourses> collection = accessor.Execute(StudentId, AcaCalId);

                forwardcoursesList = collection.ToList();
            }

            catch (Exception ex)
            {
                return forwardcoursesList;
            }

            return forwardcoursesList;
        }

        public ForwardCourses GetByCourseHistoryId(int CourseHistoryId)
        {
            ForwardCourses _forwardcourses = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ForwardCourses> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ForwardCourses>("ForwardCourseGetByCourseHistoryId", rowMapper);
                _forwardcourses = accessor.Execute(CourseHistoryId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _forwardcourses;
            }

            return _forwardcourses;
        }

        public ForwardCourses GetByRegistrationWorkSheetId(int RegWrokId)
        {
            ForwardCourses _forwardcourses = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ForwardCourses> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ForwardCourses>("ForwardCourseGetByRegistrationWorkSheetId", rowMapper);
                _forwardcourses = accessor.Execute(RegWrokId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _forwardcourses;
            }

            return _forwardcourses;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ForwardCourses forwardcourses, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, forwardcourses.Id);
            }


            db.AddInParameter(cmd, "StudentId", DbType.Int32, forwardcourses.StudentId);
            db.AddInParameter(cmd, "AcaCalId", DbType.Int32, forwardcourses.AcaCalId);
            db.AddInParameter(cmd, "CourseId", DbType.Int32, forwardcourses.CourseId);
            db.AddInParameter(cmd, "VersionId", DbType.Int32, forwardcourses.VersionId);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, forwardcourses.CreatedBy);
            db.AddInParameter(cmd, "IsActive", DbType.Boolean, forwardcourses.IsActive);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, forwardcourses.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, forwardcourses.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, forwardcourses.ModifiedDate);

            return db;
        }

        private IRowMapper<ForwardCourses> GetMaper()
        {
            IRowMapper<ForwardCourses> mapper = MapBuilder<ForwardCourses>.MapAllProperties()

           .Map(m => m.Id).ToColumn("Id")
        .Map(m => m.StudentId).ToColumn("StudentId")
        .Map(m => m.AcaCalId).ToColumn("AcaCalId")
        .Map(m => m.CourseId).ToColumn("CourseId")
        .Map(m => m.VersionId).ToColumn("VersionId")
        .Map(m => m.CreatedBy).ToColumn("CreatedBy")
        .Map(m => m.IsActive).ToColumn("IsActive")
        .Map(m => m.CreatedDate).ToColumn("CreatedDate")
        .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
        .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")

            .Build();

            return mapper;
        }
        #endregion

    }
}

