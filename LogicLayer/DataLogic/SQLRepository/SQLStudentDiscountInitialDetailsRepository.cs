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
    public partial class SqlStudentDiscountInitialDetailsRepository : IStudentDiscountInitialDetailsRepository
    {

        Database db = null;

        private string sqlInsert = "StudentDiscountInitialDetailsInsert";
        private string sqlUpdate = "StudentDiscountInitialDetailsUpdate";
        private string sqlDelete = "StudentDiscountInitialDetailsDelete";
        private string sqlGetById = "StudentDiscountInitialDetailsGetById";
        private string sqlGetAll = "StudentDiscountInitialDetailsGetAll";
        private string sqlGetAllByStudentDiscountId = "StudentDiscountInitialDetailsGetByStudentDiscountId";

        public int Insert(StudentDiscountInitialDetails studentdiscountinitialdetails)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, studentdiscountinitialdetails, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "StudentDiscountInitialDetailsId");

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

        public bool Update(StudentDiscountInitialDetails studentdiscountinitialdetails)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, studentdiscountinitialdetails, isInsert);

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

                db.AddInParameter(cmd, "StudentDiscountInitialDetailsId", DbType.Int32, id);
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

        public StudentDiscountInitialDetails GetById(int id)
        {
            StudentDiscountInitialDetails _studentdiscountinitialdetails = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountInitialDetails> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountInitialDetails>(sqlGetById, rowMapper);
                _studentdiscountinitialdetails = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentdiscountinitialdetails;
            }

            return _studentdiscountinitialdetails;
        }

        public List<StudentDiscountInitialDetails> GetAll()
        {
            List<StudentDiscountInitialDetails> studentdiscountinitialdetailsList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountInitialDetails> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountInitialDetails>(sqlGetAll, mapper);
                IEnumerable<StudentDiscountInitialDetails> collection = accessor.Execute();

                studentdiscountinitialdetailsList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentdiscountinitialdetailsList;
            }

            return studentdiscountinitialdetailsList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StudentDiscountInitialDetails studentdiscountinitialdetails, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "StudentDiscountInitialDetailsId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "StudentDiscountInitialDetailsId", DbType.Int32, studentdiscountinitialdetails.StudentDiscountInitialDetailsId);
            }


            db.AddInParameter(cmd, "StudentDiscountId", DbType.Int32, studentdiscountinitialdetails.StudentDiscountId);
            db.AddInParameter(cmd, "TypeDefinitionId", DbType.Int32, studentdiscountinitialdetails.TypeDefinitionId);
            db.AddInParameter(cmd, "TypePercentage", DbType.Decimal, studentdiscountinitialdetails.TypePercentage);
            db.AddInParameter(cmd, "AcaCalSession", DbType.Int32, studentdiscountinitialdetails.AcaCalSession);
            db.AddInParameter(cmd, "Comments", DbType.String, studentdiscountinitialdetails.Comments);

            return db;
        }

        private IRowMapper<StudentDiscountInitialDetails> GetMaper()
        {
            IRowMapper<StudentDiscountInitialDetails> mapper = MapBuilder<StudentDiscountInitialDetails>.MapAllProperties()

        .Map(m => m.StudentDiscountInitialDetailsId).ToColumn("StudentDiscountInitialDetailsId")
        .Map(m => m.StudentDiscountId).ToColumn("StudentDiscountId")
        .Map(m => m.TypeDefinitionId).ToColumn("TypeDefinitionId")
        .Map(m => m.TypePercentage).ToColumn("TypePercentage")
        .Map(m => m.AcaCalSession).ToColumn("AcaCalSession")
        .Map(m => m.Comments).ToColumn("Comments")
            .Build();

            return mapper;
        }
        #endregion

        public List<StudentDiscountInitialDetails> GetByStudentDiscountId(int StudentDiscountId)
        {
            List<StudentDiscountInitialDetails> studentdiscountinitialdetailsList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountInitialDetails> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountInitialDetails>(sqlGetAllByStudentDiscountId, mapper);
                IEnumerable<StudentDiscountInitialDetails> collection = accessor.Execute(StudentDiscountId);

                studentdiscountinitialdetailsList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentdiscountinitialdetailsList;
            }

            return studentdiscountinitialdetailsList;
        }

        #region IStudentDiscountInitialDetailsRepository Members


        public List<StudentDiscountInitialDetailsDTO> GetByStudentDiscountId(int programId, int acaCalId, string roll)
        {
            List<StudentDiscountInitialDetailsDTO> studentdiscountinitialdetailsList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountInitialDetailsDTO> mapper = MapBuilder<StudentDiscountInitialDetailsDTO>.MapAllProperties()
               .Map(m => m.StudentID).ToColumn("StudentID")
               .Map(m => m.Roll).ToColumn("Roll")
               .Map(m => m.Name).ToColumn("Name")
               .Map(m => m.BatchId).ToColumn("BatchId")
               .Map(m => m.BatchCode).ToColumn("BatchNO")
               .Map(m => m.ProgramId).ToColumn("ProgramId")
               .Map(m => m.Program).ToColumn("Program")
               .Map(m => m.StudentDiscountId).ToColumn("StudentDiscountId")
               .Map(m => m.StudentDiscountInitialDetailsId).ToColumn("StudentDiscountInitialDetailsId")
               .Map(m => m.TypeDefinitionId).ToColumn("TypeDefinitionId")
               .Map(m => m.DiscountType).ToColumn("DiscountType")
               .Map(m => m.TypePercentage).ToColumn("TypePercentage")
               .Map(m => m.Comments).ToColumn("Comments")
               .Build();

                var accessor = db.CreateSprocAccessor<StudentDiscountInitialDetailsDTO>("StudentDiscountInitialDetailsGetByProgramAcaCalRoll", mapper);
                IEnumerable<StudentDiscountInitialDetailsDTO> collection = accessor.Execute(programId, acaCalId, roll);

                studentdiscountinitialdetailsList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentdiscountinitialdetailsList;
            }

            return studentdiscountinitialdetailsList;
        }

        #endregion
    }
}

