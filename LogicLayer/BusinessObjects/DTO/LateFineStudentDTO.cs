using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class LateFineStudentDTO
    {
        public int StudentID { get; set; }
        public string Roll { get; set; }
        public string Name { get; set; }
        public decimal Dues { get; set; }
    }
}
