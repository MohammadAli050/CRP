using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class PrerequisiteMaster
    {
        public int PrerequisiteMasterID { get; set; }
        public string Name { get; set; }
        public int ProgramID { get; set; }
        public int NodeID { get; set; }
        public int NodeCourseID { get; set; }
        public decimal ReqCredits { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        public List<PrerequisiteDetail> PrerequisiteDetailList
        {
            get
            {
                return PrerequisiteDetailManager.GetByPrerequisiteMasterID(PrerequisiteMasterID);
            }
        }
    }
}
