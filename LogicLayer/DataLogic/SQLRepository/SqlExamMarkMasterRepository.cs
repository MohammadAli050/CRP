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
    public partial class SqlExamMarkMasterRepository : IExamMarkMasterRepository
    {

        Database db = null;

        private string sqlInsert = "ExamMarkMasterInsert";
        private string sqlUpdate = "ExamMarkMasterUpdate";
        private string sqlDelete = "ExamMarkMasterDelete";
        private string sqlGetById = "ExamMarkMasterGetById";
        private string sqlGetAll = "ExamMarkMasterGetAll";
        private string sqlGetByAcaCalIdAcaCalSectionIdExamTemplateItemId = "ExamMarkMasterGetByAcaCalIdAcaCalSectionIdExamTemplateItemId";
               
        public int Insert(ExamMarkMaster exammarkmaster)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, exammarkmaster, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ExamMarkMasterId");

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

        public bool Update(ExamMarkMaster exammarkmaster)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, exammarkmaster, isInsert);

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

                db.AddInParameter(cmd, "ExamMarkMasterId", DbType.Int32, id);
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

        public ExamMarkMaster GetById(int? id)
        {
            ExamMarkMaster _exammarkmaster = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkMaster> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkMaster>(sqlGetById, rowMapper);
                _exammarkmaster = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _exammarkmaster;
            }

            return _exammarkmaster;
        }

        public List<ExamMarkMaster> GetAll()
        {
            List<ExamMarkMaster> exammarkmasterList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkMaster> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkMaster>(sqlGetAll, mapper);
                IEnumerable<ExamMarkMaster> collection = accessor.Execute();

                exammarkmasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return exammarkmasterList;
            }

            return exammarkmasterList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamMarkMaster exammarkmaster, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ExamMarkMasterId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ExamMarkMasterId", DbType.Int32, exammarkmaster.ExamMarkMasterId);
            }


            db.AddInParameter(cmd, "ExamMark", DbType.Decimal, exammarkmaster.ExamMark);
		    db.AddInParameter(cmd,"ExamMarkEntryDate",DbType.DateTime,exammarkmaster.ExamMarkEntryDate);
            db.AddInParameter(cmd, "ExamDate", DbType.DateTime, exammarkmaster.ExamDate);
		    db.AddInParameter(cmd,"ExamTemplateBasicItemId",DbType.Int32,exammarkmaster.ExamTemplateBasicItemId);
		    db.AddInParameter(cmd,"AcaCalId",DbType.Int32,exammarkmaster.AcaCalId);
		    db.AddInParameter(cmd,"AcaCalSectionId",DbType.Int32,exammarkmaster.AcaCalSectionId);
		    db.AddInParameter(cmd,"Attribute1",DbType.String,exammarkmaster.Attribute1);
		    db.AddInParameter(cmd,"Attribute2",DbType.String,exammarkmaster.Attribute2);
		    db.AddInParameter(cmd,"Attribute3",DbType.String,exammarkmaster.Attribute3);
		    db.AddInParameter(cmd,"CreatedBy",DbType.Int32,exammarkmaster.CreatedBy);
		    db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,exammarkmaster.CreatedDate);
		    db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,exammarkmaster.ModifiedBy);
		    db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,exammarkmaster.ModifiedDate);
            
            return db;
        }

        private IRowMapper<ExamMarkMaster> GetMaper()
        {
            IRowMapper<ExamMarkMaster> mapper = MapBuilder<ExamMarkMaster>.MapAllProperties()

       	    .Map(m => m.ExamMarkMasterId).ToColumn("ExamMarkMasterId")
		    .Map(m => m.ExamMark).ToColumn("ExamMark")
		    .Map(m => m.ExamMarkEntryDate).ToColumn("ExamMarkEntryDate")
            .Map(m => m.ExamDate).ToColumn("ExamDate")
		    .Map(m => m.ExamTemplateBasicItemId).ToColumn("ExamTemplateBasicItemId")
		    .Map(m => m.AcaCalId).ToColumn("AcaCalId")
		    .Map(m => m.AcaCalSectionId).ToColumn("AcaCalSectionId")
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


        public ExamMarkMaster GetByAcaCalIdAcaCalSectionIdExamTemplateItemId(int acaCalId, int acaCalsectionId, int examTemplateBasicItemId) 
        {
            ExamMarkMaster _exammarkmaster = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkMaster> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkMaster>(sqlGetByAcaCalIdAcaCalSectionIdExamTemplateItemId, rowMapper);
                _exammarkmaster = accessor.Execute(acaCalId, acaCalsectionId, examTemplateBasicItemId).FirstOrDefault();

            }
            catch (Exception ex)
            {
                return _exammarkmaster;
            }

            return _exammarkmaster;
        }

    }
}

