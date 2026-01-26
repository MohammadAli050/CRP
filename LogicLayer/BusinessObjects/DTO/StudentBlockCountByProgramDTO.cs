using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentBlockCountByProgramDTO
    {
        public int StudentCount { get; set; }
        public int ProgramID { get; set; }
        public string ShortName { get; set; }
        public string  DetailName { get; set; }
        public string  Code { get; set; }
        public string ShortNameWithCode { get { return ShortName + " [ " + Code + " ]"; } }
    }
}
