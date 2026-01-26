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
    public partial class SqlExamMarkDetailsRepository : IExamMarkDetailsRepository
    {

        Database db = null;

        private string sqlInsert = "ExamMarkDetailsInsert";
        private string sqlUpdate = "ExamMarkDetailsUpdate";
        private string sqlDelete = "ExamMarkDetailsDeleteById";
        private string sqlGetById = "ExamMarkDetailsGetById";
        private string sqlGetAll = "ExamMarkDetailsGetAll";
        private string sqlGetByExamMarkDtoByParameter = "ExamMarkDtoByParameter";
               
        public int Insert(ExamMarkDetails exammarkdetails)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, exammarkdetails, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ExamMarkDetailId");

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

        public bool Update(ExamMarkDetails exammarkdetails)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, exammarkdetails, isInsert);

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

                db.AddInParameter(cmd, "ExamMarkDetailId", DbType.Int32, id);
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

        public ExamMarkDetails GetById(int? id)
        {
            ExamMarkDetails _exammarkdetails = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkDetails> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkDetails>(sqlGetById, rowMapper);
                _exammarkdetails = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _exammarkdetails;
            }

            return _exammarkdetails;
        }

        public List<ExamMarkDetails> GetAll()
        {
            List<ExamMarkDetails> exammarkdetailsList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkDetails> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkDetails>(sqlGetAll, mapper);
                IEnumerable<ExamMarkDetails> collection = accessor.Execute();

                exammarkdetailsList = collection.ToList();
            }

            catch (Exception ex)
            {
                return exammarkdetailsList;
            }

            return exammarkdetailsList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamMarkDetails exammarkdetails, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ExamMarkDetailId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ExamMarkDetailId", DbType.Int32, exammarkdetails.ExamMarkDetailId);
            }
            	
		    db.AddInParameter(cmd,"ExamMarkMasterId",DbType.Int32,exammarkdetails.ExamMarkMasterId);
		    db.AddInParameter(cmd,"CourseHistoryId",DbType.Int32,exammarkdetails.CourseHistoryId);
            db.AddInParameter(cmd,"Marks", DbType.Decimal, exammarkdetails.Marks);
            db.AddInParameter(cmd, "ConvertedMark", DbType.Decimal, exammarkdetails.ConvertedMark);
            db.AddInParameter(cmd, "IsFinalSubmit", DbType.Boolean, exammarkdetails.IsFinalSubmit);
		    db.AddInParameter(cmd,"ExamTemplateBasicItemId",DbType.Int32,exammarkdetails.ExamTemplateBasicItemId);
		    db.AddInParameter(cmd,"ExamStatus",DbType.Int32,exammarkdetails.ExamStatus);
            db.AddInParameter(cmd, "ExamMarkTypeId", DbType.Int32, exammarkdetails.ExamMarkTypeId);
		    db.AddInParameter(cmd,"Attribute1",DbType.String,exammarkdetails.Attribute1);
		    db.AddInParameter(cmd,"Attribute2",DbType.String,exammarkdetails.Attribute2);
		    db.AddInParameter(cmd,"Attribute3",DbType.String,exammarkdetails.Attribute3);
		    db.AddInParameter(cmd,"CreatedBy",DbType.Int32,exammarkdetails.CreatedBy);
		    db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,exammarkdetails.CreatedDate);
		    db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,exammarkdetails.ModifiedBy);
		    db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,exammarkdetails.ModifiedDate);
            
            return db;
        }

        private IRowMapper<ExamMarkDetails> GetMaper()
        {
            IRowMapper<ExamMarkDetails> mapper = MapBuilder<ExamMarkDetails>.MapAllProperties()

       	   .Map(m => m.ExamMarkDetailId).ToColumn("ExamMarkDetailId")
		    .Map(m => m.ExamMarkMasterId).ToColumn("ExamMarkMasterId")
		    .Map(m => m.CourseHistoryId).ToColumn("CourseHistoryId")
		    .Map(m => m.Marks).ToColumn("Marks")
            .Map(m => m.ConvertedMark).ToColumn("ConvertedMark")
            .Map(m=>m.IsFinalSubmit).ToColumn("IsFinalSubmit")
		    .Map(m => m.ExamTemplateBasicItemId).ToColumn("ExamTemplateBasicItemId")
		    .Map(m => m.ExamStatus).ToColumn("ExamStatus")
            .Map(m => m.ExamMarkTypeId).ToColumn("ExamMarkTypeId")
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

        public List<ExamMarkNewDTO> GetExamMarkForReport(int programId, int sessionId, int courseId, int versionId, int acaCalSectionId, int examTemplateBasicItemId)
        {
            List<ExamMarkNewDTO> exammarkDtoList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkNewDTO> mapper = RptStudentMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkNewDTO>("RptExamMarkView", mapper);
                IEnumerable<ExamMarkNewDTO> collection = accessor.Execute(programId, sessionId, courseId, versionId, acaCalSectionId, examTemplateBasicItemId);

                exammarkDtoList = collection.ToList();
            }

            catch (Exception ex)
            {
                return exammarkDtoList;
            }

            return exammarkDtoList;
        }

        public List<ExamMarkNewDTO> GetConvertedExamMarkForReport(int programId, int sessionId, int courseId, int versionId, int acaCalSectionId, int examTemplateBasicItemId)
        {
            List<ExamMarkNewDTO> exammarkDtoList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkNewDTO> mapper = RptStudentMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkNewDTO>("RptConvertedExamMarkView", mapper);
                IEnumerable<ExamMarkNewDTO> collection = accessor.Execute(programId, sessionId, courseId, versionId, acaCalSectionId, examTemplateBasicItemId);

                exammarkDtoList = collection.ToList();
            }

            catch (Exception ex)
            {
                return exammarkDtoList;
            }

            return exammarkDtoList;
        }

        public List<ExamMarkDetails> GetExamMarkByCourseHistoryId(int courseHistoryId)
        {
            List<ExamMarkDetails> exammarkList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkDetails> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkDetails>("ExamMarkDetailsGetByCourseHistoryId", mapper);
                IEnumerable<ExamMarkDetails> collection = accessor.Execute(courseHistoryId);

                exammarkList = collection.ToList();
            }

            catch (Exception ex)
            {
                return exammarkList;
            }

            return exammarkList;
        }

        private IRowMapper<ExamMarkNewDTO> RptStudentMaper()
        {
            IRowMapper<ExamMarkNewDTO> mapper = MapBuilder<ExamMarkNewDTO>.MapAllProperties()
            .Map(m => m.ExamMarkDetailId).ToColumn("ExamMarkDetailId")
            .Map(m => m.CourseHistoryId).ToColumn("CourseHistoryId")
            .Map(m => m.StudentName).ToColumn("StudentName")
            .Map(m => m.StudentRoll).ToColumn("StudentRoll")
            .Map(m => m.Mark).ToColumn("Mark")
            .Map(m => m.ExamMarkTypeId).ToColumn("ExamMarkTypeId")
            .Map(m=> m.ExamTemplateBasicItemName).ToColumn("ExamTemplateBasicItemName")
            .DoNotMap(m => m.IsFinalSubmit)
            .Map(m => m.ColumnSequence).ToColumn("ColumnSequence")
            .Build();

            return mapper;
        }

        public List<ExamMarkNewDTO> GetByExamMarkDtoByParameter(int programId, int sessionId, int courseId, int versionId, int acaCalSectionId, int examTemplateBasicItemId)
        {
            List<ExamMarkNewDTO> exammarkDtoList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkNewDTO> mapper = GetStudentMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkNewDTO>(sqlGetByExamMarkDtoByParameter, mapper);
                IEnumerable<ExamMarkNewDTO> collection = accessor.Execute(programId, sessionId, courseId, versionId, acaCalSectionId, examTemplateBasicItemId);

                exammarkDtoList = collection.ToList();
            }

            catch (Exception ex)
            {
                return exammarkDtoList;
            }

            return exammarkDtoList;
        }

        private IRowMapper<ExamMarkNewDTO> GetStudentMaper()
        {
            IRowMapper<ExamMarkNewDTO> mapper = MapBuilder<ExamMarkNewDTO>.MapAllProperties()
            .Map(m => m.ExamMarkDetailId).ToColumn("ExamMarkDetailId")
            .Map(m => m.CourseHistoryId).ToColumn("CourseHistoryId")
            .Map(m => m.StudentName).ToColumn("StudentName")
            .Map(m => m.StudentRoll).ToColumn("StudentRoll")
            .Map(m => m.Mark).ToColumn("Mark")
            .Map(m => m.ExamMarkTypeId).ToColumn("ExamMarkTypeId")
            .Map(m => m.IsFinalSubmit).ToColumn("IsFinalSubmit")
            .DoNotMap(m=>m.ExamTemplateBasicItemName)
            .DoNotMap(m=>m.ColumnSequence)
            .Build();

            return mapper;
        }

        public ExamMarkDetails GetByCourseHistoryIdExamTemplateItemId(int courseHistoryId, int examTemplateItemId)
        {
            ExamMarkDetails _studentresult = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkDetails> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkDetails>("ExamMarkDetailsByCourseHistoryIdExamId", rowMapper);
                _studentresult = accessor.Execute(courseHistoryId, examTemplateItemId).FirstOrDefault();

            }
            catch (Exception ex)
            {
                return _studentresult;
            }

            return _studentresult;
        }

    }
}

