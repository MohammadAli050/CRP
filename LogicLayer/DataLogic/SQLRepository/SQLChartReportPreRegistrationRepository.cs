using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLChartReportPreRegistrationRepository : IChartReportPreRegistrationRepository
    {
        Database db = null;

        private string sqlGetAllById = "GetChartReportPreRegistration";
        private string sqlGetAllByAcaCalId = "ChartReportRegistration";

        public List<ChartReportPreRegistration> GetAllById(int acaCalId)
        {
            List<ChartReportPreRegistration> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<ChartReportPreRegistration> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ChartReportPreRegistration>(sqlGetAllById, mapper);
                IEnumerable<ChartReportPreRegistration> collection = accessor.Execute(acaCalId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }
        public List<ChartReportPreRegistration> GetAllByAcaCalId(int acaCalId)
        {
            List<ChartReportPreRegistration> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<ChartReportPreRegistration> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ChartReportPreRegistration>(sqlGetAllByAcaCalId, mapper);
                IEnumerable<ChartReportPreRegistration> collection = accessor.Execute(acaCalId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<ChartReportPreRegistration> GetMaper()
        {
            IRowMapper<ChartReportPreRegistration> mapper = MapBuilder<ChartReportPreRegistration>.MapAllProperties()

            .Map(m => m.Program).ToColumn("ShortName")
            .Map(m => m.NumberOfStudent).ToColumn("NumberOfStudent")
            
            .Build();

            return mapper;
        }
    }
}
