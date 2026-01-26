using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ShareProgramInSection
    {
        public int AcademicCalenderSectionId { get; set; }
        public int ProgramId { get; set; }
        public Program Program
        {
            get
            {
                Program Program = ProgramManager.GetById(ProgramId);
                return Program;
            }
        }
    }
}

