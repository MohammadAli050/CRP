using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
   public class StudentDiscountWorkSheet_BAO
    {
       public static List<StudentDiscountWorksheetEntity> LoadData(int programId, int batchId, int studentId)
        {
            return Studentdiscountworksheet_DAO.LoadDAta(programId, batchId, studentId);
        }

        public static int Save(List<StudentDiscountWorksheetEntity> sdwEntities)
        {
            return Studentdiscountworksheet_DAO.Save(sdwEntities);
        }

        public static int Generate(int programId, int batchId, int studentId)
        {
           return Studentdiscountworksheet_DAO.Generate(programId, batchId, studentId);
        }
    }
}
