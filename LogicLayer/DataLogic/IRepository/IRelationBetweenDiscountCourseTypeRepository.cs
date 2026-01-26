using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IRelationBetweenDiscountCourseTypeRepository
    {
        int Insert(RelationBetweenDiscountCourseType relationBetweenDiscountCourseType);
        bool Update(RelationBetweenDiscountCourseType relationBetweenDiscountCourseType);
        bool Delete(int id);
        RelationBetweenDiscountCourseType GetById(int? id);
        List<RelationBetweenDiscountCourseType> GetAll();
    }
}
