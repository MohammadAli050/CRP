using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class TCGPAByStudentReportManager
    {

        public static rTCGPAByStudent GetTCGPAByStudentId(int studentId)
        {
            rTCGPAByStudent tCGPA = RepositoryManager.TCGPAByStudent_Repository.GetTCGPAByStudentId(studentId);
            return tCGPA;
        }
    }
}
