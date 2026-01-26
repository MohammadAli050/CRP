using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
    public class RelationDiscountCourse_BAO
    {
        public static int Save(List<RelationDiscountCourseEntity> dces)
        {
            int counter = 0;
            counter = RelationDiscountCourse_DAO.Save(dces);
            return counter;
        }
        public static List<RelationDiscountCourseEntity> GetRelations(int acaID, int progID)
        {
            return RelationDiscountCourse_DAO.GetRelations(acaID, progID);
        }
    }
}
