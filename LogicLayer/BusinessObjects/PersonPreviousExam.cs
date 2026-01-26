using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class PersonPreviousExam
    {
        public long PersonPreviousExamId { get; set; }
        public long PersonId { get; set; }
        public long PreviousExamId { get; set; }
        public long PreviousExamTypeId { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
