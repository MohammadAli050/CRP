using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ITCGPAByStudentRepository
    {
        rTCGPAByStudent GetTCGPAByStudentId(int studentId);
    }
}
