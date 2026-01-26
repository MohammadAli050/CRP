using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rRegistrationInSemesterYear
    {
        public string ProgramShortName { get; set; }
        public int Total { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
       
    }
}
