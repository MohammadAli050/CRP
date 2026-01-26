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
    public partial class SqlExamTemplateCalculationTypeRepository : IExamTemplateCalculationTypeRepository
    {

        Database db = null;

        private string sqlInsert = "ExamTemplateCalculationTypeInsert";
        private string sqlUpdate = "ExamTemplateCalculationTypeUpdate";
        private string sqlDelete = "ExamTemplateCalculationTypeDelete";
        private string sqlGetById = "ExamTemplateCalculationTypeGetById";
        private string sqlGetAll = "ExamTemplateCalculationTypeGetAll";
               
        public int Insert(ExamTemplateCalculationType examtemplatecalculationtype)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, examtemplatecalculationtype, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "{5}");

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

        public bool Update(ExamTemplateCalculationType examtemplatecalculationtype)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, examtemplatecalculationtype, isInsert);

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

        public ExamTemplateCalculationType GetById(int? id)
        {
            ExamTemplateCalculationType _examtemplatecalculationtype = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplateCalculationType> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplateCalculationType>(sqlGetById, rowMapper);
                _examtemplatecalculationtype = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examtemplatecalculationtype;
            }

            return _examtemplatecalculationtype;
        }

        public List<ExamTemplateCalculationType> GetAll()
        {
            List<ExamTemplateCalculationType> examtemplatecalculationtypeList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplateCalculationType> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplateCalculationType>(sqlGetAll, mapper);
                IEnumerable<ExamTemplateCalculationType> collection = accessor.Execute();

                examtemplatecalculationtypeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examtemplatecalculationtypeList;
            }

            return examtemplatecalculationtypeList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamTemplateCalculationType examtemplatecalculationtype, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ExamCalculationTypeId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ExamCalculationTypeId", DbType.Int32, examtemplatecalculationtype.ExamCalculationTypeId);
            }

            db.AddInParameter(cmd,"ExamCalculationTypeId",DbType.Int32,examtemplatecalculationtype.ExamCalculationTypeId);
		    db.AddInParameter(cmd,"ExamCalculationTypeName",DbType.String,examtemplatecalculationtype.ExamCalculationTypeName);
		    db.AddInParameter(cmd,"Attribute1",DbType.Int32,examtemplatecalculationtype.Attribute1);
		    db.AddInParameter(cmd,"Attribute2",DbType.Int32,examtemplatecalculationtype.Attribute2);
		    db.AddInParameter(cmd,"Attribute3",DbType.Int32,examtemplatecalculationtype.Attribute3);
		    db.AddInParameter(cmd,"CreatedBy",DbType.Int32,examtemplatecalculationtype.CreatedBy);
		    db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,examtemplatecalculationtype.CreatedDate);
		    db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,examtemplatecalculationtype.ModifiedBy);
		    db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,examtemplatecalculationtype.ModifiedDate);
            
            return db;
        }

        private IRowMapper<ExamTemplateCalculationType> GetMaper()
        {
            IRowMapper<ExamTemplateCalculationType> mapper = MapBuilder<ExamTemplateCalculationType>.MapAllProperties()

       	   .Map(m => m.ExamCalculationTypeId).ToColumn("ExamCalculationTypeId")
		.Map(m => m.ExamCalculationTypeName).ToColumn("ExamCalculationTypeName")
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

    }
}

