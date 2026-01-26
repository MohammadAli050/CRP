using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
    public class ExamMarksAllocationBAO
    {
        public List<ExamMarksAllocationEntity> GetExamMarksAllocation(int xmTypNmID)
        {
        return ExamMarksAllocationDAO.GetExamMarksAllocation(xmTypNmID);
        }
    }
}
