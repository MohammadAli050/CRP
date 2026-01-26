using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class ExamMarkPublishAcaCalSectionRelation
    {
        public int Id {get; set; }
		public int AcacalSectionId {get; set; }
		public bool IsFinalSubmit{get; set; }
		public DateTime FinalSubmitDate{get; set; }
		public int FinalSubmitBy {get; set; }
		public bool IsApproved{get; set; }
		public Nullable<DateTime> ApprovedDate{get; set; }
		public int ApprovedBy {get; set; }
        public bool IsPublished { get; set; }
        public Nullable<DateTime> PublishedDate { get; set; }
        public int PublishedBy { get; set; }
		public string Attribute1 {get; set; }
		public string Attribute2 {get; set; }
		public string Attribute3 {get; set; }
		public string Attribute4 {get; set; }
		public string Attribute5 {get; set; }
		public string Attribute6 {get; set; }
		public string Attribute7 {get; set; }
		public string Attribute8 {get; set; }
    }
}

