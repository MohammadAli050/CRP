using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentDiscountAndScholarshipPerSession
    {
        public int StudentDiscountAndScholarshipId { get; set; }
        public int StudentId { get; set; }
        public int TypeDefinitionId { get; set; }
        public decimal Discount { get; set; }
        public int AcaCalSession { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string StudentName
        {
            get
            {
                return StudentManager.GetById(StudentId).BasicInfo.FullName;
            }
        }
        public string StudentRoll
        {
            get
            {
                return StudentManager.GetById(StudentId).Roll;
            }
        }
        public string TypeDefinition
        {
            get
            {
                return TypeDefinitionManager.GetById(TypeDefinitionId).Definition;
            }
        }

        public Student Student { get { return StudentManager.GetById(StudentId); } }

    }
}

