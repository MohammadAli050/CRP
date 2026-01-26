using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class AdmissionRegistrationCountManager
    {        
        public static List<rAdmittedRegisteredCount> GetAdmittedRegisteredCountByProgramYearWise(int ProgramId,int FromYear,int ToYear)
        {
            List<rAdmittedRegisteredCount> list = RepositoryManager.AdmissionRegistrationCount_Repository.GetAdmittedRegisteredCountByProgramYearWise(ProgramId,FromYear,ToYear);
            return list;
        }

    }
}
