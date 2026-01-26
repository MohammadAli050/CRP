using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ISchoolRepository
    {
        int Insert(School school);
        bool Update(School school);
        bool Delete(int id);
        School GetById(int? id);
        List<School> GetAll();
    }
}
