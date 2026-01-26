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
    public partial class SQLShareBatchInSectionRepository : IShareBatchInSectionRepository
    {

        Database db = null;

        private string sqlInsert = "ShareBatchInSectionInsert";
        private string sqlUpdate = "ShareBatchInSectionUpdate";
        private string sqlDelete = "ShareBatchInSectionDelete";
        private string sqlGetById = "ShareBatchInSectionGetById";
        private string sqlGetAll = "ShareBatchInSectionGetAll";

        public int Insert(ShareBatchInSection sharebatchinsection)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, sharebatchinsection, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "{5}");

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

        public bool Update(ShareBatchInSection sharebatchinsection)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, sharebatchinsection, isInsert);

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

                db.AddInParameter(cmd, "{5}", DbType.Int32, id);
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

        public ShareBatchInSection GetById(int id)
        {
            ShareBatchInSection _sharebatchinsection = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ShareBatchInSection> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ShareBatchInSection>(sqlGetById, rowMapper);
                _sharebatchinsection = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _sharebatchinsection;
            }

            return _sharebatchinsection;
        }

        public List<ShareBatchInSection> GetAll()
        {
            List<ShareBatchInSection> sharebatchinsectionList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ShareBatchInSection> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ShareBatchInSection>(sqlGetAll, mapper);
                IEnumerable<ShareBatchInSection> collection = accessor.Execute();

                sharebatchinsectionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return sharebatchinsectionList;
            }

            return sharebatchinsectionList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ShareBatchInSection sharebatchinsection, bool isInsert)
        {
            db.AddInParameter(cmd, "AcademicCalenderSectionId", DbType.Int32, sharebatchinsection.AcademicCalenderSectionId);
            db.AddInParameter(cmd, "BatchId", DbType.Int32, sharebatchinsection.BatchId);

            return db;
        }

        private IRowMapper<ShareBatchInSection> GetMaper()
        {
            IRowMapper<ShareBatchInSection> mapper = MapBuilder<ShareBatchInSection>.MapAllProperties()

            .Map(m => m.AcademicCalenderSectionId).ToColumn("AcademicCalenderSectionId")
            .Map(m => m.BatchId).ToColumn("BatchId")

            .Build();

            return mapper;
        }
        #endregion


        #region IShareBatchInSectionRepository Members


        public bool DeleteByAcademicCalenderSectionId(int id)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("ShareBatchInSectionDeleteByAcademicCalenderSectionId");

                db.AddInParameter(cmd, "AcademicCalenderSectionId", DbType.Int32, id);
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

        #endregion
    }
}

