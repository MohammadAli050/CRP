using System;
using System.Collections.Generic;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ExamType
    {
        public int ExamTypeId { get; set; }
        public int ProgramId { get; set; }
        public string ExamTypeName { get; set; }
        public int ExamMetaTypeId { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
