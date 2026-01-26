using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IDegreeCompletionRepository
    {
        int Insert(DegreeCompletion degreeCompletion);
        bool Update(DegreeCompletion degreeCompletion);
        bool Delete(int id);
        DegreeCompletion GetById(int id);
        List<DegreeCompletion> GetAll();
        DegreeCompletion GetByStudentId(int studentId);
    }
}
