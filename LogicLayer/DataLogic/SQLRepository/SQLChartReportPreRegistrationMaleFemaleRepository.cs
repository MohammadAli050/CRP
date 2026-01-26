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
    public partial class SQLChartReportPreRegistrationMaleFemaleRepository : IChartReportPreRegistrationMaleFemaleRepository
    {
        Database db = null;

        private string sqlGetAllById = "ChartReportPreRegistrationMaleFemale";

        public List<ChartReportPreRegistrationMaleFemale> GetAllById(int acaCalId)
        {
            List<ChartReportPreRegistrationMaleFemale> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<ChartReportPreRegistrationMaleFemale> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ChartReportPreRegistrationMaleFemale>(sqlGetAllById, mapper);
                IEnumerable<ChartReportPreRegistrationMaleFemale> collection = accessor.Execute(acaCalId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<ChartReportPreRegistrationMaleFemale> GetMaper()
        {
            IRowMapper<ChartReportPreRegistrationMaleFemale> mapper = MapBuilder<ChartReportPreRegistrationMaleFemale>.MapAllProperties()

            .Map(m => m.Program).ToColumn("ShortName")
            .Map(m => m.NumberOfMaleStudent).ToColumn("NumberOfMale")
            .Map(m => m.NumberOfFeMaleStudent).ToColumn("NumberOfFemale")

            .Build();

            return mapper;
        }
    }
}
