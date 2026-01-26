using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rAcaCalSessionListByProgram
    {
        public string TypeName { get; set; }
        public int Year { get; set; }
        public int AcademicCalenderID { get; set; }
    }
}
