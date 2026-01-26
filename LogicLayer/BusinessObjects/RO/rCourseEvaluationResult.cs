using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable] 
    public class rCourseEvaluationResult
    {
        public string Question { get; set; }
        public decimal AnsCountQ1 { get; set; }
        public decimal AnsCountQ2 { get; set; }
        public decimal AnsCountQ3 { get; set; }
        public decimal AnsCountQ4 { get; set; }
        public decimal AnsCountQ5 { get; set; }

    }
}
