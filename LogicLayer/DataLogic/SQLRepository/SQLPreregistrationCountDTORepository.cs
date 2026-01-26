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
    public partial class SQLPreregistrationCountDTORepository : IPreregistrationCountDTORepository
    {
        Database db = null;

        private string sqlGetAllByProgAcaCal = "CustomRegistrationWorksheetGetAllByProgAcaCal";

        public List<PreregistrationCountDTO> GetAllByProgAcaCal(int ProgramID, int AcademicCalenderID)
        {
            List<PreregistrationCountDTO> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<PreregistrationCountDTO> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<PreregistrationCountDTO>(sqlGetAllByProgAcaCal, mapper);
                IEnumerable<PreregistrationCountDTO> collection = accessor.Execute(ProgramID, AcademicCalenderID);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        #region Mapper
        private IRowMapper<PreregistrationCountDTO> GetMaper()
        {
            IRowMapper<PreregistrationCountDTO> mapper = MapBuilder<PreregistrationCountDTO>.MapAllProperties()
           .Map(m => m.CourseID).ToColumn("CourseID")
           .Map(m => m.VersionID).ToColumn("VersionID")
           .Map(m => m.FormalCode).ToColumn("FormalCode")
           .Map(m => m.CourseTitle).ToColumn("CourseTitle")
           .Map(m => m.AutoOpen).ToColumn("AutoOpen")
           .Map(m => m.AutoAssign).ToColumn("AutoAssign")
           .Build();

            return mapper;
        }
        #endregion
    }
}
