using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IClassForceOperationRepository
    {
        List<ClassForceOperation> GetAllByParameters(int programId, string batchId, int semesterId, int courseId, string studentRoll);
    }
}
