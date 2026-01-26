using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentCourse
    {
        public string Roll { get; set; }
        public string Name { get; set; }
        public int ProgramID { get; set; }
        public int BatchId { get; set; }
        public string FormalCode { get; set; }
        public string Title { get; set; }
    }
}
