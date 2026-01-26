using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStdEducationInfoRepository
    {
        int Insert(StdEducationInfo stdEducationInfo);
        bool Update(StdEducationInfo stdEducationInfo);
        bool Delete(int id);
        StdEducationInfo GetById(int? id);
        List<StdEducationInfo> GetAll();
    }
}
