using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IValueSetRepository
    {
        int Insert(ValueSet valueset);
        bool Update(ValueSet valueset);
        bool Delete(int id);
        ValueSet GetById(int? id);
        List<ValueSet> GetAll();
    }
}
