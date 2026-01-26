using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessLogic
{
    public class ChartReportPreRegistrationMaleFemaleManager
    {
        public static List<ChartReportPreRegistrationMaleFemale> GetAllById(int acaCalId)
        {
            List<ChartReportPreRegistrationMaleFemale> list = RepositoryManager.ChartReportPreRegistrationMaleFemale_Repository.GetAllById(acaCalId);

            return list;
        }
    }
}
