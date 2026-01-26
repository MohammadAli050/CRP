using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class SMSBasicSetupManager
    {
        public static bool Update(SMSBasicSetup smsSetup)
        {
            bool isExecute = RepositoryManager.SMSBasicSetup_Repository.Update(smsSetup);
            return isExecute;
        }

        public static SMSBasicSetup Get()
        {
            SMSBasicSetup smsSetup = RepositoryManager.SMSBasicSetup_Repository.Get();
            return smsSetup;
        }
    }
}
