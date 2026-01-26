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
    public partial class SqlCampusRepository : ICampusRepository
    {

        Database db = null;

        private string sqlInsert = "CampusInsert";
        private string sqlUpdate = "CampusUpdate";
        private string sqlDelete = "CampusDelete";
        private string sqlGetById = "CampusGetById";
        private string sqlGetAll = "CampusGetAll";
               
        public int Insert(Campus campus)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, campus, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "CampusId");

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

        public bool Update(Campus campus)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, campus, isInsert);

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

                db.AddInParameter(cmd, "CampusId", DbType.Int32, id);
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

        public Campus GetById(int id)
        {
            Campus _campus = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Campus> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Campus>(sqlGetById, rowMapper);
                _campus = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _campus;
            }

            return _campus;
        }

        public List<Campus> GetAll()
        {
            List<Campus> campusList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Campus> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Campus>(sqlGetAll, mapper);
                IEnumerable<Campus> collection = accessor.Execute();

                campusList = collection.ToList();
            }

            catch (Exception ex)
            {
                return campusList;
            }

            return campusList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, Campus campus, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "CampusId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "CampusId", DbType.Int32, campus.CampusId);
            }

            	
		db.AddInParameter(cmd,"CampusName",DbType.String,campus.CampusName);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,campus.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,campus.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,campus.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,campus.ModifiedDate);
            
            return db;
        }

        private IRowMapper<Campus> GetMaper()
        {
            IRowMapper<Campus> mapper = MapBuilder<Campus>.MapAllProperties()

       	   .Map(m => m.CampusId).ToColumn("CampusId")
		.Map(m => m.CampusName).ToColumn("CampusName")
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

