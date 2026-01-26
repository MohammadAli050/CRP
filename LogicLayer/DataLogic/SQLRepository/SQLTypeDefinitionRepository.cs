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
    public partial class SQLTypeDefinitionRepository : ITypeDefinitionRepository
    {
        Database db = null;

        private string sqlInsert = "TypeDefinitionInsert";
        private string sqlUpdate = "TypeDefinitionUpdate";
        private string sqlDelete = "TypeDefinitionDeleteById";
        private string sqlGetById = "TypeDefinitionGetById";
        private string sqlGetAll = "TypeDefinitionGetAll";
        private string sqlGetAllByType = "TypeDefinitionGetAllByType";
        
        public int Insert(TypeDefinition typeDefinition)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, typeDefinition, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "TypeDefinitionID");

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

        public bool Update(TypeDefinition typeDefinition)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, typeDefinition, isInsert);

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

                db.AddInParameter(cmd, "TypeDefinitionID", DbType.Int32, id);
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

        public TypeDefinition GetById(int id)
        {
            TypeDefinition _typeDefinition = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TypeDefinition> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TypeDefinition>(sqlGetById, rowMapper);
                _typeDefinition = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _typeDefinition;
            }

            return _typeDefinition;
        }

        public List<TypeDefinition> GetAll()
        {
            List<TypeDefinition> typeDefinitionList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TypeDefinition> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TypeDefinition>(sqlGetAll, mapper);
                IEnumerable<TypeDefinition> collection = accessor.Execute();

                typeDefinitionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return typeDefinitionList;
            }

            return typeDefinitionList;
        }

        public List<TypeDefinition> GetAll(string type)
        {
            List<TypeDefinition> typeDefinitionList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TypeDefinition> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TypeDefinition>(sqlGetAllByType, mapper);
                IEnumerable<TypeDefinition> collection = accessor.Execute(type);

                typeDefinitionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return typeDefinitionList;
            }

            return typeDefinitionList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, TypeDefinition typeDefinition, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "TypeDefinitionID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "TypeDefinitionID", DbType.Int32, typeDefinition.TypeDefinitionID);
            }

            db.AddInParameter(cmd, "Type", DbType.String, typeDefinition.Type);
            db.AddInParameter(cmd, "Definition", DbType.String, typeDefinition.Definition);
            db.AddInParameter(cmd, "AccountsID", DbType.Int32, typeDefinition.AccountsID);
            db.AddInParameter(cmd, "IsCourseSpecific", DbType.Boolean, typeDefinition.IsCourseSpecific);
            db.AddInParameter(cmd, "IsLifetimeOnce", DbType.Boolean, typeDefinition.IsLifetimeOnce);
            db.AddInParameter(cmd, "IsPerAcaCal", DbType.Boolean, typeDefinition.IsPerAcaCal);
            db.AddInParameter(cmd, "Priority", DbType.Int32, typeDefinition.Priority);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, typeDefinition.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, typeDefinition.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, typeDefinition.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, typeDefinition.ModifiedDate);
            
            return db;
        }

        private IRowMapper<TypeDefinition> GetMaper()
        {
            IRowMapper<TypeDefinition> mapper = MapBuilder<TypeDefinition>.MapAllProperties()
            .Map(m => m.TypeDefinitionID).ToColumn("TypeDefinitionID")
            .Map(m => m.Type).ToColumn("Type")
            .Map(m => m.Definition).ToColumn("Definition")
            .Map(m => m.AccountsID).ToColumn("AccountsID")
            .Map(m => m.IsCourseSpecific).ToColumn("IsCourseSpecific")
            .Map(m => m.IsLifetimeOnce).ToColumn("IsLifetimeOnce")
            .Map(m => m.IsPerAcaCal).ToColumn("IsPerAcaCal")
            .Map(m => m.Priority).ToColumn("Priority")
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
