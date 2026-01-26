using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IChartReportPreRegistrationRepository
    {
        List<ChartReportPreRegistration> GetAllById(int acaCalId);
        List<ChartReportPreRegistration> GetAllByAcaCalId(int acaCalId);
    }
}
