using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentDiscountMasterRepository
    {
        int Insert(StudentDiscountMaster studentdiscountmaster);
        bool Update(StudentDiscountMaster studentdiscountmaster);
        bool Delete(int StudentDiscountId);
        StudentDiscountMaster GetById(int StudentDiscountId);
        List<StudentDiscountMaster> GetAll();

        List<StudentDiscountMaster> GetByAcaCalIDProgramID(int AcaCalID,int ProgramID);

        StudentDiscountMaster GetByStudentID(int StudentID);
    }
}

