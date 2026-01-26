using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StdCrsBillWorksheet
    {
        public int BillWorkSheetId { get; set; }
        public int StudentId { get; set; }
        public int CalCourseProgNodeID { get; set; }
        public int AcaCalSectionID { get; set; }
        public int SectionTypeId { get; set; }
        public int AcaCalId { get; set; }
        public int CourseId { get; set; }
        public int VersionId { get; set; }
        public int CourseTypeId { get; set; }
        public int ProgramId { get; set; }
        public int RetakeNo { get; set; }
        public string PreviousBestGrade { get; set; }
        public int FeeSetupId { get; set; }
        public decimal Fee { get; set; }
        public int DiscountTypeId { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
