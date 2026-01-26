using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class ExamTemplateItemDTO
    {
        public int ItemId { get; set; }
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public int TemplateMarks { get; set; }
        public int ColumnSequence { get; set; }
        public string CalculativeColumnName { get; set; }
        public int SetId { get; set; }
        public string ExamSetName { get; set; }
        public int ExamId { get; set; }
        public string ExamName { get; set; }
    }
}
