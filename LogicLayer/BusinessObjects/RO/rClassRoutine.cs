using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rClassRoutine
    {
        public string CourseCode { get; set; }
        public string Title { get; set; }
        public decimal Credit { get; set; }
        public string SectionName { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string Room { get; set; }
        public string Faculty { get; set; }
        public string Program { get; set; }
        public string SharedPrograms { get; set; }                                                                                                                                                                                                                                                
    }
}
