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
    public partial class SQLAcademicCalenderScheduleRepository : IAcademicCalenderScheduleRepository
    {

        Database db = null;

        private string sqlInsert = "AcademicCalenderScheduleInsert";
        private string sqlUpdate = "AcademicCalenderScheduleUpdate";
        private string sqlDelete = "AcademicCalenderScheduleDelete";
        private string sqlGetById = "AcademicCalenderScheduleGetById";
        private string sqlGetAll = "AcademicCalenderScheduleGetAll";

        public int Insert(AcademicCalenderSchedule acacalshedule)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, acacalshedule, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "AcademicCalenderScheduleId");

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

        public bool Update(AcademicCalenderSchedule acacalshedule)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, acacalshedule, isInsert);

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

                db.AddInParameter(cmd, "AcademicCalenderScheduleId", DbType.Int32, id);
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

        public AcademicCalenderSchedule GetById(int? id)
        {
            AcademicCalenderSchedule _academiccalenderschedule = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalenderSchedule> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalenderSchedule>(sqlGetById, rowMapper);
                _academiccalenderschedule = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _academiccalenderschedule;
            }

            return _academiccalenderschedule;
        }

        public List<AcademicCalenderSchedule> GetAll()
        {
            List<AcademicCalenderSchedule> academiccalenderscheduleList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalenderSchedule> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalenderSchedule>(sqlGetAll, mapper);
                IEnumerable<AcademicCalenderSchedule> collection = accessor.Execute();

                academiccalenderscheduleList = collection.ToList();
            }

            catch (Exception ex)
            {
                return academiccalenderscheduleList;
            }

            return academiccalenderscheduleList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, AcademicCalenderSchedule academiccalenderschedule, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "AcademicCalenderScheduleId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "AcademicCalenderScheduleId", DbType.Int32, academiccalenderschedule.AcademicCalenderScheduleId);
            }

            	
		db.AddInParameter(cmd,"AcademicCalenderID",DbType.Int32,academiccalenderschedule.AcademicCalenderID);
		db.AddInParameter(cmd,"ProgramId",DbType.Int32,academiccalenderschedule.ProgramId);
		db.AddInParameter(cmd,"ClassStartDate",DbType.DateTime,academiccalenderschedule.ClassStartDate);
		db.AddInParameter(cmd,"MarkSheetSubmissionLastDate",DbType.DateTime,academiccalenderschedule.MarkSheetSubmissionLastDate);
		db.AddInParameter(cmd,"AnswerScriptSubmissionLastDate",DbType.DateTime,academiccalenderschedule.AnswerScriptSubmissionLastDate);
		db.AddInParameter(cmd,"ResultPublicationDate",DbType.DateTime,academiccalenderschedule.ResultPublicationDate);
		db.AddInParameter(cmd,"RegistrationPamentDateWithoutFine",DbType.DateTime,academiccalenderschedule.RegistrationPamentDateWithoutFine);
		db.AddInParameter(cmd,"OrientationDate",DbType.DateTime,academiccalenderschedule.OrientationDate);
		db.AddInParameter(cmd,"RegistrationPamentDateWithFine",DbType.DateTime,academiccalenderschedule.RegistrationPamentDateWithFine);
		db.AddInParameter(cmd,"MidExamStartDate",DbType.DateTime,academiccalenderschedule.MidExamStartDate);
		db.AddInParameter(cmd,"MidExamEndDate",DbType.DateTime,academiccalenderschedule.MidExamEndDate);
		db.AddInParameter(cmd,"AdvisingStartDate",DbType.DateTime,academiccalenderschedule.AdvisingStartDate);
		db.AddInParameter(cmd,"AdvisingEnd",DbType.DateTime,academiccalenderschedule.AdvisingEnd);
		db.AddInParameter(cmd,"RegistrationStartDate",DbType.DateTime,academiccalenderschedule.RegistrationStartDate);
		db.AddInParameter(cmd,"RegistrationEndDate",DbType.DateTime,academiccalenderschedule.RegistrationEndDate);
		db.AddInParameter(cmd,"ClassEndDate",DbType.DateTime,academiccalenderschedule.ClassEndDate);
		db.AddInParameter(cmd,"FinalExamDate",DbType.DateTime,academiccalenderschedule.FinalExamDate);
		db.AddInParameter(cmd,"FinalEndDate",DbType.DateTime,academiccalenderschedule.FinalEndDate);
		db.AddInParameter(cmd,"SessionVacationStartDate",DbType.DateTime,academiccalenderschedule.SessionVacationStartDate);
		db.AddInParameter(cmd,"SessionVacationEndDate",DbType.DateTime,academiccalenderschedule.SessionVacationEndDate);
		db.AddInParameter(cmd,"AdmissionStartDate",DbType.DateTime,academiccalenderschedule.AdmissionStartDate);
		db.AddInParameter(cmd,"AdmissionEndDate",DbType.DateTime,academiccalenderschedule.AdmissionEndDate);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,academiccalenderschedule.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,academiccalenderschedule.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,academiccalenderschedule.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,academiccalenderschedule.ModifiedDate);
            
            return db;
        }

        private IRowMapper<AcademicCalenderSchedule> GetMaper()
        {
            IRowMapper<AcademicCalenderSchedule> mapper = MapBuilder<AcademicCalenderSchedule>.MapAllProperties()

       	   .Map(m => m.AcademicCalenderScheduleId).ToColumn("AcademicCalenderScheduleId")
		.Map(m => m.AcademicCalenderID).ToColumn("AcademicCalenderID")
		.Map(m => m.ProgramId).ToColumn("ProgramId")
		.Map(m => m.ClassStartDate).ToColumn("ClassStartDate")
		.Map(m => m.MarkSheetSubmissionLastDate).ToColumn("MarkSheetSubmissionLastDate")
		.Map(m => m.AnswerScriptSubmissionLastDate).ToColumn("AnswerScriptSubmissionLastDate")
		.Map(m => m.ResultPublicationDate).ToColumn("ResultPublicationDate")
		.Map(m => m.RegistrationPamentDateWithoutFine).ToColumn("RegistrationPamentDateWithoutFine")
		.Map(m => m.OrientationDate).ToColumn("OrientationDate")
		.Map(m => m.RegistrationPamentDateWithFine).ToColumn("RegistrationPamentDateWithFine")
		.Map(m => m.MidExamStartDate).ToColumn("MidExamStartDate")
		.Map(m => m.MidExamEndDate).ToColumn("MidExamEndDate")
		.Map(m => m.AdvisingStartDate).ToColumn("AdvisingStartDate")
		.Map(m => m.AdvisingEnd).ToColumn("AdvisingEnd")
		.Map(m => m.RegistrationStartDate).ToColumn("RegistrationStartDate")
		.Map(m => m.RegistrationEndDate).ToColumn("RegistrationEndDate")
		.Map(m => m.ClassEndDate).ToColumn("ClassEndDate")
		.Map(m => m.FinalExamDate).ToColumn("FinalExamDate")
		.Map(m => m.FinalEndDate).ToColumn("FinalEndDate")
		.Map(m => m.SessionVacationStartDate).ToColumn("SessionVacationStartDate")
		.Map(m => m.SessionVacationEndDate).ToColumn("SessionVacationEndDate")
		.Map(m => m.AdmissionStartDate).ToColumn("AdmissionStartDate")
		.Map(m => m.AdmissionEndDate).ToColumn("AdmissionEndDate")
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

