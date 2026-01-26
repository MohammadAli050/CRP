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
    public partial class SqlFeeTypeRepository : IFeeTypeRepository
    {

        Database db = null;

        private string sqlInsert = "FeeTypeInsert";
        private string sqlUpdate = "FeeTypeUpdate";
        private string sqlDelete = "FeeTypeDeleteById";
        private string sqlGetById = "FeeTypeGetById";
        private string sqlGetAll = "FeeTypeGetAll";
               
        public int Insert(FeeType feetype)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, feetype, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "FeeTypeId");

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

        public bool Update(FeeType feetype)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, feetype, isInsert);

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

                db.AddInParameter(cmd, "FeeTypeId", DbType.Int32, id);
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

        public FeeType GetById(int? id)
        {
            FeeType _feetype = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FeeType> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<FeeType>(sqlGetById, rowMapper);
                _feetype = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _feetype;
            }

            return _feetype;
        }

        public List<FeeType> GetAll()
        {
            List<FeeType> feetypeList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FeeType> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<FeeType>(sqlGetAll, mapper);
                IEnumerable<FeeType> collection = accessor.Execute();

                feetypeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return feetypeList;
            }

            return feetypeList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, FeeType feetype, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "FeeTypeId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "FeeTypeId", DbType.Int32, feetype.FeeTypeId);
            }

            db.AddInParameter(cmd, "FeeName", DbType.String, feetype.FeeName);
		    db.AddInParameter(cmd,"FeeDefinition",DbType.String,feetype.FeeDefinition);
		    db.AddInParameter(cmd,"IsCourseSpecific",DbType.Boolean,feetype.IsCourseSpecific);
		    db.AddInParameter(cmd,"IsLifetimeOnce",DbType.Boolean,feetype.IsLifetimeOnce);
		    db.AddInParameter(cmd,"IsPerSemester",DbType.Boolean,feetype.IsPerSemester);
		    db.AddInParameter(cmd,"Priority",DbType.Int32,feetype.Priority);
		    db.AddInParameter(cmd,"Sequence",DbType.Int32,feetype.Sequence);
		    db.AddInParameter(cmd,"Attribute1",DbType.String,feetype.Attribute1);
		    db.AddInParameter(cmd,"Attribute2",DbType.String,feetype.Attribute2);
		    db.AddInParameter(cmd,"Attribute3",DbType.String,feetype.Attribute3);
		    db.AddInParameter(cmd,"Attribute4",DbType.String,feetype.Attribute4);
		    db.AddInParameter(cmd,"CreatedBy",DbType.Int32,feetype.CreatedBy);
		    db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,feetype.CreatedDate);
		    db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,feetype.ModifiedBy);
		    db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,feetype.ModifiedDate);
            
            return db;
        }

        private IRowMapper<FeeType> GetMaper()
        {
            IRowMapper<FeeType> mapper = MapBuilder<FeeType>.MapAllProperties()

       	   .Map(m => m.FeeTypeId).ToColumn("FeeTypeId")
        .Map(m => m.FeeName).ToColumn("FeeName")
		.Map(m => m.FeeDefinition).ToColumn("FeeDefinition")
		.Map(m => m.IsCourseSpecific).ToColumn("IsCourseSpecific")
		.Map(m => m.IsLifetimeOnce).ToColumn("IsLifetimeOnce")
		.Map(m => m.IsPerSemester).ToColumn("IsPerSemester")
		.Map(m => m.Priority).ToColumn("Priority")
		.Map(m => m.Sequence).ToColumn("Sequence")
		.Map(m => m.Attribute1).ToColumn("Attribute1")
		.Map(m => m.Attribute2).ToColumn("Attribute2")
		.Map(m => m.Attribute3).ToColumn("Attribute3")
		.Map(m => m.Attribute4).ToColumn("Attribute4")
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

