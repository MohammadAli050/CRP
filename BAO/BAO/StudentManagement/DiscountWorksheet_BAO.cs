using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
   public class DiscountWorksheet_BAO
    {
       public static List<DiscountWorksheetEntity> GetBy(int acaCalId, int programId, int studentId, int courseId, int versionId)
        {
            return DiscountWorksheet_DAO.GetBy(acaCalId, programId, studentId, courseId, versionId);
        }

       public static int Save(List<DiscountWorksheetEntity> dwEntities)
       {
           return DiscountWorksheet_DAO.Save(dwEntities);
       }

       public static List<DiscountWorksheetEntity> LoadForEdit(int acaCalId, int programId, int studentId, int courseId, int versionId)
       {
           return DiscountWorksheet_DAO.LoadForEdit(acaCalId, programId, studentId, courseId, versionId);
       }
    }
}
