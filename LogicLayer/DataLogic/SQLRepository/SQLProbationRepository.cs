using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace LogicLayer.DataLogic.SQLRepository
{
    public class SQLProbationRepository:IProbationRepository
    {
        Database db = null;

        private string sqlGetAllByParameter = "StudentProbrationListGetAllByProgramOrder";
        private string sqlGetAllByAcaIdProgIdRange = "StudentProbrationListGenerateANDInsert";

        public List<rProbationStudent> GetAll(int FromAcaCalId, int ToAcaCalId, decimal FromRange, decimal ToRange, int ProgamId, int FromSemester, int ToSemester)
        {
            List<rProbationStudent> probationList = null;

            int id = 0;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rProbationStudent> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<rProbationStudent>(sqlGetAllByAcaIdProgIdRange, mapper);
                IEnumerable<rProbationStudent> collection = accessor.Execute(FromAcaCalId, ToAcaCalId, FromRange, ToRange, ProgamId, FromSemester, ToSemester);

                probationList = collection.ToList();
            }

            catch (Exception ex)
            {
                return probationList;
            }

            return probationList;
        }

        public List<rProbationStudent> GetAllByProgramOrder(int progamId, string orderType)
        {
            List<rProbationStudent> probationList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rProbationStudent> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<rProbationStudent>(sqlGetAllByParameter, mapper);
                IEnumerable<rProbationStudent> collection = accessor.Execute(progamId, orderType);

                probationList = collection.ToList();
            }

            catch (Exception ex)
            {
                return probationList;
            }

            return probationList;
        }

        private IRowMapper<rProbationStudent> GetMaper()
        {
            IRowMapper<rProbationStudent> mapper = MapBuilder<rProbationStudent>.MapAllProperties()
            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.PersonId).ToColumn("PersonId")
            .Map(m => m.Name).ToColumn("Name")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.AcaCalId).ToColumn("AcaCalId")
            .Map(m => m.AcaCalCode).ToColumn("AcaCalCode")
            .Map(m => m.GPA).ToColumn("GPA")
            .Map(m => m.CGPA).ToColumn("CGPA")
            .Map(m => m.ProbationCount).ToColumn("ProbationCount")
            .Map(m => m.CreateDate).ToColumn("CreateDate")
            .Build();

            return mapper;
        }
    }
}
