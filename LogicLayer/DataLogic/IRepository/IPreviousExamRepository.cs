using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IPreviousExamRepository
    {
        int Insert(PreviousExam exam);
        bool Update(PreviousExam exam);
        bool Delete(long id);
        PreviousExam GetById(long id);
        List<PreviousExam> GetAll();
    }
}
