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
    public partial class SqlTCGPAByStudentRepositoryRepository : ITCGPAByStudentRepository
    {
        Database db = null;


        private string sqlGetTCGPAByStudentID = "MIU_RptTCGPAByStudent";





        public rTCGPAByStudent GetTCGPAByStudentId(int studentID)
        {
            rTCGPAByStudent _TCGPA = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rTCGPAByStudent> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<rTCGPAByStudent>(sqlGetTCGPAByStudentID, rowMapper);
                _TCGPA = accessor.Execute(studentID).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _TCGPA;
            }

            return _TCGPA;
        }

        

        #region Mapper

        private IRowMapper<rTCGPAByStudent> GetMaper()
        {
            IRowMapper<rTCGPAByStudent> mapper = MapBuilder<rTCGPAByStudent>.MapAllProperties()
            .Map(m => m.TCGPA).ToColumn("TranscriptCGPA")
            
            .Build();

            return mapper;
        }
        #endregion
    }
}
