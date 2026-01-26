using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SqlAdmissionRegistrationCountRepository : IAdmissionRegistrationCountRepository
    {
        Database db = null;

        private string sqlGetAdmittedRegisteredCountByProgramYearWise = "AdmittedAndRegisteredCountByProgramBatchYearWise";

        public List<rAdmittedRegisteredCount> GetAdmittedRegisteredCountByProgramYearWise(int ProgramId, int FromYear, int ToYear)
        {
            List<rAdmittedRegisteredCount> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rAdmittedRegisteredCount> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<rAdmittedRegisteredCount>(sqlGetAdmittedRegisteredCountByProgramYearWise, mapper);
                IEnumerable<rAdmittedRegisteredCount> collection = accessor.Execute(ProgramId,FromYear,ToYear);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        #region Mapper

        private IRowMapper<rAdmittedRegisteredCount> GetMaper()
        {
            IRowMapper<rAdmittedRegisteredCount> mapper = MapBuilder<rAdmittedRegisteredCount>.MapAllProperties()
            .Map(m => m.BatchNo).ToColumn("BatchNo")
            .Map(m => m.SessionID).ToColumn("SessionID")
            .Map(m => m.SessionName).ToColumn("SessionName")
            .Map(m => m.SemesterID).ToColumn("SemesterID")
            .Map(m => m.SemesterName).ToColumn("SemesterName")
            .Map(m => m.RegisteredCount).ToColumn("RegisteredCount")
            .Map(m => m.TotalStudent).ToColumn("TotalStudent")
            .Map(m => m.DroppedStudent).ToColumn("Dropped")

            .Build();

            return mapper;
        }
        #endregion
    }
}
