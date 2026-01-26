using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class RoleMenu
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public int MenuID { get; set; }
        public Nullable<DateTime> AccessMenuStartDate { get; set; }
        public Nullable<DateTime> AccessMenuEndDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        #region Custom Property
        public string MenuName { get; set; }
        #endregion
    }
}
