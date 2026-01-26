using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ISkillTypeRepository
    {
        int Insert(SkillType skillType);
        bool Update(SkillType skillType);
        bool Delete(int id);
        SkillType GetById(int? id);
        List<SkillType> GetAll();
    }
}
