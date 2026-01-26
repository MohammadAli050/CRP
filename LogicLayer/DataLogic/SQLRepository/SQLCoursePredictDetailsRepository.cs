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
    public partial class SqlCoursePredictDetailsRepository : ICoursePredictDetailsRepository
    {

        Database db = null;

        private string sqlInsert = "CoursePredictDetailsInsert";
        private string sqlUpdate = "CoursePredictDetailsUpdate";
        private string sqlDelete = "CoursePredictDetailsDelete";
        private string sqlGetById = "CoursePredictDetailsGetById";
        private string sqlGetAll = "CoursePredictDetailsGetAll";
        private string sqlGetAllByAcaCalProgram = "CoursePredictDetailsGetAllByAcaCalProgram";
               
        public int Insert(CoursePredictDetails coursepredictdetails)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, coursepredictdetails, isInsert);
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

        public bool Update(CoursePredictDetails coursepredictdetails)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, coursepredictdetails, isInsert);

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

        public CoursePredictDetails GetById(int id)
        {
            CoursePredictDetails _coursepredictdetails = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CoursePredictDetails> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CoursePredictDetails>(sqlGetById, rowMapper);
                _coursepredictdetails = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _coursepredictdetails;
            }

            return _coursepredictdetails;
        }

        public List<CoursePredictDetails> GetAll()
        {
            List<CoursePredictDetails> coursepredictdetailsList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CoursePredictDetails> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CoursePredictDetails>(sqlGetAll, mapper);
                IEnumerable<CoursePredictDetails> collection = accessor.Execute();

                coursepredictdetailsList = collection.ToList();
            }

            catch (Exception ex)
            {
                return coursepredictdetailsList;
            }

            return coursepredictdetailsList;
        }

        public List<CoursePredictDetails> GetAll(int acaCalId, int programId)
        {
            List<CoursePredictDetails> coursePredictDetailsList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CoursePredictDetails> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CoursePredictDetails>(sqlGetAllByAcaCalProgram, mapper);
                IEnumerable<CoursePredictDetails> collection = accessor.Execute(acaCalId, programId);

                coursePredictDetailsList = collection.ToList();
            }

            catch (Exception ex)
            {
                return coursePredictDetailsList;
            }

            return coursePredictDetailsList;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, CoursePredictDetails coursepredictdetails, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, coursepredictdetails.Id);
            }

            	
		    db.AddInParameter(cmd,"CoursePredictMasterId",DbType.Int32,coursepredictdetails.CoursePredictMasterId);
		    db.AddInParameter(cmd,"StudentId",DbType.Int32,coursepredictdetails.StudentId);
            db.AddInParameter(cmd,"Gender",DbType.String, coursepredictdetails.Gender);
		    db.AddInParameter(cmd,"CourseId",DbType.Int32,coursepredictdetails.CourseId);
		    db.AddInParameter(cmd,"VersionId",DbType.Int32,coursepredictdetails.VersionId);
		    db.AddInParameter(cmd,"NodeId",DbType.Int32,coursepredictdetails.NodeId);
		    db.AddInParameter(cmd,"NodeLinkName",DbType.String,coursepredictdetails.NodeLinkName);
		    db.AddInParameter(cmd,"Attribute1",DbType.String,coursepredictdetails.Attribute1);
		    db.AddInParameter(cmd,"Attribute2",DbType.String,coursepredictdetails.Attribute2);
		    db.AddInParameter(cmd,"Attribute3",DbType.String,coursepredictdetails.Attribute3);
		    db.AddInParameter(cmd,"CreatedBy",DbType.Int32,coursepredictdetails.CreatedBy);
		    db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,coursepredictdetails.CreatedDate);
		    db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,coursepredictdetails.ModifiedBy);
		    db.AddInParameter(cmd,"MofifiedDate",DbType.DateTime,coursepredictdetails.MofifiedDate);
            
            return db;
        }

        private IRowMapper<CoursePredictDetails> GetMaper()
        {
            IRowMapper<CoursePredictDetails> mapper = MapBuilder<CoursePredictDetails>.MapAllProperties()

       	    .Map(m => m.Id).ToColumn("Id")
		    .Map(m => m.CoursePredictMasterId).ToColumn("CoursePredictMasterId")
		    .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.Gender).ToColumn("Gender")
		    .Map(m => m.CourseId).ToColumn("CourseId")
		    .Map(m => m.VersionId).ToColumn("VersionId")
		    .Map(m => m.NodeId).ToColumn("NodeId")
		    .Map(m => m.NodeLinkName).ToColumn("NodeLinkName")
		    .Map(m => m.Attribute1).ToColumn("Attribute1")
		    .Map(m => m.Attribute2).ToColumn("Attribute2")
		    .Map(m => m.Attribute3).ToColumn("Attribute3")
		    .Map(m => m.CreatedBy).ToColumn("CreatedBy")
		    .Map(m => m.CreatedDate).ToColumn("CreatedDate")
		    .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
		    .Map(m => m.MofifiedDate).ToColumn("MofifiedDate")
            .DoNotMap(m => m.CourseName)
            
            .Build();

            return mapper;
        }
        #endregion

    }
}

