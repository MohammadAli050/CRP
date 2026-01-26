using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class GradeSheetInfo
    { 
        public string RegistrationNo { get; set; }
        public string SessionName { get; set; }
        public string Roll { get; set; } 
    }
}
