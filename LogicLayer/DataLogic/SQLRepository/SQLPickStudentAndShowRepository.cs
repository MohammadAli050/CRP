using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.SQLRepository
{
    partial class SQLPickStudentAndShowRepository : IPickStudentAndShowRepository
    {
        Database db = null;

        private string sqlGetAll = "pickStudentAndShow";

        public List<PickStudentAndShow> GetAll(string Roll)
        {
            List<PickStudentAndShow> pickStudentAndShow = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PickStudentAndShow> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<PickStudentAndShow>(sqlGetAll, mapper);
                IEnumerable<PickStudentAndShow> collection = accessor.Execute(Roll);

                pickStudentAndShow = collection.ToList();
            }

            catch (Exception ex)
            {
                return pickStudentAndShow;
            }

            return pickStudentAndShow;
        }

        #region Mapper

        private IRowMapper<PickStudentAndShow> GetMaper()
        {
            IRowMapper<PickStudentAndShow> mapper = MapBuilder<PickStudentAndShow>.MapAllProperties()
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.FirstName).ToColumn("FirstName")
            .Build();

            return mapper;
        }

        #endregion
    }
}
