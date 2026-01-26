using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IDeptRegSetUpRepository
    {
        
        int Insert(DeptRegSetUp deptRegSetUp);
        bool Update(DeptRegSetUp deptRegSetUp);
        bool Delete(int id);
        DeptRegSetUp GetById(int? id);
        List<DeptRegSetUp> GetAll();
    }
}
