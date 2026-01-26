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
    partial class SQLRegistrationDateTimeLimitRepository : IRegistrationDateTimeLimitRepository
    {
        Database db = null;

        private string sqlInsert = "RegistrationDateTimeLimitInsert";
        private string sqlUpdate = "RegistrationDateTimeLimitUpdate";
        private string sqlDelete = "RegistrationDateTimeLimitDeleteById";
        private string sqlGetById = "RegistrationDateTimeLimitGetById";
        private string sqlGetAll = "RegistrationDateTimeLimitGetAll";

        public int Insert(RegistrationDateTimeLimit registrationDateTimeLimit)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, registrationDateTimeLimit, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "RegistrationDateTimeLimitID");

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

        public bool Update(RegistrationDateTimeLimit registrationDateTimeLimit)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, registrationDateTimeLimit, isInsert);

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

                db.AddInParameter(cmd, "RegistrationDateTimeLimitID", DbType.Int32, id);
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

        public RegistrationDateTimeLimit GetById(int? id)
        {
            RegistrationDateTimeLimit _registrationDateTimeLimit = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RegistrationDateTimeLimit> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationDateTimeLimit>(sqlGetById, rowMapper);
                _registrationDateTimeLimit = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _registrationDateTimeLimit;
            }

            return _registrationDateTimeLimit;
        }

        public List<RegistrationDateTimeLimit> GetAll()
        {
            List<RegistrationDateTimeLimit> registrationDateTimeLimitList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RegistrationDateTimeLimit> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationDateTimeLimit>(sqlGetAll, mapper);
                IEnumerable<RegistrationDateTimeLimit> collection = accessor.Execute();

                registrationDateTimeLimitList = collection.ToList();
            }

            catch (Exception ex)
            {
                return registrationDateTimeLimitList;
            }

            return registrationDateTimeLimitList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, RegistrationDateTimeLimit registrationDateTimeLimit, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "RegistrationDateTimeLimitID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "RegistrationDateTimeLimitID", DbType.Int32, registrationDateTimeLimit.RegistrationDateTimeLimitID);
            }


            db.AddInParameter(cmd, "DepartmentID", DbType.Int32, registrationDateTimeLimit.DepartmentID);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, registrationDateTimeLimit.ProgramID);
            db.AddInParameter(cmd, "AcaCalID", DbType.Int32, registrationDateTimeLimit.AcaCalID);
            db.AddInParameter(cmd, "PreAdvisingStartDT", DbType.DateTime, registrationDateTimeLimit.PreAdvisingStartDT);
            db.AddInParameter(cmd, "PreAdvisingEndDT", DbType.DateTime, registrationDateTimeLimit.PreAdvisingEndDT);
            db.AddInParameter(cmd, "SectionSeclationStartDT", DbType.DateTime, registrationDateTimeLimit.SectionSeclationStartDT);
            db.AddInParameter(cmd, "SectionSeclationEndDT", DbType.DateTime, registrationDateTimeLimit.SectionSeclationEndDT);
            db.AddInParameter(cmd, "RegistrationStartDT", DbType.DateTime, registrationDateTimeLimit.RegistrationStartDT);
            db.AddInParameter(cmd, "RegistrationEndDT", DbType.DateTime, registrationDateTimeLimit.RegistrationEndDT);
            db.AddInParameter(cmd, "Step1StartDT", DbType.DateTime, registrationDateTimeLimit.Step1StartDT);
            db.AddInParameter(cmd, "Step1EndDT", DbType.DateTime, registrationDateTimeLimit.Step1EndDT);
            db.AddInParameter(cmd, "Step2StartDT", DbType.DateTime, registrationDateTimeLimit.Step2StartDT);
            db.AddInParameter(cmd, "Step2EndDT", DbType.DateTime, registrationDateTimeLimit.Step2EndDT);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, registrationDateTimeLimit.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, registrationDateTimeLimit.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, registrationDateTimeLimit.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, registrationDateTimeLimit.ModifiedDate);

            return db;
        }

        private IRowMapper<RegistrationDateTimeLimit> GetMaper()
        {
            IRowMapper<RegistrationDateTimeLimit> mapper = MapBuilder<RegistrationDateTimeLimit>.MapAllProperties()
            .Map(m => m.RegistrationDateTimeLimitID).ToColumn("RegistrationDateTimeLimitID")
            .Map(m => m.DepartmentID).ToColumn("DepartmentID")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.AcaCalID).ToColumn("AcaCalID")
            .Map(m => m.PreAdvisingStartDT).ToColumn("PreAdvisingStartDT")
            .Map(m => m.PreAdvisingEndDT).ToColumn("PreAdvisingEndDT")
            .Map(m => m.SectionSeclationStartDT).ToColumn("SectionSeclationStartDT")
            .Map(m => m.SectionSeclationEndDT).ToColumn("SectionSeclationEndDT")
            .Map(m => m.RegistrationStartDT).ToColumn("RegistrationStartDT")
            .Map(m => m.RegistrationEndDT).ToColumn("RegistrationEndDT")
            .Map(m => m.Step1StartDT).ToColumn("Step1StartDT")
            .Map(m => m.Step1EndDT).ToColumn("Step1EndDT")
            .Map(m => m.Step2StartDT).ToColumn("Step2StartDT")
            .Map(m => m.Step2EndDT).ToColumn("Step2EndDT")
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
