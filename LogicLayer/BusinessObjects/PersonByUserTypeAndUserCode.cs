using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class PersonByUserTypeAndUserCode
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
