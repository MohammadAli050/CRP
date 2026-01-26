using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICourseEvaluationRepository
    {
        List<rCourseEvaluationResult> GetAllCourseEvaluationResultByAcaCalIdAndAcaSecId(int acaCalId, int acaSecId);

        List<rCourseEvaluationComment> GetAllCourseEvaluationCommentByAcaCalIdAndAcaSecId(int acaCalId, int acaSecId);
    }
}
