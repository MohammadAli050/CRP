using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class CourseHistryForDegreeValidation
    {
        public int AcaCalId { get; set; }
        public string AcaCalCode { get; set; }
        public int Priority { get; set; }
        public int CourseID { get; set; }
        public int VersionID { get; set; }
        public string FormalCode { get; set; }
        public string Title { get; set; }
        public decimal Credits { get; set; }
        public string ObtainedGrade { get; set; }
        public string CalendarDetailName { get; set; }
        public string NodeLinkName { get; set; }
        public string CourseType { get; set; }
        public string CourseGroup { get; set; }
        public int HasMultipleACUSpan { get; set; }
    }
}
