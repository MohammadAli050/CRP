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
    public partial class SqlStudentDiscountCurrentDetailsRepository : IStudentDiscountCurrentDetailsRepository
    {

        Database db = null;

        private string sqlInsert = "StudentDiscountCurrentDetailsInsert";
        private string sqlUpdate = "StudentDiscountCurrentDetailsUpdate";
        private string sqlDelete = "StudentDiscountCurrentDetailsDelete";
        private string sqlGetById = "StudentDiscountCurrentDetailsGetById";
        private string sqlGetAll = "StudentDiscountCurrentDetailsGetAll";
        private string sqlGetAllByStudentDiscountId = "StudentDiscountCurrentDetailsGetByStudentDiscountId";
        private string sqlGenetareCurrentDiscount = "StudentDiscountCurrentDetailsGenetareCurrentDiscount";
        private string sqlGetAllByStudentDiscountIdAndAcaCalSessionId = "StudentDiscountCurrentDetailsByStudentDiscountIdAndAcaCalSessionId";

        public int Insert(StudentDiscountCurrentDetails studentdiscountcurrentdetails)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, studentdiscountcurrentdetails, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "StudentDiscountCurrentDetailsId");

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

        public bool Update(StudentDiscountCurrentDetails studentdiscountcurrentdetails)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, studentdiscountcurrentdetails, isInsert);

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

                db.AddInParameter(cmd, "StudentDiscountCurrentDetailsId", DbType.Int32, id);
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

        public StudentDiscountCurrentDetails GetById(int id)
        {
            StudentDiscountCurrentDetails _studentdiscountcurrentdetails = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountCurrentDetails> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountCurrentDetails>(sqlGetById, rowMapper);
                _studentdiscountcurrentdetails = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentdiscountcurrentdetails;
            }

            return _studentdiscountcurrentdetails;
        }

        public List<StudentDiscountCurrentDetails> GetAll()
        {
            List<StudentDiscountCurrentDetails> studentdiscountcurrentdetailsList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountCurrentDetails> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountCurrentDetails>(sqlGetAll, mapper);
                IEnumerable<StudentDiscountCurrentDetails> collection = accessor.Execute();

                studentdiscountcurrentdetailsList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentdiscountcurrentdetailsList;
            }

            return studentdiscountcurrentdetailsList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StudentDiscountCurrentDetails studentdiscountcurrentdetails, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "StudentDiscountCurrentDetailsId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "StudentDiscountCurrentDetailsId", DbType.Int32, studentdiscountcurrentdetails.StudentDiscountCurrentDetailsId);
            }

            db.AddInParameter(cmd, "StudentDiscountId", DbType.Int32, studentdiscountcurrentdetails.StudentDiscountId);
            db.AddInParameter(cmd, "TypeDefinitionId", DbType.Int32, studentdiscountcurrentdetails.TypeDefinitionId);
            db.AddInParameter(cmd, "TypePercentage", DbType.Decimal, studentdiscountcurrentdetails.TypePercentage);
            db.AddInParameter(cmd, "AcaCalSession", DbType.Int32, studentdiscountcurrentdetails.AcaCalSession);
            db.AddInParameter(cmd, "Comments", DbType.String, studentdiscountcurrentdetails.Comments);

            return db;
        }

        private IRowMapper<StudentDiscountCurrentDetails> GetMaper()
        {
            IRowMapper<StudentDiscountCurrentDetails> mapper = MapBuilder<StudentDiscountCurrentDetails>.MapAllProperties()

            .Map(m => m.StudentDiscountCurrentDetailsId).ToColumn("StudentDiscountCurrentDetailsId")
            .Map(m => m.StudentDiscountId).ToColumn("StudentDiscountId")
            .Map(m => m.TypeDefinitionId).ToColumn("TypeDefinitionId")
            .Map(m => m.TypePercentage).ToColumn("TypePercentage")
            .Map(m => m.AcaCalSession).ToColumn("AcaCalSession")

            .Build();

            return mapper;
        }
        #endregion

        public List<StudentDiscountCurrentDetails> GetByStudentDiscountId(int StudentDiscountId)
        {
            List<StudentDiscountCurrentDetails> studentdiscountcurrentdetailsList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountCurrentDetails> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountCurrentDetails>(sqlGetAllByStudentDiscountId, mapper);
                IEnumerable<StudentDiscountCurrentDetails> collection = accessor.Execute(StudentDiscountId);

                studentdiscountcurrentdetailsList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentdiscountcurrentdetailsList;
            }

            return studentdiscountcurrentdetailsList;
        }

        public bool GenetareCurrentDiscount(int acaCalBatch, int acaCalSession, int program)
        {
            bool result = false;


            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                DbCommand cmd = db.GetStoredProcCommand(sqlGenetareCurrentDiscount);

                db.AddInParameter(cmd, "BatchId", DbType.Int32, acaCalBatch);
                db.AddInParameter(cmd, "SessionId", DbType.Int32, acaCalSession);
                db.AddInParameter(cmd, "ProgramId", DbType.Decimal, program);

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

        public List<StudentDiscountCurrentDetails> GetByStudentDiscountAndAcaCalSession(int StudentDiscountId, int AcaCalSessionId)
        {
            List<StudentDiscountCurrentDetails> studentdiscountcurrentdetailsList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountCurrentDetails> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountCurrentDetails>(sqlGetAllByStudentDiscountIdAndAcaCalSessionId, mapper);
                IEnumerable<StudentDiscountCurrentDetails> collection = accessor.Execute(StudentDiscountId, AcaCalSessionId);

                studentdiscountcurrentdetailsList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentdiscountcurrentdetailsList;
            }

            return studentdiscountcurrentdetailsList;
        }
        
        public bool DiscountTransferFromInitialToCurrentPerStudent(int student, int batchId, int sessionId, int programId)
        {
            bool result = false;


            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                DbCommand cmd = db.GetStoredProcCommand("StudentDiscountCurrentDetailsGenetareCurrentDiscountPerStudent");

                db.AddInParameter(cmd, "StudentId", DbType.Int32, student);
                db.AddInParameter(cmd, "BatchId", DbType.Int32, batchId);
                db.AddInParameter(cmd, "SessionId", DbType.Int32, sessionId);
                db.AddInParameter(cmd, "ProgramId", DbType.Decimal, programId);

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

        #region IStudentDiscountCurrentDetailsRepository Members

        public List<StudentDiscountCurrentDetailsDTO> GetAllDiscountCurrentByProgramBatchRoll(int programId, int acaCalBatchId, int acaCalSessionId, string roll)
        {
            List<StudentDiscountCurrentDetailsDTO> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountCurrentDetailsDTO> mapper = MapBuilder<StudentDiscountCurrentDetailsDTO>.MapAllProperties()
               .Map(m => m.StudentID).ToColumn("StudentID")
               .Map(m => m.Roll).ToColumn("Roll")
               .Map(m => m.Name).ToColumn("Name")
               .Map(m => m.BatchId).ToColumn("BatchId")
               .Map(m => m.BatchCode).ToColumn("BatchNo")
               .Map(m => m.ProgramId).ToColumn("ProgramId")
               .Map(m => m.Program).ToColumn("Program")
               .Map(m => m.StudentDiscountId).ToColumn("StudentDiscountId")
               .Map(m => m.StudentDiscountCurrentDetailsId).ToColumn("StudentDiscountCurrentDetailsId")
               .Map(m => m.TypeDefinitionId).ToColumn("TypeDefinitionId")
               .Map(m => m.DiscountType).ToColumn("DiscountType")
               .Map(m => m.TypePercentage).ToColumn("TypePercentage")
               .Map(m => m.AcaCalSession).ToColumn("AcaCalSession")
               .Map(m => m.Comments).ToColumn("Comments")
               .Build();

                var accessor = db.CreateSprocAccessor<StudentDiscountCurrentDetailsDTO>("StudentDiscountCurrentDetailsDTOGetByProgramAcaCalRoll", mapper);
                IEnumerable<StudentDiscountCurrentDetailsDTO> collection = accessor.Execute(programId, acaCalBatchId, acaCalSessionId, roll);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public StudentDiscountCurrentDetails GetBy(int studentDiscountId, int typeDefinitionId, int acaCalSessionId)
        {
            StudentDiscountCurrentDetails _studentdiscountcurrentdetails = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountCurrentDetails> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountCurrentDetails>("StudentDiscountCurrentDetailsGetByStudentdiscountDiscountTypeSession", rowMapper);
                _studentdiscountcurrentdetails = accessor.Execute(studentDiscountId, typeDefinitionId, acaCalSessionId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentdiscountcurrentdetails;
            }

            return _studentdiscountcurrentdetails;
        }

        public bool DiscountPostingWaiver(int studentId, int batchId, int programId, int sessionId, int discountTypeId)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                DbCommand cmd = db.GetStoredProcCommand("StudentDiscountCurrentDetailsDiscountPostingWaiver");

                db.AddInParameter(cmd, "StudentId", DbType.Int32, studentId);
                db.AddInParameter(cmd, "BatchId", DbType.Int32, batchId);
                db.AddInParameter(cmd, "SessionId", DbType.Int32, sessionId);
                db.AddInParameter(cmd, "ProgramId", DbType.Int32, programId);
                db.AddInParameter(cmd, "DiscountTypeId", DbType.Int32, discountTypeId);

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

        public bool Delete(int studentId, int sessionId)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("StudentDiscountCurrentDetailsDeleteByStudentIdSessionId");

                db.AddInParameter(cmd, "StudentId", DbType.Int32, studentId);
                db.AddInParameter(cmd, "SessionId", DbType.Int32, sessionId);
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

        #endregion
    }
}

