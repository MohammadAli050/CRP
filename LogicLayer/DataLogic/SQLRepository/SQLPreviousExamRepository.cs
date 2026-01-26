using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data.Common;
using System.Data;
using LogicLayer.DataLogic.IRepository;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLPreviousExamRepository:IPreviousExamRepository
    {
        Database db = null;

        private string sqlInsert = "PreviousExamInsert";
        private string sqlUpdate = "PreviousExamUpdate";
        private string sqlDelete = "PreviousExamDeleteById";
        private string sqlGetById = "PreviousExamGetById";
        private string sqlGetAll = "PreviousExamGetAll";

        public int Insert(PreviousExam exam)
        {
            int id = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db.AddOutParameter(cmd, "PreviousExamId", DbType.Int32, Int32.MaxValue);

                db.AddInParameter(cmd, "Board", DbType.String, exam.Board);
                db.AddInParameter(cmd, "RollNo", DbType.String, exam.RollNo);
                db.AddInParameter(cmd, "InstituteName", DbType.String, exam.InstituteName);
                db.AddInParameter(cmd, "Result", DbType.String, exam.Result);
                db.AddInParameter(cmd, "PassingYear", DbType.Int32, exam.PassingYear);
                db.AddInParameter(cmd, "GroupOrSubject", DbType.String, exam.GroupOrSubject);
                db.AddInParameter(cmd, "CreatedBy", DbType.Int64, exam.CreatedBy);
                db.AddInParameter(cmd, "CreatedOn", DbType.DateTime, exam.CreatedOn);
                db.AddInParameter(cmd, "ModifiedBy", DbType.Int64, exam.ModifiedBy);
                db.AddInParameter(cmd, "ModifiedOn", DbType.DateTime, exam.ModifiedOn);

                db.AddInParameter(cmd, "ResultId", DbType.Int32, exam.ResultId);
                db.AddInParameter(cmd, "GPA", DbType.Decimal, exam.GPA);
                db.AddInParameter(cmd, "GPAW4S", DbType.Decimal, exam.GPAW4S);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "PreviousExamId");

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

        public bool Update(PreviousExam exam)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db.AddInParameter(cmd, "PreviousExamId", DbType.Int32, exam.PreviousExamId);
                db.AddInParameter(cmd, "Board", DbType.String, exam.Board);
                db.AddInParameter(cmd, "RollNo", DbType.String, exam.RollNo);
                db.AddInParameter(cmd, "InstituteName", DbType.String, exam.InstituteName);
                db.AddInParameter(cmd, "Result", DbType.String, exam.Result);
                db.AddInParameter(cmd, "PassingYear", DbType.Int32, exam.PassingYear);
                db.AddInParameter(cmd, "GroupOrSubject", DbType.String, exam.GroupOrSubject);
                db.AddInParameter(cmd, "CreatedBy", DbType.Int64, exam.CreatedBy);
                db.AddInParameter(cmd, "CreatedOn", DbType.DateTime, exam.CreatedOn);
                db.AddInParameter(cmd, "ModifiedBy", DbType.Int64, exam.ModifiedBy);
                db.AddInParameter(cmd, "ModifiedOn", DbType.DateTime, exam.ModifiedOn);

                db.AddInParameter(cmd, "ResultId", DbType.Int32, exam.ResultId);
                db.AddInParameter(cmd, "GPA", DbType.Decimal, exam.GPA);
                db.AddInParameter(cmd, "GPAW4S", DbType.Decimal, exam.GPAW4S);

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

        public bool Delete(long id)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlDelete);

                db.AddInParameter(cmd, "PreviousExamId", DbType.Int32, id);

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

        public PreviousExam GetById(long id)
        {
            PreviousExam _exam = new PreviousExam();
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PreviousExam> rowMapper = MapBuilder<PreviousExam>.MapNoProperties()
                .Map(m => m.PreviousExamId).ToColumn("PreviousExamId")

                .Map(m => m.Board).ToColumn("Board")
                 .Map(m => m.RollNo).ToColumn("RollNo")
                .Map(m => m.InstituteName).ToColumn("InstituteName")
                .Map(m => m.Result).ToColumn("Result")
                .Map(m => m.PassingYear).ToColumn("PassingYear")
                .Map(m => m.GroupOrSubject).ToColumn("GroupOrSubject")
                .Map(m => m.CreatedBy).ToColumn("CreatedBy")
                .Map(m => m.CreatedOn).ToColumn("CreatedOn")
                .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
                .Map(m => m.ModifiedOn).ToColumn("ModifiedOn")
                .Map(m => m.ResultId).ToColumn("ResultId")
                .Map(m => m.GPA).ToColumn("GPA")
                .Map(m => m.GPAW4S).ToColumn("GPAW4S")


                .Build();

                var accessor = db.CreateSprocAccessor<PreviousExam>(sqlGetById, rowMapper);
                _exam = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _exam;
            }

            return _exam;
        }

        public List<PreviousExam> GetAll()
        {
            List<PreviousExam> examList = new List<PreviousExam>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PreviousExam> mapper = MapBuilder<PreviousExam>.MapAllProperties()
                .Map(m => m.PreviousExamId).ToColumn("PreviousExamId")

                .Map(m => m.Board).ToColumn("Board")
                .Map(m => m.RollNo).ToColumn("RollNo")
                .Map(m => m.InstituteName).ToColumn("InstituteName")
                .Map(m => m.Result).ToColumn("Result")
                .Map(m => m.PassingYear).ToColumn("PassingYear")
                .Map(m => m.GroupOrSubject).ToColumn("GroupOrSubject")
                .Map(m => m.CreatedBy).ToColumn("CreatedBy")
                .Map(m => m.CreatedOn).ToColumn("CreatedOn")
                .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
                .Map(m => m.ModifiedOn).ToColumn("ModifiedOn")
                .Map(m => m.ResultId).ToColumn("ResultId")
                .Map(m => m.GPA).ToColumn("GPA")
                .Map(m => m.GPAW4S).ToColumn("GPAW4S")
                .Build();

                var accessor = db.CreateSprocAccessor<PreviousExam>(sqlGetAll, mapper);
                IEnumerable<PreviousExam> collection = accessor.Execute();

                examList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examList;
            }

            return examList;
        }
    }
}
