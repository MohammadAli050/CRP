using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ITeacherInformationRepository
    {
        bool Insert(TeacherInfo teacher);
        TeacherInfo GetById(string id);
        bool Update(TeacherInfo teacher);
        List<TeacherInfo> GetByNameOrId(string name, string id);
        bool VaildateTeacher(string id);

    }
}
