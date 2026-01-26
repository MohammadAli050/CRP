using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class ExamTemplateBasicCalculativeItemDTO
    {
        public int ExamTemplateBasicItemId { get; set; }
        public int ColumnSequence { get; set; }
        public int ExamTemplateMasterId { get; set; }
        public decimal ExamTemplateBasicItemMark { get; set; }
        public string ExamTemplateBasicItemName { get; set; }
        public int ExamMetaTypeId { get; set; }
        public int ExamTemplateMasterTypeId { get; set; }
        public int CalculationType { get; set; }
    }
}
