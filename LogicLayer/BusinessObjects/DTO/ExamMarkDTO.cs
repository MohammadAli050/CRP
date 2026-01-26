using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class ExamMarkDTO
    {
        public int Id { get; set; }
        public int CourseHistoryId { get; set; }
        public int ExamId { get; set; }
        public string Mark { get; set; }
        public int Status { get; set; }
        public bool IsFinalSubmit { get; set; }
        public bool IsTransfer { get; set; }
        public string Roll { get; set; }
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public int GradeMasterId { get; set; }
    }
}
