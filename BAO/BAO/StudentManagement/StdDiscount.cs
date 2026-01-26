using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Data.SqlClient;
using DataAccess;
using Common;

namespace BussinessObject
{
    public class StdDiscount
    {
        #region Methods
        public static int Save(List<StdDiscountEntity> sds)
        {
            try
            {
                var result = from res in sds
                             where res.TypePercentage != -1M
                             select res;
                return StdDiscount_DAO.Save(result.ToList<StdDiscountEntity>());
            }
            catch (Exception ex)
            {
                //FisMe
                throw ex;
            }
        }
        public static List<StdDiscountEntity> GetStdDiscounts(int adminID)
        {
            return StdDiscount_DAO.GetStdDiscounts(adminID);
        }
        #endregion
    }
}
