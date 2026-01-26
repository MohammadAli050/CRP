using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class AddressByRoll
    {
        public string Roll { get; set; }
        public int AddressTypeId { get; set; }
        public string AddressLine { get; set; }
    }
}
