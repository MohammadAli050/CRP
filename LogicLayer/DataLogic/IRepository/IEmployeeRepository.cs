using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IEmployeeRepository
    {
        int Insert(Employee employee);
        bool Update(Employee employee);
        bool Delete(int id);
        Employee GetById(int id);
        Employee GetByPersonId(int id);
        List<Employee> GetAll();
        List<Employee> GetAllByCode(string code);
        bool ValidateEmployee(string code);
        List<Employee> GetAllByNameOrCode(string name, string code);
        List<rEmployeeInfo> GetEmployeeInfoByDeptId(int deptId);
    }
}
