using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
   public interface  IOperatorRepository
    {
       int Insert(Operator objOperator);
       bool Update(Operator objOperator);
       bool Delete(int id);
       Operator GetById(int? id);
       List<Operator> GetAll();
    }
}
