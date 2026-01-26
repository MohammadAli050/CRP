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
    public partial class SQLTeacherCourseSectionRepository : ITeacherCourseSectionRepository
    {

        Database db = null;

        private string sqlInsert = "TeacherCourseSectionInsert";
        private string sqlUpdate = "TeacherCourseSectionUpdate";
        private string sqlDelete = "TeacherCourseSectionDelete";
        private string sqlGetById = "TeacherCourseSectionGetById";
        private string sqlGetAll = "TeacherCourseSectionGetAll";
               
        public int Insert(TeacherCourseSection teachercoursesection)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, teachercoursesection, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "Id");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out id);
                }
            }
            catch (Exception ex)
            {
                id = 0;
            }

            return id;
        }

        public bool Update(TeacherCourseSection teachercoursesection)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, teachercoursesection, isInsert);

                int rowsAffected = db.ExecuteNonQuery(cmd);

                if (rowsAffected > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public bool Delete(int id)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlDelete);

                db.AddInParameter(cmd, "Id", DbType.Int32, id);
                int rowsAffected = db.ExecuteNonQuery(cmd);

                if (rowsAffected > 0)
                {
                    result = true;
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public TeacherCourseSection GetById(int? id)
        {
            TeacherCourseSection _teachercoursesection = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TeacherCourseSection> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TeacherCourseSection>(sqlGetById, rowMapper);
                _teachercoursesection = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _teachercoursesection;
            }

            return _teachercoursesection;
        }

        public List<TeacherCourseSection> GetAll()
        {
            List<TeacherCourseSection> teachercoursesectionList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TeacherCourseSection> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TeacherCourseSection>(sqlGetAll, mapper);
                IEnumerable<TeacherCourseSection> collection = accessor.Execute();

                teachercoursesectionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return teachercoursesectionList;
            }

            return teachercoursesectionList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, TeacherCourseSection teachercoursesection, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, teachercoursesection.Id);
            }

            	
		db.AddInParameter(cmd,"TeacherId",DbType.Int32,teachercoursesection.TeacherId);
		db.AddInParameter(cmd,"CourseId",DbType.Int32,teachercoursesection.CourseId);
		db.AddInParameter(cmd,"SectionId",DbType.Int32,teachercoursesection.SectionId);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,teachercoursesection.CreatedDate);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,teachercoursesection.CreatedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,teachercoursesection.ModifiedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,teachercoursesection.ModifiedBy);
            
            return db;
        }

        private IRowMapper<TeacherCourseSection> GetMaper()
        {
            IRowMapper<TeacherCourseSection> mapper = MapBuilder<TeacherCourseSection>.MapAllProperties()

       	   .Map(m => m.Id).ToColumn("Id")
		.Map(m => m.TeacherId).ToColumn("TeacherId")
		.Map(m => m.CourseId).ToColumn("CourseId")
		.Map(m => m.SectionId).ToColumn("SectionId")
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

