using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class CandidatePersonalInfo
    {
        public int FormSL { get; set; }
        public int CandidateID { get; set; }
        public int ProgramID { get; set; } 
        public int AcaCalID { get; set; }
        public int BatchId { get; set; }
        public string FirstName { get; set; }  
        public string ShortName { get; set; }
        public string Session { get; set; }
        public string Batch { get; set; }
        public int GenderId { get; set; }
        public string BloodGroup { get; set; }
        public int MaritalStatusId { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        
        #region Custom Property 
        
        public string MaritalStatus
        {
            get
            {
                string stringValue = Enum.GetName(typeof(CommonUtility.CommonEnum.MaritalStatus), MaritalStatusId);
                return stringValue;
            }
        }

        public string Gender
        {
            get
            {
                string stringValue = Enum.GetName(typeof(CommonUtility.CommonEnum.Gender), GenderId);
                return stringValue;
            }
        }

        #endregion
    }
}
