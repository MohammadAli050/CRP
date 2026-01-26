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
    public partial class SQLStudentDiscountAndScholarshipPerSessionRepository : IStudentDiscountAndScholarshipPerSessionRepository
    {

        Database db = null;

        private string sqlInsert = "StudentDiscountAndScholarshipPerSessionInsert";
        private string sqlUpdate = "StudentDiscountAndScholarshipPerSessionUpdate";
        private string sqlDelete = "StudentDiscountAndScholarshipPerSessionDelete";
        private string sqlDeleteByStudentIdSessionIdTdId = "StudentDiscountAndScholarshipPerSessionDeleteByStudentIdSessionIdTdId";
        private string sqlGetById = "StudentDiscountAndScholarshipPerSessionGetById";
        private string sqlGetAll = "StudentDiscountAndScholarshipPerSessionGetAll";
        private string sqlGetAllBySessionIDProgramID = "StudentDiscountAndScholarshipPerSessionGetAllBySessionIDProgramID";
        private string sqlGetAllByByAcaCalIDProgramID = "StudentDiscountAndScholarshipPerSessionGetAll";



        public int Insert(StudentDiscountAndScholarshipPerSession studentdiscountandscholarshippersession)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, studentdiscountandscholarshippersession, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "StudentDiscountAndScholarshipId");

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

        public bool Update(StudentDiscountAndScholarshipPerSession studentdiscountandscholarshippersession)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, studentdiscountandscholarshippersession, isInsert);

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

                db.AddInParameter(cmd, "StudentDiscountAndScholarshipId", DbType.Int32, id);
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

        public StudentDiscountAndScholarshipPerSession GetById(int id)
        {
            StudentDiscountAndScholarshipPerSession _studentdiscountandscholarshippersession = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountAndScholarshipPerSession> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountAndScholarshipPerSession>(sqlGetById, rowMapper);
                _studentdiscountandscholarshippersession = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentdiscountandscholarshippersession;
            }

            return _studentdiscountandscholarshippersession;
        }

        public List<StudentDiscountAndScholarshipPerSession> GetAll()
        {
            List<StudentDiscountAndScholarshipPerSession> studentdiscountandscholarshippersessionList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountAndScholarshipPerSession> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountAndScholarshipPerSession>(sqlGetAll, mapper);
                IEnumerable<StudentDiscountAndScholarshipPerSession> collection = accessor.Execute();

                studentdiscountandscholarshippersessionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentdiscountandscholarshippersessionList;
            }

            return studentdiscountandscholarshippersessionList;
        }
        public List<StudentDiscountAndScholarshipPerSession> GetAllBySessionIDProgramID(int sessionId, int programId)
        {
            List<StudentDiscountAndScholarshipPerSession> studentdiscountandscholarshippersessionList = null;


            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountAndScholarshipPerSession> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountAndScholarshipPerSession>(sqlGetAllBySessionIDProgramID, mapper);
                IEnumerable<StudentDiscountAndScholarshipPerSession> collection = accessor.Execute(sessionId, programId);

                studentdiscountandscholarshippersessionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentdiscountandscholarshippersessionList;
            }

            return studentdiscountandscholarshippersessionList;
        }
        


        public List<StudentDiscountAndScholarshipPerSession> GetAllByByAcaCalIDProgramID(int AcaCalId, int ProgramId)
        {
            List<StudentDiscountAndScholarshipPerSession> studentdiscountandscholarshippersessionList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountAndScholarshipPerSession> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountAndScholarshipPerSession>(sqlGetAllByByAcaCalIDProgramID, mapper);
                IEnumerable<StudentDiscountAndScholarshipPerSession> collection = accessor.Execute(AcaCalId,ProgramId);

                studentdiscountandscholarshippersessionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentdiscountandscholarshippersessionList;
            }

            return studentdiscountandscholarshippersessionList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StudentDiscountAndScholarshipPerSession studentdiscountandscholarshippersession, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "StudentDiscountAndScholarshipId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "StudentDiscountAndScholarshipId", DbType.Int32, studentdiscountandscholarshippersession.StudentDiscountAndScholarshipId);
            }


            db.AddInParameter(cmd, "StudentId", DbType.Int32, studentdiscountandscholarshippersession.StudentId);
            db.AddInParameter(cmd, "TypeDefinitionId", DbType.Int32, studentdiscountandscholarshippersession.TypeDefinitionId);
            db.AddInParameter(cmd, "Discount", DbType.Decimal, studentdiscountandscholarshippersession.Discount);
            db.AddInParameter(cmd, "AcaCalSession", DbType.Int32, studentdiscountandscholarshippersession.AcaCalSession);
            db.AddInParameter(cmd, "Remarks", DbType.String, studentdiscountandscholarshippersession.Remarks);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, studentdiscountandscholarshippersession.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, studentdiscountandscholarshippersession.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, studentdiscountandscholarshippersession.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, studentdiscountandscholarshippersession.ModifiedDate);

            return db;
        }

        private IRowMapper<StudentDiscountAndScholarshipPerSession> GetMaper()
        {
            IRowMapper<StudentDiscountAndScholarshipPerSession> mapper = MapBuilder<StudentDiscountAndScholarshipPerSession>.MapAllProperties()

           .Map(m => m.StudentDiscountAndScholarshipId).ToColumn("StudentDiscountAndScholarshipId")
        .Map(m => m.StudentId).ToColumn("StudentId")
        .Map(m => m.TypeDefinitionId).ToColumn("TypeDefinitionId")
        .Map(m => m.Discount).ToColumn("Discount")
        .Map(m => m.AcaCalSession).ToColumn("AcaCalSession")
        .Map(m => m.Remarks).ToColumn("Remarks")
        .Map(m => m.CreatedBy).ToColumn("CreatedBy")
        .Map(m => m.CreatedDate).ToColumn("CreatedDate")
        .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
        .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")

            .Build();

            return mapper;
        }
        #endregion

        public bool Delete(int studentId, int sessionId, int tdId)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlDeleteByStudentIdSessionIdTdId);

                db.AddInParameter(cmd, "studentId", DbType.Int32, studentId);
                db.AddInParameter(cmd, "sessionId", DbType.Int32, sessionId);
                db.AddInParameter(cmd, "tdId", DbType.Int32, tdId);
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

        public List<StudentDiscountAndScholarshipPerSessionCount> getCountByProgramBatch(int sessionId)
        {
            List<StudentDiscountAndScholarshipPerSessionCount> ListCount = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountAndScholarshipPerSessionCount> mapper = MapBuilder<StudentDiscountAndScholarshipPerSessionCount>.MapAllProperties()
                .Map(m => m.StudentCount).ToColumn("StudentCount")
                .Map(m => m.AcaCalId).ToColumn("AcaCalId")
                .Map(m => m.BatchCode).ToColumn("BatchCode")
                .Map(m => m.Program).ToColumn("Program")
                .Map(m => m.ProgramId).ToColumn("ProgramId")
                .Map(m => m.UnitTypeName).ToColumn("UnitTypeName")
                .Map(m => m.Year).ToColumn("Year")
                .Build();

                var accessor = db.CreateSprocAccessor<StudentDiscountAndScholarshipPerSessionCount>("StudentDiscountAndScholarshipPerSessionCountByProgramBatch", mapper);
                IEnumerable<StudentDiscountAndScholarshipPerSessionCount> collection = accessor.Execute(sessionId);

                ListCount = collection.ToList();
            }

            catch (Exception ex)
            {
                return ListCount;
            }

            return ListCount;
        }
        
    }
}

