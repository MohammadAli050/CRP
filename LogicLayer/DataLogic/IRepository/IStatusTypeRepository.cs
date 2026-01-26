using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStatusTypeRepository
    {
        int Insert(StatusType statusType);
        bool Update(StatusType statusType);
        bool Delete(int id);
        StatusType GetById(int? id);
        List<StatusType> GetAll();
    }
}
