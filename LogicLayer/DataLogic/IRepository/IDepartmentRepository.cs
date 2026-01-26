using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IDepartmentRepository
    {
        int Insert(Department department);
        bool Update(Department department);
        bool Delete(int id);
        Department GetById(int? id);
        List<Department> GetAll();
    }
}
