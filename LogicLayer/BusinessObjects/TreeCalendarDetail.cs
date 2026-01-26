using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class TreeCalendarDetail
    {
        public int TreeCalendarDetailID { get; set; }
        public int TreeCalendarMasterID { get; set; }
        public int TreeMasterID { get; set; }
        public int CalendarMasterID { get; set; }
        public int CalendarDetailID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        #region do not map
        public string CalenderUnitDistributionName { get; set; }
        #endregion
    }
}
