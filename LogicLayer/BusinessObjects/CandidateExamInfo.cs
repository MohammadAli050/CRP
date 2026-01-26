using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class CandidateExamInfo
    {
        public int CandidateID { get; set; }
        public string ExamTypeId { get; set; }
        public string TypeName { get; set; }
        public string InstituteName { get; set; }
        public string GPA { get; set; }
        public string GPAW4S { get; set; }
        public string PassingYear { get; set; }
        public string GroupOrSubject { get; set; } 
        
        #region Custom Property 
        
         

        #endregion
    }
}
