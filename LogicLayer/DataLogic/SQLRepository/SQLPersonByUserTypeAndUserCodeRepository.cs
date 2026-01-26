using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLPersonByUserTypeAndUserCodeRepository : IPersonByUserTypeAndUserCodeRepository
    {
        Database db = null;

        private string sqlPersonGetAllByUserTypeAndUserCode = "PersonGetAllByUserTypeAndUserCode";
        public List<PersonByUserTypeAndUserCode> GetAllPersonByUserTypeAndUserCode(int userType, string userCode)
        {
            List<PersonByUserTypeAndUserCode> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<PersonByUserTypeAndUserCode> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<PersonByUserTypeAndUserCode>(sqlPersonGetAllByUserTypeAndUserCode, mapper);
                IEnumerable<PersonByUserTypeAndUserCode> collection = accessor.Execute(userType, userCode);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        #region Mapper


        private IRowMapper<PersonByUserTypeAndUserCode> GetMaper()
        {
            IRowMapper<PersonByUserTypeAndUserCode> mapper = MapBuilder<PersonByUserTypeAndUserCode>.MapAllProperties()
            .Map(m => m.PersonID).ToColumn("PersonID")
            .Map(m => m.Name).ToColumn("Name")
            .Map(m => m.Code).ToColumn("Code")
            .Build();

            return mapper;
        }
        #endregion
    }
}
