using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SqlCourseEvaluationRepository : ICourseEvaluationRepository
    {
        Database db = null;

        private string sqlGetAllCourseEvaluationResultByAcaCalIdAndAcaSecId = "RptEvaluationResultByAcaCalIdAndAcaSecIdOrTeacherId";
        private string sqlGetAllCourseEvaluationCommentByAcaCalIdAndAcaSecId = "RptEvaluationCommentByAcaCalIdAndAcaSecIdOrTeacherId";

        #region Mapper

        private IRowMapper<rCourseEvaluationResult> GetCourseEvaluationResultMapper()
        {
            IRowMapper<rCourseEvaluationResult> mapper = MapBuilder<rCourseEvaluationResult>.MapAllProperties()

            .Map(m => m.Question).ToColumn("Question")
            .Map(m => m.AnsCountQ1).ToColumn("1")
            .Map(m => m.AnsCountQ2).ToColumn("2")
            .Map(m => m.AnsCountQ3).ToColumn("3")
            .Map(m => m.AnsCountQ4).ToColumn("4")
            .Map(m => m.AnsCountQ5).ToColumn("5")

            .Build();
            return mapper;
        }
        private IRowMapper<rCourseEvaluationComment> GetCourseEvaluationCommentMapper()
        {
            IRowMapper<rCourseEvaluationComment> mapper = MapBuilder<rCourseEvaluationComment>.MapAllProperties()

            .Map(m => m.Comment).ToColumn("Comment")
            
            .Build();
            return mapper;
        }

        #endregion

        public List<rCourseEvaluationResult> GetAllCourseEvaluationResultByAcaCalIdAndAcaSecId(int acaCalId,int acaSecId)
        {
            List<rCourseEvaluationResult> courseList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rCourseEvaluationResult> mapper = GetCourseEvaluationResultMapper();

                var accessor = db.CreateSprocAccessor<rCourseEvaluationResult>(sqlGetAllCourseEvaluationResultByAcaCalIdAndAcaSecId, mapper);
                IEnumerable<rCourseEvaluationResult> collection = accessor.Execute(acaCalId,acaSecId).ToList();

                courseList = collection.ToList();
            }

            catch (Exception ex)
            {
                return courseList;
            }

            return courseList;
        }
        public List<rCourseEvaluationComment> GetAllCourseEvaluationCommentByAcaCalIdAndAcaSecId(int acaCalId, int acaSecId)
        {
            List<rCourseEvaluationComment> courseList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rCourseEvaluationComment> mapper = GetCourseEvaluationCommentMapper();

                var accessor = db.CreateSprocAccessor<rCourseEvaluationComment>(sqlGetAllCourseEvaluationCommentByAcaCalIdAndAcaSecId, mapper);
                IEnumerable<rCourseEvaluationComment> collection = accessor.Execute(acaCalId, acaSecId).ToList();

                courseList = collection.ToList();
            }

            catch (Exception ex)
            {
                return courseList;
            }

            return courseList;
        }
    }
}
