using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IAddressRepository
    {
        int Insert(Address address);
        bool Update(Address address);
        bool Delete(int id);
        Address GetById(int id);
        List<Address> GetAll();
        List<AddressByRoll> GetAddressByRoll(string roll);
        List<Address> GetAddressByPersonId(int personId);
    }
}
