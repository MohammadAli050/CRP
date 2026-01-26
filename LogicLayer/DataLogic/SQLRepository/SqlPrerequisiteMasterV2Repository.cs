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
    public partial class SqlPrerequisiteMasterV2Repository : IPrerequisiteMasterV2Repository
    {

        Database db = null;

        private string sqlInsert = "PrerequisiteMasterV2Insert";//
        private string sqlUpdate = "PrerequisiteMasterV2Update";
        private string sqlDelete = "PrerequisiteMasterV2DeleteById";//
        private string sqlGetById = "PrerequisiteMasterV2GetById";//
        private string sqlGetAll = "PrerequisiteMasterV2GetAll";//
               
        public int Insert(PrerequisiteMasterV2 prerequisitemasterv2)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, prerequisitemasterv2, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "PreRequisiteMasterId");

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

        public bool Update(PrerequisiteMasterV2 prerequisitemasterv2)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, prerequisitemasterv2, isInsert);

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

                db.AddInParameter(cmd, "PreRequisiteMasterId", DbType.Int32, id);
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

        public PrerequisiteMasterV2 GetById(int? id)
        {
            PrerequisiteMasterV2 _prerequisitemasterv2 = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PrerequisiteMasterV2> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<PrerequisiteMasterV2>(sqlGetById, rowMapper);
                _prerequisitemasterv2 = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _prerequisitemasterv2;
            }

            return _prerequisitemasterv2;
        }

        public List<PrerequisiteMasterV2> GetAll()
        {
            List<PrerequisiteMasterV2> prerequisitemasterv2List= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PrerequisiteMasterV2> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<PrerequisiteMasterV2>(sqlGetAll, mapper);
                IEnumerable<PrerequisiteMasterV2> collection = accessor.Execute();

                prerequisitemasterv2List = collection.ToList();
            }

            catch (Exception ex)
            {
                return prerequisitemasterv2List;
            }

            return prerequisitemasterv2List;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, PrerequisiteMasterV2 prerequisitemasterv2, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "PreRequisiteMasterId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "PreRequisiteMasterId", DbType.Int32, prerequisitemasterv2.PreRequisiteMasterId);
            }

            	
		db.AddInParameter(cmd,"ProgramId",DbType.Int32,prerequisitemasterv2.ProgramId);
		db.AddInParameter(cmd,"NodeId",DbType.Int32,prerequisitemasterv2.NodeId);
		db.AddInParameter(cmd,"CourseId",DbType.Int32,prerequisitemasterv2.CourseId);
		db.AddInParameter(cmd,"VersionId",DbType.Int32,prerequisitemasterv2.VersionId);
		db.AddInParameter(cmd,"Remark",DbType.String,prerequisitemasterv2.Remark);
		db.AddInParameter(cmd,"Attribute1",DbType.String,prerequisitemasterv2.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,prerequisitemasterv2.Attribute2);
		db.AddInParameter(cmd,"Attribute3",DbType.String,prerequisitemasterv2.Attribute3);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,prerequisitemasterv2.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,prerequisitemasterv2.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,prerequisitemasterv2.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,prerequisitemasterv2.ModifiedDate);
            
            return db;
        }

        private IRowMapper<PrerequisiteMasterV2> GetMaper()
        {
            IRowMapper<PrerequisiteMasterV2> mapper = MapBuilder<PrerequisiteMasterV2>.MapAllProperties()

       	   .Map(m => m.PreRequisiteMasterId).ToColumn("PreRequisiteMasterId")
		.Map(m => m.ProgramId).ToColumn("ProgramId")
		.Map(m => m.NodeId).ToColumn("NodeId")
		.Map(m => m.CourseId).ToColumn("CourseId")
		.Map(m => m.VersionId).ToColumn("VersionId")
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

