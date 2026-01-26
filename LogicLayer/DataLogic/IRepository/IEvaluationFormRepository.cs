using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IEvaluationFormRepository
    {
        int Insert(EvaluationForm evaluationform);
        bool Update(EvaluationForm evaluationform);
        bool Delete(int Id);
        EvaluationForm GetById(int Id);
        List<EvaluationForm> GetAll();
        EvaluationForm GetBy(int personId, int acaCalSecId);
        List<EvaluationForm> GetAll(int acaCalSecId);
        List<EvaluationForm> GetAllByAcaCalId(int acaCalId);
        List<EvaluationForm> GetAllByPersonId(int personId);
    }
}
