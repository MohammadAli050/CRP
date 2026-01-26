using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
    public class RelationDiscountSection_BAO
    {
        public static int Save(List<RelationDiscountSectionEntity> dces)
        {
            int counter = 0;
            counter = RelationDiscountSection_DAO.Save(dces);
            return counter;
        }
        public static List<RelationDiscountSectionEntity> GetRelations(int acaID, int progID)
        {
            return RelationDiscountSection_DAO.GetRelations(acaID, progID);
        }
    }
}
