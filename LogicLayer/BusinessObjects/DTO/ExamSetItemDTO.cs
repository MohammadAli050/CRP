using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class ExamSetItemDTO
    {
        public int ItemId { get; set; }
        public string ExamName { get; set; }
        public string ExamSetName { get; set; }
    }
}
