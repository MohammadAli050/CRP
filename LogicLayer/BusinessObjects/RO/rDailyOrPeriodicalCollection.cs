using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rDailyOrPeriodicalCollection
    {
        public int SL { get; set; }
        public int Program { get; set; }
        public int Batch { get; set; }
        public int Semester { get; set; }
        public string Gender { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public decimal Cash { get; set; }
        public decimal Bank { get; set; }
        public decimal Total { get; set; }
        
    }
}
