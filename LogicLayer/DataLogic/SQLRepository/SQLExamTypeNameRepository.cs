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
    partial class SQLExamTypeNameRepository : IExamTypeNameRepository
    {
        Database db = null;

        private string sqlInsert = "ExamTypeNameInsert";
        private string sqlUpdate = "ExamTypeNameUpdate";
        private string sqlDelete = "ExamTypeNameDeleteById";
        private string sqlGetById ="ExamTypeNameGetById";
        private string sqlGetAll = "ExamTypeNameGetAll";


        public int Insert(ExamTypeName examTypeName)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, examTypeName, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ExamTypeNameID");

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

        public bool Update(ExamTypeName examTypeName)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, examTypeName, isInsert);

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

                db.AddInParameter(cmd, "ExamTypeNameID", DbType.Int32, id);
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

        public ExamTypeName GetById(int? id)
        {
            ExamTypeName _examTypeName = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTypeName> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTypeName>(sqlGetById, rowMapper);
                _examTypeName = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examTypeName;
            }

            return _examTypeName;
        }

        public List<ExamTypeName> GetAll()
        {
            List<ExamTypeName> examTypeNameList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTypeName> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTypeName>(sqlGetAll, mapper);
                IEnumerable<ExamTypeName> collection = accessor.Execute();

                examTypeNameList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examTypeNameList;
            }

            return examTypeNameList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamTypeName examTypeName, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ExamTypeNameID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ExamTypeNameID", DbType.Int32, examTypeName.ExamTypeNameID);
            }

            db.AddInParameter(cmd, "ExamTypeNameID", DbType.Int32, examTypeName.ExamTypeNameID);
            db.AddInParameter(cmd, "TypeDefinitionID", DbType.Int32, examTypeName.TypeDefinitionID);
            db.AddInParameter(cmd, "Name", DbType.Int32, examTypeName.Name);
            db.AddInParameter(cmd, "TotalAllottedMarks", DbType.String, examTypeName.TotalAllottedMarks);
            db.AddInParameter(cmd, "Default", DbType.String, examTypeName.Default);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, examTypeName.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, examTypeName.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, examTypeName.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, examTypeName.ModifiedDate);

            return db;
        }

        private IRowMapper<ExamTypeName> GetMaper()
        {
            IRowMapper<ExamTypeName> mapper = MapBuilder<ExamTypeName>.MapAllProperties()
            .Map(m => m.ExamTypeNameID).ToColumn("ExamTypeNameID")
            .Map(m => m.TypeDefinitionID).ToColumn("TypeDefinitionID")
            .Map(m => m.Name).ToColumn("Name")
            .Map(m => m.TotalAllottedMarks).ToColumn("TotalAllottedMarks")
            .Map(m => m.Default).ToColumn("Default")
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
