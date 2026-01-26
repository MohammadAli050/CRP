using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StatusType
    {
       public int StatusTypeID { get; set; }
       public string TypeDescription { get; set; }
    }
}
