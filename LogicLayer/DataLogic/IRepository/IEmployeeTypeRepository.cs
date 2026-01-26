using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IEmployeeTypeRepository
    {
        int Insert(EmployeeType employeetype);
        bool Update(EmployeeType employeetype);
        bool Delete(int EmployeeTypeId);
        EmployeeType GetById(int? EmployeeTypeId);
        List<EmployeeType> GetAll();
    }
}

