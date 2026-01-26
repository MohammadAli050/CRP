using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rTopSheetPresent
    {
        public int Id { get; set; }
        public int ExamScheduleId { get; set; }
        public string Roll { get; set; }
        public string CourseCode { get; set; }
        public string RowFlag { get; set; }
        public Nullable<int> SequenceNo { get; set; }
        public Nullable<int> RoomNo { get; set; }
        public Nullable<bool> IsPresent { get; set; }
        public string Section { get; set; }
        public string ShortName { get; set; }
        public string DetailName { get; set; }
        public string CTitle { get; set; }

    }
}
