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
    public partial class SQLClassForceOperationRepository : IClassForceOperationRepository
    {
        Database db = null;

        private string sqlGetByParameters = "ForceOperationGetByParameters";

        public List<ClassForceOperation> GetAllByParameters(int programId, string batchId, int semesterId, int courseId, string studentRoll)
        {
            List<ClassForceOperation> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<ClassForceOperation> mapper = GetMaper();
                
                var accessor = db.CreateSprocAccessor<ClassForceOperation>(sqlGetByParameters, mapper);
                IEnumerable<ClassForceOperation> collection = accessor.Execute(programId, batchId, semesterId, courseId, studentRoll);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<ClassForceOperation> GetMaper()
        {
            IRowMapper<ClassForceOperation> mapper = MapBuilder<ClassForceOperation>.MapAllProperties()

            .Map(m => m.ID).ToColumn("ID")
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.CourseStatus).ToColumn("CourseStatus")
            .Map(m => m.Grade).ToColumn("Grade")
            .Map(m => m.StudentName).ToColumn("StudentName")
            .Map(m => m.CourseCode).ToColumn("CourseCode")
            .Map(m => m.CourseName).ToColumn("CourseName")
            .Map(m => m.CourseCredit).ToColumn("CourseCredit")
            .Map(m => m.PreRequisiteCourseName).ToColumn("PreRequisiteCourseName")
            .Map(m => m.Semester).ToColumn("Semester")
            .Map(m => m.IsMandatory).ToColumn("IsMandatory")
            .Map(m => m.IsAutoAssign).ToColumn("IsAutoAssign")
            .Map(m => m.IsAutoOpen).ToColumn("IsAutoOpen")
            .Map(m => m.Priority).ToColumn("Priority")
            .Map(m => m.SequenceNo).ToColumn("SequenceNo")

            .Build();
            return mapper;
        }
    }
}