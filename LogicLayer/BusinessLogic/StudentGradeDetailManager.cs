using LogicLayer.BusinessObjects.RO;
using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessLogic
{
    public class StudentGradeDetailManager
    {
        public static List<rStudentGradeDetail> GetAllGrade(string studentId)
        {

            List<rStudentGradeDetail> list = RepositoryManager.StudentGradeDetail_Repository.GetAllGrade(studentId);
            return list;
        }
    }
}
