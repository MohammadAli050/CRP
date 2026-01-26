using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ExamTypeName
    {
       public int ExamTypeNameID { get; set; }
       public int TypeDefinitionID { get; set; }
       public string Name { get; set; }
       public int TotalAllottedMarks { get; set; }
       public bool Default { get; set; }
       public int CreatedBy { get; set; }
       public DateTime CreatedDate { get; set; }
       public int ModifiedBy { get; set; }
       public DateTime ModifiedDate { get; set; }
    }
}
