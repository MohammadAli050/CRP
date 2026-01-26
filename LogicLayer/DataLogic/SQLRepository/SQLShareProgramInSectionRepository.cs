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
    public partial class SQLShareProgramInSectionRepository : IShareProgramInSectionRepository
    {
        Database db = null;

        private string sqlInsert = "ShareProgramInSectionInsert";
        private string sqlUpdate = "ShareProgramInSectionUpdate";
        private string sqlDelete = "ShareProgramInSectionDelete";
        private string sqlGetById = "ShareProgramInSectionGetById";
        private string sqlGetAll = "ShareProgramInSectionGetAll";

        public int Insert(ShareProgramInSection shareprograminsection)
        {
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, shareprograminsection );
                db.ExecuteNonQuery(cmd);                
            }
            catch (Exception ex)
            {
                return  0;
            }

            return 1;
        }

        public bool Update(ShareProgramInSection shareprograminsection)
        {
            bool result = false;
            
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, shareprograminsection);

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

        public bool Delete(int academicCalenderSectionId, int programId)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlDelete);

                db.AddInParameter(cmd, "AcademicCalenderSectionId", DbType.Int32, academicCalenderSectionId);
                db.AddInParameter(cmd, "ProgramId", DbType.Int32, programId);
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

        public ShareProgramInSection GetById(int academicCalenderSectionId, int programId)
        {
            ShareProgramInSection _shareprograminsection = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ShareProgramInSection> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ShareProgramInSection>(sqlGetById, rowMapper);
                _shareprograminsection = accessor.Execute(academicCalenderSectionId, programId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _shareprograminsection;
            }

            return _shareprograminsection;
        }

        public List<ShareProgramInSection> GetAll()
        {
            List<ShareProgramInSection> shareprograminsectionList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ShareProgramInSection> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ShareProgramInSection>(sqlGetAll, mapper);
                IEnumerable<ShareProgramInSection> collection = accessor.Execute();

                shareprograminsectionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return shareprograminsectionList;
            }

            return shareprograminsectionList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ShareProgramInSection shareprograminsection)
        {
            db.AddInParameter(cmd, "AcademicCalenderSectionId", DbType.Int32, shareprograminsection.AcademicCalenderSectionId);
            db.AddInParameter(cmd, "ProgramId", DbType.Int32, shareprograminsection.ProgramId);

            return db;
        }

        private IRowMapper<ShareProgramInSection> GetMaper()
        {
            IRowMapper<ShareProgramInSection> mapper = MapBuilder<ShareProgramInSection>.MapAllProperties()

            .Map(m => m.AcademicCalenderSectionId).ToColumn("AcademicCalenderSectionId")
            .Map(m => m.ProgramId).ToColumn("ProgramId")

            .Build();

            return mapper;
        }
        #endregion
        
        public bool DeleteByAcademicCalenderSectionId(int academicCalenderSectionId)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("ShareProgramInSectionDeleteByAcademicCalenderSectionId");

                db.AddInParameter(cmd, "AcademicCalenderSectionId", DbType.Int32, academicCalenderSectionId);
                
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

