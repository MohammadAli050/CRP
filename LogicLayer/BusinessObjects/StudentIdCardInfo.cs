using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentIdCardInfo
    {
        public string Roll { get; set; }
        public string FullName { get; set; }
        public string DegreeName { get; set; }
        public string ShortName { get; set; }
        public string BloodGroup { get; set; }
        public string PhotoPath { get; set; }
        public string SignaturePath { get; set; } 
        
        #region Custom Property  

        #endregion
    }
}
