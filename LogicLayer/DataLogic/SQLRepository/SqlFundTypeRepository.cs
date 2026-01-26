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
    public partial class SqlFundTypeRepository : IFundTypeRepository
    {

        Database db = null;

        private string sqlInsert = "FundTypeInsert";
        private string sqlUpdate = "FundTypeUpdate";
        private string sqlDelete = "FundTypeDelete";
        private string sqlGetById = "FundTypeGetById";
        private string sqlGetAll = "FundTypeGetAll";
               
        public int Insert(FundType fundtype)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, fundtype, isInsert);
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

        public bool Update(FundType fundtype)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, fundtype, isInsert);

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

        public FundType GetById(int? id)
        {
            FundType _fundtype = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FundType> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<FundType>(sqlGetById, rowMapper);
                _fundtype = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _fundtype;
            }

            return _fundtype;
        }

        public List<FundType> GetAll()
        {
            List<FundType> fundtypeList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FundType> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<FundType>(sqlGetAll, mapper);
                IEnumerable<FundType> collection = accessor.Execute();

                fundtypeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return fundtypeList;
            }

            return fundtypeList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, FundType fundtype, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "FundTypeId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "FundTypeId", DbType.Int32, fundtype.FundTypeId);
            }

		    db.AddInParameter(cmd,"FundName",DbType.String,fundtype.FundName);
		    db.AddInParameter(cmd,"AccountNo",DbType.String,fundtype.AccountNo);
		    db.AddInParameter(cmd,"ProgramId",DbType.Int32,fundtype.ProgramId);
		    db.AddInParameter(cmd,"Attribute1",DbType.String,fundtype.Attribute1);
		    db.AddInParameter(cmd,"Attribute2",DbType.String,fundtype.Attribute2);
		    db.AddInParameter(cmd,"Attribute3",DbType.String,fundtype.Attribute3);
		    db.AddInParameter(cmd,"Attribute4",DbType.String,fundtype.Attribute4);
		    db.AddInParameter(cmd,"CreatedBy",DbType.Int32,fundtype.CreatedBy);
		    db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,fundtype.CreatedDate);
		    db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,fundtype.ModifiedBy);
		    db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,fundtype.ModifiedDate);
            
            return db;
        }

        private IRowMapper<FundType> GetMaper()
        {
            IRowMapper<FundType> mapper = MapBuilder<FundType>.MapAllProperties()

       	    .Map(m => m.FundTypeId).ToColumn("FundTypeId")
		    .Map(m => m.FundName).ToColumn("FundName")
		    .Map(m => m.AccountNo).ToColumn("AccountNo")
		    .Map(m => m.ProgramId).ToColumn("ProgramId")
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

