using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogicLayer.BusinessLogic
{
    public class CourseEvaluationManager
    {
        public static List<rCourseEvaluationResult> GetAllCourseEvaluationResultByAcaCalIdAndAcaSecId(int acaCalId, int acaSecId)
        {
            List<rCourseEvaluationResult> list = RepositoryManager.CourseEvaluation_Repository.GetAllCourseEvaluationResultByAcaCalIdAndAcaSecId(acaCalId, acaSecId);

            return list;
        }

        public static List<rCourseEvaluationComment> GetAllCourseEvaluationCommentByAcaCalIdAndAcaSecId(int acaCalId, int acaSecId)
        {
            List<rCourseEvaluationComment> list = RepositoryManager.CourseEvaluation_Repository.GetAllCourseEvaluationCommentByAcaCalIdAndAcaSecId(acaCalId, acaSecId);

            return list;
        }

    }
}
