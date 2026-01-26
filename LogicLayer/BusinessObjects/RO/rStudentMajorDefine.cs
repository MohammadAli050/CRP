using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentMajorDefine
    {
        public int StudentID { get; set; }
        public string Roll { get; set; }
        public string Name { get; set; }
        public double CompletedCr { get; set; }
        public int Major1NodeID { get; set; }
        public int Major2NodeID { get; set; }
        public string Major1Name { get; set; }
        public string Major2Name { get; set; }
    }
}
