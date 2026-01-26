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
   public partial class SQLCourseACUSpanMasRepository:ICourseACUSpanMasRepository
    {
        Database db = null;

        private string sqlInsert = "CourseACUSpanMasInsert";
        private string sqlUpdate = "CourseACUSpanMasUpdate";
        private string sqlDelete = "CourseACUSpanMasDeleteById";
        private string sqlGetById = "CourseACUSpanMasGetById";
        private string sqlGetAll = "CourseACUSpanMasGetAll";

        public int Insert(CourseACUSpanMas courseACUSpanMas)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, courseACUSpanMas, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "CourseACUSpanMasID");

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

        public bool Update(CourseACUSpanMas courseACUSpanMas)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, courseACUSpanMas, isInsert);

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

                db.AddInParameter(cmd, "CourseACUSpanMasID", DbType.Int32, id);
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

        public CourseACUSpanMas GetById(int? id)
        {
            CourseACUSpanMas _courseACUSpanMas = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseACUSpanMas> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseACUSpanMas>(sqlGetById, rowMapper);
                _courseACUSpanMas = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _courseACUSpanMas;
            }

            return _courseACUSpanMas;
        }

        public List<CourseACUSpanMas> GetAll()
        {
            List<CourseACUSpanMas> courseACUSpanMasList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseACUSpanMas> mapper = GetMaper();

                var accessor = db.CreateSqlStringAccessor<CourseACUSpanMas>(sqlGetAll, mapper);
                IEnumerable<CourseACUSpanMas> collection = accessor.Execute();

                courseACUSpanMasList = collection.ToList();
            }

            catch (Exception ex)
            {
                return courseACUSpanMasList;
            }

            return courseACUSpanMasList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, CourseACUSpanMas courseACUSpanMas, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "CourseACUSpanMasID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "CourseACUSpanMasID", DbType.Int32, courseACUSpanMas.CourseACUSpanMasID);
            }

            db.AddInParameter(cmd, "CourseACUSpanMasID", DbType.Int32, courseACUSpanMas.CourseACUSpanMasID);
            db.AddInParameter(cmd, "CourseID", DbType.Int32, courseACUSpanMas.CourseID);
            db.AddInParameter(cmd, "VersionID", DbType.Int32, courseACUSpanMas.VersionID);
            db.AddInParameter(cmd, "MaxACUNo", DbType.Int32, courseACUSpanMas.MaxACUNo);
            db.AddInParameter(cmd, "MinACUNo", DbType.Int32, courseACUSpanMas.MinACUNo);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, courseACUSpanMas.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, courseACUSpanMas.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, courseACUSpanMas.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, courseACUSpanMas.ModifiedDate);

            return db;
        }

        private IRowMapper<CourseACUSpanMas> GetMaper()
        {
            IRowMapper<CourseACUSpanMas> mapper = MapBuilder<CourseACUSpanMas>.MapAllProperties()
            .Map(m => m.CourseACUSpanMasID).ToColumn("CourseACUSpanMasID")
            .Map(m => m.CourseID).ToColumn("CourseID")
            .Map(m => m.VersionID).ToColumn("VersionID")
            .Map(m => m.MaxACUNo).ToColumn("MaxACUNo")
            .Map(m => m.MinACUNo).ToColumn("MinACUNo")
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
