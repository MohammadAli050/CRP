using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class ExamMarkColumnWiseDTO
    {
        public int StudentCourseHistoryId { get; set; }
        public int StudentId { get; set; }
        public int AcaCalId { get; set; }
        public string Roll { get; set; }
        public int ExamTemplateBasicItemId { get; set; }
        public int ExamMetaTypeId { get; set; }
        public int ColumnSequence { get; set; }
        public string ColumnName { get; set; }
        public decimal GradePoint { get; set; }
        public string Grade { get; set; }
        public decimal Marks { get; set; }
        public decimal ConvertedMark { get; set; }
        public int ExamMarkTypeId { get; set; }
    }
}
