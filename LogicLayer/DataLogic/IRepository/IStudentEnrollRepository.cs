using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentEnrollRepository
    {
        List<CandidatePersonalInfo> GetCandidatePersonalInfoByAdmissionTestRoll(string AdmissionTestRoll);
        List<CandidateAddtionalInfo> GetCandidateAddtionalInfoByAdmissionTestRoll(string AdmissionTestRoll);
        List<CandidateExamInfo> GetCandidateExamInfoByAdmissionTestRoll(string AdmissionTestRoll);
        List<CandidateAddressInfo> GetCandidateAddressInfoByAdmissionTestRoll(string AdmissionTestRoll);
        bool IsValidRoll(string Roll);
    }
}
