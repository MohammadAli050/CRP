using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class CoursePredictDetails
    {
        public int Id {get; set; }
		public int CoursePredictMasterId {get; set; }
		public int StudentId {get; set; }
        public string Gender { get; set; }
		public int CourseId {get; set; }
		public int VersionId {get; set; }
		public int NodeId {get; set; }
		public string NodeLinkName {get; set; }
		public string Attribute1 {get; set; }
		public string Attribute2 {get; set; }
		public string Attribute3 {get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedDate{get; set; }
		public int ModifiedBy {get; set; }
		public DateTime MofifiedDate{get; set; }

        public string CourseName { get; set; }
    }
}

