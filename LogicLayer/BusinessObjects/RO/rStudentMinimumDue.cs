using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.RO
{
    [Serializable]
    public class rStudentMinimumDue
    {
        public int StudentID { get; set; }
        public string Roll { get; set; } 
        public string FullName { get; set; } 
        public decimal Fee { get; set; } 
        public decimal Recived { get; set; }
        public decimal Dues { get; set; }
    }
}
