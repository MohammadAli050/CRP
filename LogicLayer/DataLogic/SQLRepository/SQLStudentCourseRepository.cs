using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.SQLRepository
{
    partial class SQLStudentCourseRepository : IStudentCourseRepository
    {
        Database db = null;

        private string sqlInsert = "Student_CourseInsert";
        private string sqlUpdate = "Student_CourseUpdate";
        private string sqlDelete = "Student_CourseDeleteById";
        private string sqlGetById = "Student_CourseGetById";
        private string sqlGetAll = "Student_CourseGetAll";


        public int Insert(StudentCourse student_Course)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, student_Course, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "Student_CourseID");

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

        public bool Update(StudentCourse student_Course)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, student_Course, isInsert);

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

                db.AddInParameter(cmd, "Student_CourseID", DbType.Int32, id);
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

        public StudentCourse GetById(int? id)
        {
            StudentCourse _student_Course = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourse> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourse>(sqlGetById, rowMapper);
                _student_Course = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _student_Course;
            }

            return _student_Course;
        }

        public List<StudentCourse> GetAll()
        {
            List<StudentCourse> student_CourseList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourse> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourse>(sqlGetAll, mapper);
                IEnumerable<StudentCourse> collection = accessor.Execute();

                student_CourseList = collection.ToList();
            }

            catch (Exception ex)
            {
                return student_CourseList;
            }

            return student_CourseList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StudentCourse student_Course, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Student_CourseID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Student_CourseID", DbType.Int32, student_Course.Student_CourseID);
            }

            db.AddInParameter(cmd, "Student_CourseID", DbType.Int32, student_Course.Student_CourseID);
            db.AddInParameter(cmd, "StudentID", DbType.Int32, student_Course.StudentID);
            db.AddInParameter(cmd, "StdAcademicCalenderID", DbType.Int32, student_Course.StdAcademicCalenderID);
            db.AddInParameter(cmd, "DscntSetUpID", DbType.Int32, student_Course.DscntSetUpID);
            db.AddInParameter(cmd, "RetakeNo", DbType.Int32, student_Course.RetakeNo);
            db.AddInParameter(cmd, "Node_CourseID", DbType.Int32, student_Course.Node_CourseID);
            db.AddInParameter(cmd, "CourseID", DbType.Int32, student_Course.CourseID);
            db.AddInParameter(cmd, "VersionID", DbType.Int32, student_Course.VersionID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, student_Course.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, student_Course.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, student_Course.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, student_Course.ModifiedDate);

            return db;
        }

        private IRowMapper<StudentCourse> GetMaper()
        {
            IRowMapper<StudentCourse> mapper = MapBuilder<StudentCourse>.MapAllProperties()
            .Map(m => m.Student_CourseID).ToColumn("Student_CourseID")
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.StdAcademicCalenderID).ToColumn("StdAcademicCalenderID")
            .Map(m => m.DscntSetUpID).ToColumn("DscntSetUpID")
             .Map(m => m.RetakeNo).ToColumn("RetakeNo")
            .Map(m => m.Node_CourseID).ToColumn("Node_CourseID")
            .Map(m => m.CourseID).ToColumn("CourseID")
            .Map(m => m.VersionID).ToColumn("VersionID")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Build();

            return mapper;
        }
        #endregion
    }
}
