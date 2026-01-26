using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class Course
    {
        public int CourseID { get; set; }
        public int VersionID { get; set; }
        public string FormalCode { get; set; }
        public string VersionCode { get; set; }
        public string Title { get; set; }
        public int AssocCourseID { get; set; }
        public int AssocVersionID { get; set; }
        public Nullable<int> StartAcademicCalenderID { get; set; }
        public Nullable<int> ProgramID { get; set; }
        public string CourseContent { get; set; }
        public Nullable<bool> IsCreditCourse { get; set; }
        public decimal Credits { get; set; }
        public Nullable<bool> IsSectionMandatory { get; set; }
        public Nullable<bool> HasEquivalents { get; set; }
        public bool HasMultipleACUSpan { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> TypeDefinitionID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public int CourseGroupId { get; set; }
        
        public string TranscriptCode { get; set; }
        public string CourseGroup { get; set; }
        public Nullable<int> BillTypeDefinitionID { get; set; }

        #region Custom Property

        public string CourseFullInfo
        {
            get
            {
                return FormalCode + " - " + Title;
            }
        }

        public string CoureIdVersionId { get { return CourseID.ToString() + "_" + VersionID.ToString(); } }

        public Course AssociateCourse
        {
            get
            {
                return CourseManager.GetByCourseIdVersionId(AssocCourseID, AssocVersionID);
            }
        }
        
        public string ProgramShortName
        {
            get
            {
                return ProgramManager.GetById(ProgramID).ShortName;
            }
        }

        public string CourseType
        {
            get
            {
                TypeDefinition td = TypeDefinitionManager.GetById((int)TypeDefinitionID);
                return td.Definition;
            }
        }
        
        #endregion
    }
}
