using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class Batch
    {
        public int BatchId { get; set; }
        public int AcaCalId { get; set; }
        public int ProgramId { get; set; }
        public int BatchNO { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Program Program { get { return ProgramManager.GetById(ProgramId); } }
        public AcademicCalender Session { get { return AcademicCalenderManager.GetById(AcaCalId); } }
        public string BatchExtended { get { return BatchNO+ " [" + Attribute1 + "] " ; } }
    }
}

