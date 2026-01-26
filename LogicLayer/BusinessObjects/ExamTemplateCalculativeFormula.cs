using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ExamTemplateCalculativeFormula
    {
        public int Id {get; set; }
		public int ExamTemplateMasterId {get; set; }
		public int CalculationType {get; set; }
		public int ExamMetaTypeId {get; set; }
		public string Attribute1 {get; set; }
		public string Attribute2 {get; set; }
		public string Attribute3 {get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedDate{get; set; }
		public int ModifiedBy {get; set; }
        public DateTime ModifiedDate { get; set; }

        public string ExamMetaTypeName { get; set; }
        public string ExamCalculationTypeName { get; set; }

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

