using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class CertificatesDTO
    {
        public int StudentID { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string ProgramName { get; set; }
        public string DegreeName { get; set; }
        public int Duration { get; set; }
        public decimal CGPA { get; set; }
        public string TypeName { get; set; }
        public int Year { get; set; }
    }
}
