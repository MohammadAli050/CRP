using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rDegreeCompletedStudent
    {
        public int AcacalID { get; set; }
        public int ProgramID { get; set; }
        public int TCount { get; set; }

        public string ProgramCode
        {
            get 
            {
                Program p = ProgramManager.GetById(ProgramID);
                return p.Code;
            }
        }
        public string Semester
        {
            get
            {
                AcademicCalender ac = AcademicCalenderManager.GetById(AcacalID);
                return ac.FullCode;
            }
        }                                                                                                                                                                                             
    }
}
