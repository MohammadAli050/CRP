using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
namespace LogicLayer.BusinessLogic
{
    public class ContactDetailsManager
    {
        public static int Insert(ContactDetails contact)
        {
            int id = RepositoryManager.ContactDetails_Repository.Insert(contact);
            return id;
        }

        public static bool Update(ContactDetails contact)
        {
            bool isExecute = RepositoryManager.ContactDetails_Repository.Update(contact);
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ContactDetails_Repository.Delete(id);
            return isExecute;
        }

        public static ContactDetails GetContactDetailsByPersonID(int id)
        {
            ContactDetails contact = RepositoryManager.ContactDetails_Repository.GetContactDetailsByPersonID(id);
            return contact;
        }

        public static List<ContactDetails> GetAll()
        {

            List<ContactDetails> list = RepositoryManager.ContactDetails_Repository.GetAll();
              
            return list;
        }

    }
}
