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
    public partial class SQLPersonPreviousExamRepository: IPersonPreviousExamRepository
    {
        Database db = null;

        private string sqlInsert = "PersonPreviousExamInsert";
        private string sqlUpdate = "PersonPreviousExamUpdate";
        private string sqlDelete = "PersonPreviousExamDeleteById";
        private string sqlGetById = "PersonPreviousExamGetById";
        private string sqlGetAll = "PersonPreviousExamGetAll";
        private string sqlGetAllByPersonId = "PersonPreviousExamGetAllByPersonId";

        public int Insert(PersonPreviousExam personexam)
        {
            int id = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db.AddOutParameter(cmd, "PersonPreviousExamId", DbType.Int32, Int32.MaxValue);

                db.AddInParameter(cmd, "PersonId", DbType.Int64, personexam.PersonId);
                db.AddInParameter(cmd, "PreviousExamId", DbType.Int64, personexam.PreviousExamId);
                db.AddInParameter(cmd, "PreviousExamTypeId", DbType.Int64, personexam.PreviousExamTypeId);
                db.AddInParameter(cmd, "CreatedBy", DbType.Int64, personexam.CreatedBy);
                db.AddInParameter(cmd, "CreatedOn", DbType.DateTime, personexam.CreatedOn);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "PersonPreviousExamId");

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


        public bool Update(PersonPreviousExam personexam)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db.AddInParameter(cmd, "PersonPreviousExamId", DbType.Int32, personexam.PersonPreviousExamId);
                db.AddInParameter(cmd, "PersonId", DbType.Int64, personexam.PersonId);
                db.AddInParameter(cmd, "PreviousExamId", DbType.Int64, personexam.PreviousExamId);
                db.AddInParameter(cmd, "PreviousExamTypeId", DbType.Int64, personexam.PreviousExamTypeId);
                db.AddInParameter(cmd, "CreatedBy", DbType.Int64, personexam.CreatedBy);
                db.AddInParameter(cmd, "CreatedOn", DbType.DateTime, personexam.CreatedOn);


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

                db.AddInParameter(cmd, "PersonPreviousExamId", DbType.Int32, id);

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

        public PersonPreviousExam GetById(int id)
        {
            PersonPreviousExam _candidateexam = new PersonPreviousExam();
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PersonPreviousExam> rowMapper = MapBuilder<PersonPreviousExam>.MapNoProperties()
                .Map(m => m.PersonPreviousExamId).ToColumn("PersonPreviousExamId")

                .Map(m => m.PersonId).ToColumn("PersonId")
                .Map(m => m.PreviousExamId).ToColumn("PreviousExamId")
                .Map(m => m.PreviousExamTypeId).ToColumn("PreviousExamTypeId")
                .Map(m => m.CreatedBy).ToColumn("CreatedBy")
                .Map(m => m.CreatedOn).ToColumn("CreatedOn")

                .Build();

                var accessor = db.CreateSprocAccessor<PersonPreviousExam>(sqlGetById, rowMapper);
                _candidateexam = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _candidateexam;
            }

            return _candidateexam;
        }

        public List<PersonPreviousExam> GetAll()
        {
            List<PersonPreviousExam> candidateexamList = new List<PersonPreviousExam>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PersonPreviousExam> mapper = MapBuilder<PersonPreviousExam>.MapAllProperties()
                .Map(m => m.PersonPreviousExamId).ToColumn("PersonPreviousExamId")
                .Map(m => m.PersonId).ToColumn("PersonId")
                .Map(m => m.PreviousExamId).ToColumn("PreviousExamId")
                .Map(m => m.PreviousExamTypeId).ToColumn("PreviousExamTypeId")
                .Map(m => m.CreatedBy).ToColumn("CreatedBy")
                .Map(m => m.CreatedOn).ToColumn("CreatedOn")
                .Build();

                var accessor = db.CreateSprocAccessor<PersonPreviousExam>(sqlGetAll, mapper);
                IEnumerable<PersonPreviousExam> collection = accessor.Execute();

                candidateexamList = collection.ToList();
            }

            catch (Exception ex)
            {
                return candidateexamList;
            }

            return candidateexamList;
        }

        public List<PersonPreviousExam> GetAllByPersonId(int id)
        {
            List<PersonPreviousExam> candidateexamList = new List<PersonPreviousExam>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PersonPreviousExam> mapper = MapBuilder<PersonPreviousExam>.MapAllProperties()
                .Map(m => m.PersonPreviousExamId).ToColumn("PersonPreviousExamId")
                .Map(m => m.PersonId).ToColumn("PersonId")
                .Map(m => m.PreviousExamId).ToColumn("PreviousExamId")
                .Map(m => m.PreviousExamTypeId).ToColumn("PreviousExamTypeId")
                .Map(m => m.CreatedBy).ToColumn("CreatedBy")
                .Map(m => m.CreatedOn).ToColumn("CreatedOn")
                .Build();

                var accessor = db.CreateSprocAccessor<PersonPreviousExam>(sqlGetAllByPersonId, mapper);
                IEnumerable<PersonPreviousExam> collection = accessor.Execute(id);

                candidateexamList = collection.ToList();
            }

            catch (Exception ex)
            {
                return candidateexamList;
            }

            return candidateexamList;
        }
    }
}
