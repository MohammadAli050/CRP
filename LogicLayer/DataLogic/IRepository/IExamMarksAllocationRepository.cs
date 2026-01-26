using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamMarksAllocationRepository
    {
        int Insert(ExamMarksAllocation examMarksAllocation);
        bool Update(ExamMarksAllocation examMarksAllocation);
        bool Delete(int id);
        ExamMarksAllocation GetById(int? id);
        List<ExamMarksAllocation> GetAll();
    }
}
