using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public  class ExamMark
    {    
        public int Id { get; set; }
        public int CourseHistoryId { get; set; }
        public int ExamId { get; set; }
        public string Mark { get; set; }
        public int Status { get; set; }
        public bool IsFinalSubmit { get; set; }
        public bool IsTransfer { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

    }
}
