using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class RoomList
    {
        public int id { get; set; }
        public int RoomNo { get; set; }
        public string RoomName { get; set; }
    }
}
