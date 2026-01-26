using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.SQLRepository
{
    public class SQLStudentGradeDetailRepository : IStudentGradeDetailRepository
    {
        Database db = null;
        public List<rStudentGradeDetail> GetAllGrade(string studentId)
        {
            List<rStudentGradeDetail> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentGradeDetail> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<rStudentGradeDetail>("RptStudentGradeDetailsById", mapper);
                IEnumerable<rStudentGradeDetail> collection = accessor.Execute(studentId);
            
                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        private IRowMapper<rStudentGradeDetail> GetMaper()
        {
            IRowMapper<rStudentGradeDetail> mapper = MapBuilder<rStudentGradeDetail>.MapAllProperties()
            .Map(m => m.MarksRange).ToColumn("MarksRange")
            .Map(m => m.Grade).ToColumn("Grade")
            .Map(m => m.GradePoint).ToColumn("GradePoint")
            
            .Build();
            return mapper;
        }
    }
}
