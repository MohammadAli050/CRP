using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class Exam
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public int Marks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreationTime { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModificationTime { get; set; }
    }
}
