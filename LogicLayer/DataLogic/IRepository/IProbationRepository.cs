using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IProbationRepository
    {
        List<rProbationStudent> GetAllByProgramOrder(int progamId, string orderType);
        List<rProbationStudent> GetAll(int FromAcaCalId, int ToAcaCalId, decimal FromRange, decimal ToRange, int ProgamId, int FromSemester, int ToSemester);
    }
}
