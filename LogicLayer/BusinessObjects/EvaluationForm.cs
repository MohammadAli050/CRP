using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class EvaluationForm
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int AcaCalSecId { get; set; }
        public int AcaCalId { get; set; }
        public string ExpectedGrade { get; set; }
        public int QSet { get; set; }
        public int Q1 { get; set; }
        public int Q2 { get; set; }
        public int Q3 { get; set; }
        public int Q4 { get; set; }
        public int Q5 { get; set; }
        public int Q6 { get; set; }
        public int Q7 { get; set; }
        public int Q8 { get; set; }
        public int Q9 { get; set; }
        public int Q10 { get; set; }
        public int Q11 { get; set; }
        public Nullable<int> Q12 { get; set; }
        public Nullable<int> Q13 { get; set; }
        public Nullable<int> Q14 { get; set; }
        public Nullable<int> Q15 { get; set; }
        public Nullable<int> Q16 { get; set; }
        public Nullable<int> Q17 { get; set; }
        public Nullable<int> Q18 { get; set; }
        public Nullable<int> Q19 { get; set; }
        public Nullable<int> Q20 { get; set; }
        public string Comment { get; set; }
        public bool IsFinalSubmit { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        public AcademicCalenderSection AcaCalSec
        {
            get
            {
                AcademicCalenderSection acaCalSec = null;
                acaCalSec = AcademicCalenderSectionManager.GetById(AcaCalSecId);
                return acaCalSec;
            }
        }
    }
}
