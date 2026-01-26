using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
    public class ExamBreakupBAO
    {
        public bool Save(ExamTypeNameEntity _examTypeName, List<ExamMarksAllocationEntity> _exmMarkAllocations)
        {
            return ExamBreakupDAO.Save(_examTypeName, _exmMarkAllocations);
            
        }

        public bool Update(ExamTypeNameEntity _examTypeName, List<ExamMarksAllocationEntity> _exmMarkAllocations)
        {
           return ExamBreakupDAO.Update(_examTypeName, _exmMarkAllocations);
        }
    }
}
