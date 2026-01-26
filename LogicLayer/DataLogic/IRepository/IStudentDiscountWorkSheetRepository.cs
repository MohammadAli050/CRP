using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentDiscountWorkSheetRepository
    {
        int Insert(StudentDiscountWorkSheet studentDiscountWorkSheet);
        bool Update(StudentDiscountWorkSheet studentDiscountWorkSheet);
        bool Delete(int id);
        StudentDiscountWorkSheet GetById(int? id);
        List<StudentDiscountWorkSheet> GetAll();
    }
}
