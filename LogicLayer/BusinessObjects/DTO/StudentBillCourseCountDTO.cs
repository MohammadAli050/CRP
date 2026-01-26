using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    public class StudentBillCourseCountDTO
    {
        public int StudentId { get; set; }
        public string Roll { get; set; }
        public string StudentName { get; set; }
        public decimal? TrimesterBill { get; set; }
        public int TheoryCourseCount { get; set; }
        public int LabCourseCount { get; set; }
        public int ProjectCount { get; set; }
    }
}
