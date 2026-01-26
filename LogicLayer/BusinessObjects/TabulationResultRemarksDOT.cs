using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class TabulationResultRemarksDOT
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int AcaCalId { get; set; }
        public string Roll { get; set; }
        public string Name { get; set; }
        public string CGPA { get; set; }
        public string SessionName { get; set; }
        public string TabulationRemarks { get; set; }
        public string ResultRemarks { get; set; } 
    }
}

