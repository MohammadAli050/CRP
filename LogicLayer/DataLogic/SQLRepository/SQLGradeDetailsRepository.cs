using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.SQLRepository
{
   partial class SQLGradeDetailsRepository:IGradeDetailsRepository
    {
        Database db = null;

        private string sqlInsert = "GradeDetailsInsert";
        private string sqlUpdate = "GradeDetailsUpdate";
        private string sqlDelete = "GradeDetailsDeleteById";
        private string sqlGetById = "GradeDetailsGetById";
        private string sqlGetAll = "GradeDetailsGetAll";
        private string sqlGetBygrade = "GradeDetailsGetByGrade";
        private string sqlGetByGradeMasterId = "GradeDetailsGetByGradeMasterId";
       
       //added by mohammad ali
        private string sqlGetByMark = "GradeDetailsGetByMark";

        public int Insert(GradeDetails gradeDetails)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, gradeDetails, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "GradeId");

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

        public bool Update(GradeDetails gradeDetails)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, gradeDetails, isInsert);

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

                db.AddInParameter(cmd, "GradeId", DbType.Int32, id);
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

        public GradeDetails GetById(int id)
        {
            GradeDetails _gradeDetails = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<GradeDetails> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<GradeDetails>(sqlGetById, rowMapper);
                _gradeDetails = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _gradeDetails;
            }

            return _gradeDetails;
        }

        public GradeDetails GetByGrade(string grade)
        {
            GradeDetails _gradeDetails = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<GradeDetails> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<GradeDetails>(sqlGetBygrade, rowMapper);
                _gradeDetails = accessor.Execute(grade).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _gradeDetails;
            }

            return _gradeDetails;
        }

        public List<GradeDetails> GetAll()
        {
            List<GradeDetails> gradeDetailsList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<GradeDetails> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<GradeDetails>(sqlGetAll, mapper);
                IEnumerable<GradeDetails> collection = accessor.Execute();

                gradeDetailsList = collection.ToList();
            }

            catch (Exception ex)
            {
                return gradeDetailsList;
            }

            return gradeDetailsList;
        }

        public List<GradeDetails> GetByGradeMasterId(int gradeMasterId)
        {
            List<GradeDetails> examList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<GradeDetails> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<GradeDetails>(sqlGetByGradeMasterId, mapper);
                List<GradeDetails> collection = accessor.Execute(gradeMasterId).ToList();

                examList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examList;
            }

            return examList;
        }

        //added by mohammad ali

        public GradeDetails GetByMarks(decimal mark)
        {
            GradeDetails _gradeDetails = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<GradeDetails> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<GradeDetails>(sqlGetByMark, rowMapper);
                _gradeDetails = accessor.Execute(mark).FirstOrDefault();

            }
            catch (Exception ex)
            {
                return _gradeDetails;
            }

            return _gradeDetails;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, GradeDetails gradeDetails, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "GradeId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "GradeId", DbType.Int32, gradeDetails.GradeId);
            }

            db.AddInParameter(cmd, "GradeMasterId", DbType.String, gradeDetails.GradeMasterId);
            db.AddInParameter(cmd, "Grade", DbType.String, gradeDetails.Grade);
            db.AddInParameter(cmd, "GradePoint", DbType.Decimal, gradeDetails.GradePoint);
            db.AddInParameter(cmd, "MinMarks", DbType.Decimal, gradeDetails.MinMarks);
            db.AddInParameter(cmd, "MaxMarks", DbType.Decimal, gradeDetails.MaxMarks);
            db.AddInParameter(cmd, "RetakeDiscount", DbType.Decimal, gradeDetails.RetakeDiscount);
            db.AddInParameter(cmd, "Sequence", DbType.Int32, gradeDetails.Sequence);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, gradeDetails.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, gradeDetails.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, gradeDetails.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, gradeDetails.ModifiedDate);

            return db;
        }

        private IRowMapper<GradeDetails> GetMaper()
        {
            IRowMapper<GradeDetails> mapper = MapBuilder<GradeDetails>.MapAllProperties()
            .Map(m => m.GradeId).ToColumn("GradeId")
            .Map(m => m.GradeMasterId).ToColumn("GradeMasterId")
            .Map(m => m.Grade).ToColumn("Grade")
            .Map(m => m.GradePoint).ToColumn("GradePoint")
            .Map(m => m.MinMarks).ToColumn("MinMarks")
            .Map(m => m.MaxMarks).ToColumn("MaxMarks")
            .Map(m => m.RetakeDiscount).ToColumn("RetakeDiscount")
            .Map(m => m.Sequence).ToColumn("Sequence")
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
