using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ITabulationResultRemarksRepository
    {
        int Insert(TabulationResultRemarks tabulationresultremarks);
        bool Update(TabulationResultRemarks tabulationresultremarks);
        bool Delete(int Id);
        TabulationResultRemarks GetById(int? Id);
        List<TabulationResultRemarks> GetAll();
        List<TabulationResultRemarksDOT> GetAllByProgramSessionBatchStudentId(int ProgramId, int SessionId, int BatchId, int StudentId);
    }
}

