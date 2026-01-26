using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentTranscriptInfoRepository
    {
        int Insert(StudentTranscriptInfo studenttranscriptinfo);
        bool Update(StudentTranscriptInfo studenttranscriptinfo);
        bool Delete(int Id);
        StudentTranscriptInfo GetById(int? Id);
        StudentTranscriptInfo GetByStudentId(int? studentId);
        List<StudentTranscriptInfo> GetByProgramIdBatchIdRoll(int ProgramId, int BatchId, string Roll);
        List<StudentTranscriptInfo> GetAll();
    }
}
