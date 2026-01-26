using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class LateRegistrationFineStudent
    {
        public int StudentID { get; set; }
        public int BillHistoryId { get; set; }
        public string Roll { get; set; }
        public string Name { get; set; }
        public decimal CalculativeFine { get; set; }
        public decimal PostedFine { get; set; }
        public decimal MisMatch { get; set; }
        public DateTime LastRegisterdDate { get; set; }
    }
}
