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
    public partial class SQLCriteriaTypeRepository : ICriteriaTypeRepository
    {

        Database db = null;

        private string sqlInsert = "CriteriaTypeInsert";
        private string sqlUpdate = "CriteriaTypeUpdate";
        private string sqlDelete = "CriteriaTypeDeleteById";
        private string sqlGetById = "CriteriaTypeGetById";
        private string sqlGetAll = "CriteriaTypeGetAll";

        public int Insert(CriteriaType criteriatype)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, criteriatype, isInsert);
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

        public bool Update(CriteriaType criteriatype)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, criteriatype, isInsert);

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

        public CriteriaType GetById(int? id)
        {
            CriteriaType _criteriatype = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CriteriaType> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CriteriaType>(sqlGetById, rowMapper);
                _criteriatype = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _criteriatype;
            }

            return _criteriatype;
        }

        public List<CriteriaType> GetAll()
        {
            List<CriteriaType> criteriatypeList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CriteriaType> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CriteriaType>(sqlGetAll, mapper);
                IEnumerable<CriteriaType> collection = accessor.Execute();

                criteriatypeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return criteriatypeList;
            }

            return criteriatypeList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, CriteriaType criteriatype, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, criteriatype.Id);
            }


            db.AddInParameter(cmd, "CriteriaName", DbType.String, criteriatype.CriteriaName);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, criteriatype.CreatedBy);
            db.AddInParameter(cmd, "CreatedTime", DbType.DateTime, criteriatype.CreatedTime);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, criteriatype.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedTime", DbType.DateTime, criteriatype.ModifiedTime);

            return db;
        }

        private IRowMapper<CriteriaType> GetMaper()
        {
            IRowMapper<CriteriaType> mapper = MapBuilder<CriteriaType>.MapAllProperties()

           .Map(m => m.Id).ToColumn("Id")
        .Map(m => m.CriteriaName).ToColumn("CriteriaName")
        .Map(m => m.CreatedBy).ToColumn("CreatedBy")
        .Map(m => m.CreatedTime).ToColumn("CreatedTime")
        .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
        .Map(m => m.ModifiedTime).ToColumn("ModifiedTime")

            .Build();

            return mapper;
        }
        #endregion

    }
}
