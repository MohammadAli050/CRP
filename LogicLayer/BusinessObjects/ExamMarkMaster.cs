using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class ExamMarkMaster
    {
        public int ExamMarkMasterId {get; set; }
        public decimal ExamMark { get; set; }
		public DateTime ExamMarkEntryDate{get; set; }
        public Nullable <DateTime> ExamDate { get; set; }
		public int ExamTemplateBasicItemId {get; set; }
		public int AcaCalId {get; set; }
		public int AcaCalSectionId {get; set; }
		public string Attribute1 {get; set; }
		public string Attribute2 {get; set; }
		public string Attribute3 {get; set; }
		public int CreatedBy {get; set; }
        public DateTime CreatedDate { get; set; }
		public int ModifiedBy {get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

