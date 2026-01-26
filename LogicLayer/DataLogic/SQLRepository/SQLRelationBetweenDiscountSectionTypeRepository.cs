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
    public partial class SQLRelationBetweenDiscountSectionTypeRepository : IRelationBetweenDiscountSectionTypeRepository
    {
        Database db = null;

        private string sqlInsert = "RelationBetweenDiscountSectionTypeInsert";
        private string sqlUpdate = "RelationBetweenDiscountSectionTypeUpdate";
        private string sqlDelete = "RelationBetweenDiscountSectionTypeDeleteById";
        private string sqlGetById = "RelationBetweenDiscountSectionTypeGetById";
        private string sqlGetAll = "RelationBetweenDiscountSectionTypeGetAll";
        
        
        public int Insert(RelationBetweenDiscountSectionType relationBetweenDiscountSectionType)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, relationBetweenDiscountSectionType, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "RelationBetweenDiscountSectionTypeID");

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

        public bool Update(RelationBetweenDiscountSectionType relationBetweenDiscountSectionType)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, relationBetweenDiscountSectionType, isInsert);

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

                db.AddInParameter(cmd, "RelationBetweenDiscountSectionTypeID", DbType.Int32, id);
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

        public RelationBetweenDiscountSectionType GetById(int? id)
        {
            RelationBetweenDiscountSectionType _RelationBetweenDiscountSectionType = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RelationBetweenDiscountSectionType> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RelationBetweenDiscountSectionType>(sqlGetById, rowMapper);
                _RelationBetweenDiscountSectionType = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _RelationBetweenDiscountSectionType;
            }

            return _RelationBetweenDiscountSectionType;
        }

        public List<RelationBetweenDiscountSectionType> GetAll()
        {
            List<RelationBetweenDiscountSectionType> relationBetweenDiscountSectionTypeList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RelationBetweenDiscountSectionType> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RelationBetweenDiscountSectionType>(sqlGetAll, mapper);
                IEnumerable<RelationBetweenDiscountSectionType> collection = accessor.Execute();

                relationBetweenDiscountSectionTypeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return relationBetweenDiscountSectionTypeList;
            }

            return relationBetweenDiscountSectionTypeList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, RelationBetweenDiscountSectionType relationBetweenDiscountSectionType, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "RelationBetweenDiscountSectionTypeID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "RelationBetweenDiscountSectionTypeID", DbType.Int32, relationBetweenDiscountSectionType.RelationBetweenDiscountSectionTypeID);
            }

            db.AddInParameter(cmd, "AcaCalID", DbType.Int32, relationBetweenDiscountSectionType.AcaCalID);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, relationBetweenDiscountSectionType.ProgramID);
            db.AddInParameter(cmd, "TypeDefDiscountID", DbType.Int32, relationBetweenDiscountSectionType.TypeDefDiscountID);
            db.AddInParameter(cmd, "TypeDefID", DbType.Int32, relationBetweenDiscountSectionType.TypeDefID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, relationBetweenDiscountSectionType.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, relationBetweenDiscountSectionType.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, relationBetweenDiscountSectionType.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, relationBetweenDiscountSectionType.ModifiedDate);
            
            return db;
        }

        private IRowMapper<RelationBetweenDiscountSectionType> GetMaper()
        {
            IRowMapper<RelationBetweenDiscountSectionType> mapper = MapBuilder<RelationBetweenDiscountSectionType>.MapAllProperties()
            .Map(m => m.RelationBetweenDiscountSectionTypeID).ToColumn("RelationBetweenDiscountSectionTypeID")
            .Map(m => m.AcaCalID).ToColumn("AcaCalID")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.TypeDefDiscountID).ToColumn("TypeDefDiscountID")
            .Map(m => m.TypeDefID).ToColumn("TypeDefID")
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
