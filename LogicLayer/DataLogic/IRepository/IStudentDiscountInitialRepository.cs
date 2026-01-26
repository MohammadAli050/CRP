using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentDiscountInitialRepository
    {
        int Insert(StudentDiscountInitial studentdiscountinitial);
        bool Update(StudentDiscountInitial studentdiscountinitial);
        bool Delete(int Id);
        StudentDiscountInitial GetById(int? Id);
        List<StudentDiscountInitial> GetAll();
    }
}