using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ScholarshipList
    {
        public int Id { get; set; }
        public int AcaCalId { get; set; }
        public int StudentId { get; set; }
        public string Roll { get; set; }
        public string Name { get; set; }
        public decimal GPA { get; set; }
        public decimal Credit { get; set; }
        public decimal PassCredit { get; set; }
        public decimal RegisterCredit { get; set; }
        public string CalculateScholarship { get; set; }
        public string ManualScholarship { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
