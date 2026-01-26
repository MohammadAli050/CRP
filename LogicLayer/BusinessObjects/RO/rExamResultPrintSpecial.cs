using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rExamResultPrintSpecial
    {
        public string Name { get; set; }
        public string Roll { get; set; }
        public string Marks1 { get; set; }
        public string Marks2 { get; set; }
        public string Marks3 { get; set; }
        public string Marks4 { get; set; }
        public string Marks5 { get; set; }
        public string Marks6 { get; set; }
        public string Marks7 { get; set; }
        public string Total { get; set; }
        public string Grade { get; set; }
        public string Point { get; set; }
    }
}
