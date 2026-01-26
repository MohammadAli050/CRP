using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class ExamMarkApproveDTO
    {
        public int AcaCal_SectionID { get; set; }
        public string Teacher { get; set; }
        public string Title { get; set; }
        public string FormalCode { get; set; }
        public string SectionName { get; set; }
        public int TotalStudent { get; set; }
        public bool IsPublish { get; set; }
        public bool IsFinalSubmit { get; set; }
        public bool IsTransfer { get; set; }        
    }
}
