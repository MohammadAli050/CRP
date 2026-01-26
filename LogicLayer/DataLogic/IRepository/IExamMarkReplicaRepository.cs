using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamMarkReplicaRepository
    {
        ExamMarkReplica GetById(int SlNo);
        List<ExamMarkReplica> GetAll();
        int InsertByAcaCalAcaCalSecRoll(int acaCalId, int acaCalSecId, string roll);
    }
}

