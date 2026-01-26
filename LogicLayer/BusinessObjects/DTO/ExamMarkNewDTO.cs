using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    public class ExamMarkNewDTO
    {
        public int ExamMarkDetailId { get; set; }
        public int CourseHistoryId { get; set; }
        public string StudentName { get; set; }
        public string StudentRoll { get; set; }
        public Nullable<decimal> Mark { get; set; }
        public int ExamMarkTypeId { get; set; }
        public string ExamTemplateBasicItemName { get; set; }
        public Nullable<bool> IsFinalSubmit { get; set; }
        public int ColumnSequence { get; set; }
    }
}
