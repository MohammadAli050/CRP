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
    public partial class SQLAcademicCalenderRepository : IAcademicCalenderRepository
    {
        Database db = null;

        private string sqlInsert = "AcademicCalenderInsert";
        private string sqlUpdate = "AcademicCalenderUpdate";
        private string sqlDelete = "AcademicCalenderDeleteById";
        private string sqlGetById = "AcademicCalenderGetById";
        private string sqlGetAll = "AcademicCalenderGetAll";
        private string sqlGetCustom = "RptAcademicCalenderGetCustom";

        public int Insert(AcademicCalender academicCalender)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, academicCalender, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "AcademicCalenderID");

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

        public bool Update(AcademicCalender academicCalender)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, academicCalender, isInsert);

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

                db.AddInParameter(cmd, "AcademicCalenderID", DbType.Int32, id);
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

        public AcademicCalender GetById(int? id)
        {
            AcademicCalender _academicCalender = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalender> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalender>(sqlGetById, rowMapper);
                _academicCalender = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _academicCalender;
            }

            return _academicCalender;
        }

        public List<AcademicCalender> GetAll()
        {
            List<AcademicCalender> academicCalenderList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalender> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalender>(sqlGetAll, mapper);
                IEnumerable<AcademicCalender> collection = accessor.Execute();

                academicCalenderList = collection.ToList();
            }

            catch (Exception ex)
            {
                return academicCalenderList;
            }

            return academicCalenderList;
        }

        public List<AcademicCalender> GetCustom()
        {
            List<AcademicCalender> academicCalenderList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalender> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalender>(sqlGetCustom, mapper);
                IEnumerable<AcademicCalender> collection = accessor.Execute();

                academicCalenderList = collection.ToList();
            }

            catch (Exception ex)
            {
                return academicCalenderList;
            }

            return academicCalenderList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, AcademicCalender academiccalender, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "AcademicCalenderID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "AcademicCalenderID", DbType.Int32, academiccalender.AcademicCalenderID);
            }

            db.AddInParameter(cmd, "CalenderUnitTypeID", DbType.Int32, academiccalender.CalenderUnitTypeID);
            db.AddInParameter(cmd, "Sequence", DbType.Int32, academiccalender.Sequence);
            db.AddInParameter(cmd, "StartDate", DbType.DateTime, academiccalender.StartDate);
            db.AddInParameter(cmd, "EndDate", DbType.DateTime, academiccalender.EndDate);
            db.AddInParameter(cmd, "Year", DbType.Int32, academiccalender.Year);
            db.AddInParameter(cmd, "Code", DbType.String, academiccalender.Code);
            db.AddInParameter(cmd, "IsCurrent", DbType.Boolean, academiccalender.IsCurrent);
            db.AddInParameter(cmd, "IsNext", DbType.Boolean, academiccalender.IsNext);
            db.AddInParameter(cmd, "AdmissionStartDate", DbType.DateTime, academiccalender.AdmissionStartDate);
            db.AddInParameter(cmd, "AdmissionEndDate", DbType.DateTime, academiccalender.AdmissionEndDate);
            db.AddInParameter(cmd, "IsActiveAdmission", DbType.Boolean, academiccalender.IsActiveAdmission);
            db.AddInParameter(cmd, "RegistrationStartDate", DbType.DateTime, academiccalender.RegistrationStartDate);
            db.AddInParameter(cmd, "RegistrationEndDate", DbType.DateTime, academiccalender.RegistrationEndDate);
            db.AddInParameter(cmd, "IsActiveRegistration", DbType.Boolean, academiccalender.IsActiveRegistration);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, academiccalender.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, academiccalender.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, academiccalender.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, academiccalender.ModifiedDate);

            return db;
        }

        private IRowMapper<AcademicCalender> GetMaper()
        {
            IRowMapper<AcademicCalender> mapper = MapBuilder<AcademicCalender>.MapAllProperties()

            .Map(m => m.AcademicCalenderID).ToColumn("AcademicCalenderID")
            .Map(m => m.CalenderUnitTypeID).ToColumn("CalenderUnitTypeID")
            .Map(m => m.Sequence).ToColumn("Sequence")
            .Map(m => m.StartDate).ToColumn("StartDate")
            .Map(m => m.EndDate).ToColumn("EndDate")
            .Map(m => m.TotalWeek).ToColumn("TotalWeek")
            .Map(m => m.Year).ToColumn("Year")
            .Map(m => m.Code).ToColumn("Code")
            .Map(m => m.IsCurrent).ToColumn("IsCurrent")
            .Map(m => m.IsNext).ToColumn("IsNext")
            .Map(m => m.AdmissionStartDate).ToColumn("AdmissionStartDate")
            .Map(m => m.AdmissionEndDate).ToColumn("AdmissionEndDate")
            .Map(m => m.IsActiveAdmission).ToColumn("IsActiveAdmission")
            .Map(m => m.RegistrationStartDate).ToColumn("RegistrationStartDate")
            .Map(m => m.RegistrationEndDate).ToColumn("RegistrationEndDate")
            .Map(m => m.IsActiveRegistration).ToColumn("IsActiveRegistration")
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


        public List<AcademicCalender> GetAll(int calenderUnitMasterID)
        {
            List<AcademicCalender> academicCalenderList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalender> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalender>("AcademicCalenderGetAllByCalenderUnitMasterID", mapper);
                IEnumerable<AcademicCalender> collection = accessor.Execute(calenderUnitMasterID);

                academicCalenderList = collection.ToList();
            }

            catch (Exception ex)
            {
                return academicCalenderList;
            }

            return academicCalenderList;
        }

        public AcademicCalender GetActiveRegistrationCalenderByProgramId(int programId)
        {
            AcademicCalender _academicCalender = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalender> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalender>("AcademicCalenderGetActiveRegistrationCalenderByProgramId", rowMapper);
                _academicCalender = accessor.Execute(programId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _academicCalender;
            }

            return _academicCalender;
        }

        public List<rAcaCalSessionListByRoll> GetAcaCalSessionListByRoll(string roll)
        {
            List<rAcaCalSessionListByRoll> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rAcaCalSessionListByRoll> mapper = GetAcaCalSessionListByRollMaper();

                var accessor = db.CreateSprocAccessor<rAcaCalSessionListByRoll>("RptAcaCalSessionListByRoll", mapper);
                IEnumerable<rAcaCalSessionListByRoll> collection = accessor.Execute(roll);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rAcaCalSessionListByRoll> GetAcaCalSessionListByRollMaper()
        {
            IRowMapper<rAcaCalSessionListByRoll> mapper = MapBuilder<rAcaCalSessionListByRoll>.MapAllProperties()

             .Map(m => m.TypeName).ToColumn("TypeName")
             .Map(m => m.Year).ToColumn("Year")
             .Map(m => m.AcademicCalenderID).ToColumn("AcademicCalenderID")
             
             .Build();

            return mapper;
        }

        public List<rAcaCalSessionListByProgram> GetAcaCalSessionListCompleted(string roll)
        {
            List<rAcaCalSessionListByProgram> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rAcaCalSessionListByProgram> mapper = GetAcaCalSessionListCompletedMaper();

                var accessor = db.CreateSprocAccessor<rAcaCalSessionListByProgram>("RptAcaCalSessionListCompleted", mapper);
                IEnumerable<rAcaCalSessionListByProgram> collection = accessor.Execute(roll);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rAcaCalSessionListByProgram> GetAcaCalSessionListCompletedMaper()
        {
            IRowMapper<rAcaCalSessionListByProgram> mapper = MapBuilder<rAcaCalSessionListByProgram>.MapAllProperties()

             .Map(m => m.TypeName).ToColumn("TypeName")
             .Map(m => m.Year).ToColumn("Year")
             .Map(m => m.AcademicCalenderID).ToColumn("AcademicCalenderID")

             .Build();

            return mapper;
        }

        public List<rYear> GetAllYear()
        {
            List<rYear> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rYear> mapper = GetYearMaper();

                var accessor = db.CreateSprocAccessor<rYear>("RptAcademicYear", mapper);
                IEnumerable<rYear> collection = accessor.Execute();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rYear> GetYearMaper()
        {
            IRowMapper<rYear> mapper = MapBuilder<rYear>.MapAllProperties()          
            .Map(m => m.Year).ToColumn("Year")

             .Build();

            return mapper;
        }


        public AcademicCalender GetIsCurrentRegistrationByProgramId(int programId)
        {
            AcademicCalender _academicCalender = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalender> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalender>("AcademicCalenderGetActiveRegistrationCalenderByProgramId", rowMapper);
                _academicCalender = accessor.Execute(programId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _academicCalender;
            }

            return _academicCalender;
        }

        public List<Semester> SemesterListByProgramIdBatchId(int programId, int BatchId)
        {
            List<Semester> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Semester> mapper = MapBuilder<Semester>.MapAllProperties()
                .Map(m => m.SemesterNo).ToColumn("SemesterNo")
                .Map(m => m.SemesterName).ToColumn("SemesterName")

               .Build();

                var accessor = db.CreateSprocAccessor<Semester>("SemesterListByProgramBatch", mapper);
                IEnumerable<Semester> collection = accessor.Execute(programId, BatchId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<AcademicCalender> AcaCalSessionByProgramIdBatchId(int programId, int BatchId)
        {
            List<AcademicCalender> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalender> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalender>("AcademicSessionListByProgramBatch", mapper);
                IEnumerable<AcademicCalender> collection = accessor.Execute(programId, BatchId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        #region IAcademicCalenderRepository Members


        public AcademicCalender GetIsActiveRegistrationByProgramId(int programId)
        {
            AcademicCalender _academicCalender = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalender> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalender>("AcademicCalenderGetIsActiveRegistrationByProgramId", rowMapper);
                _academicCalender = accessor.Execute(programId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _academicCalender;
            }

            return _academicCalender;
        }


        public List<AcademicCalender> GetAllByProgram(int programId)
        {
            List<AcademicCalender> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalender> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalender>("AcademicSessionListByProgram", mapper);
                IEnumerable<AcademicCalender> collection = accessor.Execute(programId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }
        

        #endregion
    }
}
