using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IScholarshipListRepository
    {
        int Insert(ScholarshipList scholarshiplist);
        bool Update(ScholarshipList scholarshiplist);
        bool Delete(int Id);
        ScholarshipList GetById(int Id);
        List<ScholarshipList> GetAll();
        List<ScholarshipList> GetAll(int acaCalId, string programCode, string fromBatch, string toBatch);
        List<ScholarshipList> GetAllByParameter(int acaCalId, string programCode, string fromBatch, string toBatch);
        List<ScholarshipList> GetAllByAcaCalProg(int acaCalId, string programCode);

        List<StudentMeritListForScholarship> GetStudentMeritListForScholarship(int acaCalId, int programId, int batchId);
        List<StudentMeritListForScholarship> GetStudentMeritListForScholarship2(int acaCalId, int programId, int batchId,decimal registeredCredit);
    }
}