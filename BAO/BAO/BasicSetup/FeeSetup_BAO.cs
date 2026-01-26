using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using Common;

namespace BussinessObject
{
    public class FeeSetup_BAO
    {
        #region Methods
        public static int Save(List<FeeSetupEntity> sds)
        {
            try
            {
                var result = from res in sds
                             where res.Amount != -1M
                             select res;
                return FeeSetup_DAO.Save(result.ToList<FeeSetupEntity>());
            }
            catch (Exception ex)
            {
                //FisMe
                throw ex;
            }
        }
        public static List<FeeSetupEntity> Gets(string acaCalID, string progID)
        {
            return FeeSetup_DAO.Gets(Int32.Parse(acaCalID), Int32.Parse(progID));
        }
        #endregion
    }
}
