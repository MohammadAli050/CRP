using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IPreregistrationCountDTORepository
    {
        List<PreregistrationCountDTO> GetAllByProgAcaCal(int ProgramID, int AcademicCalenderID);
    }
}
