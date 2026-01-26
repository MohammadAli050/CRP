using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICourseListByProgram
    {
        List<rCourseListByProgram> GetAllByProgram(int programId);
    }
}
