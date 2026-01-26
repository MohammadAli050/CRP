using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IFrmDsnrDetailRepository
    {
        int Insert(FrmDsnrDetail frmDsnrDetail);
        bool Update(FrmDsnrDetail frmDsnrDetail);
        bool Delete(int id);
        FrmDsnrDetail GetById(int? id);
        List<FrmDsnrDetail> GetAll();
    }
}
