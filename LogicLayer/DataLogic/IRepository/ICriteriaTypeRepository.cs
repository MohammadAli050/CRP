using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICriteriaTypeRepository
    {
        int Insert(CriteriaType criteriatype);
        bool Update(CriteriaType criteriatype);
        bool Delete(int Id);
        CriteriaType GetById(int? Id);
        List<CriteriaType> GetAll();
    }
}
