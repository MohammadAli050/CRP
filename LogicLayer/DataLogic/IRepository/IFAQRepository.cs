using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IFAQRepository
    {
        int Insert(FAQ faq);
        bool Update(FAQ faq);
        bool Delete(int FaqID);
        FAQ GetById(int? FaqID);
        List<FAQ> GetAll();
    }
}

