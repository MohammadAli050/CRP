using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class LateFineSchedule
    {
        public int LateFineScheduleId { get; set; }
        public int AcademicCalenderId { get; set; }
        public int ProgramId { get; set; }
        public string ScheduleName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public AcademicCalender Session { get { return AcademicCalenderManager.GetById(AcademicCalenderId); } }
        public Program Program { get { return ProgramManager.GetById(ProgramId); } }
    }
}

