using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
    public class RelationDiscountRetake_BAO
    {
        public static int Save(List<RelationDiscountRetakeEntity> dces)
        {
            int counter = 0;
            counter = RelationDiscountRetake_DAO.Save(dces);
            return counter;
        }
        public static List<RelationDiscountRetakeEntity> GetRelations(int acaID, int progID)
        {
            return RelationDiscountRetake_DAO.GetRelations(acaID, progID);
        }
    }
}
