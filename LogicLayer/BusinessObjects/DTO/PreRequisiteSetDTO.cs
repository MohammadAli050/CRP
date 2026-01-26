using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable] 
    public class PreRequisiteSetDTO
    {
        public int PreRequisiteDetailId { get; set; }
        public int PreRequisiteMasterId { get; set; }
        public int ProgramId { get; set; }
        public int NodeId { get; set; }
        public int CourseId { get; set; }
        public int VersionId { get; set; }
        public string FormalCode { get; set; }
        public string VersionCode { get; set; }
        public string NodeName { get; set; }
        public string Title { get; set; }

        public string ProgramName { get; set; }
        public decimal Credits { get; set; }
 
    }
}
