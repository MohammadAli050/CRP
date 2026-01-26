using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentRegistrationRepository
    {
        int Insert(StudentRegistration studentregistration);
        bool Update(StudentRegistration studentregistration);
        bool Delete(int Id);
        StudentRegistration GetById(int? Id);
        List<StudentRegistration> GetAll();
        StudentRegistration GetByStudentId(int studentId);
        List<StudentRegistration> GetAllByProgramBatchStudent(int programId, int batchId, string roll);
        StudentRegistration GetByRegistrationNo(string RegNo);
        StudentRegistration GetByLoginId(string loginId);
    }
}

