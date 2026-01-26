using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IBatchRepository
    {
        int Insert(Batch batch);
        bool Update(Batch batch);
        bool Delete(int BatchId);
        Batch GetById(int BatchId);
        List<Batch> GetAll();

        List<Batch> GetByAcaCalSectionID(int AcaCalSectionID);

        List<Batch> GetAllByProgram(int programId);

        List<rBatchListByProgram> GetBatchListByProgram(int programId);
    }
}

