using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable] 
    public class AcademicCalenderSectionWithCourse
    {
        public int AcaCal_SectionID { get; set; }
        public string Title { get; set; }
        public string SectionName { get; set; }
        public int TotalStudent { get; set; }
        public decimal TotalParticipant { get; set; }

        public string ValueField { get { return AcaCal_SectionID + " - " + TotalStudent + " - " + TotalParticipant; } }
        public string CourseWithSection { get { return SectionName + " - " + Title; } }

    }
}
