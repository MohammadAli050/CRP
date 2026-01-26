using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
    public class DiscountContinuationSetup_BAO
    {
        public static int Save(List<DiscountContinuationSetupEntity> dcses)
        {
            try
            {
                int counter = 0;
                counter = DiscountContinuationSetup_DAO.Save(dcses);
                return counter;
            }
            catch (Exception ex)
            {
                //FixMe
                throw ex;
            }
        }
        public static List<DiscountContinuationSetupEntity> Gets(string acaID, string progID)
        {
            List<DiscountContinuationSetupEntity> dcses = DiscountContinuationSetup_DAO.GetDiscounts(Int32.Parse(acaID),Int32.Parse(progID));
            
            return dcses;
            
        }
    }
}
