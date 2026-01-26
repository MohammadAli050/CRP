using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable] 
    public class StudentCourseHistoryDTO
    {
        public int StudentCourseHistoryId { get; set; }
        public int StudentId { get; set; }
        public string StudentRoll { get; set; }
        public string StudentName { get; set; }
    }
}
