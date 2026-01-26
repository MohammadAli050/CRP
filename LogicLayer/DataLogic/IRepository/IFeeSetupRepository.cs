using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IFeeSetupRepository
    {
        int Insert(FeeSetup feeSetup);
        bool Update(FeeSetup feeSetup);
        bool Delete(int id);
        FeeSetup GetById(int id);
        List<FeeSetup> GetAll();

        FeeSetup GetByTypeDefinationAndSession(int typeDefinitionID, int sessionId);

        FeeSetup GetByTypeDefinationSessionProgram(int typeDefinitionID, int sessionId, int? ProgramID);

        List<FeeSetup> GetByProgramSession(int programId, int batchId);

        List<rFeeSetup> GetFeeSetup(int programId, int batchId);
    }
}
