using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class FAQ
    {
        public int FaqID {get; set; }
		public string Tag {get; set; }
		public string Key {get; set; }
		public string Question {get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedOn{get; set; }
    }
}

