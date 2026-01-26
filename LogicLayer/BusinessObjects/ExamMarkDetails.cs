using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class ExamMarkDetails
    {
        public int ExamMarkDetailId {get; set; }
		public int ExamMarkMasterId {get; set; }
		public int CourseHistoryId {get; set; }
        public decimal Marks { get; set; }
        public decimal ConvertedMark { get; set; }
        public Nullable<bool> IsFinalSubmit { get; set; }
		public int ExamTemplateBasicItemId {get; set; }
		public int ExamStatus {get; set; }
        public int ExamMarkTypeId { get; set; }
		public string Attribute1 {get; set; }
		public string Attribute2 {get; set; }
		public string Attribute3 {get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedDate{get; set; }
		public int ModifiedBy {get; set; }
		public DateTime ModifiedDate{get; set; }
    }
}

