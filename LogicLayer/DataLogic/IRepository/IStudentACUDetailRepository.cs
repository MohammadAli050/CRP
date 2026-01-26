using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentACUDetailRepository
    {
        int Insert(StudentACUDetail studentacudetail);
        bool Update(StudentACUDetail studentacudetail);
        bool Delete(int id);
        StudentACUDetail GetById(int id);
        List<StudentACUDetail> GetAll(int studentId);
        int UpdateByAcaCalRoll(int studentId, int acaCalId);
        StudentACUDetail GetLatestCGPAByStudentId(int studentId);
        int Calculate_GPAandCGPAByRoll(string roll);
        int Calculate_GPAandCGPAByBatch(string batch);
        int Calculate_GPAandCGPA_Bulk();

        string Calculate_GpaCgpa(int acaCalId, int programId, int batchId, string studentId);

        List<StudentACUDetail> GetAllByAcaCalProgramBatchStudent(int acaCalId, int programId, int batchId, string studentId);
        List<StudentACUDetail> GetAllByAcaCalProgramBatchStudentForRemarks(int semesterNo, int programId, int batchId, string studentId);
        List<rStudentMeritList> GetMeritListByProgramSessionBatch(int programId, int acaCalId, int batchId);
    }
}
