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
    public partial class SqlBuildingRepository : IBuildingRepository
    {

        Database db = null;

        private string sqlInsert = "BuildingInsert";
        private string sqlUpdate = "BuildingUpdate";
        private string sqlDelete = "BuildingDelete";
        private string sqlGetById = "BuildingGetById";
        private string sqlGetAll = "BuildingGetAll";
               
        public int Insert(Building building)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, building, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "BuildingId");

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

        public bool Update(Building building)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, building, isInsert);

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

                db.AddInParameter(cmd, "BuildingId", DbType.Int32, id);
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

        public Building GetById(int id)
        {
            Building _building = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Building> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Building>(sqlGetById, rowMapper);
                _building = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _building;
            }

            return _building;
        }

        public List<Building> GetAll()
        {
            List<Building> buildingList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Building> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Building>(sqlGetAll, mapper);
                IEnumerable<Building> collection = accessor.Execute();

                buildingList = collection.ToList();
            }

            catch (Exception ex)
            {
                return buildingList;
            }

            return buildingList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, Building building, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "BuildingId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "BuildingId", DbType.Int32, building.BuildingId);
            }

            	
		db.AddInParameter(cmd,"BuildingName",DbType.String,building.BuildingName);
		db.AddInParameter(cmd,"CampusId",DbType.Int32,building.CampusId);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,building.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,building.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,building.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,building.ModifiedDate);
            
            return db;
        }

        private IRowMapper<Building> GetMaper()
        {
            IRowMapper<Building> mapper = MapBuilder<Building>.MapAllProperties()

       	   .Map(m => m.BuildingId).ToColumn("BuildingId")
		.Map(m => m.BuildingName).ToColumn("BuildingName")
		.Map(m => m.CampusId).ToColumn("CampusId")
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

