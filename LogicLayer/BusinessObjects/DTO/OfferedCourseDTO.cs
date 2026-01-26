using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessLogic;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class OfferedCourseDTO
    {
        public int OfferID { get; set; }
        public int AcademicCalenderID { get; set; }
        public Nullable<int> DeptID { get; set; }
        public Nullable<int> ProgramID { get; set; }
        public Nullable<int> TreeRootID { get; set; }
        public int CourseID { get; set; }
        public int VersionID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public int Limit { get; set; }
        public int Occupied { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public int Node_CourseID { get; set; }
        public bool IsActive { get; set; }

        public string VersionCode { get; set; }
        public string Title { get; set; }
        public string FormalCode { get; set; }

        public int AssignedAll
        { get; set; }
        public int Assigned
        { get; set; }
        public int OpenedAll
        { get; set; }
        public int Opened
        { get; set; }
        public int MandatoryAll
        { get; set; }
        public int Mandatory
        { get; set; }

        public decimal CourseCredit
        {
            get
            {
                Course course = CourseManager.GetByCourseIdVersionId(CourseID, VersionID);
                if (course != null)
                {
                    return course.Credits;
                }
                else
                    return 0;
            }
        }
    }
}
