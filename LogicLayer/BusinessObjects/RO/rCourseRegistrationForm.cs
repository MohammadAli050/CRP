using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rCourseRegistrationForm
    {
        public string Roll { get; set; }
        public int ProgramID { get; set; }
        public int BatchId { get; set; }
        public int AcaCalID { get; set; }
        public string DetailedName { get; set; }
        public string ProgramFullName { get; set; }
        public int Year { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public string FormalCode { get; set; }
        public string Title { get; set; }
        public decimal Credits { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal Amount { get; set; }
        public string FullName { get; set; }
    }
}
