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
    public partial class SQLSchoolRepository : ISchoolRepository
    {
        Database db = null;

        private string sqlInsert = "SchoolInsert";
        private string sqlUpdate = "SchoolUpdate";
        private string sqlDelete = "SchoolDeleteById";
        private string sqlGetById = "StudentGetById";
        private string sqlGetAll = "StudentGetAll";


        public int Insert(School school)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, school, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "SchoolID");

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

        public bool Update(School school)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, school, isInsert);

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

                db.AddInParameter(cmd, "SchoolID", DbType.Int32, id);
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

        public School GetById(int? id)
        {
            School _school = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<School> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<School>(sqlGetById, rowMapper);
                _school = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _school;
            }

            return _school;
        }

        public List<School> GetAll()
        {
            List<School> schoolList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<School> mapper = GetMaper();

                var accessor = db.CreateSqlStringAccessor<School>("Select * from School", mapper);
                IEnumerable<School> collection = accessor.Execute();

                schoolList = collection.ToList();
            }

            catch (Exception ex)
            {
                return schoolList;
            }

            return schoolList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, School school, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "SchoolID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "SchoolID", DbType.Int32, school.SchoolID);
            }

            db.AddInParameter(cmd, "SchoolID", DbType.Int32, school.SchoolID);
            db.AddInParameter(cmd, "Name", DbType.String, school.Name);
            db.AddInParameter(cmd, "Code", DbType.String, school.Code);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, school.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, school.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, school.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, school.ModifiedDate);

            return db;
        }

        private IRowMapper<School> GetMaper()
        {
            IRowMapper<School> mapper = MapBuilder<School>.MapAllProperties()
            .Map(m => m.SchoolID).ToColumn("SchoolID")
            .Map(m => m.Name).ToColumn("Name")
            .Map(m => m.Code).ToColumn("Code")
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
