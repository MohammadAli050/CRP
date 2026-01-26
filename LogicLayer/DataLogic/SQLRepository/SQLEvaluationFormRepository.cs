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
    public partial class SQLEvaluationFormRepository : IEvaluationFormRepository
    {

        Database db = null;

        private string sqlInsert = "EvaluationFormInsert";
        private string sqlUpdate = "EvaluationFormUpdate";
        private string sqlDelete = "EvaluationFormDelete";
        private string sqlGetById = "EvaluationFormGetById";
        private string sqlGetAll = "EvaluationFormGetAll";
        private string sqlGetAllByAcaCalSecId = "EvaluationFormGetAllByAcaCalSecId";
        private string sqlGetAllByAcaCalId = "EvaluationFormGetAllByAcaCalId";
        private string sqlGetAllByPersonId = "EvaluationFormGetAllByPersonId";
        private string sqlGetByPersonAcaCalSec = "EvaluationFormGetByPersonAcaCalSec";

        public int Insert(EvaluationForm evaluationform)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, evaluationform, isInsert);
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

        public bool Update(EvaluationForm evaluationform)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, evaluationform, isInsert);

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

                db.AddInParameter(cmd, "{5}", DbType.Int32, id);
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

        public EvaluationForm GetById(int id)
        {
            EvaluationForm _evaluationform = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<EvaluationForm> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<EvaluationForm>(sqlGetById, rowMapper);
                _evaluationform = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _evaluationform;
            }

            return _evaluationform;
        }

        public List<EvaluationForm> GetAll()
        {
            List<EvaluationForm> evaluationformList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<EvaluationForm> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<EvaluationForm>(sqlGetAll, mapper);
                IEnumerable<EvaluationForm> collection = accessor.Execute();

                evaluationformList = collection.ToList();
            }

            catch (Exception ex)
            {
                return evaluationformList;
            }

            return evaluationformList;
        }

        public List<EvaluationForm> GetAll(int acaCalSecId)
        {
            List<EvaluationForm> evaluationFormList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<EvaluationForm> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<EvaluationForm>(sqlGetAllByAcaCalSecId, mapper);
                IEnumerable<EvaluationForm> collection = accessor.Execute(acaCalSecId);

                evaluationFormList = collection.ToList();
            }

            catch (Exception ex)
            {
                return evaluationFormList;
            }

            return evaluationFormList;
        }

        public List<EvaluationForm> GetAllByAcaCalId(int acaCalId)
        {
            List<EvaluationForm> evaluationFormList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<EvaluationForm> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<EvaluationForm>(sqlGetAllByAcaCalId, mapper);
                IEnumerable<EvaluationForm> collection = accessor.Execute(acaCalId);

                evaluationFormList = collection.ToList();
            }

            catch (Exception ex)
            {
                return evaluationFormList;
            }

            return evaluationFormList;
        }

        public List<EvaluationForm> GetAllByPersonId(int personId)
        {
            List<EvaluationForm> evaluationFormList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<EvaluationForm> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<EvaluationForm>(sqlGetAllByPersonId, mapper);
                IEnumerable<EvaluationForm> collection = accessor.Execute(personId);

                evaluationFormList = collection.ToList();
            }

            catch (Exception ex)
            {
                return evaluationFormList;
            }

            return evaluationFormList;
        }

        public EvaluationForm GetBy(int personId, int acaCalSecId)
        {
            EvaluationForm _evaluationform = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<EvaluationForm> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<EvaluationForm>(sqlGetByPersonAcaCalSec, rowMapper);
                _evaluationform = accessor.Execute(personId, acaCalSecId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _evaluationform;
            }

            return _evaluationform;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, EvaluationForm evaluationform, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, evaluationform.Id);
            }

		    db.AddInParameter(cmd,"PersonId",DbType.Int32,evaluationform.PersonId);
		    db.AddInParameter(cmd,"AcaCalSecId",DbType.Int32,evaluationform.AcaCalSecId);
		    db.AddInParameter(cmd,"AcaCalId",DbType.Int32,evaluationform.AcaCalId);
            db.AddInParameter(cmd,"ExpectedGrade",DbType.String,evaluationform.ExpectedGrade);
		    db.AddInParameter(cmd,"QSet",DbType.Int32,evaluationform.QSet);
		    db.AddInParameter(cmd,"Q1",DbType.Int32,evaluationform.Q1);
		    db.AddInParameter(cmd,"Q2",DbType.Int32,evaluationform.Q2);
		    db.AddInParameter(cmd,"Q3",DbType.Int32,evaluationform.Q3);
		    db.AddInParameter(cmd,"Q4",DbType.Int32,evaluationform.Q4);
		    db.AddInParameter(cmd,"Q5",DbType.Int32,evaluationform.Q5);
		    db.AddInParameter(cmd,"Q6",DbType.Int32,evaluationform.Q6);
		    db.AddInParameter(cmd,"Q7",DbType.Int32,evaluationform.Q7);
		    db.AddInParameter(cmd,"Q8",DbType.Int32,evaluationform.Q8);
		    db.AddInParameter(cmd,"Q9",DbType.Int32,evaluationform.Q9);
		    db.AddInParameter(cmd,"Q10",DbType.Int32,evaluationform.Q10);
		    db.AddInParameter(cmd,"Q11",DbType.Int32,evaluationform.Q11);
		    db.AddInParameter(cmd,"Q12",DbType.Int32,evaluationform.Q12);
		    db.AddInParameter(cmd,"Q13",DbType.Int32,evaluationform.Q13);
		    db.AddInParameter(cmd,"Q14",DbType.Int32,evaluationform.Q14);
		    db.AddInParameter(cmd,"Q15",DbType.Int32,evaluationform.Q15);
		    db.AddInParameter(cmd,"Q16",DbType.Int32,evaluationform.Q16);
		    db.AddInParameter(cmd,"Q17",DbType.Int32,evaluationform.Q17);
		    db.AddInParameter(cmd,"Q18",DbType.Int32,evaluationform.Q18);
		    db.AddInParameter(cmd,"Q19",DbType.Int32,evaluationform.Q19);
		    db.AddInParameter(cmd,"Q20",DbType.Int32,evaluationform.Q20);
		    db.AddInParameter(cmd,"Comment",DbType.String,evaluationform.Comment);
		    db.AddInParameter(cmd,"IsFinalSubmit",DbType.Boolean,evaluationform.IsFinalSubmit);
		    db.AddInParameter(cmd,"CreatedBy",DbType.Int32,evaluationform.CreatedBy);
		    db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,evaluationform.CreatedDate);
		    db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,evaluationform.ModifiedBy);
		    db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,evaluationform.ModifiedDate);
            
            return db;
        }

        private IRowMapper<EvaluationForm> GetMaper()
        {
            IRowMapper<EvaluationForm> mapper = MapBuilder<EvaluationForm>.MapAllProperties()

            .Map(m => m.Id).ToColumn("Id")
            .Map(m => m.PersonId).ToColumn("PersonId")
            .Map(m => m.AcaCalSecId).ToColumn("AcaCalSecId")
            .Map(m => m.AcaCalId).ToColumn("AcaCalId")
            .Map(m => m.ExpectedGrade).ToColumn("ExpectedGrade")
            .Map(m => m.QSet).ToColumn("QSet")
            .Map(m => m.Q1).ToColumn("Q1")
            .Map(m => m.Q2).ToColumn("Q2")
            .Map(m => m.Q3).ToColumn("Q3")
            .Map(m => m.Q4).ToColumn("Q4")
            .Map(m => m.Q5).ToColumn("Q5")
            .Map(m => m.Q6).ToColumn("Q6")
            .Map(m => m.Q7).ToColumn("Q7")
            .Map(m => m.Q8).ToColumn("Q8")
            .Map(m => m.Q9).ToColumn("Q9")
            .Map(m => m.Q10).ToColumn("Q10")
            .Map(m => m.Q11).ToColumn("Q11")
            .Map(m => m.Q12).ToColumn("Q12")
            .Map(m => m.Q13).ToColumn("Q13")
            .Map(m => m.Q14).ToColumn("Q14")
            .Map(m => m.Q15).ToColumn("Q15")
            .Map(m => m.Q16).ToColumn("Q16")
            .Map(m => m.Q17).ToColumn("Q17")
            .Map(m => m.Q18).ToColumn("Q18")
            .Map(m => m.Q19).ToColumn("Q19")
            .Map(m => m.Q20).ToColumn("Q20")
            .Map(m => m.Comment).ToColumn("Comment")
            .Map(m => m.IsFinalSubmit).ToColumn("IsFinalSubmit")
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
