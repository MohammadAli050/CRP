using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
    public class SelectedStudentForGPACalculation_BAO
    {
        public static List<SelectedStudentForGPACalculationEntity> 
            GetSelectedRegisteredStudents(int batchid,int regCalId,int programID)
        {
            try
            {
                List<SelectedStudentForGPACalculationEntity> students =
                    SelectedStudentForGPACalculation_DAO.GetStudentList(batchid, regCalId, programID);
                return students;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void CalculateGPA(List<string> rolls, int registrationcalId)
        {
            foreach (string roll in rolls)
            {
                
            }
        }
    }
}
