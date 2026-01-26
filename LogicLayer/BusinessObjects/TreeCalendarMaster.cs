using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class TreeCalendarMaster
    {
        public int TreeCalendarMasterID { get; set; }
        public int TreeMasterID { get; set; }
        public int CalendarMasterID { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        #region Custom Property
        public string CalenderUnitName
        {
            get
            {
                string calenderName = CalenderUnitMasterManager.GetById(Convert.ToInt32(CalendarMasterID)).Name;
                return calenderName;
            }
        }
        #endregion
    }
}
