using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class TabulationResultRemarksManager
    {
        public static int Insert(TabulationResultRemarks tabulationresultremarks)
        {
            int id = RepositoryManager.TabulationResultRemarks_Repository.Insert(tabulationresultremarks);
            return id;
        }

        public static bool Update(TabulationResultRemarks tabulationresultremarks)
        {
            bool isExecute = RepositoryManager.TabulationResultRemarks_Repository.Update(tabulationresultremarks);
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.TabulationResultRemarks_Repository.Delete(id);
            return isExecute;
        }

        public static TabulationResultRemarks GetById(int? id)
        {
            TabulationResultRemarks tabulationresultremarks = RepositoryManager.TabulationResultRemarks_Repository.GetById(id);
            return tabulationresultremarks;
        }

        public static List<TabulationResultRemarks> GetAll()
        { 
            List<TabulationResultRemarks> list = RepositoryManager.TabulationResultRemarks_Repository.GetAll();  
            return list;
        }

        public static List<TabulationResultRemarksDOT> GetAllByProgramSessionBatchStudentId(int ProgramId, int SessionId, int BatchId, int StudentId)
        {
            List<TabulationResultRemarksDOT> list = RepositoryManager.TabulationResultRemarks_Repository.GetAllByProgramSessionBatchStudentId(ProgramId, SessionId, BatchId, StudentId);
            return list;
        }

    }
}

