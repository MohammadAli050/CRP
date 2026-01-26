using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentDiscountDetails
    {
        public int StudentId { get; set; }
        public int BatchId { get; set; }
        public int ProgramId { get; set; }
        public int TypeDefinitionId { get; set; }
        public decimal Discount { get; set; }
        public string TypeDefinition { get; set; }

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

    }
}
