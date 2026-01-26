using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class PreviousExam
    {
        public long PreviousExamId { get; set; }
        public Nullable<int> Board { get; set; }
        public string RollNo { get; set; }
        public string InstituteName { get; set; }
        public string GroupOrSubject { get; set; }
        public string Result { get; set; }
        public int PassingYear { get; set; }
        public long CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public int ResultId { get; set; }
        public decimal GPA { get; set; }
        public decimal GPAW4S { get; set; }
    }

}
