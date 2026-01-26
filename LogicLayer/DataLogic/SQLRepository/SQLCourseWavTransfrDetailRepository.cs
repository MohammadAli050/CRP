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
    partial class SQLCourseWavTransfrDetailRepository : ICourseWavTransfrDetailRepository
    {
        Database db = null;

        private string sqlInsert = "CourseWavTransfrDetailInsert";
        private string sqlUpdate = "CourseWavTransfrDetailUpdate";
        private string sqlDelete = "CourseWavTransfrDetailDeleteById";
        private string sqlGetById = "CourseWavTransfrDetailGetById";
        private string sqlGetAll = "CourseWavTransfrDetailGetAll";


        public int Insert(CourseWavTransfrDetail courseWavTransfrDetail)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, courseWavTransfrDetail, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "CourseWavTransfrDetailID");

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

        public bool Update(CourseWavTransfrDetail courseWavTransfrDetail)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, courseWavTransfrDetail, isInsert);

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

                db.AddInParameter(cmd, "CourseWavTransfrDetailID", DbType.Int32, id);
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

        public CourseWavTransfrDetail GetById(int? id)
        {
            CourseWavTransfrDetail _courseWavTransfrDetail = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseWavTransfrDetail> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseWavTransfrDetail>(sqlGetById, rowMapper);
                _courseWavTransfrDetail = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _courseWavTransfrDetail;
            }

            return _courseWavTransfrDetail;
        }

        public List<CourseWavTransfrDetail> GetAll()
        {
            List<CourseWavTransfrDetail> CourseWavTransfrDetailList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseWavTransfrDetail> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseWavTransfrDetail>(sqlGetAll, mapper);
                IEnumerable<CourseWavTransfrDetail> collection = accessor.Execute();

                CourseWavTransfrDetailList = collection.ToList();
            }

            catch (Exception ex)
            {
                return CourseWavTransfrDetailList;
            }

            return CourseWavTransfrDetailList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, CourseWavTransfrDetail courseWavTransfrDetail, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "CourseWavTransfrDetailID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "CourseWavTransfrDetailID", DbType.Int32, courseWavTransfrDetail.CourseWavTransfrDetailID);
            }

            db.AddInParameter(cmd, "CourseWavTransfrDetailID", DbType.Int32, courseWavTransfrDetail.CourseWavTransfrDetailID);
            db.AddInParameter(cmd, "CourseWavTransfrMasterID", DbType.Int32, courseWavTransfrDetail.CourseWavTransfrMasterID);
            db.AddInParameter(cmd, "OwnerNodeCourseID", DbType.Int32, courseWavTransfrDetail.OwnerNodeCourseID);
            db.AddInParameter(cmd, "AgainstCourseInfo", DbType.String, courseWavTransfrDetail.AgainstCourseInfo);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, courseWavTransfrDetail.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, courseWavTransfrDetail.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, courseWavTransfrDetail.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, courseWavTransfrDetail.ModifiedDate);

            return db;
        }

        private IRowMapper<CourseWavTransfrDetail> GetMaper()
        {
            IRowMapper<CourseWavTransfrDetail> mapper = MapBuilder<CourseWavTransfrDetail>.MapAllProperties()
            .Map(m => m.CourseWavTransfrDetailID).ToColumn("CourseWavTransfrDetailID")
            .Map(m => m.CourseWavTransfrMasterID).ToColumn("CourseWavTransfrMasterID")
            .Map(m => m.OwnerNodeCourseID).ToColumn("OwnerNodeCourseID")
            .Map(m => m.AgainstCourseInfo).ToColumn("AgainstCourseInfo")
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
