using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IPreviousExamTypeRepository
    {
        int Insert(PreviousExamType examtype);
        bool Update(PreviousExamType examtype);
        bool Delete(long id);
        PreviousExamType GetById(long id);
        List<PreviousExamType> GetAll();
        List<PreviousExamType> Search(string value);
    }
}
