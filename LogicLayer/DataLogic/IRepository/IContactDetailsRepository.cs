using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IContactDetailsRepository
    {
        int Insert(ContactDetails contact);
        bool Update(ContactDetails contact);
        bool Delete(int id);
        List<ContactDetails> GetAll();
        ContactDetails GetContactDetailsByPersonID(int personId);
    }
}
