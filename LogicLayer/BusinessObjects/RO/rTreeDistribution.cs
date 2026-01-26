using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rTreeDistribution
    {
        public int ProgramID { get; set; }
        public int TreeMasterID { get; set; }
        public decimal PassingGPA { get; set; }
        public string SessionName { get; set; }
        public string AcaCalId { get; set; }
        public int Sequence { get; set; }
        public string Name { get; set; }
        public int OfferedByProgramID { get; set; }
        public decimal Credits {get; set;}
        public string FormalCode { get; set; }
        public string Title { get; set; }
        public string DetailName { get; set; }
        public decimal RequiredUnits { get; set; }

    }
}
