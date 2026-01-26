using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ExamTemplateItem
    {
        public int ItemId { get; set; }
        public int TemplateId { get; set; }
        public int ExamSetId { get; set; }
        public int ExamId { get; set; }
        public string CalculativeColumnName { get; set; }
        public int ColumnSequence { get; set; }
        public decimal DivideBy { get; set; }
        public decimal MultiplyBy { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
