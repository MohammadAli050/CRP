using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ChartReportPreRegistrationMaleFemale
    {
        public string Program { get; set; }
        public int NumberOfMaleStudent { get; set; }
        public int NumberOfFeMaleStudent { get; set; }

    }
}
