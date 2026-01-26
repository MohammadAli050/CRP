using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;
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
    public class SQLRegistrationStatusRepository : IRegistrationStatusRepository
    {
        Database db = null;

        public List<rptRegistrationStatus> GetByCourseIdOrTeacherId(int programId, int sessionId, int courseId, int teacherId) 
        {
            List<rptRegistrationStatus> registrationStatusList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rptRegistrationStatus> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<rptRegistrationStatus>("rptRegistrationStatusByCourseOrTeacherId", mapper);
                IEnumerable<rptRegistrationStatus> collection = accessor.Execute(programId, sessionId, courseId, teacherId);

                registrationStatusList = collection.ToList();
            }

            catch (Exception ex)
            {
                return registrationStatusList;
            }

            return registrationStatusList;
        }

        private IRowMapper<rptRegistrationStatus> GetMaper()
        {
            IRowMapper<rptRegistrationStatus> mapper = MapBuilder<rptRegistrationStatus>.MapAllProperties()
            .Map(m => m.CourseHistoryId).ToColumn("CourseHistoryId")
            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.CourseId).ToColumn("CourseId")
            .Map(m => m.StudentName).ToColumn("StudentName")
            .Map(m => m.StudentRoll).ToColumn("StudentRoll")
            .Map(m => m.SectionName).ToColumn("SectionName")
            .Map(m => m.CourseCode).ToColumn("CourseCode")
            .Map(m => m.CourseName).ToColumn("CourseName")
            .Map(m => m.TeacherName).ToColumn("TeacherName")
            .Map(m => m.TeacherCode).ToColumn("TeacherCode")


            .Build();

            return mapper;
        }
    }
}
