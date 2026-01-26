using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rClassRoutineConflictPair
    {
        public string CP1 { get; set; }
        public string CP2 { get; set; }
        public string CP3 { get; set; }
        public string ConflictPair { get; set; }
    }
}
