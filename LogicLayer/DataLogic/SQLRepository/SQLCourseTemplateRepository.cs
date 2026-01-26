using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLCourseTemplateRepository : ICourseTemplateRepository
    {

        Database db = null;

        private string sqlGetAll = "CourseTemplateByCourseId";
               


        public CourseTemplate GetByCourseId(int courseId)
        {
            CourseTemplate _coursetemplate = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseTemplate> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CourseTemplate>("CourseTemplateByCourseId", rowMapper);
                _coursetemplate = accessor.Execute(courseId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _coursetemplate;
            }

            return _coursetemplate;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, CourseTemplate coursetemplate, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, coursetemplate.Id);
            }

            	
		db.AddInParameter(cmd,"CourseId",DbType.Int32,coursetemplate.CourseId);
		db.AddInParameter(cmd,"TemplateId",DbType.Int32,coursetemplate.TemplateId);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,coursetemplate.CreatedDate);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,coursetemplate.CreatedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,coursetemplate.ModifiedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,coursetemplate.ModifiedBy);
            
            return db;
        }

        private IRowMapper<CourseTemplate> GetMaper()
        {
            IRowMapper<CourseTemplate> mapper = MapBuilder<CourseTemplate>.MapAllProperties()

       	   .Map(m => m.Id).ToColumn("Id")
		.Map(m => m.CourseId).ToColumn("CourseId")
		.Map(m => m.TemplateId).ToColumn("TemplateId")
		.Map(m => m.CreatedDate).ToColumn("CreatedDate")
		.Map(m => m.CreatedBy).ToColumn("CreatedBy")
		.Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
		.Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            
            .Build();

            return mapper;
        }
        #endregion

    }
}

