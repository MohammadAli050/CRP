using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentSkillTypeRepository
    {
        int Insert(StudentSkillType studentSkillType);
        bool Update(StudentSkillType studentSkillType);
        bool Delete(int id);
        StudentSkillType GetById(int? id);
        List<StudentSkillType> GetAll();
    }
}
