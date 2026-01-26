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
    public partial class SqlCoursePredictMasterRepository : ICoursePredictMasterRepository
    {

        Database db = null;

        private string sqlInsert = "CoursePredictMasterInsert";
        private string sqlUpdate = "CoursePredictMasterUpdate";
        private string sqlDelete = "CoursePredictMasterDelete";
        private string sqlGetById = "CoursePredictMasterGetById";
        private string sqlGetAll = "CoursePredictMasterGetAll";
        private string sqlInsertByAcaCalProgram = "CoursePredictMasterInsertByAcaCalProgram";
        private string sqlGetAllByAcaCalProgram = "CoursePredictMasterGetAllByAcaCalProgram";
               
        public int Insert(CoursePredictMaster coursepredictmaster)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, coursepredictmaster, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "Id");

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

        public bool Update(CoursePredictMaster coursepredictmaster)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, coursepredictmaster, isInsert);

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

                db.AddInParameter(cmd, "Id", DbType.Int32, id);
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

        public CoursePredictMaster GetById(int id)
        {
            CoursePredictMaster _coursepredictmaster = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CoursePredictMaster> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CoursePredictMaster>(sqlGetById, rowMapper);
                _coursepredictmaster = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _coursepredictmaster;
            }

            return _coursepredictmaster;
        }

        public List<CoursePredictMaster> GetAll()
        {
            List<CoursePredictMaster> coursepredictmasterList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CoursePredictMaster> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CoursePredictMaster>(sqlGetAll, mapper);
                IEnumerable<CoursePredictMaster> collection = accessor.Execute();

                coursepredictmasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return coursepredictmasterList;
            }

            return coursepredictmasterList;
        }

        public bool InsertByAcaCalProgram(int acaCalId, int programId)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsertByAcaCalProgram);

                db.AddInParameter(cmd, "AcaCalId", DbType.Int32, acaCalId);
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

        public List<CoursePredictMaster> GetAll(int acaCalId, int programId)
        {
            List<CoursePredictMaster> coursePredictMasterList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CoursePredictMaster> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CoursePredictMaster>(sqlGetAllByAcaCalProgram, mapper);
                IEnumerable<CoursePredictMaster> collection = accessor.Execute(acaCalId, programId);

                coursePredictMasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return coursePredictMasterList;
            }

            return coursePredictMasterList;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, CoursePredictMaster coursepredictmaster, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, coursepredictmaster.Id);
            }

            	
		db.AddInParameter(cmd,"AcaCalId",DbType.Int32,coursepredictmaster.AcaCalId);
		db.AddInParameter(cmd,"ProgramId",DbType.Int32,coursepredictmaster.ProgramId);
		db.AddInParameter(cmd,"BatchNo",DbType.Int32,coursepredictmaster.BatchNo);
		db.AddInParameter(cmd,"AcaCalSessionNo",DbType.Int32,coursepredictmaster.AcaCalSessionNo);
		db.AddInParameter(cmd,"NoOfActiveStudent",DbType.Int32,coursepredictmaster.NoOfActiveStudent);
		db.AddInParameter(cmd,"Attribute1",DbType.String,coursepredictmaster.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,coursepredictmaster.Attribute2);
		db.AddInParameter(cmd,"Attribute3",DbType.String,coursepredictmaster.Attribute3);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,coursepredictmaster.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,coursepredictmaster.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,coursepredictmaster.ModifiedBy);
		db.AddInParameter(cmd,"MofifiedDate",DbType.DateTime,coursepredictmaster.MofifiedDate);
            
            return db;
        }

        private IRowMapper<CoursePredictMaster> GetMaper()
        {
            IRowMapper<CoursePredictMaster> mapper = MapBuilder<CoursePredictMaster>.MapAllProperties()

       	   .Map(m => m.Id).ToColumn("Id")
		.Map(m => m.AcaCalId).ToColumn("AcaCalId")
		.Map(m => m.ProgramId).ToColumn("ProgramId")
		.Map(m => m.BatchNo).ToColumn("BatchNo")
		.Map(m => m.AcaCalSessionNo).ToColumn("AcaCalSessionNo")
		.Map(m => m.NoOfActiveStudent).ToColumn("NoOfActiveStudent")
		.Map(m => m.Attribute1).ToColumn("Attribute1")
		.Map(m => m.Attribute2).ToColumn("Attribute2")
		.Map(m => m.Attribute3).ToColumn("Attribute3")
		.Map(m => m.CreatedBy).ToColumn("CreatedBy")
		.Map(m => m.CreatedDate).ToColumn("CreatedDate")
		.Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
		.Map(m => m.MofifiedDate).ToColumn("MofifiedDate")
            
            .Build();

            return mapper;
        }
        #endregion

    }
}

