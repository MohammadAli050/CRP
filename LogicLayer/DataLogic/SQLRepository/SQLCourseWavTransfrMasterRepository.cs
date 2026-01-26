using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LogicLayer.BusinessObjects;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data.Common;
using System.Data;

namespace LogicLayer.DataLogic.SQLRepository
{
    partial class SQLCourseWavTransfrMasterRepository : ICourseWavTransfrMasterRepository
    {
        Database db = null;

        private string sqlInsert = "CourseWavTransfrMasterInsert";
        private string sqlUpdate = "CourseWavTransfrMasterUpdate";
        private string sqlDelete = "CourseWavTransfrMasterDeleteById";
        private string sqlGetById ="CourseWavTransfrMasterGetById";
        private string sqlGetAll = "CourseWavTransfrMasterGetAll";


        public int Insert(CourseWavTransfrMaster courseWavTransfrMaster)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, courseWavTransfrMaster, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "CourseWavTransfrMasterID");

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

        public bool Update(CourseWavTransfrMaster courseWavTransfrMaster)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, courseWavTransfrMaster, isInsert);

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

                db.AddInParameter(cmd, "CourseWavTransfrMasterID", DbType.Int32, id);
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

        public CourseWavTransfrMaster GetById(int? id)
        {
            CourseWavTransfrMaster _courseWavTransfrMaster = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseWavTransfrMaster> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseWavTransfrMaster>(sqlGetById, rowMapper);
                _courseWavTransfrMaster = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _courseWavTransfrMaster;
            }

            return _courseWavTransfrMaster;
        }

        public List<CourseWavTransfrMaster> GetAll()
        {
            List<CourseWavTransfrMaster> CourseWavTransfrMasterList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseWavTransfrMaster> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseWavTransfrMaster>(sqlGetAll, mapper);
                IEnumerable<CourseWavTransfrMaster> collection = accessor.Execute();

                CourseWavTransfrMasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return CourseWavTransfrMasterList;
            }

            return CourseWavTransfrMasterList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, CourseWavTransfrMaster courseWavTransfrMaster, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "CourseWavTransfrMasterID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "CourseWavTransfrMasterID", DbType.Int32, courseWavTransfrMaster.CourseWavTransfrMasterID);
            }

            db.AddInParameter(cmd, "CourseWavTransfrMasterID", DbType.Int32, courseWavTransfrMaster.CourseWavTransfrMasterID);
            db.AddInParameter(cmd, "StudentID", DbType.Int32, courseWavTransfrMaster.StudentID);
            db.AddInParameter(cmd, "UniversityName", DbType.String, courseWavTransfrMaster.UniversityName);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, courseWavTransfrMaster.FromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, courseWavTransfrMaster.ToDate);
            db.AddInParameter(cmd, "DivisionType", DbType.Int32, courseWavTransfrMaster.DivisionType);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, courseWavTransfrMaster.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, courseWavTransfrMaster.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, courseWavTransfrMaster.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, courseWavTransfrMaster.ModifiedDate);

            return db;
        }

        private IRowMapper<CourseWavTransfrMaster> GetMaper()
        {
            IRowMapper<CourseWavTransfrMaster> mapper = MapBuilder<CourseWavTransfrMaster>.MapAllProperties()
            .Map(m => m.CourseWavTransfrMasterID).ToColumn("CourseWavTransfrMasterID")
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.UniversityName).ToColumn("UniversityName")
            .Map(m => m.FromDate).ToColumn("FromDate")
            .Map(m => m.ToDate).ToColumn("ToDate")
            .Map(m => m.DivisionType).ToColumn("DivisionType")
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
