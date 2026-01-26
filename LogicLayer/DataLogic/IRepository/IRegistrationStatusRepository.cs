using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IRegistrationStatusRepository
    {
        List<rptRegistrationStatus> GetByCourseIdOrTeacherId(int programId, int sessionId, int courseId, int teacherId);
    }
}
