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
    public partial class SQLRelationBetweenDiscountCourseTypeRepository : IRelationBetweenDiscountCourseTypeRepository
    {
        Database db = null;

        private string sqlInsert = "RelationBetweenDiscountCourseTypeInsert";
        private string sqlUpdate = "RelationBetweenDiscountCourseTypeUpdate";
        private string sqlDelete = "RelationBetweenDiscountCourseTypeDeleteById";
        private string sqlGetById = "RelationBetweenDiscountCourseTypeGetById";
        private string sqlGetAll = "RelationBetweenDiscountCourseTypeGetAll";
        
        
        public int Insert(RelationBetweenDiscountCourseType relationBetweenDiscountCourseType)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, relationBetweenDiscountCourseType, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "RelationBetweenDiscountCourseTypeID");

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

        public bool Update(RelationBetweenDiscountCourseType relationBetweenDiscountCourseType)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, relationBetweenDiscountCourseType, isInsert);

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

                db.AddInParameter(cmd, "RelationBetweenDiscountCourseTypeID", DbType.Int32, id);
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

        public RelationBetweenDiscountCourseType GetById(int? id)
        {
            RelationBetweenDiscountCourseType _relationBetweenDiscountCourseType = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RelationBetweenDiscountCourseType> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RelationBetweenDiscountCourseType>(sqlGetById, rowMapper);
                _relationBetweenDiscountCourseType = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _relationBetweenDiscountCourseType;
            }

            return _relationBetweenDiscountCourseType;
        }

        public List<RelationBetweenDiscountCourseType> GetAll()
        {
            List<RelationBetweenDiscountCourseType> relationBetweenDiscountCourseTypeList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RelationBetweenDiscountCourseType> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RelationBetweenDiscountCourseType>(sqlGetAll, mapper);
                IEnumerable<RelationBetweenDiscountCourseType> collection = accessor.Execute();

                relationBetweenDiscountCourseTypeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return relationBetweenDiscountCourseTypeList;
            }

            return relationBetweenDiscountCourseTypeList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, RelationBetweenDiscountCourseType relationBetweenDiscountCourseType, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "RelationBetweenDiscountCourseTypeID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "RelationBetweenDiscountCourseTypeID", DbType.Int32, relationBetweenDiscountCourseType.RelationBetweenDiscountCourseTypeID);
            }

            db.AddInParameter(cmd, "AcaCalID", DbType.Int32, relationBetweenDiscountCourseType.AcaCalID);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, relationBetweenDiscountCourseType.ProgramID);
            db.AddInParameter(cmd, "TypeDefDiscountID", DbType.Int32, relationBetweenDiscountCourseType.TypeDefDiscountID);
            db.AddInParameter(cmd, "TypeDefCourseID", DbType.Int32, relationBetweenDiscountCourseType.TypeDefCourseID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, relationBetweenDiscountCourseType.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, relationBetweenDiscountCourseType.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, relationBetweenDiscountCourseType.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, relationBetweenDiscountCourseType.ModifiedDate);
           
            return db;
        }

        private IRowMapper<RelationBetweenDiscountCourseType> GetMaper()
        {
            IRowMapper<RelationBetweenDiscountCourseType> mapper = MapBuilder<RelationBetweenDiscountCourseType>.MapAllProperties()
            .Map(m => m.RelationBetweenDiscountCourseTypeID).ToColumn("RelationBetweenDiscountCourseTypeID")
            .Map(m => m.AcaCalID).ToColumn("AcaCalID")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.TypeDefDiscountID).ToColumn("TypeDefDiscountID")
            .Map(m => m.TypeDefCourseID).ToColumn("TypeDefCourseID")
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
