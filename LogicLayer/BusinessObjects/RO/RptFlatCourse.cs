using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LogicLayer.BusinessObjects.RO
{
    [Serializable]
    public class RptFlatCourse
    {
        public Nullable <int> ProgramID { get; set; }
        public string ShortName { get; set; }
        public string FormalCode { get; set; }
        public string Title { get; set; }
    }
}
