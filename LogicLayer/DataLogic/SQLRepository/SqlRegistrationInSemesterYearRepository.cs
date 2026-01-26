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
    public partial class SqlRegistrationInSemesterYearRepository : IRegistrationInSemesterYear
    {
        Database db = null;


        private string sqlGetAllByGetAllBySemesterYear = "MIU_UCAM_RegistrationInSemesterYear";

        public List<rRegistrationInSemesterYear> GetAllBySemesterYear(int year, int trimesterAcaCalId, int semesterAcaCalId)
        {
            List<rRegistrationInSemesterYear> list = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rRegistrationInSemesterYear> mapper = GetMaper();


                var accessor = db.CreateSprocAccessor<rRegistrationInSemesterYear>(sqlGetAllByGetAllBySemesterYear, mapper);

                IEnumerable<rRegistrationInSemesterYear> collection = accessor.Execute(year, trimesterAcaCalId, semesterAcaCalId).ToList();

                list = collection.ToList();

            }
            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        

        #region Mapper

        private IRowMapper<rRegistrationInSemesterYear> GetMaper()
        {
            IRowMapper<rRegistrationInSemesterYear> mapper = MapBuilder<rRegistrationInSemesterYear>.MapAllProperties()
            .Map(m => m.Total).ToColumn("Total")
            .Map(m => m.Male).ToColumn("Male")
            .Map(m => m.Female).ToColumn("Female")
            .Map(m => m.ProgramShortName).ToColumn("Program")
            
            .Build();

            return mapper;
        }
        #endregion
    }
}
