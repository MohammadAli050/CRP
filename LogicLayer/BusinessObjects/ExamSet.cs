using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ExamSet
    {
        public int SetId { get; set; }
        public string ExamSetName { get; set; }
        public int Mark { get; set; }
        public int CriteriaId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedTime { get; set; }
    }
}
