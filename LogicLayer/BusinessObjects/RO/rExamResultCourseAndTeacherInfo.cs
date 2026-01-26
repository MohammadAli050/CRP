using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rExamResultCourseAndTeacherInfo
    {
       public string ShortName { get; set; }
       public string DetailedName { get; set; }
       public string Title { get; set; }
       public string FormalCode { get; set; }
       public decimal Credits { get; set; }
       public string FullName { get; set; }
       public string Phone { get; set; }
       public int Year { get; set; }
       public string TypeName { get; set; }
    }
}
