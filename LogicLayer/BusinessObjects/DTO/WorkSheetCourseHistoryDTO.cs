using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class WorkSheetCourseHistoryDTO
    {
        public string FullName { get; set; }
        public string Roll { get; set; }
        public int BatchNo { get; set; }
        public bool IsWorkSheet { get; set; }
        public bool IsCourseHistory { get; set; }
    }
}
