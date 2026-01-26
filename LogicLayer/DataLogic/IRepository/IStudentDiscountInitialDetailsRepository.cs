using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentDiscountInitialDetailsRepository
    {
        int Insert(StudentDiscountInitialDetails studentdiscountinitialdetails);
        bool Update(StudentDiscountInitialDetails studentdiscountinitialdetails);
        bool Delete(int StudentDiscountInitialDetailsId);
        StudentDiscountInitialDetails GetById(int StudentDiscountInitialDetailsId);
        List<StudentDiscountInitialDetails> GetAll();

        List<StudentDiscountInitialDetails> GetByStudentDiscountId(int StudentDiscountId);

        List<StudentDiscountInitialDetailsDTO> GetByStudentDiscountId(int programId, int acaCalId, string roll);
    }
}

