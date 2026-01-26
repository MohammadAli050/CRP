using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICollectionHistoryRepository
    {
        int Insert(CollectionHistory collectionhistory);
        bool Update(CollectionHistory collectionhistory);
        bool Delete(int CollectionHistoryId);
        CollectionHistory GetById(int? CollectionHistoryId);
        List<CollectionHistory> GetAll();
        CollectionHistory IsDuplicateMoneyReceipt(string moneyReceiptNo, string paymentType);
    }
}

