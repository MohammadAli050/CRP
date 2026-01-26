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
    public partial class SQLGradeWiseRetakeDiscountRepository : IGradeWiseRetakeDiscountRepository
    {
        Database db = null;

        private string sqlInsert = "GradeWiseRetakeDiscountInsert";
        private string sqlUpdate = "GradeWiseRetakeDiscountUpdate";
        private string sqlDelete = "GradeWiseRetakeDiscountDelete";
        private string sqlGetById = "GradeWiseRetakeDiscountGetById";
        private string sqlGetAll = "GradeWiseRetakeDiscountGetAll";
        private string sqlGetAllByProgramSession = "GradeWiseRetakeDiscountGetByProgramSession";

        public int Insert(GradeWiseRetakeDiscount gradewiseretakediscount)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, gradewiseretakediscount, isInsert);
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

        public bool Update(GradeWiseRetakeDiscount gradewiseretakediscount)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, gradewiseretakediscount, isInsert);

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

        public GradeWiseRetakeDiscount GetById(int id)
        {
            GradeWiseRetakeDiscount _gradewiseretakediscount = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<GradeWiseRetakeDiscount> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<GradeWiseRetakeDiscount>(sqlGetById, rowMapper);
                _gradewiseretakediscount = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _gradewiseretakediscount;
            }

            return _gradewiseretakediscount;
        }

        public List<GradeWiseRetakeDiscount> GetAll()
        {
            List<GradeWiseRetakeDiscount> gradewiseretakediscountList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<GradeWiseRetakeDiscount> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<GradeWiseRetakeDiscount>(sqlGetAll, mapper);
                IEnumerable<GradeWiseRetakeDiscount> collection = accessor.Execute();

                gradewiseretakediscountList = collection.ToList();
            }

            catch (Exception ex)
            {
                return gradewiseretakediscountList;
            }

            return gradewiseretakediscountList;
        }
        
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, GradeWiseRetakeDiscount gradewiseretakediscount, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "GradeWiseRetakeDiscountId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "GradeWiseRetakeDiscountId", DbType.Int32, gradewiseretakediscount.GradeWiseRetakeDiscountId);
            }
	
		db.AddInParameter(cmd,"GradeId",DbType.Int32,gradewiseretakediscount.GradeId);
		db.AddInParameter(cmd,"SessionId",DbType.Int32,gradewiseretakediscount.SessionId);
		db.AddInParameter(cmd,"ProgramId",DbType.Int32,gradewiseretakediscount.ProgramId);
        db.AddInParameter(cmd,"RetakeDiscount", DbType.Decimal, gradewiseretakediscount.RetakeDiscount);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,gradewiseretakediscount.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,gradewiseretakediscount.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,gradewiseretakediscount.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,gradewiseretakediscount.ModifiedDate);
        db.AddInParameter(cmd, "RetakeDiscountOnTrOrWav", DbType.Decimal, gradewiseretakediscount.RetakeDiscountOnTrOrWav);
            
            return db;
        }

        private IRowMapper<GradeWiseRetakeDiscount> GetMaper()
        {
            IRowMapper<GradeWiseRetakeDiscount> mapper = MapBuilder<GradeWiseRetakeDiscount>.MapAllProperties()

        .Map(m => m.GradeWiseRetakeDiscountId).ToColumn("GradeWiseRetakeDiscountId")
        .Map(m => m.GradeId).ToColumn("GradeId")
        .Map(m => m.SessionId).ToColumn("SessionId")
        .Map(m => m.ProgramId).ToColumn("ProgramId")
        .Map(m => m.RetakeDiscount).ToColumn("RetakeDiscount")
        .Map(m => m.CreatedBy).ToColumn("CreatedBy")
        .Map(m => m.CreatedDate).ToColumn("CreatedDate")
        .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
        .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
        .Map(m => m.RetakeDiscountOnTrOrWav).ToColumn("RetakeDiscountOnTrOrWav")

            .Build();

            return mapper;
        }
        #endregion
        
        public List<GradeWiseRetakeDiscount> GetAllBy(int? programId, int sessionId)
        {
            List<GradeWiseRetakeDiscount> gradewiseretakediscountList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<GradeWiseRetakeDiscount> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<GradeWiseRetakeDiscount>(sqlGetAllByProgramSession, mapper);
                IEnumerable<GradeWiseRetakeDiscount> collection = accessor.Execute(programId, sessionId);

                gradewiseretakediscountList = collection.ToList();
            }

            catch (Exception ex)
            {
                return gradewiseretakediscountList;
            }

            return gradewiseretakediscountList;
        }
    }
}

