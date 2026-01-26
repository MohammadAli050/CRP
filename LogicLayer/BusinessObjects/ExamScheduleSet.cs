using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ExamScheduleSet
    {
        public int Id { get; set; }
        public int AcaCalId { get; set; }
        public string SetName { get; set; }
        public int TotalDay { get; set; }
        public int TotalTimeSlot { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
