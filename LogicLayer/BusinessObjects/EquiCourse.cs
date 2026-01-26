using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class EquiCourse
    {
       public int EquivalentID { get; set; }
       public int ParentCourseID { get; set; }
       public int ParentVersionID { get; set; }
       public int EquiCourseID { get; set; }
       public int EquiVersionID { get; set; }
       public int CreatedBy { get; set; }
       public DateTime CreatedDate { get; set; }
       public int ModifiedBy { get; set; }
       public DateTime ModifiedDate { get; set; }

      public Course ParentCourse
       {
           get 
           {
               return CourseManager.GetByCourseIdVersionId(ParentCourseID, ParentVersionID);
           }
       }

     public  Course EquivalentCourse
       {
           get
           {
               return CourseManager.GetByCourseIdVersionId(EquiCourseID, EquiVersionID);
           }
       }

    }
}
