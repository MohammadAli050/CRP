using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentDiscountCurrentDetailsRepository
    {
        int Insert(StudentDiscountCurrentDetails studentdiscountcurrentdetails);
        bool Update(StudentDiscountCurrentDetails studentdiscountcurrentdetails);
        bool Delete(int StudentDiscountCurrentDetailsId);
        StudentDiscountCurrentDetails GetById(int StudentDiscountCurrentDetailsId);
        List<StudentDiscountCurrentDetails> GetAll();

        List<StudentDiscountCurrentDetails> GetByStudentDiscountId(int StudentDiscountId);

        bool GenetareCurrentDiscount(int acaCalBatch, int acaCalSession, int program);

        List<StudentDiscountCurrentDetails> GetByStudentDiscountAndAcaCalSession(int StudentDiscountId, int AcaCalSessionId);

        bool DiscountTransferFromInitialToCurrentPerStudent(int student, int batchId, int sessionId, int programId);

        List<StudentDiscountCurrentDetailsDTO> GetAllDiscountCurrentByProgramBatchRoll(int programId, int acaCalBatchId, int acaCalSessionId, string roll);

        StudentDiscountCurrentDetails GetBy(int StudentDiscountId, int typeDefinitionId, int acaCalSessionId);

        bool DiscountPostingWaiver(int studentId, int batchId, int programId, int sessionId, int discountTypeId);

        bool Delete(int studentId, int sessionId);
    }
}

