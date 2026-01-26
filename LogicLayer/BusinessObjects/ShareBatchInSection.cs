using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ShareBatchInSection
    {
        public int AcademicCalenderSectionId {get; set; }
		public int BatchId {get; set; }
    }
}

