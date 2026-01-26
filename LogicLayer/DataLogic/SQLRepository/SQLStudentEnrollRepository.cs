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
    public partial class SQLStudentEnrollRepository : IStudentEnrollRepository
    {
        Database db = null;

        private string sqlPersonalInfoGetByAdmissionTestRoll = "CandidatePersonalInfoGetByAdmissionTestRoll";
        private string sqlAddtionalInfoGetByAdmissionTestRoll = "CandidateAddtionalInfoGetByAdmissionTestRoll";
        private string sqlExamInfoByAdmissionTestRoll = "CandidateExamInfoGetByAdmissionTestRoll";
        private string sqlAddressInfoGetByAdmissionTestRoll = "CandidateAddressInfoGetByAdmissionTestRoll";

        public List<CandidatePersonalInfo> GetCandidatePersonalInfoByAdmissionTestRoll(string AdmissionTestRoll)
        {
            List<CandidatePersonalInfo> List = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CandidatePersonalInfo> mapper = GetCandidatePersonalInfoMaper();

                var accessor = db.CreateSprocAccessor<CandidatePersonalInfo>(sqlPersonalInfoGetByAdmissionTestRoll, mapper);
                IEnumerable<CandidatePersonalInfo> collection = accessor.Execute(AdmissionTestRoll);

                List = collection.ToList();
            }

            catch (Exception ex)
            {
                return List;
            }

            return List;
        }

        public List<CandidateAddtionalInfo> GetCandidateAddtionalInfoByAdmissionTestRoll(string AdmissionTestRoll)
        {
            List<CandidateAddtionalInfo> List = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CandidateAddtionalInfo> mapper = GetCandidateAddtionalInfoMaper();

                var accessor = db.CreateSprocAccessor<CandidateAddtionalInfo>(sqlAddtionalInfoGetByAdmissionTestRoll, mapper);
                IEnumerable<CandidateAddtionalInfo> collection = accessor.Execute(AdmissionTestRoll);

                List = collection.ToList();
            }

            catch (Exception ex)
            {
                return List;
            }

            return List;
        }

        public List<CandidateExamInfo> GetCandidateExamInfoByAdmissionTestRoll(string AdmissionTestRoll)
        {
            List<CandidateExamInfo> List = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CandidateExamInfo> mapper = GetCandidateExamInfoMaper();

                var accessor = db.CreateSprocAccessor<CandidateExamInfo>(sqlExamInfoByAdmissionTestRoll, mapper);
                IEnumerable<CandidateExamInfo> collection = accessor.Execute(AdmissionTestRoll);

                List = collection.ToList();
            }

            catch (Exception ex)
            {
                return List;
            }

            return List;
        }

        public List<CandidateAddressInfo> GetCandidateAddressInfoByAdmissionTestRoll(string AdmissionTestRoll)
        {
            List<CandidateAddressInfo> List = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CandidateAddressInfo> mapper = GetCandidateAddressInfoMaper();

                var accessor = db.CreateSprocAccessor<CandidateAddressInfo>(sqlAddressInfoGetByAdmissionTestRoll, mapper);
                IEnumerable<CandidateAddressInfo> collection = accessor.Execute(AdmissionTestRoll);

                List = collection.ToList();
            }

            catch (Exception ex)
            {
                return List;
            }

            return List;
        }

        public bool IsValidRoll(string Roll)
        {
            int id = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("IsStudentRollValid");

                db.AddOutParameter(cmd, "Count", DbType.Int32, Int32.MaxValue);
                db.AddInParameter(cmd, "Roll", DbType.String, Roll); 
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "Count");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out id);
                }
            }
            catch (Exception ex)
            {
                id = 0;
            }

            if (id == 0)
                return true;
            else
                return false;
        }


        #region Mapper

        private IRowMapper<CandidatePersonalInfo> GetCandidatePersonalInfoMaper()
        {
            IRowMapper<CandidatePersonalInfo> mapper = MapBuilder<CandidatePersonalInfo>.MapAllProperties()

            .Map(m => m.CandidateID).ToColumn("CandidateID")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.AcaCalID).ToColumn("AcaCalID")
            .Map(m => m.BatchId).ToColumn("BatchId")
            .Map(m => m.FirstName).ToColumn("FirstName")
            .Map(m => m.ShortName).ToColumn("ShortName")
            .Map(m => m.Session).ToColumn("Session")
            .Map(m => m.Batch).ToColumn("Batch")
            .Map(m => m.Email).ToColumn("Email")
            .Map(m => m.Phone).ToColumn("Phone")
            .Map(m => m.GenderId).ToColumn("Gender")
            .Map(m => m.BloodGroup).ToColumn("GroupName")
            .Map(m => m.MaritalStatusId).ToColumn("MaritalStatus")
            .Map(m => m.BirthDate).ToColumn("BirthDate")

            .Build();

            return mapper;
        }

        private IRowMapper<CandidateAddtionalInfo> GetCandidateAddtionalInfoMaper()
        {
            IRowMapper<CandidateAddtionalInfo> mapper = MapBuilder<CandidateAddtionalInfo>.MapAllProperties()

            .Map(m => m.CandidateID).ToColumn("CandidateID")
            .Map(m => m.FatherName).ToColumn("FatherName")
            .Map(m => m.FatherOccupation).ToColumn("FatherOccupation")
            .Map(m => m.FatherMailingAddress).ToColumn("FatherMailingAddress")
            .Map(m => m.FatherLandPhone).ToColumn("FatherLandPhone")
            .Map(m => m.FatherMobile).ToColumn("FatherMobile")
            .Map(m => m.MotherName).ToColumn("MotherName")
            .Map(m => m.MotherOccupation).ToColumn("MotherOccupation")
            .Map(m => m.MotherMailingAddress).ToColumn("MotherMailingAddress")
            .Map(m => m.MotherLandPhone).ToColumn("MotherLandPhone")
            .Map(m => m.MotherMailingAddress).ToColumn("MotherMailingAddress")
            .Map(m => m.MotherMobile).ToColumn("MotherMobile")
            .Map(m => m.GuardianName).ToColumn("GuardianName")
            .Map(m => m.GuardianRelation).ToColumn("GuardianRelation")
            .Map(m => m.GuardianLandPhone).ToColumn("GuardianLandPhone")
            .Map(m => m.GuardianMobile).ToColumn("GuardianMobile")
            .Map(m => m.GuardianEmail).ToColumn("GuardianEmail")

            .Build();

            return mapper;
        }

        private IRowMapper<CandidateExamInfo> GetCandidateExamInfoMaper()
        {
            IRowMapper<CandidateExamInfo> mapper = MapBuilder<CandidateExamInfo>.MapAllProperties()

            .Map(m => m.CandidateID).ToColumn("CandidateID")
            .Map(m => m.ExamTypeId).ToColumn("ExamTypeId")
            .Map(m => m.TypeName).ToColumn("TypeName")
            .Map(m => m.InstituteName).ToColumn("InstituteName")
            .Map(m => m.GPA).ToColumn("GPA")
            .Map(m => m.GPAW4S).ToColumn("GPAW4S")
            .Map(m => m.PassingYear).ToColumn("PassingYear")
            .Map(m => m.GroupOrSubject).ToColumn("GroupOrSubject")

            .Build();

            return mapper;
        }

        private IRowMapper<CandidateAddressInfo> GetCandidateAddressInfoMaper()
        {
            IRowMapper<CandidateAddressInfo> mapper = MapBuilder<CandidateAddressInfo>.MapAllProperties()

            .Map(m => m.CandidateID).ToColumn("CandidateID")
            .Map(m => m.AddressTypeId).ToColumn("AddressTypeId")
            .Map(m => m.AddressLine).ToColumn("AddressLine")

            .Build();

            return mapper;
        }

        #endregion
    }
}
