using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ExamMarksAllocation
    {
       public int ExamMarksAllocationID { get; set; }
       public int ExamTypeNameID { get; set; }
       public int AllottedMarks { get; set; }
       public string ExamName { get; set; }
       public int CreatedBy { get; set; }
       public DateTime CreatedDate { get; set; }
       public int ModifiedBy { get; set; }
       public DateTime ModifiedDate { get; set; }
    }
}
