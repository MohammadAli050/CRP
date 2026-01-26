using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class BillView
    {
        public int BillViewId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int VersionId { get; set; }
        public int TrimesterId { get; set; }
        public decimal Amount { get; set; }
        public string Purpose { get; set; }
        public int AccountsID { get; set; }
        public decimal AmountByCollectiveDiscount { get; set; }
        public decimal AmountByIterativeDiscount { get; set; }

        public Student StudentInfo
        {
            get
            {
                return StudentManager.GetById(StudentId);
            }
        }

        public Course CourseInfo
        {
            get
            {
                return CourseManager.GetByCourseIdVersionId(CourseId, VersionId);
            }
        }

        public AccountHeads AccountHead
        {
            get
            {
                return AccountHeadsManager.GetById(AccountsID);
            }
        }

        public AcademicCalender AcademicCalender
        {
            get
            {
                return AcademicCalenderManager.GetById(TrimesterId);
            }
        }

    }
}

