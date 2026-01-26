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
    public partial class SqlExamMarkPublishAcaCalSectionRelationRepository : IExamMarkPublishAcaCalSectionRelationRepository
    {

        Database db = null;

        private string sqlInsert = "ExamMarkPublishAcaCalSectionRelationInsert";
        private string sqlUpdate = "ExamMarkPublishAcaCalSectionRelationUpdate";
        private string sqlDelete = "ExamMarkPublishAcaCalSectionRelationDeleteById";
        private string sqlGetById = "ExamMarkPublishAcaCalSectionRelationGetById";
        private string sqlGetAll = "ExamMarkPublishAcaCalSectionRelationGetAll";
        private string sqlByProgramIdAcaCalId = "AcademicCalenderSectionForResultPublish";
               
        public int Insert(ExamMarkPublishAcaCalSectionRelation exammarkpublishacacalsectionrelation)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, exammarkpublishacacalsectionrelation, isInsert);
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

        public bool Update(ExamMarkPublishAcaCalSectionRelation exammarkpublishacacalsectionrelation)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, exammarkpublishacacalsectionrelation, isInsert);

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

        public ExamMarkPublishAcaCalSectionRelation GetById(int? id)
        {
            ExamMarkPublishAcaCalSectionRelation _exammarkpublishacacalsectionrelation = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkPublishAcaCalSectionRelation> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkPublishAcaCalSectionRelation>(sqlGetById, rowMapper);
                _exammarkpublishacacalsectionrelation = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _exammarkpublishacacalsectionrelation;
            }

            return _exammarkpublishacacalsectionrelation;
        }

        public ExamMarkPublishAcaCalSectionRelation GetByAcacalSecId(int id)
        {
            ExamMarkPublishAcaCalSectionRelation _exammarkpublishacacalsectionrelation = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkPublishAcaCalSectionRelation> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkPublishAcaCalSectionRelation>("ExamMarkPublishGetByAcaCalSecId", rowMapper);
                _exammarkpublishacacalsectionrelation = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _exammarkpublishacacalsectionrelation;
            }

            return _exammarkpublishacacalsectionrelation;
        }

        public List<ExamMarkPublishAcaCalSectionRelation> GetAll()
        {
            List<ExamMarkPublishAcaCalSectionRelation> exammarkpublishacacalsectionrelationList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkPublishAcaCalSectionRelation> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkPublishAcaCalSectionRelation>(sqlGetAll, mapper);
                IEnumerable<ExamMarkPublishAcaCalSectionRelation> collection = accessor.Execute();

                exammarkpublishacacalsectionrelationList = collection.ToList();
            }

            catch (Exception ex)
            {
                return exammarkpublishacacalsectionrelationList;
            }

            return exammarkpublishacacalsectionrelationList;
        }


        public List<AcacalSectionResultPublishDTO> GetByProgramIdAcaCalId(int programId, int acaCalId)
        {
            List<AcacalSectionResultPublishDTO> exammarkpublishacacalsectionrelationList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcacalSectionResultPublishDTO> mapper = GetResultPublishDTOMaper();

                var accessor = db.CreateSprocAccessor<AcacalSectionResultPublishDTO>(sqlByProgramIdAcaCalId, mapper);
                IEnumerable<AcacalSectionResultPublishDTO> collection = accessor.Execute(programId, acaCalId);

                exammarkpublishacacalsectionrelationList = collection.ToList();
            }

            catch (Exception ex)
            {
                return exammarkpublishacacalsectionrelationList;
            }

            return exammarkpublishacacalsectionrelationList;
        }

        private IRowMapper<AcacalSectionResultPublishDTO> GetResultPublishDTOMaper()
        {
            IRowMapper<AcacalSectionResultPublishDTO> mapper = MapBuilder<AcacalSectionResultPublishDTO>.MapAllProperties()

            .Map(m => m.AcaCal_SectionID).ToColumn("AcaCal_SectionID")
            .Map(m => m.AcademicCalenderID).ToColumn("AcademicCalenderID")
            .Map(m => m.CourseID).ToColumn("CourseID")
            .Map(m => m.VersionID).ToColumn("VersionID")
            .Map(m => m.CourseName).ToColumn("CourseName")
            .Map(m => m.SectionName).ToColumn("SectionName")
            .Map(m => m.StudentCount).ToColumn("StudentCount")
            .Map(m => m.ApprovedDate).ToColumn("ApprovedDate")
            .Map(m => m.IsApproved).ToColumn("IsApproved")
            .Map(m => m.FinalSubmitDate).ToColumn("FinalSubmitDate")
            .Map(m => m.IsFinalSubmit).ToColumn("IsFinalSubmit")
            .Map(m => m.PublishedDate).ToColumn("PublishedDate")
            .Map(m => m.IsPublished).ToColumn("IsPublished")

            .Build();

            return mapper;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamMarkPublishAcaCalSectionRelation exammarkpublishacacalsectionrelation, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, exammarkpublishacacalsectionrelation.Id);
            }

            	
		db.AddInParameter(cmd,"AcacalSectionId",DbType.Int32,exammarkpublishacacalsectionrelation.AcacalSectionId);
		db.AddInParameter(cmd,"IsFinalSubmit",DbType.Boolean,exammarkpublishacacalsectionrelation.IsFinalSubmit);
		db.AddInParameter(cmd,"FinalSubmitDate",DbType.DateTime,exammarkpublishacacalsectionrelation.FinalSubmitDate);
		db.AddInParameter(cmd,"FinalSubmitBy",DbType.Int32,exammarkpublishacacalsectionrelation.FinalSubmitBy);
		db.AddInParameter(cmd,"IsApproved",DbType.Boolean,exammarkpublishacacalsectionrelation.IsApproved);
		db.AddInParameter(cmd,"ApprovedDate",DbType.DateTime,exammarkpublishacacalsectionrelation.ApprovedDate);
		db.AddInParameter(cmd,"ApprovedBy",DbType.Int32,exammarkpublishacacalsectionrelation.ApprovedBy);
        db.AddInParameter(cmd, "IsPublished", DbType.Boolean, exammarkpublishacacalsectionrelation.IsPublished);
        db.AddInParameter(cmd, "PublishedDate", DbType.DateTime, exammarkpublishacacalsectionrelation.PublishedDate);
        db.AddInParameter(cmd, "PublishedBy", DbType.Int32, exammarkpublishacacalsectionrelation.PublishedBy);
		db.AddInParameter(cmd,"Attribute1",DbType.String,exammarkpublishacacalsectionrelation.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,exammarkpublishacacalsectionrelation.Attribute2);
		db.AddInParameter(cmd,"Attribute3",DbType.String,exammarkpublishacacalsectionrelation.Attribute3);
		db.AddInParameter(cmd,"Attribute4",DbType.String,exammarkpublishacacalsectionrelation.Attribute4);
		db.AddInParameter(cmd,"Attribute5",DbType.String,exammarkpublishacacalsectionrelation.Attribute5);
		db.AddInParameter(cmd,"Attribute6",DbType.String,exammarkpublishacacalsectionrelation.Attribute6);
		db.AddInParameter(cmd,"Attribute7",DbType.String,exammarkpublishacacalsectionrelation.Attribute7);
		db.AddInParameter(cmd,"Attribute8",DbType.String,exammarkpublishacacalsectionrelation.Attribute8);
            
            return db;
        }

        private IRowMapper<ExamMarkPublishAcaCalSectionRelation> GetMaper()
        {
            IRowMapper<ExamMarkPublishAcaCalSectionRelation> mapper = MapBuilder<ExamMarkPublishAcaCalSectionRelation>.MapAllProperties()

       	    .Map(m => m.Id).ToColumn("Id")
		    .Map(m => m.AcacalSectionId).ToColumn("AcacalSectionId")
		    .Map(m => m.IsFinalSubmit).ToColumn("IsFinalSubmit")
		    .Map(m => m.FinalSubmitDate).ToColumn("FinalSubmitDate")
		    .Map(m => m.FinalSubmitBy).ToColumn("FinalSubmitBy")
		    .Map(m => m.IsApproved).ToColumn("IsApproved")
		    .Map(m => m.ApprovedDate).ToColumn("ApprovedDate")
		    .Map(m => m.ApprovedBy).ToColumn("ApprovedBy")
            .Map(m => m.IsPublished).ToColumn("IsPublished")
            .Map(m => m.PublishedDate).ToColumn("PublishedDate")
            .Map(m => m.PublishedBy).ToColumn("PublishedBy")
		    .Map(m => m.Attribute1).ToColumn("Attribute1")
		    .Map(m => m.Attribute2).ToColumn("Attribute2")
		    .Map(m => m.Attribute3).ToColumn("Attribute3")
		    .Map(m => m.Attribute4).ToColumn("Attribute4")
		    .Map(m => m.Attribute5).ToColumn("Attribute5")
		    .Map(m => m.Attribute6).ToColumn("Attribute6")
		    .Map(m => m.Attribute7).ToColumn("Attribute7")
		    .Map(m => m.Attribute8).ToColumn("Attribute8")
            
            .Build();

            return mapper;
        }
        #endregion

    }
}

