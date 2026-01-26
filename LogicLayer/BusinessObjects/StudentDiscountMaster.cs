using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentDiscountMaster
    {
        public int StudentDiscountId { get; set; }
        public int StudentId { get; set; }
        public int BatchId { get; set; }
        public int ProgramId { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public List<StudentDiscountInitialDetails> StudentDiscountsInitial
        {
            get 
            {
                return StudentDiscountInitialDetailsManager.GetByStudentDiscountId(StudentDiscountId);
            }
        }

        public List<StudentDiscountCurrentDetails> StudentDiscountsCurrent 
        {
            get 
            {
                return StudentDiscountCurrentDetailsManager.GetByStudentDiscountId(StudentDiscountId);               
            } 
        }
    }
}

