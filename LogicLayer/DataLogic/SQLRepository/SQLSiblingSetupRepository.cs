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
    public partial class SQLSiblingSetupRepository : ISiblingSetupRepository
    {
        Database db = null;

        private string sqlInsert = "SiblingSetupInsert";
        private string sqlUpdate = "SiblingSetupUpdate";
        private string sqlDelete = "SiblingSetupDeleteById";
        private string sqlGetById = "SiblingSetupGetById";
        private string sqlGetAll = "SiblingSetupGetAll";

        public int Insert(SiblingSetup siblingSetup)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, siblingSetup, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "SiblingSetupId");

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

        public bool Update(SiblingSetup siblingSetup)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, siblingSetup, isInsert);

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

                db.AddInParameter(cmd, "SiblingSetupId", DbType.Int32, id);
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

        public SiblingSetup GetById(int? id)
        {
            SiblingSetup _siblingSetup = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<SiblingSetup> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<SiblingSetup>(sqlGetById, rowMapper);
                _siblingSetup = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _siblingSetup;
            }

            return _siblingSetup;
        }

        public List<SiblingSetup> GetAll()
        {
            List<SiblingSetup> siblingSetupList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<SiblingSetup> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<SiblingSetup>(sqlGetAll, mapper);
                IEnumerable<SiblingSetup> collection = accessor.Execute();

                siblingSetupList = collection.ToList();
            }

            catch (Exception ex)
            {
                return siblingSetupList;
            }

            return siblingSetupList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, SiblingSetup siblingSetup, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "SiblingSetupId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "SiblingSetupId", DbType.Int32, siblingSetup.SiblingSetupId);
            }

            db.AddInParameter(cmd, "GroupID", DbType.Int32, siblingSetup.GroupID);
            db.AddInParameter(cmd, "ApplicantId", DbType.Int32, siblingSetup.ApplicantId);
            db.AddInParameter(cmd, "TypeDefinitionID", DbType.Int32, siblingSetup.TypeDefinitionID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, siblingSetup.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, siblingSetup.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, siblingSetup.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, siblingSetup.ModifiedDate);

            return db;
        }

        private IRowMapper<SiblingSetup> GetMaper()
        {
            IRowMapper<SiblingSetup> mapper = MapBuilder<SiblingSetup>.MapAllProperties()
            .Map(m => m.SiblingSetupId).ToColumn("SiblingSetupId")
            .Map(m => m.GroupID).ToColumn("GroupID")
            .Map(m => m.ApplicantId).ToColumn("ApplicantId")
            .Map(m => m.TypeDefinitionID).ToColumn("TypeDefinitionID")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Build();

            return mapper;
        }
        #endregion

        public SiblingSetup GetByApplicantId(int ApplicantId)
        {
            SiblingSetup _siblingSetup = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<SiblingSetup> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<SiblingSetup>("SiblingSetupGetByApplicantId", rowMapper);
                _siblingSetup = accessor.Execute(ApplicantId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _siblingSetup;
            }

            return _siblingSetup;
        }

        public List<SiblingSetup> GetAllByGroupId(int groupId)
        {
            List<SiblingSetup> siblingSetupList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<SiblingSetup> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<SiblingSetup>("SiblingSetupGetByGroupId", mapper);
                IEnumerable<SiblingSetup> collection = accessor.Execute(groupId);

                siblingSetupList = collection.ToList();
            }

            catch (Exception ex)
            {
                return siblingSetupList;
            }

            return siblingSetupList;
        }

        public bool DeleteByApplicantIdGroupId(int applicant, int groupId)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("SiblingSetupDeleteByApplicantAndGroup");

                db.AddInParameter(cmd, "@GroupID", DbType.Int32, groupId);
                db.AddInParameter(cmd, "@ApplicantId", DbType.Int32, applicant);

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

    }
}
