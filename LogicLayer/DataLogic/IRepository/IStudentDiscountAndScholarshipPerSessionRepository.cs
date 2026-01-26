using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentDiscountAndScholarshipPerSessionRepository
    {
        int Insert(StudentDiscountAndScholarshipPerSession studentdiscountandscholarshippersession);
        bool Update(StudentDiscountAndScholarshipPerSession studentdiscountandscholarshippersession);
        bool Delete(int StudentDiscountAndScholarshipId);
        StudentDiscountAndScholarshipPerSession GetById(int StudentDiscountAndScholarshipId);
        List<StudentDiscountAndScholarshipPerSession> GetAll();
        List<StudentDiscountAndScholarshipPerSession> GetAllBySessionIDProgramID(int sessionId, int programId);

        bool Delete(int studentId, int sessionId, int tdId);

        List<StudentDiscountAndScholarshipPerSessionCount> getCountByProgramBatch(int sessionId);
    }
}

