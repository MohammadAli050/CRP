using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class RelationBetweenDiscountCourseType
    {
        public int RelationBetweenDiscountCourseTypeID { get; set; }
        public int AcaCalID { get; set; }
        public int ProgramID { get; set; }
        public int TypeDefDiscountID { get; set; }
        public int TypeDefCourseID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
