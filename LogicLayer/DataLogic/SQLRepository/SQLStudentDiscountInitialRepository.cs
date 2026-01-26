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
    public partial class SqlStudentDiscountInitialRepository : IStudentDiscountInitialRepository
    {

        Database db = null;

        private string sqlInsert = "StudentDiscountInitialInsert";
        private string sqlUpdate = "StudentDiscountInitialUpdate";
        private string sqlDelete = "StudentDiscountInitialDelete";
        private string sqlGetById = "StudentDiscountInitialGetById";
        private string sqlGetAll = "StudentDiscountInitialGetAll";

        public int Insert(StudentDiscountInitial studentdiscountinitial)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, studentdiscountinitial, isInsert);
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

        public bool Update(StudentDiscountInitial studentdiscountinitial)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, studentdiscountinitial, isInsert);

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

        public StudentDiscountInitial GetById(int? id)
        {
            StudentDiscountInitial _studentdiscountinitial = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountInitial> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountInitial>(sqlGetById, rowMapper);
                _studentdiscountinitial = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentdiscountinitial;
            }

            return _studentdiscountinitial;
        }

        public List<StudentDiscountInitial> GetAll()
        {
            List<StudentDiscountInitial> studentdiscountinitialList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountInitial> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountInitial>(sqlGetAll, mapper);
                IEnumerable<StudentDiscountInitial> collection = accessor.Execute();

                studentdiscountinitialList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentdiscountinitialList;
            }

            return studentdiscountinitialList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StudentDiscountInitial studentdiscountinitial, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, studentdiscountinitial.Id);
            }

            db.AddInParameter(cmd,"PersonId",DbType.Int32,studentdiscountinitial.PersonId);
            db.AddInParameter(cmd,"TypeDefinitionID",DbType.Int32,studentdiscountinitial.TypeDefinitionID);
		
            db.AddInParameter(cmd,"BatchId",DbType.Int32,studentdiscountinitial.BatchId);
            db.AddInParameter(cmd,"Effective_AcaCalId",DbType.Int32,studentdiscountinitial.Effective_AcaCalId);
            db.AddInParameter(cmd,"CreatedBy",DbType.Int32,studentdiscountinitial.CreatedBy);
            db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,studentdiscountinitial.CreatedDate);
            db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,studentdiscountinitial.ModifiedBy);
            db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,studentdiscountinitial.ModifiedDate);
            
            return db;
        }

        private IRowMapper<StudentDiscountInitial> GetMaper()
        {
            IRowMapper<StudentDiscountInitial> mapper = MapBuilder<StudentDiscountInitial>.MapAllProperties()

            .Map(m => m.Id).ToColumn("Id")
            .Map(m => m.PersonId).ToColumn("PersonId")
            .Map(m => m.TypeDefinitionID).ToColumn("TypeDefinitionID")
            .Map(m => m.Percentage).ToColumn("Percentage")
            .Map(m => m.BatchId).ToColumn("BatchId")
            .Map(m => m.Effective_AcaCalId).ToColumn("Effective_AcaCalId")
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

