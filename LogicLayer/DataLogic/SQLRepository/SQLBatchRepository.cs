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
    public partial class SQLBatchRepository : IBatchRepository
    {

        Database db = null;

        private string sqlInsert = "BatchInsert";
        private string sqlUpdate = "BatchUpdate";
        private string sqlDelete = "BatchDelete";
        private string sqlGetById = "BatchGetById";
        private string sqlGetAll = "BatchGetAll";
               
        public int Insert(Batch batch)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, batch, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "BatchId");

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

        public bool Update(Batch batch)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, batch, isInsert);

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

                db.AddInParameter(cmd, "BatchId", DbType.Int32, id);
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

        public Batch GetById(int id)
        {
            Batch _batch = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Batch> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Batch>(sqlGetById, rowMapper);
                _batch = accessor.Execute(id).FirstOrDefault();

            }
            catch (Exception ex)
            {
                return _batch;
            }

            return _batch;
        }

        public List<Batch> GetAll()
        {
            List<Batch> batchList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Batch> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Batch>(sqlGetAll, mapper);
                IEnumerable<Batch> collection = accessor.Execute();

                batchList = collection.ToList();
            }

            catch (Exception ex)
            {
                return batchList;
            }

            return batchList;
        }


        public List<rBatchListByProgram> GetBatchListByProgram(int programId)
        {
            List<rBatchListByProgram> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rBatchListByProgram> mapper = GetBatchListByProgramMaper();

                var accessor = db.CreateSprocAccessor<rBatchListByProgram>("RptBatchListByProgram", mapper);
                IEnumerable<rBatchListByProgram> collection = accessor.Execute(programId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        #region Mapper

        private IRowMapper<rBatchListByProgram> GetBatchListByProgramMaper()
        {
            IRowMapper<rBatchListByProgram> mapper = MapBuilder<rBatchListByProgram>.MapAllProperties()

            .Map(m => m.BatchNO).ToColumn("BatchNO")
            .Map(m => m.BatchId).ToColumn("BatchId")

            .Build();

            return mapper;
        }
       
       
        private Database addParam(Database db, DbCommand cmd, Batch batch, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "BatchId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "BatchId", DbType.Int32, batch.BatchId);
            }

            	
		db.AddInParameter(cmd,"AcaCalId",DbType.Int32,batch.AcaCalId);
		db.AddInParameter(cmd,"ProgramId",DbType.Int32,batch.ProgramId);
		db.AddInParameter(cmd,"BatchNO",DbType.Int32,batch.BatchNO);
		db.AddInParameter(cmd,"Attribute1",DbType.String,batch.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,batch.Attribute2);
		db.AddInParameter(cmd,"Attribute3",DbType.String,batch.Attribute3);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,batch.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,batch.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,batch.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,batch.ModifiedDate);
            
            return db;
        }

        private IRowMapper<Batch> GetMaper()
        {
            IRowMapper<Batch> mapper = MapBuilder<Batch>.MapAllProperties()

       	    .Map(m => m.BatchId).ToColumn("BatchId")
		    .Map(m => m.AcaCalId).ToColumn("AcaCalId")
		    .Map(m => m.ProgramId).ToColumn("ProgramId")
		    .Map(m => m.BatchNO).ToColumn("BatchNO")
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


        #region IBatchRepository Members


        public List<Batch> GetByAcaCalSectionID(int AcaCalSectionID)
        {
            List<Batch> batchList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Batch> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Batch>("BatchGetByAcademicCalenderSectionId", mapper);
                IEnumerable<Batch> collection = accessor.Execute(AcaCalSectionID);

                batchList = collection.ToList();
            }

            catch (Exception ex)
            {
                return batchList;
            }

            return batchList;
        }

        #endregion

        #region IBatchRepository Members


        public List<Batch> GetAllByProgram(int programId)
        {
            List<Batch> batchList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Batch> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Batch>("BatchGetByProgramId", mapper);
                IEnumerable<Batch> collection = accessor.Execute(programId);

                batchList = collection.ToList();
            }

            catch (Exception ex)
            {
                return batchList;
            }

            return batchList;
        }

        #endregion
    }
}

