using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
   public class Candidate_BAO
    {

        public static List<CandidateEntity> GetCandidateBy(int acaCalID, int programID)
        {
            return Candidate_DAO.GetCandidateBy(acaCalID, programID);
        }
    }
}
