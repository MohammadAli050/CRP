using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class ExamTemplateBasicItemDetails
    {
        public int ExamTemplateBasicItemId {get; set; }
		public int ExamTemplateMasterId {get; set; }
        public decimal ExamTemplateBasicItemMark { get; set; }
		public string ExamTemplateBasicItemName {get; set; }
		public int ExamTypeId {get; set; }
		public int ColumnSequence {get; set; }
		public string Attribute1 {get; set; }
		public string Attribute2 {get; set; }
		public string Attribute3 {get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedDate{get; set; }
		public int ModifiedBy {get; set; }
		public DateTime ModifiedDate{get; set; }

        public ExamTemplateMaster ExamTemplateMaster
        {
            get
            {
                ExamTemplateMaster examTemplateMaster = ExamTemplateMasterManager.GetById(ExamTemplateMasterId);
                return examTemplateMaster;
            }
        }
    }
}

