using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class StudentList
    {
        public int CourseCode { get; set; }
        public int CourseId { get; set; }
        public int VersionId { get; set; }
        public string Roll { get; set; }
        public decimal Marks { get; set; }
    }
}
