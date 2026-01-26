using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessLogic;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class OfferedCourse
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
        public string CourseCode
        {
            get
            {
                return CourseManager.GetByCourseIdVersionId(CourseID, VersionID).FormalCode;

            }
        }
        public string CourseTitle
        {
            get
            {
                return CourseManager.GetByCourseIdVersionId(CourseID, VersionID).Title;
            }
        }
        public string FormalCodeAndTitle { get; set; }

        public TreeMaster TreeRoot
        {
            get
            {
                TreeMaster treeMaster = TreeMasterManager.GetById(TreeRootID);
                return treeMaster;
            }
        }
    }
}
