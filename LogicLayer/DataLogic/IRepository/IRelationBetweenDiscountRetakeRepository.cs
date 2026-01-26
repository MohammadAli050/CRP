using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IRelationBetweenDiscountRetakeRepository
    {
        int Insert(RelationBetweenDiscountRetake relationBetweenDiscountRetake);
        bool Update(RelationBetweenDiscountRetake relationBetweenDiscountRetake);
        bool Delete(int id);
        RelationBetweenDiscountRetake GetById(int? id);
        List<RelationBetweenDiscountRetake> GetAll();   
    }
}
