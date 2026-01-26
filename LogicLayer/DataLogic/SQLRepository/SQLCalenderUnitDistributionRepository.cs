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
    public partial class SQLCalenderUnitDistributionRepository : ICalenderUnitDistributionRepository
    {
        Database db = null;

        private string sqlInsert = "CalenderUnitDistributionInsert";
        private string sqlUpdate = "CalenderUnitDistributionUpdate";
        private string sqlDelete = "CalenderUnitDistributionDeleteById";
        private string sqlGetById = "CalenderUnitDistributionGetById";
        private string sqlGetAll = "CalenderUnitDistributionGetAll";
        
        
        public int Insert(CalenderUnitDistribution calenderUnitDistribution)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, calenderUnitDistribution, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "CalenderUnitDistributionID");

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

        public bool Update(CalenderUnitDistribution calenderUnitDistribution)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, calenderUnitDistribution, isInsert);

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

                db.AddInParameter(cmd, "CalenderUnitDistributionID", DbType.Int32, id);
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

        public CalenderUnitDistribution GetById(int? id)
        {
            CalenderUnitDistribution _calenderUnitDistribution = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CalenderUnitDistribution> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CalenderUnitDistribution>(sqlGetById, rowMapper);
                _calenderUnitDistribution = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _calenderUnitDistribution;
            }

            return _calenderUnitDistribution;
        }

        public CalenderUnitDistribution GetByCourseId(int? id)
        {
            CalenderUnitDistribution _calenderUnitDistribution = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CalenderUnitDistribution> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CalenderUnitDistribution>("CalenderUnitDistributionGetByCourseId", rowMapper);
                _calenderUnitDistribution = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _calenderUnitDistribution;
            }

            return _calenderUnitDistribution;
        }

        public List<CalenderUnitDistribution> GetAll()
        {
            List<CalenderUnitDistribution> calenderUnitDistributionList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CalenderUnitDistribution> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CalenderUnitDistribution>(sqlGetAll, mapper);
                IEnumerable<CalenderUnitDistribution> collection = accessor.Execute();

                calenderUnitDistributionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return calenderUnitDistributionList;
            }

            return calenderUnitDistributionList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, CalenderUnitDistribution calenderUnitDistribution, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "CalenderUnitDistributionID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "CalenderUnitDistributionID", DbType.Int32, calenderUnitDistribution.CalenderUnitDistributionID);
            }

            db.AddInParameter(cmd, "CalenderUnitMasterID", DbType.Int32, calenderUnitDistribution.CalenderUnitMasterID);
            db.AddInParameter(cmd, "Name", DbType.String, calenderUnitDistribution.Name);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, calenderUnitDistribution.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, calenderUnitDistribution.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, calenderUnitDistribution.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, calenderUnitDistribution.ModifiedDate);
            
            return db;
        }

        private IRowMapper<CalenderUnitDistribution> GetMaper()
        {
            IRowMapper<CalenderUnitDistribution> mapper = MapBuilder<CalenderUnitDistribution>.MapAllProperties()
            .Map(m => m.CalenderUnitDistributionID).ToColumn("CalenderUnitDistributionID")
            .Map(m => m.CalenderUnitMasterID).ToColumn("CalenderUnitMasterID")
            .Map(m => m.Name).ToColumn("Name")
            .Map(m=> m.Sequence).ToColumn("Sequence")
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
