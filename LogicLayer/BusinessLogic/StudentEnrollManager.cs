using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class StudentEnrollManager
    {
        public static List<CandidatePersonalInfo> GetCandidatePersonalInfoByAdmissionTestRoll(string AdmissionTestRoll)
        {
            List<CandidatePersonalInfo> list = RepositoryManager.StudentEnroll_Repository.GetCandidatePersonalInfoByAdmissionTestRoll(AdmissionTestRoll);

            return list;
        }

        public static List<CandidateAddtionalInfo> GetCandidateAddtionalInfoByAdmissionTestRoll(string AdmissionTestRoll)
        {
            List<CandidateAddtionalInfo> list = RepositoryManager.StudentEnroll_Repository.GetCandidateAddtionalInfoByAdmissionTestRoll(AdmissionTestRoll);

            return list;
        }

        public static List<CandidateExamInfo> GetCandidateExamInfoByAdmissionTestRoll(string AdmissionTestRoll)
        {
            List<CandidateExamInfo> list = RepositoryManager.StudentEnroll_Repository.GetCandidateExamInfoByAdmissionTestRoll(AdmissionTestRoll);

            return list;
        }

        public static List<CandidateAddressInfo> GetCandidateAddressInfoByAdmissionTestRoll(string AdmissionTestRoll)
        {
            List<CandidateAddressInfo> list = RepositoryManager.StudentEnroll_Repository.GetCandidateAddressInfoByAdmissionTestRoll(AdmissionTestRoll);

            return list;
        }

        public static bool IsValidRoll(string Roll)
        {

            return RepositoryManager.StudentEnroll_Repository.IsValidRoll(Roll);

        }

    }
}
