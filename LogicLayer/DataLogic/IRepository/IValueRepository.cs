using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IValueRepository
    {
        int Insert(Value value);
        bool Update(Value value);
        bool Delete(int id);
        Value GetById(int id);
        List<Value> GetAll();
        List<Value> GetByValueSetId(int valueSet);
    }
}
