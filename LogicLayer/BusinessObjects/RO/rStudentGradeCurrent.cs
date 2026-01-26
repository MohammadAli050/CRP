using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentGradeCurrent
    {
        public decimal ObtainedGPA { get; set; }
        public decimal CAttemped { get; set; }
        public string FormalCode { get; set; }
        public decimal Credits { get; set; }
        public string Title { get; set; }
        public string Roll { get; set; }
        public string Major { get; set; }
        public string ObtainedGrade { get; set; }
        public string FatherName { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public string DetailName { get; set; }
        public string DepartmentName { get; set; }
        public int Year { get; set; }
        public string TypeName { get; set; }
        public decimal CEarned { get; set; }
        public decimal CAttemptedTotal { get; set; }
    }
}
