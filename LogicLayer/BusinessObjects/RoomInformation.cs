using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class RoomInformation
    {
        public int RoomInfoID { get; set; }
        public string RoomNumber { get; set; }
        public string RoomName { get; set; }
        public string RoomFloorNo { get; set; }
        public int RoomTypeID { get; set; }
        public int Capacity { get; set; }
        public int ExamCapacity { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int BuildingId { get; set; }
        public int AddressID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        #region Property Not Set

        public string RomeTypeName { get; set; }
        public string CampusName { get; set; }
        public string BuildingName { get; set; }

        #endregion

    }
}
