using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class CourseACUSpanMas
    {
        public int CourseACUSpanMasID { get; set; }
        public int CourseID { get; set; }
        public int VersionID { get; set; }
        public int MaxACUNo { get; set; }
        public int MinACUNo { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public  List<CourseACUSpanDtl> CourseACUSpanDetails
        {
            get 
            {
                List<CourseACUSpanDtl> courseACUSpanDetail = null;
                courseACUSpanDetail = CourseACUSpanDtlManager.GetAllByCourseACUSpanMasID(CourseACUSpanMasID);

                return courseACUSpanDetail;
            }
        }
    }
}
