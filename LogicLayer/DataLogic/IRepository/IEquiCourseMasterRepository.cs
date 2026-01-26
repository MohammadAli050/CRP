using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IEquiCourseMasterRepository
    {
        int Insert(EquiCourseMaster equicoursemaster);
        bool Update(EquiCourseMaster equicoursemaster);
        bool Delete(int EquiCourseMasterId);
        EquiCourseMaster GetById(int? EquiCourseMasterId);
        List<EquiCourseMaster> GetAll();
        BillMaxReceiptNoDTO GetMaxGroupNo();
    }
}

