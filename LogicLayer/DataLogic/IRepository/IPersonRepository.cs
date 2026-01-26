using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IPersonRepository
    {
        int Insert(Person person);
        bool Update(Person person);
        bool Delete(int id);
        Person GetById(int? id);
        List<Person> GetAll();
        Person GetByUserId(int User_ID);
    }
}
