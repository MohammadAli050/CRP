using LogicLayer.BusinessObjects.RO;
using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessLogic
{
    public class RegistrationStatusReportManager
    {
        public static List<rptRegistrationStatus> GetByCourseIdOrTeacherId(int programId, int sessionId, int courseId, int teacherId)
        {
            List<rptRegistrationStatus> registrationSatatusList = RepositoryManager.RegistrationStatus_Repository.GetByCourseIdOrTeacherId(programId,sessionId,courseId, teacherId);
            return registrationSatatusList;
        }
    }
}
