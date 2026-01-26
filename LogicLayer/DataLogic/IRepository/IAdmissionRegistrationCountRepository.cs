using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IAdmissionRegistrationCountRepository
    {
        List<rAdmittedRegisteredCount> GetAdmittedRegisteredCountByProgramYearWise(int ProgramId, int FromYear, int ToYear);
    }
}
