using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IVoucherRepository
    {
        int Insert(Voucher voucher);
        bool Update(Voucher voucher);
        bool Delete(int VoucherID);
        Voucher GetById(int VoucherID);
        List<Voucher> GetAll();

        int Insert(List<Voucher> voucherList);
    }
}

