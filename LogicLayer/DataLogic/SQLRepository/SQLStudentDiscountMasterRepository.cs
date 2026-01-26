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
    public partial class SqlStudentDiscountMasterRepository : IStudentDiscountMasterRepository
    {

        Database db = null;

        private string sqlInsert = "StudentDiscountMasterInsert";
        private string sqlUpdate = "StudentDiscountMasterUpdate";
        private string sqlDelete = "StudentDiscountMasterDelete";
        private string sqlGetById = "StudentDiscountMasterGetById";
        private string sqlGetAll = "StudentDiscountMasterGetAll";
        private string sqlGetByStudentID = "StudentDiscountMasterGetByStudentID";

        private string sqlGetByAcaCalIDProgramID = "StudentDiscountMasterGetByAcaCalIDProgramID";

        public int Insert(StudentDiscountMaster studentdiscountmaster)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, studentdiscountmaster, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "StudentDiscountId");

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

        public bool Update(StudentDiscountMaster studentdiscountmaster)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, studentdiscountmaster, isInsert);

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

                db.AddInParameter(cmd, "StudentDiscountId", DbType.Int32, id);
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
        
        public StudentDiscountMaster GetById(int id)
        {
            StudentDiscountMaster _studentdiscountmaster = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountMaster> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountMaster>(sqlGetById, rowMapper);
                _studentdiscountmaster = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentdiscountmaster;
            }

            return _studentdiscountmaster;
        }

        public List<StudentDiscountMaster> GetAll()
        {
            List<StudentDiscountMaster> studentdiscountmasterList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountMaster> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountMaster>(sqlGetAll, mapper);
                IEnumerable<StudentDiscountMaster> collection = accessor.Execute();

                studentdiscountmasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentdiscountmasterList;
            }

            return studentdiscountmasterList;
        }
        public List<StudentDiscountMaster> GetByAcaCalIDProgramID(int acaCalID, int programId)
        {
            List<StudentDiscountMaster> studentdiscountmasterList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountMaster> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountMaster>(sqlGetByAcaCalIDProgramID, mapper);
                IEnumerable<StudentDiscountMaster> collection = accessor.Execute(acaCalID, programId);

                studentdiscountmasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentdiscountmasterList;
            }

            return studentdiscountmasterList;
        }
        
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StudentDiscountMaster studentdiscountmaster, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "StudentDiscountId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "StudentDiscountId", DbType.Int32, studentdiscountmaster.StudentDiscountId);
            }


            db.AddInParameter(cmd, "StudentId", DbType.Int32, studentdiscountmaster.StudentId);
            db.AddInParameter(cmd, "BatchId", DbType.Int32, studentdiscountmaster.BatchId);
            db.AddInParameter(cmd, "ProgramId", DbType.Int32, studentdiscountmaster.ProgramId);
            db.AddInParameter(cmd, "Remarks", DbType.String, studentdiscountmaster.Remarks);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, studentdiscountmaster.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, studentdiscountmaster.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, studentdiscountmaster.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, studentdiscountmaster.ModifiedDate);

            return db;
        }

        private IRowMapper<StudentDiscountMaster> GetMaper()
        {
            IRowMapper<StudentDiscountMaster> mapper = MapBuilder<StudentDiscountMaster>.MapAllProperties()

           .Map(m => m.StudentDiscountId).ToColumn("StudentDiscountId")
        .Map(m => m.StudentId).ToColumn("StudentId")
        .Map(m => m.BatchId).ToColumn("BatchId")
        .Map(m => m.ProgramId).ToColumn("ProgramId")
        .Map(m => m.Remarks).ToColumn("Remarks")
        .Map(m => m.CreatedBy).ToColumn("CreatedBy")
        .Map(m => m.CreatedDate).ToColumn("CreatedDate")
        .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
        .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")

            .Build();

            return mapper;
        }
        #endregion
        
        public StudentDiscountMaster GetByStudentID(int StudentID)
        {
            StudentDiscountMaster _studentdiscountmaster = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountMaster> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountMaster>(sqlGetByStudentID, rowMapper);
                _studentdiscountmaster = accessor.Execute(StudentID).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentdiscountmaster;
            }

            return _studentdiscountmaster;
        }        
    }
}

