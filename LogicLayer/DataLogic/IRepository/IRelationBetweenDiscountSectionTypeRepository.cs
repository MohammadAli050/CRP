using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IRelationBetweenDiscountSectionTypeRepository
    {
        int Insert(RelationBetweenDiscountSectionType relationBetweenDiscountSectionType);
        bool Update(RelationBetweenDiscountSectionType relationBetweenDiscountSectionType);
        bool Delete(int id);
        RelationBetweenDiscountSectionType GetById(int? id);
        List<RelationBetweenDiscountSectionType> GetAll();
    }
}
