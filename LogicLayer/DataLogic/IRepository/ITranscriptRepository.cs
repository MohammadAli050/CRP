using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
namespace LogicLayer.DataLogic.IRepository
{
    public interface ITranscriptRepository
    {
        List<TranscriptResultDetails> GetResultByStudentId(string roll);
        List<TranscriptStudentInfo> GetInfoByStudentId(string roll);
        List<TranscriptTransferDetails> GetTransferResultStudentId(string id);
        List<TranscriptTransferDetails> GetWaiverResultStudentId(string id);
    }
}
