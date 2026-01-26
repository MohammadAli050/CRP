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
    public partial class SqlExamTemplateMasterRepository : IExamTemplateMasterRepository
    {

        Database db = null;

        private string sqlInsert = "ExamTemplateMasterInsert";
        private string sqlUpdate = "ExamTemplateMasterUpdate";
        private string sqlDelete = "ExamTemplateMasterDeleteById";
        private string sqlGetById = "ExamTemplateMasterGetById";
        private string sqlGetAll = "ExamTemplateMasterGetAll";
        private string sqlGetExamTemplateMasterByName = "ExamTemplateMasterGetByName";
        private string sqlGetExamTemplateItemGetByAcaCalSectionId = "ExamTemplateItemGetByAcaCalSectionId";
        private string sqlGetExamMarkColumnWise = "ExamMarkColmneWiseProcessByCourseIdAcaCalIdSectionId";
        private string sqlGetExamMarkColumnWiseCourseHistoryId = "ExamMarkColmneWiseProcessByCourseIdAcaCalIdSectionIdCourseHistoryId";
               
        public int Insert(ExamTemplateMaster examtemplatemaster)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, examtemplatemaster, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ExamTemplateMasterId");

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

        public bool Update(ExamTemplateMaster examtemplatemaster)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, examtemplatemaster, isInsert);

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

                db.AddInParameter(cmd, "ExamTemplateMasterId", DbType.Int32, id);
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

        public ExamTemplateMaster GetById(int? id)
        {
            ExamTemplateMaster _examtemplatemaster = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplateMaster> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplateMaster>(sqlGetById, rowMapper);
                _examtemplatemaster = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examtemplatemaster;
            }

            return _examtemplatemaster;
        }

        public List<ExamTemplateMaster> GetAll()
        {
            List<ExamTemplateMaster> examtemplatemasterList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplateMaster> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplateMaster>(sqlGetAll, mapper);
                IEnumerable<ExamTemplateMaster> collection = accessor.Execute();

                examtemplatemasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examtemplatemasterList;
            }

            return examtemplatemasterList;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamTemplateMaster examtemplatemaster, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ExamTemplateMasterId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ExamTemplateMasterId", DbType.Int32, examtemplatemaster.ExamTemplateMasterId);
            }
            	
		    db.AddInParameter(cmd,"ProgramId",DbType.Int32,examtemplatemaster.ProgramId);
		    db.AddInParameter(cmd,"ExamTemplateMasterTypeId",DbType.Int32,examtemplatemaster.ExamTemplateMasterTypeId);
		    db.AddInParameter(cmd,"ExamTemplateMasterName",DbType.String,examtemplatemaster.ExamTemplateMasterName);
            db.AddInParameter(cmd, "ExamTemplateMasterTotalMark", DbType.Decimal, examtemplatemaster.ExamTemplateMasterTotalMark);
		    db.AddInParameter(cmd,"Attribute1",DbType.String,examtemplatemaster.Attribute1);
		    db.AddInParameter(cmd,"Attribute2",DbType.String,examtemplatemaster.Attribute2);
		    db.AddInParameter(cmd,"Attribute3",DbType.String,examtemplatemaster.Attribute3);
		    db.AddInParameter(cmd,"CreatedBy",DbType.Int32,examtemplatemaster.CreatedBy);
		    db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,examtemplatemaster.CreatedDate);
		    db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,examtemplatemaster.ModifiedBy);
		    db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,examtemplatemaster.ModifiedDate);
            
            return db;
        }

        private IRowMapper<ExamTemplateMaster> GetMaper()
        {
            IRowMapper<ExamTemplateMaster> mapper = MapBuilder<ExamTemplateMaster>.MapAllProperties()

       	    .Map(m => m.ExamTemplateMasterId).ToColumn("ExamTemplateMasterId")
		    .Map(m => m.ProgramId).ToColumn("ProgramId")
		    .Map(m => m.ExamTemplateMasterTypeId).ToColumn("ExamTemplateMasterTypeId")
		    .Map(m => m.ExamTemplateMasterName).ToColumn("ExamTemplateMasterName")
		    .Map(m => m.ExamTemplateMasterTotalMark).ToColumn("ExamTemplateMasterTotalMark")
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

        public ExamTemplateMaster GetExamTemplateMasterByName(string examTemplateMasterName)
        {
            ExamTemplateMaster _examtemplatemaster = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplateMaster> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplateMaster>(sqlGetExamTemplateMasterByName, rowMapper);
                _examtemplatemaster = accessor.Execute(examTemplateMasterName).FirstOrDefault();

            }
            catch (Exception ex)
            {
                return _examtemplatemaster;
            }

            return _examtemplatemaster;
        }

        public List<ExamTemplateBasicCalculativeItemDTO> ExamTemplateItemGetByAcaCalSectionId(int acaCalSectionId)
        {
            List<ExamTemplateBasicCalculativeItemDTO> examtemplatemasterList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplateBasicCalculativeItemDTO> mapper = GetExamTemplateItemGetByAcaCalSectionIdMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplateBasicCalculativeItemDTO>(sqlGetExamTemplateItemGetByAcaCalSectionId, mapper);
                IEnumerable<ExamTemplateBasicCalculativeItemDTO> collection = accessor.Execute(acaCalSectionId);

                examtemplatemasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examtemplatemasterList;
            }

            return examtemplatemasterList;
        }

        private IRowMapper<ExamTemplateBasicCalculativeItemDTO> GetExamTemplateItemGetByAcaCalSectionIdMaper()
        {
            IRowMapper<ExamTemplateBasicCalculativeItemDTO> mapper = MapBuilder<ExamTemplateBasicCalculativeItemDTO>.MapAllProperties()
            
            .Map(m => m.ExamTemplateBasicItemId).ToColumn("ExamTemplateBasicItemId")   
            .Map(m => m.ColumnSequence).ToColumn("ColumnSequence")
            .Map(m => m.ExamTemplateMasterId).ToColumn("ExamTemplateMasterId")
            .Map(m => m.ExamTemplateBasicItemMark).ToColumn("ExamTemplateBasicItemMark")
            .Map(m => m.ExamTemplateBasicItemName).ToColumn("ExamTemplateBasicItemName")
            .Map(m => m.ExamMetaTypeId).ToColumn("ExamMetaTypeId")
            .Map(m => m.ExamTemplateMasterTypeId).ToColumn("ExamTemplateMasterTypeId")
            .Map(m => m.CalculationType).ToColumn("CalculationType")

            .Build();
            return mapper;
        }

        public List<ExamMarkColumnWiseDTO> GetStudentExamMarkColumnWise(int courseId, int versionId, int acaCalId, int acaCalSectionId)
        {
            List<ExamMarkColumnWiseDTO> examtemplatemasterList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkColumnWiseDTO> mapper = GetExamMarkColumnWiseDTOMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkColumnWiseDTO>(sqlGetExamMarkColumnWise, mapper);
                IEnumerable<ExamMarkColumnWiseDTO> collection = accessor.Execute(courseId,  versionId,  acaCalId,  acaCalSectionId);

                examtemplatemasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examtemplatemasterList;
            }

            return examtemplatemasterList;
        }

        public List<ExamMarkColumnWiseDTO> GetStudentExamMarkColumnWiseByStudentId(int courseId, int versionId, int acaCalId, int acaCalSectionId, int courseHistoryId)
        {
            List<ExamMarkColumnWiseDTO> examtemplatemasterList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkColumnWiseDTO> mapper = GetExamMarkColumnWiseDTOMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkColumnWiseDTO>(sqlGetExamMarkColumnWiseCourseHistoryId, mapper);
                IEnumerable<ExamMarkColumnWiseDTO> collection = accessor.Execute(courseId, versionId, acaCalId, acaCalSectionId, courseHistoryId);

                examtemplatemasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examtemplatemasterList;
            }

            return examtemplatemasterList;
        }

        private IRowMapper<ExamMarkColumnWiseDTO> GetExamMarkColumnWiseDTOMaper()
        {
            IRowMapper<ExamMarkColumnWiseDTO> mapper = MapBuilder<ExamMarkColumnWiseDTO>.MapAllProperties()

            .Map(m => m.StudentCourseHistoryId).ToColumn("StudentCourseHistoryId")
            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.AcaCalId).ToColumn("AcaCalId")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.ExamTemplateBasicItemId).ToColumn("ExamTemplateBasicItemId")
            .Map(m => m.ExamMetaTypeId).ToColumn("ExamMetaTypeId")
            .Map(m => m.ColumnSequence).ToColumn("ColumnSequence")
            .Map(m => m.ColumnName).ToColumn("ColumnName")
            .Map(m => m.GradePoint).ToColumn("GradePoint")
            .Map(m => m.Grade).ToColumn("Grade")
            .Map(m => m.Marks).ToColumn("Marks")
            .Map(m => m.ConvertedMark).ToColumn("ConvertedMark")
            .Map(m => m.ExamMarkTypeId).ToColumn("ExamMarkTypeId")
            .Build();
            return mapper;
        }
    }
}

