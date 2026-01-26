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
    public partial class SQLCourseACUSpanDtlRepository : ICourseACUSpanDtlRepository
    {
        Database db = null;

        private string sqlInsert = "CourseACUSpanDtlInsert";
        private string sqlUpdate = "CourseACUSpanDtlUpdate";
        private string sqlDelete = "CourseACUSpanDtlDeleteById";
        private string sqlGetById = "CourseACUSpanDtlGetById";
        private string sqlGetAll = "CourseACUSpanDtlGetAll";

        public int Insert(CourseACUSpanDtl courseACUSpanDtl)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, courseACUSpanDtl, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "CourseACUSpanDtlID");

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

        public bool Update(CourseACUSpanDtl courseACUSpanDtl)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, courseACUSpanDtl, isInsert);

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

                db.AddInParameter(cmd, "CourseACUSpanDtlID", DbType.Int32, id);
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

        public CourseACUSpanDtl GetById(int? id)
        {
            CourseACUSpanDtl _courseACUSpanDtl = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseACUSpanDtl> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseACUSpanDtl>(sqlGetById, rowMapper);
                _courseACUSpanDtl = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _courseACUSpanDtl;
            }

            return _courseACUSpanDtl;
        }

        public List<CourseACUSpanDtl> GetAll()
        {
            List<CourseACUSpanDtl> CourseACUSpanDtlList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseACUSpanDtl> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseACUSpanDtl>(sqlGetAll, mapper);
                IEnumerable<CourseACUSpanDtl> collection = accessor.Execute();

                CourseACUSpanDtlList = collection.ToList();
            }

            catch (Exception ex)
            {
                return CourseACUSpanDtlList;
            }

            return CourseACUSpanDtlList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, CourseACUSpanDtl courseACUSpanDtl, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "CourseACUSpanDtlID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "CourseACUSpanDtlID", DbType.Int32, courseACUSpanDtl.CourseACUSpanDtlID);
            }

            db.AddInParameter(cmd, "CourseACUSpanDtlID", DbType.Int32, courseACUSpanDtl.CourseACUSpanDtlID);
            db.AddInParameter(cmd, "CourseACUSpanMasID", DbType.Int32, courseACUSpanDtl.CourseACUSpanMasID);
            db.AddInParameter(cmd, "CreditUnits", DbType.Decimal, courseACUSpanDtl.CreditUnits);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, courseACUSpanDtl.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, courseACUSpanDtl.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, courseACUSpanDtl.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, courseACUSpanDtl.ModifiedDate);
            

            return db;
        }

        private IRowMapper<CourseACUSpanDtl> GetMaper()
        {
            IRowMapper<CourseACUSpanDtl> mapper = MapBuilder<CourseACUSpanDtl>.MapAllProperties()
            .Map(m => m.CourseACUSpanDtlID).ToColumn("CourseACUSpanDtlID")
            .Map(m => m.CourseACUSpanMasID).ToColumn("CourseACUSpanMasID")
            .Map(m => m.CreditUnits).ToColumn("CreditUnits")
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
