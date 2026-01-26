using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class ExamResultDTO
    {
        public string StudentName { get; set; }
        public string Roll { get; set; }
        public string ExamName { get; set; }
        public int ColumnSequence { get; set; }
        public string MarksOrGrade { get; set; }
    }
}
