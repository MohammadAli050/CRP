using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IAdmissionRepository
    {
        int Insert(Admission admission);
        bool Update(Admission admission);
        bool Delete(int id);
        Admission GetById(int? id);
        Admission GetByStudentId(int studentID);
        List<Admission> GetAll();
    }
}
