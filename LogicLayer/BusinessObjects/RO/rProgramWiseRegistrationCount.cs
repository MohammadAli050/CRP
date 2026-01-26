using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rProgramWiseRegistrationCount
    {
        public int Total { get; set; }
        public string ProgramCode { get; set; }
        public int BatchNo { get; set; }
        public string ShortName { get; set; }
    }
}
