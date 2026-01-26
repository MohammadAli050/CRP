using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class ExamTemplateMaster
    {
        public int ExamTemplateMasterId {get; set; }
		public int ProgramId {get; set; }
		public int ExamTemplateMasterTypeId {get; set; }
		public string ExamTemplateMasterName {get; set; }
        public decimal ExamTemplateMasterTotalMark { get; set; }
		public string Attribute1 {get; set; }
		public string Attribute2 {get; set; }
		public string Attribute3 {get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedDate{get; set; }
		public int ModifiedBy {get; set; }
		public DateTime ModifiedDate{get; set; }

        public string ShortName
        {
            get
            {
                string sName = "";
                try
                {
                    sName = ProgramManager.GetById(ProgramId).ShortName;
                    return sName;
                }
                catch
                {
                    return sName;
                }
            }
        }
    }
}

