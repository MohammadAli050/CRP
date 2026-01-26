using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
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
using LogicLayer.DataLogic.IRepository;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLPreviousExamTypeRepository : IPreviousExamTypeRepository
    {
        Database db = null;

        private string sqlInsert = "PreviousExamTypeInsert";
        private string sqlUpdate = "PreviousExamTypeUpdate";
        private string sqlDelete = "PreviousExamTypeDeleteById";
        private string sqlGetById = "PreviousExamTypeGetById";
        private string sqlGetAll = "PreviousExamTypeGetAll";
        private string sqlGetByValue = "PreviousExamTypeGetByValue";

        public int Insert(PreviousExamType examtype)
        {
            int id = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db.AddOutParameter(cmd, "PreviousExamTypeId", DbType.Int32, Int32.MaxValue);

                db.AddInParameter(cmd, "TypeName", DbType.String, examtype.TypeName);
                db.AddInParameter(cmd, "Code", DbType.String, examtype.Code);
                db.AddInParameter(cmd, "CreatedBy", DbType.Int64, examtype.CreatedBy);
                db.AddInParameter(cmd, "CreatedOn", DbType.DateTime, examtype.CreatedOn);
                db.AddInParameter(cmd, "ModifiedBy", DbType.Int64, examtype.ModifiedBy);
                db.AddInParameter(cmd, "ModifiedOn", DbType.DateTime, examtype.ModifiedOn);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "PreviousExamTypeId");

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

        public bool Update(PreviousExamType examtype)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db.AddInParameter(cmd, "PreviousExamTypeId", DbType.Int32, examtype.PreviousExamTypeId);
                db.AddInParameter(cmd, "TypeName", DbType.String, examtype.TypeName);
                db.AddInParameter(cmd, "Code", DbType.String, examtype.Code);
                db.AddInParameter(cmd, "CreatedBy", DbType.Int64, examtype.CreatedBy);
                db.AddInParameter(cmd, "CreatedOn", DbType.DateTime, examtype.CreatedOn);
                db.AddInParameter(cmd, "ModifiedBy", DbType.Int64, examtype.ModifiedBy);
                db.AddInParameter(cmd, "ModifiedOn", DbType.DateTime, examtype.ModifiedOn);

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

                db.AddInParameter(cmd, "PreviousExamTypeId", DbType.Int32, id);

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

        public PreviousExamType GetById(long id)
        {
            PreviousExamType _examtype = new PreviousExamType();
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PreviousExamType> rowMapper = MapBuilder<PreviousExamType>.MapNoProperties()
                .Map(m => m.PreviousExamTypeId).ToColumn("PreviousExamTypeId")

                .Map(m => m.TypeName).ToColumn("TypeName")
                .Map(m => m.Code).ToColumn("Code")
                .Map(m => m.EducationCategory).ToColumn("EducationCategory")
                .Map(m => m.CreatedBy).ToColumn("CreatedBy")
                .Map(m => m.CreatedOn).ToColumn("CreatedOn")
                .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
                .Map(m => m.ModifiedOn).ToColumn("ModifiedOn")

                .Build();

                var accessor = db.CreateSprocAccessor<PreviousExamType>(sqlGetById, rowMapper);
                _examtype = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examtype;
            }

            return _examtype;
        }

        public List<PreviousExamType> GetAll()
        {
            List<PreviousExamType> examtypeList = new List<PreviousExamType>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PreviousExamType> mapper = MapBuilder<PreviousExamType>.MapAllProperties()
                .Map(m => m.PreviousExamTypeId).ToColumn("PreviousExamTypeId")

                .Map(m => m.TypeName).ToColumn("TypeName")
                .Map(m => m.Code).ToColumn("Code")
                .Map(m => m.EducationCategory).ToColumn("EducationCategory")
                .Map(m => m.CreatedBy).ToColumn("CreatedBy")
                .Map(m => m.CreatedOn).ToColumn("CreatedOn")
                .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
                .Map(m => m.ModifiedOn).ToColumn("ModifiedOn")

                .Build();

                var accessor = db.CreateSprocAccessor<PreviousExamType>(sqlGetAll, mapper);
                IEnumerable<PreviousExamType> collection = accessor.Execute();

                examtypeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examtypeList;
            }

            return examtypeList;
        }


        public List<PreviousExamType> Search(string value)
        {
            List<PreviousExamType> examtypeList = new List<PreviousExamType>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PreviousExamType> mapper = MapBuilder<PreviousExamType>.MapAllProperties()
                .Map(m => m.PreviousExamTypeId).ToColumn("PreviousExamTypeId")
                .Map(m => m.TypeName).ToColumn("TypeName")
                .Map(m => m.Code).ToColumn("Code")
                .Map(m => m.EducationCategory).ToColumn("EducationCategory")
                .Map(m => m.CreatedBy).ToColumn("CreatedBy")
                .Map(m => m.CreatedOn).ToColumn("CreatedOn")
                .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
                .Map(m => m.ModifiedOn).ToColumn("ModifiedOn")

                .Build();

                var accessor = db.CreateSprocAccessor<PreviousExamType>(sqlGetByValue, mapper);
                IEnumerable<PreviousExamType> collection = accessor.Execute(value);

                examtypeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examtypeList;
            }

            return examtypeList;
        }
    }
}
