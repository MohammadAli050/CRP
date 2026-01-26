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
using LogicLayer.BusinessObjects.DTO;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SqlEquiCourseMasterRepository : IEquiCourseMasterRepository
    {

        Database db = null;

        private string sqlInsert = "EquiCourseMasterInsert";
        private string sqlUpdate = "EquiCourseMasterUpdate";
        private string sqlDelete = "EquiCourseMasterDeleteById";
        private string sqlGetById = "EquiCourseMasterGetById";
        private string sqlGetAll = "EquiCourseMasterGetAll";
        private string sqlGetMaxGroupNo = "EquiCourseMasterGetMaxGroupNo";
               
        public int Insert(EquiCourseMaster equicoursemaster)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, equicoursemaster, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "EquiCourseMasterId");

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

        public bool Update(EquiCourseMaster equicoursemaster)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, equicoursemaster, isInsert);

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

                db.AddInParameter(cmd, "EquiCourseMasterId", DbType.Int32, id);
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

        public EquiCourseMaster GetById(int? id)
        {
            EquiCourseMaster _equicoursemaster = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<EquiCourseMaster> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<EquiCourseMaster>(sqlGetById, rowMapper);
                _equicoursemaster = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _equicoursemaster;
            }

            return _equicoursemaster;
        }

        public List<EquiCourseMaster> GetAll()
        {
            List<EquiCourseMaster> equicoursemasterList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<EquiCourseMaster> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<EquiCourseMaster>(sqlGetAll, mapper);
                IEnumerable<EquiCourseMaster> collection = accessor.Execute();

                equicoursemasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return equicoursemasterList;
            }

            return equicoursemasterList;
        }

        public BillMaxReceiptNoDTO GetMaxGroupNo()
        {
            BillMaxReceiptNoDTO billMaxReceiptNoMax = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<BillMaxReceiptNoDTO> mapper = MapBuilder<BillMaxReceiptNoDTO>.MapAllProperties()
                .Map(m => m.MaxMoneyReceiptNo).ToColumn("MaxGroupNo")
                .Build();

                var accessor = db.CreateSprocAccessor<BillMaxReceiptNoDTO>(sqlGetMaxGroupNo, mapper);
                BillMaxReceiptNoDTO collection = accessor.Execute().FirstOrDefault();

                billMaxReceiptNoMax = collection;
            }

            catch (Exception ex)
            {
                return billMaxReceiptNoMax;
            }

            return billMaxReceiptNoMax;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, EquiCourseMaster equicoursemaster, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "EquiCourseMasterId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "EquiCourseMasterId", DbType.Int32, equicoursemaster.EquiCourseMasterId);
            }

            	
		db.AddInParameter(cmd,"GroupNo",DbType.Int32,equicoursemaster.GroupNo);
		db.AddInParameter(cmd,"GroupName",DbType.String,equicoursemaster.GroupName);
		db.AddInParameter(cmd,"Remark",DbType.String,equicoursemaster.Remark);
		db.AddInParameter(cmd,"Attribute1",DbType.String,equicoursemaster.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,equicoursemaster.Attribute2);
		db.AddInParameter(cmd,"Attribute3",DbType.String,equicoursemaster.Attribute3);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,equicoursemaster.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,equicoursemaster.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,equicoursemaster.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,equicoursemaster.ModifiedDate);
            
            return db;
        }

        private IRowMapper<EquiCourseMaster> GetMaper()
        {
            IRowMapper<EquiCourseMaster> mapper = MapBuilder<EquiCourseMaster>.MapAllProperties()

       	   .Map(m => m.EquiCourseMasterId).ToColumn("EquiCourseMasterId")
		.Map(m => m.GroupNo).ToColumn("GroupNo")
		.Map(m => m.GroupName).ToColumn("GroupName")
		.Map(m => m.Remark).ToColumn("Remark")
		.Map(m => m.Attribute1).ToColumn("Attribute1")
		.Map(m => m.Attribute2).ToColumn("Attribute2")
		.Map(m => m.Attribute3).ToColumn("Attribute3")
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

