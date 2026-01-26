using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ChartReportPreRegistration
    {
        public string Program { get; set; }
        public int NumberOfStudent { get; set; }
    }
}
