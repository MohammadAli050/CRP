using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
    public class AdmissionResult_BAO
    {
        public static List<FormGridEntity> GetAllCandidateAdmissionInfo()
        {
            return FormGrid_DAO.GetAllInfoForAdmissionResultGrid();
        }
        public static List<FormGridEntity> GetAllCandidateAdmissionInfoByProgram(string programShotName)
        {
            return FormGrid_DAO.GetAllCandidatesByProgram(programShotName);
        }
        public static List<EmsCandidateEntity> GetCandidatesByTestRolls(List<string> testRolls)
        {
            return EmsCandidate_DAO.GetCandidatesByAdmissionTestRolls(testRolls);
        }

        public static int UpdateCandidates(List<EmsCandidateEntity> candidateEntities){
            return EmsCandidate_DAO.save(candidateEntities);
        }
    }
}
