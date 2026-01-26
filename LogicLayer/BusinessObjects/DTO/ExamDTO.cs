using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class ExamDTO
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public int Marks { get; set; }
    }
}
