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
    public partial class SqlAffiliatedInstitutionRepository : IAffiliatedInstitutionRepository
    {

        Database db = null;

        private string sqlInsert = "AffiliatedInstitutionInsert";
        private string sqlUpdate = "AffiliatedInstitutionUpdate";
        private string sqlDelete = "AffiliatedInstitutionDelete";
        private string sqlGetById = "AffiliatedInstitutionGetById";

        private string sqlGetAll = "AffiliatedInstitutionGetAll";

        public int Insert(AffiliatedInstitution affiliatedinstitution)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, affiliatedinstitution, isInsert);
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

        public bool Update(AffiliatedInstitution affiliatedinstitution)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, affiliatedinstitution, isInsert);

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

        public AffiliatedInstitution GetById(int? id)
        {
            AffiliatedInstitution _affiliatedinstitution = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AffiliatedInstitution> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AffiliatedInstitution>(sqlGetById, rowMapper);
                _affiliatedinstitution = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _affiliatedinstitution;
            }

            return _affiliatedinstitution;
        }

        public List<AffiliatedInstitution> GetAll()
        {
            List<AffiliatedInstitution> affiliatedinstitutionList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AffiliatedInstitution> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AffiliatedInstitution>(sqlGetAll, mapper);
                IEnumerable<AffiliatedInstitution> collection = accessor.Execute();

                affiliatedinstitutionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return affiliatedinstitutionList;
            }

            return affiliatedinstitutionList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, AffiliatedInstitution affiliatedinstitution, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, affiliatedinstitution);
            }

            db.AddInParameter(cmd, "Name", DbType.String, affiliatedinstitution.Name);
            db.AddInParameter(cmd, "Code", DbType.String, affiliatedinstitution.Code);
            db.AddInParameter(cmd, "Attribute1", DbType.String, affiliatedinstitution.Attribute1);
            db.AddInParameter(cmd, "Attribute2", DbType.String, affiliatedinstitution.Attribute2);
            db.AddInParameter(cmd, "Attribute3", DbType.String, affiliatedinstitution.Attribute3);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, affiliatedinstitution.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, affiliatedinstitution.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, affiliatedinstitution.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, affiliatedinstitution.ModifiedDate);

            return db;
        }

        private IRowMapper<AffiliatedInstitution> GetMaper()
        {
            IRowMapper<AffiliatedInstitution> mapper = MapBuilder<AffiliatedInstitution>.MapAllProperties()

            .Map(m => m.Id).ToColumn("Id")
            .Map(m => m.Name).ToColumn("Name")
            .Map(m => m.Code).ToColumn("Code")
            .Map(m => m.Attribute1).ToColumn("Attribute1")
            .Map(m => m.Attribute2).ToColumn("Attribute2")
            .Map(m => m.Attribute3).ToColumn("Attribute3")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            
            .DoNotMap(m => m.Address)
            
            .Build();

            return mapper;
        }
        #endregion

    }
}

