using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessLogic
{
   public class ChartReportPreRegistrationManager
    {
       public static List<ChartReportPreRegistration> GetAllById(int acaCalId)
        {
            List<ChartReportPreRegistration> list = RepositoryManager.ChartReportPreRegistration_Repository.GetAllById(acaCalId);
               
            return list;
        }
       public static List<ChartReportPreRegistration> GetAllByAcaCalId(int acaCalId)
       {
           List<ChartReportPreRegistration> list = RepositoryManager.ChartReportPreRegistration_Repository.GetAllByAcaCalId(acaCalId);

           return list;
       }

    }
}
