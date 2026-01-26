using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IFrmDsnrMasterRepository
    {
        int Insert(FrmDsnrMaster frmDsnrMaster);
        bool Update(FrmDsnrMaster frmDsnrMaster);
        bool Delete(int id);
        FrmDsnrMaster GetById(int? id);
        List<FrmDsnrMaster> GetAll();
    }
}
