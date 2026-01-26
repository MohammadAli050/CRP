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
    public partial class SQLRelationBetweenDiscountRetakeRepository : IRelationBetweenDiscountRetakeRepository
    {
        Database db = null;

        private string sqlInsert = "RelationBetweenDiscountRetakeInsert";
        private string sqlUpdate = "RelationBetweenDiscountRetakeUpdate";
        private string sqlDelete = "RelationBetweenDiscountRetakeDeleteById";
        private string sqlGetById = "RelationBetweenDiscountRetakeGetById";
        private string sqlGetAll = "RelationBetweenDiscountRetakeGetAll";
        
        
        public int Insert(RelationBetweenDiscountRetake relationBetweenDiscountRetake)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, relationBetweenDiscountRetake, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "RelationBetweenDiscountRetakeID");

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

        public bool Update(RelationBetweenDiscountRetake relationBetweenDiscountRetake)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, relationBetweenDiscountRetake, isInsert);

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

                db.AddInParameter(cmd, "RelationBetweenDiscountRetakeID", DbType.Int32, id);
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

        public RelationBetweenDiscountRetake GetById(int? id)
        {
            RelationBetweenDiscountRetake _relationBetweenDiscountRetake = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RelationBetweenDiscountRetake> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RelationBetweenDiscountRetake>(sqlGetById, rowMapper);
                _relationBetweenDiscountRetake = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _relationBetweenDiscountRetake;
            }

            return _relationBetweenDiscountRetake;
        }

        public List<RelationBetweenDiscountRetake> GetAll()
        {
            List<RelationBetweenDiscountRetake> relationBetweenDiscountRetakeList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RelationBetweenDiscountRetake> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RelationBetweenDiscountRetake>(sqlGetAll, mapper);
                IEnumerable<RelationBetweenDiscountRetake> collection = accessor.Execute();

                relationBetweenDiscountRetakeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return relationBetweenDiscountRetakeList;
            }

            return relationBetweenDiscountRetakeList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, RelationBetweenDiscountRetake relationBetweenDiscountRetake, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "RelationBetweenDiscountRetakeID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "RelationBetweenDiscountRetakeID", DbType.Int32, relationBetweenDiscountRetake.RelationBetweenDiscountRetakeID);
            }

            db.AddInParameter(cmd, "AcaCalID", DbType.Int32, relationBetweenDiscountRetake.AcaCalID);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, relationBetweenDiscountRetake.ProgramID);
            db.AddInParameter(cmd, "TypeDefDiscountID", DbType.Int32, relationBetweenDiscountRetake.TypeDefDiscountID);
            db.AddInParameter(cmd, "RetakeEqualsToZero", DbType.Boolean, relationBetweenDiscountRetake.RetakeEqualsToZero);
            db.AddInParameter(cmd, "RetakeGreaterThanZero", DbType.Boolean, relationBetweenDiscountRetake.RetakeGreaterThanZero);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, relationBetweenDiscountRetake.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, relationBetweenDiscountRetake.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, relationBetweenDiscountRetake.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, relationBetweenDiscountRetake.ModifiedDate);
            
            return db;
        }

        private IRowMapper<RelationBetweenDiscountRetake> GetMaper()
        {
            IRowMapper<RelationBetweenDiscountRetake> mapper = MapBuilder<RelationBetweenDiscountRetake>.MapAllProperties()
            .Map(m => m.RelationBetweenDiscountRetakeID).ToColumn("RelationBetweenDiscountRetakeID")
            .Map(m => m.AcaCalID).ToColumn("AcaCalID")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.TypeDefDiscountID).ToColumn("TypeDefDiscountID")
            .Map(m => m.RetakeEqualsToZero).ToColumn("RetakeEqualsToZero")
            .Map(m => m.RetakeGreaterThanZero).ToColumn("RetakeGreaterThanZero")
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
