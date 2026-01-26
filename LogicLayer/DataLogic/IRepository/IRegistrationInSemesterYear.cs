using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IRegistrationInSemesterYear
    {
        List<rRegistrationInSemesterYear> GetAllBySemesterYear(int year, int trimesterAcaCalId, int semesterAcaCalId);
    }
}
