using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class CandidateAddressInfo
    {
        public int CandidateID { get; set; }  
        public int AddressTypeId { get; set; }
        public string AddressLine { get; set; }
        
        #region Custom Property  

        #endregion
    }
}
