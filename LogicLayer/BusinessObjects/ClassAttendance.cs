using LogicLayer.BusinessLogic;
//using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ClassAttendance
    {
        public int ID { get; set; }
        public int StudentId { get; set; }
        public string Roll { get; set; }
        public string Name { get; set; }
        public int AcaCalSectionID { get; set; }
        public Nullable<DateTime> AttendanceDate { get; set; }
        public int StatusID { get; set; }
        public string Comment { get; set; }
		public int CreatedBy {get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        
    }
}

