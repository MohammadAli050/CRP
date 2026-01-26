using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class CandidateAddtionalInfo
    {
        public int CandidateID { get; set; }
        public string FatherName { get; set; }
        public string FatherOccupation { get; set; }
        public string FatherMailingAddress { get; set; }
        public string FatherLandPhone { get; set; }
        public string FatherMobile { get; set; }
        public string MotherName { get; set; }
        public string MotherOccupation { get; set; }
        public string MotherMailingAddress { get; set; }
        public string MotherLandPhone { get; set; }
        public string MotherMobile { get; set; }
        public string GuardianName { get; set; }
        public string GuardianRelation { get; set; }
        public string GuardianLandPhone { get; set; }
        public string GuardianMobile { get; set; }
        public string GuardianEmail { get; set; }
        
        #region Custom Property 
        
         

        #endregion
    }
}
