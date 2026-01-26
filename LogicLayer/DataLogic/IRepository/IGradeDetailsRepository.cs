using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
   public interface IGradeDetailsRepository
    {
       int Insert(GradeDetails gradeDetails);
       bool Update(GradeDetails gradeDetails);
       bool Delete(int id);
       GradeDetails GetById(int id);
       GradeDetails GetByGrade(string grade);
       List<GradeDetails> GetAll();
       List<GradeDetails> GetByGradeMasterId(int gradeMasterId);
       //added by mohammad ali

       GradeDetails GetByMarks(decimal mark);
    }
}
