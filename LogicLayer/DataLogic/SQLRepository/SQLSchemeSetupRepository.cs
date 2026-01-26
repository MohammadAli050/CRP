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
    public partial class SQLSchemeSetupRepository : ISchemeSetupRepository
    {
        Database db = null;

        private string sqlInsert = "SchemeSetupInsert";
        private string sqlUpdate = "SchemeSetupUpdate";
        private string sqlDelete = "SchemeSetupDelete";
        private string sqlGetById = "SchemeSetupGetById";
        private string sqlGetAll = "SchemeSetupGetAll";
               
        public int Insert(SchemeSetup schemesetup)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, schemesetup, isInsert);
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

        public bool Update(SchemeSetup schemesetup)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, schemesetup, isInsert);

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

        public SchemeSetup GetById(int id)
        {
            SchemeSetup _schemesetup = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<SchemeSetup> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<SchemeSetup>(sqlGetById, rowMapper);
                _schemesetup = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _schemesetup;
            }

            return _schemesetup;
        }

        public List<SchemeSetup> GetAll()
        {
            List<SchemeSetup> schemesetupList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<SchemeSetup> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<SchemeSetup>(sqlGetAll, mapper);
                IEnumerable<SchemeSetup> collection = accessor.Execute();

                schemesetupList = collection.ToList();
            }

            catch (Exception ex)
            {
                return schemesetupList;
            }

            return schemesetupList;
        }
               
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, SchemeSetup schemesetup, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, schemesetup.Id);
            }

            db.AddInParameter(cmd, "SchemeName", DbType.String, schemesetup.SchemeName);
		    db.AddInParameter(cmd,"FromBatch",DbType.Int32,schemesetup.FromBatch);
		    db.AddInParameter(cmd,"ToBatch",DbType.Int32,schemesetup.ToBatch);
            db.AddInParameter(cmd, "Percentage100", DbType.String, schemesetup.Percentage100);
            db.AddInParameter(cmd, "Percentage50", DbType.String, schemesetup.Percentage50);
            db.AddInParameter(cmd, "Percentage25", DbType.String, schemesetup.Percentage25);
		    db.AddInParameter(cmd,"Attribute1",DbType.String,schemesetup.Attribute1);
		    db.AddInParameter(cmd,"Attribute2",DbType.String,schemesetup.Attribute2);
		    db.AddInParameter(cmd,"Attribute3",DbType.String,schemesetup.Attribute3);
		    db.AddInParameter(cmd,"CreatedBy",DbType.Int32,schemesetup.CreatedBy);
		    db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,schemesetup.CreatedDate);
		    db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,schemesetup.ModifiedBy);
		    db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,schemesetup.ModifiedDate);
            
            return db;
        }

        private IRowMapper<SchemeSetup> GetMaper()
        {
            IRowMapper<SchemeSetup> mapper = MapBuilder<SchemeSetup>.MapAllProperties()

       	    .Map(m => m.Id).ToColumn("Id")
            .Map(m => m.SchemeName).ToColumn("SchemeName")
		    .Map(m => m.FromBatch).ToColumn("FromBatch")
		    .Map(m => m.ToBatch).ToColumn("ToBatch")
		    .Map(m => m.Percentage100).ToColumn("Percentage100")
		    .Map(m => m.Percentage50).ToColumn("Percentage50")
		    .Map(m => m.Percentage25).ToColumn("Percentage25")
		    .Map(m => m.Attribute1).ToColumn("Attribute1")
		    .Map(m => m.Attribute2).ToColumn("Attribute2")
		    .Map(m => m.Attribute3).ToColumn("Attribute3")
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

