using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
    public class ExamTypeNameBAO
    {

        public List<ExamTypeNameEntity> GetAllExamTypeName(string name)
        {
            return ExamTypeNameDAO.GetAllExamTypeName(name);
        }
        
        public ExamTypeNameEntity GetExamTypeName(int id)
        {
            return ExamTypeNameDAO.GetExamTypeName(id);
        }

        public bool Delete(int id)
        {
            return ExamTypeNameDAO.Delete(id);
        }
    }
}
